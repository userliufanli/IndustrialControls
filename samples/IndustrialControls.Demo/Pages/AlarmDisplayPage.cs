using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Demo;
using IndustrialControls.Controls.Alarm;

namespace IndustrialControls.Demo.Pages
{
    public partial class AlarmDisplayPage : UserControl
    {
        private Timer _demoTimer;
        private Random _rnd = new Random();
        private int _alarmCount;
        private StringBuilder _logBuilder = new StringBuilder();

        public AlarmDisplayPage()
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
            // AlarmDisplay 事件
            alarmDisplay.AlarmAdded += (s, alarm) => LogEvent($"报警添加: [{alarm.Level}] {alarm.Message}");
            alarmDisplay.AlarmAcknowledged += (s, alarm) => LogEvent($"报警确认: {alarm.Message} 由 {alarm.AcknowledgedBy}");
            alarmDisplay.AlarmCleared += (s, e) => LogEvent("报警全部清除");
        }

        private void SetupDemo()
        {
            // 添加初始报警
            alarmDisplay.AddAlarm(AlarmLevel.Emergency, "电机过温保护触发", "电机驱动器");
            alarmDisplay.AddAlarm(AlarmLevel.Important, "气压低于设定值", "气动系统");
            alarmDisplay.AddAlarm(AlarmLevel.General, "滤芯需要更换", "液压系统");
            alarmDisplay.AddAlarm(AlarmLevel.Info, "系统启动完成", "主控制器");

            // 自动模拟报警
            _demoTimer = new Timer { Interval = 5000 };
            _demoTimer.Tick += DemoTimer_Tick;
            _demoTimer.Start();

            LogEvent("=== 报警显示演示已初始化 ===");
            LogEvent("提示：双击报警条目可确认，滚轮滚动查看更多");
            LogEvent("");
        }

        private void DemoTimer_Tick(object sender, EventArgs e)
        {
            var levels = new[] { AlarmLevel.Info, AlarmLevel.General, AlarmLevel.Important };
            var messages = new[] { "传感器信号异常", "通信超时", "位置偏差超限", "温度波动" };
            alarmDisplay.AddAlarm(levels[_rnd.Next(levels.Length)], messages[_rnd.Next(messages.Length)] + $" #{++_alarmCount}", "自动监测");
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

        private void btnAddEmergency_Click(object sender, EventArgs e)
        {
            alarmDisplay.AddAlarm(AlarmLevel.Emergency, $"紧急报警测试 #{++_alarmCount}", "设备A");
        }

        private void btnAddImportant_Click(object sender, EventArgs e)
        {
            alarmDisplay.AddAlarm(AlarmLevel.Important, $"重要报警测试 #{++_alarmCount}", "设备B");
        }

        private void btnAddGeneral_Click(object sender, EventArgs e)
        {
            alarmDisplay.AddAlarm(AlarmLevel.General, $"一般报警测试 #{++_alarmCount}", "设备C");
        }

        private void btnAckAll_Click(object sender, EventArgs e)
        {
            alarmDisplay.AcknowledgeAll("操作员");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            alarmDisplay.ClearAcknowledged();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            alarmDisplay.ClearAll();
        }

        private void btnFilterLastHour_Click(object sender, EventArgs e)
        {
            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.AddHours(-1);
            alarmDisplay.ApplyTimeFilter(startTime, endTime);
            LogEvent("应用时间筛选：最近1小时");
        }

        private void btnFilterLast5Min_Click(object sender, EventArgs e)
        {
            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.AddMinutes(-5);
            alarmDisplay.ApplyTimeFilter(startTime, endTime);
            LogEvent("应用时间筛选：最近5分钟");
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            alarmDisplay.ClearTimeFilter();
            LogEvent("清除时间筛选");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _demoTimer?.Stop();
                _demoTimer?.Dispose();
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
