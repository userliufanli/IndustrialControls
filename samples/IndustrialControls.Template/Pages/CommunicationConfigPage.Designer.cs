namespace IndustrialControls.Template.Pages
{
    partial class CommunicationConfigPage
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label descLabel;
        private IndustrialControls.Controls.Communication.CommunicationControl communicationControl1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommunicationConfigPage));
            this.descLabel = new System.Windows.Forms.Label();
            this.communicationControl1 = new IndustrialControls.Controls.Communication.CommunicationControl();
            this.SuspendLayout();
            // 
            // descLabel
            // 
            this.descLabel.AutoSize = true;
            this.descLabel.Location = new System.Drawing.Point(12, 10);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(491, 12);
            this.descLabel.TabIndex = 0;
            this.descLabel.Text = "通信配置：使用 CommunicationControl（参数持久化见控件自带 ParameterManager 分组）";
            // 
            // communicationControl1
            // 
            this.communicationControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.communicationControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.communicationControl1.DataEncoding = ((System.Text.Encoding)(resources.GetObject("communicationControl1.DataEncoding")));
            this.communicationControl1.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.communicationControl1.FrameDelimiter = new byte[] {
        ((byte)(13)),
        ((byte)(10))};
            this.communicationControl1.Location = new System.Drawing.Point(14, 45);
            this.communicationControl1.MaximumSize = new System.Drawing.Size(200, 196);
            this.communicationControl1.MinimumSize = new System.Drawing.Size(200, 196);
            this.communicationControl1.Name = "communicationControl1";
            this.communicationControl1.SerialPortName = "COM3";
            this.communicationControl1.Size = new System.Drawing.Size(200, 196);
            this.communicationControl1.TabIndex = 1;
            // 
            // CommunicationConfigPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.communicationControl1);
            this.Controls.Add(this.descLabel);
            this.Name = "CommunicationConfigPage";
            this.Size = new System.Drawing.Size(900, 520);
            this.Load += new System.EventHandler(this.CommunicationConfigPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
