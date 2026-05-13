using System;
using System.Drawing;
using System.Windows.Forms;
using IndustrialControls.Theme;

namespace IndustrialControls.Controls.Login
{
    /// <summary>
    /// 扁平化用户管理：添加、删除、修改密码。
    /// </summary>
    public sealed partial class LoginUserManagementForm : Form
    {
        private readonly LoginUserStore _store;

        /// <summary>
        /// 在宿主已确认权限（菜单、角色、口令等）后调用；登录面板上不应直接挂载用户管理入口。
        /// </summary>
        public static void ShowForStore(LoginUserStore store, IWin32Window owner)
        {
            if (store == null)
                throw new ArgumentNullException(nameof(store));
            using (var f = new LoginUserManagementForm(store))
                f.ShowDialog(owner);
        }

        public LoginUserManagementForm(LoginUserStore store)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
            InitializeComponent();
            ApplyThemeColors();
            UpdateHintText();
            RefreshList();
        }

        private void ApplyThemeColors()
        {
            var c = ThemeManager.Instance.CurrentTheme?.Colors ?? new FlatLightTheme().Colors;
            BackColor = c.Background;
            ForeColor = c.TextPrimary;
            lblHint.ForeColor = c.TextSecondary;
            listUsers.BackColor = c.Surface;
            listUsers.ForeColor = c.TextPrimary;
            grpAdd.ForeColor = c.TextPrimary;
            grpChange.ForeColor = c.TextPrimary;
            lblAddUserCaption.ForeColor = c.TextSecondary;
            lblAddPasswordCaption.ForeColor = c.TextSecondary;
            lblAddConfirmCaption.ForeColor = c.TextSecondary;
            lblChangeOldCaption.ForeColor = c.TextSecondary;
            lblChangeNewCaption.ForeColor = c.TextSecondary;
            foreach (Control child in grpAdd.Controls)
                ApplyInputChrome(child, c);
            foreach (Control child in grpChange.Controls)
                ApplyInputChrome(child, c);
            btnAdd.BackColor = c.Primary;
            btnAdd.ForeColor = c.TextOnPrimary;
            btnChangePwd.BackColor = c.Primary;
            btnChangePwd.ForeColor = c.TextOnPrimary;
            btnDelete.BackColor = c.Danger;
            btnDelete.ForeColor = c.TextOnPrimary;
            btnClose.BackColor = c.Surface;
            btnClose.ForeColor = c.TextPrimary;
            btnClose.FlatAppearance.BorderColor = c.Border;
        }

        private static void ApplyInputChrome(Control control, ThemeColors c)
        {
            if (control is TextBox tb)
            {
                tb.BackColor = c.Surface;
                tb.ForeColor = c.TextPrimary;
            }
        }

        private void UpdateHintText()
        {
            lblHint.Text = "用户数据保存在参数管理器分组「" + _store.GroupName + "」。用户名不区分大小写。";
        }

        private void RefreshList()
        {
            listUsers.Items.Clear();
            foreach (var n in _store.ListUserNames())
                listUsers.Items.Add(n);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OnAdd(object sender, EventArgs e)
        {
            if (!string.Equals(txtNewPassword.Text, txtConfirm.Text, StringComparison.Ordinal))
            {
                MessageBox.Show(this, "两次输入的密码不一致。", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_store.TryAddUser(txtNewUser.Text, txtNewPassword.Text, out string err))
            {
                MessageBox.Show(this, err, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtNewUser.Clear();
            txtNewPassword.Clear();
            txtConfirm.Clear();
            RefreshList();
        }

        private void OnDelete(object sender, EventArgs e)
        {
            if (listUsers.SelectedItem == null)
            {
                MessageBox.Show(this, "请先在列表中选择用户。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string name = listUsers.SelectedItem.ToString();
            if (MessageBox.Show(this, "确定删除用户「" + name + "」？", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (!_store.TryRemoveUser(name, out string err))
            {
                MessageBox.Show(this, err, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RefreshList();
        }

        private void OnChangePassword(object sender, EventArgs e)
        {
            if (listUsers.SelectedItem == null)
            {
                MessageBox.Show(this, "请先在列表中选择用户。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string name = listUsers.SelectedItem.ToString();
            if (!_store.TryChangePassword(name, txtChangeOld.Text, txtChangeNew.Text, out string err))
            {
                MessageBox.Show(this, err, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtChangeOld.Clear();
            txtChangeNew.Clear();
            MessageBox.Show(this, "密码已更新。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
