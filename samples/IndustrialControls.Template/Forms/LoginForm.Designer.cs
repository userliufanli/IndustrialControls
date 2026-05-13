namespace IndustrialControls.Template.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private IndustrialControls.Controls.Login.FlatLoginControl flatLoginControl;
        private System.Windows.Forms.FlowLayoutPanel flowBottom;
        private System.Windows.Forms.Button buttonCancel;

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
            this.flatLoginControl = new IndustrialControls.Controls.Login.FlatLoginControl();
            this.flowBottom = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.flowBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // flatLoginControl
            // 
            this.flatLoginControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.flatLoginControl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.flatLoginControl.Location = new System.Drawing.Point(21, 12);
            this.flatLoginControl.MaximumSize = new System.Drawing.Size(400, 320);
            this.flatLoginControl.MinimumSize = new System.Drawing.Size(400, 320);
            this.flatLoginControl.Name = "flatLoginControl";
            this.flatLoginControl.Padding = new System.Windows.Forms.Padding(16);
            this.flatLoginControl.Size = new System.Drawing.Size(400, 320);
            this.flatLoginControl.TabIndex = 0;
            // 
            // flowBottom
            // 
            this.flowBottom.AutoSize = true;
            this.flowBottom.Controls.Add(this.buttonCancel);
            this.flowBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowBottom.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowBottom.Location = new System.Drawing.Point(0, 326);
            this.flowBottom.Name = "flowBottom";
            this.flowBottom.Padding = new System.Windows.Forms.Padding(8);
            this.flowBottom.Size = new System.Drawing.Size(440, 45);
            this.flowBottom.TabIndex = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(346, 11);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "退出";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(440, 371);
            this.Controls.Add(this.flowBottom);
            this.Controls.Add(this.flatLoginControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.flowBottom.ResumeLayout(false);
            this.flowBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
