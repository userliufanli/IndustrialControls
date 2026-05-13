namespace IndustrialControls.Demo.Pages
{
    partial class DeviceButtonPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        private void InitializeComponent()
        {
            this.descLabel = new System.Windows.Forms.Label();
            this.basicGroup = new System.Windows.Forms.GroupBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.btn3 = new IndustrialControls.Controls.DeviceButton.DeviceControlButton();
            this.btn2 = new IndustrialControls.Controls.DeviceButton.DeviceControlButton();
            this.btn1 = new IndustrialControls.Controls.DeviceButton.DeviceControlButton();
            this.longPressGroup = new System.Windows.Forms.GroupBox();
            this.longPressLabel = new System.Windows.Forms.Label();
            this.btnLongPress = new IndustrialControls.Controls.DeviceButton.DeviceControlButton();
            this.disabledGroup = new System.Windows.Forms.GroupBox();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.btnDisabled = new IndustrialControls.Controls.DeviceButton.DeviceControlButton();
            this.basicGroup.SuspendLayout();
            this.longPressGroup.SuspendLayout();
            this.disabledGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // descLabel
            // 
            this.descLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descLabel.AutoSize = true;
            this.descLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.descLabel.Location = new System.Drawing.Point(20, 20);
            this.descLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(345, 20);
            this.descLabel.TabIndex = 0;
            this.descLabel.Text = "设备控制按钮演示 - 多状态切换、长按确认、图标支持";
            // 
            // basicGroup
            // 
            this.basicGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.basicGroup.Controls.Add(this.statusLabel);
            this.basicGroup.Controls.Add(this.btn3);
            this.basicGroup.Controls.Add(this.btn2);
            this.basicGroup.Controls.Add(this.btn1);
            this.basicGroup.Location = new System.Drawing.Point(20, 60);
            this.basicGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.basicGroup.Name = "basicGroup";
            this.basicGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.basicGroup.Size = new System.Drawing.Size(548, 130);
            this.basicGroup.TabIndex = 1;
            this.basicGroup.TabStop = false;
            this.basicGroup.Text = "基本控制按钮";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.ForeColor = System.Drawing.Color.Gray;
            this.statusLabel.Location = new System.Drawing.Point(20, 95);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(101, 12);
            this.statusLabel.TabIndex = 3;
            this.statusLabel.Text = "点击按钮切换状态";
            // 
            // btn3
            // 
            this.btn3.ButtonText = "复位";
            this.btn3.CornerRadius = 6;
            this.btn3.CurrentStateIndex = 0;
            this.btn3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn3.Icon = null;
            this.btn3.Location = new System.Drawing.Point(380, 40);
            this.btn3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn3.Mode = IndustrialControls.Controls.DeviceButton.DisplayMode.StatusMode;
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(140, 45);
            this.btn3.TabIndex = 2;
            // 
            // btn2
            // 
            this.btn2.ButtonText = "暂停/恢复";
            this.btn2.CornerRadius = 6;
            this.btn2.CurrentStateIndex = 0;
            this.btn2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn2.Icon = null;
            this.btn2.Location = new System.Drawing.Point(210, 40);
            this.btn2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(140, 45);
            this.btn2.TabIndex = 1;
            // 
            // btn1
            // 
            this.btn1.ButtonText = "启动/停止";
            this.btn1.CornerRadius = 6;
            this.btn1.CurrentStateIndex = 0;
            this.btn1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn1.Icon = null;
            this.btn1.Location = new System.Drawing.Point(40, 40);
            this.btn1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn1.Mode = IndustrialControls.Controls.DeviceButton.DisplayMode.StatusMode;
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(140, 45);
            this.btn1.TabIndex = 0;
            this.btn1.StateChanged += new System.EventHandler<IndustrialControls.Controls.DeviceButton.DeviceButtonState>(this.btn1_StateChanged);
            // 
            // longPressGroup
            // 
            this.longPressGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.longPressGroup.Controls.Add(this.longPressLabel);
            this.longPressGroup.Controls.Add(this.btnLongPress);
            this.longPressGroup.Location = new System.Drawing.Point(20, 200);
            this.longPressGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.longPressGroup.Name = "longPressGroup";
            this.longPressGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.longPressGroup.Size = new System.Drawing.Size(548, 120);
            this.longPressGroup.TabIndex = 2;
            this.longPressGroup.TabStop = false;
            this.longPressGroup.Text = "长按确认按钮（防误操作）";
            // 
            // longPressLabel
            // 
            this.longPressLabel.AutoSize = true;
            this.longPressLabel.ForeColor = System.Drawing.Color.Gray;
            this.longPressLabel.Location = new System.Drawing.Point(190, 50);
            this.longPressLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.longPressLabel.Name = "longPressLabel";
            this.longPressLabel.Size = new System.Drawing.Size(227, 12);
            this.longPressLabel.TabIndex = 1;
            this.longPressLabel.Text = "长按1.5秒确认执行，进度条显示长按进度";
            // 
            // btnLongPress
            // 
            this.btnLongPress.ButtonText = "紧急停止";
            this.btnLongPress.CornerRadius = 6;
            this.btnLongPress.CurrentStateIndex = 0;
            this.btnLongPress.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLongPress.EnableLongPress = true;
            this.btnLongPress.Icon = null;
            this.btnLongPress.Location = new System.Drawing.Point(40, 40);
            this.btnLongPress.LongPressTime = 1500;
            this.btnLongPress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLongPress.Name = "btnLongPress";
            this.btnLongPress.Size = new System.Drawing.Size(140, 50);
            this.btnLongPress.TabIndex = 0;
            this.btnLongPress.StateChanged += new System.EventHandler<IndustrialControls.Controls.DeviceButton.DeviceButtonState>(this.btnLongPress_StateChanged);
            // 
            // disabledGroup
            // 
            this.disabledGroup.Controls.Add(this.chkEnable);
            this.disabledGroup.Controls.Add(this.btnDisabled);
            this.disabledGroup.Location = new System.Drawing.Point(20, 330);
            this.disabledGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.disabledGroup.Name = "disabledGroup";
            this.disabledGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.disabledGroup.Size = new System.Drawing.Size(335, 120);
            this.disabledGroup.TabIndex = 3;
            this.disabledGroup.TabStop = false;
            this.disabledGroup.Text = "禁用状态";
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Location = new System.Drawing.Point(200, 50);
            this.chkEnable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(72, 16);
            this.chkEnable.TabIndex = 1;
            this.chkEnable.Text = "启用按钮";
            this.chkEnable.CheckedChanged += new System.EventHandler(this.chkEnable_CheckedChanged);
            // 
            // btnDisabled
            // 
            this.btnDisabled.ButtonText = "已禁用";
            this.btnDisabled.CornerRadius = 10;
            this.btnDisabled.CurrentStateIndex = 0;
            this.btnDisabled.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDisabled.Enabled = false;
            this.btnDisabled.Icon = null;
            this.btnDisabled.Location = new System.Drawing.Point(40, 40);
            this.btnDisabled.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDisabled.Name = "btnDisabled";
            this.btnDisabled.Size = new System.Drawing.Size(140, 50);
            this.btnDisabled.TabIndex = 0;
            // 
            // DeviceButtonPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.disabledGroup);
            this.Controls.Add(this.longPressGroup);
            this.Controls.Add(this.basicGroup);
            this.Controls.Add(this.descLabel);
            this.Name = "DeviceButtonPage";
            this.Size = new System.Drawing.Size(596, 500);
            this.basicGroup.ResumeLayout(false);
            this.basicGroup.PerformLayout();
            this.longPressGroup.ResumeLayout(false);
            this.longPressGroup.PerformLayout();
            this.disabledGroup.ResumeLayout(false);
            this.disabledGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label descLabel;
        private System.Windows.Forms.GroupBox basicGroup;
        private System.Windows.Forms.Label statusLabel;
        private IndustrialControls.Controls.DeviceButton.DeviceControlButton btn3;
        private IndustrialControls.Controls.DeviceButton.DeviceControlButton btn2;
        private IndustrialControls.Controls.DeviceButton.DeviceControlButton btn1;
        private System.Windows.Forms.GroupBox longPressGroup;
        private System.Windows.Forms.Label longPressLabel;
        private IndustrialControls.Controls.DeviceButton.DeviceControlButton btnLongPress;
        private System.Windows.Forms.GroupBox disabledGroup;
        private System.Windows.Forms.CheckBox chkEnable;
        private IndustrialControls.Controls.DeviceButton.DeviceControlButton btnDisabled;
    }
}
