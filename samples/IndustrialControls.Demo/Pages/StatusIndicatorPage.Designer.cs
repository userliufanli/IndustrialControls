namespace IndustrialControls.Demo.Pages
{
    partial class StatusIndicatorPage
    {
        private System.ComponentModel.IContainer components = null;

        #region 组件设计器生成的代码

        private void InitializeComponent()
        {
            this.descLabel = new System.Windows.Forms.Label();
            this.controlPanel = new System.Windows.Forms.GroupBox();
            this.chkBlink = new System.Windows.Forms.CheckBox();
            this.btnWarning = new System.Windows.Forms.Button();
            this.btnFault = new System.Windows.Forms.Button();
            this.btnStopped = new System.Windows.Forms.Button();
            this.btnRunning = new System.Windows.Forms.Button();
            this.historyLabel = new System.Windows.Forms.Label();
            this.logGroup = new System.Windows.Forms.GroupBox();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.indicator4 = new IndustrialControls.Controls.StatusIndicator.StatusIndicator();
            this.indicator3 = new IndustrialControls.Controls.StatusIndicator.StatusIndicator();
            this.indicator2 = new IndustrialControls.Controls.StatusIndicator.StatusIndicator();
            this.indicator1 = new IndustrialControls.Controls.StatusIndicator.StatusIndicator();
            this.controlPanel.SuspendLayout();
            this.logGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // descLabel
            // 
            this.descLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descLabel.AutoSize = true;
            this.descLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.descLabel.Location = new System.Drawing.Point(20, 20);
            this.descLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(453, 27);
            this.descLabel.TabIndex = 0;
            this.descLabel.Text = "状态指示器演示 - 支持多种状态、形状和闪烁效果";
            // 
            // controlPanel
            // 
            this.controlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlPanel.Controls.Add(this.chkBlink);
            this.controlPanel.Controls.Add(this.btnWarning);
            this.controlPanel.Controls.Add(this.btnFault);
            this.controlPanel.Controls.Add(this.btnStopped);
            this.controlPanel.Controls.Add(this.btnRunning);
            this.controlPanel.Location = new System.Drawing.Point(20, 190);
            this.controlPanel.Margin = new System.Windows.Forms.Padding(4);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Padding = new System.Windows.Forms.Padding(4);
            this.controlPanel.Size = new System.Drawing.Size(650, 140);
            this.controlPanel.TabIndex = 5;
            this.controlPanel.TabStop = false;
            this.controlPanel.Text = "控制面板";
            // 
            // chkBlink
            // 
            this.chkBlink.AutoSize = true;
            this.chkBlink.Location = new System.Drawing.Point(60, 90);
            this.chkBlink.Margin = new System.Windows.Forms.Padding(4);
            this.chkBlink.Name = "chkBlink";
            this.chkBlink.Size = new System.Drawing.Size(106, 22);
            this.chkBlink.TabIndex = 4;
            this.chkBlink.Text = "启用闪烁";
            this.chkBlink.CheckedChanged += new System.EventHandler(this.chkBlink_CheckedChanged);
            // 
            // btnWarning
            // 
            this.btnWarning.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWarning.Location = new System.Drawing.Point(480, 30);
            this.btnWarning.Margin = new System.Windows.Forms.Padding(4);
            this.btnWarning.Name = "btnWarning";
            this.btnWarning.Size = new System.Drawing.Size(120, 40);
            this.btnWarning.TabIndex = 3;
            this.btnWarning.Text = "警告";
            this.btnWarning.Click += new System.EventHandler(this.btnWarning_Click);
            // 
            // btnFault
            // 
            this.btnFault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFault.Location = new System.Drawing.Point(340, 30);
            this.btnFault.Margin = new System.Windows.Forms.Padding(4);
            this.btnFault.Name = "btnFault";
            this.btnFault.Size = new System.Drawing.Size(120, 40);
            this.btnFault.TabIndex = 2;
            this.btnFault.Text = "故障";
            this.btnFault.Click += new System.EventHandler(this.btnFault_Click);
            // 
            // btnStopped
            // 
            this.btnStopped.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopped.Location = new System.Drawing.Point(200, 30);
            this.btnStopped.Margin = new System.Windows.Forms.Padding(4);
            this.btnStopped.Name = "btnStopped";
            this.btnStopped.Size = new System.Drawing.Size(120, 40);
            this.btnStopped.TabIndex = 1;
            this.btnStopped.Text = "停止";
            this.btnStopped.Click += new System.EventHandler(this.btnStopped_Click);
            // 
            // btnRunning
            // 
            this.btnRunning.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunning.Location = new System.Drawing.Point(60, 30);
            this.btnRunning.Margin = new System.Windows.Forms.Padding(4);
            this.btnRunning.Name = "btnRunning";
            this.btnRunning.Size = new System.Drawing.Size(120, 40);
            this.btnRunning.TabIndex = 0;
            this.btnRunning.Text = "运行";
            this.btnRunning.Click += new System.EventHandler(this.btnRunning_Click);
            // 
            // historyLabel
            // 
            this.historyLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.historyLabel.AutoSize = true;
            this.historyLabel.ForeColor = System.Drawing.Color.Gray;
            this.historyLabel.Location = new System.Drawing.Point(20, 345);
            this.historyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.historyLabel.Name = "historyLabel";
            this.historyLabel.Size = new System.Drawing.Size(359, 18);
            this.historyLabel.TabIndex = 6;
            this.historyLabel.Text = "状态变更历史将记录在控件的History属性中";
            // 
            // logGroup
            // 
            this.logGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logGroup.Controls.Add(this.logTextBox);
            this.logGroup.Location = new System.Drawing.Point(20, 370);
            this.logGroup.Margin = new System.Windows.Forms.Padding(4);
            this.logGroup.Name = "logGroup";
            this.logGroup.Padding = new System.Windows.Forms.Padding(4);
            this.logGroup.Size = new System.Drawing.Size(750, 150);
            this.logGroup.TabIndex = 6;
            this.logGroup.TabStop = false;
            this.logGroup.Text = "事件日志";
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Font = new System.Drawing.Font("Consolas", 9F);
            this.logTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.logTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.TabIndex = 0;
            // 
            // indicator4
            // 
            this.indicator4.CustomColor = System.Drawing.Color.Gray;
            this.indicator4.Label = "停止";
            this.indicator4.Location = new System.Drawing.Point(495, 70);
            this.indicator4.Margin = new System.Windows.Forms.Padding(4);
            this.indicator4.Name = "indicator4";
            this.indicator4.Size = new System.Drawing.Size(100, 100);
            this.indicator4.State = IndustrialControls.Controls.StatusIndicator.IndicatorState.Idle;
            this.indicator4.TabIndex = 4;
            // 
            // indicator3
            // 
            this.indicator3.BlinkInterval = 800;
            this.indicator3.CustomColor = System.Drawing.Color.Gray;
            this.indicator3.Label = "警告";
            this.indicator3.Location = new System.Drawing.Point(365, 70);
            this.indicator3.Margin = new System.Windows.Forms.Padding(4);
            this.indicator3.Name = "indicator3";
            this.indicator3.Size = new System.Drawing.Size(100, 100);
            this.indicator3.State = IndustrialControls.Controls.StatusIndicator.IndicatorState.Warning;
            this.indicator3.TabIndex = 3;
            // 
            // indicator2
            // 
            this.indicator2.CustomColor = System.Drawing.Color.Snow;
            this.indicator2.Label = "故障";
            this.indicator2.Location = new System.Drawing.Point(235, 70);
            this.indicator2.Margin = new System.Windows.Forms.Padding(4);
            this.indicator2.Name = "indicator2";
            this.indicator2.Size = new System.Drawing.Size(100, 100);
            this.indicator2.State = IndustrialControls.Controls.StatusIndicator.IndicatorState.Fault;
            this.indicator2.TabIndex = 2;
            // 
            // indicator1
            // 
            this.indicator1.BackColor = System.Drawing.Color.Transparent;
            this.indicator1.BlinkInterval = 100;
            this.indicator1.CustomColor = System.Drawing.Color.Gray;
            this.indicator1.Label = "运行中";
            this.indicator1.Location = new System.Drawing.Point(100, 70);
            this.indicator1.Margin = new System.Windows.Forms.Padding(4);
            this.indicator1.Name = "indicator1";
            this.indicator1.Size = new System.Drawing.Size(100, 100);
            this.indicator1.State = IndustrialControls.Controls.StatusIndicator.IndicatorState.Running;
            this.indicator1.TabIndex = 1;
            // 
            // StatusIndicatorPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.logGroup);
            this.Controls.Add(this.historyLabel);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.indicator4);
            this.Controls.Add(this.indicator3);
            this.Controls.Add(this.indicator2);
            this.Controls.Add(this.indicator1);
            this.Controls.Add(this.descLabel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StatusIndicatorPage";
            this.Size = new System.Drawing.Size(800, 550);
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            this.logGroup.ResumeLayout(false);
            this.logGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label descLabel;
        private IndustrialControls.Controls.StatusIndicator.StatusIndicator indicator1;
        private IndustrialControls.Controls.StatusIndicator.StatusIndicator indicator2;
        private IndustrialControls.Controls.StatusIndicator.StatusIndicator indicator3;
        private IndustrialControls.Controls.StatusIndicator.StatusIndicator indicator4;
        private System.Windows.Forms.GroupBox controlPanel;
        private System.Windows.Forms.Button btnRunning;
        private System.Windows.Forms.Button btnStopped;
        private System.Windows.Forms.Button btnFault;
        private System.Windows.Forms.Button btnWarning;
        private System.Windows.Forms.CheckBox chkBlink;
        private System.Windows.Forms.Label historyLabel;
        private System.Windows.Forms.GroupBox logGroup;
        private System.Windows.Forms.TextBox logTextBox;
    }
}
