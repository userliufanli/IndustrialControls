# FlatLoginControl（扁平登录与用户管理）

**命名空间**：`IndustrialControls.Controls.Login`

## 组成

| 类型 | 说明 |
|------|------|
| `FlatLoginControl` | **UserControl**：用户名/密码、登录按钮、状态行；通过 `ParameterManager` 指定分组持久化用户列表。 |
| `LoginUserStore` | 对用户列表的增删改查与校验（基于 `ParameterManager` 的 `Section`）。 |
| `LoginUserManagementForm` | **Form**：添加用户、改密、删除；**不应**挂在登录面板上，由宿主在鉴权后打开。 |

## FlatLoginControl 要点

| 成员 | 说明 |
|------|------|
| `ParameterManager` | 为空则使用 `ParameterAccessor.Default` |
| `ParameterGroupName` | 参数分组名，默认 `LoginUsers`，存放用户 JSON |
| `TitleText` | 卡片标题，默认「用户登录」 |
| `LoginSucceeded` | `EventHandler<string>`，参数为解析后的用户名 |
| `LoginFailed` | `EventHandler<string>`，失败原因 |
| `ClearSensitiveFields()` | 清除密码与状态行 |

加载时会按 `ThemeManager` 当前主题配色。

## LoginUserManagementForm

在已确认操作者权限（菜单、角色、口令等）后调用：

```csharp
LoginUserManagementForm.ShowForStore(store, owner);
```

其中 `store` 可为 `new LoginUserStore(parameterManager.Section("LoginUsers"))` 或与登录控件相同的 section。

## 代码示例

```csharp
using IndustrialControls.Controls.Login;
using IndustrialControls.Core;

var login = new FlatLoginControl
{
    Dock = DockStyle.Fill,
    ParameterManager = myParameterManager,
    ParameterGroupName = "LoginUsers",
    TitleText = "操作员登录"
};

login.LoginSucceeded += (s, userName) =>
{
    // 进入主界面
};
login.LoginFailed += (s, reason) => { /* 已显示在状态行，可记日志 */ };
```

## 另见

- [ParameterManager](./ParameterManager.md) 与 [ParameterManager 使用指南](../ParameterManager使用指南.md)  
- [ThemeManager](./ThemeManager.md)  
- [文档索引](./README.md)  
