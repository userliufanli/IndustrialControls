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
    /// 数据输入面板，标签+输入+单位的标准化布局
    /// </summary>
    [ToolboxItem(true)]
    [DefaultProperty("LabelText")]
    public class DataInputPanel : BaseControl
    {
        private string _labelText = "参数名称";
        private string _unit = "";
        private Label _label;
        private TextBox _inputBox;
        private Label _unitLabel;
        private double _value;
        private double _minimum = double.MinValue;
        private double _maximum = double.MaxValue;
        private int _decimalPlaces = 2;

        /// <summary>
        /// 值变更事件
        /// </summary>
        public event EventHandler<double> ValueChanged;

        /// <summary>
        /// 标签文本
        /// </summary>
        [Category("外观")]
        [Description("参数标签文本")]
        [DefaultValue("参数名称")]
        public string LabelText
        {
            get => _labelText;
            set
            {
                _labelText = value ?? "";
                if (_label != null) _label.Text = _labelText;
            }
        }

        /// <summary>
        /// 单位
        /// </summary>
        [Category("外观")]
        [Description("数值单位")]
        [DefaultValue("")]
        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value ?? "";
                if (_unitLabel != null)
                {
                    _unitLabel.Text = _unit;
                    _unitLabel.Visible = !string.IsNullOrEmpty(_unit);
                }
                LayoutControls();
            }
        }

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
                double newVal = ValidationHelper.Clamp(value, _minimum, _maximum);
                bool valueChanged = Math.Abs(_value - newVal) > double.Epsilon;
                _value = newVal;
                UpdateDisplay();
                if (valueChanged)
                    ValueChanged?.Invoke(this, _value);
            }
        }

        [Category("数据")]
        [Description("允许输入的最小值")]
        [DefaultValue(double.MinValue)]
        public double Minimum { get => _minimum; set { _minimum = value; } }

        [Category("数据")]
        [Description("允许输入的最大值")]
        [DefaultValue(double.MaxValue)]
        public double Maximum { get => _maximum; set { _maximum = value; } }

        [Category("数据")]
        [Description("数值显示的小数位数")]
        [DefaultValue(2)]
        public int DecimalPlaces { get => _decimalPlaces; set { _decimalPlaces = Math.Max(0, value); UpdateDisplay(); } }

        public DataInputPanel()
        {
            Size = new Size(280, 36);
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
            _label = new Label
            {
                Text = _labelText,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false
            };

            _inputBox = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = HorizontalAlignment.Right,
                Text = "0.00"
            };
            _inputBox.Leave += InputBox_Leave;
            _inputBox.KeyPress += InputBox_KeyPress;

            _unitLabel = new Label
            {
                Text = _unit,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false,
                Visible = !string.IsNullOrEmpty(_unit)
            };

            Controls.Add(_label);
            Controls.Add(_inputBox);
            Controls.Add(_unitLabel);

            LayoutControls();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            LayoutControls();
        }

        private void LayoutControls()
        {
            if (_label == null) return;

            int labelWidth = Width / 3;
            int padding = 4;
            int unitMinWidth = 50; // 单位标签最小宽度，确保布局一致性
            
            // 根据实际文本宽度计算单位标签宽度（至少为最小宽度）
            int unitWidth = 0;
            if (!string.IsNullOrEmpty(_unit) && _unitLabel != null)
            {
                // 使用 TextRenderer.MeasureText 更可靠，支持设计器模式
                Font unitFont = _unitLabel.Font ?? this.Font ?? Control.DefaultFont;
                Size textSize = TextRenderer.MeasureText(_unit, unitFont);
                unitWidth = Math.Max(unitMinWidth, textSize.Width + padding);
                
                // 确保单位标签可见
                _unitLabel.Visible = true;
            }
            else
            {
                _unitLabel.Visible = false;
            }
            
            int inputWidth = Width - labelWidth - unitWidth - padding * 2;

            _label.SetBounds(0, 0, labelWidth, Height);
            _inputBox.SetBounds(labelWidth + padding, (Height - _inputBox.PreferredHeight) / 2, inputWidth, _inputBox.PreferredHeight);
            _unitLabel.SetBounds(labelWidth + padding + inputWidth, 0, unitWidth, Height);
        }

        protected override void ApplyTheme()
        {
            base.ApplyTheme();
            if (_label != null)
            {
                _label.ForeColor = ForeColor;
                _inputBox.BackColor = CurrentTheme.Colors.Surface;
                _inputBox.ForeColor = ForeColor;
                _unitLabel.ForeColor = CurrentTheme.Colors.TextSecondary;
            }
            SetThemeBackColor(CurrentTheme.Colors.Background);
            LayoutControls();
        }

        private void InputBox_Leave(object sender, EventArgs e)
        {
            if (double.TryParse(_inputBox.Text, out double val))
            {
                Value = val;
            }
            else
            {
                UpdateDisplay();
            }
        }

        private void InputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-' && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void UpdateDisplay()
        {
            if (_inputBox != null && !_inputBox.Focused)
            {
                _inputBox.Text = ValidationHelper.FormatNumber(_value, _decimalPlaces);
            }
        }

        /// <summary>
        /// 控件加载时验证初始值
        /// </summary>
        private void ValidateOnLoad()
        {
            // 验证初始值是否在有效范围内
            double clampedValue = ValidationHelper.Clamp(_value, _minimum, _maximum);
            if (Math.Abs(_value - clampedValue) > double.Epsilon)
            {
                // 初始值超出范围，自动调整
                _value = clampedValue;
                UpdateDisplay();
            }
        }
    }
}
