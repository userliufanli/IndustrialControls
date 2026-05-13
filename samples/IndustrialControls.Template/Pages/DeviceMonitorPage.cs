using System;
using System.Windows.Forms;
using IndustrialControls.Controls.DeviceButton;
using IndustrialControls.Controls.StatusIndicator;
using IndustrialControls.Core;
using IndustrialControls.Core.Runtime;
using IndustrialControls.Template.Core;

namespace IndustrialControls.Template.Pages
{
    public partial class DeviceMonitorPage : UserControl
    {
        public DeviceMonitorPage()
        {
            InitializeComponent();
        }

        private void DeviceMonitorPage_Load(object sender, EventArgs e)
        {
            if (ControlDesignerHelper.IsDesignMode(this))
                return;
            TemplateUiChrome.ApplyPage(this);
            TemplateUiChrome.StyleGroupBox(groupBoxMotor);

            deviceControlButtonMotor.EnableLongPress = false;
            deviceControlButtonMotor.Mode = DisplayMode.ActionMode;
            deviceControlButtonMotor.DefaultStateIndex = 0;
            deviceControlButtonMotor.SetStates(DeviceButtonState.Stop, DeviceButtonState.Start);
            statusIndicatorMotor.Label = "主电机";

            TemplateRuntime.Tags.SetActiveTags(new[] { SimulatedTagDataSource.MotorRunningTagId });
            TemplateRuntime.Tags.TagChanged += Tags_TagChanged;
            BindMotorFromSnapshot();
        }

        private void Tags_TagChanged(object sender, TagChangedEventArgs e)
        {
            if (e.TagId != SimulatedTagDataSource.MotorRunningTagId)
                return;
            if (e.Snapshot.Value is bool b)
                statusIndicatorMotor.State = SimulatedTagDataSource.ToIndicatorState(b);
        }

        private void BindMotorFromSnapshot()
        {
            if (TemplateRuntime.Tags.TryGetSnapshot(SimulatedTagDataSource.MotorRunningTagId, out var snap) && snap.Value is bool b)
                statusIndicatorMotor.State = SimulatedTagDataSource.ToIndicatorState(b);
        }

        private void deviceControlButtonMotor_StateChanged(object sender, DeviceButtonState e)
        {
            if (e == null)
                return;
            bool run = string.Equals(e.Name, "Start", StringComparison.OrdinalIgnoreCase);
            TemplateRuntime.Tags.SetMotorRunning(run);
            TemplateRuntime.DeviceCommands.SendCommand("Motor", e.DisplayText ?? e.Name);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (TemplateRuntime.Tags != null)
                TemplateRuntime.Tags.TagChanged -= Tags_TagChanged;
            base.OnHandleDestroyed(e);
        }
    }
}
