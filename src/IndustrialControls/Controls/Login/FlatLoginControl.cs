using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Theme;

namespace IndustrialControls.Controls.Login
{
    /// <summary>
    /// 扁平风格登录面板：使用 <see cref="ParameterManager"/> 指定分组持久化用户列表。
    /// 用户增删改请在宿主侧鉴权后通过 <see cref="LoginUserManagementForm.ShowForStore"/> 打开，不在本控件上提供入口。
    /// </summary>
    public partial class FlatLoginControl : UserControl
    {
        private ParameterManager _parameterManager;
        private string _parameterGroupName = "LoginUsers";
        private LoginUserStore _store;

        /// <summary>
        /// 登录成功（参数为登录用户名）。
        /// </summary>
        public event EventHandler<string> LoginSucceeded;

        /// <summary>
        /// 登录失败（参数为原因说明）。
        /// </summary>
        public event EventHandler<string> LoginFailed;

        public FlatLoginControl()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        /// <summary>
        /// 为空则使用 <see cref="ParameterAccessor.Default"/>。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ParameterManager ParameterManager
        {
            get => _parameterManager;
            set
            {
                _parameterManager = value;
                _store = null;
            }
        }

        /// <summary>
        /// 存储凭据的参数分组名。
        /// </summary>
        [Category("数据")]
        [DefaultValue("LoginUsers")]
        [Description("ParameterManager 中的分组名称，用于存放登录用户 JSON。")]
        public string ParameterGroupName
        {
            get => _parameterGroupName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("分组名不能为空。", nameof(value));
                _parameterGroupName = value.Trim();
                _store = null;
            }
        }

        /// <summary>
        /// 卡片标题文案。
        /// </summary>
        [Category("外观")]
        [DefaultValue("用户登录")]
        public string TitleText
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value ?? string.Empty;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyThemeColors();
            UpdateCardLayout();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateCardLayout();
        }

        private void ApplyThemeColors()
        {
            var c = ThemeManager.Instance.CurrentTheme?.Colors ?? new FlatLightTheme().Colors;
            var header = ThemeManager.Instance.CurrentTheme?.HeaderFont ?? new Font("Microsoft YaHei UI", 12f, FontStyle.Bold);
            var body = ThemeManager.Instance.CurrentTheme?.DefaultFont ?? new Font("Microsoft YaHei UI", 9f);

            BackColor = c.Background;
            Font = body;
            cardPanel.BackColor = c.Surface;
            lblTitle.Font = header;
            lblTitle.ForeColor = c.TextPrimary;
            lblUser.ForeColor = c.TextSecondary;
            lblPassword.ForeColor = c.TextSecondary;
            txtUser.BackColor = c.Surface;
            txtUser.ForeColor = c.TextPrimary;
            txtPassword.BackColor = c.Surface;
            txtPassword.ForeColor = c.TextPrimary;
            btnLogin.BackColor = c.Primary;
            btnLogin.ForeColor = c.TextOnPrimary;
            lblStatus.ForeColor = c.Danger;
        }

        private void UpdateCardLayout()
        {
            if (cardPanel == null || ClientSize.Width <= 0 || ClientSize.Height <= 0)
                return;

            int inner = Math.Max(220, Math.Min(320, ClientSize.Width - Padding.Horizontal - 48));
            txtUser.Width = inner;
            txtPassword.Width = inner;
            lblStatus.Width = inner;

            btnLogin.Size = new Size(inner, 36);

            int cardW = inner + 48;
            int cardH = lblStatus.Bottom + 24;
            cardPanel.Size = new Size(cardW, cardH);
            cardPanel.Left = Math.Max(0, (ClientSize.Width - cardW) / 2);
            cardPanel.Top = Math.Max(0, (ClientSize.Height - cardH) / 2);
        }

        private LoginUserStore GetStore()
        {
            if (_store != null)
                return _store;

            var mgr = _parameterManager ?? ParameterAccessor.Default;
            _store = new LoginUserStore(mgr.Section(_parameterGroupName));
            return _store;
        }

        private void OnLoginClick(object sender, EventArgs e)
        {
            lblStatus.Text = string.Empty;
            var store = GetStore();
            string user = txtUser.Text ?? string.Empty;

            if (!store.TryVerify(user, txtPassword.Text, out string err))
            {
                lblStatus.Text = err;
                LoginFailed?.Invoke(this, err);
                return;
            }

            var resolved = store.ListUserNames().FirstOrDefault(n => string.Equals(n, user, StringComparison.OrdinalIgnoreCase))
                ?? user.Trim();
            LoginSucceeded?.Invoke(this, resolved);
        }

        /// <summary>
        /// 清除状态行与密码框。
        /// </summary>
        public void ClearSensitiveFields()
        {
            txtPassword.Clear();
            lblStatus.Text = string.Empty;
        }
    }
}
