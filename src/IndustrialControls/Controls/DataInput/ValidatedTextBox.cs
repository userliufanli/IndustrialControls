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
    /// 验证模式
    /// </summary>
    public enum ValidationMode
    {
        /// <summary>实时验证</summary>
        Realtime,
        /// <summary>失焦验证</summary>
        OnLeave
    }

    /// <summary>
    /// 验证预设类型转换器（用于设计器下拉列表显示）
    /// </summary>
    public class ValidationPresetConverter : EnumConverter
    {
        public ValidationPresetConverter() : base(typeof(ValidationPreset)) { }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is ValidationPreset)
            {
                ValidationPreset preset = (ValidationPreset)value;
                switch (preset)
                {
                    case ValidationPreset.None: return "无（自定义验证）";
                    case ValidationPreset.Email: return "邮箱地址";
                    case ValidationPreset.MobilePhone: return "手机号码";
                    case ValidationPreset.IdCard: return "身份证号码";
                    case ValidationPreset.Url: return "URL地址";
                    case ValidationPreset.Number: return "纯数字";
                    case ValidationPreset.StrongPassword: return "强密码";
                    case ValidationPreset.IpAddress: return "IP地址";
                    case ValidationPreset.ChineseName: return "中文姓名";
                    case ValidationPreset.PostalCode: return "邮政编码";
                    default: return base.ConvertTo(context, culture, value, destinationType);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                string text = (string)value;
                switch (text)
                {
                    case "无（自定义验证）": return ValidationPreset.None;
                    case "邮箱地址": return ValidationPreset.Email;
                    case "手机号码": return ValidationPreset.MobilePhone;
                    case "身份证号码": return ValidationPreset.IdCard;
                    case "URL地址": return ValidationPreset.Url;
                    case "纯数字": return ValidationPreset.Number;
                    case "强密码": return ValidationPreset.StrongPassword;
                    case "IP地址": return ValidationPreset.IpAddress;
                    case "中文姓名": return ValidationPreset.ChineseName;
                    case "邮政编码": return ValidationPreset.PostalCode;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }

    /// <summary>
    /// 带验证的文本输入框
    /// </summary>
    [ToolboxItem(true)]
    [DefaultProperty("Text")]
    public class ValidatedTextBox : BaseControl
    {
        private TextBox _textBox;
        private Label _errorLabel;
        private string _validationPattern = "";
        private bool _required;
        private bool _requiredExplicitlySet = false; // 标记 Required 是否被显式设置
        private ValidationMode _validationMode = ValidationMode.OnLeave;
        private string _errorMessage = "";
        private bool _isValid = true;
        private string _placeholder = "";
        private ValidationPreset _validationPreset = ValidationPreset.None;

        /// <summary>
        /// 验证状态变更事件
        /// </summary>
        public event EventHandler<bool> ValidationChanged;

        /// <summary>
        /// 文本内容
        /// </summary>
        [Category("数据")]
        [Description("文本内容")]
        public override string Text
        {
            get => _textBox?.Text ?? "";
            set { if (_textBox != null) _textBox.Text = value; }
        }

        /// <summary>
        /// 正则验证模式
        /// </summary>
        [Category("验证")]
        [Description("正则表达式验证模式")]
        [DefaultValue("")]
        public string ValidationPattern
        {
            get => _validationPattern;
            set => _validationPattern = value ?? "";
        }

        /// <summary>
        /// 是否必填
        /// </summary>
        [Category("验证")]
        [Description("是否为必填项")]
        [DefaultValue(false)]
        public bool Required
        {
            get => _required;
            set
            {
                _required = value;
                _requiredExplicitlySet = true; // 标记为已显式设置
            }
        }

        /// <summary>
        /// 验证模式
        /// </summary>
        [Category("验证")]
        [Description("验证触发模式")]
        [DefaultValue(ValidationMode.OnLeave)]
        public ValidationMode Mode
        {
            get => _validationMode;
            set => _validationMode = value;
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        [Category("验证")]
        [Description("验证失败时的错误消息")]
        [DefaultValue("")]
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value ?? "";
                if (_errorLabel != null) _errorLabel.Text = _errorMessage;
            }
        }

        /// <summary>
        /// 占位文本
        /// </summary>
        [Category("外观")]
        [Description("输入框占位提示文本")]
        [DefaultValue("")]
        public string Placeholder
        {
            get => _placeholder;
            set => _placeholder = value ?? "";
        }

        /// <summary>
        /// 验证预设类型
        /// </summary>
        [Category("验证")]
        [Description("常用验证预设类型（选择后自动配置正则、错误消息和占位符）")]
        [DefaultValue(ValidationPreset.None)]
        [TypeConverter(typeof(ValidationPresetConverter))]
        public ValidationPreset Preset
        {
            get => _validationPreset;
            set
            {
                _validationPreset = value;
                if (value != ValidationPreset.None)
                {
                    // 应用预设配置
                    ApplyPreset(value);
                }
                else
                {
                    // 选择"无"时清空预设配置
                    ClearPreset();
                }
            }
        }

        /// <summary>
        /// 是否验证通过
        /// </summary>
        [Browsable(false)]
        public bool IsValid => _isValid;

        public ValidatedTextBox()
        {
            Size = new Size(200, 48);
            InitializeComponents();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            ApplyTheme();

            // 控件加载完成后重新应用设计器配置
            if (!IsDesignMode)
            {
                // 重新应用预设配置（设计器属性在构造函数后设置）
                if (_validationPreset != ValidationPreset.None)
                {
                    ApplyPreset(_validationPreset);
                }
                else
                {
                    // 如果设计器选择了"无"，也要确保清空
                    ClearPreset();
                }
                
                // 验证初始值
                ValidateOnLoad();
            }
        }

        private void InitializeComponents()
        {
            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Dock = DockStyle.None
            };
            _textBox.TextChanged += OnTextBoxTextChanged;
            _textBox.Leave += OnTextBoxLeave;
            _textBox.Enter += OnTextBoxEnter;

            _errorLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.None,
                ForeColor = Color.Red,
                Visible = false
            };

            Controls.Add(_textBox);
            Controls.Add(_errorLabel);

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
            int padding = 6;
            _textBox.SetBounds(padding, padding, Width - padding * 2, _textBox.PreferredHeight);
            _errorLabel.SetBounds(padding, _textBox.Bottom + 2, Width - padding * 2, 16);
        }

        protected override void ApplyTheme()
        {
            base.ApplyTheme();
            if (_textBox != null)
            {
                _textBox.BackColor = CurrentTheme.Colors.Surface;
                _textBox.ForeColor = ForeColor;
            }
            if (_errorLabel != null)
            {
                _errorLabel.ForeColor = CurrentTheme.Colors.Danger;
            }
            SetThemeBackColor(CurrentTheme.Colors.Surface);
            LayoutControls();
        }

        private void OnTextBoxTextChanged(object sender, EventArgs e)
        {
            if (_validationMode == ValidationMode.Realtime)
            {
                Validate();
            }
        }

        private void OnTextBoxLeave(object sender, EventArgs e)
        {
            if (_validationMode == ValidationMode.OnLeave)
            {
                Validate();
            }
            Invalidate();
        }

        private void OnTextBoxEnter(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// 执行验证
        /// </summary>
        public bool Validate()
        {
            bool valid = true;
            string error = "";

            if (_required && !ValidationHelper.ValidateRequired(_textBox.Text))
            {
                valid = false;
                error = string.IsNullOrEmpty(_errorMessage) ? "此项为必填" : _errorMessage;
            }
            else if (!string.IsNullOrEmpty(_validationPattern) && !string.IsNullOrEmpty(_textBox.Text))
            {
                if (!ValidationHelper.ValidateRegex(_textBox.Text, _validationPattern))
                {
                    valid = false;
                    error = string.IsNullOrEmpty(_errorMessage) ? "格式不正确" : _errorMessage;
                }
            }

            _isValid = valid;
            _errorLabel.Text = error;
            _errorLabel.Visible = !valid;
            ValidationChanged?.Invoke(this, valid);
            Invalidate();
            return valid;
        }

        /// <summary>
        /// 控件加载时验证初始值
        /// </summary>
        private void ValidateOnLoad()
        {
            // 加载时立即执行验证，确保初始值符合验证规则
            Validate();
        }

        /// <summary>
        /// 应用验证预设
        /// </summary>
        /// <param name="preset">验证预设类型</param>
        private void ApplyPreset(ValidationPreset preset)
        {
            var config = ValidationPresets.GetConfig(preset);

            _validationPattern = config.Pattern;
            _errorMessage = config.ErrorMessage;
            _placeholder = config.Placeholder;
            
            // 只在设计器未显式设置 Required 时才应用预设的 Required
            // 通过检查 DesignerSerializationVisibility 来判断
            if (!_requiredExplicitlySet)
            {
                _required = config.Required;
            }

            // 更新控件显示
            if (_errorLabel != null)
            {
                _errorLabel.Text = _errorMessage;
            }

            // 重新验证
            if (!IsDesignMode)
            {
                Validate();
            }
        }

        /// <summary>
        /// 清空验证预设配置
        /// </summary>
        private void ClearPreset()
        {
            _validationPattern = "";
            _errorMessage = "";
            _placeholder = "";
            
            // 不修改 _required，尊重设计器的设置

            // 更新控件显示
            if (_errorLabel != null)
            {
                _errorLabel.Text = "";
                _errorLabel.Visible = false;
            }

            // 重新验证（清空后应该通过验证）
            if (!IsDesignMode)
            {
                _isValid = true;
                Validate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var theme = CurrentTheme;
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
