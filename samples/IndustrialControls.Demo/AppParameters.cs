using System;
using IndustrialControls.Core;

namespace IndustrialControls.Demo
{
    /// <summary>
    /// 应用参数 - 直接属性访问方式
    /// </summary>
    public static class AppParameters
    {
        #region 默认配置（parameters.json）

        /// <summary>
        /// 默认参数管理器
        /// </summary>
        public static ParameterManager Default => ParameterAccessor.Default;

        private static readonly ParameterSection _app = ParameterAccessor.Section("App");
        private static readonly ParameterSection _ui = ParameterAccessor.Section("UI");

        /// <summary>
        /// App 参数组
        /// </summary>
        public static class App
        {
            public static string Language
            {
                get => _app.Get("Language", "zh-CN");
                set => _app.Set("Language", value);
            }

            public static string Theme
            {
                get => _app.Get("Theme", "浅色");
                set => _app.Set("Theme", value);
            }

            public static bool AutoSave
            {
                get => _app.Get("AutoSave", true);
                set => _app.Set("AutoSave", value);
            }

            public static string LastOpenedPath
            {
                get => _app.Get("LastOpenedPath", "");
                set => _app.Set("LastOpenedPath", value);
            }

            /// <summary>
            /// 演示/运维：打开「用户管理」前校验的口令（默认 1688，可在 parameters.json 的 App 组修改）。正式项目请改为 RBAC 或服务端鉴权。
            /// </summary>
            public static string LoginManagementUnlockPin
            {
                get => _app.Get("LoginManagementUnlockPin", "1688");
                set => _app.Set("LoginManagementUnlockPin", value);
            }
        }

        /// <summary>
        /// UI 参数组
        /// </summary>
        public static class UI
        {
            public static int WindowWidth
            {
                get => _ui.Get("WindowWidth", 800);
                set => _ui.Set("WindowWidth", value);
            }

            public static int WindowHeight
            {
                get => _ui.Get("WindowHeight", 600);
                set => _ui.Set("WindowHeight", value);
            }

            public static int FontSize
            {
                get => _ui.Get("FontSize", 12);
                set => _ui.Set("FontSize", value);
            }

            public static string WindowState
            {
                get => _ui.Get("WindowState", "Normal");
                set => _ui.Set("WindowState", value);
            }
        }

        /// <summary>
        /// 登录演示与登录控件使用的参数分组名（默认 parameters.json）。
        /// </summary>
        public const string LoginUsersGroupName = "LoginUsers";

        private static readonly ParameterSection _loginUsers = ParameterAccessor.Section(LoginUsersGroupName);

        /// <summary>
        /// 登录用户凭据所在分组（键 <c>CredentialList</c> 存 JSON 列表）。
        /// </summary>
        public static ParameterSection LoginUsers => _loginUsers;

        #endregion

        #region 通讯配置（Config/communication.json）

        public static ParameterManager CommConfig => ParameterAccessor.Register(
            "Communication",
            System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "communication.json"));

        private static readonly ParameterSection _comm = CommConfig.Section("CommConfig");

        public static class Comm
        {
            public static string TcpIp
            {
                get => _comm.Get("TcpIp", "127.0.0.1");
                set => _comm.Set("TcpIp", value);
            }

            public static int TcpPort
            {
                get => _comm.Get("TcpPort", 5000);
                set => _comm.Set("TcpPort", value);
            }

            public static int TcpTimeout
            {
                get => _comm.Get("TcpTimeout", 3000);
                set => _comm.Set("TcpTimeout", value);
            }

            public static string SerialPortName
            {
                get => _comm.Get("SerialPortName", "COM1");
                set => _comm.Set("SerialPortName", value);
            }

            public static int SerialBaudRate
            {
                get => _comm.Get("SerialBaudRate", 115200);
                set => _comm.Set("SerialBaudRate", value);
            }

            public static string Mode
            {
                get => _comm.Get("Mode", "Tcp");
                set => _comm.Set("Mode", value);
            }
        }

        #endregion
    }
}
