namespace IndustrialControls.Template.Pages
{
    partial class DeviceMonitorPage
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox groupBoxMotor;
        private IndustrialControls.Controls.StatusIndicator.StatusIndicator statusIndicatorMotor;
        private IndustrialControls.Controls.DeviceButton.DeviceControlButton deviceControlButtonMotor;
        private System.Windows.Forms.Label descLabel;

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
            this.descLabel = new System.Windows.Forms.Label();
            this.groupBoxMotor = new System.Windows.Forms.GroupBox();
            this.statusIndicatorMotor = new IndustrialControls.Controls.StatusIndicator.StatusIndicator();
            this.deviceControlButtonMotor = new IndustrialControls.Controls.DeviceButton.DeviceControlButton();
            this.groupBoxMotor.SuspendLayout();
            this.SuspendLayout();
            //
            // descLabel
            //
            this.descLabel.AutoSize = true;
            this.descLabel.Location = new System.Drawing.Point(18, 12);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(281, 12);
            this.descLabel.TabIndex = 0;
            this.descLabel.Text = "设备监控：状态指示器 + 控制按钮（模拟标签源）";
            //
            // groupBoxMotor
            //
            this.groupBoxMotor.Controls.Add(this.deviceControlButtonMotor);
            this.groupBoxMotor.Controls.Add(this.statusIndicatorMotor);
            this.groupBoxMotor.Location = new System.Drawing.Point(20, 36);
            this.groupBoxMotor.Name = "groupBoxMotor";
            this.groupBoxMotor.Size = new System.Drawing.Size(420, 120);
            this.groupBoxMotor.TabIndex = 1;
            this.groupBoxMotor.TabStop = false;
            this.groupBoxMotor.Text = "主电机";
            //
            // statusIndicatorMotor
            //
            this.statusIndicatorMotor.Location = new System.Drawing.Point(16, 22);
            this.statusIndicatorMotor.Name = "statusIndicatorMotor";
            this.statusIndicatorMotor.Size = new System.Drawing.Size(72, 88);
            this.statusIndicatorMotor.TabIndex = 0;
            //
            // deviceControlButtonMotor
            //
            this.deviceControlButtonMotor.Location = new System.Drawing.Point(110, 36);
            this.deviceControlButtonMotor.Name = "deviceControlButtonMotor";
            this.deviceControlButtonMotor.Size = new System.Drawing.Size(140, 44);
            this.deviceControlButtonMotor.TabIndex = 1;
            this.deviceControlButtonMotor.StateChanged += new System.EventHandler<IndustrialControls.Controls.DeviceButton.DeviceButtonState>(this.deviceControlButtonMotor_StateChanged);
            //
            // DeviceMonitorPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxMotor);
            this.Controls.Add(this.descLabel);
            this.Name = "DeviceMonitorPage";
            this.Size = new System.Drawing.Size(900, 520);
            this.Load += new System.EventHandler(this.DeviceMonitorPage_Load);
            this.groupBoxMotor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
