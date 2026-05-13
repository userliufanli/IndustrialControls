using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IndustrialControls.Theme
{
    /// <summary>
    /// 主题管理器，单例模式，观察者模式通知所有控件
    /// </summary>
    public sealed class ThemeManager
    {
        private static readonly Lazy<ThemeManager> _instance =
            new Lazy<ThemeManager>(() => new ThemeManager());

        private ITheme _currentTheme;
        private readonly Dictionary<string, ITheme> _themes = new Dictionary<string, ITheme>();
        private readonly List<Form> _registeredForms = new List<Form>();

        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static ThemeManager Instance => _instance.Value;

        /// <summary>
        /// 主题变更事件
        /// </summary>
        public event EventHandler ThemeChanged;

        /// <summary>
        /// 当前主题
        /// </summary>
        public ITheme CurrentTheme
        {
            get => _currentTheme;
            private set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    OnThemeChanged();
                }
            }
        }

        /// <summary>
        /// 所有已注册主题
        /// </summary>
        public IReadOnlyDictionary<string, ITheme> Themes => _themes;

        private ThemeManager()
        {
            // 注册默认主题
            var lightTheme = new FlatLightTheme();
            var darkTheme = new FlatDarkTheme();
            RegisterTheme(lightTheme);
            RegisterTheme(darkTheme);
            _currentTheme = lightTheme;
        }

        /// <summary>
        /// 注册主题
        /// </summary>
        public void RegisterTheme(ITheme theme)
        {
            if (theme == null) throw new ArgumentNullException(nameof(theme));
            _themes[theme.Name] = theme;
        }

        /// <summary>
        /// 切换主题
        /// </summary>
        public void SetTheme(string themeName)
        {
            if (string.IsNullOrEmpty(themeName))
                throw new ArgumentNullException(nameof(themeName));

            if (!_themes.ContainsKey(themeName))
                throw new ArgumentException($"主题 '{themeName}' 未注册");

            CurrentTheme = _themes[themeName];
        }

        /// <summary>
        /// 切换主题
        /// </summary>
        public void SetTheme(ITheme theme)
        {
            if (theme == null) throw new ArgumentNullException(nameof(theme));
            if (!_themes.ContainsKey(theme.Name))
                RegisterTheme(theme);
            CurrentTheme = theme;
        }

        /// <summary>
        /// 切换到下一个主题（用于循环切换）
        /// </summary>
        public void ToggleTheme()
        {
            if (CurrentTheme is FlatLightTheme)
                SetTheme("FlatDark");
            else
                SetTheme("FlatLight");
        }

        /// <summary>
        /// 注册窗体到主题管理器（用于全局主题切换）
        /// </summary>
        /// <param name="form">要注册的窗体</param>
        public void RegisterForm(Form form)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            if (!_registeredForms.Contains(form))
            {
                _registeredForms.Add(form);
                form.FormClosed += (s, e) => _registeredForms.Remove(form);
            }
        }

        /// <summary>
        /// 应用主题到所有已注册的窗体
        /// </summary>
        public void ApplyThemeToAllForms()
        {
            foreach (var form in _registeredForms.ToArray())
            {
                if (form.IsDisposed) continue;
                
                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() => ApplyThemeToForm(form)));
                }
                else
                {
                    ApplyThemeToForm(form);
                }
            }
        }

        /// <summary>
        /// 应用主题到单个窗体
        /// </summary>
        /// <param name="form">目标窗体</param>
        private void ApplyThemeToForm(Form form)
        {
            try
            {
                var theme = CurrentTheme;
                
                // 更新窗体背景色
                form.BackColor = theme.Colors.Background;
                
                // 递归更新所有控件
                UpdateControlTheme(form, theme);
                
                // 强制重绘
                form.Invalidate(true);
            }
            catch
            {
                // 忽略更新过程中的异常
            }
        }

        /// <summary>
        /// 递归更新控件主题
        /// </summary>
        /// <param name="control">要更新的控件</param>
        /// <param name="theme">目标主题</param>
        private void UpdateControlTheme(Control control, ITheme theme)
        {
            try
            {
                string ns = control.GetType().Namespace ?? "";
                
                // 跳过控件库的自定义控件（它们会通过事件自己处理主题）
                // 但不跳过 Demo 项目中的 UserControl 页面
                if (ns.StartsWith("IndustrialControls.Controls") || 
                    ns.StartsWith("IndustrialControls.Core"))
                {
                    // 仍然需要递归处理子控件
                    foreach (Control child in control.Controls)
                    {
                        UpdateControlTheme(child, theme);
                    }
                    return;
                }

                // 更新标准控件的背景色和前景色
                if (control is Form || control is Panel || control is GroupBox || 
                    control is TabControl || control is TabPage || control is UserControl)
                {
                    control.BackColor = theme.Colors.Background;
                    control.ForeColor = theme.Colors.TextPrimary;
                }
                else if (control is TextBox || control is RichTextBox)
                {
                    control.BackColor = theme.Colors.Surface;
                    control.ForeColor = theme.Colors.TextPrimary;
                }
                else if (control is Label)
                {
                    control.ForeColor = theme.Colors.TextPrimary;
                }
                else if (control is Button)
                {
                    control.BackColor = theme.Colors.SurfaceVariant;
                    control.ForeColor = theme.Colors.TextPrimary;
                }
                else if (control is ListBox || control is ComboBox || control is DataGridView)
                {
                    control.BackColor = theme.Colors.Surface;
                    control.ForeColor = theme.Colors.TextPrimary;
                }

                // 递归处理子控件
                foreach (Control child in control.Controls)
                {
                    UpdateControlTheme(child, theme);
                }
            }
            catch
            {
                // 忽略单个控件的更新异常
            }
        }

        private void OnThemeChanged()
        {
            // 先应用主题到所有已注册的窗体
            ApplyThemeToAllForms();
            
            // 再触发事件通知自定义控件
            ThemeChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
