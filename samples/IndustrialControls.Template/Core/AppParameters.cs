using System;
using System.IO;
using System.Windows.Forms;
using IndustrialControls.Core;

namespace IndustrialControls.Template.Core
{
    /// <summary>
    /// 模板参数入口：<c>Config/appsettings.json</c> 与 <c>Config/communication.json</c>。
    /// </summary>
    public static class AppParameters
    {
        public const string LoginUsersGroupName = "LoginUsers";

        private static readonly Lazy<ParameterManager> SettingsLazy = new Lazy<ParameterManager>(() =>
            new ParameterManager(Path.Combine(Application.StartupPath, "Config", "appsettings.json"))
            {
                EnableHotReload = true
            });

        public static ParameterManager Settings => SettingsLazy.Value;

        public static ParameterSection Machine => Settings.Section("Machine");

        public static ParameterSection App => Settings.Section("App");

        public static ParameterSection Ui => Settings.Section("UI");

        public static ParameterSection Security => Settings.Section("Security");

        public static ParameterSection LoginUsers => Settings.Section(LoginUsersGroupName);

        private static readonly Lazy<ParameterManager> CommLazy = new Lazy<ParameterManager>(() =>
            ParameterAccessor.Register(
                "TemplateCommunication",
                Path.Combine(Application.StartupPath, "Config", "communication.json")));

        public static ParameterManager CommunicationConfig => CommLazy.Value;

        public static ParameterSection Comm => CommunicationConfig.Section("CommConfig");

        public static class Navigation
        {
            public static string LastActiveTab
            {
                get => Ui.Get("LastActiveTab", "tabDashboard");
                set => Ui.Set("LastActiveTab", value);
            }
        }

        public static class AppSection
        {
            public static string Theme
            {
                get => App.Get("Theme", "FlatLight");
                set => App.Set("Theme", value);
            }

            public static string LoginManagementUnlockPin
            {
                get => App.Get("LoginManagementUnlockPin", "1688");
                set => App.Set("LoginManagementUnlockPin", value);
            }
        }
    }
}
