namespace IndustrialControls.Template.Services
{
    /// <summary>设备命令下发抽象。</summary>
    public interface IDeviceService
    {
        void SendCommand(string deviceKey, string commandDisplayText);
    }
}
