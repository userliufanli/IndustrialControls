using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Demo;
using IndustrialControls.Controls.DataVisualization;

namespace IndustrialControls.Demo.Pages
{
    public partial class DataVisualizationPage : UserControl
    {
        private Timer _dataTimer;
        private Random _rnd = new Random();
        private double _angle;
        private StringBuilder _logBuilder = new StringBuilder();

        public DataVisualizationPage()
        {
            InitializeComponent();
            DemoUiChrome.ApplyPage(this);

            if (!ControlDesignerHelper.IsDesignMode(this))
            {
                SetupControls();
                SetupEvents();

                // 添加趋势图通道（运行时才能调用）
                trendChart.AddChannel("温度", Color.FromArgb(239, 68, 68));
                trendChart.AddChannel("压力", Color.FromArgb(59, 130, 246));
                trendChart.AddChannel("流量", Color.FromArgb(16, 185, 129));

                _dataTimer = new Timer { Interval = 100 };
                _dataTimer.Tick += DataTimer_Tick;
                _dataTimer.Start();
            }
        }

        private void SetupControls()
        {





            LogEvent("=== 数据可视化演示已初始化 ===");
            LogEvent("仪表盘: 实时数值显示，平滑动画");
            LogEvent("进度条: 百分比可视化，动态填充");
            LogEvent("趋势图: 多通道实时数据曲线");
            LogEvent("");
        }

        private void SetupEvents()
        {
            gauge1.ValueChanged += (s, v) => LogEvent($"仪表盘1({gauge1.Title}) 值: {v:F1}");
            gauge2.ValueChanged += (s, v) => LogEvent($"仪表盘2({gauge2.Title}) 值: {v:F0}");
            progressBar1.ValueChanged += (s, v) => LogEvent($"进度条1 值: {v:F1}%");
            progressBar2.ValueChanged += (s, v) => LogEvent($"进度条2 值: {v:F1}%");
        }

        private void DataTimer_Tick(object sender, EventArgs e)
        {
            _angle += 0.1;

            // 更新趋势图数据
            double temp = 50 + 20 * Math.Sin(_angle * 0.3) + _rnd.NextDouble() * 3;
            double pressure = 30 + 15 * Math.Cos(_angle * 0.2) + _rnd.NextDouble() * 2;
            double flow = 40 + 10 * Math.Sin(_angle * 0.5 + 1) + _rnd.NextDouble() * 2;
            double d = 10 + 10 * Math.Sin(_angle * 0.5 + 1) + _rnd.NextDouble() * 2;

            trendChart.AddDataPoint("年龄", d);
            trendChart.AddDataPoint("温度", temp);
            trendChart.AddDataPoint("压力", pressure);
            trendChart.AddDataPoint("流量", flow);

            // 更新仪表盘
            gauge1.Value = temp;
            gauge2.Value = 1500 + 500 * Math.Sin(_angle * 0.1) + _rnd.NextDouble() * 50;

            // 更新进度条
            progressBar1.Value = 40 + 20 * Math.Sin(_angle * 0.15) + _rnd.NextDouble() * 5;
            progressBar2.Value = 60 + 10 * Math.Cos(_angle * 0.08) + _rnd.NextDouble() * 3;
            progressBar3.Value = 60 + 10 * Math.Cos(_angle * 0.08) + _rnd.NextDouble() * 3;

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

        private void btnToggleAnimation_Click(object sender, EventArgs e)
        {
            if (_dataTimer.Enabled)
            {
                _dataTimer.Stop();
                LogEvent("--- 数据模拟已暂停 ---");
                btnToggleAnimation.Text = "继续模拟";
            }
            else
            {
                _dataTimer.Start();
                LogEvent("--- 数据模拟已继续 ---");
                btnToggleAnimation.Text = "暂停模拟";
            }
        }

        private void btnClearChart_Click(object sender, EventArgs e)
        {
            trendChart.ClearData();
            LogEvent("趋势图已清空数据");
        }

        private void btnTestGauge_Click(object sender, EventArgs e)
        {
            gauge1.Value = gauge1.Maximum;
            LogEvent($"仪表盘1 设为最大值: {gauge1.Maximum}");
            gauge2.Value = gauge2.Minimum;
            LogEvent($"仪表盘2 设为最小值: {gauge2.Minimum}");
        }

        private void btnChangeCornerRadius_Click(object sender, EventArgs e)
        {
            int newRadius = gauge1.CornerRadius == 8 ? 16 : 8;
            gauge1.CornerRadius = newRadius;
            gauge2.CornerRadius = newRadius;

            progressBar1.CornerRadius = newRadius;
            progressBar2.CornerRadius = newRadius;
            progressBar3.CornerRadius = newRadius;

            trendChart.CornerRadius = newRadius;
            LogEvent($"圆角半径更改为: {newRadius}");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataTimer?.Stop();
                _dataTimer?.Dispose();
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
