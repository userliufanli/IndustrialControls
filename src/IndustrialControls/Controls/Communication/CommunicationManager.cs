using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IndustrialControls.Utilities;

namespace IndustrialControls.Controls.Communication
{
    /// <summary>
    /// 工业通讯管理器，支持 TCP 和串口通讯模式。
    /// 提供异步连接、数据发送/接收、状态监控和完整的资源释放功能。
    /// </summary>
    public class CommunicationManager : Component
    {
        private CommunicationMode _mode = CommunicationMode.Tcp;
        private CommunicationState _state = CommunicationState.Disconnected;
        private long _bytesSent;
        private long _bytesReceived;

        // TCP 参数
        private string _tcpIp = "127.0.0.1";
        private int _tcpPort = 502;
        private int _tcpTimeout = 5000;

        // TCP 服务端参数
        private string _tcpServerIp = "0.0.0.0";
        private int _tcpServerPort = 502;

        // 串口参数
        private string _serialPortName = "COM1";
        private int _serialBaudRate = 9600;
        private Parity _serialParity = Parity.None;
        private int _serialDataBits = 8;
        private StopBits _serialStopBits = StopBits.One;

        // 运行时组件
        private TcpListener _tcpListener;
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        private SerialPort _serialPort;
        private CancellationTokenSource _cts;
        private Task _readTask;

        // 多客户端管理（TCP服务端模式）
        private readonly ConcurrentDictionary<string, TcpClient> _clients = new ConcurrentDictionary<string, TcpClient>();
        private readonly ConcurrentDictionary<string, NetworkStream> _clientStreams = new ConcurrentDictionary<string, NetworkStream>();
        private readonly ConcurrentDictionary<string, Task> _clientReadTasks = new ConcurrentDictionary<string, Task>();
        private Task _acceptTask;

        private SynchronizationContext _syncContext;
        private readonly object _stateLock = new object();
        private readonly SemaphoreSlim _operationLock = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);
        private volatile bool _disposed = false;

        /// <summary>断开时等待操作锁的首段时长；TCP 连接等可能长时间占用锁。</summary>
        private static readonly TimeSpan DisconnectPrimaryLockWait = TimeSpan.FromSeconds(30);

        /// <summary>强制关闭资源后再次等待操作锁的时长（用于解除卡在 Accept/Open 上的锁）。</summary>
        private static readonly TimeSpan DisconnectSecondaryLockWait = TimeSpan.FromSeconds(15);

        // 帧解析
        private FrameParser _frameParser;
        private readonly ConcurrentDictionary<string, FrameParser> _clientFrameParsers = new ConcurrentDictionary<string, FrameParser>();

        // 增强统计
        private long _framesSent;
        private long _framesReceived;
        private long _errorCount;

        /// <summary>
        /// 状态变更事件
        /// </summary>
        public event EventHandler<CommunicationState> StateChanged;

        /// <summary>
        /// 数据接收事件（原始字节）
        /// </summary>
        public event EventHandler<byte[]> DataReceived;

        /// <summary>
        /// 字符串数据接收事件（使用 DataEncoding 自动解码，适用于所有通讯模式）
        /// </summary>
        public event EventHandler<string> StringReceived;

        /// <summary>
        /// 错误发生事件
        /// </summary>
        public event EventHandler<Exception> ErrorOccurred;

        /// <summary>客户端连接事件（TCP服务端模式）</summary>
        public event EventHandler<ClientEventArgs> ClientConnected;

        /// <summary>客户端断开事件（TCP服务端模式）</summary>
        public event EventHandler<ClientEventArgs> ClientDisconnected;

        /// <summary>带客户端来源的数据接收事件（TCP服务端模式）</summary>
        public event EventHandler<ClientDataEventArgs> ClientDataReceived;

        /// <summary>带客户端来源的字符串接收事件（TCP服务端模式，使用 DataEncoding 自动解码）</summary>
        public event EventHandler<ClientStringEventArgs> ClientStringReceived;

        /// <summary>帧接收事件（分隔符解析后的完整数据帧）</summary>
        public event EventHandler<FrameReceivedEventArgs> FrameReceived;

        /// <summary>带客户端来源的帧接收事件（TCP服务端模式）</summary>
        public event EventHandler<ClientFrameReceivedEventArgs> ClientFrameReceived;

        /// <summary>
        /// 初始化 <see cref="CommunicationManager"/> 的新实例（捕获当前 <see cref="SynchronizationContext"/> 用于事件投递）
        /// </summary>
        public CommunicationManager()
            : this(SynchronizationContext.Current)
        {
        }

        /// <summary>
        /// 使用指定同步上下文初始化，便于在非 UI 线程构造时仍把事件投递到界面线程。
        /// </summary>
        /// <param name="synchronizationContext">为 null 时使用默认同步上下文（不自动切到 UI 线程）</param>
        public CommunicationManager(SynchronizationContext synchronizationContext)
        {
            _syncContext = synchronizationContext ?? new SynchronizationContext();
        }

        /// <summary>
        /// 将状态/数据等事件的投递目标切换到指定同步上下文（例如 WinForms 窗体所在 UI 线程的同步上下文）。
        /// 应在宿主控件已处于 UI 线程且具备有效上下文后调用，以修复构造时 <see cref="SynchronizationContext.Current"/> 为空导致的界面不刷新。
        /// </summary>
        public void UseSynchronizationContext(SynchronizationContext synchronizationContext)
        {
            if (synchronizationContext != null)
                _syncContext = synchronizationContext;
        }

        /// <summary>
        /// 获取或设置通讯模式
        /// </summary>
        /// <remarks>
        /// 切换模式时当前状态必须为 <see cref="CommunicationState.Disconnected"/>。
        /// 如果当前状态为 <see cref="CommunicationState.Error"/>，会自动断开连接后允许切换。
        /// </remarks>
        /// <exception cref="InvalidOperationException">当前不处于未连接状态时切换模式会抛出此异常</exception>
        [Category("通讯")]
        [Description("通讯模式：TCP 或串口")]
        [DefaultValue(CommunicationMode.Tcp)]
        /// <remarks>
        /// 此属性应在 UI 线程上调用。如果当前状态为 Error，会自动清理后允许切换。
        /// </remarks>
        public CommunicationMode Mode
        {
            get => _mode;
            set
            {
                if (_mode == value) return;

                // 先处理 Error 状态（在锁外避免死锁）
                if (State == CommunicationState.Error)
                {
                    try { DisconnectInternal(); } catch { }
                    lock (_stateLock) { _state = CommunicationState.Disconnected; }
                }

                var currentState = State;
                if (currentState != CommunicationState.Disconnected)
                {
                    throw new InvalidOperationException("请先断开连接再切换模式");
                }

                _mode = value;
            }
        }

        /// <summary>
        /// 获取当前通讯状态
        /// </summary>
        [Browsable(false)]
        public CommunicationState State
        {
            get
            {
                lock (_stateLock)
                {
                    return _state;
                }
            }
        }

        /// <summary>
        /// 获取已发送字节数
        /// </summary>
        [Browsable(false)]
        public long BytesSent => Interlocked.Read(ref _bytesSent);

        /// <summary>
        /// 获取已接收字节数
        /// </summary>
        [Browsable(false)]
        public long BytesReceived => Interlocked.Read(ref _bytesReceived);

        /// <summary>当前连接的客户端数量（TCP服务端模式）</summary>
        [Browsable(false)]
        public int ClientCount => _clients.Count;

        /// <summary>
        /// 获取或设置 TCP 目标 IP 地址
        /// </summary>
        [Category("TCP")]
        [Description("TCP 目标 IP 地址")]
        [DefaultValue("127.0.0.1")]
        public string TcpIp
        {
            get => _tcpIp;
            set => _tcpIp = value ?? "127.0.0.1";
        }

        /// <summary>
        /// 获取或设置 TCP 目标端口号
        /// </summary>
        [Category("TCP")]
        [Description("TCP 目标端口号")]
        [DefaultValue(502)]
        public int TcpPort
        {
            get => _tcpPort;
            set => _tcpPort = value;
        }

        /// <summary>
        /// 获取或设置 TCP 连接超时时间（毫秒）
        /// </summary>
        [Category("TCP")]
        [Description("TCP 连接超时时间（毫秒）")]
        [DefaultValue(5000)]
        public int TcpTimeout
        {
            get => _tcpTimeout;
            set => _tcpTimeout = Math.Max(100, value);
        }

        /// <summary>
        /// 获取或设置 TCP 服务端监听 IP 地址
        /// </summary>
        [Category("TCP服务端")]
        [Description("TCP 服务端监听 IP 地址")]
        [DefaultValue("0.0.0.0")]
        public string TcpServerIp
        {
            get => _tcpServerIp;
            set => _tcpServerIp = value ?? "0.0.0.0";
        }

        /// <summary>
        /// 获取或设置 TCP 服务端监听端口号
        /// </summary>
        [Category("TCP服务端")]
        [Description("TCP 服务端监听端口号")]
        [DefaultValue(502)]
        public int TcpServerPort
        {
            get => _tcpServerPort;
            set => _tcpServerPort = value;
        }

        /// <summary>
        /// 获取或设置串口名称
        /// </summary>
        [Category("串口")]
        [Description("串口名称，如 COM1")]
        [DefaultValue("COM1")]
        public string SerialPortName
        {
            get => _serialPortName;
            set => _serialPortName = value ?? "COM1";
        }

        /// <summary>
        /// 获取或设置串口波特率
        /// </summary>
        [Category("串口")]
        [Description("串口波特率")]
        [DefaultValue(9600)]
        public int SerialBaudRate
        {
            get => _serialBaudRate;
            set => _serialBaudRate = value;
        }

        /// <summary>
        /// 获取或设置串口校验位
        /// </summary>
        [Category("串口")]
        [Description("串口校验位")]
        [DefaultValue(Parity.None)]
        public Parity SerialParity
        {
            get => _serialParity;
            set => _serialParity = value;
        }

        /// <summary>
        /// 获取或设置串口数据位
        /// </summary>
        [Category("串口")]
        [Description("串口数据位")]
        [DefaultValue(8)]
        public int SerialDataBits
        {
            get => _serialDataBits;
            set => _serialDataBits = value;
        }

        /// <summary>
        /// 获取或设置串口停止位
        /// </summary>
        [Category("串口")]
        [Description("串口停止位")]
        [DefaultValue(StopBits.One)]
        public StopBits SerialStopBits
        {
            get => _serialStopBits;
            set => _serialStopBits = value;
        }

        /// <summary>帧分隔符（默认 \r\n）</summary>
        [Category("帧解析")]
        [Description("帧分隔符字节序列（默认 \\r\\n）")]
        [Browsable(false)]
        public byte[] FrameDelimiter { get; set; } = new byte[] { 0x0D, 0x0A };

        /// <summary>字符串编码（发送/接收字符串时使用）</summary>
        [Category("帧解析")]
        [Description("字符串编码")]
        [Browsable(false)]
        public Encoding DataEncoding { get; set; } = Encoding.UTF8;

        /// <summary>已发送帧数</summary>
        [Browsable(false)]
        public long FramesSent => Interlocked.Read(ref _framesSent);

        /// <summary>已接收帧数</summary>
        [Browsable(false)]
        public long FramesReceived => Interlocked.Read(ref _framesReceived);

        /// <summary>错误计数</summary>
        [Browsable(false)]
        public long ErrorCount => Interlocked.Read(ref _errorCount);

        /// <summary>
        /// 异步连接到目标设备
        /// </summary>
        /// <exception cref="InvalidOperationException">当前已处于连接状态时抛出</exception>
        /// <exception cref="TimeoutException">TCP 连接超时</exception>
        public async Task ConnectAsync()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(CommunicationManager));

            await _operationLock.WaitAsync().ConfigureAwait(false);
            try
            {
                // 允许从 Disconnected 和 Error 状态发起连接
                if (State == CommunicationState.Error)
                {
                    // Error 状态自动清理，恢复到可连接状态
                    DisconnectInternal();
                    lock (_stateLock) { _state = CommunicationState.Disconnected; }
                }

                if (State != CommunicationState.Disconnected)
                    throw new InvalidOperationException("当前已处于连接状态，请先断开连接");

                // 重置收发统计
                Interlocked.Exchange(ref _bytesSent, 0);
                Interlocked.Exchange(ref _bytesReceived, 0);
                Interlocked.Exchange(ref _framesSent, 0);
                Interlocked.Exchange(ref _framesReceived, 0);
                Interlocked.Exchange(ref _errorCount, 0);

                // 初始化帧解析器
                _frameParser = new FrameParser { Delimiter = FrameDelimiter, MaxBufferSize = 65536 };
                _clientFrameParsers.Clear();

                // 清理可能遗留的旧CTS
                _cts?.Dispose();
                _cts = new CancellationTokenSource();
                SetState(CommunicationState.Connecting);

                if (_mode == CommunicationMode.Tcp)
                {
                    await ConnectTcpAsync(_cts.Token).ConfigureAwait(false);
                }
                else if (_mode == CommunicationMode.TcpServer)
                {
                    await StartTcpServerAsync(_cts.Token).ConfigureAwait(false);
                }
                else
                {
                    ConnectSerial();
                }

                SetState(CommunicationState.Connected);
            }
            catch (Exception)
            {
                DisconnectInternal();
                SetState(CommunicationState.Error);
                throw;
            }
            finally
            {
                _operationLock.Release();
            }
        }

        private async Task ConnectTcpAsync(CancellationToken ct)
        {
            _tcpClient = new TcpClient();
            var connectTask = _tcpClient.ConnectAsync(_tcpIp, _tcpPort);
            var timeoutTask = Task.Delay(_tcpTimeout, ct);

            var completedTask = await Task.WhenAny(connectTask, timeoutTask).ConfigureAwait(false);

            if (completedTask == timeoutTask)
            {
                // 超时：释放 TcpClient
                try { _tcpClient?.Dispose(); } catch { }
                _tcpClient = null;

                // 避免 connectTask 后续以 Faulted 结束却无人观察导致未处理异常
                _ = connectTask.ContinueWith(t => { _ = t.Exception; },
                    CancellationToken.None,
                    TaskContinuationOptions.OnlyOnFaulted,
                    TaskScheduler.Default);

                // 检查是否是取消导致的
                ct.ThrowIfCancellationRequested();
                throw new TimeoutException($"TCP 连接超时（{_tcpTimeout}ms）");
            }

            // 连接完成，检查是否有异常
            await connectTask.ConfigureAwait(false);
            _networkStream = _tcpClient.GetStream();
            _readTask = Task.Run(() => TcpReadLoopAsync(_networkStream, ct), ct);
        }

        private async Task StartTcpServerAsync(CancellationToken ct)
        {
            IPAddress ipAddress;
            if (_tcpServerIp == "0.0.0.0")
                ipAddress = IPAddress.Any;
            else if (!IPAddress.TryParse(_tcpServerIp, out ipAddress))
                ipAddress = IPAddress.Any;

            _tcpListener = new TcpListener(ipAddress, _tcpServerPort);
            _tcpListener.Start();

            // 启动接受循环（作为 _readTask 以复用断开等待逻辑）
            _acceptTask = AcceptClientsLoopAsync(ct);
            _readTask = _acceptTask;
        }

        private async Task AcceptClientsLoopAsync(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    var acceptTask = _tcpListener.AcceptTcpClientAsync();
                    var cancelTask = Task.Delay(Timeout.Infinite, ct);
                    var completed = await Task.WhenAny(acceptTask, cancelTask).ConfigureAwait(false);

                    if (completed == cancelTask)
                    {
                        try { _tcpListener?.Stop(); } catch { }
                        break;
                    }

                    var client = await acceptTask.ConfigureAwait(false);
                    var clientId = client.Client.RemoteEndPoint?.ToString() ?? Guid.NewGuid().ToString();
                    var stream = client.GetStream();

                    _clients[clientId] = client;
                    _clientStreams[clientId] = stream;

                    // 为此客户端启动独立的读循环
                    var readTask = Task.Run(() => ClientReadLoopAsync(clientId, stream, ct), CancellationToken.None);
                    _clientReadTasks[clientId] = readTask;

                    OnClientConnected(clientId);
                }
            }
            catch (ObjectDisposedException) when (ct.IsCancellationRequested) { }
            catch (SocketException) when (ct.IsCancellationRequested) { }
            catch (Exception ex)
            {
                if (!_disposed && !ct.IsCancellationRequested)
                    OnErrorOccurred(ex);
            }
        }

        private void ConnectSerial()
        {
            _serialPort = new SerialPort(
                _serialPortName,
                _serialBaudRate,
                _serialParity,
                _serialDataBits,
                _serialStopBits);
            _serialPort.DataReceived += SerialPort_DataReceived;
            _serialPort.ErrorReceived += SerialPort_ErrorReceived;
            _serialPort.Open();
        }

        /// <summary>
        /// 异步断开连接（推荐，不阻塞调用线程）
        /// </summary>
        /// <remarks>
        /// 使用带超时的锁获取策略，防止在 TCP 服务端等待客户端连接期间
        /// ConnectAsync 持有锁导致的死锁问题。超时后强制关闭资源。
        /// </remarks>
        public async Task DisconnectAsync()
        {
            if (_disposed) return;

            bool lockAcquired = await TryAcquireOperationLockForDisconnectAsync().ConfigureAwait(false);
            if (!lockAcquired)
            {
                OnErrorOccurred(new TimeoutException(
                    "断开连接时无法在限时内取得操作锁；已尝试取消并关闭底层套接字/串口。若状态仍异常，请稍后重试或重启应用。"));
                return;
            }

            try
            {
                // 第一步：取消 CTS，通知所有循环和等待中的操作退出
                try { _cts?.Cancel(); } catch { }

                // 第二步：立即关闭底层 I/O 资源，强制中断阻塞的 ReadAsync/AcceptAsync
                // 在 .NET Framework 4.8 中，NetworkStream.ReadAsync 和 AcceptTcpClientAsync
                // 不会响应 CancellationToken，必须通过关闭底层资源来中断阻塞
                ForceCloseResources();

                // 第三步：等待读取任务退出（资源已关闭，应很快退出）
                if (_readTask != null && !_readTask.IsCompleted)
                {
                    try { await Task.WhenAny(_readTask, Task.Delay(3000)).ConfigureAwait(false); }
                    catch { }
                }
                _readTask = null;

                // 第四步：完整释放剩余资源
                DisconnectInternal();
                SetState(CommunicationState.Disconnected);
            }
            finally
            {
                _operationLock.Release();
            }
        }

        /// <summary>
        /// 先等待操作锁；若超时则取消并强制关闭 I/O 以解除可能卡在串口 Open / Accept 上的连接，再尝试获取锁。
        /// </summary>
        private async Task<bool> TryAcquireOperationLockForDisconnectAsync()
        {
            if (await _operationLock.WaitAsync(DisconnectPrimaryLockWait).ConfigureAwait(false))
                return true;

            try { _cts?.Cancel(); } catch { }
            ForceCloseResources();
            return await _operationLock.WaitAsync(DisconnectSecondaryLockWait).ConfigureAwait(false);
        }

        /// <summary>同步版：用于 <see cref="Disconnect"/> 与释放路径。</summary>
        private bool TryAcquireOperationLockForDisconnect()
        {
            if (_operationLock.Wait(DisconnectPrimaryLockWait))
                return true;

            try { _cts?.Cancel(); } catch { }
            ForceCloseResources();
            return _operationLock.Wait(DisconnectSecondaryLockWait);
        }

        /// <summary>
        /// 同步断开连接
        /// </summary>
        /// <remarks>
        /// 使用带超时的锁获取策略，防止死锁。超时后强制关闭资源。
        /// </remarks>
        public void Disconnect()
        {
            if (_disposed) return;

            bool lockAcquired = TryAcquireOperationLockForDisconnect();
            if (!lockAcquired)
            {
                OnErrorOccurred(new TimeoutException(
                    "断开连接时无法在限时内取得操作锁；已尝试取消并关闭底层套接字/串口。若状态仍异常，请稍后重试或重启应用。"));
                return;
            }

            try
            {
                // 取消 CTS
                try { _cts?.Cancel(); } catch { }

                // 立即关闭底层 I/O 资源，强制中断阻塞操作
                ForceCloseResources();

                // 等待读取任务退出
                if (_readTask != null && !_readTask.IsCompleted)
                {
                    try { _readTask.Wait(TimeSpan.FromSeconds(3)); }
                    catch { }
                }
                _readTask = null;

                DisconnectInternal();
                SetState(CommunicationState.Disconnected);
            }
            finally
            {
                _operationLock.Release();
            }
        }

        /// <summary>强制关闭所有底层资源，中断阻塞的I/O操作</summary>
        private void ForceCloseResources()
        {
            // 清理所有客户端连接（TCP服务端多客户端模式）
            foreach (var stream in _clientStreams.Values)
            {
                try { stream.Dispose(); } catch { }
            }
            foreach (var client in _clients.Values)
            {
                try { client.Close(); } catch { }
            }

            try { _tcpListener?.Stop(); } catch { }
            try { _networkStream?.Dispose(); } catch { }
            try { _tcpClient?.Close(); } catch { }
            try { _serialPort?.Close(); } catch { }
        }

        private void DisconnectInternal()
        {
            _cts?.Cancel();

            // 清理多客户端集合
            _clientStreams.Clear();
            _clients.Clear();
            _clientReadTasks.Clear();
            _clientFrameParsers.Clear();
            _frameParser?.Reset();
            _acceptTask = null;

            try { _networkStream?.Dispose(); } catch { }
            _networkStream = null;

            try { _tcpClient?.Close(); _tcpClient?.Dispose(); } catch { }
            _tcpClient = null;

            try { _tcpListener?.Stop(); } catch { }
            _tcpListener = null;

            try
            {
                if (_serialPort != null)
                {
                    _serialPort.DataReceived -= SerialPort_DataReceived;
                    _serialPort.ErrorReceived -= SerialPort_ErrorReceived;
                    if (_serialPort.IsOpen)
                        _serialPort.Close();
                    _serialPort.Dispose();
                }
            }
            catch { }
            _serialPort = null;

            _cts?.Dispose();
            _cts = null;
        }

        /// <summary>
        /// 异步发送数据
        /// </summary>
        /// <param name="data">要发送的字节数据</param>
        /// <exception cref="ArgumentNullException">data 为 null 时抛出</exception>
        /// <exception cref="InvalidOperationException">未连接时抛出</exception>
        public async Task SendAsync(byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) return;
            if (_disposed) throw new ObjectDisposedException(nameof(CommunicationManager));

            await _sendLock.WaitAsync().ConfigureAwait(false);
            try
            {
                // 在锁内检查状态
                if (State != CommunicationState.Connected)
                    throw new InvalidOperationException("未连接，无法发送数据");

                if (_mode == CommunicationMode.TcpServer)
                {
                    // 广播给所有已连接客户端
                    var streams = _clientStreams.Values.ToArray();
                    if (streams.Length == 0)
                        throw new InvalidOperationException("无已连接的客户端");
                    foreach (var s in streams)
                    {
                        try
                        {
                            await s.WriteAsync(data, 0, data.Length).ConfigureAwait(false);
                        }
                        catch { /* 单个客户端写入失败不影响其他 */ }
                    }
                }
                else if (_mode == CommunicationMode.Tcp)
                {
                    if (_networkStream == null)
                        throw new InvalidOperationException("网络流未初始化");
                    await _networkStream.WriteAsync(data, 0, data.Length).ConfigureAwait(false);
                }
                else
                {
                    if (_serialPort == null || !_serialPort.IsOpen)
                        throw new InvalidOperationException("串口未打开");
                    var stream = _serialPort.BaseStream;
                    await stream.WriteAsync(data, 0, data.Length).ConfigureAwait(false);
                }

                Interlocked.Add(ref _bytesSent, data.Length);
                Interlocked.Increment(ref _framesSent);
            }
            catch (ObjectDisposedException)
            {
                // 连接已被释放，安全忽略
            }
            catch (Exception ex) when (!(ex is InvalidOperationException || ex is ArgumentNullException))
            {
                OnErrorOccurred(ex);
                throw;
            }
            finally
            {
                _sendLock.Release();
            }
        }

        /// <summary>
        /// 向指定客户端发送数据（仅TCP服务端模式）
        /// </summary>
        /// <param name="clientId">客户端标识（RemoteEndPoint）</param>
        /// <param name="data">要发送的字节数据</param>
        public async Task SendToClientAsync(string clientId, byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) return;
            if (string.IsNullOrEmpty(clientId)) throw new ArgumentNullException(nameof(clientId));
            if (_disposed) throw new ObjectDisposedException(nameof(CommunicationManager));
            if (_mode != CommunicationMode.TcpServer)
                throw new InvalidOperationException("仅TCP服务端模式支持定向发送");

            await _sendLock.WaitAsync().ConfigureAwait(false);
            try
            {
                if (State != CommunicationState.Connected)
                    throw new InvalidOperationException("未连接，无法发送数据");

                if (_clientStreams.TryGetValue(clientId, out var stream))
                {
                    await stream.WriteAsync(data, 0, data.Length).ConfigureAwait(false);
                    Interlocked.Add(ref _bytesSent, data.Length);
                    Interlocked.Increment(ref _framesSent);
                }
                else
                {
                    throw new InvalidOperationException($"客户端 '{clientId}' 不存在或已断开");
                }
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception ex) when (!(ex is InvalidOperationException || ex is ArgumentNullException))
            {
                OnErrorOccurred(ex);
                throw;
            }
            finally
            {
                _sendLock.Release();
            }
        }

        /// <summary>发送字符串数据（使用 DataEncoding 编码）</summary>
        public Task SendAsync(string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            return SendAsync(DataEncoding.GetBytes(text));
        }

        /// <summary>广播数据到所有客户端（仅TCP服务端模式，等效于 SendAsync）</summary>
        public Task SendToAllAsync(byte[] data)
        {
            if (_mode != CommunicationMode.TcpServer)
                throw new InvalidOperationException("仅TCP服务端模式支持广播发送");
            return SendAsync(data);
        }

        /// <summary>广播字符串到所有客户端（仅TCP服务端模式）</summary>
        public Task SendToAllAsync(string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            return SendToAllAsync(DataEncoding.GetBytes(text));
        }

        /// <summary>向指定客户端发送字符串（仅TCP服务端模式）</summary>
        public Task SendToClientAsync(string clientId, string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            return SendToClientAsync(clientId, DataEncoding.GetBytes(text));
        }

        #region 同步发送方法

        /// <summary>同步发送字节数据（适用于所有通讯模式）</summary>
        /// <param name="data">要发送的字节数据</param>
        public void Send(byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) return;
            if (_disposed) throw new ObjectDisposedException(nameof(CommunicationManager));

            _sendLock.Wait();
            try
            {
                if (State != CommunicationState.Connected)
                    throw new InvalidOperationException("未连接，无法发送数据");

                if (_mode == CommunicationMode.TcpServer)
                {
                    var streams = _clientStreams.Values.ToArray();
                    if (streams.Length == 0)
                        throw new InvalidOperationException("无已连接的客户端");
                    foreach (var s in streams)
                    {
                        try { s.Write(data, 0, data.Length); }
                        catch { }
                    }
                }
                else if (_mode == CommunicationMode.Tcp)
                {
                    if (_networkStream == null)
                        throw new InvalidOperationException("网络流未初始化");
                    _networkStream.Write(data, 0, data.Length);
                }
                else
                {
                    if (_serialPort == null || !_serialPort.IsOpen)
                        throw new InvalidOperationException("串口未打开");
                    _serialPort.BaseStream.Write(data, 0, data.Length);
                }

                Interlocked.Add(ref _bytesSent, data.Length);
                Interlocked.Increment(ref _framesSent);
            }
            catch (ObjectDisposedException) { }
            catch (Exception ex) when (!(ex is InvalidOperationException || ex is ArgumentNullException))
            {
                OnErrorOccurred(ex);
                throw;
            }
            finally
            {
                _sendLock.Release();
            }
        }

        /// <summary>同步发送字符串数据（使用 DataEncoding 编码，适用于所有通讯模式）</summary>
        public void Send(string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            Send(DataEncoding.GetBytes(text));
        }

        /// <summary>同步广播数据到所有客户端（仅TCP服务端模式）</summary>
        public void SendToAll(byte[] data)
        {
            if (_mode != CommunicationMode.TcpServer)
                throw new InvalidOperationException("仅TCP服务端模式支持广播发送");
            Send(data);
        }

        /// <summary>同步广播字符串到所有客户端（仅TCP服务端模式）</summary>
        public void SendToAll(string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            SendToAll(DataEncoding.GetBytes(text));
        }

        /// <summary>同步向指定客户端发送数据（仅TCP服务端模式）</summary>
        public void SendToClient(string clientId, byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) return;
            if (string.IsNullOrEmpty(clientId)) throw new ArgumentNullException(nameof(clientId));
            if (_disposed) throw new ObjectDisposedException(nameof(CommunicationManager));
            if (_mode != CommunicationMode.TcpServer)
                throw new InvalidOperationException("仅TCP服务端模式支持定向发送");

            _sendLock.Wait();
            try
            {
                if (State != CommunicationState.Connected)
                    throw new InvalidOperationException("未连接，无法发送数据");

                if (_clientStreams.TryGetValue(clientId, out var stream))
                {
                    stream.Write(data, 0, data.Length);
                    Interlocked.Add(ref _bytesSent, data.Length);
                    Interlocked.Increment(ref _framesSent);
                }
                else
                {
                    throw new InvalidOperationException($"客户端 '{clientId}' 不存在或已断开");
                }
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception ex) when (!(ex is InvalidOperationException || ex is ArgumentNullException))
            {
                OnErrorOccurred(ex);
                throw;
            }
            finally
            {
                _sendLock.Release();
            }
        }

        /// <summary>同步向指定客户端发送字符串（仅TCP服务端模式）</summary>
        public void SendToClient(string clientId, string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            SendToClient(clientId, DataEncoding.GetBytes(text));
        }

        #endregion

        /// <summary>
        /// 获取所有已连接客户端的ID列表
        /// </summary>
        public string[] GetConnectedClients()
        {
            return _clients.Keys.ToArray();
        }

        /// <summary>重置所有收发统计</summary>
        public void ResetStatistics()
        {
            Interlocked.Exchange(ref _bytesSent, 0);
            Interlocked.Exchange(ref _bytesReceived, 0);
            Interlocked.Exchange(ref _framesSent, 0);
            Interlocked.Exchange(ref _framesReceived, 0);
            Interlocked.Exchange(ref _errorCount, 0);
        }

        private async Task TcpReadLoopAsync(NetworkStream stream, CancellationToken ct)
        {
            var pooledBuffer = ByteBufferPool.Rent(4096);
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    int read = await stream.ReadAsync(pooledBuffer.Array, 0, pooledBuffer.Capacity, ct).ConfigureAwait(false);
                    if (read == 0) break;

                    pooledBuffer.Count = read;
                    Interlocked.Add(ref _bytesReceived, read);

                    byte[] data = pooledBuffer.ToArray();
                    OnDataReceived(data);

                    if (_frameParser != null)
                    {
                        var frames = _frameParser.Append(data);
                        foreach (var frame in frames)
                        {
                            Interlocked.Increment(ref _framesReceived);
                            OnFrameReceived(frame);
                        }
                    }

                    pooledBuffer.Reset();
                }
            }
            catch (OperationCanceledException) { }
            catch (ObjectDisposedException) { }
            catch (System.IO.IOException) { }
            catch (SocketException) { }
            catch (Exception ex)
            {
                if (!_disposed && !ct.IsCancellationRequested)
                    OnErrorOccurred(ex);
            }
            finally
            {
                pooledBuffer.Dispose();
                if (!_disposed && !ct.IsCancellationRequested
                    && (State == CommunicationState.Connected || State == CommunicationState.Connecting))
                {
                    try { DisconnectInternal(); } catch { }
                    SetState(CommunicationState.Disconnected);
                }
            }
        }

        private async Task ClientReadLoopAsync(string clientId, NetworkStream stream, CancellationToken ct)
        {
            var pooledBuffer = ByteBufferPool.Rent(4096);
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    int read = await stream.ReadAsync(pooledBuffer.Array, 0, pooledBuffer.Capacity, ct).ConfigureAwait(false);
                    if (read == 0) break;

                    pooledBuffer.Count = read;
                    Interlocked.Add(ref _bytesReceived, read);

                    byte[] data = pooledBuffer.ToArray();
                    OnDataReceived(data);
                    OnClientDataReceived(clientId, data);

                    var clientParser = _clientFrameParsers.GetOrAdd(clientId, _ => new FrameParser { Delimiter = FrameDelimiter, MaxBufferSize = 65536 });
                    var frames = clientParser.Append(data);
                    foreach (var frame in frames)
                    {
                        Interlocked.Increment(ref _framesReceived);
                        OnFrameReceived(frame);
                        OnClientFrameReceived(clientId, frame);
                    }

                    pooledBuffer.Reset();
                }
            }
            catch (OperationCanceledException) { }
            catch (ObjectDisposedException) { }
            catch (System.IO.IOException) { }
            catch (SocketException) { }
            catch (Exception ex)
            {
                if (!_disposed && !ct.IsCancellationRequested)
                    OnErrorOccurred(ex);
            }
            finally
            {
                pooledBuffer.Dispose();
                _clientFrameParsers.TryRemove(clientId, out _);
                RemoveClient(clientId);
            }
        }

        private void RemoveClient(string clientId)
        {
            _clientReadTasks.TryRemove(clientId, out _);
            if (_clientStreams.TryRemove(clientId, out var stream))
            {
                try { stream.Dispose(); } catch { }
            }
            if (_clients.TryRemove(clientId, out var client))
            {
                try { client.Close(); } catch { }
            }
            OnClientDisconnected(clientId);
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var port = (SerialPort)sender;

                // 防御性检查：Dispose后仍可能触发（.NET FW 4.8 已知问题）
                if (!port.IsOpen || _disposed)
                    return;

                int bytesToRead = port.BytesToRead;
                if (bytesToRead > 0)
                {
                    byte[] data = new byte[bytesToRead];
                    int read = port.Read(data, 0, bytesToRead);
                    if (read > 0)
                    {
                        if (read < data.Length)
                        {
                            byte[] trimmed = new byte[read];
                            Buffer.BlockCopy(data, 0, trimmed, 0, read);
                            data = trimmed;
                        }
                        Interlocked.Add(ref _bytesReceived, read);
                        OnDataReceived(data);

                        // 帧解析
                        if (_frameParser != null)
                        {
                            var frames = _frameParser.Append(data);
                            foreach (var frame in frames)
                            {
                                Interlocked.Increment(ref _framesReceived);
                                OnFrameReceived(frame);
                            }
                        }
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // .NET FW 4.8 已知问题：Dispose后仍触发事件
            }
            catch (InvalidOperationException)
            {
                // 端口已关闭
            }
            catch (Exception ex)
            {
                if (!_disposed)
                {
                    OnErrorOccurred(ex);
                    if (State == CommunicationState.Connected || State == CommunicationState.Connecting)
                    {
                        try { DisconnectInternal(); } catch { }
                        SetState(CommunicationState.Error);
                    }
                }
            }
        }

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (_disposed) return;

            var ex = new Exception($"串口错误: {e.EventType}");
            OnErrorOccurred(ex);
            if (State == CommunicationState.Connected || State == CommunicationState.Connecting)
            {
                try { DisconnectInternal(); } catch { }
                SetState(CommunicationState.Error);
            }
        }

        private void SetState(CommunicationState state)
        {
            lock (_stateLock)
            {
                if (_state == state) return;
                _state = state;
            }
            _syncContext.Post(_ => StateChanged?.Invoke(this, state), null);
        }

        private void OnDataReceived(byte[] data)
        {
            _syncContext.Post(_ =>
            {
                DataReceived?.Invoke(this, data);
                StringReceived?.Invoke(this, DataEncoding.GetString(data));
            }, null);
        }

        private void OnErrorOccurred(Exception ex)
        {
            Interlocked.Increment(ref _errorCount);
            _syncContext.Post(_ => ErrorOccurred?.Invoke(this, ex), null);
        }

        private void OnClientConnected(string clientId)
        {
            _syncContext.Post(_ =>
            {
                ClientConnected?.Invoke(this, new ClientEventArgs
                {
                    ClientId = clientId,
                    RemoteEndPoint = clientId,
                    ClientCount = _clients.Count
                });
            }, null);
        }

        private void OnClientDisconnected(string clientId)
        {
            _syncContext.Post(_ =>
            {
                ClientDisconnected?.Invoke(this, new ClientEventArgs
                {
                    ClientId = clientId,
                    RemoteEndPoint = clientId,
                    ClientCount = _clients.Count
                });
            }, null);
        }

        private void OnClientDataReceived(string clientId, byte[] data)
        {
            _syncContext.Post(_ =>
            {
                ClientDataReceived?.Invoke(this, new ClientDataEventArgs { ClientId = clientId, Data = data });
                ClientStringReceived?.Invoke(this, new ClientStringEventArgs { ClientId = clientId, Text = DataEncoding.GetString(data) });
            }, null);
        }

        private void OnFrameReceived(byte[] frameData)
        {
            _syncContext.Post(_ => FrameReceived?.Invoke(this, new FrameReceivedEventArgs { FrameData = frameData, Text = DataEncoding.GetString(frameData) }), null);
        }

        private void OnClientFrameReceived(string clientId, byte[] frameData)
        {
            _syncContext.Post(_ => ClientFrameReceived?.Invoke(this, new ClientFrameReceivedEventArgs { ClientId = clientId, FrameData = frameData, Text = DataEncoding.GetString(frameData) }), null);
        }

        /// <summary>
        /// 释放由 <see cref="CommunicationManager"/> 使用的所有资源
        /// </summary>
        /// <param name="disposing">是否释放托管资源</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;

                try { _cts?.Cancel(); } catch { }

                bool opEntered = false;
                bool sendEntered = false;
                try
                {
                    if (_operationLock != null)
                        opEntered = TryAcquireOperationLockForDisconnect();
                    if (_sendLock != null)
                    {
                        try
                        {
                            sendEntered = _sendLock.Wait(TimeSpan.FromSeconds(30));
                        }
                        catch { }
                    }

                    DisconnectInternal();
                    _readTask = null;
                    SetState(CommunicationState.Disconnected);
                }
                finally
                {
                    if (sendEntered)
                    {
                        try { _sendLock?.Release(); } catch { }
                    }
                    if (opEntered)
                    {
                        try { _operationLock?.Release(); } catch { }
                    }
                    try { _operationLock?.Dispose(); } catch { }
                    try { _sendLock?.Dispose(); } catch { }
                }
            }
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// 客户端事件参数
    /// </summary>
    public class ClientEventArgs : EventArgs
    {
        /// <summary>客户端标识（通常为 IP:Port）</summary>
        public string ClientId { get; set; }

        /// <summary>远程端点地址</summary>
        public string RemoteEndPoint { get; set; }

        /// <summary>当前连接的客户端总数</summary>
        public int ClientCount { get; set; }
    }

    /// <summary>带客户端来源的数据接收事件参数</summary>
    public class ClientDataEventArgs : EventArgs
    {
        /// <summary>客户端标识</summary>
        public string ClientId { get; set; }

        /// <summary>接收到的原始数据</summary>
        public byte[] Data { get; set; }
    }

    /// <summary>帧接收事件参数</summary>
    public class FrameReceivedEventArgs : EventArgs
    {
        /// <summary>帧原始字节数据</summary>
        public byte[] FrameData { get; set; }

        /// <summary>帧文本内容（使用 DataEncoding 解码）</summary>
        public string Text { get; set; }
    }

    /// <summary>带客户端来源的帧接收事件参数</summary>
    public class ClientFrameReceivedEventArgs : EventArgs
    {
        /// <summary>客户端标识</summary>
        public string ClientId { get; set; }

        /// <summary>帧原始字节数据</summary>
        public byte[] FrameData { get; set; }

        /// <summary>帧文本内容</summary>
        public string Text { get; set; }
    }

    /// <summary>带客户端来源的字符串接收事件参数</summary>
    public class ClientStringEventArgs : EventArgs
    {
        /// <summary>客户端标识</summary>
        public string ClientId { get; set; }

        /// <summary>接收到的字符串内容</summary>
        public string Text { get; set; }
    }
}

