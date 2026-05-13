using System;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Template.Core;

namespace IndustrialControls.Template.Pages
{
    public partial class CommunicationConfigPage : UserControl
    {
        public CommunicationConfigPage()
        {
            InitializeComponent();
        }

        private void CommunicationConfigPage_Load(object sender, EventArgs e)
        {
            if (ControlDesignerHelper.IsDesignMode(this))
                return;
            TemplateUiChrome.ApplyPage(this, compactPadding: true);
        }
    }
}
