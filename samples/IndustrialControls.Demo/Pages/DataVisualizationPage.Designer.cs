namespace IndustrialControls.Demo.Pages
{
    partial class DataVisualizationPage
    {
        private System.ComponentModel.IContainer components = null;

        #region 组件设计器生成的代码

        private void InitializeComponent()
        {
            IndustrialControls.Controls.DataVisualization.ChannelConfig channelConfig1 = new IndustrialControls.Controls.DataVisualization.ChannelConfig();
            this.descLabel = new System.Windows.Forms.Label();
            this.progressGroup = new System.Windows.Forms.GroupBox();
            this.logGroup = new System.Windows.Forms.GroupBox();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnToggleAnimation = new System.Windows.Forms.Button();
            this.btnClearChart = new System.Windows.Forms.Button();
            this.btnTestGauge = new System.Windows.Forms.Button();
            this.btnChangeCornerRadius = new System.Windows.Forms.Button();
            this.progressBar1 = new IndustrialControls.Controls.DataVisualization.IndustrialProgressBar();
            this.progressBar2 = new IndustrialControls.Controls.DataVisualization.IndustrialProgressBar();
            this.progressBar3 = new IndustrialControls.Controls.DataVisualization.IndustrialProgressBar();
            this.gauge2 = new IndustrialControls.Controls.DataVisualization.GaugeControl();
            this.gauge1 = new IndustrialControls.Controls.DataVisualization.GaugeControl();
            this.trendChart = new IndustrialControls.Controls.DataVisualization.TrendChart();
            this.progressGroup.SuspendLayout();
            this.logGroup.SuspendLayout();
            this.btnPanel.SuspendLayout();
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
            this.descLabel.Size = new System.Drawing.Size(303, 20);
            this.descLabel.TabIndex = 0;
            this.descLabel.Text = "实时数据可视化演示 - 趋势图、仪表盘、进度条";
            // 
            // progressGroup
            // 
            this.progressGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressGroup.Controls.Add(this.progressBar1);
            this.progressGroup.Controls.Add(this.progressBar2);
            this.progressGroup.Controls.Add(this.progressBar3);
            this.progressGroup.Location = new System.Drawing.Point(20, 295);
            this.progressGroup.Margin = new System.Windows.Forms.Padding(4);
            this.progressGroup.Name = "progressGroup";
            this.progressGroup.Padding = new System.Windows.Forms.Padding(4);
            this.progressGroup.Size = new System.Drawing.Size(602, 160);
            this.progressGroup.TabIndex = 4;
            this.progressGroup.TabStop = false;
            this.progressGroup.Text = "工业进度条";
            // 
            // logGroup
            // 
            this.logGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logGroup.Controls.Add(this.logTextBox);
            this.logGroup.Location = new System.Drawing.Point(20, 695);
            this.logGroup.Margin = new System.Windows.Forms.Padding(4);
            this.logGroup.Name = "logGroup";
            this.logGroup.Padding = new System.Windows.Forms.Padding(4);
            this.logGroup.Size = new System.Drawing.Size(614, 110);
            this.logGroup.TabIndex = 5;
            this.logGroup.TabStop = false;
            this.logGroup.Text = "事件日志";
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Font = new System.Drawing.Font("Consolas", 9F);
            this.logTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.logTextBox.Location = new System.Drawing.Point(4, 18);
            this.logTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(606, 88);
            this.logTextBox.TabIndex = 0;
            // 
            // btnPanel
            // 
            this.btnPanel.Controls.Add(this.btnToggleAnimation);
            this.btnPanel.Controls.Add(this.btnClearChart);
            this.btnPanel.Controls.Add(this.btnTestGauge);
            this.btnPanel.Controls.Add(this.btnChangeCornerRadius);
            this.btnPanel.Location = new System.Drawing.Point(20, 819);
            this.btnPanel.Margin = new System.Windows.Forms.Padding(4);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(614, 50);
            this.btnPanel.TabIndex = 6;
            // 
            // btnToggleAnimation
            // 
            this.btnToggleAnimation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleAnimation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleAnimation.Location = new System.Drawing.Point(21, 8);
            this.btnToggleAnimation.Margin = new System.Windows.Forms.Padding(4);
            this.btnToggleAnimation.Name = "btnToggleAnimation";
            this.btnToggleAnimation.Size = new System.Drawing.Size(120, 35);
            this.btnToggleAnimation.TabIndex = 0;
            this.btnToggleAnimation.Text = "暂停模拟";
            this.btnToggleAnimation.UseVisualStyleBackColor = true;
            this.btnToggleAnimation.Click += new System.EventHandler(this.btnToggleAnimation_Click);
            // 
            // btnClearChart
            // 
            this.btnClearChart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearChart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearChart.Location = new System.Drawing.Point(151, 8);
            this.btnClearChart.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearChart.Name = "btnClearChart";
            this.btnClearChart.Size = new System.Drawing.Size(120, 35);
            this.btnClearChart.TabIndex = 1;
            this.btnClearChart.Text = "清空图表";
            this.btnClearChart.UseVisualStyleBackColor = true;
            this.btnClearChart.Click += new System.EventHandler(this.btnClearChart_Click);
            // 
            // btnTestGauge
            // 
            this.btnTestGauge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestGauge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestGauge.Location = new System.Drawing.Point(281, 8);
            this.btnTestGauge.Margin = new System.Windows.Forms.Padding(4);
            this.btnTestGauge.Name = "btnTestGauge";
            this.btnTestGauge.Size = new System.Drawing.Size(140, 35);
            this.btnTestGauge.TabIndex = 2;
            this.btnTestGauge.Text = "测试极值";
            this.btnTestGauge.UseVisualStyleBackColor = true;
            this.btnTestGauge.Click += new System.EventHandler(this.btnTestGauge_Click);
            // 
            // btnChangeCornerRadius
            // 
            this.btnChangeCornerRadius.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeCornerRadius.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeCornerRadius.Location = new System.Drawing.Point(431, 8);
            this.btnChangeCornerRadius.Margin = new System.Windows.Forms.Padding(4);
            this.btnChangeCornerRadius.Name = "btnChangeCornerRadius";
            this.btnChangeCornerRadius.Size = new System.Drawing.Size(140, 35);
            this.btnChangeCornerRadius.TabIndex = 3;
            this.btnChangeCornerRadius.Text = "切换圆角半径";
            this.btnChangeCornerRadius.UseVisualStyleBackColor = true;
            this.btnChangeCornerRadius.Click += new System.EventHandler(this.btnChangeCornerRadius_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.DangerThreshold = 10D;
            this.progressBar1.Direction = IndustrialControls.Controls.DataVisualization.ProgressDirection.Vertical;
            this.progressBar1.InverseThreshold = true;
            this.progressBar1.Label = "CPU";
            this.progressBar1.Location = new System.Drawing.Point(549, 13);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.ShowValue = false;
            this.progressBar1.Size = new System.Drawing.Size(26, 139);
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Value = 100D;
            this.progressBar1.WarningThreshold = 50D;
            // 
            // progressBar2
            // 
            this.progressBar2.DangerThreshold = 95D;
            this.progressBar2.ForeColor = System.Drawing.Color.Black;
            this.progressBar2.Label = "内存:";
            this.progressBar2.Location = new System.Drawing.Point(3, 31);
            this.progressBar2.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(506, 30);
            this.progressBar2.TabIndex = 1;
            this.progressBar2.TextColor = System.Drawing.Color.Black;
            this.progressBar2.Value = 94D;
            this.progressBar2.WarningThreshold = 100D;
            // 
            // progressBar3
            // 
            this.progressBar3.BackColor = System.Drawing.Color.Transparent;
            this.progressBar3.DangerThreshold = 95D;
            this.progressBar3.Location = new System.Drawing.Point(34, 89);
            this.progressBar3.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.ShowValue = false;
            this.progressBar3.Size = new System.Drawing.Size(475, 30);
            this.progressBar3.TabIndex = 2;
            this.progressBar3.Value = 78D;
            this.progressBar3.WarningThreshold = 85D;
            // 
            // gauge2
            // 
            this.gauge2.BackColor = System.Drawing.Color.Transparent;
            this.gauge2.DangerThreshold = 2700D;
            this.gauge2.ForeColor = System.Drawing.Color.Black;
            this.gauge2.Location = new System.Drawing.Point(20, 473);
            this.gauge2.Margin = new System.Windows.Forms.Padding(4);
            this.gauge2.Maximum = 3000D;
            this.gauge2.Name = "gauge2";
            this.gauge2.Size = new System.Drawing.Size(304, 213);
            this.gauge2.TabIndex = 3;
            this.gauge2.TextColor = System.Drawing.Color.Black;
            this.gauge2.Title = "转速";
            this.gauge2.Unit = "rpm";
            this.gauge2.Value = 1500D;
            this.gauge2.WarningThreshold = 2200D;
            // 
            // gauge1
            // 
            this.gauge1.BackColor = System.Drawing.Color.Transparent;
            this.gauge1.DangerThreshold = 20D;
            this.gauge1.InverseThreshold = true;
            this.gauge1.Location = new System.Drawing.Point(317, 473);
            this.gauge1.Margin = new System.Windows.Forms.Padding(4);
            this.gauge1.Maximum = 120D;
            this.gauge1.Name = "gauge1";
            this.gauge1.Size = new System.Drawing.Size(304, 213);
            this.gauge1.TabIndex = 2;
            this.gauge1.Title = "温度";
            this.gauge1.Unit = "°C";
            this.gauge1.Value = 45D;
            this.gauge1.WarningThreshold = 50D;
            // 
            // trendChart
            // 
            this.trendChart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trendChart.BackColor = System.Drawing.Color.Transparent;
            this.trendChart.Channels.Add(channelConfig1);
            this.trendChart.CornerRadius = 6;
            this.trendChart.ForeColor = System.Drawing.Color.Black;
            this.trendChart.Location = new System.Drawing.Point(20, 60);
            this.trendChart.Margin = new System.Windows.Forms.Padding(4);
            this.trendChart.Name = "trendChart";
            this.trendChart.Size = new System.Drawing.Size(602, 227);
            this.trendChart.TabIndex = 1;
            this.trendChart.TextColor = System.Drawing.Color.Black;
            this.trendChart.Title = "实时数据趋势";
            this.trendChart.VisiblePoints = 150;
            // 
            // DataVisualizationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnPanel);
            this.Controls.Add(this.logGroup);
            this.Controls.Add(this.progressGroup);
            this.Controls.Add(this.gauge2);
            this.Controls.Add(this.gauge1);
            this.Controls.Add(this.trendChart);
            this.Controls.Add(this.descLabel);
            this.Name = "DataVisualizationPage";
            this.Size = new System.Drawing.Size(643, 883);
            this.progressGroup.ResumeLayout(false);
            this.logGroup.ResumeLayout(false);
            this.logGroup.PerformLayout();
            this.btnPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label descLabel;
        private IndustrialControls.Controls.DataVisualization.TrendChart trendChart;
        private IndustrialControls.Controls.DataVisualization.GaugeControl gauge1;
        private IndustrialControls.Controls.DataVisualization.GaugeControl gauge2;
        private System.Windows.Forms.GroupBox progressGroup;
        private IndustrialControls.Controls.DataVisualization.IndustrialProgressBar progressBar1;
        private IndustrialControls.Controls.DataVisualization.IndustrialProgressBar progressBar2;
        private IndustrialControls.Controls.DataVisualization.IndustrialProgressBar progressBar3;
        private System.Windows.Forms.GroupBox logGroup;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnToggleAnimation;
        private System.Windows.Forms.Button btnClearChart;
        private System.Windows.Forms.Button btnTestGauge;
        private System.Windows.Forms.Button btnChangeCornerRadius;
    }
}
