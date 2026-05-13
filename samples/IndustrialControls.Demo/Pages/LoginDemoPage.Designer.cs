namespace IndustrialControls.Demo.Pages
{
    partial class LoginDemoPage
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

        #region 组件设计器生成的代码

        private void InitializeComponent()
        {
            this.descLabel = new System.Windows.Forms.Label();
            this.grpAdmin = new System.Windows.Forms.GroupBox();
            this.lblAdminHint = new System.Windows.Forms.Label();
            this.txtAdminPin = new System.Windows.Forms.TextBox();
            this.btnOpenUserMgmt = new System.Windows.Forms.Button();
            this.flatLoginControl1 = new IndustrialControls.Controls.Login.FlatLoginControl();
            this.grpAdmin.SuspendLayout();
            this.SuspendLayout();
            // 
            // descLabel
            // 
            this.descLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.descLabel.Location = new System.Drawing.Point(0, 0);
            this.descLabel.Margin = new System.Windows.Forms.Padding(20, 12, 20, 8);
            this.descLabel.Name = "descLabel";
            this.descLabel.Padding = new System.Windows.Forms.Padding(20, 4, 20, 8);
            this.descLabel.Size = new System.Drawing.Size(1016, 56);
            this.descLabel.TabIndex = 0;
            this.descLabel.Text = "登录控件演示：凭据由 ParameterManager 持久化。请先完成下方「运维入口」中的用户管理（需管理口令），再使用用户名与密码登录。";
            // 
            // grpAdmin
            // 
            this.grpAdmin.Controls.Add(this.lblAdminHint);
            this.grpAdmin.Controls.Add(this.txtAdminPin);
            this.grpAdmin.Controls.Add(this.btnOpenUserMgmt);
            this.grpAdmin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpAdmin.Location = new System.Drawing.Point(0, 560);
            this.grpAdmin.Name = "grpAdmin";
            this.grpAdmin.Padding = new System.Windows.Forms.Padding(12, 8, 12, 10);
            this.grpAdmin.Size = new System.Drawing.Size(1016, 112);
            this.grpAdmin.TabIndex = 2;
            this.grpAdmin.TabStop = false;
            this.grpAdmin.Text = "运维入口（需管理口令）";
            // 
            // lblAdminHint
            // 
            this.lblAdminHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAdminHint.Location = new System.Drawing.Point(14, 22);
            this.lblAdminHint.Name = "lblAdminHint";
            this.lblAdminHint.Size = new System.Drawing.Size(988, 36);
            this.lblAdminHint.TabIndex = 0;
            this.lblAdminHint.Text = "用户管理不应在登录界面直接开放。此处为演示：口令默认 1688，可在 parameters.json 的 App 组修改键 LoginManagementUnlo" +
    "ckPin。正式项目请改为角色权限或服务端鉴权后再调用 LoginUserManagementForm.ShowForStore。";
            // 
            // txtAdminPin
            // 
            this.txtAdminPin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAdminPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdminPin.Location = new System.Drawing.Point(14, 72);
            this.txtAdminPin.Name = "txtAdminPin";
            this.txtAdminPin.Size = new System.Drawing.Size(200, 21);
            this.txtAdminPin.TabIndex = 1;
            this.txtAdminPin.UseSystemPasswordChar = true;
            // 
            // btnOpenUserMgmt
            // 
            this.btnOpenUserMgmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenUserMgmt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenUserMgmt.Location = new System.Drawing.Point(228, 70);
            this.btnOpenUserMgmt.Name = "btnOpenUserMgmt";
            this.btnOpenUserMgmt.Size = new System.Drawing.Size(132, 28);
            this.btnOpenUserMgmt.TabIndex = 2;
            this.btnOpenUserMgmt.Text = "打开用户管理";
            this.btnOpenUserMgmt.UseVisualStyleBackColor = true;
            this.btnOpenUserMgmt.Click += new System.EventHandler(this.btnOpenUserMgmt_Click);
            // 
            // flatLoginControl1
            // 
            this.flatLoginControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.flatLoginControl1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.flatLoginControl1.Location = new System.Drawing.Point(228, 108);
            this.flatLoginControl1.Margin = new System.Windows.Forms.Padding(2);
            this.flatLoginControl1.MaximumSize = new System.Drawing.Size(400, 320);
            this.flatLoginControl1.MinimumSize = new System.Drawing.Size(400, 320);
            this.flatLoginControl1.Name = "flatLoginControl1";
            this.flatLoginControl1.Padding = new System.Windows.Forms.Padding(16);
            this.flatLoginControl1.Size = new System.Drawing.Size(400, 320);
            this.flatLoginControl1.TabIndex = 1;
            // 
            // LoginDemoPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.flatLoginControl1);
            this.Controls.Add(this.descLabel);
            this.Controls.Add(this.grpAdmin);
            this.Name = "LoginDemoPage";
            this.Size = new System.Drawing.Size(1016, 672);
            this.grpAdmin.ResumeLayout(false);
            this.grpAdmin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label descLabel;
        private IndustrialControls.Controls.Login.FlatLoginControl flatLoginControl1;
        private System.Windows.Forms.GroupBox grpAdmin;
        private System.Windows.Forms.Label lblAdminHint;
        private System.Windows.Forms.TextBox txtAdminPin;
        private System.Windows.Forms.Button btnOpenUserMgmt;
    }
}
