using System;
using System.Windows.Forms;
using IndustrialControls.Controls.Login;

namespace IndustrialControls.Demo.Pages
{
    public partial class LoginDemoPage : UserControl
    {
        public LoginDemoPage()
        {
            InitializeComponent();
            DemoUiChrome.ApplyPage(this);
            DemoUiChrome.StyleGroupBox(grpAdmin);

            flatLoginControl1.ParameterManager = AppParameters.Default;
            flatLoginControl1.ParameterGroupName = AppParameters.LoginUsersGroupName;
            flatLoginControl1.LoginSucceeded += OnLoginSucceeded;
            flatLoginControl1.LoginFailed += OnLoginFailed;
        }

        private void btnOpenUserMgmt_Click(object sender, EventArgs e)
        {
            string expected = AppParameters.App.LoginManagementUnlockPin ?? string.Empty;
            string entered = (txtAdminPin.Text ?? string.Empty).Trim();
            if (!string.Equals(entered, expected.Trim(), StringComparison.Ordinal))
            {
                MessageBox.Show(this, "管理口令不正确。", "运维入口", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var store = new LoginUserStore(AppParameters.LoginUsers);
            LoginUserManagementForm.ShowForStore(store, FindForm() ?? (IWin32Window)this);
            txtAdminPin.Clear();
        }

        private void OnLoginSucceeded(object sender, string userName)
        {
            flatLoginControl1.ClearSensitiveFields();
            MessageBox.Show(
                this,
                "已通过本地参数验证，当前用户：" + userName + Environment.NewLine +
                "凭据保存在默认 parameters.json 的分组「" + AppParameters.LoginUsersGroupName + "」。",
                "登录成功",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void OnLoginFailed(object sender, string reason)
        {
            // 错误已在控件上显示，此处仅保留扩展点
        }
    }
}
