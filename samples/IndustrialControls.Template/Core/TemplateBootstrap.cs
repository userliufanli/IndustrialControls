using System.IO;
using System.Windows.Forms;
using IndustrialControls.Controls.Login;
using IndustrialControls.Core;

namespace IndustrialControls.Template.Core
{
    /// <summary>首次运行写入默认参数与演示账号。</summary>
    public static class TemplateBootstrap
    {
        public static void EnsureDefaultsAndAdmin(ParameterManager settings)
        {
            var machine = settings.Section("Machine");
            if (!machine.Contains("CurrentModelId"))
                machine.Set("CurrentModelId", "Default");
            if (!machine.Contains("LineConfigRelative"))
                machine.Set("LineConfigRelative", @"Config\Models\Default\line.json");

            var app = settings.Section("App");
            if (!app.Contains("Theme"))
                app.Set("Theme", "FlatLight");

            var ui = settings.Section("UI");
            if (!ui.Contains("LastActiveTab"))
                ui.Set("LastActiveTab", "tabDashboard");

            var sec = settings.Section("Security");
            if (!sec.Contains("FailedLoginCount"))
                sec.Set("FailedLoginCount", 0);

            var store = new LoginUserStore(settings.Section(AppParameters.LoginUsersGroupName));
            if (store.ListUserNames().Count == 0)
            {
                if (!store.TryAddUser("admin", "admin", out var err))
                    Logger.Warn("未能创建默认 admin 用户: " + err);
            }

            settings.SaveToFile();

            var commPath = Path.Combine(Application.StartupPath, "Config", "communication.json");
            if (!File.Exists(commPath))
            {
                var comm = AppParameters.CommunicationConfig;
                comm.Section("CommConfig").Set("TcpIp", "127.0.0.1");
                comm.Section("CommConfig").Set("TcpPort", 502);
                comm.Section("CommConfig").Set("Mode", "Tcp");
                comm.SaveToFile();
            }
        }
    }
}
