using System;
using System.Linq;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Template.Core;

namespace IndustrialControls.Template.Pages
{
    public partial class DashboardPage : UserControl
    {
        public DashboardPage()
        {
            InitializeComponent();
        }

        private void DashboardPage_Load(object sender, EventArgs e)
        {
            if (ControlDesignerHelper.IsDesignMode(this))
                return;
            TemplateUiChrome.ApplyPage(this);
            RefreshSummary();
        }

        public void RefreshSummary()
        {
            labelUser.Text = "当前用户：" + (CurrentUser.IsLoggedIn ? CurrentUser.Name : "—");
            labelModel.Text = "机型：" + AppParameters.Machine.Get("CurrentModelId", "Default");
            labelLine.Text = "通信摘要：" + (TemplateRuntime.Communication?.LastSummary ?? "—");
            var devs = TemplateRuntime.Devices.LoadAll();
            labelDevices.Text = "设备条目：" + devs.Count + (devs.Count > 0 ? "（" + string.Join("、", devs.Take(3).Select(d => d.ToString())) + "）" : "");
        }
    }
}
