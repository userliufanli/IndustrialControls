using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Demo;
using IndustrialControls.Controls.DataInput;

namespace IndustrialControls.Demo.Pages
{
    public partial class DataInputPage : UserControl
    {
        private StringBuilder _logBuilder = new StringBuilder();

        public DataInputPage()
        {
            InitializeComponent();
            DemoUiChrome.ApplyPage(this);

            if (!ControlDesignerHelper.IsDesignMode(this))
            {
                SetupEvents();
                SetupDemoControls();
            }
        }

        private void SetupEvents()
        {
            // NumericInputBox 事件
            numInput1.ValueChanged += (s, v) => LogEvent($"numInput1 值变更: {v:F2}");
            numInput1.ValidationFailed += (s, msg) => LogEvent($"numInput1 验证失败: {msg}");
            numInput2.ValueChanged += (s, v) => LogEvent($"numInput2 值变更: {v:F1}");
            numInput2.ValidationFailed += (s, msg) => LogEvent($"numInput2 验证失败: {msg}");

            // ValidatedTextBox 事件
            validText1.ValidationChanged += (s, isValid) => LogEvent($"validText1 验证状态变更: {(isValid ? "通过" : "失败")}");
            validText2.ValidationChanged += (s, isValid) => LogEvent($"validText2 验证状态变更: {(isValid ? "通过" : "失败")}");

            // DataInputPanel 事件
            panel1.ValueChanged += (s, v) => LogEvent($"panel1({panel1.LabelText}) 值变更: {v:F0}");
            panel2.ValueChanged += (s, v) => LogEvent($"panel2({panel2.LabelText}) 值变更: {v:F0}");
            panel3.ValueChanged += (s, v) => LogEvent($"panel3({panel3.LabelText}) 值变更: {v:F1}");
        }

        private void SetupDemoControls()
        {
            // 配置 numInput1: 步长0.5，范围0~100
            numInput1.Minimum = 0;
            numInput1.Maximum = 100;
            numInput1.Step = 0.5;
            numInput1.DecimalPlaces = 2;
            numInput1.Value = 50;
            numInput1.Unit = "mm";

            // 配置 numInput2: 范围-50~200，小数1位
            numInput2.Minimum = -50;
            numInput2.Maximum = 200;
            numInput2.Step = 1;
            numInput2.DecimalPlaces = 1;
            numInput2.Value = 25.5;
            numInput2.Unit = "°C";

            // 配置 validText1: 默认使用纯数字验证（设计器可覆盖）
            validText1.Mode = ValidationMode.Realtime;

            // 配置 validText2: 默认使用邮箱验证（设计器可覆盖）
            validText2.Mode = ValidationMode.OnLeave;

            LogEvent("=== 验证预设演示 ==="); 
            LogEvent($"validText1: 当前预设={validText1.Preset}（{validText1.Mode}）");
            LogEvent($"validText2: 当前预设={validText2.Preset}（{validText2.Mode}）");
            LogEvent("");

            // 配置 DataInputPanel
            panel1.Minimum = 0;
            panel1.Maximum = 3000;
            panel1.DecimalPlaces = 0;
            panel1.Value = 1500;

            panel2.Minimum = 0;
            panel2.Maximum = 150;
            panel2.DecimalPlaces = 0;
            panel2.Value = 85;

            panel3.Minimum = 0;
            panel3.Maximum = 10;
            panel3.DecimalPlaces = 1;
            panel3.Value = 2.5;

            LogEvent("=== 数据输入控件演示已初始化 ===");
            LogEvent("NumericInputBox: 支持上下限验证、步进调节、单位显示");
            LogEvent("ValidatedTextBox: 支持必填、正则、实时/离焦验证");
            LogEvent("DataInputPanel: 标签+输入框+单位的标准组合");
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
            // 限制日志行数
            var lines = _logBuilder.ToString().Split('\n');
            if (lines.Length > 50)
            {
                _logBuilder.Clear();
                int start = lines.Length - 40;
                for (int i = start; i < lines.Length; i++)
                    _logBuilder.AppendLine(lines[i]);
            }

            logTextBox.Text = _logBuilder.ToString();
            logTextBox.SelectionStart = logTextBox.Text.Length;
            logTextBox.ScrollToCaret();
        }

        private void btnResetValues_Click(object sender, EventArgs e)
        {
            numInput1.Value = 50;
            numInput2.Value = 25.5;
            panel1.Value = 1500;
            panel2.Value = 85;
            panel3.Value = 2.5;
            validText1.Text = "";
            validText2.Text = "";
            LogEvent("=== 所有值已重置 ===");
        }

        private void btnTestClamp_Click(object sender, EventArgs e)
        {
            LogEvent("--- 测试上下限钳位 ---");
            LogEvent($"设置 numInput1.Value = 200 (最大100) → 实际: {numInput1.Value}");
            numInput1.Value = 200;
            LogEvent($"设置 numInput1.Value = -10 (最小0) → 实际: {numInput1.Value}");
            numInput1.Value = -10;
            LogEvent($"设置 panel3.Value = 999 (最大10) → 实际: {panel3.Value}");
            panel3.Value = 999;
        }

        private void btnToggleButtons_Click(object sender, EventArgs e)
        {
            numInput1.ShowButtons = !numInput1.ShowButtons;
            numInput2.ShowButtons = !numInput2.ShowButtons;
            LogEvent($"步进按钮显示: {(numInput1.ShowButtons ? "开启" : "关闭")}");
        }

        private void btnChangeCornerRadius_Click(object sender, EventArgs e)
        {
            int newRadius = numInput1.CornerRadius == 4 ? 12 : 4;
            numInput1.CornerRadius = newRadius;
            numInput2.CornerRadius = newRadius;
            validText1.CornerRadius = newRadius;
            validText2.CornerRadius = newRadius;
            panel1.CornerRadius = newRadius;
            panel2.CornerRadius = newRadius;
            panel3.CornerRadius = newRadius;
            LogEvent($"圆角半径更改为: {newRadius}");
        }
    }
}
