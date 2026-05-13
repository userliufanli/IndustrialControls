using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Windows.Forms;
using IndustrialControls.Theme;

namespace IndustrialControls.Core
{
    /// <summary>
    /// 为抽象控件基类提供设计时具体实例的TypeDescriptionProvider。
    /// 解决VS设计器无法实例化抽象类的问题。
    /// </summary>
    /// <typeparam name="TAbstract">抽象基类类型</typeparam>
    /// <typeparam name="TDesignTime">设计时具体替代类型</typeparam>
    public class AbstractControlDescriptionProvider<TAbstract, TDesignTime> : TypeDescriptionProvider
    {
        public AbstractControlDescriptionProvider()
            : base(TypeDescriptor.GetProvider(typeof(TAbstract)))
        {
        }

        public override Type GetReflectionType(Type objectType, object instance)
        {
            if (objectType == typeof(TAbstract))
                return typeof(TDesignTime);
            return base.GetReflectionType(objectType, instance);
        }

        public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
        {
            if (objectType == typeof(TAbstract))
                objectType = typeof(TDesignTime);
            return base.CreateInstance(provider, objectType, argTypes, args);
        }
    }

    /// <summary>
    /// 所有工业控件的抽象基类，提供主题订阅、Dispose模式和通用属性。
    /// 支持Visual Studio设计器安全加载和渲染。
    /// </summary>
    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<BaseControl, DesignTimeBaseControl>))]
    public class BaseControl : Control, IDisposable
    {
        private bool _disposed;
        private bool _isHovered;
        private bool _isPressed;
        private static readonly ITheme _designTimeFallbackTheme = new FlatLightTheme();

        /// <summary>
        /// 获取当前控件是否处于设计模式
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected bool IsDesignMode => ControlDesignerHelper.IsDesignMode(this);

        /// <summary>
        /// 获取当前是否鼠标悬停状态
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsHovered
        {
            get => _isHovered;
            private set
            {
                if (_isHovered != value)
                {
                    _isHovered = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取当前是否按下状态
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPressed
        {
            get => _isPressed;
            private set
            {
                if (_isPressed != value)
                {
                    _isPressed = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取当前主题（设计时使用默认浅色主题回退）
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected ITheme CurrentTheme
        {
            get
            {
                try
                {
                    if (ControlDesignerHelper.IsDesignMode(this))
                        return _designTimeFallbackTheme;
                    return ThemeManager.Instance.CurrentTheme;
                }
                catch
                {
                    return _designTimeFallbackTheme;
                }
            }
        }

        private Color _textColor = Color.Empty;
        private bool _backColorOverridden = false;
        private bool _applyingThemeColors = false;

        /// <summary>
        /// 自定义字体颜色。设置后立即生效，切换主题时自动清除并跟随新主题。
        /// 设为 Color.Empty 则跟随主题。
        /// </summary>
        [Category("外观")]
        [Description("自定义字体颜色，切换主题时自动清除并跟随新主题")]
        public Color TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                ForeColor = _textColor != Color.Empty ? _textColor : CurrentTheme.Colors.TextPrimary;
                Invalidate();
            }
        }

        private bool ShouldSerializeTextColor() => _textColor != Color.Empty;
        private void ResetTextColor() => TextColor = Color.Empty;

        /// <summary>
        /// 由主题系统设置背景色，仅在用户未自定义 BackColor 时生效。
        /// 子类在 ApplyTheme 中应使用此方法而非直接设置 BackColor。
        /// </summary>
        protected void SetThemeBackColor(Color color)
        {
            if (_backColorOverridden) return;
            _applyingThemeColors = true;
            BackColor = color;
            _applyingThemeColors = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRegion();
        }

        /// <summary>
        /// 根据 CornerRadius 更新控件的裁剪区域（实现真正圆角）。
        /// 子类重写此方法可自定义裁剪逻辑。
        /// </summary>
        protected virtual void UpdateRegion()
        {
            if (!IsHandleCreated) return;

            var oldRegion = Region;
            Region = CornerRadius > 0
                ? new Region(Utilities.GraphicsHelper.CreateRoundedRectangle(ClientRectangle, CornerRadius))
                : null;
            oldRegion?.Dispose();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateRegion();
            // Handle 创建前（设计器序列化、代码设置）设置的 BackColor 需要在此确认
            if (!_backColorOverridden && !IsDesignMode)
            {
                // 非设计模式下，如果 BackColor 不同于主题默认色，视为用户自定义
                if (BackColor != _designTimeFallbackTheme.Colors.Background)
                    _backColorOverridden = true;
            }
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            // 仅在 Handle 已创建且非应用主题颜色时标记为自定义
            if (IsHandleCreated && !_applyingThemeColors && !IsDesignMode)
                _backColorOverridden = true;
        }

        private int _cornerRadius = 4;

        /// <summary>
        /// 圆角半径
        /// </summary>
        [Category("外观")]
        [Description("控件圆角半径")]
        [DefaultValue(4)]
        public int CornerRadius
        {
            get => _cornerRadius;
            set
            {
                if (_cornerRadius != value)
                {
                    _cornerRadius = Math.Max(0, value);
                    UpdateRegion();
                    Invalidate();
                }
            }
        }

        protected BaseControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();

            // 设计模式下不订阅主题变更事件，避免设计器异常
            if (!ControlDesignerHelper.IsDesignMode(this))
            {
                try
                {
                    ThemeManager.Instance.ThemeChanged += OnThemeChanged;
                }
                catch
                {
                    // 设计时访问ThemeManager可能失败，安全忽略
                }
            }
        }

        /// <summary>
        /// 主题变更时的回调，自动清除自定义颜色并应用新主题
        /// </summary>
        protected virtual void OnThemeChanged(object sender, EventArgs e)
        {
            if (IsDisposed) return;
            _textColor = Color.Empty;
            _backColorOverridden = false;
            ApplyTheme();
            Invalidate();
        }

        /// <summary>
        /// 应用当前主题到控件。
        /// 注意：不在基类中设置Font，避免覆盖AutoScaling缩放后的字体导致布局异常。
        /// 各子控件可通过CurrentTheme.DefaultFont获取主题字体。
        /// </summary>
        protected virtual void ApplyTheme()
        {
            var theme = CurrentTheme;
            if (theme == null) return;
            SetThemeBackColor(theme.Colors.Background);
            ForeColor = _textColor != Color.Empty ? _textColor : theme.Colors.TextPrimary;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            IsHovered = true;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            IsHovered = false;
            IsPressed = false;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
                IsPressed = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            IsPressed = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    try
                    {
                        ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
                    }
                    catch
                    {
                        // 设计时安全忽略
                    }
                    DisposeManagedResources();
                }
                _disposed = true;
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 子类重写此方法释放托管资源
        /// </summary>
        protected virtual void DisposeManagedResources()
        {
        }
    }

    /// <summary>
    /// BaseControl的设计时具体替代类，仅供VS设计器实例化使用。
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    public class DesignTimeBaseControl : BaseControl
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var brush = new System.Drawing.SolidBrush(ForeColor))
            {
                e.Graphics.DrawString(GetType().Name, Font, brush, 5, 5);
            }
        }
    }
}
