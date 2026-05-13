namespace IndustrialControls.Automation
{
    /// <summary>
    /// 工位对外协议类型（与物理链路 Transport 组合使用）。
    /// </summary>
    public enum IndustrialProtocolKind
    {
        Raw = 0,
        ModbusTcp = 1,
        ModbusRtu = 2,
        OpcUa = 3,
        SiemensS7 = 4,
        EtherNetIp = 5
    }
}
