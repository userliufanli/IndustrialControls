using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Theme;

namespace IndustrialControls.Controls.Communication
{
    /// <summary>
    /// 工业通讯复合控件，集成模式选择、参数配置、连接控制、数据收发和状态显示。
    /// 使用者只需拖放一个控件即可拥有完整的通讯配置和收发功能。
    /// </summary>
    [ToolboxItem(true)]
    public partial class CommunicationControl : UserControl
    {
        private readonly CommunicationManager _manager;
        private static readonly ConcurrentDictionary<string, ParameterManager> _sharedParameterManagers
            = new ConcurrentDictionary<string, ParameterManager>(StringComparer.OrdinalIgnoreCase);
        
        private string _resolvedConfigPath;
        private EventHandler<CommunicationState> _onManagerStateChanged;
        private EventHandler<byte[]> _onManagerDataReceived;
        private EventHandler<Exception> _onManagerErrorOccurred;
        private long _lastStatsUpdateTick;
        private const long STATS_THROTTLE_MS = 200;
        
        // 用于取消异步操作的 TokenSource
        private CancellationTokenSource _asyncOperationCts;
        
        // 自动重连：尝试次数（与 MaxReconnectAttempts 配合）
        private int _reconnectAttempts;

        /// <summary>非 0 时表示用户主动断开，抑制自动重连直至下次 <see cref="ConnectAsync"/>。</summary>
        private int _suppressAutoReconnect;

        private CancellationTokenSource _reconnectDelayCts;

        /// <summary>已挂接 Load/Shown 的宿主窗体（用于主窗体打开即自动连接，无需先切换 Tab）。</summary>
        private Form _hostFormAutoConnectHooked;

        private readonly object _hostFormAutoConnectLock = new object();
        private readonly EventHandler _hostFormEarlyAutoConnectHandler;
        private int _earlyAutoConnectKickoffDone;

        /// <summary>宿主窗体 Load/Shown 已错过时，用第一次 Idle 再尝试订阅与连接。</summary>
        private readonly EventHandler _applicationIdleAutoConnectHandler;
        private bool _applicationIdleHookActive;

        #region 颜色常量（工业级配色标准）
        private static class Colors
        {
            public static readonly Color Success = Color.FromArgb(16, 185, 129);
            public static readonly Color Warning = Color.FromArgb(245, 158, 11);
            public static readonly Color Danger = Color.FromArgb(239, 68, 68);
            public static readonly Color Gray = Color.FromArgb(156, 163, 175);
            public static readonly Color Gray300 = Color.FromArgb(209, 213, 219);
            public static readonly Color Gray500 = Color.FromArgb(107, 114, 128);
            public static readonly Color Gray100 = Color.FromArgb(247, 247, 248);
            public static readonly Color Gray200 = Color.FromArgb(235, 235, 235);
            public static readonly Color Green600 = Color.FromArgb(22, 163, 74);
            public static readonly Color Red600 = Color.FromArgb(220, 38, 38);
            public static readonly Color Indigo600 = Color.FromArgb(79, 70, 229);
            public static readonly Color White = Color.White;
            public static readonly Color ControlBackground = Color.FromArgb(252, 252, 252);
            public static readonly Color Border = Color.FromArgb(220, 220, 220);
        }
        #endregion

        /// <summary>状态变更事件</summary>
        public event EventHandler<CommunicationState> StateChanged;

        /// <summary>数据接收事件（原始字节）</summary>
        public event EventHandler<byte[]> DataReceived;

        /// <summary>字符串数据接收事件（使用 DataEncoding 自动解码，适用于所有通讯模式）</summary>
        public event EventHandler<string> StringReceived
        {
            add => _manager.StringReceived += value;
            remove => _manager.StringReceived -= value;
        }

        /// <summary>错误发生事件</summary>
        public event EventHandler<Exception> ErrorOccurred;

        /// <summary>保存参数请求事件</summary>
        public event EventHandler ParametersSaveRequested;

        /// <summary>客户端连接事件（TCP服务端模式）</summary>
        public event EventHandler<ClientEventArgs> ClientConnected
        {
            add => _manager.ClientConnected += value;
            remove => _manager.ClientConnected -= value;
        }

        /// <summary>客户端断开事件（TCP服务端模式）</summary>
        public event EventHandler<ClientEventArgs> ClientDisconnected
        {
            add => _manager.ClientDisconnected += value;
            remove => _manager.ClientDisconnected -= value;
        }

        /// <summary>带客户端来源的数据接收事件（TCP服务端模式）</summary>
        public event EventHandler<ClientDataEventArgs> ClientDataReceived
        {
            add => _manager.ClientDataReceived += value;
            remove => _manager.ClientDataReceived -= value;
        }

        /// <summary>带客户端来源的字符串接收事件（TCP服务端模式，使用 DataEncoding 自动解码）</summary>
        public event EventHandler<ClientStringEventArgs> ClientStringReceived
        {
            add => _manager.ClientStringReceived += value;
            remove => _manager.ClientStringReceived -= value;
        }

        /// <summary>帧接收事件（分隔符解析后的完整数据帧）</summary>
        public event EventHandler<FrameReceivedEventArgs> FrameReceived
        {
            add => _manager.FrameReceived += value;
            remove => _manager.FrameReceived -= value;
        }

        /// <summary>带客户端来源的帧接收事件（TCP服务端模式）</summary>
        public event EventHandler<ClientFrameReceivedEventArgs> ClientFrameReceived
        {
            add => _manager.ClientFrameReceived += value;
            remove => _manager.ClientFrameReceived -= value;
        }

        /// <summary>
        /// 初始化 <see cref="CommunicationControl"/> 的新实例
        /// </summary>
        public CommunicationControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            BackColor = Color.FromArgb(252, 252, 252);

            _manager = new CommunicationManager(SynchronizationContext.Current);
            this.components.Add(_manager);

            SetComboDefaults();
            SetupManagerEvents();
            SetupControlEvents();
            LoadSerialPorts();
            SyncUIFromManager();
            UpdateModeVisibility();
            UpdateStateDisplay();
            UpdateStats();

            // ToolTip提示
            toolTip1.SetToolTip(txtTcpIp, "TCP服务器IP地址");
            toolTip1.SetToolTip(nudTcpPort, "TCP端口号");
            toolTip1.SetToolTip(nudTcpTimeout, "连接超时(ms)");
            toolTip1.SetToolTip(txtServerIp, "监听IP地址");
            toolTip1.SetToolTip(nudServerPort, "监听端口号");
            toolTip1.SetToolTip(cboSerialPort, "串口号");
            toolTip1.SetToolTip(cboBaudRate, "波特率");
            toolTip1.SetToolTip(cboParity, "校验位");
            toolTip1.SetToolTip(cboDataBits, "数据位");
            toolTip1.SetToolTip(cboStopBits, "停止位");

            _hostFormEarlyAutoConnectHandler = OnHostFormLoadOrShownForEarlyAutoConnect;
            _applicationIdleAutoConnectHandler = OnApplicationIdleForAutoConnect;
            EnsureApplicationIdleAutoConnectHook();
        }

        #region 公开属性

        [Category("外观")]
        [Description("控件标识标题，用于区分多个通讯控件实例")]
        [DefaultValue("通讯控件")]
        public string Title
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value ?? "通讯控件";
        }

        [Category("通讯")]
        [Description("通讯模式：TCP、TCP服务端 或 串口")]
        [DefaultValue(CommunicationMode.Tcp)]
        public CommunicationMode Mode
        {
            get => _manager.Mode;
            set
            {
                if (_manager.Mode != value)
                {
                    _manager.Mode = value;
                    SyncUIFromManager();
                    UpdateModeVisibility();
                }
            }
        }

        [Browsable(false)]
        public CommunicationState State => _manager.State;

        [Browsable(false)]
        public long BytesSent => _manager.BytesSent;

        [Browsable(false)]
        public long BytesReceived => _manager.BytesReceived;

        /// <summary>已发送帧数</summary>
        [Browsable(false)]
        public long FramesSent => _manager.FramesSent;

        /// <summary>已接收帧数</summary>
        [Browsable(false)]
        public long FramesReceived => _manager.FramesReceived;

        /// <summary>错误计数</summary>
        [Browsable(false)]
        public long ErrorCount => _manager.ErrorCount;

        /// <summary>帧分隔符（默认 \r\n）</summary>
        [Category("帧解析")]
        [Description("帧分隔符字节序列")]
        [Browsable(false)]
        public byte[] FrameDelimiter
        {
            get => _manager.FrameDelimiter;
            set => _manager.FrameDelimiter = value;
        }

        /// <summary>字符串编码（发送/接收字符串时使用）</summary>
        [Category("帧解析")]
        [Description("字符串编码")]
        [Browsable(false)]
        public Encoding DataEncoding
        {
            get => _manager.DataEncoding;
            set => _manager.DataEncoding = value;
        }

        [Category("TCP")]
        [Description("TCP 目标 IP 地址")]
        [DefaultValue("127.0.0.1")]
        public string TcpIp
        {
            get => txtTcpIp.Text;
            set => txtTcpIp.Text = value ?? "127.0.0.1";
        }

        [Category("TCP")]
        [Description("TCP 目标端口号")]
        [DefaultValue(502)]
        public int TcpPort
        {
            get => (int)nudTcpPort.Value;
            set => nudTcpPort.Value = value;
        }

        [Category("TCP")]
        [Description("TCP 连接超时时间（毫秒）")]
        [DefaultValue(5000)]
        public int TcpTimeout
        {
            get => (int)nudTcpTimeout.Value;
            set => nudTcpTimeout.Value = value;
        }

        [Category("TCP服务端")]
        [Description("TCP 服务端监听 IP 地址")]
        [DefaultValue("0.0.0.0")]
        public string TcpServerIp
        {
            get => txtServerIp.Text;
            set => txtServerIp.Text = value ?? "0.0.0.0";
        }

        [Category("TCP服务端")]
        [Description("TCP 服务端监听端口号")]
        [DefaultValue(502)]
        public int TcpServerPort
        {
            get => (int)nudServerPort.Value;
            set => nudServerPort.Value = value;
        }

        [Category("串口")]
        [Description("串口名称")]
        [DefaultValue("COM1")]
        public string SerialPortName
        {
            get => cboSerialPort.SelectedItem?.ToString() ?? "COM1";
            set
            {
                int idx = cboSerialPort.Items.IndexOf(value);
                if (idx >= 0) cboSerialPort.SelectedIndex = idx;
                else if (cboSerialPort.Items.Count > 0) cboSerialPort.SelectedIndex = 0;
            }
        }

        [Category("串口")]
        [Description("串口波特率")]
        [DefaultValue(9600)]
        public int SerialBaudRate
        {
            get
            {
                if (int.TryParse(cboBaudRate.SelectedItem?.ToString(), out int val))
                    return val;
                return 9600;
            }
            set
            {
                string s = value.ToString();
                int idx = cboBaudRate.Items.IndexOf(s);
                if (idx >= 0) cboBaudRate.SelectedIndex = idx;
            }
        }

        [Category("串口")]
        [Description("串口校验位")]
        [DefaultValue(Parity.None)]
        public Parity SerialParity
        {
            get
            {
                string s = cboParity.SelectedItem?.ToString();
                return s == "Odd" ? Parity.Odd : (s == "Even" ? Parity.Even : Parity.None);
            }
            set
            {
                string s = value == Parity.Odd ? "Odd" : (value == Parity.Even ? "Even" : "None");
                int idx = cboParity.Items.IndexOf(s);
                if (idx >= 0) cboParity.SelectedIndex = idx;
            }
        }

        [Category("串口")]
        [Description("串口数据位")]
        [DefaultValue(8)]
        public int SerialDataBits
        {
            get
            {
                if (int.TryParse(cboDataBits.SelectedItem?.ToString(), out int val))
                    return val;
                return 8;
            }
            set
            {
                string s = value.ToString();
                int idx = cboDataBits.Items.IndexOf(s);
                if (idx >= 0) cboDataBits.SelectedIndex = idx;
            }
        }

        [Category("串口")]
        [Description("串口停止位")]
        [DefaultValue(StopBits.One)]
        public StopBits SerialStopBits
        {
            get
            {
                string s = cboStopBits.SelectedItem?.ToString();
                return s == "1.5" ? StopBits.OnePointFive : (s == "2" ? StopBits.Two : StopBits.One);
            }
            set
            {
                string s = value == StopBits.OnePointFive ? "1.5" : (value == StopBits.Two ? "2" : "1");
                int idx = cboStopBits.Items.IndexOf(s);
                if (idx >= 0) cboStopBits.SelectedIndex = idx;
            }
        }

        [Category("参数持久化")]
        [Description("是否启用自动参数持久化（拖放即用，无需手动编写保存/加载代码）")]
        [DefaultValue(true)]
        public bool AutoPersist { get; set; } = true;

        [Category("参数持久化")]
        [Description("持久化键名（用于区分多个控件实例，为空时使用控件 Name）")]
        [DefaultValue("")]
        public string PersistKey { get; set; } = "";

        [Category("参数持久化")]
        [Description("参数配置文件路径（为空或路径无效时使用默认路径 Config/communication.json）")]
        [DefaultValue("")]
        public string ConfigFilePath { get; set; } = "";

        [Category("参数持久化")]
        [Description("宿主窗体打开后即自动连接（无需先切换到含本控件的 Tab）；需配合 AutoPersist 与已保存的配置")]
        [DefaultValue(true)]
        public bool AutoConnect { get; set; } = true;

        [Category("通讯")]
        [Description("是否启用自动重连机制")]
        [DefaultValue(true)]
        public bool AutoReconnect { get; set; } = true;

        [Category("通讯")]
        [Description("最大自动重连尝试次数")]
        [DefaultValue(5)]
        public int MaxReconnectAttempts { get; set; } = 5;

        [Category("通讯")]
        [Description("重连间隔时间（毫秒）")]
        [DefaultValue(3000)]
        public int ReconnectDelayMs { get; set; } = 3000;

        #endregion

        #region 公开方法

        /// <summary>异步连接到目标设备</summary>
        public Task ConnectAsync()
        {
            Interlocked.Exchange(ref _suppressAutoReconnect, 0);
            return _manager.ConnectAsync();
        }

        /// <summary>异步断开连接</summary>
        public Task DisconnectAsync() => _manager.DisconnectAsync();

        /// <summary>同步断开连接</summary>
        public void Disconnect() => _manager.Disconnect();

        /// <summary>异步发送数据</summary>
        public Task SendAsync(byte[] data) => _manager.SendAsync(data);

        /// <summary>向指定客户端发送数据（仅TCP服务端模式）</summary>
        public Task SendToClientAsync(string clientId, byte[] data) => _manager.SendToClientAsync(clientId, data);

        /// <summary>发送字符串数据（使用 DataEncoding 编码）</summary>
        public Task SendAsync(string text) => _manager.SendAsync(text);

        /// <summary>广播数据到所有客户端（仅TCP服务端模式）</summary>
        public Task SendToAllAsync(byte[] data) => _manager.SendToAllAsync(data);

        /// <summary>广播字符串到所有客户端（仅TCP服务端模式）</summary>
        public Task SendToAllAsync(string text) => _manager.SendToAllAsync(text);

        /// <summary>向指定客户端发送字符串（仅TCP服务端模式）</summary>
        public Task SendToClientAsync(string clientId, string text) => _manager.SendToClientAsync(clientId, text);

        /// <summary>同步发送字节数据（适用于所有通讯模式）</summary>
        public void Send(byte[] data) => _manager.Send(data);

        /// <summary>同步发送字符串数据（适用于所有通讯模式）</summary>
        public void Send(string text) => _manager.Send(text);

        /// <summary>同步广播数据到所有客户端（仅TCP服务端模式）</summary>
        public void SendToAll(byte[] data) => _manager.SendToAll(data);

        /// <summary>同步广播字符串到所有客户端（仅TCP服务端模式）</summary>
        public void SendToAll(string text) => _manager.SendToAll(text);

        /// <summary>同步向指定客户端发送数据（仅TCP服务端模式）</summary>
        public void SendToClient(string clientId, byte[] data) => _manager.SendToClient(clientId, data);

        /// <summary>同步向指定客户端发送字符串（仅TCP服务端模式）</summary>
        public void SendToClient(string clientId, string text) => _manager.SendToClient(clientId, text);

        /// <summary>重置所有收发统计</summary>
        public void ResetStatistics() => _manager.ResetStatistics();

        /// <summary>获取所有已连接客户端ID列表</summary>
        public string[] GetConnectedClients() => _manager.GetConnectedClients();

        /// <summary>当前连接的客户端数量</summary>
        [Browsable(false)]
        public int ClientCount => _manager.ClientCount;

        /// <summary>
        /// 从外部参数加载到控件UI
        /// </summary>
        public void LoadParameters(string tcpIp, int tcpPort, int tcpTimeout,
            string serverIp, int serverPort,
            string serialPort, int baudRate, string parity, int dataBits, string stopBits,
            string mode)
        {
            txtTcpIp.Text = tcpIp ?? "127.0.0.1";
            nudTcpPort.Value = tcpPort;
            nudTcpTimeout.Value = tcpTimeout;

            txtServerIp.Text = serverIp ?? "0.0.0.0";
            nudServerPort.Value = serverPort;

            int portIdx = cboSerialPort.Items.IndexOf(serialPort);
            cboSerialPort.SelectedIndex = portIdx >= 0 ? portIdx : (cboSerialPort.Items.Count > 0 ? 0 : -1);

            string baudStr = baudRate.ToString();
            int baudIdx = cboBaudRate.Items.IndexOf(baudStr);
            if (baudIdx < 0) baudIdx = cboBaudRate.Items.IndexOf("115200");
            cboBaudRate.SelectedIndex = baudIdx >= 0 ? baudIdx : 0;

            string parityStr = parity == "O" ? "Odd" : (parity == "E" ? "Even" : "None");
            int parityIdx = cboParity.Items.IndexOf(parityStr);
            cboParity.SelectedIndex = parityIdx >= 0 ? parityIdx : 0;

            string dbStr = dataBits.ToString();
            int dbIdx = cboDataBits.Items.IndexOf(dbStr);
            if (dbIdx < 0) dbIdx = cboDataBits.Items.IndexOf("8");
            cboDataBits.SelectedIndex = dbIdx >= 0 ? dbIdx : 0;

            string sbStr = stopBits == "1.5" ? "1.5" : (stopBits == "2" ? "2" : "1");
            int sbIdx = cboStopBits.Items.IndexOf(sbStr);
            cboStopBits.SelectedIndex = sbIdx >= 0 ? sbIdx : 0;

            string modeText = mode == "Serial" ? "串口" : (mode == "TcpServer" ? "TCP服务端" : "TCP客户端");
            int modeIdx = cboMode.Items.IndexOf(modeText);
            cboMode.SelectedIndex = modeIdx >= 0 ? modeIdx : 0;

            SyncToManager();
            UpdateModeVisibility();
        }

        /// <summary>
        /// 从控件UI读取当前参数
        /// </summary>
        public void GetCurrentParameters(out string tcpIp, out int tcpPort, out int tcpTimeout,
            out string serverIp, out int serverPort,
            out string serialPort, out int baudRate, out string parity, out int dataBits, out string stopBits,
            out string mode)
        {
            tcpIp = txtTcpIp.Text;
            tcpPort = (int)nudTcpPort.Value;
            tcpTimeout = (int)nudTcpTimeout.Value;
            serverIp = txtServerIp.Text;
            serverPort = (int)nudServerPort.Value;
            serialPort = cboSerialPort.SelectedItem?.ToString() ?? "COM1";

            if (!int.TryParse(cboBaudRate.SelectedItem?.ToString(), out baudRate))
                baudRate = 115200;

            string p = cboParity.SelectedItem?.ToString();
            parity = p == "Odd" ? "O" : (p == "Even" ? "E" : "N");

            if (!int.TryParse(cboDataBits.SelectedItem?.ToString(), out dataBits))
                dataBits = 8;

            string sb = cboStopBits.SelectedItem?.ToString();
            stopBits = sb ?? "1";

            string m = cboMode.SelectedItem?.ToString();
            mode = m == "串口" ? "Serial" : (m == "TCP服务端" ? "TcpServer" : "Tcp");
        }

        #endregion

        #region 内部初始化

        private void SetComboDefaults()
        {
            if (cboMode.Items.Count > 0) cboMode.SelectedIndex = 0;

            int baudIdx = cboBaudRate.Items.IndexOf("115200");
            if (baudIdx < 0) baudIdx = 0;
            if (cboBaudRate.Items.Count > 0) cboBaudRate.SelectedIndex = baudIdx;

            int parityIdx = cboParity.Items.IndexOf("None");
            if (parityIdx < 0) parityIdx = 0;
            if (cboParity.Items.Count > 0) cboParity.SelectedIndex = parityIdx;

            int dbIdx = cboDataBits.Items.IndexOf("8");
            if (dbIdx < 0) dbIdx = 0;
            if (cboDataBits.Items.Count > 0) cboDataBits.SelectedIndex = dbIdx;

            int sbIdx = cboStopBits.Items.IndexOf("1");
            if (sbIdx < 0) sbIdx = 0;
            if (cboStopBits.Items.Count > 0) cboStopBits.SelectedIndex = sbIdx;
        }

        private void SetupManagerEvents()
        {
            _onManagerStateChanged = (s, e) =>
            {
                SafeInvoke(() =>
                {
                    UpdateStateDisplay();
                    StateChanged?.Invoke(this, e);
                    
                    // 自动重连逻辑：当连接断开且非手动断开时触发重连
                    HandleAutoReconnect(e);
                });
            };

            _onManagerDataReceived = (s, e) =>
            {
                SafeInvoke(() =>
                {
                    ThrottledUpdateStats();
                    DataReceived?.Invoke(this, e);
                });
            };

            _onManagerErrorOccurred = (s, e) =>
            {
                SafeInvoke(() =>
                {
                    UpdateStateDisplay();
                    UpdateStats();
                    ErrorOccurred?.Invoke(this, e);
                });
            };

            _manager.StateChanged += _onManagerStateChanged;
            _manager.DataReceived += _onManagerDataReceived;
            _manager.ErrorOccurred += _onManagerErrorOccurred;
            _manager.ClientConnected += (s, e) => SafeInvoke(() => UpdateStats());
            _manager.ClientDisconnected += (s, e) => SafeInvoke(() => UpdateStats());
        }

        private void SetupControlEvents()
        {
            cboMode.SelectedIndexChanged += CboMode_SelectedIndexChanged;
            btnConnect.Click += BtnConnect_Click;
            btnDisconnect.Click += BtnDisconnect_Click;
            btnSave.Click += BtnSave_Click;

            pnlIndicator.Paint += PnlIndicator_Paint;
        }

        private void LoadSerialPorts()
        {
            cboSerialPort.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
                cboSerialPort.Items.AddRange(ports);
            else
                cboSerialPort.Items.Add("COM1");

            if (cboSerialPort.Items.Count > 0)
                cboSerialPort.SelectedIndex = 0;
        }

        #endregion

        #region 同步与更新

        private void UpdateModeVisibility()
        {
            string selected = cboMode.SelectedItem?.ToString();
            pnlTcpParams.Visible = selected == "TCP客户端";
            pnlTcpServerParams.Visible = selected == "TCP服务端";
            pnlSerialParams.Visible = selected == "串口";
        }

        private void SyncToManager()
        {
            _manager.TcpIp = txtTcpIp.Text;
            _manager.TcpPort = (int)nudTcpPort.Value;
            _manager.TcpTimeout = (int)nudTcpTimeout.Value;

            _manager.TcpServerIp = txtServerIp.Text;
            _manager.TcpServerPort = (int)nudServerPort.Value;

            _manager.SerialPortName = cboSerialPort.SelectedItem?.ToString() ?? "COM1";

            if (int.TryParse(cboBaudRate.SelectedItem?.ToString(), out int baud))
                _manager.SerialBaudRate = baud;

            string parity = cboParity.SelectedItem?.ToString();
            _manager.SerialParity = parity == "Odd" ? Parity.Odd : (parity == "Even" ? Parity.Even : Parity.None);

            if (int.TryParse(cboDataBits.SelectedItem?.ToString(), out int db))
                _manager.SerialDataBits = db;

            string stopBits = cboStopBits.SelectedItem?.ToString();
            _manager.SerialStopBits = stopBits == "1.5" ? StopBits.OnePointFive : (stopBits == "2" ? StopBits.Two : StopBits.One);

            string modeText = cboMode.SelectedItem?.ToString();
            _manager.Mode = modeText == "串口" ? CommunicationMode.Serial :
                           (modeText == "TCP服务端" ? CommunicationMode.TcpServer : CommunicationMode.Tcp);
        }

        private void SyncUIFromManager()
        {
            txtTcpIp.Text = _manager.TcpIp;
            nudTcpPort.Value = _manager.TcpPort;
            nudTcpTimeout.Value = _manager.TcpTimeout;
            txtServerIp.Text = _manager.TcpServerIp;
            nudServerPort.Value = _manager.TcpServerPort;

            int portIdx = cboSerialPort.Items.IndexOf(_manager.SerialPortName);
            if (portIdx >= 0) cboSerialPort.SelectedIndex = portIdx;

            string baudStr = _manager.SerialBaudRate.ToString();
            int baudIdx = cboBaudRate.Items.IndexOf(baudStr);
            if (baudIdx >= 0) cboBaudRate.SelectedIndex = baudIdx;

            string parityStr = _manager.SerialParity == Parity.Odd ? "Odd" : (_manager.SerialParity == Parity.Even ? "Even" : "None");
            int parityIdx = cboParity.Items.IndexOf(parityStr);
            if (parityIdx >= 0) cboParity.SelectedIndex = parityIdx;

            string dbStr = _manager.SerialDataBits.ToString();
            int dbIdx = cboDataBits.Items.IndexOf(dbStr);
            if (dbIdx >= 0) cboDataBits.SelectedIndex = dbIdx;

            string sbStr = _manager.SerialStopBits == StopBits.OnePointFive ? "1.5" : (_manager.SerialStopBits == StopBits.Two ? "2" : "1");
            int sbIdx = cboStopBits.Items.IndexOf(sbStr);
            if (sbIdx >= 0) cboStopBits.SelectedIndex = sbIdx;

            string modeText = _manager.Mode == CommunicationMode.Serial ? "串口" :
                             (_manager.Mode == CommunicationMode.TcpServer ? "TCP服务端" : "TCP客户端");
            int modeIdx = cboMode.Items.IndexOf(modeText);
            if (modeIdx >= 0) cboMode.SelectedIndex = modeIdx;
        }

        private void UpdateStateDisplay()
        {
            switch (_manager.State)
            {
                case CommunicationState.Connected:
                    pnlIndicator.BackColor = Color.FromArgb(16, 185, 129);
                    lblState.Text = (_manager.Mode == CommunicationMode.TcpServer) ? "监听中" : "已连接";
                    lblState.ForeColor = Color.FromArgb(16, 185, 129);
                    break;
                case CommunicationState.Connecting:
                    pnlIndicator.BackColor = Color.FromArgb(245, 158, 11);
                    lblState.Text = (_manager.Mode == CommunicationMode.TcpServer) ? "启动中..." : "连接中...";
                    lblState.ForeColor = Color.FromArgb(245, 158, 11);
                    break;
                case CommunicationState.Error:
                    pnlIndicator.BackColor = Color.FromArgb(239, 68, 68);
                    lblState.Text = "错误";
                    lblState.ForeColor = Color.FromArgb(239, 68, 68);
                    break;
                default:
                    pnlIndicator.BackColor = Color.FromArgb(156, 163, 175);
                    lblState.Text = "未连接";
                    lblState.ForeColor = Color.FromArgb(156, 163, 175);
                    break;
            }
            pnlIndicator.Invalidate();

            // 按钮状态联动
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            var state = _manager.State;
            bool isDisconnected = (state == CommunicationState.Disconnected || state == CommunicationState.Error);
            bool isConnected = (state == CommunicationState.Connected);
            bool isConnecting = (state == CommunicationState.Connecting);

            btnConnect.Enabled = isDisconnected;
            btnDisconnect.Enabled = isConnected || isConnecting;
            btnSave.Enabled = isDisconnected;
            cboMode.Enabled = isDisconnected;

            // 按钮颜色联动：禁用时变灰，启用时恢复语义色
            btnConnect.BackColor = btnConnect.Enabled
                ? Color.FromArgb(22, 163, 74)    // Green-600
                : Color.FromArgb(209, 213, 219); // Gray-300
            btnDisconnect.BackColor = btnDisconnect.Enabled
                ? Color.FromArgb(220, 38, 38)    // Red-600
                : Color.FromArgb(209, 213, 219); // Gray-300
            btnSave.BackColor = btnSave.Enabled
                ? Color.FromArgb(79, 70, 229)    // Indigo-600
                : Color.FromArgb(209, 213, 219); // Gray-300

            // 文字颜色联动
            btnConnect.ForeColor = btnConnect.Enabled ? Color.White : Color.FromArgb(156, 163, 175);
            btnDisconnect.ForeColor = btnDisconnect.Enabled ? Color.White : Color.FromArgb(156, 163, 175);
            btnSave.ForeColor = btnSave.Enabled ? Color.White : Color.FromArgb(156, 163, 175);

            // 参数输入区域也应在连接时禁用
            pnlTcpParams.Enabled = isDisconnected;
            pnlTcpServerParams.Enabled = isDisconnected;
            pnlSerialParams.Enabled = isDisconnected;
        }

        /// <summary>
        /// 处理自动重连逻辑（取消上一次未完成的延迟任务，避免多条 async void 链重叠）
        /// </summary>
        private void HandleAutoReconnect(CommunicationState newState)
        {
            _reconnectDelayCts?.Cancel();
            _reconnectDelayCts?.Dispose();
            _reconnectDelayCts = null;

            if (newState == CommunicationState.Connected)
            {
                Interlocked.Exchange(ref _reconnectAttempts, 0);
                return;
            }

            if (newState != CommunicationState.Disconnected && newState != CommunicationState.Error)
                return;

            if (!AutoReconnect)
            {
                Interlocked.Exchange(ref _reconnectAttempts, 0);
                return;
            }

            if (Interlocked.CompareExchange(ref _suppressAutoReconnect, 0, 0) != 0)
            {
                Interlocked.Exchange(ref _reconnectAttempts, 0);
                return;
            }

            int prior = Interlocked.CompareExchange(ref _reconnectAttempts, 0, 0);
            if (prior >= MaxReconnectAttempts)
            {
                System.Diagnostics.Debug.WriteLine($"[CommunicationControl] 已达到最大重连次数 {MaxReconnectAttempts}，停止自动重连");
                Interlocked.Exchange(ref _reconnectAttempts, 0);
                return;
            }

            try
            {
                _reconnectDelayCts = new CancellationTokenSource();
            }
            catch
            {
                return;
            }

            var ct = _reconnectDelayCts.Token;
            int delayMs = ReconnectDelayMs;
            int maxAttempts = MaxReconnectAttempts;

            try
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await Task.Delay(delayMs, ct).ConfigureAwait(false);
                        if (ct.IsCancellationRequested || IsDisposed)
                            return;

                        if (Interlocked.CompareExchange(ref _suppressAutoReconnect, 0, 0) != 0)
                            return;

                        var st = _manager.State;
                        if (st != CommunicationState.Disconnected && st != CommunicationState.Error)
                            return;

                        int attempt = Interlocked.Increment(ref _reconnectAttempts);
                        if (attempt > maxAttempts)
                            return;

                        System.Diagnostics.Debug.WriteLine($"[CommunicationControl] 自动重连尝试 {attempt}/{maxAttempts}");

                        await ConnectAsync().ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"[CommunicationControl] 自动重连失败: {ex.Message}");
                    }
                }, ct);
            }
            catch (OperationCanceledException)
            {
                // Task.Run 在 ct 已取消时可能同步抛出
            }
        }

        /// <summary>
        /// 重置重连计数器（手动连接成功时调用）
        /// </summary>
        public void ResetReconnectCounter()
        {
            Interlocked.Exchange(ref _reconnectAttempts, 0);
        }

        private void UpdateStats()
        {
            if (_manager.Mode == CommunicationMode.TcpServer && _manager.ClientCount > 0)
                lblStats.Text = $"TX:{BytesSent} RX:{BytesReceived} C:{_manager.ClientCount}";
            else
                lblStats.Text = $"TX:{BytesSent} RX:{BytesReceived}";
        }

        private void ThrottledUpdateStats()
        {
            long now = Environment.TickCount;
            if (now - _lastStatsUpdateTick >= STATS_THROTTLE_MS)
            {
                _lastStatsUpdateTick = now;
                UpdateStats();
            }
        }

        private void SafeInvoke(Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            try
            {
                if (IsDisposed)
                    return;

                if (IsHandleCreated)
                {
                    if (InvokeRequired)
                        BeginInvoke(action);
                    else
                        action();
                    return;
                }

                // 未创建句柄时（如未选中的 Tab），仍尝试经宿主窗体队列刷新，避免状态已连接但界面停在未连接
                var host = FindForm() ?? TopLevelControl as Form;
                if (host != null && !host.IsDisposed && host.IsHandleCreated)
                {
                    host.BeginInvoke(new Action(() =>
                    {
                        if (IsDisposed) return;
                        try
                        {
                            action();
                        }
                        catch (Exception ex)
                        {
                            LogError($"SafeInvoke(经宿主窗体) 执行失败: {ex.Message}");
                        }
                    }));
                }
            }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
            catch (Exception ex)
            {
                LogError($"SafeInvoke 执行失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 带超时的安全Invoke
        /// </summary>
        private async void SafeInvokeAsync(Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            try
            {
                if (IsDisposed || !IsHandleCreated)
                    return;

                if (InvokeRequired)
                {
                    var taskCompletionSource = new TaskCompletionSource<bool>();
                    IAsyncResult result = BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            action();
                            taskCompletionSource.TrySetResult(true);
                        }
                        catch (Exception ex)
                        {
                            taskCompletionSource.TrySetException(ex);
                        }
                    }));

                    using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2)))
                    {
                        var invokeTask = Task.Factory.FromAsync(result, r => EndInvoke(r));
                        var timeoutTask = Task.Delay(Timeout.Infinite, cts.Token);
                        var completedTask = await Task.WhenAny(invokeTask, timeoutTask);

                        if (completedTask == timeoutTask)
                        {
                            LogError("SafeInvokeAsync 超时");
                        }
                    }
                }
                else
                {
                    action();
                }
            }
            catch (Exception ex)
            {
                LogError($"SafeInvokeAsync 执行失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取实际的持久化分组名。
        /// 规则：PersistKey 显式设置时直接使用；否则自动加上父级页面/窗体类型名前缀，
        /// 确保同名控件在不同页面中互不冲突。
        /// </summary>
        private string GetPersistGroupName()
        {
            // 用户显式指定了 PersistKey，直接使用（用户承担唯一性责任）
            if (!string.IsNullOrEmpty(PersistKey))
                return PersistKey;

            // 自动作用域：查找最近的父级页面（UserControl）或窗体（Form）
            string ownerScope = ResolveOwnerScope();
            if (!string.IsNullOrEmpty(ownerScope))
                return $"{ownerScope}.{Name}";

            // 无父级容器（设计器或独立使用），直接用 Name
            return Name;
        }

        /// <summary>
        /// 解析控件所属的页面/窗体类型名作为作用域前缀。
        /// 向上遍历 Parent 链，找到第一个 UserControl 或 Form 祖先。
        /// </summary>
        private string ResolveOwnerScope()
        {
            Control parent = this.Parent;
            while (parent != null)
            {
                if (parent is Form || (parent is UserControl && parent != this))
                    return parent.GetType().Name;
                parent = parent.Parent;
            }
            return null;
        }

        /// <summary>
        /// 获取参数管理器实例（同一文件路径的所有控件共享同一实例，防止多控件互相覆盖配置）
        /// </summary>
        private ParameterManager GetParameterManager()
        {
            if (DesignMode) return null;

            try
            {
                string path = ResolveConfigPath();
                if (string.IsNullOrEmpty(path))
                    return null;

                if (!ValidatePathSecurity(path))
                {
                    LogError("参数配置路径安全验证失败");
                    return null;
                }

                _resolvedConfigPath = path;
                return _sharedParameterManagers.GetOrAdd(path, p => new ParameterManager(p));
            }
            catch (Exception ex)
            {
                LogError($"获取参数管理器失败: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 解析配置文件路径
        /// </summary>
        private string ResolveConfigPath()
        {
            string path = ConfigFilePath;

            if (!string.IsNullOrWhiteSpace(path))
            {
                return System.IO.Path.GetFullPath(path);
            }

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            if (string.IsNullOrEmpty(baseDir))
                return null;

            // 安全检查：禁止在 Program Files 目录下创建配置文件
            if (baseDir.StartsWith("C:\\Program Files", StringComparison.OrdinalIgnoreCase) ||
                baseDir.StartsWith("C:\\Program Files (x86)", StringComparison.OrdinalIgnoreCase))
            {
                // 使用用户应用数据目录作为替代
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return System.IO.Path.Combine(appDataPath, Application.ProductName, "Config", "communication.json");
            }

            return System.IO.Path.Combine(baseDir, "Config", "communication.json");
        }

        /// <summary>
        /// 验证路径安全性，防止路径遍历攻击
        /// </summary>
        private bool ValidatePathSecurity(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            // 检查路径遍历字符
            if (path.Contains(".."))
                return false;

            // 检查非法字符
            char[] invalidChars = System.IO.Path.GetInvalidPathChars();
            foreach (char c in invalidChars)
            {
                if (path.Contains(c))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        private void LogError(string message)
        {
            System.Diagnostics.Debug.WriteLine($"[CommunicationControl][ERROR] {DateTime.Now:HH:mm:ss.fff} - {message}");
        }

        /// <summary>自动加载参数（运行时 OnLoad 调用）</summary>
        /// <returns>是否成功加载了已保存的参数（true=有配置记录并已加载）</returns>
        private bool AutoLoadParameters()
        {
            if (!AutoPersist || DesignMode) return false;
            var pm = GetParameterManager();
            if (pm == null) return false;
            string group = GetPersistGroupName();
            if (!pm.Contains(group, "Mode")) return false; // 无保存记录，使用默认值

            // 从 ParameterManager 读取并应用到 UI
            LoadParameters(
                pm.GetValue(group, "TcpIp", "127.0.0.1"),
                pm.GetValue(group, "TcpPort", 502),
                pm.GetValue(group, "TcpTimeout", 5000),
                pm.GetValue(group, "ServerIp", "0.0.0.0"),
                pm.GetValue(group, "ServerPort", 502),
                pm.GetValue(group, "SerialPort", "COM1"),
                pm.GetValue(group, "BaudRate", 115200),
                pm.GetValue(group, "Parity", "N"),
                pm.GetValue(group, "DataBits", 8),
                pm.GetValue(group, "StopBits", "1"),
                pm.GetValue(group, "Mode", "Tcp")
            );
            return true;
        }

        /// <summary>自动保存参数</summary>
        private void AutoSaveParameters()
        {
            if (!AutoPersist || DesignMode) return;
            var pm = GetParameterManager();
            if (pm == null) return;
            string group = GetPersistGroupName();

            GetCurrentParameters(out string tcpIp, out int tcpPort, out int tcpTimeout,
                out string serverIp, out int serverPort,
                out string serialPort, out int baudRate, out string parity,
                out int dataBits, out string stopBits, out string mode);

            pm.SetValue(group, "TcpIp", tcpIp);
            pm.SetValue(group, "TcpPort", tcpPort);
            pm.SetValue(group, "TcpTimeout", tcpTimeout);
            pm.SetValue(group, "ServerIp", serverIp);
            pm.SetValue(group, "ServerPort", serverPort);
            pm.SetValue(group, "SerialPort", serialPort);
            pm.SetValue(group, "BaudRate", baudRate);
            pm.SetValue(group, "Parity", parity);
            pm.SetValue(group, "DataBits", dataBits);
            pm.SetValue(group, "StopBits", stopBits);
            pm.SetValue(group, "Mode", mode);
        }

        #endregion

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            TrySubscribeHostFormAutoConnect();
            EnsureApplicationIdleAutoConnectHook();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            EnsureManagerSynchronizationContext();
            TrySubscribeHostFormAutoConnect();
            EnsureApplicationIdleAutoConnectHook();
        }

        /// <summary>
        /// 将管理器事件投递绑定到当前 UI 同步上下文（解决自动连接时 StateChanged 未回到控件线程导致界面仍显示未连接）。
        /// </summary>
        private void EnsureManagerSynchronizationContext()
        {
            var ctx = SynchronizationContext.Current;
            if (ctx != null)
                _manager.UseSynchronizationContext(ctx);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // 非 Tab 场景或主窗体事件未触发时的兜底
            var host = FindForm() ?? TopLevelControl as Form;
            if (host != null)
                TryKickoffAutoLoadAndConnectOnce(host);
            else
            {
                bool hasConfig = AutoLoadParameters();
                if (AutoConnect && !DesignMode && hasConfig)
                    ScheduleAutoConnect();
            }
        }

        /// <summary>
        /// 在宿主窗体 Load/Shown 上订阅，使位于未选中 Tab 内的控件也能在主界面打开后立刻自动连接。
        /// </summary>
        private void TrySubscribeHostFormAutoConnect()
        {
            if (DesignMode) return;
            var form = FindForm() ?? TopLevelControl as Form;
            if (form == null || form.IsDisposed) return;

            bool requestLateKickoff = false;
            lock (_hostFormAutoConnectLock)
            {
                if (_hostFormAutoConnectHooked == form)
                {
                    requestLateKickoff = form.Visible && form.IsHandleCreated;
                }
                else
                {
                    if (_hostFormAutoConnectHooked != null)
                    {
                        try
                        {
                            _hostFormAutoConnectHooked.Load -= _hostFormEarlyAutoConnectHandler;
                            _hostFormAutoConnectHooked.Shown -= _hostFormEarlyAutoConnectHandler;
                        }
                        catch { }
                    }

                    _hostFormAutoConnectHooked = form;
                    form.Load += _hostFormEarlyAutoConnectHandler;
                    form.Shown += _hostFormEarlyAutoConnectHandler;
                    requestLateKickoff = form.Visible && form.IsHandleCreated;
                }
            }

            if (requestLateKickoff && AutoConnect
                && Interlocked.CompareExchange(ref _earlyAutoConnectKickoffDone, 0, 0) == 0)
            {
                try
                {
                    if (!form.IsDisposed && form.IsHandleCreated)
                        form.BeginInvoke(new Action(() => TryKickoffAutoLoadAndConnectOnce(form)));
                }
                catch (ObjectDisposedException) { }
                catch (InvalidOperationException) { }
            }

            // Load/Shown 可能早于本控件挂到窗体；用 Idle 再补一次
            EnsureApplicationIdleAutoConnectHook();
        }

        private void EnsureApplicationIdleAutoConnectHook()
        {
            if (DesignMode || !AutoConnect) return;
            if (Interlocked.CompareExchange(ref _earlyAutoConnectKickoffDone, 0, 0) != 0)
                return;

            lock (_hostFormAutoConnectLock)
            {
                if (_applicationIdleHookActive) return;
                _applicationIdleHookActive = true;
                try
                {
                    Application.Idle += _applicationIdleAutoConnectHandler;
                }
                catch
                {
                    _applicationIdleHookActive = false;
                }
            }
        }

        private void OnApplicationIdleForAutoConnect(object sender, EventArgs e)
        {
            try
            {
                Application.Idle -= _applicationIdleAutoConnectHandler;
            }
            catch { }

            lock (_hostFormAutoConnectLock)
            {
                _applicationIdleHookActive = false;
            }

            if (IsDisposed || !AutoConnect) return;

            TrySubscribeHostFormAutoConnect();

            var form = FindForm() ?? TopLevelControl as Form;
            if (form == null || !form.IsHandleCreated || form.IsDisposed) return;

            try
            {
                form.BeginInvoke(new Action(() =>
                {
                    if (IsDisposed) return;
                    TryKickoffAutoLoadAndConnectOnce(form);
                }));
            }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        private void OnHostFormLoadOrShownForEarlyAutoConnect(object sender, EventArgs e)
        {
            if (IsDisposed) return;
            if (sender is Form f && !f.IsDisposed)
                TryKickoffAutoLoadAndConnectOnce(f);
        }

        /// <summary>
        /// 在主线程加载参数并调度一次连接；多实例/多次 Load+Shown 通过原子标志去重。
        /// </summary>
        private void TryKickoffAutoLoadAndConnectOnce(Form hostForm)
        {
            if (DesignMode || !AutoConnect || IsDisposed) return;
            if (hostForm == null || hostForm.IsDisposed) return;

            if (Interlocked.CompareExchange(ref _earlyAutoConnectKickoffDone, 1, 0) != 0)
                return;

            // 仅当开启持久化时才要求「已有保存分组」；关闭 AutoPersist 时用当前界面默认值连接
            if (AutoPersist)
            {
                bool hasSaved = AutoLoadParameters();
                if (!hasSaved)
                {
                    Interlocked.Exchange(ref _earlyAutoConnectKickoffDone, 0);
                    return;
                }
            }

            UnsubscribeHostFormEarlyAutoConnectEvents();
            ScheduleAutoConnectFromHostForm(hostForm);
        }

        private void UnsubscribeHostFormEarlyAutoConnectEvents()
        {
            lock (_hostFormAutoConnectLock)
            {
                if (_hostFormAutoConnectHooked == null) return;
                try
                {
                    _hostFormAutoConnectHooked.Load -= _hostFormEarlyAutoConnectHandler;
                    _hostFormAutoConnectHooked.Shown -= _hostFormEarlyAutoConnectHandler;
                }
                catch { }
                _hostFormAutoConnectHooked = null;
            }
        }

        /// <summary>设计器 Dispose 中调用，解除宿主窗体与 Idle 上的自动连接订阅。</summary>
        private void DetachHostFormEarlyAutoConnect()
        {
            try
            {
                Application.Idle -= _applicationIdleAutoConnectHandler;
            }
            catch { }

            lock (_hostFormAutoConnectLock)
            {
                _applicationIdleHookActive = false;
            }

            UnsubscribeHostFormEarlyAutoConnectEvents();
        }

        /// <summary>
        /// 在宿主窗体消息队列上执行连接，避免未选中 Tab 内控件尚未 CreateHandle 导致 BeginInvoke 失败。
        /// </summary>
        private void ScheduleAutoConnectFromHostForm(Form hostForm)
        {
            if (hostForm == null || hostForm.IsDisposed || IsDisposed) return;
            try
            {
                if (!hostForm.IsHandleCreated) return;
                hostForm.BeginInvoke(new Action(async () =>
                {
                    if (IsDisposed) return;
                    try
                    {
                        // 与手动连接一致：先绑定 WinForms 同步上下文，再建句柄/同步参数，否则 StateChanged 无法正确刷新本控件 UI
                        EnsureManagerSynchronizationContext();

                        // 未选中 Tab 上的控件可能尚未 CreateControl，先创建句柄再 Sync，避免读到空参数
                        try
                        {
                            if (!IsHandleCreated && Parent != null)
                                CreateControl();
                        }
                        catch (Exception exCreate)
                        {
                            System.Diagnostics.Debug.WriteLine($"[CommunicationControl] 自动连接 CreateControl: {exCreate.Message}");
                        }

                        SyncToManager();
                        await ConnectAsync();

                        // 连接完成后强制对齐一次显示（防止异步投递顺序导致仍显示旧状态）
                        UpdateStateDisplay();
                        UpdateButtonStates();
                        UpdateStats();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"[CommunicationControl] 自动连接失败: {ex.Message}");
                    }
                }));
            }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[CommunicationControl] 调度自动连接失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 调度自动连接（无宿主窗体句柄时退回本控件队列）。
        /// </summary>
        private void ScheduleAutoConnect()
        {
            try
            {
                Form parentForm = FindForm() ?? TopLevelControl as Form;

                if (parentForm == null || parentForm.IsDisposed)
                {
                    if (!DesignMode && IsHandleCreated)
                    {
                        BeginInvoke(new Action(async () =>
                        {
                            try
                            {
                                await ConnectAsync();
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"[CommunicationControl] 自动连接失败: {ex.Message}");
                            }
                        }));
                    }
                    return;
                }

                ScheduleAutoConnectFromHostForm(parentForm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[CommunicationControl] 调度自动连接失败: {ex.Message}");
            }
        }

        #region 绘制

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 圆角卡片边框
            var borderRect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = CreateRoundedRectPath(borderRect, 8))
            using (var pen = new Pen(Color.FromArgb(220, 220, 220), 1f))
            {
                g.DrawPath(pen, path);
            }

            // 分隔线
            using (var pen = new Pen(Color.FromArgb(235, 235, 235), 1f))
            {
                g.DrawLine(pen, 10, 24, Width - 10, 24);   // 标题下方
                g.DrawLine(pen, 10, 130, Width - 10, 130);  // 参数区下方
            }
        }

        private System.Drawing.Drawing2D.GraphicsPath CreateRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        #endregion

        #region 控件事件处理

        private void CboMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateModeVisibility();
            SyncToManager();
        }

        private async void BtnConnect_Click(object sender, EventArgs e)
        {
            // 立即检查控件是否已释放
            if (IsDisposed) return;
            
            try
            {
                btnConnect.Enabled = false;
                
                // 创建新的取消令牌用于此操作
                _asyncOperationCts = new CancellationTokenSource();
                
                // 检查控件是否在同步操作期间被释放
                if (IsDisposed) return;

                EnsureManagerSynchronizationContext();
                
                SyncToManager();
                UpdateStats();
                
                // 检查控件是否在同步操作期间被释放
                if (IsDisposed) return;
                
                // 使用取消令牌进行连接操作（经 ConnectAsync 以清除“手动断开”抑制标志）
                await ConnectAsync();
            }
            catch (OperationCanceledException)
            {
                // 操作被取消，无需处理
            }
            catch (Exception ex)
            {
                // 仅在控件未释放时显示消息框
                if (!IsDisposed)
                {
                    MessageBox.Show(this, $"连接失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                // 仅在控件未释放时更新按钮状态
                if (!IsDisposed)
                {
                    UpdateButtonStates();
                }
                // 清理取消令牌
                _asyncOperationCts?.Dispose();
                _asyncOperationCts = null;
            }
        }

        private async void BtnDisconnect_Click(object sender, EventArgs e)
        {
            // 立即检查控件是否已释放
            if (IsDisposed) return;
            
            try
            {
                btnDisconnect.Enabled = false;
                
                // 创建新的取消令牌用于此操作
                _asyncOperationCts = new CancellationTokenSource();
                
                // 检查控件是否在同步操作期间被释放
                if (IsDisposed) return;

                _reconnectDelayCts?.Cancel();
                Interlocked.Exchange(ref _suppressAutoReconnect, 1);

                await _manager.DisconnectAsync();
            }
            catch (OperationCanceledException)
            {
                // 操作被取消，无需处理
            }
            catch (Exception ex)
            {
                // 仅在控件未释放时显示消息框
                if (!IsDisposed)
                {
                    MessageBox.Show(this, $"断开失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                // 仅在控件未释放时更新按钮状态
                if (!IsDisposed)
                {
                    UpdateButtonStates();
                }
                // 清理取消令牌
                _asyncOperationCts?.Dispose();
                _asyncOperationCts = null;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SyncToManager();
            AutoSaveParameters();
            ParametersSaveRequested?.Invoke(this, EventArgs.Empty);
        }


        private void PnlIndicator_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, pnlIndicator.Width - 1, pnlIndicator.Height - 1);
            using (var brush = new SolidBrush(pnlIndicator.BackColor))
            {
                g.FillEllipse(brush, rect);
            }
        }

        #endregion

        
    }
}
