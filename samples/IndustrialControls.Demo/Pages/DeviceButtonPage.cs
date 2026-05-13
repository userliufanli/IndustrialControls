using IndustrialControls.Controls.DeviceButton;
using IndustrialControls.Demo;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IndustrialControls.Demo.Pages
{
    public partial class DeviceButtonPage : UserControl
    {
        public DeviceButtonPage()
        {
            InitializeComponent();
            DemoUiChrome.ApplyPage(this);
            SetupButtonStates();
            statusLabel.Text = $"当前状态: {btn1.ButtonText}";
        }

        private void SetupButtonStates()
        {
            // 配置 btn1 - 动作模式（默认显示索引1“已停止”）
            // btn1.Mode = DisplayMode.ActionMode;  // 默认就是ActionMode
            btn1.DefaultStateIndex = 1;  // 设置默认状态为索引1（已停止）
            btn1.SetStates(
                new DeviceButtonState("Running", "运行中", Color.FromArgb(16, 185, 129)),
                new DeviceButtonState("Stopped", "已停止", Color.FromArgb(239, 68, 68))
            );
            
            // 配置 btn2 - 状态模式（默认显示索引1“运行中”）
            btn2.Mode = DisplayMode.StatusMode;
            btn2.DefaultStateIndex = 1;  // 设置默认状态为索引1（运行中）
            btn2.SetStates(
                new DeviceButtonState("Paused", "已暂停", Color.FromArgb(245, 158, 11)),
                new DeviceButtonState("Running", "运行中", Color.FromArgb(16, 185, 129))
            );
            
            // 配置 btn3 - 状态模式（单状态，显示“就绪”）
            btn3.Mode = DisplayMode.StatusMode;
            btn3.DefaultStateIndex = 0;  // 单状态，只能是索引0
            btn3.SetStates(
                new DeviceButtonState("Ready", "就绪", Color.FromArgb(59, 130, 246))
            );
            
            // 配置 btnLongPress - 状态模式（默认显示索引0“准备就绪”）
            btnLongPress.Mode = DisplayMode.StatusMode;
            btnLongPress.DefaultStateIndex = 0;  // 设置默认状态为索引0（准备就绪）
            btnLongPress.SetStates(
                new DeviceButtonState("Ready", "准备就绪", Color.FromArgb(245, 158, 11)),
                new DeviceButtonState("Triggered", "已触发", Color.FromArgb(220, 38, 38))
            );
            
            // 配置 btnDisabled - 状态模式（默认显示索引0“启用”）
            btnDisabled.Mode = DisplayMode.StatusMode;
            btnDisabled.DefaultStateIndex = 0;  // 设置默认状态为索引0（启用）
            btnDisabled.SetStates(
                new DeviceButtonState("Enabled", "启用", Color.FromArgb(16, 185, 129)),
                new DeviceButtonState("Disabled", "禁用", Color.FromArgb(107, 114, 128))
            );
        }

        private void btn1_StateChanged(object sender, DeviceButtonState state)
        {
            statusLabel.Text = $"当前状态: {state.DisplayText}";
        }

        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            btnDisabled.Enabled = chkEnable.Checked;
        }

        private void btnLongPress_StateChanged(object sender, DeviceButtonState e)
        {
           
        }
    }
}
