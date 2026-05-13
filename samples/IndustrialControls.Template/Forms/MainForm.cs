using System;
using System.Reflection;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Theme;
using IndustrialControls.Template.Core;

namespace IndustrialControls.Template.Forms
{
    public partial class MainForm : Form
    {
        private bool _suppressNavSave;

        public MainForm()
        {
            InitializeComponent();
            if (!ControlDesignerHelper.IsDesignMode(this))
            {
                ThemeManager.Instance.RegisterForm(this);
                ThemeManager.Instance.ThemeChanged += ThemeManager_ThemeChanged;
            }
        }

        private void ThemeManager_ThemeChanged(object sender, EventArgs e)
        {
            if (!IsDisposed)
                Invalidate(true);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (ControlDesignerHelper.IsDesignMode(this))
                return;

            try
            {
                ThemeManager.Instance.SetTheme(AppParameters.AppSection.Theme);
            }
            catch
            {
                ThemeManager.Instance.SetTheme("FlatLight");
            }

            UpdateUserInfo();
            treeViewNav.ExpandAll();
            RestoreNavigationSelection();
            BindModelCombo();
            ReloadCommunicationFromParameters();
            SyncConnectButtonsEnabled();

            timerStatus.Start();
            TemplateRuntime.Alarms.Raise(IndustrialControls.Controls.Alarm.AlarmLevel.Info, "主界面已加载。", "App");
        }

        private void BindModelCombo()
        {
            toolStripComboModel.Items.Clear();
            toolStripComboModel.Items.AddRange(new object[] { "Default", "ModelB" });
            string id = AppParameters.Machine.Get("CurrentModelId", "Default");
            int idx = toolStripComboModel.Items.IndexOf(id);
            toolStripComboModel.SelectedIndex = idx >= 0 ? idx : 0;
        }

        private void UpdateUserInfo()
        {
            labelUserInfo.Text = CurrentUser.IsLoggedIn
                ? $"用户: {CurrentUser.Name}"
                : "用户: —";
        }

        private static TreeNode FindNodeByKey(TreeNodeCollection nodes, string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;
            foreach (TreeNode n in nodes)
            {
                if (string.Equals(n.Name, key, StringComparison.Ordinal))
                    return n;
                TreeNode c = FindNodeByKey(n.Nodes, key);
                if (c != null)
                    return c;
            }

            return null;
        }

        private void RestoreNavigationSelection()
        {
            string name = AppParameters.Navigation.LastActiveTab;
            _suppressNavSave = true;
            try
            {
                TreeNode match = FindNodeByKey(treeViewNav.Nodes, name);
                if (match != null)
                    treeViewNav.SelectedNode = match;
                else
                {
                    TreeNode dash = FindNodeByKey(treeViewNav.Nodes, "tabDashboard");
                    if (dash != null)
                        treeViewNav.SelectedNode = dash;
                    else
                        NavigateToPageCore("tabDashboard", persist: false, refreshDashboardIfNeeded: true);
                }
            }
            finally
            {
                _suppressNavSave = false;
            }
        }

        private static bool IsNavPageKey(string name)
        {
            return string.Equals(name, "tabDashboard", StringComparison.Ordinal)
                || string.Equals(name, "tabDeviceMonitor", StringComparison.Ordinal)
                || string.Equals(name, "tabDataVisualization", StringComparison.Ordinal)
                || string.Equals(name, "tabAlarmManagement", StringComparison.Ordinal)
                || string.Equals(name, "tabCommunicationConfig", StringComparison.Ordinal)
                || string.Equals(name, "tabParameterConfig", StringComparison.Ordinal)
                || string.Equals(name, "tabSystemSettings", StringComparison.Ordinal);
        }

        private void ShowPage(string key)
        {
            dashboardPage1.Visible = string.Equals(key, "tabDashboard", StringComparison.Ordinal);
            deviceMonitorPage1.Visible = string.Equals(key, "tabDeviceMonitor", StringComparison.Ordinal);
            dataVisualizationPage1.Visible = string.Equals(key, "tabDataVisualization", StringComparison.Ordinal);
            alarmManagementPage1.Visible = string.Equals(key, "tabAlarmManagement", StringComparison.Ordinal);
            communicationConfigPage1.Visible = string.Equals(key, "tabCommunicationConfig", StringComparison.Ordinal);
            parameterConfigPage1.Visible = string.Equals(key, "tabParameterConfig", StringComparison.Ordinal);
            systemSettingsPage1.Visible = string.Equals(key, "tabSystemSettings", StringComparison.Ordinal);
        }

        private void NavigateToPageCore(string key, bool persist, bool refreshDashboardIfNeeded)
        {
            if (!IsNavPageKey(key))
                return;

            ShowPage(key);

            if (refreshDashboardIfNeeded && string.Equals(key, "tabDashboard", StringComparison.Ordinal))
                dashboardPage1.RefreshSummary();

            if (persist)
                AppParameters.Navigation.LastActiveTab = key;
        }

        private void treeViewNav_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || !IsNavPageKey(e.Node.Name))
                return;

            NavigateToPageCore(e.Node.Name, persist: !_suppressNavSave, refreshDashboardIfNeeded: true);
        }

        private void NavigateToPageFromMenu(string key)
        {
            TreeNode n = FindNodeByKey(treeViewNav.Nodes, key);
            if (n != null)
            {
                treeViewNav.SelectedNode = n;
                return;
            }

            NavigateToPageCore(key, persist: true, refreshDashboardIfNeeded: true);
        }

        private void menuViewPage_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem mi && mi.Tag is string key)
                NavigateToPageFromMenu(key);
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuHelpAbout_Click(object sender, EventArgs e)
        {
            string ver = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "—";
            MessageBox.Show(this,
                "IndustrialControls.Template\r\n版本: " + ver + "\r\n用于演示商业化主窗体壳层（菜单、工具栏、侧栏导航与内容区）。",
                "关于",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ReloadCommunicationFromParameters()
        {
            string rel = AppParameters.Machine.Get("LineConfigRelative", @"Config\Models\Default\line.json");
            TemplateRuntime.Communication.ReloadFromRelativePath(rel);
            toolStripStatusLabelComm.Text = "通信: " + (TemplateRuntime.Communication.LastSummary ?? "—");
            SyncConnectButtonsEnabled();
        }

        private void SyncConnectButtonsEnabled()
        {
            bool has = TemplateRuntime.Communication.HasHost;
            toolStripButtonConnect.Enabled = has;
            toolStripButtonDisconnect.Enabled = has;
            menuToolsConnect.Enabled = has;
            menuToolsDisconnect.Enabled = has;
        }

        private void buttonApplyModel_Click(object sender, EventArgs e)
        {
            string modelId = toolStripComboModel.SelectedItem as string ?? "Default";
            string relative = modelId == "ModelB"
                ? @"Config\Models\ModelB\line.json"
                : @"Config\Models\Default\line.json";
            AppParameters.Machine.Set("CurrentModelId", modelId);
            AppParameters.Machine.Set("LineConfigRelative", relative);
            AppParameters.Settings.SaveToFile();
            ReloadCommunicationFromParameters();
            dashboardPage1.RefreshSummary();
        }

        private async void buttonConnect_Click(object sender, EventArgs e)
        {
            if (!TemplateRuntime.Communication.HasHost)
                return;
            try
            {
                UseWaitCursor = true;
                await TemplateRuntime.Communication.ConnectAllAsync().ConfigureAwait(true);
                toolStripStatusLabelComm.Text = "通信: 已连接";
            }
            catch (Exception ex)
            {
                toolStripStatusLabelComm.Text = "通信: 失败";
                TemplateRuntime.Alarms.Raise(IndustrialControls.Controls.Alarm.AlarmLevel.Important, ex.Message, "Comm");
                MessageBox.Show(this, ex.Message, "连接", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private async void buttonDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;
                await TemplateRuntime.Communication.DisconnectAllAsync().ConfigureAwait(true);
                ReloadCommunicationFromParameters();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "断开", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void buttonToggleTheme_Click(object sender, EventArgs e)
        {
            TemplateRuntime.Theme.Toggle();
            AppParameters.AppSection.Theme = TemplateRuntime.Theme.CurrentName;
            AppParameters.Settings.SaveToFile();
        }

        private void buttonDeviceConfig_Click(object sender, EventArgs e)
        {
            using (var f = new DeviceConfigForm())
            {
                f.ShowDialog(this);
            }

            dashboardPage1.RefreshSummary();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            CurrentUser.Clear();
            Application.Restart();
        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabelTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            toolStripStatusLabelAlarms.Text = "报警缓冲: " + TemplateRuntime.Alarms.ActiveCount;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ThemeManager.Instance.ThemeChanged -= ThemeManager_ThemeChanged;
        }
    }
}
