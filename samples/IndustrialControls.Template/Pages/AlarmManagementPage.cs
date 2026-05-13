using System;
using System.Windows.Forms;
using IndustrialControls.Controls.Alarm;
using IndustrialControls.Core;
using IndustrialControls.Template.Core;
using IndustrialControls.Template.Models;

namespace IndustrialControls.Template.Pages
{
    public partial class AlarmManagementPage : UserControl
    {
        public AlarmManagementPage()
        {
            InitializeComponent();
        }

        private void AlarmManagementPage_Load(object sender, EventArgs e)
        {
            if (ControlDesignerHelper.IsDesignMode(this))
                return;
            TemplateUiChrome.ApplyPage(this);
            TemplateUiChrome.StyleGroupBox(groupBoxAlarms);
            TemplateRuntime.Alarms.AlarmRaised += Alarms_AlarmRaised;
        }

        private void Alarms_AlarmRaised(object sender, AlarmRecord e)
        {
            if (e == null || alarmDisplayMain.IsDisposed)
                return;
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => Alarms_AlarmRaised(sender, e)));
                return;
            }

            alarmDisplayMain.AddAlarm(e.Level, e.Message, e.Source);
        }

        private void buttonAckAll_Click(object sender, EventArgs e)
        {
            alarmDisplayMain.AcknowledgeAll(CurrentUser.Name ?? "local");
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            alarmDisplayMain.ClearAll();
        }

        private void buttonTestAlarm_Click(object sender, EventArgs e)
        {
            TemplateRuntime.Alarms.Raise(AlarmLevel.General, "测试报警：由模板页面触发。", "HMI");
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (TemplateRuntime.Alarms != null)
                TemplateRuntime.Alarms.AlarmRaised -= Alarms_AlarmRaised;
            base.OnHandleDestroyed(e);
        }
    }
}
