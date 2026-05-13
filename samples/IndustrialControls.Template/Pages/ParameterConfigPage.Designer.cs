namespace IndustrialControls.Template.Pages
{
    partial class ParameterConfigPage
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox textBoxHint;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelTitle;

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
            this.textBoxHint = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
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
            this.labelTitle.Text = "参数配置";
            //
            // textBoxHint
            //
            this.textBoxHint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHint.Location = new System.Drawing.Point(18, 46);
            this.textBoxHint.Multiline = true;
            this.textBoxHint.Name = "textBoxHint";
            this.textBoxHint.ReadOnly = true;
            this.textBoxHint.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxHint.Size = new System.Drawing.Size(864, 420);
            this.textBoxHint.TabIndex = 1;
            //
            // buttonSave
            //
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(768, 480);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(114, 28);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "保存全局参数";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            //
            // ParameterConfigPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxHint);
            this.Controls.Add(this.labelTitle);
            this.Name = "ParameterConfigPage";
            this.Size = new System.Drawing.Size(900, 520);
            this.Load += new System.EventHandler(this.ParameterConfigPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
