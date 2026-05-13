namespace IndustrialControls.Demo.Pages
{
    partial class DataInputPage
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
            this.numGroup = new System.Windows.Forms.GroupBox();
            this.numLabel = new System.Windows.Forms.Label();
            this.numInput2 = new IndustrialControls.Controls.DataInput.NumericInputBox();
            this.numInput1 = new IndustrialControls.Controls.DataInput.NumericInputBox();
            this.validGroup = new System.Windows.Forms.GroupBox();
            this.validText2 = new IndustrialControls.Controls.DataInput.ValidatedTextBox();
            this.validText1 = new IndustrialControls.Controls.DataInput.ValidatedTextBox();
            this.panelGroup = new System.Windows.Forms.GroupBox();
            this.panel3 = new IndustrialControls.Controls.DataInput.DataInputPanel();
            this.panel2 = new IndustrialControls.Controls.DataInput.DataInputPanel();
            this.panel1 = new IndustrialControls.Controls.DataInput.DataInputPanel();
            this.logGroup = new System.Windows.Forms.GroupBox();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnResetValues = new System.Windows.Forms.Button();
            this.btnTestClamp = new System.Windows.Forms.Button();
            this.btnToggleButtons = new System.Windows.Forms.Button();
            this.btnChangeCornerRadius = new System.Windows.Forms.Button();
            this.numGroup.SuspendLayout();
            this.validGroup.SuspendLayout();
            this.panelGroup.SuspendLayout();
            this.logGroup.SuspendLayout();
            this.btnPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // descLabel
            // 
            this.descLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descLabel.AutoSize = true;
            this.descLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.descLabel.Location = new System.Drawing.Point(20, 20);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(331, 20);
            this.descLabel.TabIndex = 0;
            this.descLabel.Text = "数据输入面板演示 - 数值输入、文本验证、组合面板";
            // 
            // numGroup
            // 
            this.numGroup.Controls.Add(this.numLabel);
            this.numGroup.Controls.Add(this.numInput2);
            this.numGroup.Controls.Add(this.numInput1);
            this.numGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numGroup.Location = new System.Drawing.Point(20, 40);
            this.numGroup.Name = "numGroup";
            this.numGroup.Size = new System.Drawing.Size(520, 107);
            this.numGroup.TabIndex = 1;
            this.numGroup.TabStop = false;
            this.numGroup.Text = "数值输入框 (NumericInputBox)";
            // 
            // numLabel
            // 
            this.numLabel.AutoSize = true;
            this.numLabel.ForeColor = System.Drawing.Color.Gray;
            this.numLabel.Location = new System.Drawing.Point(20, 67);
            this.numLabel.Name = "numLabel";
            this.numLabel.Size = new System.Drawing.Size(209, 12);
            this.numLabel.TabIndex = 2;
            this.numLabel.Text = "支持上下限验证、步进调节、单位显示";
            // 
            // numInput2
            // 
            this.numInput2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.numInput2.DecimalPlaces = 1;
            this.numInput2.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.numInput2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.numInput2.Location = new System.Drawing.Point(192, 27);
            this.numInput2.Maximum = 200D;
            this.numInput2.Minimum = -50D;
            this.numInput2.Name = "numInput2";
            this.numInput2.Size = new System.Drawing.Size(151, 30);
            this.numInput2.TabIndex = 1;
            this.numInput2.Unit = "°C";
            this.numInput2.Value = 25.5D;
            // 
            // numInput1
            // 
            this.numInput1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.numInput1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.numInput1.ForeColor = System.Drawing.Color.Black;
            this.numInput1.Location = new System.Drawing.Point(33, 27);
            this.numInput1.Name = "numInput1";
            this.numInput1.Size = new System.Drawing.Size(153, 30);
            this.numInput1.Step = 0.5D;
            this.numInput1.TabIndex = 0;
            this.numInput1.TextColor = System.Drawing.Color.Black;
            this.numInput1.Unit = "mm";
            this.numInput1.Value = 50D;
            // 
            // validGroup
            // 
            this.validGroup.Controls.Add(this.validText2);
            this.validGroup.Controls.Add(this.validText1);
            this.validGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.validGroup.Location = new System.Drawing.Point(20, 157);
            this.validGroup.Name = "validGroup";
            this.validGroup.Size = new System.Drawing.Size(520, 107);
            this.validGroup.TabIndex = 2;
            this.validGroup.TabStop = false;
            this.validGroup.Text = "验证文本框 (ValidatedTextBox)";
            // 
            // validText2
            // 
            this.validText2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.validText2.ErrorMessage = "请输入有效的IPv4地址";
            this.validText2.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.validText2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.validText2.Location = new System.Drawing.Point(20, 63);
            this.validText2.Mode = IndustrialControls.Controls.DataInput.ValidationMode.Realtime;
            this.validText2.Name = "validText2";
            this.validText2.Placeholder = "192.168.1.1";
            this.validText2.Preset = IndustrialControls.Controls.DataInput.ValidationPreset.IpAddress;
            this.validText2.Required = true;
            this.validText2.Size = new System.Drawing.Size(467, 33);
            this.validText2.TabIndex = 1;
            this.validText2.ValidationPattern = "^(\\d{1,3}\\.){3}\\d{1,3}$";
            // 
            // validText1
            // 
            this.validText1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.validText1.ErrorMessage = "请输入有效的数字";
            this.validText1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.validText1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.validText1.Location = new System.Drawing.Point(20, 23);
            this.validText1.Mode = IndustrialControls.Controls.DataInput.ValidationMode.Realtime;
            this.validText1.Name = "validText1";
            this.validText1.Placeholder = "123.45";
            this.validText1.Preset = IndustrialControls.Controls.DataInput.ValidationPreset.Number;
            this.validText1.Required = true;
            this.validText1.Size = new System.Drawing.Size(467, 33);
            this.validText1.TabIndex = 0;
            this.validText1.ValidationPattern = "^-?\\d+(\\.\\d+)?$";
            // 
            // panelGroup
            // 
            this.panelGroup.Controls.Add(this.panel3);
            this.panelGroup.Controls.Add(this.panel2);
            this.panelGroup.Controls.Add(this.panel1);
            this.panelGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGroup.Location = new System.Drawing.Point(20, 269);
            this.panelGroup.Name = "panelGroup";
            this.panelGroup.Size = new System.Drawing.Size(520, 125);
            this.panelGroup.TabIndex = 3;
            this.panelGroup.TabStop = false;
            this.panelGroup.Text = "数据输入面板 (DataInputPanel)";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.panel3.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.LabelText = "压力设定";
            this.panel3.Location = new System.Drawing.Point(20, 79);
            this.panel3.Maximum = 10D;
            this.panel3.Minimum = 0D;
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(179, 30);
            this.panel3.TabIndex = 2;
            this.panel3.TextColor = System.Drawing.Color.Black;
            this.panel3.Unit = "MPa";
            this.panel3.Value = 2.5D;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.panel2.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panel2.LabelText = "温度上限";
            this.panel2.Location = new System.Drawing.Point(20, 48);
            this.panel2.Maximum = 150D;
            this.panel2.Minimum = 0D;
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(179, 30);
            this.panel2.TabIndex = 1;
            this.panel2.Unit = "°C";
            this.panel2.Value = 85D;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.panel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panel1.LabelText = "速度设定";
            this.panel1.Location = new System.Drawing.Point(20, 17);
            this.panel1.Maximum = 3000D;
            this.panel1.Minimum = 0D;
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(179, 26);
            this.panel1.TabIndex = 0;
            this.panel1.Unit = "rpm";
            this.panel1.Value = 1500D;
            // 
            // logGroup
            // 
            this.logGroup.Controls.Add(this.logTextBox);
            this.logGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logGroup.Location = new System.Drawing.Point(20, 400);
            this.logGroup.Name = "logGroup";
            this.logGroup.Size = new System.Drawing.Size(520, 80);
            this.logGroup.TabIndex = 4;
            this.logGroup.TabStop = false;
            this.logGroup.Text = "事件日志";
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Font = new System.Drawing.Font("Consolas", 9F);
            this.logTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.logTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.TabIndex = 0;
            // 
            // btnPanel
            // 
            this.btnPanel.Controls.Add(this.btnResetValues);
            this.btnPanel.Controls.Add(this.btnTestClamp);
            this.btnPanel.Controls.Add(this.btnToggleButtons);
            this.btnPanel.Controls.Add(this.btnChangeCornerRadius);
            this.btnPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPanel.Location = new System.Drawing.Point(20, 490);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(520, 33);
            this.btnPanel.TabIndex = 5;
            // 
            // btnResetValues
            // 
            this.btnResetValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetValues.Location = new System.Drawing.Point(0, 4);
            this.btnResetValues.Name = "btnResetValues";
            this.btnResetValues.Size = new System.Drawing.Size(93, 25);
            this.btnResetValues.TabIndex = 0;
            this.btnResetValues.Text = "重置所有值";
            this.btnResetValues.UseVisualStyleBackColor = true;
            this.btnResetValues.Click += new System.EventHandler(this.btnResetValues_Click);
            // 
            // btnTestClamp
            // 
            this.btnTestClamp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestClamp.Location = new System.Drawing.Point(100, 4);
            this.btnTestClamp.Name = "btnTestClamp";
            this.btnTestClamp.Size = new System.Drawing.Size(107, 25);
            this.btnTestClamp.TabIndex = 1;
            this.btnTestClamp.Text = "测试上下限钳位";
            this.btnTestClamp.UseVisualStyleBackColor = true;
            this.btnTestClamp.Click += new System.EventHandler(this.btnTestClamp_Click);
            // 
            // btnToggleButtons
            // 
            this.btnToggleButtons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleButtons.Location = new System.Drawing.Point(213, 4);
            this.btnToggleButtons.Name = "btnToggleButtons";
            this.btnToggleButtons.Size = new System.Drawing.Size(107, 25);
            this.btnToggleButtons.TabIndex = 2;
            this.btnToggleButtons.Text = "切换步进按钮";
            this.btnToggleButtons.UseVisualStyleBackColor = true;
            this.btnToggleButtons.Click += new System.EventHandler(this.btnToggleButtons_Click);
            // 
            // btnChangeCornerRadius
            // 
            this.btnChangeCornerRadius.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeCornerRadius.Location = new System.Drawing.Point(327, 4);
            this.btnChangeCornerRadius.Name = "btnChangeCornerRadius";
            this.btnChangeCornerRadius.Size = new System.Drawing.Size(107, 25);
            this.btnChangeCornerRadius.TabIndex = 3;
            this.btnChangeCornerRadius.Text = "切换圆角半径";
            this.btnChangeCornerRadius.UseVisualStyleBackColor = true;
            this.btnChangeCornerRadius.Click += new System.EventHandler(this.btnChangeCornerRadius_Click);
            // 
            // DataInputPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnPanel);
            this.Controls.Add(this.logGroup);
            this.Controls.Add(this.panelGroup);
            this.Controls.Add(this.validGroup);
            this.Controls.Add(this.numGroup);
            this.Controls.Add(this.descLabel);
            this.Name = "DataInputPage";
            this.Size = new System.Drawing.Size(560, 540);
            this.numGroup.ResumeLayout(false);
            this.numGroup.PerformLayout();
            this.validGroup.ResumeLayout(false);
            this.panelGroup.ResumeLayout(false);
            this.logGroup.ResumeLayout(false);
            this.logGroup.PerformLayout();
            this.btnPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label descLabel;
        private System.Windows.Forms.GroupBox numGroup;
        private System.Windows.Forms.Label numLabel;
        private IndustrialControls.Controls.DataInput.NumericInputBox numInput2;
        private IndustrialControls.Controls.DataInput.NumericInputBox numInput1;
        private System.Windows.Forms.GroupBox validGroup;
        private IndustrialControls.Controls.DataInput.ValidatedTextBox validText2;
        private IndustrialControls.Controls.DataInput.ValidatedTextBox validText1;
        private System.Windows.Forms.GroupBox panelGroup;
        private IndustrialControls.Controls.DataInput.DataInputPanel panel3;
        private IndustrialControls.Controls.DataInput.DataInputPanel panel2;
        private IndustrialControls.Controls.DataInput.DataInputPanel panel1;
        private System.Windows.Forms.GroupBox logGroup;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnResetValues;
        private System.Windows.Forms.Button btnTestClamp;
        private System.Windows.Forms.Button btnToggleButtons;
        private System.Windows.Forms.Button btnChangeCornerRadius;
    }
}
