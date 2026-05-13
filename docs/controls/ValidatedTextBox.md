# ValidatedTextBox（验证文本框）

**命名空间**：`IndustrialControls.Controls.DataInput`  
**类型**：`ValidatedTextBox`（内置正则/预设校验、错误行、占位符）

## 用途

用户名、IP、邮箱、手机号等需要格式校验的文本输入；支持设计器中选择预设或手写正则。

## 枚举

### `ValidationMode`

| 值 | 说明 |
|----|------|
| `Realtime` | 输入过程中校验 |
| `OnLeave` | 失焦时校验（默认） |

### `ValidationPreset`

| 值 | 说明 |
|----|------|
| `None` | 无预设，使用 `ValidationPattern` 自定义 |
| `Email` | 邮箱 |
| `MobilePhone` | 中国大陆手机 |
| `IdCard` | 18 位身份证 |
| `Url` | URL |
| `Number` | 数字 |
| `StrongPassword` | 强密码规则 |
| `IpAddress` | IPv4 形式 |
| `ChineseName` | 中文姓名 |
| `PostalCode` | 6 位邮编 |

静态类 `ValidationPresets.GetConfig(preset)` 可取得预设对应的正则、错误文案与占位符（控件在设置 `Preset` 时会自动应用）。

## 常用属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `Text` | `string` | 文本内容 |
| `ValidationPattern` | `string` | 正则（`Preset` 为 `None` 或叠加时使用） |
| `Required` | `bool` | 是否必填 |
| `Mode` | `ValidationMode` | 校验触发时机 |
| `ErrorMessage` | `string` | 失败提示（预设会填充） |
| `Placeholder` | `string` | 占位提示 |
| `Preset` | `ValidationPreset` | 设计器下拉选择预设 |
| `IsValid` | `bool` | 当前是否有效（只读） |

## 事件

| 事件 | 说明 |
|------|------|
| `ValidationChanged` | `EventHandler<bool>`，校验结果变化 |

## 方法

| 方法 | 说明 |
|------|------|
| `Validate()` | 主动执行校验，返回是否通过 |

## 代码示例

```csharp
using IndustrialControls.Controls.DataInput;

var box = new ValidatedTextBox
{
    Preset = ValidationPreset.IpAddress,
    Mode = ValidationMode.OnLeave
};

box.ValidationChanged += (s, ok) => { if (!ok) { /* 禁用确定按钮等 */ } };
```

## 另见

- [文档索引](./README.md)  
