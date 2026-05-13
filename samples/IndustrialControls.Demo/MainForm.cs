using System;
using System.Drawing;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Theme;
using IndustrialControls.Controls.VirtualKeyboard;

namespace IndustrialControls.Demo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // 初始化全局虚拟键盘管理器（自动跟踪所有窗体焦点）
            VirtualKeyboardManager.Initialize();

            // 运行时逻辑：订阅主题
            if (!ControlDesignerHelper.IsDesignMode(this))
            {
                // 注册到主题管理器，支持全局主题切换
                ThemeManager.Instance.RegisterForm(this);
                ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            }
        }

        private void btnToggleTheme_Click(object sender, EventArgs e)
        {
            ThemeManager.Instance.ToggleTheme();
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            // ThemeManager.ApplyThemeToAllForms() 会自动更新所有控件
            // 这里只需要强制重绘即可
            this.Invalidate(true);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // 确保窗口不会超出屏幕工作区
            var screen = Screen.FromControl(this);
            if (Width > screen.WorkingArea.Width)
                Width = screen.WorkingArea.Width - 50;
            if (Height > screen.WorkingArea.Height)
                Height = screen.WorkingArea.Height - 50;
        }

    }
}
