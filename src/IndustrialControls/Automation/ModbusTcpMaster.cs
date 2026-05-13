using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace IndustrialControls.Automation
{
    public sealed class ModbusTcpMaster : IDisposable
    {
        private readonly string _host;
        private readonly int _port;
        private readonly int _connectTimeoutMs;
        private readonly int _ioTimeoutMs;
        private TcpClient _tcp;
        private NetworkStream _stream;
        private ushort _transactionId;
        private readonly SemaphoreSlim _io = new SemaphoreSlim(1, 1);
        private volatile bool _disposed;

        public ModbusTcpMaster(string host, int port, int connectTimeoutMs = 5000, int ioTimeoutMs = 3000)
        {
            if (string.IsNullOrWhiteSpace(host)) throw new ArgumentException("host 不能为空。", nameof(host));
            if (port <= 0 || port > 65535) throw new ArgumentOutOfRangeException(nameof(port));
            _host = host.Trim();
            _port = port;
            _connectTimeoutMs = connectTimeoutMs;
            _ioTimeoutMs = ioTimeoutMs;
        }

        public bool IsConnected => _tcp != null && _tcp.Connected;

        public async Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            await _io.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (IsConnected) return;
                _tcp = new TcpClient();
                var connectTask = _tcp.ConnectAsync(_host, _port);
                var delayTask = Task.Delay(_connectTimeoutMs, cancellationToken);
                var done = await Task.WhenAny(connectTask, delayTask).ConfigureAwait(false);
                if (done != connectTask)
                {
                    try { _tcp.Close(); } catch { }
                    _tcp = null;
                    cancellationToken.ThrowIfCancellationRequested();
                    throw new TimeoutException($"连接 {_host}:{_port} 超时（{_connectTimeoutMs} ms）。");
                }
                await connectTask.ConfigureAwait(false);
                _stream = _tcp.GetStream();
                _stream.ReadTimeout = _ioTimeoutMs;
                _stream.WriteTimeout = _ioTimeoutMs;
            }
            finally
            {
                _io.Release();
            }
        }

        public void Disconnect()
        {
            try
            {
                _stream?.Dispose();
                _stream = null;
                _tcp?.Close();
                _tcp = null;
            }
            catch { }
        }

        public async Task<ushort[]> ReadHoldingRegistersAsync(byte unitId, ushort startAddress, ushort quantity, CancellationToken cancellationToken = default)
        {
            if (quantity == 0 || quantity > 125)
                throw new ArgumentOutOfRangeException(nameof(quantity));
            var pdu = new byte[6];
            pdu[0] = unitId;
            pdu[1] = 0x03;
            pdu[2] = (byte)(startAddress >> 8);
            pdu[3] = (byte)(startAddress & 0xFF);
            pdu[4] = (byte)(quantity >> 8);
            pdu[5] = (byte)(quantity & 0xFF);
            var adu = await TransactAsync(pdu, cancellationToken).ConfigureAwait(false);
            if (adu.Length < 9)
                throw new IOException("Modbus TCP 响应过短。");
            byte respUnit = adu[6];
            byte fn = adu[7];
            if (respUnit != unitId)
                throw new IOException($"单元号不匹配：期望 {unitId}，实际 {respUnit}。");
            if ((fn & 0x80) != 0)
            {
                byte ex = adu.Length > 8 ? adu[8] : (byte)0;
                throw new IOException($"Modbus 异常应答：功能码 0x{fn:X2}，异常码 {ex}。");
            }
            if (fn != 0x03)
                throw new IOException($"功能码非 0x03：0x{fn:X2}。");
            byte byteCount = adu[8];
            if (byteCount != quantity * 2 || adu.Length < 9 + byteCount)
                throw new IOException("寄存器数据长度与请求数量不一致。");
            var regs = new ushort[quantity];
            for (int i = 0; i < quantity; i++)
            {
                int p = 9 + i * 2;
                regs[i] = (ushort)((adu[p] << 8) | adu[p + 1]);
            }
            return regs;
        }

        public async Task WriteSingleRegisterAsync(byte unitId, ushort address, ushort value, CancellationToken cancellationToken = default)
        {
            var pdu = new byte[6];
            pdu[0] = unitId;
            pdu[1] = 0x06;
            pdu[2] = (byte)(address >> 8);
            pdu[3] = (byte)(address & 0xFF);
            pdu[4] = (byte)(value >> 8);
            pdu[5] = (byte)(value & 0xFF);
            var adu = await TransactAsync(pdu, cancellationToken).ConfigureAwait(false);
            if (adu.Length < 12)
                throw new IOException("Modbus TCP 写单寄存器响应过短。");
            for (int i = 0; i < 6; i++)
            {
                if (adu[6 + i] != pdu[i])
                    throw new IOException("写单寄存器回显与请求不一致。");
            }
        }

        public async Task WriteMultipleRegistersAsync(byte unitId, ushort startAddress, ushort[] values, CancellationToken cancellationToken = default)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            if (values.Length == 0 || values.Length > 123)
                throw new ArgumentOutOfRangeException(nameof(values));
            int byteCount = values.Length * 2;
            var pdu = new byte[7 + byteCount];
            pdu[0] = unitId;
            pdu[1] = 0x10;
            pdu[2] = (byte)(startAddress >> 8);
            pdu[3] = (byte)(startAddress & 0xFF);
            pdu[4] = (byte)(values.Length >> 8);
            pdu[5] = (byte)(values.Length & 0xFF);
            pdu[6] = (byte)byteCount;
            for (int i = 0; i < values.Length; i++)
            {
                pdu[7 + i * 2] = (byte)(values[i] >> 8);
                pdu[8 + i * 2] = (byte)(values[i] & 0xFF);
            }
            var adu = await TransactAsync(pdu, cancellationToken).ConfigureAwait(false);
            if (adu.Length < 12)
                throw new IOException("Modbus TCP 写多寄存器响应过短。");
            if (adu[6] != unitId || adu[7] != 0x10)
                throw new IOException("写多寄存器响应头非法。");
        }

        private async Task<byte[]> TransactAsync(byte[] pdu, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            if (!IsConnected)
                throw new InvalidOperationException("请先调用 ConnectAsync。");
            await _io.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                ushort tid = unchecked(++_transactionId);
                int lenField = pdu.Length;
                var request = new byte[6 + pdu.Length];
                request[0] = (byte)(tid >> 8);
                request[1] = (byte)(tid & 0xFF);
                request[2] = 0;
                request[3] = 0;
                request[4] = (byte)(lenField >> 8);
                request[5] = (byte)(lenField & 0xFF);
                Buffer.BlockCopy(pdu, 0, request, 6, pdu.Length);

                using (var linked = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
                {
                    linked.CancelAfter(_ioTimeoutMs);
                    await _stream.WriteAsync(request, 0, request.Length, linked.Token).ConfigureAwait(false);

                    var header = new byte[6];
                    await ReadExactAsync(_stream, header, 0, 6, linked.Token).ConfigureAwait(false);
                    ushort respTid = (ushort)((header[0] << 8) | header[1]);
                    if (respTid != tid)
                        throw new IOException($"事务 ID 不匹配：发送 {tid}，收到 {respTid}。");
                    if (header[2] != 0 || header[3] != 0)
                        throw new IOException("非 Modbus 协议标识。");
                    int remaining = (header[4] << 8) | header[5];
                    if (remaining < 2 || remaining > 260)
                        throw new IOException($"非法 MBAP 长度：{remaining}。");
                    var body = new byte[6 + remaining];
                    Buffer.BlockCopy(header, 0, body, 0, 6);
                    await ReadExactAsync(_stream, body, 6, remaining, linked.Token).ConfigureAwait(false);
                    return body;
                }
            }
            finally
            {
                _io.Release();
            }
        }

        private static async Task ReadExactAsync(Stream stream, byte[] buffer, int offset, int count, CancellationToken ct)
        {
            int read = 0;
            while (read < count)
            {
                int n = await stream.ReadAsync(buffer, offset + read, count - read, ct).ConfigureAwait(false);
                if (n == 0)
                    throw new IOException("连接已关闭。");
                read += n;
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ModbusTcpMaster));
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            Disconnect();
            _io.Dispose();
        }
    }
}
