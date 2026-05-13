using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Demo;
using IndustrialControls.Controls.StatusIndicator;

namespace IndustrialControls.Demo.Pages
{
    public partial class StatusIndicatorPage : UserControl
    {
        private StringBuilder _logBuilder = new StringBuilder();
        private int _clickCount;

        public StatusIndicatorPage()
        {
            InitializeComponent();
            DemoUiChrome.ApplyPage(this);

            if (!ControlDesignerHelper.IsDesignMode(this))
            {
                SetupEvents();
                SetupDemo();
            }
        }

        private void SetupEvents()
        {
            // StatusIndicator 事件
            indicator1.Click += (s, e) =>
            {
                _clickCount++;
                var states = new[] { IndicatorState.Running, IndicatorState.Stopped, IndicatorState.Fault, IndicatorState.Warning };
                indicator1.State = states[_clickCount % states.Length];
                LogEvent($"indicator1 点击 #{_clickCount} → 状态: {indicator1.State}");
            };
            indicator2.Click += (s, e) => LogEvent($"indicator2 点击 → 状态: {indicator2.State}");
            indicator3.Click += (s, e) => LogEvent($"indicator3 点击 → 状态: {indicator3.State}");
        }

        private void SetupDemo()
        {
            //// 配置不同状态
            //indicator1.State = IndicatorState.Running;
            //indicator1.Shape = IndicatorShape.Circle;
            ////indicator1.Size = new Size(60, 60);
            //indicator1.EnableBlink = false;

            //indicator2.State = IndicatorState.Stopped;
            //indicator2.Shape = IndicatorShape.Square;
            //indicator2.Size = new Size(60, 60);

            //indicator3.State = IndicatorState.Fault;
            //indicator3.Shape = IndicatorShape.RoundedRectangle;
            //indicator3.Size = new Size(60, 60);

            //indicator4.State = IndicatorState.Warning;
            //indicator4.Shape = IndicatorShape.Circle;
            //indicator4.Size = new Size(60, 60);
            //indicator4.EnableBlink = true;

            LogEvent("=== 状态指示器演示已初始化 ===");
            LogEvent("indicator1: 点击切换状态");
            LogEvent("indicator4: 启用闪烁效果");
            LogEvent("");
        }

        private void LogEvent(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => LogEvent(message)));
                return;
            }

            _logBuilder.AppendLine($"[{DateTime.Now:HH:mm:ss}] {message}");
            var lines = _logBuilder.ToString().Split('\n');
            if (lines.Length > 30)
            {
                _logBuilder.Clear();
                int start = lines.Length - 25;
                for (int i = start; i < lines.Length; i++)
                    _logBuilder.AppendLine(lines[i]);
            }

            logTextBox.Text = _logBuilder.ToString();
            logTextBox.SelectionStart = logTextBox.Text.Length;
            logTextBox.ScrollToCaret();
        }

        private void chkBlink_CheckedChanged(object sender, EventArgs e)
        {
            indicator1.EnableBlink = chkBlink.Checked;
            indicator2.EnableBlink = chkBlink.Checked;
            indicator3.EnableBlink = chkBlink.Checked;
            indicator4.EnableBlink = chkBlink.Checked;


        }

        private void btnRunning_Click(object sender, EventArgs e)
        {
            indicator1.State = IndicatorState.Running;
            indicator2.State = IndicatorState.Running;
            indicator3.State = IndicatorState.Running;
            indicator4.State = IndicatorState.Running;


        }

        private void btnStopped_Click(object sender, EventArgs e)
        {
            indicator1.State = IndicatorState.Stopped;
            indicator2.State = IndicatorState.Stopped;
            indicator3.State = IndicatorState.Stopped;
            indicator4.State = IndicatorState.Stopped;
        }

        private void btnFault_Click(object sender, EventArgs e)
        {
            indicator1.State = IndicatorState.Fault;
            indicator2.State = IndicatorState.Fault;
            indicator3.State = IndicatorState.Fault;
            indicator4.State = IndicatorState.Fault;
        }

        private void btnWarning_Click(object sender, EventArgs e)
        {
            indicator1.State = IndicatorState.Warning;
            indicator2.State = IndicatorState.Warning;
            indicator3.State = IndicatorState.Warning;
            indicator4.State = IndicatorState.Warning;
        }

        private void DemoTimer_Tick(object sender, EventArgs e)
        {
            //var states = new[] { IndicatorState.Running, IndicatorState.Warning, IndicatorState.Fault, IndicatorState.Stopped };
            //var rnd = new Random();
            //indicator4.State = states[rnd.Next(states.Length)];
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }
    }
}
