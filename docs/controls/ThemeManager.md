# ThemeManager（主题管理器）

**命名空间**：`IndustrialControls.Theme`  
**类型**：`ThemeManager`（单例）、`ITheme`、`ThemeColors`、内置 `FlatLightTheme` / `FlatDarkTheme`

## 用途

统一上位机界面配色与字体；工业控件在绘制或加载时会读取当前主题颜色（如状态色、报警色、边框色）。

## 常用成员

| 成员 | 说明 |
|------|------|
| `ThemeManager.Instance` | 单例 |
| `CurrentTheme` | 当前 `ITheme` |
| `ApplyTheme<T>()` | 切换主题类型 |
| `RegisterTheme` / `GetRegisteredThemes` | 注册自定义主题 |

## ThemeColors（节选）

含 `Primary`、`Background`、`Surface`、`TextPrimary`、`Border`、`Running`、`Fault`、各级报警色等，完整列表见源码 `ThemeColors.cs` 或设计器绑定。

## 代码示例

```csharp
using IndustrialControls.Theme;

ThemeManager.Instance.ApplyTheme<FlatDarkTheme>();

var colors = ThemeManager.Instance.CurrentTheme.Colors;
someButton.BackColor = colors.Primary;
someButton.ForeColor = colors.TextOnPrimary;
```

## 另见

- [StatusIndicator](./StatusIndicator.md)、[AlarmDisplay](./AlarmDisplay.md)（语义色与主题一致）  
- [文档索引](./README.md)  
