using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Theme;
using IndustrialControls.Utilities;

namespace IndustrialControls.Controls.DataInput
{
    /// <summary>
    /// 数值输入框控件，带上下限验证、步进按钮、单位显示
    /// </summary>
    [ToolboxItem(true)]
    [DefaultProperty("Value")]
    [DefaultEvent("ValueChanged")]
    public class NumericInputBox : BaseControl
    {
        private double _value;
        private double _minimum;
        private double _maximum = 100;
        private double _step = 1;
        private int _decimalPlaces = 2;
        private string _unit = "";
        private bool _showButtons = true;
        private TextBox _textBox;
        private Button _btnUp;
        private Button _btnDown;
        private Label _lblUnit;
        private bool _isValid = true;

        /// <summary>
        /// 值变更事件
        /// </summary>
        public event EventHandler<double> ValueChanged;

        /// <summary>
        /// 验证失败事件
        /// </summary>
        public event EventHandler<string> ValidationFailed;

        /// <summary>
        /// 当前值
        /// </summary>
        [Category("数据")]
        [Description("当前数值")]
        [DefaultValue(0.0)]
        public double Value
        {
            get => _value;
            set
            {
                double newValue = ValidationHelper.Clamp(value, _minimum, _maximum);
                bool valueChanged = Math.Abs(_value - newValue) > double.Epsilon;
                _value = newValue;
                UpdateDisplay();
                if (valueChanged)
                    ValueChanged?.Invoke(this, _value);
            }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        [Category("数据")]
        [Description("最小值")]
        [DefaultValue(0.0)]
        public double Minimum
        {
            get => _minimum;
            set { _minimum = value; ValidateCurrentValue(); }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        [Category("数据")]
        [Description("最大值")]
        [DefaultValue(100.0)]
        public double Maximum
        {
            get => _maximum;
            set { _maximum = value; ValidateCurrentValue(); }
        }

        /// <summary>
        /// 步进值
        /// </summary>
        [Category("数据")]
        [Description("步进值")]
        [DefaultValue(1.0)]
        public double Step
        {
            get => _step;
            set => _step = Math.Max(0.001, value);
        }

        /// <summary>
        /// 小数位数
        /// </summary>
        [Category("数据")]
        [Description("小数位数")]
        [DefaultValue(2)]
        public int DecimalPlaces
        {
            get => _decimalPlaces;
            set { _decimalPlaces = Math.Max(0, value); UpdateDisplay(); }
        }

        /// <summary>
        /// 单位
        /// </summary>
        [Category("外观")]
        [Description("数值输入框显示的单位文本")]
        [DefaultValue("")]
        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value ?? "";
                if (_lblUnit != null)
                {
                    _lblUnit.Text = _unit;
                    _lblUnit.Visible = !string.IsNullOrEmpty(_unit);
                }
                LayoutControls();
            }
        }

        /// <summary>
        /// 是否显示步进按钮
        /// </summary>
        [Category("外观")]
        [Description("是否显示上下步进按钮")]
        [DefaultValue(true)]
        public bool ShowButtons
        {
            get => _showButtons;
            set { _showButtons = value; LayoutControls(); }
        }

        /// <summary>
        /// 当前输入是否有效
        /// </summary>
        [Browsable(false)]
        public bool IsValid => _isValid;

        public NumericInputBox()
        {
            Size = new Size(180, 32);
            InitializeComponents();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            // 在设计器模式下，当父控件变化时重新布局
            if (IsDesignMode)
            {
                LayoutControls();
                Invalidate();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            ApplyTheme();
            
            // 控件加载完成后验证初始值
            if (!IsDesignMode)
            {
                ValidateOnLoad();
            }
        }

        private void InitializeComponents()
        {
            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                TextAlign = HorizontalAlignment.Right,
                Text = "0.00"
            };
            _textBox.KeyPress += TextBox_KeyPress;
            _textBox.Leave += TextBox_Leave;
            _textBox.TextChanged += TextBox_TextChanged;

            _btnUp = new Button { Text = "+", FlatStyle = FlatStyle.Flat, TabStop = false };
            _btnUp.FlatAppearance.BorderSize = 0;
            _btnUp.Click += (s, e) => Value += _step;

            _btnDown = new Button { Text = "-", FlatStyle = FlatStyle.Flat, TabStop = false };
            _btnDown.FlatAppearance.BorderSize = 0;
            _btnDown.Click += (s, e) => Value -= _step;

            _lblUnit = new Label
            {
                Text = _unit,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false,
                Visible = !string.IsNullOrEmpty(_unit)
            };

            Controls.Add(_textBox);
            Controls.Add(_btnUp);
            Controls.Add(_btnDown);
            Controls.Add(_lblUnit);

            LayoutControls();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            LayoutControls();
        }

        private void LayoutControls()
        {
            if (_textBox == null) return;

            int btnWidth = _showButtons ? 28 : 0;
            int padding = 4;
            int unitMinWidth = 50; // 单位标签最小宽度，确保布局一致性
            
            // 根据实际文本宽度计算单位标签宽度（至少为最小宽度）
            int unitWidth = 0;
            if (!string.IsNullOrEmpty(_unit) && _lblUnit != null)
            {
                // 使用 TextRenderer.MeasureText 更可靠，支持设计器模式
                Font unitFont = _lblUnit.Font ?? this.Font ?? Control.DefaultFont;
                Size textSize = TextRenderer.MeasureText(_unit, unitFont);
                unitWidth = Math.Max(unitMinWidth, textSize.Width + padding);
                
                // 确保单位标签可见
                _lblUnit.Visible = true;
            }
            else
            {
                _lblUnit.Visible = false;
            }

            _btnDown.Visible = _showButtons;
            _btnUp.Visible = _showButtons;

            int textWidth = Width - btnWidth * 2 - unitWidth - padding * 2;

            _btnDown.SetBounds(padding, 2, btnWidth, Height - 4);
            _textBox.SetBounds(padding + btnWidth, (Height - _textBox.PreferredHeight) / 2, textWidth, _textBox.PreferredHeight);
            _lblUnit.SetBounds(padding + btnWidth + textWidth, 2, unitWidth, Height - 4);
            _btnUp.SetBounds(Width - btnWidth - padding, 2, btnWidth, Height - 4);
        }

        protected override void ApplyTheme()
        {
            base.ApplyTheme();
            if (_textBox != null)
            {
                _textBox.BackColor = CurrentTheme.Colors.Surface;
                _textBox.ForeColor = ForeColor;
            }
            if (_btnUp != null)
            {
                _btnUp.BackColor = CurrentTheme.Colors.SurfaceVariant;
                _btnUp.ForeColor = ForeColor;
                _btnDown.BackColor = CurrentTheme.Colors.SurfaceVariant;
                _btnDown.ForeColor = ForeColor;
            }
            if (_lblUnit != null)
            {
                _lblUnit.ForeColor = CurrentTheme.Colors.TextSecondary;
            }
            SetThemeBackColor(CurrentTheme.Colors.Surface);
            LayoutControls();
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 允许数字、小数点、负号和控制字符
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (double.TryParse(_textBox.Text, out double val))
            {
                Value = val;
            }
            else
            {
                UpdateDisplay();
                _isValid = false;
                ValidationFailed?.Invoke(this, "输入的值不是有效数字");
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            _isValid = double.TryParse(_textBox.Text, out _);
            var theme = CurrentTheme;
            _textBox.ForeColor = _isValid ? theme.Colors.TextPrimary : theme.Colors.Danger;
        }

        private void UpdateDisplay()
        {
            if (_textBox != null && !_textBox.Focused)
            {
                _textBox.Text = ValidationHelper.FormatNumber(_value, _decimalPlaces);
            }
        }

        private void ValidateCurrentValue()
        {
            Value = _value; // 触发Clamp
        }

        /// <summary>
        /// 控件加载时验证初始值
        /// </summary>
        private void ValidateOnLoad()
        {
            // 验证当前值是否在有效范围内
            double clampedValue = ValidationHelper.Clamp(_value, _minimum, _maximum);
            if (Math.Abs(_value - clampedValue) > double.Epsilon)
            {
                // 初始值被钳位，说明超出范围
                _value = clampedValue;
                UpdateDisplay();
                _isValid = false;
                string formattedValue = _value.ToString("F" + _decimalPlaces);
                ValidationFailed?.Invoke(this, $"初始值已调整为范围内的值: {formattedValue}");
            }
            else
            {
                _isValid = true;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var theme = CurrentTheme;
            // 绘制边框
            var borderColor = _textBox != null && _textBox.Focused
                ? theme.Colors.BorderFocus
                : (_isValid ? theme.Colors.Border : theme.Colors.Danger);

            using (var pen = new Pen(borderColor, theme.BorderWidth))
            {
                var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                GraphicsHelper.DrawRoundedRectangle(e.Graphics, pen, rect, CornerRadius);
            }
        }
    }
}
