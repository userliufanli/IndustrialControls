using System;
using System.Drawing;
using System.Windows.Forms;
using IndustrialControls.Controls.VirtualKeyboard;

namespace IndustrialControls.Demo
{
    /// <summary>
    /// 跨窗体输入测试窗体
    /// </summary>
    public partial class CrossFormTestForm : Form
    {
        private VirtualKeyboardForm _keyboardForm;
        private Timer _statusTimer;

        public CrossFormTestForm(VirtualKeyboardForm keyboardForm)
        {
            _keyboardForm = keyboardForm;
            InitializeComponent();
            InitializeLogic();
        }

        private void InitializeLogic()
        {
            // 启动计时器更新状态显示
            _statusTimer = new Timer();
            _statusTimer.Interval = 200;
            _statusTimer.Tick += StatusTimer_Tick;
            _statusTimer.Start();
        }

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            var activeControl = VirtualKeyboardManager.GetActiveControl();
            if (activeControl != null)
            {
                statusLabel.Text = $"状态: 当前焦点 = {activeControl.Name ?? activeControl.GetType().Name} ({activeControl.GetType().Name})";
            }
            else
            {
                statusLabel.Text = "状态: 等待输入...";
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // 窗体显示后，自动显示虚拟键盘
            _keyboardForm.ShowAt(new Point(this.Right + 10, this.Top));
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void closeBtn_MouseEnter(object sender, EventArgs e)
        {
            closeBtn.BackColor = Color.FromArgb(220, 53, 69);
        }

        private void closeBtn_MouseLeave(object sender, EventArgs e)
        {
            closeBtn.BackColor = Color.FromArgb(196, 43, 28);
        }
    }
}
