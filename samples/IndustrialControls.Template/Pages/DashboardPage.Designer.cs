namespace IndustrialControls.Template.Pages
{
    partial class DashboardPage
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.Label labelLine;
        private System.Windows.Forms.Label labelDevices;

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
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelUser = new System.Windows.Forms.Label();
            this.labelModel = new System.Windows.Forms.Label();
            this.labelLine = new System.Windows.Forms.Label();
            this.labelDevices = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // labelTitle
            //
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(18, 16);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(106, 21);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "仪表盘首页";
            //
            // labelUser
            //
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(20, 56);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(65, 12);
            this.labelUser.TabIndex = 1;
            this.labelUser.Text = "当前用户：";
            //
            // labelModel
            //
            this.labelModel.AutoSize = true;
            this.labelModel.Location = new System.Drawing.Point(20, 84);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(41, 12);
            this.labelModel.TabIndex = 2;
            this.labelModel.Text = "机型：";
            //
            // labelLine
            //
            this.labelLine.AutoSize = true;
            this.labelLine.Location = new System.Drawing.Point(20, 112);
            this.labelLine.Name = "labelLine";
            this.labelLine.Size = new System.Drawing.Size(65, 12);
            this.labelLine.TabIndex = 3;
            this.labelLine.Text = "通信摘要：";
            //
            // labelDevices
            //
            this.labelDevices.AutoSize = true;
            this.labelDevices.Location = new System.Drawing.Point(20, 140);
            this.labelDevices.Name = "labelDevices";
            this.labelDevices.Size = new System.Drawing.Size(65, 12);
            this.labelDevices.TabIndex = 4;
            this.labelDevices.Text = "设备条目：";
            //
            // DashboardPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDevices);
            this.Controls.Add(this.labelLine);
            this.Controls.Add(this.labelModel);
            this.Controls.Add(this.labelUser);
            this.Controls.Add(this.labelTitle);
            this.Name = "DashboardPage";
            this.Size = new System.Drawing.Size(900, 520);
            this.Load += new System.EventHandler(this.DashboardPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
