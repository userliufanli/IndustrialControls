namespace IndustrialControls.Demo.Pages
{
    partial class VirtualKeyboardPage
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
            this.controlGroup = new System.Windows.Forms.GroupBox();
            this.btnCrossFormTest = new System.Windows.Forms.Button();
            this.btnShowForNumeric = new System.Windows.Forms.Button();
            this.btnShowForTextBox2 = new System.Windows.Forms.Button();
            this.btnShowForTextBox1 = new System.Windows.Forms.Button();
            this.inputGroup = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.actionGroup = new System.Windows.Forms.GroupBox();
            this.btnHideKeyboard = new System.Windows.Forms.Button();
            this.btnToggleLayout = new System.Windows.Forms.Button();
            this.btnToggleKeyboard = new System.Windows.Forms.Button();
            this.statusGroup = new System.Windows.Forms.GroupBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.controlGroup.SuspendLayout();
            this.inputGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.actionGroup.SuspendLayout();
            this.statusGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // descLabel
            // 
            this.descLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descLabel.AutoSize = true;
            this.descLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.descLabel.Location = new System.Drawing.Point(20, 16);
            this.descLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(303, 20);
            this.descLabel.TabIndex = 0;
            this.descLabel.Text = "虚拟键盘演示 - 支持全局输入和跨窗体焦点管理";
            // 
            // controlGroup
            // 
            this.controlGroup.Controls.Add(this.btnCrossFormTest);
            this.controlGroup.Controls.Add(this.btnShowForNumeric);
            this.controlGroup.Controls.Add(this.btnShowForTextBox2);
            this.controlGroup.Controls.Add(this.btnShowForTextBox1);
            this.controlGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlGroup.Location = new System.Drawing.Point(20, 174);
            this.controlGroup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.controlGroup.Name = "controlGroup";
            this.controlGroup.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.controlGroup.Size = new System.Drawing.Size(548, 97);
            this.controlGroup.TabIndex = 2;
            this.controlGroup.TabStop = false;
            this.controlGroup.Text = "为目标控件显示键盘";
            // 
            // btnCrossFormTest
            // 
            this.btnCrossFormTest.BackColor = System.Drawing.Color.White;
            this.btnCrossFormTest.FlatAppearance.BorderSize = 0;
            this.btnCrossFormTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrossFormTest.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCrossFormTest.ForeColor = System.Drawing.Color.Black;
            this.btnCrossFormTest.Location = new System.Drawing.Point(413, 37);
            this.btnCrossFormTest.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCrossFormTest.Name = "btnCrossFormTest";
            this.btnCrossFormTest.Size = new System.Drawing.Size(120, 30);
            this.btnCrossFormTest.TabIndex = 3;
            this.btnCrossFormTest.Text = "跨窗体输入测试";
            this.btnCrossFormTest.UseVisualStyleBackColor = false;
            this.btnCrossFormTest.Click += new System.EventHandler(this.btnCrossFormTest_Click);
            // 
            // btnShowForNumeric
            // 
            this.btnShowForNumeric.Location = new System.Drawing.Point(280, 37);
            this.btnShowForNumeric.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnShowForNumeric.Name = "btnShowForNumeric";
            this.btnShowForNumeric.Size = new System.Drawing.Size(120, 30);
            this.btnShowForNumeric.TabIndex = 2;
            this.btnShowForNumeric.Text = "为数字框显示键盘";
            this.btnShowForNumeric.UseVisualStyleBackColor = true;
            this.btnShowForNumeric.Click += new System.EventHandler(this.btnShowForNumeric_Click);
            // 
            // btnShowForTextBox2
            // 
            this.btnShowForTextBox2.Location = new System.Drawing.Point(149, 39);
            this.btnShowForTextBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnShowForTextBox2.Name = "btnShowForTextBox2";
            this.btnShowForTextBox2.Size = new System.Drawing.Size(120, 30);
            this.btnShowForTextBox2.TabIndex = 1;
            this.btnShowForTextBox2.Text = "为多行文本框显示键盘";
            this.btnShowForTextBox2.UseVisualStyleBackColor = true;
            this.btnShowForTextBox2.Click += new System.EventHandler(this.btnShowForTextBox2_Click);
            // 
            // btnShowForTextBox1
            // 
            this.btnShowForTextBox1.Location = new System.Drawing.Point(15, 39);
            this.btnShowForTextBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnShowForTextBox1.Name = "btnShowForTextBox1";
            this.btnShowForTextBox1.Size = new System.Drawing.Size(120, 30);
            this.btnShowForTextBox1.TabIndex = 0;
            this.btnShowForTextBox1.Text = "为文本框显示键盘";
            this.btnShowForTextBox1.UseVisualStyleBackColor = true;
            this.btnShowForTextBox1.Click += new System.EventHandler(this.btnShowForTextBox1_Click);
            // 
            // inputGroup
            // 
            this.inputGroup.Controls.Add(this.comboBox1);
            this.inputGroup.Controls.Add(this.numericUpDown1);
            this.inputGroup.Controls.Add(this.textBox2);
            this.inputGroup.Controls.Add(this.textBox1);
            this.inputGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputGroup.Location = new System.Drawing.Point(20, 48);
            this.inputGroup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inputGroup.Name = "inputGroup";
            this.inputGroup.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inputGroup.Size = new System.Drawing.Size(548, 112);
            this.inputGroup.TabIndex = 1;
            this.inputGroup.TabStop = false;
            this.inputGroup.Text = "输入控件（点击获得焦点）";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "选项1",
            "选项2",
            "选项3"});
            this.comboBox1.Location = new System.Drawing.Point(400, 56);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(128, 25);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.Enter += new System.EventHandler(this.comboBox1_Enter);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.numericUpDown1.Location = new System.Drawing.Point(268, 56);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(110, 25);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Enter += new System.EventHandler(this.numericUpDown1_Enter);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.textBox2.Location = new System.Drawing.Point(24, 72);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(220, 28);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "多行文本框";
            this.textBox2.Enter += new System.EventHandler(this.textBox2_Enter);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.textBox1.Location = new System.Drawing.Point(24, 34);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(220, 25);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "单行文本框";
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // actionGroup
            // 
            this.actionGroup.Controls.Add(this.btnHideKeyboard);
            this.actionGroup.Controls.Add(this.btnToggleLayout);
            this.actionGroup.Controls.Add(this.btnToggleKeyboard);
            this.actionGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actionGroup.Location = new System.Drawing.Point(20, 282);
            this.actionGroup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.actionGroup.Name = "actionGroup";
            this.actionGroup.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.actionGroup.Size = new System.Drawing.Size(548, 72);
            this.actionGroup.TabIndex = 3;
            this.actionGroup.TabStop = false;
            this.actionGroup.Text = "键盘控制";
            // 
            // btnHideKeyboard
            // 
            this.btnHideKeyboard.Location = new System.Drawing.Point(353, 18);
            this.btnHideKeyboard.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHideKeyboard.Name = "btnHideKeyboard";
            this.btnHideKeyboard.Size = new System.Drawing.Size(120, 30);
            this.btnHideKeyboard.TabIndex = 2;
            this.btnHideKeyboard.Text = "隐藏键盘";
            this.btnHideKeyboard.UseVisualStyleBackColor = true;
            this.btnHideKeyboard.Click += new System.EventHandler(this.btnHideKeyboard_Click);
            // 
            // btnToggleLayout
            // 
            this.btnToggleLayout.Location = new System.Drawing.Point(187, 18);
            this.btnToggleLayout.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnToggleLayout.Name = "btnToggleLayout";
            this.btnToggleLayout.Size = new System.Drawing.Size(120, 30);
            this.btnToggleLayout.TabIndex = 1;
            this.btnToggleLayout.Text = "切换布局";
            this.btnToggleLayout.UseVisualStyleBackColor = true;
            this.btnToggleLayout.Click += new System.EventHandler(this.btnToggleLayout_Click);
            // 
            // btnToggleKeyboard
            // 
            this.btnToggleKeyboard.Location = new System.Drawing.Point(40, 18);
            this.btnToggleKeyboard.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnToggleKeyboard.Name = "btnToggleKeyboard";
            this.btnToggleKeyboard.Size = new System.Drawing.Size(120, 30);
            this.btnToggleKeyboard.TabIndex = 0;
            this.btnToggleKeyboard.Text = "显示键盘";
            this.btnToggleKeyboard.UseVisualStyleBackColor = true;
            this.btnToggleKeyboard.Click += new System.EventHandler(this.btnToggleKeyboard_Click);
            // 
            // statusGroup
            // 
            this.statusGroup.Controls.Add(this.statusLabel);
            this.statusGroup.Controls.Add(this.infoLabel);
            this.statusGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusGroup.Location = new System.Drawing.Point(20, 364);
            this.statusGroup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.statusGroup.Name = "statusGroup";
            this.statusGroup.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.statusGroup.Size = new System.Drawing.Size(548, 72);
            this.statusGroup.TabIndex = 4;
            this.statusGroup.TabStop = false;
            this.statusGroup.Text = "状态信息";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(13, 23);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(77, 12);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "目标控件: 无";
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(13, 43);
            this.infoLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(125, 12);
            this.infoLabel.TabIndex = 1;
            this.infoLabel.Text = "点击输入控件获得焦点";
            // 
            // VirtualKeyboardPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.statusGroup);
            this.Controls.Add(this.actionGroup);
            this.Controls.Add(this.controlGroup);
            this.Controls.Add(this.inputGroup);
            this.Controls.Add(this.descLabel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "VirtualKeyboardPage";
            this.Size = new System.Drawing.Size(596, 456);
            this.controlGroup.ResumeLayout(false);
            this.inputGroup.ResumeLayout(false);
            this.inputGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.actionGroup.ResumeLayout(false);
            this.statusGroup.ResumeLayout(false);
            this.statusGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label descLabel;
        private System.Windows.Forms.GroupBox controlGroup;
        private System.Windows.Forms.Button btnShowForNumeric;
        private System.Windows.Forms.Button btnShowForTextBox2;
        private System.Windows.Forms.Button btnShowForTextBox1;
        private System.Windows.Forms.GroupBox inputGroup;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox actionGroup;
        private System.Windows.Forms.Button btnHideKeyboard;
        private System.Windows.Forms.Button btnToggleLayout;
        private System.Windows.Forms.Button btnToggleKeyboard;
        private System.Windows.Forms.Button btnCrossFormTest;
        private System.Windows.Forms.GroupBox statusGroup;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label infoLabel;
    }
}
