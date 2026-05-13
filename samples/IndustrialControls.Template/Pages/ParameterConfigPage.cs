using System;
using System.Text;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Template.Core;

namespace IndustrialControls.Template.Pages
{
    public partial class ParameterConfigPage : UserControl
    {
        public ParameterConfigPage()
        {
            InitializeComponent();
        }

        private void ParameterConfigPage_Load(object sender, EventArgs e)
        {
            if (ControlDesignerHelper.IsDesignMode(this))
                return;
            TemplateUiChrome.ApplyPage(this);
            ReloadHint();
        }

        private void ReloadHint()
        {
            var sb = new StringBuilder();
            sb.AppendLine("全局参数文件：Config\\appsettings.json");
            sb.AppendLine("通讯独立文件：Config\\communication.json");
            sb.AppendLine("机型 line：由 Machine.LineConfigRelative 指定，默认 Config\\Models\\Default\\line.json");
            sb.AppendLine("");
            sb.AppendLine("点击「保存全局参数」将调用 AppParameters.Settings.SaveToFile()。");
            textBoxHint.Text = sb.ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            AppParameters.Settings.SaveToFile();
            MessageBox.Show(this, "已保存 appsettings.json。", "参数", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
