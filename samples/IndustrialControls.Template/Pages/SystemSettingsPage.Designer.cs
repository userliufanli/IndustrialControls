namespace IndustrialControls.Template.Pages
{
    partial class SystemSettingsPage
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelTheme;
        private System.Windows.Forms.ComboBox comboTheme;
        private System.Windows.Forms.Button buttonApplyTheme;
        private System.Windows.Forms.Label labelCurrent;

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
            this.labelTheme = new System.Windows.Forms.Label();
            this.comboTheme = new System.Windows.Forms.ComboBox();
            this.buttonApplyTheme = new System.Windows.Forms.Button();
            this.labelCurrent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // labelTitle
            //
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(16, 14);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(69, 20);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "系统设置";
            //
            // labelTheme
            //
            this.labelTheme.AutoSize = true;
            this.labelTheme.Location = new System.Drawing.Point(18, 52);
            this.labelTheme.Name = "labelTheme";
            this.labelTheme.Size = new System.Drawing.Size(65, 12);
            this.labelTheme.TabIndex = 1;
            this.labelTheme.Text = "界面主题：";
            //
            // comboTheme
            //
            this.comboTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTheme.FormattingEnabled = true;
            this.comboTheme.Location = new System.Drawing.Point(89, 48);
            this.comboTheme.Name = "comboTheme";
            this.comboTheme.Size = new System.Drawing.Size(160, 20);
            this.comboTheme.TabIndex = 2;
            //
            // buttonApplyTheme
            //
            this.buttonApplyTheme.Location = new System.Drawing.Point(268, 46);
            this.buttonApplyTheme.Name = "buttonApplyTheme";
            this.buttonApplyTheme.Size = new System.Drawing.Size(96, 24);
            this.buttonApplyTheme.TabIndex = 3;
            this.buttonApplyTheme.Text = "应用并保存";
            this.buttonApplyTheme.UseVisualStyleBackColor = true;
            this.buttonApplyTheme.Click += new System.EventHandler(this.buttonApplyTheme_Click);
            //
            // labelCurrent
            //
            this.labelCurrent.AutoSize = true;
            this.labelCurrent.Location = new System.Drawing.Point(18, 88);
            this.labelCurrent.Name = "labelCurrent";
            this.labelCurrent.Size = new System.Drawing.Size(65, 12);
            this.labelCurrent.TabIndex = 4;
            this.labelCurrent.Text = "当前主题：";
            //
            // SystemSettingsPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelCurrent);
            this.Controls.Add(this.buttonApplyTheme);
            this.Controls.Add(this.comboTheme);
            this.Controls.Add(this.labelTheme);
            this.Controls.Add(this.labelTitle);
            this.Name = "SystemSettingsPage";
            this.Size = new System.Drawing.Size(900, 520);
            this.Load += new System.EventHandler(this.SystemSettingsPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
