# AlarmDisplay（报警显示）

**命名空间**：`IndustrialControls.Controls.Alarm`  
**类型**：`AlarmDisplay`（报警列表、分级、确认与筛选）

## 用途

集中展示工艺或设备报警，支持等级颜色区分、确认、按时间筛选与数量上限控制。

## 枚举 `AlarmLevel`

| 值 | 典型用途 |
|----|----------|
| `Emergency` | 紧急 |
| `Important` | 重要 |
| `General` | 一般 |
| `Info` | 提示 |

## 常用属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `ItemHeight` | `int` | 单行高度 |
| `MaxAlarms` | `int` | 最大保留条数 |
| `AutoScroll` | `bool` | 新报警是否自动滚入视野 |
| `TotalCount` | `int` | 总条数（只读） |
| `UnacknowledgedCount` | `int` | 未确认条数（只读） |
| `EmergencyCount` | `int` | 紧急条数（只读） |

## 事件

| 事件 | 说明 |
|------|------|
| `AlarmAcknowledged` | `EventHandler<AlarmItem>` |
| `AlarmAdded` | `EventHandler<AlarmItem>` |
| `AlarmCleared` | `EventHandler` |

## 常用方法

| 方法 | 说明 |
|------|------|
| `AddAlarm(AlarmItem)` / `AddAlarm(AlarmLevel, message, source)` | 添加报警 |
| `AcknowledgeSelected(user)` / `AcknowledgeAll(user)` | 确认 |
| `ClearAcknowledged()` / `ClearAll()` | 清除 |
| `ApplyTimeFilter(start, end)` / `ClearTimeFilter()` | 时间筛选 |

## 代码示例

```csharp
using IndustrialControls.Controls.Alarm;

var alarmDisplay = new AlarmDisplay
{
    Size = new Size(400, 300),
    AutoScroll = true
};

alarmDisplay.AddAlarm(AlarmLevel.Emergency, "电机过载", "设备A");
alarmDisplay.AcknowledgeAll();
```

## 另见

- [ThemeManager](./ThemeManager.md)（报警配色）  
- [文档索引](./README.md)  
