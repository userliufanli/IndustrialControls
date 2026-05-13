using System;
using System.IO.Ports;
using IndustrialControls.Controls.Communication;

namespace IndustrialControls.Automation
{
    public sealed class StationCommProfile
    {
        public int StationId { get; set; }
        public string Name { get; set; } = "";
        public IndustrialProtocolKind Protocol { get; set; } = IndustrialProtocolKind.Raw;
        public StationTransportKind Transport { get; set; } = StationTransportKind.TcpClient;
        public string TcpRemoteHost { get; set; } = "127.0.0.1";
        public int TcpRemotePort { get; set; } = 502;
        public int TcpConnectTimeoutMs { get; set; } = 5000;
        public string TcpServerBindHost { get; set; } = "0.0.0.0";
        public int TcpServerBindPort { get; set; } = 502;
        public string SerialPortName { get; set; } = "COM1";
        public int SerialBaudRate { get; set; } = 9600;
        public Parity SerialParity { get; set; } = Parity.None;
        public int SerialDataBits { get; set; } = 8;
        public StopBits SerialStopBits { get; set; } = StopBits.One;
        public byte ModbusUnitId { get; set; } = 1;
        public int IoTimeoutMs { get; set; } = 3000;

        public void ApplyToCommunicationManager(CommunicationManager manager, System.Threading.SynchronizationContext uiSync = null)
        {
            if (manager == null) throw new ArgumentNullException(nameof(manager));
            if (Protocol == IndustrialProtocolKind.ModbusTcp)
                throw new InvalidOperationException("Modbus TCP 不使用 CommunicationManager。");
            if (Protocol == IndustrialProtocolKind.OpcUa || Protocol == IndustrialProtocolKind.SiemensS7 || Protocol == IndustrialProtocolKind.EtherNetIp)
                throw new InvalidOperationException("该协议应在主机项目中使用专用库实现。");

            if (uiSync != null)
                manager.UseSynchronizationContext(uiSync);

            switch (Transport)
            {
                case StationTransportKind.TcpClient:
                    manager.Mode = CommunicationMode.Tcp;
                    manager.TcpIp = TcpRemoteHost ?? "127.0.0.1";
                    manager.TcpPort = TcpRemotePort;
                    manager.TcpTimeout = TcpConnectTimeoutMs;
                    break;
                case StationTransportKind.TcpServer:
                    manager.Mode = CommunicationMode.TcpServer;
                    manager.TcpServerIp = string.IsNullOrWhiteSpace(TcpServerBindHost) ? "0.0.0.0" : TcpServerBindHost;
                    manager.TcpServerPort = TcpServerBindPort;
                    break;
                case StationTransportKind.Serial:
                    manager.Mode = CommunicationMode.Serial;
                    manager.SerialPortName = SerialPortName ?? "COM1";
                    manager.SerialBaudRate = SerialBaudRate;
                    manager.SerialParity = SerialParity;
                    manager.SerialDataBits = SerialDataBits;
                    manager.SerialStopBits = SerialStopBits;
                    break;
                default:
                    throw new InvalidOperationException("Transport 未指定。");
            }
        }

        internal void Validate()
        {
            if (StationId < 0)
                throw new InvalidOperationException("StationId 不能为负数。");

            switch (Protocol)
            {
                case IndustrialProtocolKind.Raw:
                    if (Transport == StationTransportKind.TcpClient)
                    {
                        if (string.IsNullOrWhiteSpace(TcpRemoteHost))
                            throw new InvalidOperationException($"工位 {StationId}：Raw + TcpClient 需要 TcpRemoteHost。");
                        if (TcpRemotePort <= 0 || TcpRemotePort > 65535)
                            throw new InvalidOperationException($"工位 {StationId}：TcpRemotePort 非法。");
                    }
                    else if (Transport == StationTransportKind.TcpServer)
                    {
                        if (TcpServerBindPort <= 0 || TcpServerBindPort > 65535)
                            throw new InvalidOperationException($"工位 {StationId}：TcpServerBindPort 非法。");
                    }
                    else if (Transport == StationTransportKind.Serial)
                    {
                        if (string.IsNullOrWhiteSpace(SerialPortName))
                            throw new InvalidOperationException($"工位 {StationId}：Raw + Serial 需要 SerialPortName。");
                    }
                    else
                        throw new InvalidOperationException($"工位 {StationId}：Raw 需要 Transport。");
                    break;

                case IndustrialProtocolKind.ModbusTcp:
                    if (string.IsNullOrWhiteSpace(TcpRemoteHost))
                        throw new InvalidOperationException($"工位 {StationId}：ModbusTcp 需要 TcpRemoteHost。");
                    if (TcpRemotePort <= 0 || TcpRemotePort > 65535)
                        throw new InvalidOperationException($"工位 {StationId}：ModbusTcp TcpRemotePort 非法。");
                    break;

                case IndustrialProtocolKind.ModbusRtu:
                    if (Transport != StationTransportKind.Serial)
                        throw new InvalidOperationException($"工位 {StationId}：ModbusRtu 仅支持 Serial。");
                    if (string.IsNullOrWhiteSpace(SerialPortName))
                        throw new InvalidOperationException($"工位 {StationId}：ModbusRtu 需要 SerialPortName。");
                    break;

                case IndustrialProtocolKind.OpcUa:
                case IndustrialProtocolKind.SiemensS7:
                case IndustrialProtocolKind.EtherNetIp:
                    throw new NotSupportedException(
                        $"工位 {StationId}：协议「{Protocol}」需在库外实现 IStationCommSession。");

                default:
                    throw new InvalidOperationException($"工位 {StationId}：未知协议 {Protocol}。");
            }
        }
    }
}
