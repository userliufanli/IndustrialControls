# 登录控件 (FlatLoginControl) API 使用手册

## 概述

`FlatLoginControl` 是一个扁平风格的登录面板控件,使用 `ParameterManager` 持久化用户列表,支持用户认证和登录管理。用户管理通过 `LoginUserManagementForm` 独立窗体处理。

## 命名空间

```csharp
IndustrialControls.Controls.Login
```

## 继承关系

```
System.Object
  └─ System.MarshalByRefObject
     └─ System.ComponentModel.Component
        └─ System.Windows.Forms.Control
           └─ System.Windows.Forms.ScrollableControl
              └─ System.Windows.Forms.ContainerControl
                 └─ System.Windows.Forms.UserControl
                    └─ FlatLoginControl
```

## 快速开始

### 1. 基本使用

```csharp
using IndustrialControls.Controls.Login;
using IndustrialControls.Core;

// 创建登录控件
var loginControl = new FlatLoginControl
{
    Size = new Size(400, 300),
    Location = new Point(100, 50),
    TitleText = "用户登录",
    ParameterGroupName = "LoginUsers"
};

// 监听登录事件
loginControl.LoginSucceeded += (sender, userName) =>
{
    Console.WriteLine($"登录成功: {userName}");
    
    // 跳转到主界面
    ShowMainForm(userName);
};

loginControl.LoginFailed += (sender, reason) =>
{
    Console.WriteLine($"登录失败: {reason}");
    // 错误信息已在控件内部显示
};

this.Controls.Add(loginControl);
```

### 2. 配置参数管理器

```csharp
// 使用默认参数管理器
var loginControl = new FlatLoginControl();

// 或使用自定义参数管理器
var paramManager = new ParameterManager("config/users.json");
loginControl.ParameterManager = paramManager;
loginControl.ParameterGroupName = "AppUsers";
```

### 3. 用户管理

```csharp
using IndustrialControls.Controls.Login;

// 打开用户管理窗体(需要管理员权限)
private void btnUserManagement_Click(object sender, EventArgs e)
{
    var store = new LoginUserStore(
        loginControl.ParameterManager.Section(loginControl.ParameterGroupName)
    );
    
    // 显示用户管理界面
    LoginUserManagementForm.ShowForStore(store, this);
}
```

## 属性

### ParameterManager

- **类型**: `ParameterManager`
- **默认值**: `ParameterAccessor.Default`
- **说明**: 参数管理器实例,为空则使用默认
- **示例**:
  ```csharp
  var pm = new ParameterManager("app_config.json");
  loginControl.ParameterManager = pm;
  ```

### ParameterGroupName

- **类型**: `string`
- **默认值**: `"LoginUsers"`
- **说明**: 存储凭据的参数分组名
- **示例**:
  ```csharp
  loginControl.ParameterGroupName = "SystemUsers";
  ```

### TitleText

- **类型**: `string`
- **默认值**: `"用户登录"`
- **说明**: 卡片标题文案
- **示例**:
  ```csharp
  loginControl.TitleText = "操作员登录";
  loginControl.TitleText = "系统管理员登录";
  ```

## 方法

### ClearSensitiveFields()

清除状态行与密码框。

**示例**:
```csharp
// 登录成功后清除敏感信息
loginControl.LoginSucceeded += (sender, userName) =>
{
    loginControl.ClearSensitiveFields();
    ShowMainForm(userName);
};
```

## 事件

### LoginSucceeded

登录成功时触发。

**事件参数**: `EventHandler<string>` (参数为登录用户名)

**示例**:
```csharp
loginControl.LoginSucceeded += (sender, userName) =>
{
    // 记录登录日志
    Logger.Info($"用户 {userName} 于 {DateTime.Now} 登录系统");
    
    // 设置当前用户
    CurrentUser.Instance.UserName = userName;
    
    // 跳转到主界面
    this.Hide();
    var mainForm = new MainForm(userName);
    mainForm.Show();
};
```

### LoginFailed

登录失败时触发。

**事件参数**: `EventHandler<string>` (参数为失败原因)

**示例**:
```csharp
loginControl.LoginFailed += (sender, reason) =>
{
    // 记录失败日志
    Logger.Warn($"登录失败: {reason}");
    
    // 可选:额外的错误处理
    if (reason.Contains("用户不存在"))
    {
        // 引导用户注册或联系管理员
    }
    else if (reason.Contains("密码错误"))
    {
        // 可选:计数连续失败次数
        failedAttempts++;
        if (failedAttempts >= 3)
        {
            MessageBox.Show("连续失败次数过多,请稍后再试");
        }
    }
};
```

## 关联类型

### LoginUserStore

用户存储管理类,负责用户的增删改查和凭据验证。

**主要方法**:

#### TryVerify(string userName, string password, out string error)

验证用户凭据。

**参数**:
- `userName`: 用户名
- `password`: 密码
- `error`: 输出参数,验证失败时的错误消息

**返回**: `bool` - 是否验证成功

**示例**:
```csharp
var store = new LoginUserStore(parameterManager.Section("LoginUsers"));

if (store.TryVerify("admin", "123456", out string error))
{
    Console.WriteLine("验证成功");
}
else
{
    Console.WriteLine($"验证失败: {error}");
}
```

#### ListUserNames()

列出所有用户名。

**返回**: `IEnumerable<string>`

**示例**:
```csharp
var users = store.ListUserNames();
foreach (var user in users)
{
    Console.WriteLine($"用户: {user}");
}
```

#### AddUser(string userName, string password)

添加新用户。

**示例**:
```csharp
store.AddUser("operator1", "password123");
```

#### RemoveUser(string userName)

删除用户。

**示例**:
```csharp
store.RemoveUser("olduser");
```

#### ChangePassword(string userName, string oldPassword, string newPassword)

修改用户密码。

**示例**:
```csharp
if (store.ChangePassword("admin", "old123", "new456"))
{
    Console.WriteLine("密码修改成功");
}
```

### LoginUserManagementForm

用户管理窗体,提供用户增删改查的图形界面。

**主要方法**:

#### ShowForStore(LoginUserStore store, IWin32Window owner)

显示用户管理窗体。

**参数**:
- `store`: 用户存储实例
- `owner`: 所有者窗体

**示例**:
```csharp
private void btnManageUsers_Click(object sender, EventArgs e)
{
    var store = new LoginUserStore(
        loginControl.ParameterManager.Section(loginControl.ParameterGroupName)
    );
    
    LoginUserManagementForm.ShowForStore(store, this);
}
```

## 完整示例

### 完整登录系统

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;
using IndustrialControls.Controls.Login;
using IndustrialControls.Core;

namespace LoginSystem
{
    public partial class LoginForm : Form
    {
        private FlatLoginControl loginControl;
        private int failedAttempts = 0;
        private const int MaxFailedAttempts = 5;

        public LoginForm()
        {
            InitializeComponent();
            InitializeLoginControl();
        }

        private void InitializeLoginControl()
        {
            // 创建登录控件
            loginControl = new FlatLoginControl
            {
                Size = new Size(450, 350),
                Location = new Point(
                    (this.ClientSize.Width - 450) / 2,
                    (this.ClientSize.Height - 350) / 2
                ),
                TitleText = "工业自动化系统 - 用户登录",
                ParameterGroupName = "SystemUsers",
                Anchor = AnchorStyles.None
            };

            // 可选:使用独立的配置文件
            var paramManager = new ParameterManager("config/auth.json");
            loginControl.ParameterManager = paramManager;

            // 订阅事件
            loginControl.LoginSucceeded += OnLoginSucceeded;
            loginControl.LoginFailed += OnLoginFailed;

            this.Controls.Add(loginControl);

            // 添加用户管理按钮
            var btnManage = new Button
            {
                Text = "用户管理",
                Location = new Point(
                    loginControl.Right + 10,
                    loginControl.Top
                ),
                Size = new Size(100, 35)
            };
            btnManage.Click += btnManageUsers_Click;
            this.Controls.Add(btnManage);
        }

        private void OnLoginSucceeded(object sender, string userName)
        {
            // 清除敏感字段
            loginControl.ClearSensitiveFields();

            // 重置失败计数
            failedAttempts = 0;

            // 记录登录日志
            Logger.Info($"[{DateTime.Now}] 用户登录成功: {userName}");

            // 保存当前用户信息
            CurrentUser.Instance.UserName = userName;
            CurrentUser.Instance.LoginTime = DateTime.Now;

            // 显示主界面
            this.Hide();
            var mainForm = new MainForm(userName);
            mainForm.FormClosed += (s, e) => this.Close();
            mainForm.Show();
        }

        private void OnLoginFailed(object sender, string reason)
        {
            failedAttempts++;

            // 记录失败日志
            Logger.Warn($"[{DateTime.Now}] 登录失败 ({failedAttempts}/{MaxFailedAttempts}): {reason}");

            // 超过最大失败次数
            if (failedAttempts >= MaxFailedAttempts)
            {
                MessageBox.Show(
                    $"连续登录失败次数已达上限({MaxFailedAttempts}次)\n" +
                    "请检查用户名和密码,或联系系统管理员。",
                    "登录失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                // 可选:锁定账户或延迟重试
                DisableLoginTemporarily();
            }
        }

        private void DisableLoginTemporarily()
        {
            loginControl.Enabled = false;

            // 30秒后重新启用
            var timer = new Timer { Interval = 30000 };
            timer.Tick += (s, e) =>
            {
                loginControl.Enabled = true;
                failedAttempts = 0;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            // 打开用户管理界面
            var store = new LoginUserStore(
                loginControl.ParameterManager.Section(loginControl.ParameterGroupName)
            );

            LoginUserManagementForm.ShowForStore(store, this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 如果是首次运行,创建默认管理员账户
            var store = new LoginUserStore(
                loginControl.ParameterManager.Section(loginControl.ParameterGroupName)
            );

            if (!store.ListUserNames().GetEnumerator().MoveNext())
            {
                // 用户列表为空,创建默认管理员
                store.AddUser("admin", "admin123");
                
                MessageBox.Show(
                    "系统已创建默认管理员账户:\n" +
                    "用户名: admin\n" +
                    "密码: admin123\n\n" +
                    "请在首次登录后立即修改密码!",
                    "欢迎使用",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
    }
}
```

### 与权限系统集成

```csharp
using IndustrialControls.Controls.Login;

// 登录成功后加载用户权限
loginControl.LoginSucceeded += (sender, userName) =>
{
    // 从配置文件或数据库加载用户角色
    var userRole = LoadUserRole(userName);
    
    // 应用权限
    ApplyUserPermissions(userRole);
    
    // 记录审计日志
    AuditLog.LogLogin(userName, success: true);
};

private UserRole LoadUserRole(string userName)
{
    // 从参数管理器或数据库读取
    var store = new LoginUserStore(
        loginControl.ParameterManager.Section(loginControl.ParameterGroupName)
    );
    
    // 返回用户角色信息
    return store.GetUserRole(userName);
}

private void ApplyUserPermissions(UserRole role)
{
    // 根据角色启用/禁用功能
    switch (role)
    {
        case UserRole.Admin:
            EnableAllFeatures();
            break;
        case UserRole.Operator:
            EnableOperatorFeatures();
            break;
        case UserRole.Viewer:
            EnableViewOnlyFeatures();
            break;
    }
}
```

## 数据存储

### 配置文件格式

用户数据以JSON格式存储在参数配置文件中:

```json
{
  "LoginUsers": {
    "Users": "[{\"UserName\":\"admin\",\"PasswordHash\":\"...\",\"Role\":\"Admin\",\"CreateTime\":\"2024-01-01T00:00:00\"}]"
  }
}
```

**说明**:
- 密码使用哈希存储,不保存明文
- 支持用户名、密码哈希、角色、创建时间等字段
- 可通过 `ParameterManager` 直接编辑配置文件

## 安全建议

1. **密码策略**: 在用户管理中实施密码复杂度要求
2. **默认账户**: 首次运行创建默认账户后提示修改
3. **失败限制**: 限制连续失败次数,防止暴力破解
4. **日志记录**: 记录所有登录尝试(成功和失败)
5. **权限分离**: 用户管理功能应与普通登录分离
6. **配置文件保护**: 配置文件应设置适当的文件权限

## 注意事项

1. **线程安全**: 登录验证在UI线程执行
2. **设计器支持**: 支持Visual Studio设计器拖放
3. **主题适配**: 自动跟随系统主题
4. **密码安全**: 密码使用哈希存储,不可逆
5. **独立管理**: 用户管理通过独立窗体处理,不在登录控件上提供入口

## 最佳实践

1. **首次运行**: 检查用户列表是否为空,创建默认管理员
2. **失败处理**: 记录失败次数,超过阈值时暂时锁定
3. **日志审计**: 详细记录登录日志,便于追溯
4. **权限控制**: 登录成功后立即应用用户权限
5. **密码管理**: 定期提醒用户修改密码
6. **配置文件**: 使用独立的配置文件存储用户数据

## 相关控件

- **CommunicationControl**: 通讯控件,登录后启用设备通讯
- **StatusIndicator**: 状态指示器,显示登录状态
- **AlarmDisplay**: 报警显示,根据用户角色显示不同报警
