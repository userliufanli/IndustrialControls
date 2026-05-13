namespace IndustrialControls.Template.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelShell;
        private System.Windows.Forms.Panel panelBranding;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label labelUserInfo;
        private System.Windows.Forms.Button buttonToggleTheme;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Button buttonDeviceConfig;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuView;
        private System.Windows.Forms.ToolStripMenuItem menuViewDashboard;
        private System.Windows.Forms.ToolStripMenuItem menuViewDeviceMonitor;
        private System.Windows.Forms.ToolStripMenuItem menuViewDataVisualization;
        private System.Windows.Forms.ToolStripMenuItem menuViewAlarmManagement;
        private System.Windows.Forms.ToolStripMenuItem menuViewCommunicationConfig;
        private System.Windows.Forms.ToolStripMenuItem menuViewParameterConfig;
        private System.Windows.Forms.ToolStripMenuItem menuViewSystemSettings;
        private System.Windows.Forms.ToolStripMenuItem menuTools;
        private System.Windows.Forms.ToolStripMenuItem menuToolsConnect;
        private System.Windows.Forms.ToolStripMenuItem menuToolsDisconnect;
        private System.Windows.Forms.ToolStripMenuItem menuToolsApplyModel;
        private System.Windows.Forms.ToolStripMenuItem menuToolsDeviceConfig;
        private System.Windows.Forms.ToolStripMenuItem menuToolsToggleTheme;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonConnect;
        private System.Windows.Forms.ToolStripButton toolStripButtonDisconnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelModel;
        private System.Windows.Forms.ToolStripComboBox toolStripComboModel;
        private System.Windows.Forms.ToolStripButton toolStripButtonApplyModel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeviceConfig;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.TreeView treeViewNav;
        private System.Windows.Forms.Panel panelContentHost;
        private IndustrialControls.Template.Pages.DashboardPage dashboardPage1;
        private IndustrialControls.Template.Pages.DeviceMonitorPage deviceMonitorPage1;
        private IndustrialControls.Template.Pages.DataVisualizationPage dataVisualizationPage1;
        private IndustrialControls.Template.Pages.AlarmManagementPage alarmManagementPage1;
        private IndustrialControls.Template.Pages.CommunicationConfigPage communicationConfigPage1;
        private IndustrialControls.Template.Pages.ParameterConfigPage parameterConfigPage1;
        private IndustrialControls.Template.Pages.SystemSettingsPage systemSettingsPage1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelComm;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAlarms;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTime;
        private System.Windows.Forms.Timer timerStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("仪表盘");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("运行总览", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("设备监控");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("数据可视化");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("报警管理");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("设备与数据", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("通信配置");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("参数配置");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("系统设置");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("工程与服务", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9});
            this.panelShell = new System.Windows.Forms.Panel();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.treeViewNav = new System.Windows.Forms.TreeView();
            this.panelContentHost = new System.Windows.Forms.Panel();
            this.dashboardPage1 = new IndustrialControls.Template.Pages.DashboardPage();
            this.deviceMonitorPage1 = new IndustrialControls.Template.Pages.DeviceMonitorPage();
            this.dataVisualizationPage1 = new IndustrialControls.Template.Pages.DataVisualizationPage();
            this.alarmManagementPage1 = new IndustrialControls.Template.Pages.AlarmManagementPage();
            this.communicationConfigPage1 = new IndustrialControls.Template.Pages.CommunicationConfigPage();
            this.parameterConfigPage1 = new IndustrialControls.Template.Pages.ParameterConfigPage();
            this.systemSettingsPage1 = new IndustrialControls.Template.Pages.SystemSettingsPage();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDisconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelModel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboModel = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonApplyModel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDeviceConfig = new System.Windows.Forms.ToolStripButton();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewDashboard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewDeviceMonitor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewDataVisualization = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewAlarmManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewCommunicationConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewParameterConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewSystemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsApplyModel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsDeviceConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsToggleTheme = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.panelBranding = new System.Windows.Forms.Panel();
            this.buttonDeviceConfig = new System.Windows.Forms.Button();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonToggleTheme = new System.Windows.Forms.Button();
            this.labelUserInfo = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelComm = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelAlarms = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            this.panelShell.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.panelContentHost.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.panelBranding.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            //
            // panelShell
            //
            this.panelShell.Controls.Add(this.splitMain);
            this.panelShell.Controls.Add(this.toolStripMain);
            this.panelShell.Controls.Add(this.menuStripMain);
            this.panelShell.Controls.Add(this.panelBranding);
            this.panelShell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelShell.Location = new System.Drawing.Point(0, 0);
            this.panelShell.Name = "panelShell";
            this.panelShell.Size = new System.Drawing.Size(1184, 708);
            this.panelShell.TabIndex = 0;
            //
            // splitMain
            //
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitMain.Location = new System.Drawing.Point(0, 120);
            this.splitMain.Name = "splitMain";
            //
            // splitMain.Panel1
            //
            this.splitMain.Panel1.Controls.Add(this.treeViewNav);
            this.splitMain.Panel1MinSize = 160;
            //
            // splitMain.Panel2
            //
            this.splitMain.Panel2.Controls.Add(this.panelContentHost);
            this.splitMain.Size = new System.Drawing.Size(1184, 588);
            this.splitMain.SplitterDistance = 232;
            this.splitMain.SplitterWidth = 5;
            this.splitMain.TabIndex = 3;
            //
            // treeViewNav
            //
            this.treeViewNav.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewNav.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.5F);
            this.treeViewNav.FullRowSelect = true;
            this.treeViewNav.HideSelection = false;
            this.treeViewNav.HotTracking = true;
            this.treeViewNav.ItemHeight = 22;
            this.treeViewNav.Location = new System.Drawing.Point(0, 0);
            this.treeViewNav.Name = "treeViewNav";
            treeNode1.Name = "tabDashboard";
            treeNode1.Text = "仪表盘";
            treeNode2.Name = "grpOverview";
            treeNode2.Text = "运行总览";
            treeNode3.Name = "tabDeviceMonitor";
            treeNode3.Text = "设备监控";
            treeNode4.Name = "tabDataVisualization";
            treeNode4.Text = "数据可视化";
            treeNode5.Name = "tabAlarmManagement";
            treeNode5.Text = "报警管理";
            treeNode6.Name = "grpDeviceData";
            treeNode6.Text = "设备与数据";
            treeNode7.Name = "tabCommunicationConfig";
            treeNode7.Text = "通信配置";
            treeNode8.Name = "tabParameterConfig";
            treeNode8.Text = "参数配置";
            treeNode9.Name = "tabSystemSettings";
            treeNode9.Text = "系统设置";
            treeNode10.Name = "grpEngineering";
            treeNode10.Text = "工程与服务";
            this.treeViewNav.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode6,
            treeNode10});
            this.treeViewNav.ShowLines = true;
            this.treeViewNav.ShowPlusMinus = true;
            this.treeViewNav.ShowRootLines = true;
            this.treeViewNav.Size = new System.Drawing.Size(232, 588);
            this.treeViewNav.TabIndex = 0;
            this.treeViewNav.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewNav_AfterSelect);
            //
            // panelContentHost
            //
            this.panelContentHost.Controls.Add(this.dashboardPage1);
            this.panelContentHost.Controls.Add(this.deviceMonitorPage1);
            this.panelContentHost.Controls.Add(this.dataVisualizationPage1);
            this.panelContentHost.Controls.Add(this.alarmManagementPage1);
            this.panelContentHost.Controls.Add(this.communicationConfigPage1);
            this.panelContentHost.Controls.Add(this.parameterConfigPage1);
            this.panelContentHost.Controls.Add(this.systemSettingsPage1);
            this.panelContentHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContentHost.Location = new System.Drawing.Point(0, 0);
            this.panelContentHost.Name = "panelContentHost";
            this.panelContentHost.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.panelContentHost.Size = new System.Drawing.Size(947, 588);
            this.panelContentHost.TabIndex = 0;
            //
            // dashboardPage1
            //
            this.dashboardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboardPage1.Location = new System.Drawing.Point(0, 1);
            this.dashboardPage1.Name = "dashboardPage1";
            this.dashboardPage1.Size = new System.Drawing.Size(947, 587);
            this.dashboardPage1.TabIndex = 0;
            this.dashboardPage1.Visible = true;
            //
            // deviceMonitorPage1
            //
            this.deviceMonitorPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceMonitorPage1.Location = new System.Drawing.Point(0, 1);
            this.deviceMonitorPage1.Name = "deviceMonitorPage1";
            this.deviceMonitorPage1.Size = new System.Drawing.Size(947, 587);
            this.deviceMonitorPage1.TabIndex = 1;
            this.deviceMonitorPage1.Visible = false;
            //
            // dataVisualizationPage1
            //
            this.dataVisualizationPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataVisualizationPage1.Location = new System.Drawing.Point(0, 1);
            this.dataVisualizationPage1.Name = "dataVisualizationPage1";
            this.dataVisualizationPage1.Size = new System.Drawing.Size(947, 587);
            this.dataVisualizationPage1.TabIndex = 2;
            this.dataVisualizationPage1.Visible = false;
            //
            // alarmManagementPage1
            //
            this.alarmManagementPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alarmManagementPage1.Location = new System.Drawing.Point(0, 1);
            this.alarmManagementPage1.Name = "alarmManagementPage1";
            this.alarmManagementPage1.Size = new System.Drawing.Size(947, 587);
            this.alarmManagementPage1.TabIndex = 3;
            this.alarmManagementPage1.Visible = false;
            //
            // communicationConfigPage1
            //
            this.communicationConfigPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.communicationConfigPage1.Location = new System.Drawing.Point(0, 1);
            this.communicationConfigPage1.Name = "communicationConfigPage1";
            this.communicationConfigPage1.Size = new System.Drawing.Size(947, 587);
            this.communicationConfigPage1.TabIndex = 4;
            this.communicationConfigPage1.Visible = false;
            //
            // parameterConfigPage1
            //
            this.parameterConfigPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parameterConfigPage1.Location = new System.Drawing.Point(0, 1);
            this.parameterConfigPage1.Name = "parameterConfigPage1";
            this.parameterConfigPage1.Size = new System.Drawing.Size(947, 587);
            this.parameterConfigPage1.TabIndex = 5;
            this.parameterConfigPage1.Visible = false;
            //
            // systemSettingsPage1
            //
            this.systemSettingsPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.systemSettingsPage1.Location = new System.Drawing.Point(0, 1);
            this.systemSettingsPage1.Name = "systemSettingsPage1";
            this.systemSettingsPage1.Size = new System.Drawing.Size(947, 587);
            this.systemSettingsPage1.TabIndex = 6;
            this.systemSettingsPage1.Visible = false;
            //
            // toolStripMain
            //
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonConnect,
            this.toolStripButtonDisconnect,
            this.toolStripSeparator1,
            this.toolStripLabelModel,
            this.toolStripComboModel,
            this.toolStripButtonApplyModel,
            this.toolStripSeparator2,
            this.toolStripButtonDeviceConfig});
            this.toolStripMain.Location = new System.Drawing.Point(0, 93);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Padding = new System.Windows.Forms.Padding(6, 0, 1, 0);
            this.toolStripMain.Size = new System.Drawing.Size(1184, 27);
            this.toolStripMain.TabIndex = 2;
            this.toolStripMain.Text = "toolStripMain";
            //
            // toolStripButtonConnect
            //
            this.toolStripButtonConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonConnect.Name = "toolStripButtonConnect";
            this.toolStripButtonConnect.Size = new System.Drawing.Size(36, 24);
            this.toolStripButtonConnect.Text = "连接";
            this.toolStripButtonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            //
            // toolStripButtonDisconnect
            //
            this.toolStripButtonDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDisconnect.Name = "toolStripButtonDisconnect";
            this.toolStripButtonDisconnect.Size = new System.Drawing.Size(36, 24);
            this.toolStripButtonDisconnect.Text = "断开";
            this.toolStripButtonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            //
            // toolStripSeparator1
            //
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            //
            // toolStripLabelModel
            //
            this.toolStripLabelModel.Name = "toolStripLabelModel";
            this.toolStripLabelModel.Size = new System.Drawing.Size(35, 24);
            this.toolStripLabelModel.Text = "机型";
            //
            // toolStripComboModel
            //
            this.toolStripComboModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboModel.Name = "toolStripComboModel";
            this.toolStripComboModel.Size = new System.Drawing.Size(110, 27);
            //
            // toolStripButtonApplyModel
            //
            this.toolStripButtonApplyModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonApplyModel.Name = "toolStripButtonApplyModel";
            this.toolStripButtonApplyModel.Size = new System.Drawing.Size(60, 24);
            this.toolStripButtonApplyModel.Text = "应用机型";
            this.toolStripButtonApplyModel.Click += new System.EventHandler(this.buttonApplyModel_Click);
            //
            // toolStripSeparator2
            //
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            //
            // toolStripButtonDeviceConfig
            //
            this.toolStripButtonDeviceConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDeviceConfig.Name = "toolStripButtonDeviceConfig";
            this.toolStripButtonDeviceConfig.Size = new System.Drawing.Size(60, 24);
            this.toolStripButtonDeviceConfig.Text = "设备配置";
            this.toolStripButtonDeviceConfig.Click += new System.EventHandler(this.buttonDeviceConfig_Click);
            //
            // menuStripMain
            //
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuView,
            this.menuTools,
            this.menuHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 64);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStripMain.Size = new System.Drawing.Size(1184, 29);
            this.menuStripMain.TabIndex = 1;
            this.menuStripMain.Text = "menuStripMain";
            //
            // menuFile
            //
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(58, 25);
            this.menuFile.Text = "文件(&F)";
            //
            // menuFileExit
            //
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.Size = new System.Drawing.Size(180, 22);
            this.menuFileExit.Text = "退出(&X)";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            //
            // menuView
            //
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuViewDashboard,
            this.menuViewDeviceMonitor,
            this.menuViewDataVisualization,
            this.menuViewAlarmManagement,
            this.menuViewCommunicationConfig,
            this.menuViewParameterConfig,
            this.menuViewSystemSettings});
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(59, 25);
            this.menuView.Text = "视图(&V)";
            //
            // menuViewDashboard
            //
            this.menuViewDashboard.Name = "menuViewDashboard";
            this.menuViewDashboard.Size = new System.Drawing.Size(180, 22);
            this.menuViewDashboard.Tag = "tabDashboard";
            this.menuViewDashboard.Text = "仪表盘";
            this.menuViewDashboard.Click += new System.EventHandler(this.menuViewPage_Click);
            //
            // menuViewDeviceMonitor
            //
            this.menuViewDeviceMonitor.Name = "menuViewDeviceMonitor";
            this.menuViewDeviceMonitor.Size = new System.Drawing.Size(180, 22);
            this.menuViewDeviceMonitor.Tag = "tabDeviceMonitor";
            this.menuViewDeviceMonitor.Text = "设备监控";
            this.menuViewDeviceMonitor.Click += new System.EventHandler(this.menuViewPage_Click);
            //
            // menuViewDataVisualization
            //
            this.menuViewDataVisualization.Name = "menuViewDataVisualization";
            this.menuViewDataVisualization.Size = new System.Drawing.Size(180, 22);
            this.menuViewDataVisualization.Tag = "tabDataVisualization";
            this.menuViewDataVisualization.Text = "数据可视化";
            this.menuViewDataVisualization.Click += new System.EventHandler(this.menuViewPage_Click);
            //
            // menuViewAlarmManagement
            //
            this.menuViewAlarmManagement.Name = "menuViewAlarmManagement";
            this.menuViewAlarmManagement.Size = new System.Drawing.Size(180, 22);
            this.menuViewAlarmManagement.Tag = "tabAlarmManagement";
            this.menuViewAlarmManagement.Text = "报警管理";
            this.menuViewAlarmManagement.Click += new System.EventHandler(this.menuViewPage_Click);
            //
            // menuViewCommunicationConfig
            //
            this.menuViewCommunicationConfig.Name = "menuViewCommunicationConfig";
            this.menuViewCommunicationConfig.Size = new System.Drawing.Size(180, 22);
            this.menuViewCommunicationConfig.Tag = "tabCommunicationConfig";
            this.menuViewCommunicationConfig.Text = "通信配置";
            this.menuViewCommunicationConfig.Click += new System.EventHandler(this.menuViewPage_Click);
            //
            // menuViewParameterConfig
            //
            this.menuViewParameterConfig.Name = "menuViewParameterConfig";
            this.menuViewParameterConfig.Size = new System.Drawing.Size(180, 22);
            this.menuViewParameterConfig.Tag = "tabParameterConfig";
            this.menuViewParameterConfig.Text = "参数配置";
            this.menuViewParameterConfig.Click += new System.EventHandler(this.menuViewPage_Click);
            //
            // menuViewSystemSettings
            //
            this.menuViewSystemSettings.Name = "menuViewSystemSettings";
            this.menuViewSystemSettings.Size = new System.Drawing.Size(180, 22);
            this.menuViewSystemSettings.Tag = "tabSystemSettings";
            this.menuViewSystemSettings.Text = "系统设置";
            this.menuViewSystemSettings.Click += new System.EventHandler(this.menuViewPage_Click);
            //
            // menuTools
            //
            this.menuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolsConnect,
            this.menuToolsDisconnect,
            this.menuToolsApplyModel,
            this.menuToolsDeviceConfig,
            this.menuToolsToggleTheme});
            this.menuTools.Name = "menuTools";
            this.menuTools.Size = new System.Drawing.Size(59, 25);
            this.menuTools.Text = "工具(&T)";
            //
            // menuToolsConnect
            //
            this.menuToolsConnect.Name = "menuToolsConnect";
            this.menuToolsConnect.Size = new System.Drawing.Size(180, 22);
            this.menuToolsConnect.Text = "连接通信";
            this.menuToolsConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            //
            // menuToolsDisconnect
            //
            this.menuToolsDisconnect.Name = "menuToolsDisconnect";
            this.menuToolsDisconnect.Size = new System.Drawing.Size(180, 22);
            this.menuToolsDisconnect.Text = "断开通信";
            this.menuToolsDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            //
            // menuToolsApplyModel
            //
            this.menuToolsApplyModel.Name = "menuToolsApplyModel";
            this.menuToolsApplyModel.Size = new System.Drawing.Size(180, 22);
            this.menuToolsApplyModel.Text = "应用当前机型";
            this.menuToolsApplyModel.Click += new System.EventHandler(this.buttonApplyModel_Click);
            //
            // menuToolsDeviceConfig
            //
            this.menuToolsDeviceConfig.Name = "menuToolsDeviceConfig";
            this.menuToolsDeviceConfig.Size = new System.Drawing.Size(180, 22);
            this.menuToolsDeviceConfig.Text = "设备配置…";
            this.menuToolsDeviceConfig.Click += new System.EventHandler(this.buttonDeviceConfig_Click);
            //
            // menuToolsToggleTheme
            //
            this.menuToolsToggleTheme.Name = "menuToolsToggleTheme";
            this.menuToolsToggleTheme.Size = new System.Drawing.Size(180, 22);
            this.menuToolsToggleTheme.Text = "切换主题";
            this.menuToolsToggleTheme.Click += new System.EventHandler(this.buttonToggleTheme_Click);
            //
            // menuHelp
            //
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(61, 25);
            this.menuHelp.Text = "帮助(&H)";
            //
            // menuHelpAbout
            //
            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Size = new System.Drawing.Size(180, 22);
            this.menuHelpAbout.Text = "关于(&A)";
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
            //
            // panelBranding
            //
            this.panelBranding.Controls.Add(this.buttonDeviceConfig);
            this.panelBranding.Controls.Add(this.buttonLogout);
            this.panelBranding.Controls.Add(this.buttonToggleTheme);
            this.panelBranding.Controls.Add(this.labelUserInfo);
            this.panelBranding.Controls.Add(this.lblSubtitle);
            this.panelBranding.Controls.Add(this.lblTitle);
            this.panelBranding.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBranding.Location = new System.Drawing.Point(0, 0);
            this.panelBranding.Name = "panelBranding";
            this.panelBranding.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.panelBranding.Size = new System.Drawing.Size(1184, 64);
            this.panelBranding.TabIndex = 0;
            //
            // buttonDeviceConfig
            //
            this.buttonDeviceConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeviceConfig.Location = new System.Drawing.Point(1094, 10);
            this.buttonDeviceConfig.Name = "buttonDeviceConfig";
            this.buttonDeviceConfig.Size = new System.Drawing.Size(76, 26);
            this.buttonDeviceConfig.TabIndex = 5;
            this.buttonDeviceConfig.Text = "设备配置";
            this.buttonDeviceConfig.UseVisualStyleBackColor = true;
            this.buttonDeviceConfig.Click += new System.EventHandler(this.buttonDeviceConfig_Click);
            //
            // buttonLogout
            //
            this.buttonLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLogout.Location = new System.Drawing.Point(1008, 10);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(80, 26);
            this.buttonLogout.TabIndex = 4;
            this.buttonLogout.Text = "注销重启";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            //
            // buttonToggleTheme
            //
            this.buttonToggleTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonToggleTheme.Location = new System.Drawing.Point(922, 10);
            this.buttonToggleTheme.Name = "buttonToggleTheme";
            this.buttonToggleTheme.Size = new System.Drawing.Size(80, 26);
            this.buttonToggleTheme.TabIndex = 3;
            this.buttonToggleTheme.Text = "切换主题";
            this.buttonToggleTheme.UseVisualStyleBackColor = true;
            this.buttonToggleTheme.Click += new System.EventHandler(this.buttonToggleTheme_Click);
            //
            // labelUserInfo
            //
            this.labelUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUserInfo.AutoEllipsis = true;
            this.labelUserInfo.Location = new System.Drawing.Point(17, 40);
            this.labelUserInfo.Name = "labelUserInfo";
            this.labelUserInfo.Size = new System.Drawing.Size(880, 16);
            this.labelUserInfo.TabIndex = 2;
            this.labelUserInfo.Text = "用户: —";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSubtitle.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblSubtitle.Location = new System.Drawing.Point(17, 26);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(249, 16);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "产线监控 · 数据与工程管理 · 可扩展模板壳层";
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(14, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(284, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "IndustrialControls 上位机程序模板";
            //
            // statusStrip
            //
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelComm,
            this.toolStripStatusLabelAlarms,
            this.toolStripStatusLabelTime});
            this.statusStrip.Location = new System.Drawing.Point(0, 708);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            //
            // toolStripStatusLabelComm
            //
            this.toolStripStatusLabelComm.Name = "toolStripStatusLabelComm";
            this.toolStripStatusLabelComm.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabelComm.Text = "通信: —";
            //
            // toolStripStatusLabelAlarms
            //
            this.toolStripStatusLabelAlarms.Name = "toolStripStatusLabelAlarms";
            this.toolStripStatusLabelAlarms.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabelAlarms.Text = "报警缓冲: 0";
            //
            // toolStripStatusLabelTime
            //
            this.toolStripStatusLabelTime.Name = "toolStripStatusLabelTime";
            this.toolStripStatusLabelTime.Size = new System.Drawing.Size(1115, 17);
            this.toolStripStatusLabelTime.Spring = true;
            this.toolStripStatusLabelTime.Text = "时间";
            this.toolStripStatusLabelTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // timerStatus
            //
            this.timerStatus.Interval = 1000;
            this.timerStatus.Tick += new System.EventHandler(this.timerStatus_Tick);
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 730);
            this.Controls.Add(this.panelShell);
            this.Controls.Add(this.statusStrip);
            this.MainMenuStrip = this.menuStripMain;
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IndustrialControls.Template";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelShell.ResumeLayout(false);
            this.panelShell.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.panelContentHost.ResumeLayout(false);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.panelBranding.ResumeLayout(false);
            this.panelBranding.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
