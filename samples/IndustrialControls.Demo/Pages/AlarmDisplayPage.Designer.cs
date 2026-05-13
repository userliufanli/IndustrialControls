namespace IndustrialControls.Demo.Pages
{
    partial class AlarmDisplayPage
    {
        private System.ComponentModel.IContainer components = null;

        #region 组件设计器生成的代码

        private void InitializeComponent()
        {
            this.descLabel = new System.Windows.Forms.Label();
            this.alarmDisplay = new IndustrialControls.Controls.Alarm.AlarmDisplay();
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnAddEmergency = new System.Windows.Forms.Button();
            this.btnAddImportant = new System.Windows.Forms.Button();
            this.btnAddGeneral = new System.Windows.Forms.Button();
            this.btnAckAll = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnFilterLastHour = new System.Windows.Forms.Button();
            this.btnFilterLast5Min = new System.Windows.Forms.Button();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.tipLabel = new System.Windows.Forms.Label();
            this.logGroup = new System.Windows.Forms.GroupBox();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.btnPanel.SuspendLayout();
            this.logGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // descLabel
            // 
            this.descLabel.AutoSize = true;
            this.descLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.descLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descLabel.Location = new System.Drawing.Point(20, 20);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(303, 20);
            this.descLabel.TabIndex = 0;
            this.descLabel.Text = "报警显示演示 - 分级报警、自动排序、确认清除";
            // 
            // alarmDisplay
            // 
            this.alarmDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.alarmDisplay.Location = new System.Drawing.Point(20, 48);
            this.alarmDisplay.Name = "alarmDisplay";
            this.alarmDisplay.Size = new System.Drawing.Size(602, 213);
            this.alarmDisplay.TabIndex = 1;
            // 
            // btnPanel
            // 
            this.btnPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPanel.Controls.Add(this.btnAddEmergency);
            this.btnPanel.Controls.Add(this.btnAddImportant);
            this.btnPanel.Controls.Add(this.btnAddGeneral);
            this.btnPanel.Controls.Add(this.btnAckAll);
            this.btnPanel.Controls.Add(this.btnClear);
            this.btnPanel.Controls.Add(this.btnClearAll);
            this.btnPanel.Controls.Add(this.btnFilterLastHour);
            this.btnPanel.Controls.Add(this.btnFilterLast5Min);
            this.btnPanel.Controls.Add(this.btnClearFilter);
            this.btnPanel.Location = new System.Drawing.Point(20, 271);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(602, 71);
            this.btnPanel.TabIndex = 2;
            // 
            // btnAddEmergency
            // 
            this.btnAddEmergency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnAddEmergency.FlatAppearance.BorderSize = 0;
            this.btnAddEmergency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddEmergency.ForeColor = System.Drawing.Color.White;
            this.btnAddEmergency.Location = new System.Drawing.Point(0, 7);
            this.btnAddEmergency.Name = "btnAddEmergency";
            this.btnAddEmergency.Size = new System.Drawing.Size(100, 27);
            this.btnAddEmergency.TabIndex = 0;
            this.btnAddEmergency.Text = "添加紧急报警";
            this.btnAddEmergency.UseVisualStyleBackColor = false;
            this.btnAddEmergency.Click += new System.EventHandler(this.btnAddEmergency_Click);
            // 
            // btnAddImportant
            // 
            this.btnAddImportant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnAddImportant.FlatAppearance.BorderSize = 0;
            this.btnAddImportant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddImportant.ForeColor = System.Drawing.Color.White;
            this.btnAddImportant.Location = new System.Drawing.Point(107, 7);
            this.btnAddImportant.Name = "btnAddImportant";
            this.btnAddImportant.Size = new System.Drawing.Size(100, 27);
            this.btnAddImportant.TabIndex = 1;
            this.btnAddImportant.Text = "添加重要报警";
            this.btnAddImportant.UseVisualStyleBackColor = false;
            this.btnAddImportant.Click += new System.EventHandler(this.btnAddImportant_Click);
            // 
            // btnAddGeneral
            // 
            this.btnAddGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(138)))), ((int)(((byte)(4)))));
            this.btnAddGeneral.FlatAppearance.BorderSize = 0;
            this.btnAddGeneral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddGeneral.ForeColor = System.Drawing.Color.White;
            this.btnAddGeneral.Location = new System.Drawing.Point(213, 7);
            this.btnAddGeneral.Name = "btnAddGeneral";
            this.btnAddGeneral.Size = new System.Drawing.Size(100, 27);
            this.btnAddGeneral.TabIndex = 2;
            this.btnAddGeneral.Text = "添加一般报警";
            this.btnAddGeneral.UseVisualStyleBackColor = false;
            this.btnAddGeneral.Click += new System.EventHandler(this.btnAddGeneral_Click);
            // 
            // btnAckAll
            // 
            this.btnAckAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAckAll.Location = new System.Drawing.Point(320, 7);
            this.btnAckAll.Name = "btnAckAll";
            this.btnAckAll.Size = new System.Drawing.Size(87, 27);
            this.btnAckAll.TabIndex = 3;
            this.btnAckAll.Text = "全部确认";
            this.btnAckAll.UseVisualStyleBackColor = true;
            this.btnAckAll.Click += new System.EventHandler(this.btnAckAll_Click);
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(413, 7);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 27);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "清除已确认";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAll.Location = new System.Drawing.Point(505, 7);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(87, 27);
            this.btnClearAll.TabIndex = 5;
            this.btnClearAll.Text = "全部清除";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnFilterLastHour
            // 
            this.btnFilterLastHour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterLastHour.Location = new System.Drawing.Point(320, 39);
            this.btnFilterLastHour.Name = "btnFilterLastHour";
            this.btnFilterLastHour.Size = new System.Drawing.Size(87, 27);
            this.btnFilterLastHour.TabIndex = 6;
            this.btnFilterLastHour.Text = "最近1小时";
            this.btnFilterLastHour.UseVisualStyleBackColor = true;
            this.btnFilterLastHour.Click += new System.EventHandler(this.btnFilterLastHour_Click);
            // 
            // btnFilterLast5Min
            // 
            this.btnFilterLast5Min.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterLast5Min.Location = new System.Drawing.Point(413, 40);
            this.btnFilterLast5Min.Name = "btnFilterLast5Min";
            this.btnFilterLast5Min.Size = new System.Drawing.Size(87, 27);
            this.btnFilterLast5Min.TabIndex = 7;
            this.btnFilterLast5Min.Text = "最近5分钟";
            this.btnFilterLast5Min.UseVisualStyleBackColor = true;
            this.btnFilterLast5Min.Click += new System.EventHandler(this.btnFilterLast5Min_Click);
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFilter.Location = new System.Drawing.Point(505, 40);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(87, 27);
            this.btnClearFilter.TabIndex = 8;
            this.btnClearFilter.Text = "清除筛选";
            this.btnClearFilter.UseVisualStyleBackColor = true;
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // tipLabel
            // 
            this.tipLabel.AutoSize = true;
            this.tipLabel.ForeColor = System.Drawing.Color.Gray;
            this.tipLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tipLabel.Location = new System.Drawing.Point(20, 348);
            this.tipLabel.Name = "tipLabel";
            this.tipLabel.Size = new System.Drawing.Size(305, 12);
            this.tipLabel.TabIndex = 3;
            this.tipLabel.Text = "提示：双击报警条目可确认该条报警，滚轮滚动查看更多";
            // 
            // logGroup
            // 
            this.logGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logGroup.Controls.Add(this.logTextBox);
            this.logGroup.Location = new System.Drawing.Point(20, 365);
            this.logGroup.Name = "logGroup";
            this.logGroup.Size = new System.Drawing.Size(602, 80);
            this.logGroup.TabIndex = 4;
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
            // AlarmDisplayPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.logGroup);
            this.Controls.Add(this.tipLabel);
            this.Controls.Add(this.btnPanel);
            this.Controls.Add(this.alarmDisplay);
            this.Controls.Add(this.descLabel);
            this.Name = "AlarmDisplayPage";
            this.Size = new System.Drawing.Size(656, 468);
            this.btnPanel.ResumeLayout(false);
            this.logGroup.ResumeLayout(false);
            this.logGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label descLabel;
        private IndustrialControls.Controls.Alarm.AlarmDisplay alarmDisplay;
        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnAddEmergency;
        private System.Windows.Forms.Button btnAddImportant;
        private System.Windows.Forms.Button btnAddGeneral;
        private System.Windows.Forms.Button btnAckAll;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnFilterLastHour;
        private System.Windows.Forms.Button btnFilterLast5Min;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.Label tipLabel;
        private System.Windows.Forms.GroupBox logGroup;
        private System.Windows.Forms.TextBox logTextBox;
    }
}
