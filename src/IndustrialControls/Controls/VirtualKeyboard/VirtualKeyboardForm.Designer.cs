namespace IndustrialControls.Controls.VirtualKeyboard
{
    partial class VirtualKeyboardForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            // 清理布局监控计时器
            if (disposing)
            {
                _layoutMonitorTimer?.Stop();
                _layoutMonitorTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            this._titleBar = new System.Windows.Forms.Panel();
            this._closeButton = new System.Windows.Forms.Button();
            this._keyboardPanel = new IndustrialControls.Controls.VirtualKeyboard.VirtualKeyboardPanel();
            this._titleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _titleBar
            // 
            this._titleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this._titleBar.Controls.Add(this._closeButton);
            this._titleBar.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this._titleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this._titleBar.Location = new System.Drawing.Point(0, 0);
            this._titleBar.Margin = new System.Windows.Forms.Padding(2);
            this._titleBar.Name = "_titleBar";
            this._titleBar.Size = new System.Drawing.Size(580, 26);
            this._titleBar.TabIndex = 0;
            this._titleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);
            this._titleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseMove);
            this._titleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseUp);
            // 
            // _closeButton
            // 
            this._closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(43)))), ((int)(((byte)(28)))));
            this._closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._closeButton.Dock = System.Windows.Forms.DockStyle.Right;
            this._closeButton.FlatAppearance.BorderSize = 0;
            this._closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._closeButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._closeButton.ForeColor = System.Drawing.Color.White;
            this._closeButton.Location = new System.Drawing.Point(549, 0);
            this._closeButton.Margin = new System.Windows.Forms.Padding(2);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(31, 26);
            this._closeButton.TabIndex = 1;
            this._closeButton.Text = "✕";
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // _keyboardPanel
            // 
            this._keyboardPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this._keyboardPanel.Config.ButtonHeight = 50;
            this._keyboardPanel.Config.ButtonSpacing = 6;
            this._keyboardPanel.Config.ButtonWidth = 60;
            this._keyboardPanel.Config.NormalFont = new System.Drawing.Font("Segoe UI", 13F);
            this._keyboardPanel.Config.PanelPadding = 10;
            this._keyboardPanel.Config.SpecialFont = new System.Drawing.Font("Segoe UI", 11F);
            this._keyboardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._keyboardPanel.Location = new System.Drawing.Point(0, 26);
            this._keyboardPanel.Margin = new System.Windows.Forms.Padding(2);
            this._keyboardPanel.Name = "_keyboardPanel";
            this._keyboardPanel.Size = new System.Drawing.Size(580, 198);
            this._keyboardPanel.TabIndex = 1;
            // 
            // VirtualKeyboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(580, 224);
            this.Controls.Add(this._keyboardPanel);
            this.Controls.Add(this._titleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "VirtualKeyboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "虚拟键盘";
            this._titleBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _titleBar;
        private System.Windows.Forms.Button _closeButton;
        private VirtualKeyboardPanel _keyboardPanel;
    }
}
