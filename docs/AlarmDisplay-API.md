# 报警显示控件 (AlarmDisplay) API 使用手册

## 概述

`AlarmDisplay` 是一个专业的工业报警显示控件,支持分级报警管理、自动排序、确认清除等功能,适用于工业自动化监控系统的报警管理界面。

## 命名空间

```csharp
IndustrialControls.Controls.Alarm
```

## 继承关系

```
System.Object
  └─ System.MarshalByRefObject
     └─ System.ComponentModel.Component
        └─ System.Windows.Forms.Control
           └─ IndustrialControls.Core.DoubleBufferedControl
              └─ AlarmDisplay
```

## 快速开始

### 1. 基本使用

```csharp
using IndustrialControls.Controls.Alarm;

// 创建报警显示控件
var alarmDisplay = new AlarmDisplay
{
    Size = new Size(600, 400),
    Location = new Point(10, 10),
    MaxAlarms = 500,
    AutoScroll = true
};

// 添加到窗体
this.Controls.Add(alarmDisplay);
```

### 2. 添加报警

```csharp
// 方式一:使用简便方法
alarmDisplay.AddAlarm(AlarmLevel.Emergency, "温度超限报警", "温度传感器#1");
alarmDisplay.AddAlarm(AlarmLevel.Important, "压力异常", "压力传感器#2");
alarmDisplay.AddAlarm(AlarmLevel.General, "设备振动偏高", "振动传感器#3");
alarmDisplay.AddAlarm(AlarmLevel.Info, "系统正常运行", "系统监控");

// 方式二:使用 AlarmItem 对象
var alarm = new AlarmItem(AlarmLevel.Emergency, "电机过载保护触发", "电机控制器");
alarmDisplay.AddAlarm(alarm);
```

### 3. 处理报警事件

```csharp
// 报警确认事件
alarmDisplay.AlarmAcknowledged += (sender, alarm) =>
{
    Console.WriteLine($"报警已确认: {alarm.Message}");
    Console.WriteLine($"确认人: {alarm.AcknowledgedBy}");
    Console.WriteLine($"确认时间: {alarm.AcknowledgedTime}");
};

// 新报警添加事件
alarmDisplay.AlarmAdded += (sender, alarm) =>
{
    Console.WriteLine($"新报警: {alarm.Message}");
    // 可以在这里播放报警声音或发送通知
};

// 报警清除事件
alarmDisplay.AlarmCleared += (sender, e) =>
{
    Console.WriteLine("报警已清除");
};
```

### 4. 报警操作

```csharp
// 确认选中的报警(双击报警条目)
alarmDisplay.AcknowledgeSelected("操作员张三");

// 确认所有未确认报警
alarmDisplay.AcknowledgeAll("操作员张三");

// 清除所有已确认的报警
alarmDisplay.ClearAcknowledged();

// 清除所有报警
alarmDisplay.ClearAll();
```

### 5. 时间筛选

```csharp
// 筛选特定时间范围的报警
var startTime = new DateTime(2024, 1, 1, 8, 0, 0);
var endTime = new DateTime(2024, 1, 1, 17, 0, 0);
alarmDisplay.ApplyTimeFilter(startTime, endTime);

// 清除筛选,显示所有报警
alarmDisplay.ClearTimeFilter();
```

## 属性

### ItemHeight

- **类型**: `int`
- **默认值**: `32`
- **说明**: 每条报警的显示高度(像素)
- **示例**:
  ```csharp
  alarmDisplay.ItemHeight = 40; // 增加行高
  ```

### MaxAlarms

- **类型**: `int`
- **默认值**: `1000`
- **说明**: 最大保留报警条数,超出后自动删除最旧的报警
- **示例**:
  ```csharp
  alarmDisplay.MaxAlarms = 2000; // 增加容量
  ```

### AutoScroll

- **类型**: `bool`
- **默认值**: `true`
- **说明**: 新报警是否自动滚动到可见区域
- **示例**:
  ```csharp
  alarmDisplay.AutoScroll = false; // 禁用自动滚动
  ```

### TotalCount

- **类型**: `int` (只读)
- **说明**: 当前报警总数
- **示例**:
  ```csharp
  int total = alarmDisplay.TotalCount;
  ```

### UnacknowledgedCount

- **类型**: `int` (只读)
- **说明**: 未确认报警数量
- **示例**:
  ```csharp
  int unacked = alarmDisplay.UnacknowledgedCount;
  ```

### EmergencyCount

- **类型**: `int` (只读)
- **说明**: 紧急级别未确认报警数量
- **示例**:
  ```csharp
  int emergency = alarmDisplay.EmergencyCount;
  ```

## 方法

### AddAlarm(AlarmItem alarm)

添加报警条目。

**参数**:
- `alarm`: `AlarmItem` 报警对象

**示例**:
```csharp
var alarm = new AlarmItem(AlarmLevel.Emergency, "紧急报警", "设备A");
alarmDisplay.AddAlarm(alarm);
```

### AddAlarm(AlarmLevel level, string message, string source = "")

简便方法,直接添加报警。

**参数**:
- `level`: `AlarmLevel` 报警级别
- `message`: `string` 报警消息
- `source`: `string` 报警来源(可选)

**示例**:
```csharp
alarmDisplay.AddAlarm(AlarmLevel.Important, "压力异常", "传感器#1");
```

### AcknowledgeSelected(string user = "")

确认选中的报警。

**参数**:
- `user`: `string` 确认人(可选)

**示例**:
```csharp
alarmDisplay.AcknowledgeSelected("操作员张三");
```

### AcknowledgeAll(string user = "")

确认所有未确认报警。

**参数**:
- `user`: `string` 确认人(可选)

**示例**:
```csharp
alarmDisplay.AcknowledgeAll("操作员李四");
```

### ClearAcknowledged()

清除所有已确认的报警。

**示例**:
```csharp
alarmDisplay.ClearAcknowledged();
```

### ClearAll()

清除所有报警。

**示例**:
```csharp
alarmDisplay.ClearAll();
```

### ApplyTimeFilter(DateTime? startTime, DateTime? endTime)

应用时间筛选。

**参数**:
- `startTime`: `DateTime?` 开始时间(null表示不限制)
- `endTime`: `DateTime?` 结束时间(null表示不限制)

**示例**:
```csharp
// 查看今天8点到17点的报警
var start = DateTime.Today.AddHours(8);
var end = DateTime.Today.AddHours(17);
alarmDisplay.ApplyTimeFilter(start, end);
```

### ClearTimeFilter()

清除时间筛选,显示所有报警。

**示例**:
```csharp
alarmDisplay.ClearTimeFilter();
```

## 事件

### AlarmAcknowledged

报警确认时触发。

**事件参数**: `EventHandler<AlarmItem>`

**示例**:
```csharp
alarmDisplay.AlarmAcknowledged += (sender, alarm) =>
{
    // 记录确认日志
    Logger.Info($"报警 [{alarm.Id}] 已被 {alarm.AcknowledgedBy} 确认");
};
```

### AlarmAdded

新报警添加时触发。

**事件参数**: `EventHandler<AlarmItem>`

**示例**:
```csharp
alarmDisplay.AlarmAdded += (sender, alarm) =>
{
    // 紧急报警时播放声音
    if (alarm.Level == AlarmLevel.Emergency)
    {
        System.Media.SystemSounds.Exclamation.Play();
    }
};
```

### AlarmCleared

报警清除时触发。

**事件参数**: `EventHandler`

**示例**:
```csharp
alarmDisplay.AlarmCleared += (sender, e) =>
{
    Console.WriteLine("报警列表已清空");
};
```

## 关联类型

### AlarmLevel 枚举

报警级别定义。

```csharp
public enum AlarmLevel
{
    Emergency,  // 紧急(红色)
    Important,  // 重要(橙色)
    General,    // 一般(黄色)
    Info        // 信息(蓝色)
}
```

### AlarmItem 类

报警条目数据模型。

**主要属性**:
- `Id`: `string` - 唯一标识
- `Level`: `AlarmLevel` - 报警级别
- `Message`: `string` - 报警消息
- `Source`: `string` - 报警来源
- `Timestamp`: `DateTime` - 触发时间
- `IsAcknowledged`: `bool` - 是否已确认
- `AcknowledgedTime`: `DateTime?` - 确认时间
- `AcknowledgedBy`: `string` - 确认人
- `IsNew`: `bool` - 是否为新报警(用于闪烁提示)

**主要方法**:
- `Acknowledge(string user = "")`: 确认报警

**示例**:
```csharp
var alarm = new AlarmItem(AlarmLevel.Emergency, "温度超限", "传感器#1");
Console.WriteLine($"报警ID: {alarm.Id}");
Console.WriteLine($"触发时间: {alarm.Timestamp}");

// 确认报警
alarm.Acknowledge("操作员张三");
Console.WriteLine($"确认时间: {alarm.AcknowledgedTime}");
```

## 交互说明

### 鼠标操作

- **单击**: 选中报警条目
- **双击**: 确认选中的报警
- **滚轮**: 滚动查看报警列表

### 排序规则

报警列表按以下规则自动排序:
1. 未确认的报警排在前面
2. 同级别内按时间降序(最新的在前)

### 视觉效果

- **新报警闪烁**: 新添加的报警会闪烁提示
- **等级指示条**: 左侧显示不同颜色的等级指示条
- **确认标记**: 已确认的报警右侧显示 ✓ 标记
- **已确认样式**: 已确认的报警文字变灰

## 完整示例

```csharp
using System;
using System.Windows.Forms;
using IndustrialControls.Controls.Alarm;

namespace AlarmDemo
{
    public partial class MainForm : Form
    {
        private AlarmDisplay alarmDisplay;
        private Timer simulationTimer;

        public MainForm()
        {
            InitializeComponent();
            InitializeAlarmDisplay();
            StartSimulation();
        }

        private void InitializeAlarmDisplay()
        {
            alarmDisplay = new AlarmDisplay
            {
                Dock = DockStyle.Fill,
                MaxAlarms = 1000,
                AutoScroll = true,
                ItemHeight = 35
            };

            // 订阅事件
            alarmDisplay.AlarmAcknowledged += OnAlarmAcknowledged;
            alarmDisplay.AlarmAdded += OnAlarmAdded;

            this.Controls.Add(alarmDisplay);
        }

        private void OnAlarmAcknowledged(object sender, AlarmItem alarm)
        {
            // 记录到数据库或日志
            Console.WriteLine($"[{alarm.Timestamp}] 报警已确认: {alarm.Message}");
        }

        private void OnAlarmAdded(object sender, AlarmItem alarm)
        {
            // 紧急报警时弹窗提醒
            if (alarm.Level == AlarmLevel.Emergency)
            {
                MessageBox.Show($"紧急报警: {alarm.Message}", "报警", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void StartSimulation()
        {
            simulationTimer = new Timer { Interval = 3000 };
            simulationTimer.Tick += (s, e) =>
            {
                var levels = new[] { 
                    AlarmLevel.Emergency, AlarmLevel.Important, 
                    AlarmLevel.General, AlarmLevel.Info 
                };
                var sources = new[] { "传感器#1", "传感器#2", "控制器", "系统" };
                var messages = new[] { 
                    "温度超限", "压力异常", "振动偏高", "通讯中断",
                    "设备启动", "参数调整", "正常运行"
                };

                var random = new Random();
                var level = levels[random.Next(levels.Length)];
                var source = sources[random.Next(sources.Length)];
                var message = messages[random.Next(messages.Length)];

                alarmDisplay.AddAlarm(level, message, source);
            };
            simulationTimer.Start();
        }
    }
}
```

## 注意事项

1. **线程安全**: 控件内部使用锁机制保证线程安全,可在后台线程直接调用 `AddAlarm`
2. **性能优化**: 大量报警时建议合理设置 `MaxAlarms`,避免内存占用过高
3. **设计器支持**: 支持Visual Studio设计器拖放使用
4. **主题适配**: 自动跟随系统主题(FlatDark/FlatLight)
5. **时间筛选**: 筛选不会删除数据,只是临时过滤显示

## 最佳实践

1. **定期清理**: 定时调用 `ClearAcknowledged()` 清理已确认报警
2. **确认记录**: 在 `AlarmAcknowledged` 事件中记录确认人和时间
3. **分级处理**: 根据报警级别采取不同的处理策略
4. **声音提醒**: 在 `AlarmAdded` 中为高级别报警添加声音提醒
5. **数据持久化**: 重要的报警数据应保存到数据库

## 相关控件

- **StatusIndicator**: 状态指示器,可配合显示设备运行状态
- **DeviceControlButton**: 设备控制按钮,用于设备操作
- **CommunicationControl**: 通讯控件,用于设备数据采集
