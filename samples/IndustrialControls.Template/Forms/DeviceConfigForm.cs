using System;
using System.Text;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Template.Core;

namespace IndustrialControls.Template.Forms
{
    public partial class DeviceConfigForm : Form
    {
        public DeviceConfigForm()
        {
            InitializeComponent();
        }

        private void DeviceConfigForm_Load(object sender, EventArgs e)
        {
            if (ControlDesignerHelper.IsDesignMode(this))
                return;
            RefreshList();
        }

        private void RefreshList()
        {
            listBoxDevices.Items.Clear();
            foreach (var d in TemplateRuntime.Devices.LoadAll())
            {
                listBoxDevices.Items.Add(d);
            }

            textBoxDetail.Clear();
        }

        private void listBoxDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDevices.SelectedItem is Models.DeviceConfig cfg)
            {
                var sb = new StringBuilder();
                sb.AppendLine("DeviceId: " + cfg.DeviceId);
                sb.AppendLine("DisplayName: " + cfg.DisplayName);
                sb.AppendLine("LineConfigRelative: " + cfg.LineConfigRelative);
                sb.AppendLine("Notes: " + cfg.Notes);
                textBoxDetail.Text = sb.ToString();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }
    }
}
