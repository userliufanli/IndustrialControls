using System;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Template.Core;

namespace IndustrialControls.Template.Pages
{
    public partial class SystemSettingsPage : UserControl
    {
        public SystemSettingsPage()
        {
            InitializeComponent();
        }

        private void SystemSettingsPage_Load(object sender, EventArgs e)
        {
            if (ControlDesignerHelper.IsDesignMode(this))
                return;
            TemplateUiChrome.ApplyPage(this);
            comboTheme.Items.Clear();
            comboTheme.Items.AddRange(new object[] { "FlatLight", "FlatDark" });
            string t = AppParameters.AppSection.Theme;
            int i = comboTheme.Items.IndexOf(t);
            comboTheme.SelectedIndex = i >= 0 ? i : 0;
            labelCurrent.Text = "当前主题：" + TemplateRuntime.Theme.CurrentName;
        }

        private void buttonApplyTheme_Click(object sender, EventArgs e)
        {
            string name = comboTheme.SelectedItem as string ?? "FlatLight";
            try
            {
                TemplateRuntime.Theme.ApplyRegisteredTheme(name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "主题", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AppParameters.AppSection.Theme = name;
            AppParameters.Settings.SaveToFile();
            labelCurrent.Text = "当前主题：" + TemplateRuntime.Theme.CurrentName;
        }
    }
}
