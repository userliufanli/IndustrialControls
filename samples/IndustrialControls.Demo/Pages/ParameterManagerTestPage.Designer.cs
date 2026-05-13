namespace IndustrialControls.Demo.Pages
{
    partial class ParameterManagerTestPage
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabParameters;
        private System.Windows.Forms.GroupBox grpDirectApp;
        private System.Windows.Forms.Label lblDirectLanguage;
        private System.Windows.Forms.TextBox txtDirectLanguage;
        private System.Windows.Forms.Label lblDirectTheme;
        private System.Windows.Forms.ComboBox cmbDirectTheme;
        private System.Windows.Forms.Label lblDirectAutoSave;
        private System.Windows.Forms.CheckBox chkDirectAutoSave;
        private System.Windows.Forms.GroupBox grpDirectUI;
        private System.Windows.Forms.Label lblDirectWidth;
        private System.Windows.Forms.NumericUpDown numDirectWidth;
        private System.Windows.Forms.Label lblDirectHeight;
        private System.Windows.Forms.NumericUpDown numDirectHeight;
        private System.Windows.Forms.GroupBox grpEvents;
        private System.Windows.Forms.TextBox txtEventLog;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnOpenConfig;
        private System.Windows.Forms.Panel footerPanel;

        /// <summary>
        /// 清理正在使用的资源。
        /// </summary>
        /// <param name="disposing">若为 true，则释放托管资源与非托管资源；否则仅释放非托管资源。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 — 请勿使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabParameters = new System.Windows.Forms.TabPage();
            this.grpDirectApp = new System.Windows.Forms.GroupBox();
            this.lblDirectLanguage = new System.Windows.Forms.Label();
            this.txtDirectLanguage = new System.Windows.Forms.TextBox();
            this.lblDirectTheme = new System.Windows.Forms.Label();
            this.cmbDirectTheme = new System.Windows.Forms.ComboBox();
            this.lblDirectAutoSave = new System.Windows.Forms.Label();
            this.chkDirectAutoSave = new System.Windows.Forms.CheckBox();
            this.grpDirectUI = new System.Windows.Forms.GroupBox();
            this.lblDirectWidth = new System.Windows.Forms.Label();
            this.numDirectWidth = new System.Windows.Forms.NumericUpDown();
            this.lblDirectHeight = new System.Windows.Forms.Label();
            this.numDirectHeight = new System.Windows.Forms.NumericUpDown();
            this.grpEvents = new System.Windows.Forms.GroupBox();
            this.txtEventLog = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnOpenConfig = new System.Windows.Forms.Button();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.tabControl.SuspendLayout();
            this.tabParameters.SuspendLayout();
            this.grpDirectApp.SuspendLayout();
            this.grpDirectUI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDirectWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDirectHeight)).BeginInit();
            this.grpEvents.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabParameters);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(8, 6);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(920, 220);
            this.tabControl.TabIndex = 0;
            // 
            // tabParameters
            // 
            this.tabParameters.Controls.Add(this.grpDirectApp);
            this.tabParameters.Controls.Add(this.grpDirectUI);
            this.tabParameters.Location = new System.Drawing.Point(4, 32);
            this.tabParameters.Margin = new System.Windows.Forms.Padding(0);
            this.tabParameters.Name = "tabParameters";
            this.tabParameters.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.tabParameters.Size = new System.Drawing.Size(912, 184);
            this.tabParameters.TabIndex = 0;
            this.tabParameters.Text = "AppParameters";
            this.tabParameters.UseVisualStyleBackColor = true;
            // 
            // grpDirectApp
            // 
            this.grpDirectApp.Controls.Add(this.lblDirectLanguage);
            this.grpDirectApp.Controls.Add(this.txtDirectLanguage);
            this.grpDirectApp.Controls.Add(this.lblDirectTheme);
            this.grpDirectApp.Controls.Add(this.cmbDirectTheme);
            this.grpDirectApp.Controls.Add(this.lblDirectAutoSave);
            this.grpDirectApp.Controls.Add(this.chkDirectAutoSave);
            this.grpDirectApp.Location = new System.Drawing.Point(12, 12);
            this.grpDirectApp.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.grpDirectApp.Name = "grpDirectApp";
            this.grpDirectApp.Padding = new System.Windows.Forms.Padding(10, 8, 10, 10);
            this.grpDirectApp.Size = new System.Drawing.Size(400, 110);
            this.grpDirectApp.TabIndex = 0;
            this.grpDirectApp.TabStop = false;
            this.grpDirectApp.Text = "应用程序参数 (AppParameters.App)";
            // 
            // lblDirectLanguage
            // 
            this.lblDirectLanguage.AutoSize = true;
            this.lblDirectLanguage.Location = new System.Drawing.Point(13, 22);
            this.lblDirectLanguage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDirectLanguage.Name = "lblDirectLanguage";
            this.lblDirectLanguage.Size = new System.Drawing.Size(35, 17);
            this.lblDirectLanguage.TabIndex = 0;
            this.lblDirectLanguage.Text = "语言:";
            // 
            // txtDirectLanguage
            // 
            this.txtDirectLanguage.Location = new System.Drawing.Point(80, 19);
            this.txtDirectLanguage.Margin = new System.Windows.Forms.Padding(2);
            this.txtDirectLanguage.Name = "txtDirectLanguage";
            this.txtDirectLanguage.Size = new System.Drawing.Size(168, 23);
            this.txtDirectLanguage.TabIndex = 1;
            this.txtDirectLanguage.Leave += new System.EventHandler(this.txtDirectLanguage_Leave);
            // 
            // lblDirectTheme
            // 
            this.lblDirectTheme.AutoSize = true;
            this.lblDirectTheme.Location = new System.Drawing.Point(13, 45);
            this.lblDirectTheme.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDirectTheme.Name = "lblDirectTheme";
            this.lblDirectTheme.Size = new System.Drawing.Size(35, 17);
            this.lblDirectTheme.TabIndex = 2;
            this.lblDirectTheme.Text = "主题:";
            // 
            // cmbDirectTheme
            // 
            this.cmbDirectTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDirectTheme.FormattingEnabled = true;
            this.cmbDirectTheme.Items.AddRange(new object[] {
            "浅色",
            "深色",
            "蓝色"});
            this.cmbDirectTheme.Location = new System.Drawing.Point(80, 43);
            this.cmbDirectTheme.Margin = new System.Windows.Forms.Padding(2);
            this.cmbDirectTheme.Name = "cmbDirectTheme";
            this.cmbDirectTheme.Size = new System.Drawing.Size(168, 25);
            this.cmbDirectTheme.TabIndex = 3;
            this.cmbDirectTheme.SelectedIndexChanged += new System.EventHandler(this.cmbDirectTheme_SelectedIndexChanged);
            // 
            // lblDirectAutoSave
            // 
            this.lblDirectAutoSave.AutoSize = true;
            this.lblDirectAutoSave.Location = new System.Drawing.Point(13, 69);
            this.lblDirectAutoSave.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDirectAutoSave.Name = "lblDirectAutoSave";
            this.lblDirectAutoSave.Size = new System.Drawing.Size(59, 17);
            this.lblDirectAutoSave.TabIndex = 4;
            this.lblDirectAutoSave.Text = "自动保存:";
            // 
            // chkDirectAutoSave
            // 
            this.chkDirectAutoSave.AutoSize = true;
            this.chkDirectAutoSave.Location = new System.Drawing.Point(80, 72);
            this.chkDirectAutoSave.Margin = new System.Windows.Forms.Padding(2);
            this.chkDirectAutoSave.Name = "chkDirectAutoSave";
            this.chkDirectAutoSave.Size = new System.Drawing.Size(15, 14);
            this.chkDirectAutoSave.TabIndex = 5;
            this.chkDirectAutoSave.UseVisualStyleBackColor = true;
            this.chkDirectAutoSave.CheckedChanged += new System.EventHandler(this.chkDirectAutoSave_CheckedChanged);
            // 
            // grpDirectUI
            // 
            this.grpDirectUI.Controls.Add(this.lblDirectWidth);
            this.grpDirectUI.Controls.Add(this.numDirectWidth);
            this.grpDirectUI.Controls.Add(this.lblDirectHeight);
            this.grpDirectUI.Controls.Add(this.numDirectHeight);
            this.grpDirectUI.Location = new System.Drawing.Point(430, 12);
            this.grpDirectUI.Margin = new System.Windows.Forms.Padding(0);
            this.grpDirectUI.Name = "grpDirectUI";
            this.grpDirectUI.Padding = new System.Windows.Forms.Padding(10, 8, 10, 10);
            this.grpDirectUI.Size = new System.Drawing.Size(460, 110);
            this.grpDirectUI.TabIndex = 1;
            this.grpDirectUI.TabStop = false;
            this.grpDirectUI.Text = "界面参数 (AppParameters.UI)";
            // 
            // lblDirectWidth
            // 
            this.lblDirectWidth.AutoSize = true;
            this.lblDirectWidth.Location = new System.Drawing.Point(13, 22);
            this.lblDirectWidth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDirectWidth.Name = "lblDirectWidth";
            this.lblDirectWidth.Size = new System.Drawing.Size(59, 17);
            this.lblDirectWidth.TabIndex = 0;
            this.lblDirectWidth.Text = "窗口宽度:";
            // 
            // numDirectWidth
            // 
            this.numDirectWidth.Location = new System.Drawing.Point(100, 19);
            this.numDirectWidth.Margin = new System.Windows.Forms.Padding(2);
            this.numDirectWidth.Maximum = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            this.numDirectWidth.Minimum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numDirectWidth.Name = "numDirectWidth";
            this.numDirectWidth.Size = new System.Drawing.Size(187, 23);
            this.numDirectWidth.TabIndex = 1;
            this.numDirectWidth.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.numDirectWidth.ValueChanged += new System.EventHandler(this.numDirectWidth_ValueChanged);
            // 
            // lblDirectHeight
            // 
            this.lblDirectHeight.AutoSize = true;
            this.lblDirectHeight.Location = new System.Drawing.Point(13, 45);
            this.lblDirectHeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDirectHeight.Name = "lblDirectHeight";
            this.lblDirectHeight.Size = new System.Drawing.Size(59, 17);
            this.lblDirectHeight.TabIndex = 2;
            this.lblDirectHeight.Text = "窗口高度:";
            // 
            // numDirectHeight
            // 
            this.numDirectHeight.Location = new System.Drawing.Point(100, 43);
            this.numDirectHeight.Margin = new System.Windows.Forms.Padding(2);
            this.numDirectHeight.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.numDirectHeight.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numDirectHeight.Name = "numDirectHeight";
            this.numDirectHeight.Size = new System.Drawing.Size(187, 23);
            this.numDirectHeight.TabIndex = 3;
            this.numDirectHeight.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numDirectHeight.ValueChanged += new System.EventHandler(this.numDirectHeight_ValueChanged);
            // 
            // grpEvents
            // 
            this.grpEvents.Controls.Add(this.txtEventLog);
            this.grpEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEvents.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.grpEvents.Location = new System.Drawing.Point(0, 220);
            this.grpEvents.Margin = new System.Windows.Forms.Padding(0);
            this.grpEvents.Name = "grpEvents";
            this.grpEvents.Padding = new System.Windows.Forms.Padding(10, 8, 10, 10);
            this.grpEvents.Size = new System.Drawing.Size(920, 404);
            this.grpEvents.TabIndex = 1;
            this.grpEvents.TabStop = false;
            this.grpEvents.Text = "事件日志 (实时监听参数变更)";
            // 
            // txtEventLog
            // 
            this.txtEventLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtEventLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEventLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEventLog.ForeColor = System.Drawing.Color.LightGreen;
            this.txtEventLog.Location = new System.Drawing.Point(10, 24);
            this.txtEventLog.Margin = new System.Windows.Forms.Padding(0);
            this.txtEventLog.Multiline = true;
            this.txtEventLog.Name = "txtEventLog";
            this.txtEventLog.ReadOnly = true;
            this.txtEventLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEventLog.Size = new System.Drawing.Size(900, 370);
            this.txtEventLog.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(108)))), ((int)(((byte)(120)))));
            this.lblStatus.Location = new System.Drawing.Point(16, 6);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblStatus.Size = new System.Drawing.Size(888, 22);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "提示: 通过 IndustrialControls.Demo.AppParameters 读写默认与通讯配置";
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearLog.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnClearLog.Location = new System.Drawing.Point(16, 30);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(100, 30);
            this.btnClearLog.TabIndex = 1;
            this.btnClearLog.Text = "清空日志";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnOpenConfig
            // 
            this.btnOpenConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenConfig.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnOpenConfig.Location = new System.Drawing.Point(128, 30);
            this.btnOpenConfig.Name = "btnOpenConfig";
            this.btnOpenConfig.Size = new System.Drawing.Size(140, 30);
            this.btnOpenConfig.TabIndex = 2;
            this.btnOpenConfig.Text = "打开配置文件目录";
            this.btnOpenConfig.UseVisualStyleBackColor = true;
            this.btnOpenConfig.Click += new System.EventHandler(this.btnOpenConfig_Click);
            // 
            // footerPanel
            // 
            this.footerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(253)))), ((int)(((byte)(255)))));
            this.footerPanel.Controls.Add(this.btnOpenConfig);
            this.footerPanel.Controls.Add(this.btnClearLog);
            this.footerPanel.Controls.Add(this.lblStatus);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 624);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Padding = new System.Windows.Forms.Padding(16, 6, 16, 10);
            this.footerPanel.Size = new System.Drawing.Size(920, 72);
            this.footerPanel.TabIndex = 2;
            // 
            // ParameterManagerTestPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.grpEvents);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.footerPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ParameterManagerTestPage";
            this.Size = new System.Drawing.Size(920, 696);
            this.tabControl.ResumeLayout(false);
            this.tabParameters.ResumeLayout(false);
            this.grpDirectApp.ResumeLayout(false);
            this.grpDirectApp.PerformLayout();
            this.grpDirectUI.ResumeLayout(false);
            this.grpDirectUI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDirectWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDirectHeight)).EndInit();
            this.grpEvents.ResumeLayout(false);
            this.grpEvents.PerformLayout();
            this.footerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
