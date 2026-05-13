using System;
using System.Windows.Forms;
using IndustrialControls.Controls.Login;
using IndustrialControls.Core;
using IndustrialControls.Template.Core;
using IndustrialControls.Template.Models;

namespace IndustrialControls.Template.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (ControlDesignerHelper.IsDesignMode(this))
                return;

            flatLoginControl.ParameterManager = AppParameters.Settings;
            flatLoginControl.ParameterGroupName = AppParameters.LoginUsersGroupName;
            flatLoginControl.LoginSucceeded += FlatLoginControl_LoginSucceeded;
            flatLoginControl.LoginFailed += FlatLoginControl_LoginFailed;
        }

        private void FlatLoginControl_LoginSucceeded(object sender, string userName)
        {
            CurrentUser.Set(userName, UserRole.Operator);
            flatLoginControl.ClearSensitiveFields();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FlatLoginControl_LoginFailed(object sender, string reason)
        {
            int n = AppParameters.Security.Get("FailedLoginCount", 0);
            AppParameters.Security.Set("FailedLoginCount", n + 1);
            AppParameters.Settings.SaveToFile();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
