using System;
using System.IO;
using System.Windows.Forms;
using IndustrialControls.Controls.VirtualKeyboard;
using IndustrialControls.Core;
using IndustrialControls.Template.Core;

namespace IndustrialControls.Template
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string logDir = Path.Combine(Application.StartupPath, "Logs");
            Logger.Initialize(logDir);

            ParameterManager settings = AppParameters.Settings;
            TemplateBootstrap.EnsureDefaultsAndAdmin(settings);

            VirtualKeyboardManager.Initialize();

            using (var login = new Forms.LoginForm())
            {
                if (login.ShowDialog() != DialogResult.OK)
                    return;
            }

            TemplateRuntime.Initialize();
            Application.Run(new Forms.MainForm());
        }
    }
}
