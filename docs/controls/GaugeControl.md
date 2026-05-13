# GaugeControl（仪表盘）

**命名空间**：`IndustrialControls.Controls.DataVisualization`  
**类型**：`GaugeControl`（圆弧刻度、指针、阈值区与简单动画）

## 用途

单点模拟量展示（压力、温度、负载等），支持警告/危险阈值与指针过渡动画。

## 常用属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `Value` | `double` | 当前值 |
| `Minimum` / `Maximum` | `double` | 量程 |
| `WarningThreshold` | `double` | 警告阈值 |
| `DangerThreshold` | `double` | 危险阈值 |
| `InverseThreshold` | `bool` | 是否反转阈值语义（低值更危险等场景） |
| `Unit` | `string` | 单位 |
| `Title` | `string` | 标题 |
| `StartAngle` / `SweepAngle` | `int` | 圆弧起止角（度） |

（其余外观相关属性可在设计器「外观」分类中查看。）

## 事件

| 事件 | 说明 |
|------|------|
| `ValueChanged` | `EventHandler<double>` |

## 代码示例

```csharp
using IndustrialControls.Controls.DataVisualization;

var gauge = new GaugeControl
{
    Minimum = 0,
    Maximum = 200,
    Unit = "°C",
    WarningThreshold = 70,
    DangerThreshold = 90
};

gauge.Value = 85.5;
```

## 另见

- [IndustrialProgressBar](./IndustrialProgressBar.md)  
- [文档索引](./README.md)  
