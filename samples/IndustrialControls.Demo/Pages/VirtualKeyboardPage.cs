using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using IndustrialControls.Controls.VirtualKeyboard;
using IndustrialControls.Demo;

namespace IndustrialControls.Demo.Pages
{
    public partial class VirtualKeyboardPage : UserControl
    {
        private VirtualKeyboardForm _keyboardForm;
        private Control _targetControl;

        public VirtualKeyboardPage()
        {
            InitializeComponent();
            DemoUiChrome.ApplyPage(this);
            InitializeKeyboard();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (_keyboardForm != null)
            {
                _keyboardForm.KeyInput -= OnKeyInput;
            }
            base.OnHandleDestroyed(e);
        }

        private void InitializeKeyboard()
        {
            _keyboardForm = new VirtualKeyboardForm();
            _keyboardForm.KeyInput += OnKeyInput;
            UpdateStatus();
        }

        private void OnKeyInput(object sender, VirtualKeyboardKeyInputEventArgs e)
        {
            Control targetControl = e.Target;
            string key = e.Key;

            if (targetControl == null || targetControl.IsDisposed)
            {
                System.Diagnostics.Debug.WriteLine("[OnKeyInput] 警告: 未找到有效目标控件");
                return;
            }

            switch (targetControl)
            {
                case TextBox textBox:
                    HandleTextBoxInput(textBox, key);
                    break;
                case NumericUpDown numericUpDown:
                    HandleNumericUpDownInput(numericUpDown, key);
                    break;
                case ComboBox comboBox:
                    HandleComboBoxInput(comboBox, key);
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine($"[OnKeyInput] 不支持的控件类型: {targetControl.GetType().Name}");
                    break;
            }
        }

        private void HandleTextBoxInput(TextBox textBox, string key)
        {
            try
            {
                if (textBox == null || textBox.IsDisposed)
                    return;

                switch (key)
                {
                    case "Backspace":
                        if (textBox.SelectionLength > 0)
                        {
                            int start = textBox.SelectionStart;
                            textBox.Text = textBox.Text.Remove(start, textBox.SelectionLength);
                            textBox.SelectionStart = start;
                        }
                        else if (textBox.SelectionStart > 0)
                        {
                            int pos = textBox.SelectionStart - 1;
                            textBox.Text = textBox.Text.Remove(pos, 1);
                            textBox.SelectionStart = pos;
                        }
                        break;

                    case "Enter":
                        if (textBox.Multiline)
                        {
                            int pos = textBox.SelectionStart;
                            textBox.Text = textBox.Text.Insert(pos, Environment.NewLine);
                            textBox.SelectionStart = pos + Environment.NewLine.Length;
                        }
                        break;

                    case "Space":
                        {
                            int pos = textBox.SelectionStart;
                            textBox.Text = textBox.Text.Insert(pos, " ");
                            textBox.SelectionStart = pos + 1;
                        }
                        break;

                    case "Tab":
                        if (textBox.Multiline)
                        {
                            int pos = textBox.SelectionStart;
                            textBox.Text = textBox.Text.Insert(pos, "\t");
                            textBox.SelectionStart = pos + 1;
                        }
                        break;

                    case "+/-":
                        HandlePlusMinus(textBox);
                        break;

                    default:
                        {
                            int pos = textBox.SelectionStart;
                            textBox.Text = textBox.Text.Insert(pos, key);
                            textBox.SelectionStart = pos + key.Length;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[TextBox] 输入错误: {ex.Message}");
            }
        }

        private void HandlePlusMinus(TextBox textBox)
        {
            string text = textBox.Text.Trim();
            if (text.StartsWith("-", StringComparison.Ordinal))
            {
                textBox.Text = text.Substring(1);
            }
            else if (!string.IsNullOrEmpty(text))
            {
                textBox.Text = "-" + text;
            }
        }

        private void HandleNumericUpDownInput(NumericUpDown numericUpDown, string key)
        {
            try
            {
                if (numericUpDown == null || numericUpDown.IsDisposed || !numericUpDown.Enabled)
                    return;

                switch (key)
                {
                    case "Backspace":
                        if (numericUpDown.Value > numericUpDown.Minimum)
                        {
                            decimal decrement = numericUpDown.DecimalPlaces > 0
                                ? 1m / (decimal)Math.Pow(10, numericUpDown.DecimalPlaces)
                                : 1m;
                            numericUpDown.Value = Math.Max(numericUpDown.Minimum, numericUpDown.Value - decrement);
                        }
                        break;

                    case "+/-":
                        decimal neg = -numericUpDown.Value;
                        if (neg >= numericUpDown.Minimum && neg <= numericUpDown.Maximum)
                            numericUpDown.Value = neg;
                        break;

                    default:
                        if (key.Length == 1 && (char.IsDigit(key[0]) || key[0] == '.' || key[0] == '-' || key[0] == '+'))
                        {
                            string edit = numericUpDown.Text;
                            if (numericUpDown.Hexadecimal)
                                return;

                            string next = edit + key;
                            if (decimal.TryParse(next, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal parsed) &&
                                parsed >= numericUpDown.Minimum && parsed <= numericUpDown.Maximum)
                            {
                                numericUpDown.Value = parsed;
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"NumericUpDown 输入错误: {ex.Message}");
            }
        }

        private void HandleComboBoxInput(ComboBox comboBox, string key)
        {
            try
            {
                if (comboBox == null || comboBox.IsDisposed || !comboBox.Enabled)
                    return;

                if (comboBox.DropDownStyle != ComboBoxStyle.DropDown)
                    return;

                Form parentForm = comboBox.FindForm();
                if (parentForm == null || !parentForm.Visible)
                    return;

                switch (key)
                {
                    case "Backspace":
                        if (comboBox.SelectionLength > 0)
                        {
                            int start = comboBox.SelectionStart;
                            comboBox.Text = comboBox.Text.Remove(start, comboBox.SelectionLength);
                            comboBox.SelectionStart = start;
                        }
                        else if (comboBox.SelectionStart > 0 && comboBox.Text.Length > 0)
                        {
                            int pos = comboBox.SelectionStart - 1;
                            comboBox.Text = comboBox.Text.Remove(pos, 1);
                            comboBox.SelectionStart = pos;
                        }
                        break;

                    case "Space":
                        {
                            int pos = comboBox.SelectionStart;
                            comboBox.Text = comboBox.Text.Insert(pos, " ");
                            comboBox.SelectionStart = pos + 1;
                        }
                        break;

                    default:
                        if (!string.IsNullOrEmpty(key))
                        {
                            int pos = comboBox.SelectionStart;
                            comboBox.Text = comboBox.Text.Insert(pos, key);
                            comboBox.SelectionStart = pos + key.Length;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ComboBox] 输入错误: {ex.Message}");
            }
        }

        private void UpdateStatus()
        {
            string targetInfo = _targetControl != null ? $"目标: {_targetControl.Name}" : "无目标";

            VirtualKeyboardPanel panel = _keyboardForm?.KeyboardPanel;
            if (panel != null)
            {
                string layoutMode = panel.LayoutMode.ToString();
                statusLabel.Text = $"{targetInfo} | 布局: {layoutMode} | 键盘: {(_keyboardForm.Visible ? "显示" : "隐藏")}";
            }
        }

        #region 按钮事件

        private void btnToggleKeyboard_Click(object sender, EventArgs e)
        {
            if (_keyboardForm.Visible)
            {
                _keyboardForm.Hide();
            }
            else
            {
                Point screen = PointToScreen(new Point(100, 200));
                _keyboardForm.ShowAt(screen);
            }
            UpdateStatus();
            btnToggleKeyboard.Text = _keyboardForm.Visible ? "隐藏键盘" : "显示键盘";
        }

        private void btnToggleLayout_Click(object sender, EventArgs e)
        {
            VirtualKeyboardPanel panel = _keyboardForm.KeyboardPanel;
            if (panel != null)
            {
                panel.LayoutMode = panel.LayoutMode == KeyboardLayoutMode.QWERTY
                    ? KeyboardLayoutMode.NumberPad
                    : KeyboardLayoutMode.QWERTY;
                UpdateStatus();
            }
        }

        private void btnShowForTextBox1_Click(object sender, EventArgs e)
        {
            _targetControl = textBox1;
            _keyboardForm.SetTargetControl(textBox1);
            Point p = textBox1.PointToScreen(new Point(0, textBox1.Height + 4));
            _keyboardForm.ShowAt(p);
            textBox1.Focus();
            UpdateStatus();
        }

        private void btnShowForTextBox2_Click(object sender, EventArgs e)
        {
            _targetControl = textBox2;
            _keyboardForm.SetTargetControl(textBox2);
            Point p = textBox2.PointToScreen(new Point(0, textBox2.Height + 4));
            _keyboardForm.ShowAt(p);
            textBox2.Focus();
            UpdateStatus();
        }

        private void btnShowForNumeric_Click(object sender, EventArgs e)
        {
            VirtualKeyboardPanel panel = _keyboardForm.KeyboardPanel;
            if (panel != null)
                panel.LayoutMode = KeyboardLayoutMode.NumberPad;

            _targetControl = numericUpDown1;
            _keyboardForm.SetTargetControl(numericUpDown1);
            Point p = numericUpDown1.PointToScreen(new Point(0, numericUpDown1.Height + 4));
            _keyboardForm.ShowAt(p);
            numericUpDown1.Focus();
            UpdateStatus();
        }

        private void btnHideKeyboard_Click(object sender, EventArgs e)
        {
            _keyboardForm.Hide();
            UpdateStatus();
        }

        private void btnCrossFormTest_Click(object sender, EventArgs e)
        {
            var testForm = new CrossFormTestForm(_keyboardForm);
            testForm.Show(this);
        }

        #endregion

        #region 控件焦点事件

        private void textBox1_Enter(object sender, EventArgs e)
        {
            _targetControl = textBox1;
            _keyboardForm.SetTargetControl(textBox1);
            infoLabel.Text = "当前焦点: textBox1 - 标准文本输入框";
            UpdateStatus();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            _targetControl = textBox2;
            _keyboardForm.SetTargetControl(textBox2);
            infoLabel.Text = "当前焦点: textBox2 - 多行文本框";
            UpdateStatus();
        }

        private void numericUpDown1_Enter(object sender, EventArgs e)
        {
            _targetControl = numericUpDown1;
            _keyboardForm.SetTargetControl(numericUpDown1);
            infoLabel.Text = "当前焦点: numericUpDown1 - 数字输入框（建议切换到数字键盘）";
            UpdateStatus();
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            _targetControl = comboBox1;
            _keyboardForm.SetTargetControl(comboBox1);
            infoLabel.Text = "当前焦点: comboBox1 - 可编辑下拉框";
            UpdateStatus();
        }

        #endregion
    }
}
