# 趋势图控件 (TrendChart) API 使用手册

## 概述

`TrendChart` 是一个实时数据趋势显示控件,支持多通道数据曲线、自动缩放、网格显示、图例标注等功能,适用于工业数据采集和过程监控。

## 命名空间

```csharp
IndustrialControls.Controls.DataVisualization
```

## 继承关系

```
System.Object
  └─ System.MarshalByRefObject
     └─ System.ComponentModel.Component
        └─ System.Windows.Forms.Control
           └─ IndustrialControls.Core.DoubleBufferedControl
              └─ TrendChart
```

## 快速开始

### 1. 基本使用

```csharp
using IndustrialControls.Controls.DataVisualization;

// 创建趋势图
var trendChart = new TrendChart
{
    Size = new Size(600, 350),
    Location = new Point(10, 10),
    Title = "温度趋势",
    VisiblePoints = 100,  // 显示100个数据点
    AutoScaleY = true     // Y轴自动缩放
};

// 添加数据通道
trendChart.AddChannel("温度1", Color.Blue);
trendChart.AddChannel("温度2", Color.Red);

// 添加到窗体
this.Controls.Add(trendChart);
```

### 2. 添加数据

```csharp
// 模拟数据源
var timer = new Timer { Interval = 100 }; // 100ms更新一次
timer.Tick += (s, e) =>
{
    var random = new Random();
    
    // 添加数据点
    double temp1 = 50 + Math.Sin(DateTime.Now.Second) * 10 + random.NextDouble() * 2;
    double temp2 = 45 + Math.Cos(DateTime.Now.Second) * 8 + random.NextDouble() * 2;
    
    trendChart.AddDataPoint("温度1", temp1);
    trendChart.AddDataPoint("温度2", temp2);
};
timer.Start();
```

### 3. 配置通道

```csharp
// 在设计器中或通过代码配置通道
var channels = new List<ChannelConfig>
{
    new ChannelConfig("通道A", Color.FromArgb(59, 130, 246)),
    new ChannelConfig("通道B", Color.FromArgb(16, 185, 129)),
    new ChannelConfig("通道C", Color.FromArgb(239, 68, 68))
};

trendChart.Channels.Clear();
trendChart.Channels.AddRange(channels);
```

## 属性

### Title

- **类型**: `string`
- **默认值**: `"趋势图"`
- **说明**: 图表标题
- **示例**:
  ```csharp
  trendChart.Title = "实时温度监控";
  ```

### VisiblePoints

- **类型**: `int`
- **默认值**: `200`
- **说明**: 图表上同时可见的数据点数量
- **示例**:
  ```csharp
  trendChart.VisiblePoints = 500; // 显示更多历史数据
  ```

### BufferCapacity

- **类型**: `int`
- **默认值**: `2000`
- **说明**: 每个通道可存储的最大数据点数
- **示例**:
  ```csharp
  trendChart.BufferCapacity = 5000; // 增加缓冲区
  ```

### AutoScaleY

- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否根据数据自动调整Y轴显示范围
- **示例**:
  ```csharp
  trendChart.AutoScaleY = false; // 手动设置Y轴范围
  trendChart.YMin = 0;
  trendChart.YMax = 100;
  ```

### YMin / YMax

- **类型**: `double`
- **默认值**: `0 / 100`
- **说明**: Y轴显示范围(AutoScaleY关闭时生效)
- **示例**:
  ```csharp
  trendChart.AutoScaleY = false;
  trendChart.YMin = -50;
  trendChart.YMax = 150;
  ```

### ShowGrid

- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否显示图表背景网格线
- **示例**:
  ```csharp
  trendChart.ShowGrid = false;
  ```

### GridLineCount

- **类型**: `int`
- **默认值**: `5`
- **说明**: 图表背景网格线数量
- **示例**:
  ```csharp
  trendChart.GridLineCount = 10; // 更密集的网格
  ```

### ShowXAxis / ShowYAxis

- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否显示X轴/Y轴刻度和标签
- **示例**:
  ```csharp
  trendChart.ShowXAxis = true;
  trendChart.ShowYAxis = true;
  ```

### XTickCount / YTickCount

- **类型**: `int`
- **默认值**: `5 / 4`
- **说明**: X轴/Y轴显示的刻度数量
- **示例**:
  ```csharp
  trendChart.XTickCount = 10;
  trendChart.YTickCount = 8;
  ```

### LineThickness

- **类型**: `int`
- **默认值**: `2`
- **说明**: 数据曲线的线条粗细(像素)
- **示例**:
  ```csharp
  trendChart.LineThickness = 3; // 更粗的线条
  ```

### ShowLegend

- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否显示通道图例
- **示例**:
  ```csharp
  trendChart.ShowLegend = false;
  ```

### LegendPosition

- **类型**: `LegendPosition`
- **默认值**: `LegendPosition.TopRight`
- **说明**: 图例显示位置
- **示例**:
  ```csharp
  trendChart.LegendPosition = LegendPosition.TopLeft;
  ```

### ShowLegendBackground

- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否为图例显示半透明背景
- **示例**:
  ```csharp
  trendChart.ShowLegendBackground = false;
  ```

### LeftMargin

- **类型**: `int`
- **默认值**: `50`
- **说明**: 图表左边距(用于Y轴标签空间)
- **示例**:
  ```csharp
  trendChart.LeftMargin = 70; // 为长标签预留空间
  ```

### TimeInterval

- **类型**: `double`
- **默认值**: `0.1`
- **说明**: 相邻数据点之间的时间间隔(秒),用于X轴时间显示
- **示例**:
  ```csharp
  trendChart.TimeInterval = 1.0; // 每秒一个点
  trendChart.TimeInterval = 0.05; // 每秒20个点
  ```

### Channels

- **类型**: `List<ChannelConfig>` (只读集合)
- **说明**: 数据通道配置列表
- **示例**:
  ```csharp
  trendChart.Channels.Add(new ChannelConfig("新通道", Color.Purple));
  ```

## 方法

### AddChannel(string name, Color? color = null)

添加数据通道。

**参数**:
- `name`: `string` 通道名称
- `color`: `Color?` 通道颜色(可选,自动分配默认颜色)

**示例**:
```csharp
trendChart.AddChannel("温度", Color.Red);
trendChart.AddChannel("压力"); // 自动分配颜色
```

### RemoveChannel(string name)

移除数据通道。

**参数**:
- `name`: `string` 通道名称

**示例**:
```csharp
trendChart.RemoveChannel("温度");
```

### AddDataPoint(string channel, double value)

添加数据点。

**参数**:
- `channel`: `string` 通道名称
- `value`: `double` 数据值

**示例**:
```csharp
trendChart.AddDataPoint("温度", 25.5);
trendChart.AddDataPoint("压力", 1.2);
```

### ClearData()

清空所有通道数据。

**示例**:
```csharp
trendChart.ClearData();
```

## 事件

TrendChart控件主要通过属性配置和数据添加进行交互,不提供自定义事件。

## 枚举类型

### LegendPosition

图例位置枚举。

```csharp
public enum LegendPosition
{
    TopLeft,      // 左上角
    TopRight,     // 右上角
    BottomLeft,   // 左下角
    BottomRight   // 右下角
}
```

## 关联类型

### ChannelConfig

数据通道配置类。

**主要属性**:
- `Name`: `string` - 通道名称
- `Color`: `Color` - 通道颜色

**示例**:
```csharp
var config = new ChannelConfig("温度传感器#1", Color.Blue);
Console.WriteLine($"通道: {config.Name}");
Console.WriteLine($"颜色: {config.Color}");
```

### DataBuffer

数据缓冲区类(内部使用)。

**主要功能**:
- 循环缓冲区实现
- 自动管理容量
- 提供Min/Max统计

## 完整示例

### 多通道实时监控

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;
using IndustrialControls.Controls.DataVisualization;

namespace MultiChannelMonitor
{
    public partial class MainForm : Form
    {
        private TrendChart trendChart;
        private Timer dataTimer;
        private Random random;

        public MainForm()
        {
            InitializeComponent();
            random = new Random();
            InitializeTrendChart();
            StartDataSimulation();
        }

        private void InitializeTrendChart()
        {
            trendChart = new TrendChart
            {
                Dock = DockStyle.Fill,
                Title = "多通道实时监控",
                VisiblePoints = 300,
                BufferCapacity = 5000,
                AutoScaleY = true,
                ShowGrid = true,
                GridLineCount = 8,
                LineThickness = 2,
                ShowLegend = true,
                LegendPosition = LegendPosition.TopRight,
                ShowLegendBackground = true,
                TimeInterval = 0.1 // 100ms一个点
            };

            // 添加多个通道
            trendChart.AddChannel("温度A", Color.FromArgb(59, 130, 246)); // 蓝色
            trendChart.AddChannel("温度B", Color.FromArgb(16, 185, 129)); // 绿色
            trendChart.AddChannel("压力", Color.FromArgb(239, 68, 68));   // 红色
            trendChart.AddChannel("流量", Color.FromArgb(245, 158, 11));  // 橙色

            this.Controls.Add(trendChart);
        }

        private void StartDataSimulation()
        {
            dataTimer = new Timer { Interval = 100 }; // 100ms
            dataTimer.Tick += (s, e) =>
            {
                var now = DateTime.Now;
                double seconds = now.TimeOfDay.TotalSeconds;

                // 模拟温度A(正弦波 + 噪声)
                double tempA = 50 + Math.Sin(seconds * 0.5) * 15 + random.NextDouble() * 3;
                trendChart.AddDataPoint("温度A", tempA);

                // 模拟温度B(余弦波 + 噪声)
                double tempB = 45 + Math.Cos(seconds * 0.3) * 12 + random.NextDouble() * 2;
                trendChart.AddDataPoint("温度B", tempB);

                // 模拟压力(缓慢变化)
                double pressure = 1.5 + Math.Sin(seconds * 0.1) * 0.3 + random.NextDouble() * 0.05;
                trendChart.AddDataPoint("压力", pressure);

                // 模拟流量(脉冲式)
                double flow = 100 + Math.Sin(seconds * 2) * 20 + random.NextDouble() * 10;
                if (random.Next(100) < 5) flow += 50; // 随机尖峰
                trendChart.AddDataPoint("流量", flow);
            };
            dataTimer.Start();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            trendChart.ClearData();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (dataTimer.Enabled)
            {
                dataTimer.Stop();
                btnPause.Text = "继续";
            }
            else
            {
                dataTimer.Start();
                btnPause.Text = "暂停";
            }
        }

        private void chkAutoScale_CheckedChanged(object sender, EventArgs e)
        {
            trendChart.AutoScaleY = chkAutoScale.Checked;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // 导出数据(需要自行实现)
            ExportDataToCsv(trendChart);
        }

        private void ExportDataToCsv(TrendChart chart)
        {
            // 示例:导出数据到CSV
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV文件|*.csv";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // 实现导出逻辑
                    MessageBox.Show("导出功能待实现");
                }
            }
        }
    }
}
```

### 与通讯控件配合

```csharp
using IndustrialControls.Controls.Communication;
using IndustrialControls.Controls.DataVisualization;

// 接收设备数据并显示趋势
commControl.StringReceived += (sender, data) =>
{
    // 解析数据协议
    // 假设格式: "TEMP:25.5,PRESS:1.23,FLOW:100"
    var parts = data.Split(',');
    foreach (var part in parts)
    {
        var kv = part.Split(':');
        if (kv.Length == 2)
        {
            string channel = kv[0].Trim();
            double value = double.Parse(kv[1].Trim());
            
            trendChart.AddDataPoint(channel, value);
        }
    }
};

// 预设通道
trendChart.AddChannel("TEMP", Color.Red);
trendChart.AddChannel("PRESS", Color.Blue);
trendChart.AddChannel("FLOW", Color.Green);
```

## 视觉效果

### 网格系统

- 水平网格线:按Y轴刻度分布
- 垂直网格线:按X轴刻度分布
- 虚线样式,半透明显示

### 图例系统

- 显示通道名称和对应颜色
- 支持四个角落位置
- 可选半透明背景

### 时间轴

X轴自动显示时间标签,根据时间跨度智能选择格式:
- **< 60秒**: `HH:mm:ss`
- **< 1小时**: `HH:mm`
- **≥ 1小时**: `MM-dd HH:mm`

### 曲线渲染

- 抗锯齿线条
- 圆角连接
- 可配置粗细
- 自动裁剪到图表区域

## 注意事项

1. **性能优化**: 高频数据时增大 `BufferCapacity`,合理设置 `VisiblePoints`
2. **线程安全**: 可在后台线程调用 `AddDataPoint`
3. **自动缩放**: `AutoScaleY = true` 时会根据数据动态调整Y轴
4. **时间间隔**: 设置 `TimeInterval` 以匹配数据采样频率
5. **设计器支持**: 支持Visual Studio设计器配置通道
6. **内存管理**: 超出 `BufferCapacity` 的旧数据自动丢弃

## 最佳实践

1. **采样频率**: 根据实际需求设置定时器间隔,避免过度刷新
2. **通道数量**: 建议不超过6个通道,保持图表清晰
3. **颜色选择**: 使用对比度高的颜色区分不同通道
4. **Y轴范围**: 已知数据范围时关闭 `AutoScaleY`,手动设置固定范围
5. **数据导出**: 定期导出历史数据用于分析
6. **性能监控**: 大量数据时监控CPU和内存使用

## 相关控件

- **CommunicationControl**: 通讯控件,接收实时数据
- **StatusIndicator**: 状态指示器,显示设备状态
- **NumericInputBox**: 数值输入框,设置报警阈值
- **AlarmDisplay**: 报警显示,超限报警
