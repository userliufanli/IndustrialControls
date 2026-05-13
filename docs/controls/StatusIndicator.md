# StatusIndicator（状态指示器）

**命名空间**：`IndustrialControls.Controls.StatusIndicator`  
**类型**：`StatusIndicator`（工业状态灯样式，支持标签、闪烁与历史记录）

## 用途

在设备监控、产线看板等界面中展示设备或工位状态（运行、停止、故障等），可选闪烁与状态切换历史。

## 枚举

### `IndicatorState`

| 值 | 含义 |
|----|------|
| `Stopped` | 停止 |
| `Running` | 运行 |
| `Fault` | 故障 |
| `Warning` | 警告 |
| `Idle` | 空闲 |
| `Custom` | 自定义（配合 `CustomColor`） |

### `IndicatorShape`

| 值 | 含义 |
|----|------|
| `Circle` | 圆形 |
| `Square` | 方形 |
| `RoundedRectangle` | 圆角矩形 |

## 常用属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `State` | `IndicatorState` | 当前状态 |
| `Shape` | `IndicatorShape` | 外观形状 |
| `EnableBlink` | `bool` | 是否闪烁 |
| `BlinkInterval` | `int` | 闪烁间隔（毫秒） |
| `CustomColor` | `Color` | `Custom` 状态时颜色 |
| `Label` | `string` | 标签文案 |
| `ShowLabel` | `bool` | 是否显示标签 |
| `MaxHistoryCount` | `int` | 历史条数上限 |
| `History` | `IReadOnlyList<StatusHistoryRecord>` | 只读历史 |

## 事件

| 事件 | 说明 |
|------|------|
| `StateChanged` | `EventHandler<IndicatorState>`，状态变化 |

## 方法

| 方法 | 说明 |
|------|------|
| `ClearHistory()` | 清空历史 |

## 代码示例

```csharp
using IndustrialControls.Controls.StatusIndicator;

var indicator = new StatusIndicator
{
    State = IndicatorState.Running,
    Label = "电机1",
    EnableBlink = true,
    BlinkInterval = 500,
    Size = new Size(60, 80)
};

indicator.StateChanged += (s, state) => { /* 更新业务或日志 */ };
```

## 另见

- [ThemeManager](./ThemeManager.md)（状态色与全局主题）  
- [文档索引](./README.md)  
