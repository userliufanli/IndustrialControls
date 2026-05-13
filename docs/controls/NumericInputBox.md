# NumericInputBox（数值输入框）

**命名空间**：`IndustrialControls.Controls.DataInput`  
**类型**：`NumericInputBox`（带上下限、步进按钮、单位与小数位）

## 用途

设定转速、温度设定值、延时等数值参数；内部做范围钳制与格式显示。

## 常用属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `Value` | `double` | 当前值（受 Min/Max 约束） |
| `Minimum` / `Maximum` | `double` | 允许范围 |
| `Step` | `double` | 步进（≥ 0.001） |
| `DecimalPlaces` | `int` | 小数位数 |
| `Unit` | `string` | 单位文案 |
| `ShowButtons` | `bool` | 是否显示增减按钮 |
| `IsValid` | `bool` | 当前是否通过校验（只读） |

## 事件

| 事件 | 说明 |
|------|------|
| `ValueChanged` | `EventHandler<double>` |
| `ValidationFailed` | `EventHandler<string>` |

## 代码示例

```csharp
using IndustrialControls.Controls.DataInput;

var inputBox = new NumericInputBox
{
    Minimum = 0,
    Maximum = 1000,
    Step = 10,
    DecimalPlaces = 2,
    Unit = "rpm"
};

inputBox.ValueChanged += (s, v) => { /* */ };
```

## 另见

- [DataInputPanel](./DataInputPanel.md)（单行标签+数值布局）  
- [文档索引](./README.md)  
