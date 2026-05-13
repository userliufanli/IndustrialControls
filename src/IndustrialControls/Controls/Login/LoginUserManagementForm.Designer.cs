namespace IndustrialControls.Controls.Login
{
    partial class LoginUserManagementForm
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
            this.lblHint = new System.Windows.Forms.Label();
            this.listUsers = new System.Windows.Forms.ListBox();
            this.grpAdd = new System.Windows.Forms.GroupBox();
            this.lblAddUserCaption = new System.Windows.Forms.Label();
            this.lblAddPasswordCaption = new System.Windows.Forms.Label();
            this.lblAddConfirmCaption = new System.Windows.Forms.Label();
            this.txtNewUser = new System.Windows.Forms.TextBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpChange = new System.Windows.Forms.GroupBox();
            this.lblChangeOldCaption = new System.Windows.Forms.Label();
            this.lblChangeNewCaption = new System.Windows.Forms.Label();
            this.txtChangeOld = new System.Windows.Forms.TextBox();
            this.txtChangeNew = new System.Windows.Forms.TextBox();
            this.btnChangePwd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpAdd.SuspendLayout();
            this.grpChange.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHint
            // 
            this.lblHint.Location = new System.Drawing.Point(16, 12);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(388, 36);
            this.lblHint.TabIndex = 0;
            this.lblHint.Text = "用户数据保存在参数管理器分组中。用户名不区分大小写。";
            // 
            // listUsers
            // 
            this.listUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listUsers.IntegralHeight = false;
            this.listUsers.ItemHeight = 17;
            this.listUsers.Location = new System.Drawing.Point(16, 52);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(388, 120);
            this.listUsers.TabIndex = 1;
            // 
            // grpAdd
            // 
            this.grpAdd.Controls.Add(this.lblAddUserCaption);
            this.grpAdd.Controls.Add(this.lblAddPasswordCaption);
            this.grpAdd.Controls.Add(this.lblAddConfirmCaption);
            this.grpAdd.Controls.Add(this.txtNewUser);
            this.grpAdd.Controls.Add(this.txtNewPassword);
            this.grpAdd.Controls.Add(this.txtConfirm);
            this.grpAdd.Controls.Add(this.btnAdd);
            this.grpAdd.Location = new System.Drawing.Point(16, 180);
            this.grpAdd.Name = "grpAdd";
            this.grpAdd.Padding = new System.Windows.Forms.Padding(2);
            this.grpAdd.Size = new System.Drawing.Size(388, 167);
            this.grpAdd.TabIndex = 2;
            this.grpAdd.TabStop = false;
            this.grpAdd.Text = "添加用户";
            // 
            // lblAddUserCaption
            // 
            this.lblAddUserCaption.AutoSize = true;
            this.lblAddUserCaption.Location = new System.Drawing.Point(12, 20);
            this.lblAddUserCaption.Name = "lblAddUserCaption";
            this.lblAddUserCaption.Size = new System.Drawing.Size(44, 17);
            this.lblAddUserCaption.TabIndex = 0;
            this.lblAddUserCaption.Text = "用户名";
            // 
            // lblAddPasswordCaption
            // 
            this.lblAddPasswordCaption.AutoSize = true;
            this.lblAddPasswordCaption.Location = new System.Drawing.Point(12, 62);
            this.lblAddPasswordCaption.Name = "lblAddPasswordCaption";
            this.lblAddPasswordCaption.Size = new System.Drawing.Size(32, 17);
            this.lblAddPasswordCaption.TabIndex = 1;
            this.lblAddPasswordCaption.Text = "密码";
            // 
            // lblAddConfirmCaption
            // 
            this.lblAddConfirmCaption.AutoSize = true;
            this.lblAddConfirmCaption.Location = new System.Drawing.Point(202, 62);
            this.lblAddConfirmCaption.Name = "lblAddConfirmCaption";
            this.lblAddConfirmCaption.Size = new System.Drawing.Size(32, 17);
            this.lblAddConfirmCaption.TabIndex = 2;
            this.lblAddConfirmCaption.Text = "确认";
            // 
            // txtNewUser
            // 
            this.txtNewUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewUser.Location = new System.Drawing.Point(12, 38);
            this.txtNewUser.Name = "txtNewUser";
            this.txtNewUser.Size = new System.Drawing.Size(360, 23);
            this.txtNewUser.TabIndex = 3;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewPassword.Location = new System.Drawing.Point(12, 80);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(170, 23);
            this.txtNewPassword.TabIndex = 4;
            this.txtNewPassword.UseSystemPasswordChar = true;
            // 
            // txtConfirm
            // 
            this.txtConfirm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirm.Location = new System.Drawing.Point(202, 80);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(170, 23);
            this.txtConfirm.TabIndex = 5;
            this.txtConfirm.UseSystemPasswordChar = true;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(192)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(280, 120);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(92, 30);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.OnAdd);
            // 
            // grpChange
            // 
            this.grpChange.Controls.Add(this.lblChangeOldCaption);
            this.grpChange.Controls.Add(this.lblChangeNewCaption);
            this.grpChange.Controls.Add(this.txtChangeOld);
            this.grpChange.Controls.Add(this.txtChangeNew);
            this.grpChange.Controls.Add(this.btnChangePwd);
            this.grpChange.Location = new System.Drawing.Point(16, 381);
            this.grpChange.Name = "grpChange";
            this.grpChange.Padding = new System.Windows.Forms.Padding(2);
            this.grpChange.Size = new System.Drawing.Size(388, 86);
            this.grpChange.TabIndex = 3;
            this.grpChange.TabStop = false;
            this.grpChange.Text = "修改密码（选中列表中的用户）";
            // 
            // lblChangeOldCaption
            // 
            this.lblChangeOldCaption.AutoSize = true;
            this.lblChangeOldCaption.Location = new System.Drawing.Point(12, 25);
            this.lblChangeOldCaption.Name = "lblChangeOldCaption";
            this.lblChangeOldCaption.Size = new System.Drawing.Size(44, 17);
            this.lblChangeOldCaption.TabIndex = 0;
            this.lblChangeOldCaption.Text = "原密码";
            // 
            // lblChangeNewCaption
            // 
            this.lblChangeNewCaption.AutoSize = true;
            this.lblChangeNewCaption.Location = new System.Drawing.Point(135, 25);
            this.lblChangeNewCaption.Name = "lblChangeNewCaption";
            this.lblChangeNewCaption.Size = new System.Drawing.Size(44, 17);
            this.lblChangeNewCaption.TabIndex = 1;
            this.lblChangeNewCaption.Text = "新密码";
            // 
            // txtChangeOld
            // 
            this.txtChangeOld.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChangeOld.Location = new System.Drawing.Point(12, 45);
            this.txtChangeOld.Name = "txtChangeOld";
            this.txtChangeOld.Size = new System.Drawing.Size(115, 23);
            this.txtChangeOld.TabIndex = 2;
            this.txtChangeOld.UseSystemPasswordChar = true;
            // 
            // txtChangeNew
            // 
            this.txtChangeNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChangeNew.Location = new System.Drawing.Point(135, 45);
            this.txtChangeNew.Name = "txtChangeNew";
            this.txtChangeNew.Size = new System.Drawing.Size(115, 23);
            this.txtChangeNew.TabIndex = 3;
            this.txtChangeNew.UseSystemPasswordChar = true;
            // 
            // btnChangePwd
            // 
            this.btnChangePwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(192)))));
            this.btnChangePwd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePwd.FlatAppearance.BorderSize = 0;
            this.btnChangePwd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePwd.ForeColor = System.Drawing.Color.White;
            this.btnChangePwd.Location = new System.Drawing.Point(272, 38);
            this.btnChangePwd.Name = "btnChangePwd";
            this.btnChangePwd.Size = new System.Drawing.Size(100, 30);
            this.btnChangePwd.TabIndex = 4;
            this.btnChangePwd.Text = "修改密码";
            this.btnChangePwd.UseVisualStyleBackColor = false;
            this.btnChangePwd.Click += new System.EventHandler(this.OnChangePassword);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(43)))), ((int)(((byte)(28)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(16, 477);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 32);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除选中用户";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.OnDelete);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(209)))), ((int)(((byte)(209)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(300, 477);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 32);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // LoginUserManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(420, 517);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.grpChange);
            this.Controls.Add(this.grpAdd);
            this.Controls.Add(this.listUsers);
            this.Controls.Add(this.lblHint);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginUserManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "用户管理";
            this.grpAdd.ResumeLayout(false);
            this.grpAdd.PerformLayout();
            this.grpChange.ResumeLayout(false);
            this.grpChange.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.ListBox listUsers;
        private System.Windows.Forms.GroupBox grpAdd;
        private System.Windows.Forms.Label lblAddUserCaption;
        private System.Windows.Forms.Label lblAddPasswordCaption;
        private System.Windows.Forms.Label lblAddConfirmCaption;
        private System.Windows.Forms.TextBox txtNewUser;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox grpChange;
        private System.Windows.Forms.Label lblChangeOldCaption;
        private System.Windows.Forms.Label lblChangeNewCaption;
        private System.Windows.Forms.TextBox txtChangeOld;
        private System.Windows.Forms.TextBox txtChangeNew;
        private System.Windows.Forms.Button btnChangePwd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
    }
}
