# IndustrialProgressBar（工业进度条）

**命名空间**：`IndustrialControls.Controls.DataVisualization`  
**类型**：`IndustrialProgressBar`（分段着色、阈值区、横/竖方向）

## 用途

完成度、负载率等进度展示；可根据 `WarningThreshold` / `DangerThreshold` 改变条上颜色分段。

## 枚举 `ProgressDirection`

| 值 | 说明 |
|----|------|
| `Horizontal` | 横向 |
| `Vertical` | 纵向 |

## 常用属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `Value` | `double` | 当前值 |
| `Minimum` / `Maximum` | `double` | 量程 |
| `WarningThreshold` / `DangerThreshold` | `double` | 警告 / 危险阈值 |
| `InverseThreshold` | `bool` | 反转阈值语义 |
| `ShowValue` | `bool` | 是否绘制当前数值 |
| `Unit` | `string` | 单位 |
| `Label` | `string` | 标签 |
| `Direction` | `ProgressDirection` | 方向 |

## 事件

| 事件 | 说明 |
|------|------|
| `ValueChanged` | `EventHandler<double>` |

## 代码示例

```csharp
using IndustrialControls.Controls.DataVisualization;

var bar = new IndustrialProgressBar
{
    Minimum = 0,
    Maximum = 100,
    Value = 72,
    ShowValue = true,
    Unit = "%"
};
```

## 另见

- [GaugeControl](./GaugeControl.md)  
- [文档索引](./README.md)  
