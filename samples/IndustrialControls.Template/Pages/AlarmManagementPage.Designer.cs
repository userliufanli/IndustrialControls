namespace IndustrialControls.Template.Pages
{
    partial class AlarmManagementPage
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox groupBoxAlarms;
        private IndustrialControls.Controls.Alarm.AlarmDisplay alarmDisplayMain;
        private System.Windows.Forms.FlowLayoutPanel flowButtons;
        private System.Windows.Forms.Button buttonAckAll;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonTestAlarm;

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
            this.groupBoxAlarms = new System.Windows.Forms.GroupBox();
            this.alarmDisplayMain = new IndustrialControls.Controls.Alarm.AlarmDisplay();
            this.flowButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonTestAlarm = new System.Windows.Forms.Button();
            this.buttonAckAll = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.groupBoxAlarms.SuspendLayout();
            this.flowButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBoxAlarms
            //
            this.groupBoxAlarms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAlarms.Controls.Add(this.flowButtons);
            this.groupBoxAlarms.Controls.Add(this.alarmDisplayMain);
            this.groupBoxAlarms.Location = new System.Drawing.Point(14, 12);
            this.groupBoxAlarms.Name = "groupBoxAlarms";
            this.groupBoxAlarms.Size = new System.Drawing.Size(872, 496);
            this.groupBoxAlarms.TabIndex = 0;
            this.groupBoxAlarms.TabStop = false;
            this.groupBoxAlarms.Text = "报警列表";
            //
            // alarmDisplayMain
            //
            this.alarmDisplayMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alarmDisplayMain.Location = new System.Drawing.Point(3, 17);
            this.alarmDisplayMain.Name = "alarmDisplayMain";
            this.alarmDisplayMain.Size = new System.Drawing.Size(866, 436);
            this.alarmDisplayMain.TabIndex = 0;
            //
            // flowButtons
            //
            this.flowButtons.AutoSize = true;
            this.flowButtons.Controls.Add(this.buttonClear);
            this.flowButtons.Controls.Add(this.buttonAckAll);
            this.flowButtons.Controls.Add(this.buttonTestAlarm);
            this.flowButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowButtons.Location = new System.Drawing.Point(3, 453);
            this.flowButtons.Name = "flowButtons";
            this.flowButtons.Padding = new System.Windows.Forms.Padding(6);
            this.flowButtons.Size = new System.Drawing.Size(866, 46);
            this.flowButtons.TabIndex = 1;
            //
            // buttonTestAlarm
            //
            this.buttonTestAlarm.Location = new System.Drawing.Point(14, 9);
            this.buttonTestAlarm.Name = "buttonTestAlarm";
            this.buttonTestAlarm.Size = new System.Drawing.Size(96, 24);
            this.buttonTestAlarm.TabIndex = 2;
            this.buttonTestAlarm.Text = "产生测试报警";
            this.buttonTestAlarm.UseVisualStyleBackColor = true;
            this.buttonTestAlarm.Click += new System.EventHandler(this.buttonTestAlarm_Click);
            //
            // buttonAckAll
            //
            this.buttonAckAll.Location = new System.Drawing.Point(116, 9);
            this.buttonAckAll.Name = "buttonAckAll";
            this.buttonAckAll.Size = new System.Drawing.Size(96, 24);
            this.buttonAckAll.TabIndex = 1;
            this.buttonAckAll.Text = "全部确认";
            this.buttonAckAll.UseVisualStyleBackColor = true;
            this.buttonAckAll.Click += new System.EventHandler(this.buttonAckAll_Click);
            //
            // buttonClear
            //
            this.buttonClear.Location = new System.Drawing.Point(218, 9);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(96, 24);
            this.buttonClear.TabIndex = 0;
            this.buttonClear.Text = "清空列表";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            //
            // AlarmManagementPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxAlarms);
            this.Name = "AlarmManagementPage";
            this.Size = new System.Drawing.Size(900, 520);
            this.Load += new System.EventHandler(this.AlarmManagementPage_Load);
            this.groupBoxAlarms.ResumeLayout(false);
            this.groupBoxAlarms.PerformLayout();
            this.flowButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
