using System.Threading;
using System.Windows.Forms;
using IndustrialControls.Template.Services;

namespace IndustrialControls.Template.Core
{
    /// <summary>跨窗体共享的运行时服务（单进程模板）。</summary>
    public static class TemplateRuntime
    {
        public static SimulatedTagDataSource Tags { get; private set; }

        public static CommunicationService Communication { get; private set; }

        public static AlarmManager Alarms { get; private set; }

        public static DeviceManager Devices { get; private set; }

        public static IDataService Data { get; private set; }

        public static IDeviceService DeviceCommands { get; private set; }

        public static ThemeService Theme { get; private set; }

        public static void Initialize()
        {
            Tags?.Dispose();
            Communication?.Dispose();

            Tags = new SimulatedTagDataSource(SynchronizationContext.Current);
            Communication = new CommunicationService();
            Alarms = new AlarmManager();
            Devices = new DeviceManager();
            var mock = new MockDataService();
            Data = mock;
            DeviceCommands = mock;
            Theme = new ThemeService();

            string line = AppParameters.Machine.Get("LineConfigRelative", @"Config\Models\Default\line.json");
            Communication.ReloadFromRelativePath(line);
        }
    }
}
