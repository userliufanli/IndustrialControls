# 状态指示器 (StatusIndicator) API 使用手册

## 概述

`StatusIndicator` 是一个直观的设备状态指示控件,支持多种颜色状态、闪烁动画和状态历史记录,适用于工业自动化系统的设备状态监控。

## 命名空间

```csharp
IndustrialControls.Controls.StatusIndicator
```

## 继承关系

```
System.Object
  └─ System.MarshalByRefObject
     └─ System.ComponentModel.Component
        └─ System.Windows.Forms.Control
           └─ IndustrialControls.Core.DoubleBufferedControl
              └─ StatusIndicator
```

## 快速开始

### 1. 基本使用

```csharp
using IndustrialControls.Controls.StatusIndicator;

// 创建状态指示器
var indicator = new StatusIndicator
{
    Size = new Size(80, 80),
    Location = new Point(10, 10),
    State = IndicatorState.Stopped,
    Label = "电机A",
    ShowLabel = true
};

// 添加到窗体
this.Controls.Add(indicator);
```

### 2. 状态切换

```csharp
// 切换到运行状态
indicator.State = IndicatorState.Running;

// 切换到故障状态(自动闪烁)
indicator.State = IndicatorState.Fault;

// 切换到警告状态(自动闪烁)
indicator.State = IndicatorState.Warning;

// 切换到空闲状态
indicator.State = IndicatorState.Idle;

// 自定义颜色
indicator.State = IndicatorState.Custom;
indicator.CustomColor = Color.Purple;
```

### 3. 启用闪烁

```csharp
// 启用闪烁效果
indicator.EnableBlink = true;
indicator.BlinkInterval = 500; // 闪烁间隔500ms

// 闪烁仅在 Fault、Warning、Running 状态下生效
indicator.State = IndicatorState.Fault; // 会自动闪烁
```

### 4. 监听状态变更

```csharp
indicator.StateChanged += (sender, newState) =>
{
    Console.WriteLine($"状态变更: {newState}");
    
    switch (newState)
    {
        case IndicatorState.Running:
            Console.WriteLine("设备运行中");
            break;
        case IndicatorState.Fault:
            Console.WriteLine("设备故障!");
            break;
    }
};
```

### 5. 查看历史记录

```csharp
// 查看状态历史
foreach (var record in indicator.History)
{
    Console.WriteLine($"时间: {record.Timestamp}");
    Console.WriteLine($"状态: {record.State}");
    Console.WriteLine($"持续: {record.Duration}");
}

// 清除历史
indicator.ClearHistory();
```

## 属性

### State

- **类型**: `IndicatorState`
- **默认值**: `IndicatorState.Stopped`
- **说明**: 指示器当前状态
- **示例**:
  ```csharp
  indicator.State = IndicatorState.Running;
  ```

### Shape

- **类型**: `IndicatorShape`
- **默认值**: `IndicatorShape.Circle`
- **说明**: 指示器形状(圆形/方形/圆角矩形)
- **示例**:
  ```csharp
  indicator.Shape = IndicatorShape.RoundedRectangle;
  ```

### EnableBlink

- **类型**: `bool`
- **默认值**: `false`
- **说明**: 是否启用闪烁效果(仅在Fault/Warning/Running状态下生效)
- **示例**:
  ```csharp
  indicator.EnableBlink = true;
  ```

### BlinkInterval

- **类型**: `int`
- **默认值**: `500`
- **说明**: 闪烁间隔时间(毫秒)
- **示例**:
  ```csharp
  indicator.BlinkInterval = 300; // 快速闪烁
  ```

### CustomColor

- **类型**: `Color`
- **默认值**: `Color.Gray`
- **说明**: 自定义状态颜色(State为Custom时使用)
- **示例**:
  ```csharp
  indicator.State = IndicatorState.Custom;
  indicator.CustomColor = Color.FromArgb(156, 39, 176); // 紫色
  ```

### Label

- **类型**: `string`
- **默认值**: `""`
- **说明**: 指示器标签文本
- **示例**:
  ```csharp
  indicator.Label = "泵机#1";
  ```

### ShowLabel

- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否显示标签
- **示例**:
  ```csharp
  indicator.ShowLabel = false; // 隐藏标签
  ```

### MaxHistoryCount

- **类型**: `int`
- **默认值**: `100`
- **说明**: 最大状态历史记录条数
- **示例**:
  ```csharp
  indicator.MaxHistoryCount = 200;
  ```

### History

- **类型**: `IReadOnlyList<StatusHistoryRecord>` (只读)
- **说明**: 状态历史记录列表
- **示例**:
  ```csharp
  var history = indicator.History;
  Console.WriteLine($"历史记录数: {history.Count}");
  ```

## 方法

### ClearHistory()

清除所有状态历史记录。

**示例**:
```csharp
indicator.ClearHistory();
```

## 事件

### StateChanged

状态变更时触发。

**事件参数**: `EventHandler<IndicatorState>`

**示例**:
```csharp
indicator.StateChanged += (sender, newState) =>
{
    // 更新数据库状态
    UpdateDeviceStatus(deviceId, newState);
    
    // 故障时报警
    if (newState == IndicatorState.Fault)
    {
        ShowAlarmDialog("设备故障,请检查!");
    }
};
```

## 枚举类型

### IndicatorState

指示器状态枚举。

```csharp
public enum IndicatorState
{
    Running,   // 运行中(绿色)
    Stopped,   // 已停止(灰色)
    Fault,     // 故障(红色,自动闪烁)
    Warning,   // 警告(橙色,自动闪烁)
    Idle,      // 空闲(黄色)
    Custom     // 自定义颜色
}
```

### IndicatorShape

指示器形状枚举。

```csharp
public enum IndicatorShape
{
    Circle,           // 圆形
    Square,           // 方形
    RoundedRectangle  // 圆角矩形
}
```

## 关联类型

### StatusHistoryRecord

状态历史记录类。

**主要属性**:
- `State`: `IndicatorState` - 状态
- `Timestamp`: `DateTime` - 状态变更时间
- `Duration`: `TimeSpan` - 状态持续时间

**示例**:
```csharp
var record = indicator.History[0];
Console.WriteLine($"状态: {record.State}");
Console.WriteLine($"时间: {record.Timestamp}");
Console.WriteLine($"持续: {record.Duration.TotalSeconds}秒");
```

## 颜色映射

状态指示器根据当前主题自动映射颜色:

| 状态 | 颜色说明 | 默认颜色(Dark主题) |
|------|---------|-------------------|
| Running | 运行中 | #10B981 (绿色) |
| Stopped | 已停止 | #6B7280 (灰色) |
| Fault | 故障 | #EF4444 (红色) |
| Warning | 警告 | #F59E0B (橙色) |
| Idle | 空闲 | #8B5CF6 (紫色) |
| Custom | 自定义 | CustomColor属性值 |

## 完整示例

### 设备监控面板

```csharp
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using IndustrialControls.Controls.StatusIndicator;

namespace DeviceMonitor
{
    public partial class MainForm : Form
    {
        private Dictionary<string, StatusIndicator> deviceIndicators;
        private Timer updateTimer;

        public MainForm()
        {
            InitializeComponent();
            InitializeIndicators();
            StartMonitoring();
        }

        private void InitializeIndicators()
        {
            deviceIndicators = new Dictionary<string, StatusIndicator>();

            // 创建多个设备指示器
            string[] devices = { "电机A", "电机B", "泵机#1", "泵机#2", "压缩机" };
            
            for (int i = 0; i < devices.Length; i++)
            {
                var indicator = new StatusIndicator
                {
                    Size = new Size(100, 100),
                    Location = new Point(20 + (i % 3) * 120, 20 + (i / 3) * 120),
                    State = IndicatorState.Stopped,
                    Label = devices[i],
                    Shape = IndicatorShape.Circle,
                    EnableBlink = true,
                    BlinkInterval = 500,
                    MaxHistoryCount = 50
                };

                indicator.StateChanged += OnDeviceStateChanged;
                
                deviceIndicators[devices[i]] = indicator;
                this.Controls.Add(indicator);
            }
        }

        private void OnDeviceStateChanged(object sender, IndicatorState newState)
        {
            var indicator = (StatusIndicator)sender;
            
            // 记录状态变更日志
            Console.WriteLine($"[{DateTime.Now}] {indicator.Label} -> {newState}");
            
            // 故障时弹窗报警
            if (newState == IndicatorState.Fault)
            {
                MessageBox.Show(
                    $"{indicator.Label} 发生故障!",
                    "设备报警",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void StartMonitoring()
        {
            updateTimer = new Timer { Interval = 2000 };
            updateTimer.Tick += (s, e) =>
            {
                // 模拟设备状态变化
                var random = new Random();
                var states = new[] {
                    IndicatorState.Running,
                    IndicatorState.Stopped,
                    IndicatorState.Idle,
                    IndicatorState.Fault,
                    IndicatorState.Warning
                };

                foreach (var kvp in deviceIndicators)
                {
                    // 80%概率保持当前状态,20%概率切换
                    if (random.Next(100) < 20)
                    {
                        var newState = states[random.Next(states.Length)];
                        kvp.Value.State = newState;
                    }
                }
            };
            updateTimer.Start();
        }

        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            // 查看某个设备的历史记录
            var indicator = deviceIndicators["电机A"];
            
            string historyText = "电机A 状态历史:\n\n";
            foreach (var record in indicator.History)
            {
                historyText += $"{record.Timestamp:HH:mm:ss} - {record.State}";
                if (record.Duration.TotalSeconds > 0)
                {
                    historyText += $" (持续{record.Duration.TotalSeconds:F0}秒)";
                }
                historyText += "\n";
            }
            
            MessageBox.Show(historyText, "状态历史", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
```

### 与通讯控件配合使用

```csharp
using IndustrialControls.Controls.Communication;
using IndustrialControls.Controls.StatusIndicator;

// 通讯状态指示
var commIndicator = new StatusIndicator
{
    Size = new Size(60, 60),
    State = IndicatorState.Stopped,
    Label = "通讯",
    Shape = IndicatorShape.RoundedRectangle
};

// 监听通讯状态
commControl.StateChanged += (sender, state) =>
{
    switch (state)
    {
        case CommunicationState.Connected:
            commIndicator.State = IndicatorState.Running;
            break;
        case CommunicationState.Connecting:
            commIndicator.State = IndicatorState.Idle;
            break;
        case CommunicationState.Error:
            commIndicator.State = IndicatorState.Fault;
            break;
        default:
            commIndicator.State = IndicatorState.Stopped;
            break;
    }
};
```

## 视觉效果

### 高光效果

指示器自带高光渲染,呈现立体感:
- 顶部1/3区域有渐变高光
- 增强视觉层次感和现代感

### 闪烁动画

在以下状态下自动闪烁:
- **Fault**: 红色闪烁,提醒故障
- **Warning**: 橙色闪烁,提醒警告
- **Running**: 绿色闪烁(可选),表示运行中

闪烁时透明度从100%降到60%,形成呼吸效果。

### 主题适配

自动跟随系统主题切换颜色:
- **FlatDark**: 深色背景配色
- **FlatLight**: 浅色背景配色

## 注意事项

1. **闪烁控制**: `EnableBlink` 设置为 `true` 后,仅在特定状态自动闪烁
2. **历史记录**: 超出 `MaxHistoryCount` 时自动删除最旧的记录
3. **持续时间**: 前一条记录的 `Duration` 在新状态变更时自动计算
4. **线程安全**: 可在后台线程设置 `State` 属性
5. **设计器支持**: 支持Visual Studio设计器拖放和属性配置

## 最佳实践

1. **状态映射**: 建立清晰的设备状态到IndicatorState的映射关系
2. **闪烁使用**: 仅在需要提醒的状态启用闪烁,避免视觉疲劳
3. **历史分析**: 定期分析 `History` 数据,统计设备运行时间
4. **颜色统一**: 使用 `Custom` 状态时保持颜色语义一致性
5. **标签命名**: 使用简洁明确的设备名称作为 `Label`

## 相关控件

- **AlarmDisplay**: 报警显示,配合显示详细报警信息
- **DeviceControlButton**: 设备控制按钮,用于设备启停操作
- **CommunicationControl**: 通讯控件,监控通讯连接状态
- **TrendChart**: 趋势图,可视化设备运行数据
