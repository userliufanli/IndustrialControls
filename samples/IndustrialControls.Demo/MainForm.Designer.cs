namespace IndustrialControls.Demo
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.topPanel = new System.Windows.Forms.Panel();
            this.btnToggleTheme = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabStatus = new System.Windows.Forms.TabPage();
            this.statusIndicatorPage1 = new IndustrialControls.Demo.Pages.StatusIndicatorPage();
            this.tabInput = new System.Windows.Forms.TabPage();
            this.dataInputPage1 = new IndustrialControls.Demo.Pages.DataInputPage();
            this.tabButton = new System.Windows.Forms.TabPage();
            this.deviceButtonPage1 = new IndustrialControls.Demo.Pages.DeviceButtonPage();
            this.tabAlarm = new System.Windows.Forms.TabPage();
            this.alarmDisplayPage1 = new IndustrialControls.Demo.Pages.AlarmDisplayPage();
            this.tabVisualization = new System.Windows.Forms.TabPage();
            this.dataVisualizationPage1 = new IndustrialControls.Demo.Pages.DataVisualizationPage();
            this.tabKeyboard = new System.Windows.Forms.TabPage();
            this.virtualKeyboardPage1 = new IndustrialControls.Demo.Pages.VirtualKeyboardPage();
            this.tabParameters = new System.Windows.Forms.TabPage();
            this.parameterManagerTestPage1 = new IndustrialControls.Demo.Pages.ParameterManagerTestPage();
            this.tabLogin = new System.Windows.Forms.TabPage();
            this.loginDemoPage1 = new IndustrialControls.Demo.Pages.LoginDemoPage();
            this.tabCommunication = new System.Windows.Forms.TabPage();
            this.communicationTestForm1 = new IndustrialControls.Demo.Pages.CommunicationTestForm();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.flowCommunicationTiles = new System.Windows.Forms.FlowLayoutPanel();
            this.communicationControl1 = new IndustrialControls.Controls.Communication.CommunicationControl();
            this.communicationControl2 = new IndustrialControls.Controls.Communication.CommunicationControl();
            this.communicationControl11 = new IndustrialControls.Controls.Communication.CommunicationControl();
            this.communicationControl12 = new IndustrialControls.Controls.Communication.CommunicationControl();
            this.communicationControl10 = new IndustrialControls.Controls.Communication.CommunicationControl();
            this.communicationControl4 = new IndustrialControls.Controls.Communication.CommunicationControl();
            this.communicationControl3 = new IndustrialControls.Controls.Communication.CommunicationControl();
            this.communicationControl5 = new IndustrialControls.Controls.Communication.CommunicationControl();
            this.topPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabStatus.SuspendLayout();
            this.tabInput.SuspendLayout();
            this.tabButton.SuspendLayout();
            this.tabAlarm.SuspendLayout();
            this.tabVisualization.SuspendLayout();
            this.tabKeyboard.SuspendLayout();
            this.tabParameters.SuspendLayout();
            this.tabLogin.SuspendLayout();
            this.tabCommunication.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.flowCommunicationTiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(253)))), ((int)(((byte)(255)))));
            this.topPanel.Controls.Add(this.btnToggleTheme);
            this.topPanel.Controls.Add(this.lblTitle);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.topPanel.Size = new System.Drawing.Size(1024, 54);
            this.topPanel.TabIndex = 0;
            // 
            // btnToggleTheme
            // 
            this.btnToggleTheme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(192)))));
            this.btnToggleTheme.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnToggleTheme.FlatAppearance.BorderSize = 0;
            this.btnToggleTheme.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleTheme.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnToggleTheme.ForeColor = System.Drawing.Color.White;
            this.btnToggleTheme.Location = new System.Drawing.Point(912, 8);
            this.btnToggleTheme.Name = "btnToggleTheme";
            this.btnToggleTheme.Size = new System.Drawing.Size(100, 38);
            this.btnToggleTheme.TabIndex = 1;
            this.btnToggleTheme.Text = "切换主题";
            this.btnToggleTheme.UseVisualStyleBackColor = false;
            this.btnToggleTheme.Click += new System.EventHandler(this.btnToggleTheme_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTitle.Location = new System.Drawing.Point(16, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(324, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "IndustrialControls 工业自动化控件库";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabStatus);
            this.tabControl.Controls.Add(this.tabInput);
            this.tabControl.Controls.Add(this.tabButton);
            this.tabControl.Controls.Add(this.tabAlarm);
            this.tabControl.Controls.Add(this.tabVisualization);
            this.tabControl.Controls.Add(this.tabKeyboard);
            this.tabControl.Controls.Add(this.tabParameters);
            this.tabControl.Controls.Add(this.tabLogin);
            this.tabControl.Controls.Add(this.tabCommunication);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.5F);
            this.tabControl.Location = new System.Drawing.Point(0, 54);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(10, 8);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1024, 714);
            this.tabControl.TabIndex = 1;
            // 
            // tabStatus
            // 
            this.tabStatus.Controls.Add(this.statusIndicatorPage1);
            this.tabStatus.Location = new System.Drawing.Point(4, 38);
            this.tabStatus.Name = "tabStatus";
            this.tabStatus.Size = new System.Drawing.Size(1016, 672);
            this.tabStatus.TabIndex = 0;
            this.tabStatus.Text = "状态指示器";
            this.tabStatus.UseVisualStyleBackColor = true;
            // 
            // statusIndicatorPage1
            // 
            this.statusIndicatorPage1.AutoScroll = true;
            this.statusIndicatorPage1.BackColor = System.Drawing.SystemColors.Control;
            this.statusIndicatorPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusIndicatorPage1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.statusIndicatorPage1.Location = new System.Drawing.Point(0, 0);
            this.statusIndicatorPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.statusIndicatorPage1.Name = "statusIndicatorPage1";
            this.statusIndicatorPage1.Size = new System.Drawing.Size(1016, 672);
            this.statusIndicatorPage1.TabIndex = 0;
            // 
            // tabInput
            // 
            this.tabInput.Controls.Add(this.dataInputPage1);
            this.tabInput.Location = new System.Drawing.Point(4, 38);
            this.tabInput.Name = "tabInput";
            this.tabInput.Size = new System.Drawing.Size(1016, 672);
            this.tabInput.TabIndex = 1;
            this.tabInput.Text = "数据输入";
            this.tabInput.UseVisualStyleBackColor = true;
            // 
            // dataInputPage1
            // 
            this.dataInputPage1.AutoScroll = true;
            this.dataInputPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.dataInputPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataInputPage1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.dataInputPage1.Location = new System.Drawing.Point(0, 0);
            this.dataInputPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataInputPage1.Name = "dataInputPage1";
            this.dataInputPage1.Size = new System.Drawing.Size(1016, 672);
            this.dataInputPage1.TabIndex = 0;
            // 
            // tabButton
            // 
            this.tabButton.Controls.Add(this.deviceButtonPage1);
            this.tabButton.Location = new System.Drawing.Point(4, 38);
            this.tabButton.Name = "tabButton";
            this.tabButton.Size = new System.Drawing.Size(905, 605);
            this.tabButton.TabIndex = 2;
            this.tabButton.Text = "设备按钮";
            this.tabButton.UseVisualStyleBackColor = true;
            // 
            // deviceButtonPage1
            // 
            this.deviceButtonPage1.AutoScroll = true;
            this.deviceButtonPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.deviceButtonPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceButtonPage1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.deviceButtonPage1.Location = new System.Drawing.Point(0, 0);
            this.deviceButtonPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.deviceButtonPage1.Name = "deviceButtonPage1";
            this.deviceButtonPage1.Size = new System.Drawing.Size(905, 605);
            this.deviceButtonPage1.TabIndex = 0;
            // 
            // tabAlarm
            // 
            this.tabAlarm.Controls.Add(this.alarmDisplayPage1);
            this.tabAlarm.Location = new System.Drawing.Point(4, 38);
            this.tabAlarm.Name = "tabAlarm";
            this.tabAlarm.Size = new System.Drawing.Size(905, 605);
            this.tabAlarm.TabIndex = 3;
            this.tabAlarm.Text = "报警显示";
            this.tabAlarm.UseVisualStyleBackColor = true;
            // 
            // alarmDisplayPage1
            // 
            this.alarmDisplayPage1.AutoScroll = true;
            this.alarmDisplayPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.alarmDisplayPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alarmDisplayPage1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.alarmDisplayPage1.Location = new System.Drawing.Point(0, 0);
            this.alarmDisplayPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.alarmDisplayPage1.Name = "alarmDisplayPage1";
            this.alarmDisplayPage1.Size = new System.Drawing.Size(905, 605);
            this.alarmDisplayPage1.TabIndex = 0;
            // 
            // tabVisualization
            // 
            this.tabVisualization.Controls.Add(this.dataVisualizationPage1);
            this.tabVisualization.Location = new System.Drawing.Point(4, 38);
            this.tabVisualization.Name = "tabVisualization";
            this.tabVisualization.Size = new System.Drawing.Size(905, 605);
            this.tabVisualization.TabIndex = 4;
            this.tabVisualization.Text = "数据可视化";
            this.tabVisualization.UseVisualStyleBackColor = true;
            // 
            // dataVisualizationPage1
            // 
            this.dataVisualizationPage1.AutoScroll = true;
            this.dataVisualizationPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.dataVisualizationPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataVisualizationPage1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.dataVisualizationPage1.Location = new System.Drawing.Point(0, 0);
            this.dataVisualizationPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataVisualizationPage1.Name = "dataVisualizationPage1";
            this.dataVisualizationPage1.Size = new System.Drawing.Size(905, 605);
            this.dataVisualizationPage1.TabIndex = 0;
            // 
            // tabKeyboard
            // 
            this.tabKeyboard.Controls.Add(this.virtualKeyboardPage1);
            this.tabKeyboard.Location = new System.Drawing.Point(4, 38);
            this.tabKeyboard.Name = "tabKeyboard";
            this.tabKeyboard.Size = new System.Drawing.Size(1016, 672);
            this.tabKeyboard.TabIndex = 5;
            this.tabKeyboard.Text = "虚拟键盘";
            this.tabKeyboard.UseVisualStyleBackColor = true;
            // 
            // virtualKeyboardPage1
            // 
            this.virtualKeyboardPage1.AutoScroll = true;
            this.virtualKeyboardPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.virtualKeyboardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.virtualKeyboardPage1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.virtualKeyboardPage1.Location = new System.Drawing.Point(0, 0);
            this.virtualKeyboardPage1.Margin = new System.Windows.Forms.Padding(2);
            this.virtualKeyboardPage1.Name = "virtualKeyboardPage1";
            this.virtualKeyboardPage1.Size = new System.Drawing.Size(1016, 672);
            this.virtualKeyboardPage1.TabIndex = 0;
            // 
            // tabParameters
            // 
            this.tabParameters.Controls.Add(this.parameterManagerTestPage1);
            this.tabParameters.Location = new System.Drawing.Point(4, 38);
            this.tabParameters.Name = "tabParameters";
            this.tabParameters.Size = new System.Drawing.Size(1016, 672);
            this.tabParameters.TabIndex = 6;
            this.tabParameters.Text = "参数管理";
            this.tabParameters.UseVisualStyleBackColor = true;
            // 
            // parameterManagerTestPage1
            // 
            this.parameterManagerTestPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.parameterManagerTestPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parameterManagerTestPage1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.parameterManagerTestPage1.Location = new System.Drawing.Point(0, 0);
            this.parameterManagerTestPage1.Margin = new System.Windows.Forms.Padding(2);
            this.parameterManagerTestPage1.Name = "parameterManagerTestPage1";
            this.parameterManagerTestPage1.Size = new System.Drawing.Size(1016, 672);
            this.parameterManagerTestPage1.TabIndex = 0;
            // 
            // tabLogin
            // 
            this.tabLogin.Controls.Add(this.loginDemoPage1);
            this.tabLogin.Location = new System.Drawing.Point(4, 38);
            this.tabLogin.Name = "tabLogin";
            this.tabLogin.Size = new System.Drawing.Size(1016, 672);
            this.tabLogin.TabIndex = 7;
            this.tabLogin.Text = "登录";
            this.tabLogin.UseVisualStyleBackColor = true;
            // 
            // loginDemoPage1
            // 
            this.loginDemoPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginDemoPage1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.loginDemoPage1.Location = new System.Drawing.Point(0, 0);
            this.loginDemoPage1.Margin = new System.Windows.Forms.Padding(2);
            this.loginDemoPage1.Name = "loginDemoPage1";
            this.loginDemoPage1.Size = new System.Drawing.Size(1016, 672);
            this.loginDemoPage1.TabIndex = 0;
            // 
            // tabCommunication
            // 
            this.tabCommunication.Controls.Add(this.communicationTestForm1);
            this.tabCommunication.Location = new System.Drawing.Point(4, 38);
            this.tabCommunication.Name = "tabCommunication";
            this.tabCommunication.Size = new System.Drawing.Size(1016, 672);
            this.tabCommunication.TabIndex = 8;
            this.tabCommunication.Text = "通讯测试";
            this.tabCommunication.UseVisualStyleBackColor = true;
            // 
            // communicationTestForm1
            // 
            this.communicationTestForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.communicationTestForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.communicationTestForm1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.communicationTestForm1.Location = new System.Drawing.Point(0, 0);
            this.communicationTestForm1.Margin = new System.Windows.Forms.Padding(0);
            this.communicationTestForm1.Name = "communicationTestForm1";
            this.communicationTestForm1.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.communicationTestForm1.Size = new System.Drawing.Size(1016, 672);
            this.communicationTestForm1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.flowCommunicationTiles);
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1016, 672);
            this.tabPage1.TabIndex = 9;
            this.tabPage1.Text = "通讯控件阵列";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // flowCommunicationTiles
            // 
            this.flowCommunicationTiles.AutoScroll = true;
            this.flowCommunicationTiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.flowCommunicationTiles.Controls.Add(this.communicationControl1);
            this.flowCommunicationTiles.Controls.Add(this.communicationControl2);
            this.flowCommunicationTiles.Controls.Add(this.communicationControl11);
            this.flowCommunicationTiles.Controls.Add(this.communicationControl12);
            this.flowCommunicationTiles.Controls.Add(this.communicationControl10);
            this.flowCommunicationTiles.Controls.Add(this.communicationControl4);
            this.flowCommunicationTiles.Controls.Add(this.communicationControl3);
            this.flowCommunicationTiles.Controls.Add(this.communicationControl5);
            this.flowCommunicationTiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowCommunicationTiles.Location = new System.Drawing.Point(0, 0);
            this.flowCommunicationTiles.Name = "flowCommunicationTiles";
            this.flowCommunicationTiles.Padding = new System.Windows.Forms.Padding(18);
            this.flowCommunicationTiles.Size = new System.Drawing.Size(1016, 672);
            this.flowCommunicationTiles.TabIndex = 0;
            // 
            // communicationControl1
            // 
            this.communicationControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.communicationControl1.DataEncoding = ((System.Text.Encoding)(resources.GetObject("communicationControl1.DataEncoding")));
            this.communicationControl1.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.communicationControl1.FrameDelimiter = new byte[] {
        ((byte)(13)),
        ((byte)(10))};
            this.communicationControl1.Location = new System.Drawing.Point(26, 26);
            this.communicationControl1.Margin = new System.Windows.Forms.Padding(8);
            this.communicationControl1.MaximumSize = new System.Drawing.Size(200, 196);
            this.communicationControl1.MinimumSize = new System.Drawing.Size(200, 196);
            this.communicationControl1.Name = "communicationControl1";
            this.communicationControl1.SerialPortName = "COM3";
            this.communicationControl1.Size = new System.Drawing.Size(200, 196);
            this.communicationControl1.TabIndex = 0;
            // 
            // communicationControl2
            // 
            this.communicationControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.communicationControl2.DataEncoding = ((System.Text.Encoding)(resources.GetObject("communicationControl2.DataEncoding")));
            this.communicationControl2.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.communicationControl2.FrameDelimiter = new byte[] {
        ((byte)(13)),
        ((byte)(10))};
            this.communicationControl2.Location = new System.Drawing.Point(242, 26);
            this.communicationControl2.Margin = new System.Windows.Forms.Padding(8);
            this.communicationControl2.MaximumSize = new System.Drawing.Size(200, 196);
            this.communicationControl2.MinimumSize = new System.Drawing.Size(200, 196);
            this.communicationControl2.Name = "communicationControl2";
            this.communicationControl2.SerialPortName = "COM3";
            this.communicationControl2.Size = new System.Drawing.Size(200, 196);
            this.communicationControl2.TabIndex = 1;
            // 
            // communicationControl11
            // 
            this.communicationControl11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.communicationControl11.DataEncoding = ((System.Text.Encoding)(resources.GetObject("communicationControl11.DataEncoding")));
            this.communicationControl11.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.communicationControl11.FrameDelimiter = new byte[] {
        ((byte)(13)),
        ((byte)(10))};
            this.communicationControl11.Location = new System.Drawing.Point(458, 26);
            this.communicationControl11.Margin = new System.Windows.Forms.Padding(8);
            this.communicationControl11.MaximumSize = new System.Drawing.Size(200, 196);
            this.communicationControl11.MinimumSize = new System.Drawing.Size(200, 196);
            this.communicationControl11.Name = "communicationControl11";
            this.communicationControl11.SerialPortName = "COM3";
            this.communicationControl11.Size = new System.Drawing.Size(200, 196);
            this.communicationControl11.TabIndex = 10;
            // 
            // communicationControl12
            // 
            this.communicationControl12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.communicationControl12.DataEncoding = ((System.Text.Encoding)(resources.GetObject("communicationControl12.DataEncoding")));
            this.communicationControl12.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.communicationControl12.FrameDelimiter = new byte[] {
        ((byte)(13)),
        ((byte)(10))};
            this.communicationControl12.Location = new System.Drawing.Point(674, 26);
            this.communicationControl12.Margin = new System.Windows.Forms.Padding(8);
            this.communicationControl12.MaximumSize = new System.Drawing.Size(200, 196);
            this.communicationControl12.MinimumSize = new System.Drawing.Size(200, 196);
            this.communicationControl12.Name = "communicationControl12";
            this.communicationControl12.SerialPortName = "COM3";
            this.communicationControl12.Size = new System.Drawing.Size(200, 196);
            this.communicationControl12.TabIndex = 11;
            // 
            // communicationControl10
            // 
            this.communicationControl10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.communicationControl10.DataEncoding = ((System.Text.Encoding)(resources.GetObject("communicationControl10.DataEncoding")));
            this.communicationControl10.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.communicationControl10.FrameDelimiter = new byte[] {
        ((byte)(13)),
        ((byte)(10))};
            this.communicationControl10.Location = new System.Drawing.Point(26, 238);
            this.communicationControl10.Margin = new System.Windows.Forms.Padding(8);
            this.communicationControl10.MaximumSize = new System.Drawing.Size(200, 196);
            this.communicationControl10.MinimumSize = new System.Drawing.Size(200, 196);
            this.communicationControl10.Name = "communicationControl10";
            this.communicationControl10.SerialPortName = "COM3";
            this.communicationControl10.Size = new System.Drawing.Size(200, 196);
            this.communicationControl10.TabIndex = 9;
            // 
            // communicationControl4
            // 
            this.communicationControl4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.communicationControl4.DataEncoding = ((System.Text.Encoding)(resources.GetObject("communicationControl4.DataEncoding")));
            this.communicationControl4.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.communicationControl4.FrameDelimiter = new byte[] {
        ((byte)(13)),
        ((byte)(10))};
            this.communicationControl4.Location = new System.Drawing.Point(242, 238);
            this.communicationControl4.Margin = new System.Windows.Forms.Padding(8);
            this.communicationControl4.MaximumSize = new System.Drawing.Size(200, 196);
            this.communicationControl4.MinimumSize = new System.Drawing.Size(200, 196);
            this.communicationControl4.Name = "communicationControl4";
            this.communicationControl4.SerialPortName = "COM3";
            this.communicationControl4.Size = new System.Drawing.Size(200, 196);
            this.communicationControl4.TabIndex = 3;
            // 
            // communicationControl3
            // 
            this.communicationControl3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.communicationControl3.DataEncoding = ((System.Text.Encoding)(resources.GetObject("communicationControl3.DataEncoding")));
            this.communicationControl3.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.communicationControl3.FrameDelimiter = new byte[] {
        ((byte)(13)),
        ((byte)(10))};
            this.communicationControl3.Location = new System.Drawing.Point(458, 238);
            this.communicationControl3.Margin = new System.Windows.Forms.Padding(8);
            this.communicationControl3.MaximumSize = new System.Drawing.Size(200, 196);
            this.communicationControl3.MinimumSize = new System.Drawing.Size(200, 196);
            this.communicationControl3.Name = "communicationControl3";
            this.communicationControl3.SerialPortName = "COM3";
            this.communicationControl3.Size = new System.Drawing.Size(200, 196);
            this.communicationControl3.TabIndex = 2;
            // 
            // communicationControl5
            // 
            this.communicationControl5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.communicationControl5.DataEncoding = ((System.Text.Encoding)(resources.GetObject("communicationControl5.DataEncoding")));
            this.communicationControl5.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.communicationControl5.FrameDelimiter = new byte[] {
        ((byte)(13)),
        ((byte)(10))};
            this.communicationControl5.Location = new System.Drawing.Point(674, 238);
            this.communicationControl5.Margin = new System.Windows.Forms.Padding(8);
            this.communicationControl5.MaximumSize = new System.Drawing.Size(200, 196);
            this.communicationControl5.MinimumSize = new System.Drawing.Size(200, 196);
            this.communicationControl5.Name = "communicationControl5";
            this.communicationControl5.SerialPortName = "COM3";
            this.communicationControl5.Size = new System.Drawing.Size(200, 196);
            this.communicationControl5.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.topPanel);
            this.MinimumSize = new System.Drawing.Size(880, 600);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工业控件库演示 - IndustrialControls Demo";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabStatus.ResumeLayout(false);
            this.tabInput.ResumeLayout(false);
            this.tabButton.ResumeLayout(false);
            this.tabAlarm.ResumeLayout(false);
            this.tabVisualization.ResumeLayout(false);
            this.tabKeyboard.ResumeLayout(false);
            this.tabParameters.ResumeLayout(false);
            this.tabLogin.ResumeLayout(false);
            this.tabCommunication.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.flowCommunicationTiles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnToggleTheme;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabStatus;
        private System.Windows.Forms.TabPage tabInput;
        private System.Windows.Forms.TabPage tabButton;
        private System.Windows.Forms.TabPage tabAlarm;
        private System.Windows.Forms.TabPage tabVisualization;
        private System.Windows.Forms.TabPage tabKeyboard;
        private System.Windows.Forms.TabPage tabParameters;
        private System.Windows.Forms.TabPage tabLogin;
        private System.Windows.Forms.TabPage tabCommunication;
        private IndustrialControls.Demo.Pages.StatusIndicatorPage statusIndicatorPage1;
        private IndustrialControls.Demo.Pages.DataInputPage dataInputPage1;
        private IndustrialControls.Demo.Pages.DeviceButtonPage deviceButtonPage1;
        private IndustrialControls.Demo.Pages.AlarmDisplayPage alarmDisplayPage1;
        private IndustrialControls.Demo.Pages.DataVisualizationPage dataVisualizationPage1;
        private IndustrialControls.Demo.Pages.VirtualKeyboardPage virtualKeyboardPage1;
        private IndustrialControls.Demo.Pages.ParameterManagerTestPage parameterManagerTestPage1;
        private IndustrialControls.Demo.Pages.LoginDemoPage loginDemoPage1;
        private IndustrialControls.Demo.Pages.CommunicationTestForm communicationTestForm1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.FlowLayoutPanel flowCommunicationTiles;
        private Controls.Communication.CommunicationControl communicationControl12;
        private Controls.Communication.CommunicationControl communicationControl11;
        private Controls.Communication.CommunicationControl communicationControl10;
        private Controls.Communication.CommunicationControl communicationControl5;
        private Controls.Communication.CommunicationControl communicationControl4;
        private Controls.Communication.CommunicationControl communicationControl3;
        private Controls.Communication.CommunicationControl communicationControl2;
        private Controls.Communication.CommunicationControl communicationControl1;
    }
}
