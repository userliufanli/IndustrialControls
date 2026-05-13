# DataInputPanel（标签 + 数值行）

**命名空间**：`IndustrialControls.Controls.DataInput`  
**类型**：`DataInputPanel`（左侧标签、中间数值输入、右侧单位的标准一行布局）

## 用途

参数页中单行「名称 | 数值 | 单位」的快速排版；失焦时解析文本并写回 `Value`，非法输入会恢复显示。

## 常用属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `LabelText` | `string` | 左侧标签 |
| `Unit` | `string` | 右侧单位（空则隐藏单位区） |
| `Value` | `double` | 当前数值（写入时钳制到 Min~Max） |
| `Minimum` / `Maximum` | `double` | 允许范围 |
| `DecimalPlaces` | `int` | 显示小数位 |

## 事件

| 事件 | 说明 |
|------|------|
| `ValueChanged` | `EventHandler<double>` |

## 行为说明

- 中间为文本框，仅允许数字、负号与小数点；失焦时 `TryParse` 成功则更新 `Value`，失败则按当前 `Value` 刷新显示。  
- 布局随 `Unit` 文本宽度变化，设计器改父容器宽度时可自动重排。

## 代码示例

```csharp
using IndustrialControls.Controls.DataInput;

var row = new DataInputPanel
{
    LabelText = "目标温度",
    Unit = "℃",
    Minimum = 0,
    Maximum = 300,
    DecimalPlaces = 1,
    Value = 25
};

row.ValueChanged += (s, v) => { /* 写 PLC 或参数 */ };
```

## 另见

- [NumericInputBox](./NumericInputBox.md)（带步进按钮的数值框）  
- [文档索引](./README.md)  
