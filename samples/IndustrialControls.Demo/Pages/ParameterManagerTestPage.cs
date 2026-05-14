using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Demo;

namespace IndustrialControls.Demo.Pages
{
    /// <summary>
    /// 参数示例页：统一通过 <see cref="IndustrialControls.Demo.AppParameters"/> 读写持久化参数。
    /// 控件均在 Designer 中声明与布局，此处仅含业务逻辑与事件处理。
    /// </summary>
    public partial class ParameterManagerTestPage : UserControl
    {
        public ParameterManagerTestPage()
        {
            InitializeComponent();
            DemoUiChrome.ApplyPage(this);

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                InitializeLogic();
            }
        }

        private void InitializeLogic()
        {
            LoadAllParameters();

            AppParameters.Default.ParameterChanged += OnGlobalParameterChanged;

            Log("=== AppParameters 已初始化 ===");
            Log($"配置文件: {ParameterAccessor.GetDefaultConfigPath()}");
            Log("提示: 修改参数后会自动保存");
        }

        private void LoadAllParameters()
        {
            txtDirectLanguage.Text = AppParameters.App.Language;
            var theme = AppParameters.App.Theme;
            bool themeFound = false;
            for (int i = 0; i < cmbDirectTheme.Items.Count; i++)
            {
                if (string.Equals(cmbDirectTheme.Items[i]?.ToString(), theme, StringComparison.Ordinal))
                {
                    cmbDirectTheme.SelectedIndex = i;
                    themeFound = true;
                    break;
                }
            }
            if (!themeFound && cmbDirectTheme.Items.Count > 0)
                cmbDirectTheme.SelectedIndex = 0;

            chkDirectAutoSave.Checked = AppParameters.App.AutoSave;

            numDirectWidth.Value = ClampDecimal(numDirectWidth, AppParameters.UI.WindowWidth);
            numDirectHeight.Value = ClampDecimal(numDirectHeight, AppParameters.UI.WindowHeight);
        }

        private static decimal ClampDecimal(NumericUpDown n, int value)
        {
            return Math.Min(Math.Max(n.Minimum, value), n.Maximum);
        }

        private void OnGlobalParameterChanged(object sender, ParameterChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnGlobalParameterChanged(sender, e)));
                return;
            }

            string info;
            switch (e.ChangeType)
            {
                case ParameterChangeType.Added:
                    info = $"[新增] {e.GroupName}.{e.Key} = {FormatValue(e.Value)}";
                    break;
                case ParameterChangeType.Modified:
                    info = $"[修改] {e.GroupName}.{e.Key} = {FormatValue(e.Value)}";
                    break;
                case ParameterChangeType.Deleted:
                    info = $"[删除] {e.GroupName}.{e.Key}";
                    break;
                case ParameterChangeType.GroupDeleted:
                    info = $"[删除分组] {e.GroupName}";
                    break;
                case ParameterChangeType.Reloaded:
                    info = "[重载] 配置文件被外部修改";
                    break;
                default:
                    info = $"[{e.ChangeType}] {e.GroupName}.{e.Key}";
                    break;
            }

            Log(info);

            if (e.ChangeType == ParameterChangeType.Reloaded)
                LoadAllParameters();
        }

        private static string FormatValue(object value)
        {
            if (value == null) return "null";
            if (value is string s) return $"\"{s}\"";
            return value.ToString();
        }

        private void txtDirectLanguage_Leave(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            AppParameters.App.Language = txtDirectLanguage.Text;
            Log($"[AppParameters] App.Language = \"{txtDirectLanguage.Text}\"");
        }

        private void cmbDirectTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            if (cmbDirectTheme.SelectedItem != null)
            {
                AppParameters.App.Theme = cmbDirectTheme.SelectedItem.ToString();
                Log($"[AppParameters] App.Theme = \"{AppParameters.App.Theme}\"");
            }
        }

        private void chkDirectAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            AppParameters.App.AutoSave = chkDirectAutoSave.Checked;
            Log($"[AppParameters] App.AutoSave = {AppParameters.App.AutoSave}");
        }

        private void numDirectWidth_ValueChanged(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            AppParameters.UI.WindowWidth = (int)numDirectWidth.Value;
            Log($"[AppParameters] UI.WindowWidth = {AppParameters.UI.WindowWidth}");
        }

        private void numDirectHeight_ValueChanged(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            AppParameters.UI.WindowHeight = (int)numDirectHeight.Value;
            Log($"[AppParameters] UI.WindowHeight = {AppParameters.UI.WindowHeight}");
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtEventLog.Clear();
        }

        private void btnOpenConfig_Click(object sender, EventArgs e)
        {
            try
            {
                string path = ParameterAccessor.GetDefaultConfigPath();
                string folder = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(folder) && Directory.Exists(folder))
                {
                    Process.Start("explorer.exe", folder);
                    Log($"[系统] 打开配置目录: {folder}");
                }
            }
            catch (Exception ex)
            {
                Log($"[错误] {ex.Message}");
            }
        }

        private void Log(string message)
        {
            if (txtEventLog == null) return;
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => Log(message)));
                return;
            }
            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            txtEventLog.AppendText($"[{timestamp}] {message}\r\n");
            txtEventLog.SelectionStart = txtEventLog.Text.Length;
            txtEventLog.ScrollToCaret();
        }
    }
}
