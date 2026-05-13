# TrendChart（趋势图）

**命名空间**：`IndustrialControls.Controls.DataVisualization`  
**类型**：`TrendChart`（多通道实时/缓冲趋势曲线）

## 用途

多路模拟量随时间变化曲线；通过通道名增删曲线，向指定通道追加采样点，内部环形缓冲控制容量。

## 相关类型

- `ChannelConfig`：通道显示名与颜色（设计器可展开编辑）。  
- `LegendPosition`：图例位置（`TopLeft`、`TopRight` 等）。

## 常用属性（节选）

| 属性 | 类型 | 说明 |
|------|------|------|
| `Title` | `string` | 图表标题 |
| `ShowGrid` | `bool` | 网格 |
| `ShowXAxis` / `ShowYAxis` | `bool` | 坐标轴 |
| `ShowLegend` | `bool` | 图例 |
| `ShowLegendBackground` | `bool` | 图例背景 |
| `AutoScaleY` | `bool` | Y 轴自动缩放 |
| `YMin` / `YMax` | `double` | 手动 Y 范围（与自动缩放配合以源码为准） |
| `VisiblePoints` | `int` | 可见点数 |
| `BufferCapacity` | `int` | 缓冲容量 |
| `XTickCount` / `YTickCount` | `int` | 刻度数量 |
| `LeftMargin` | `int` | 左边距 |
| `LineThickness` | `int` | 线宽 |
| `GridLineCount` | `int` | 网格线数量 |
| `TimeInterval` | `double` | 时间轴间隔相关（秒，详见设计器说明） |

## 方法

| 方法 | 说明 |
|------|------|
| `AddChannel(name, color?)` | 增加通道 |
| `RemoveChannel(name)` | 移除通道 |
| `AddDataPoint(channel, value)` | 向某通道追加一个数据点（时间由控件内部推进） |
| `ClearData()` | 清空数据 |

## 代码示例

```csharp
using IndustrialControls.Controls.DataVisualization;

var chart = new TrendChart { Size = new Size(500, 260) };
chart.AddChannel("温度", Color.LimeGreen);
chart.AddChannel("压力", Color.DodgerBlue);

chart.AddDataPoint("温度", 25.4);
chart.AddDataPoint("压力", 1.02);
```

## 另见

- [文档索引](./README.md)  
