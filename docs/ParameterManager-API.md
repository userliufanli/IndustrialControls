# 参数管理系统 (ParameterManager) API 使用手册

## 概述

参数管理系统是IndustrialControls控件库的核心基础设施,提供配置文件读写、参数分组管理、热更新、变更事件等企业级功能。系统由 `ParameterManager`(管理器)、`ParameterAccessor`(访问器)和 `ParameterSection`(分组视图)三个核心类组成。

## 命名空间

```csharp
IndustrialControls.Core
```

## 架构概览

```
┌─────────────────────────────────────────────────────────┐
│                  ParameterAccessor                       │
│               (静态访问层 - 零配置使用)                    │
│                                                          │
│  • Default                    - 默认实例                  │
│  • Register(name, path)       - 注册命名配置              │
│  • Section(groupName)         - 快捷分组视图              │
│  • Get/Set/SetBatch           - 便捷方法                  │
└────────────────────────┬────────────────────────────────┘
                         │
                         ▼
┌─────────────────────────────────────────────────────────┐
│                  ParameterManager                        │
│             (核心管理器 - 完整功能)                        │
│                                                          │
│  • GetValue/SetValue            - 读写参数                │
│  • GetOrSet                     - 获取或设置默认值        │
│  • SetBatch/GetBatch            - 批量操作                │
│  • LoadFromFile/SaveToFile      - 文件操作                │
│  • ParameterChanged             - 变更事件                │
│  • 热更新、防抖保存、线程安全                              │
└────────────────────────┬────────────────────────────────┘
                         │
                         ▼
┌─────────────────────────────────────────────────────────┐
│                  ParameterSection                        │
│             (分组视图 - 简化访问)                          │
│                                                          │
│  • Get/Set                      - 读写(无需分组名)        │
│  • Contains/TryGet              - 查询                    │
│  • GetOrSet                     - 获取或设置              │
│  • SetBatch/GetBatch            - 批量操作                │
└─────────────────────────────────────────────────────────┘
```

---

## ParameterManager - 核心管理器

### 快速开始

#### 1. 基本使用

```csharp
using IndustrialControls.Core;

// 创建参数管理器
var paramManager = new ParameterManager("config/app.json");

// 设置参数
paramManager.SetValue("UserSettings", "Theme", "Dark");
paramManager.SetValue("UserSettings", "FontSize", 14);
paramManager.SetValue("UserSettings", "AutoSave", true);

// 获取参数
string theme = paramManager.GetValue("UserSettings", "Theme", "Light");
int fontSize = paramManager.GetValue("UserSettings", "FontSize", 12);
bool autoSave = paramManager.GetValue("UserSettings", "AutoSave", false);
```

#### 2. 使用默认配置(零配置)

```csharp
// 使用默认配置管理器(自动创建在 %AppData%)
var defaultManager = ParameterAccessor.Default;

// 或直接使用静态方法
ParameterAccessor.Set("UserSettings", "Language", "zh-CN");
string lang = ParameterAccessor.Get("UserSettings", "Language", "en-US");
```

#### 3. 分组视图简化访问

```csharp
// 创建分组视图(绑定到特定分组)
var userSection = paramManager.Section("UserSettings");

// 使用视图读写(无需重复写分组名)
userSection.Set("Theme", "Dark");
userSection.Set("FontSize", 14);

string theme = userSection.Get("Theme", "Light");
int fontSize = userSection.Get("FontSize", 12);
```

#### 4. 监听参数变更

```csharp
paramManager.ParameterChanged += (sender, e) =>
{
    Console.WriteLine($"参数变更: {e.GroupName}.{e.Key}");
    Console.WriteLine($"变更类型: {e.ChangeType}");
    Console.WriteLine($"新值: {e.NewValue}");
};
```

#### 5. 批量操作

```csharp
// 批量设置
var parameters = new Dictionary<string, object>
{
    { "Width", 1920 },
    { "Height", 1080 },
    { "Fullscreen", true },
    { "Volume", 75 }
};
paramManager.SetBatch("DisplaySettings", parameters);

// 批量获取
var displaySettings = paramManager.GetBatch("DisplaySettings");
foreach (var kvp in displaySettings)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
}
```

### 构造函数

#### ParameterManager(string configFilePath)

创建参数管理器实例。

**参数**:
- `configFilePath`: `string` - 配置文件路径(JSON格式)

**异常**:
- `ArgumentNullException`: 路径为null时抛出
- `SecurityException`: 路径包含非法字符或路径遍历时抛出
- `InvalidOperationException`: 无法创建目录或文件时抛出

**示例**:
```csharp
// 相对路径(相对于应用程序目录)
var pm1 = new ParameterManager("config/app.json");

// 绝对路径
var pm2 = new ParameterManager("C:\\AppData\\config.json");

// 用户数据目录
string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string configPath = Path.Combine(appData, "MyApp", "settings.json");
var pm3 = new ParameterManager(configPath);
```

### 属性

#### ConfigFilePath

- **类型**: `string` (只读)
- **说明**: 配置文件完整路径

**示例**:
```csharp
Console.WriteLine($"配置文件: {paramManager.ConfigFilePath}");
```

#### EnableHotReload

- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否启用热更新监控(文件外部修改时自动重新加载)

**示例**:
```csharp
paramManager.EnableHotReload = false; // 禁用热更新
```

#### EnableLogging

- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否启用详细日志记录

**示例**:
```csharp
paramManager.EnableLogging = true;
```

### 方法

#### GetValue<T>(string groupName, string key, T defaultValue = default)

获取参数值(带默认值)。

**参数**:
- `groupName`: `string` - 分组名称
- `key`: `string` - 参数键
- `defaultValue`: `T` - 默认值(参数不存在时返回)

**返回**: `T` - 参数值

**示例**:
```csharp
// 字符串
string theme = paramManager.GetValue("UI", "Theme", "Light");

// 整数
int port = paramManager.GetValue("Network", "Port", 8080);

// 布尔值
bool enabled = paramManager.GetValue("Features", "AutoSave", true);

// 复杂类型(需可序列化)
var config = paramManager.GetValue("Advanced", "Config", new MyConfig());
```

#### SetValue<T>(string groupName, string key, T value)

设置参数值(自动保存)。

**参数**:
- `groupName`: `string` - 分组名称
- `key`: `string` - 参数键
- `value`: `T` - 参数值

**示例**:
```csharp
paramManager.SetValue("UI", "Theme", "Dark");
paramManager.SetValue("Network", "Port", 9090);
paramManager.SetValue("Features", "AutoSave", false);
```

**注意**: 
- 值未变化时不会触发保存和事件
- 自动防抖保存(300ms合并)

#### TryGetValue<T>(string groupName, string key, out T value)

尝试获取参数值。

**参数**:
- `groupName`: `string` - 分组名称
- `key`: `string` - 参数键
- `value`: `out T` - 输出参数值

**返回**: `bool` - 是否获取成功

**示例**:
```csharp
if (paramManager.TryGetValue("UI", "Theme", out string theme))
{
    Console.WriteLine($"主题: {theme}");
}
else
{
    Console.WriteLine("主题参数不存在");
}
```

#### GetOrSet<T>(string groupName, string key, T seedValue)

获取或设置参数(不存在时写入默认值)。

**参数**:
- `groupName`: `string` - 分组名称
- `key`: `string` - 参数键
- `seedValue`: `T` - 参数不存在时写入的值

**返回**: `T` - 已存在的值或刚写入的seedValue

**示例**:
```csharp
// 首次运行时创建默认值
int retryCount = paramManager.GetOrSet("Network", "RetryCount", 3);

// 后续运行时读取已保存的值
// retryCount = 3 (首次) 或读取保存的值
```

#### Delete(string groupName, string key)

删除参数。

**参数**:
- `groupName`: `string` - 分组名称
- `key`: `string` - 参数键

**返回**: `bool` - 是否删除成功

**示例**:
```csharp
bool removed = paramManager.Delete("UI", "OldTheme");
if (removed)
{
    Console.WriteLine("参数已删除");
}
```

#### DeleteGroup(string groupName)

删除整个分组。

**参数**:
- `groupName`: `string` - 分组名称

**返回**: `bool` - 是否删除成功

**示例**:
```csharp
bool removed = paramManager.DeleteGroup("DeprecatedSettings");
```

#### Contains(string groupName, string key)

检查参数是否存在。

**参数**:
- `groupName`: `string` - 分组名称
- `key`: `string` - 参数键

**返回**: `bool` - 是否存在

**示例**:
```csharp
if (paramManager.Contains("UserSettings", "Theme"))
{
    Console.WriteLine("主题参数已配置");
}
```

#### GetGroupNames()

获取所有分组名称。

**返回**: `IEnumerable<string>`

**示例**:
```csharp
foreach (var groupName in paramManager.GetGroupNames())
{
    Console.WriteLine($"分组: {groupName}");
}
```

#### GetKeys(string groupName)

获取分组下的所有参数键。

**参数**:
- `groupName`: `string` - 分组名称

**返回**: `IEnumerable<string>`

**示例**:
```csharp
foreach (var key in paramManager.GetKeys("UserSettings"))
{
    Console.WriteLine($"键: {key}");
}
```

#### SetBatch(string groupName, Dictionary<string, object> parameters)

批量设置参数。

**参数**:
- `groupName`: `string` - 分组名称
- `parameters`: `Dictionary<string, object>` - 参数字典

**示例**:
```csharp
var settings = new Dictionary<string, object>
{
    { "Width", 1920 },
    { "Height", 1080 },
    { "Fullscreen", true },
    { "Theme", "Dark" }
};
paramManager.SetBatch("Display", settings);
```

#### GetBatch(string groupName)

批量获取参数。

**参数**:
- `groupName`: `string` - 分组名称

**返回**: `Dictionary<string, object>` - 参数字典

**示例**:
```csharp
var settings = paramManager.GetBatch("Display");
int width = Convert.ToInt32(settings["Width"]);
bool fullscreen = Convert.ToBoolean(settings["Fullscreen"]);
```

#### Section(string groupName)

获取固定分组的参数视图。

**参数**:
- `groupName`: `string` - 分组名称

**返回**: `ParameterSection`

**示例**:
```csharp
var userSection = paramManager.Section("UserSettings");
userSection.Set("Theme", "Dark");
string theme = userSection.Get("Theme", "Light");
```

#### LoadFromFile()

从文件加载配置。

**示例**:
```csharp
// 手动重新加载(热更新失败时使用)
paramManager.LoadFromFile();
```

#### SaveToFile()

立即保存配置到文件(公共方法)。

**示例**:
```csharp
// 强制立即保存(不等待防抖定时器)
paramManager.SaveToFile();
```

### 事件

#### ParameterChanged

参数变更时触发。

**事件参数**: `EventHandler<ParameterChangedEventArgs>`

**ParameterChangedEventArgs属性**:
- `GroupName`: `string` - 分组名称
- `Key`: `string` - 参数键
- `NewValue`: `object` - 新值
- `ChangeType`: `ParameterChangeType` - 变更类型

**示例**:
```csharp
paramManager.ParameterChanged += (sender, e) =>
{
    Console.WriteLine($"[{e.ChangeType}] {e.GroupName}.{e.Key} = {e.NewValue}");
    
    switch (e.ChangeType)
    {
        case ParameterChangeType.Added:
            Console.WriteLine("参数新增");
            break;
        case ParameterChangeType.Modified:
            Console.WriteLine("参数修改");
            break;
        case ParameterChangeType.Deleted:
            Console.WriteLine("参数删除");
            break;
        case ParameterChangeType.GroupDeleted:
            Console.WriteLine("分组删除");
            break;
    }
};
```

#### SaveFailed

保存失败时触发。

**事件参数**: `EventHandler<Exception>`

**示例**:
```csharp
paramManager.SaveFailed += (sender, ex) =>
{
    MessageBox.Show($"参数保存失败: {ex.Message}", "错误",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
};
```

#### LoadFailed

加载失败时触发。

**事件参数**: `EventHandler<Exception>`

**示例**:
```csharp
paramManager.LoadFailed += (sender, ex) =>
{
    Console.WriteLine($"配置加载失败: {ex.Message}");
    // 使用默认配置继续运行
};
```

### 枚举类型

#### ParameterChangeType

参数变更类型枚举。

```csharp
public enum ParameterChangeType
{
    Added,          // 新增
    Modified,       // 修改
    Deleted,        // 删除
    GroupDeleted    // 分组删除
}
```

---

## ParameterAccessor - 静态访问层

### 概述

ParameterAccessor提供零配置的静态访问方式,无需手动创建ParameterManager实例。

### 属性

#### Default

获取默认配置管理器。

**类型**: `ParameterManager`

**示例**:
```csharp
var manager = ParameterAccessor.Default;
manager.SetValue("Settings", "Key", "Value");
```

### 方法

#### Register(string name, string configFilePath)

注册或获取命名配置。

**参数**:
- `name`: `string` - 配置名称
- `configFilePath`: `string` - 配置文件路径

**返回**: `ParameterManager`

**示例**:
```csharp
// 注册通讯配置
var commConfig = ParameterAccessor.Register(
    "Communication",
    "config/communication.json"
);

// 注册用户配置
var userConfig = ParameterAccessor.Register(
    "Users",
    "config/users.json"
);

// 使用注册的配置
commConfig.SetValue("Tcp", "Port", 502);
```

#### Resolve(string name)

获取指定名称的参数管理器。

**参数**:
- `name`: `string` - 配置名称

**返回**: `ParameterManager`

**异常**: `KeyNotFoundException` - 未找到指定配置

**示例**:
```csharp
try
{
    var commConfig = ParameterAccessor.Resolve("Communication");
    int port = commConfig.GetValue("Tcp", "Port", 502);
}
catch (KeyNotFoundException)
{
    Console.WriteLine("配置未注册");
}
```

#### TryResolve(string name, out ParameterManager manager)

尝试获取指定名称的参数管理器。

**参数**:
- `name`: `string` - 配置名称
- `manager`: `out ParameterManager` - 输出管理器实例

**返回**: `bool` - 是否获取成功

**示例**:
```csharp
if (ParameterAccessor.TryResolve("Communication", out var commConfig))
{
    commConfig.SetValue("Tcp", "Ip", "192.168.1.100");
}
```

#### IsRegistered(string name)

检查指定名称的配置是否已注册。

**参数**:
- `name`: `string` - 配置名称

**返回**: `bool`

**示例**:
```csharp
if (!ParameterAccessor.IsRegistered("Communication"))
{
    ParameterAccessor.Register("Communication", "config/comm.json");
}
```

#### GetRegisteredNames()

获取所有已注册的配置名称。

**返回**: `IEnumerable<string>`

**示例**:
```csharp
foreach (var name in ParameterAccessor.GetRegisteredNames())
{
    Console.WriteLine($"已注册配置: {name}");
}
```

#### Remove(string name)

移除指定名称的配置。

**参数**:
- `name`: `string` - 配置名称

**返回**: `bool` - 是否移除成功

**示例**:
```csharp
bool removed = ParameterAccessor.Remove("OldConfig");
```

#### Section(string groupName)

默认配置文件下指定分组的参数视图。

**参数**:
- `groupName`: `string` - 分组名称

**返回**: `ParameterSection`

**示例**:
```csharp
var userSection = ParameterAccessor.Section("UserSettings");
userSection.Set("Theme", "Dark");
```

#### Set<T>(string groupName, string key, T value)

设置参数值(默认配置)。

**示例**:
```csharp
ParameterAccessor.Set("UserSettings", "Language", "zh-CN");
```

#### Get<T>(string groupName, string key, T defaultValue = default)

获取参数值(默认配置)。

**示例**:
```csharp
string lang = ParameterAccessor.Get("UserSettings", "Language", "en-US");
```

#### SetBatch(string groupName, Dictionary<string, object> parameters)

批量设置参数(默认配置)。

**示例**:
```csharp
var settings = new Dictionary<string, object>
{
    { "Theme", "Dark" },
    { "FontSize", 14 }
};
ParameterAccessor.SetBatch("UI", settings);
```

---

## ParameterSection - 分组视图

### 概述

ParameterSection提供绑定到特定分组的简化访问接口,无需重复传递分组名称。

### 构造函数

#### ParameterSection(ParameterManager manager, string groupName)

创建分组视图。

**参数**:
- `manager`: `ParameterManager` - 参数管理器
- `groupName`: `string` - 分组名称

**示例**:
```csharp
var userSection = new ParameterSection(paramManager, "UserSettings");
```

### 属性

#### Manager

- **类型**: `ParameterManager` (只读)
- **说明**: 所属参数管理器

**示例**:
```csharp
var manager = userSection.Manager;
```

#### GroupName

- **类型**: `string` (只读)
- **说明**: 分组名称

**示例**:
```csharp
Console.WriteLine($"分组: {userSection.GroupName}");
```

### 方法

#### Get<T>(string key, T defaultValue = default)

获取参数值。

**示例**:
```csharp
string theme = userSection.Get("Theme", "Light");
int fontSize = userSection.Get("FontSize", 12);
```

#### Set<T>(string key, T value)

设置参数值。

**示例**:
```csharp
userSection.Set("Theme", "Dark");
userSection.Set("FontSize", 14);
```

#### Contains(string key)

检查参数是否存在。

**示例**:
```csharp
if (userSection.Contains("Theme"))
{
    Console.WriteLine("主题已配置");
}
```

#### TryGet<T>(string key, out T value)

尝试获取参数值。

**示例**:
```csharp
if (userSection.TryGet("Theme", out string theme))
{
    Console.WriteLine($"主题: {theme}");
}
```

#### GetOrSet<T>(string key, T value)

获取或设置参数。

**示例**:
```csharp
// 首次运行时创建默认值
int retryCount = userSection.GetOrSet("RetryCount", 3);
```

#### Delete(string key)

删除参数。

**示例**:
```csharp
userSection.Delete("OldSetting");
```

#### SetBatch(Dictionary<string, object> parameters)

批量设置参数。

**示例**:
```csharp
var settings = new Dictionary<string, object>
{
    { "Theme", "Dark" },
    { "FontSize", 14 },
    { "Language", "zh-CN" }
};
userSection.SetBatch(settings);
```

#### GetBatch()

批量获取参数。

**返回**: `Dictionary<string, object>`

**示例**:
```csharp
var settings = userSection.GetBatch();
foreach (var kvp in settings)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
}
```

---

## 完整示例

### 1. 多配置文件管理(推荐模式)

这是实际项目中的最佳实践,通过静态类封装参数访问,提供强类型属性和清晰的分组结构。

**示例代码** (`AppParameters.cs`):

```csharp
using System;
using IndustrialControls.Core;

namespace IndustrialControls.Demo
{
    /// <summary>
    /// 应用参数 - 直接属性访问方式
    /// </summary>
    public static class AppParameters
    {
        #region 默认配置(parameters.json)

        /// <summary>
        /// 默认参数管理器
        /// </summary>
        public static ParameterManager Default => ParameterAccessor.Default;

        private static readonly ParameterSection _app = ParameterAccessor.Section("App");
        private static readonly ParameterSection _ui = ParameterAccessor.Section("UI");

        /// <summary>
        /// App 参数组
        /// </summary>
        public static class App
        {
            public static string Language
            {
                get => _app.Get("Language", "zh-CN");
                set => _app.Set("Language", value);
            }

            public static string Theme
            {
                get => _app.Get("Theme", "浅色");
                set => _app.Set("Theme", value);
            }

            public static bool AutoSave
            {
                get => _app.Get("AutoSave", true);
                set => _app.Set("AutoSave", value);
            }

            public static string LastOpenedPath
            {
                get => _app.Get("LastOpenedPath", "");
                set => _app.Set("LastOpenedPath", value);
            }
        }

        /// <summary>
        /// UI 参数组
        /// </summary>
        public static class UI
        {
            public static int WindowWidth
            {
                get => _ui.Get("WindowWidth", 800);
                set => _ui.Set("WindowWidth", value);
            }

            public static int WindowHeight
            {
                get => _ui.Get("WindowHeight", 600);
                set => _ui.Set("WindowHeight", value);
            }

            public static int FontSize
            {
                get => _ui.Get("FontSize", 12);
                set => _ui.Set("FontSize", value);
            }

            public static string WindowState
            {
                get => _ui.Get("WindowState", "Normal");
                set => _ui.Set("WindowState", value);
            }
        }

        #endregion

        #region 通讯配置(Config/communication.json)

        /// <summary>
        /// 通讯配置管理器(独立配置文件)
        /// </summary>
        public static ParameterManager CommConfig => ParameterAccessor.Register(
            "Communication",
            System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "communication.json"));

        private static readonly ParameterSection _comm = CommConfig.Section("CommConfig");

        /// <summary>
        /// 通讯参数组
        /// </summary>
        public static class Comm
        {
            public static string TcpIp
            {
                get => _comm.Get("TcpIp", "127.0.0.1");
                set => _comm.Set("TcpIp", value);
            }

            public static int TcpPort
            {
                get => _comm.Get("TcpPort", 5000);
                set => _comm.Set("TcpPort", value);
            }

            public static int TcpTimeout
            {
                get => _comm.Get("TcpTimeout", 3000);
                set => _comm.Set("TcpTimeout", value);
            }

            public static string SerialPortName
            {
                get => _comm.Get("SerialPortName", "COM1");
                set => _comm.Set("SerialPortName", value);
            }

            public static int SerialBaudRate
            {
                get => _comm.Get("SerialBaudRate", 115200);
                set => _comm.Set("SerialBaudRate", value);
            }

            public static string Mode
            {
                get => _comm.Get("Mode", "Tcp");
                set => _comm.Set("Mode", value);
            }
        }

        #endregion
    }
}
```

**使用示例**:

```csharp
// 1. 强类型属性访问(推荐)
AppParameters.App.Theme = "Dark";
AppParameters.UI.FontSize = 14;
AppParameters.Comm.TcpIp = "192.168.1.100";

// 2. 读取配置
string theme = AppParameters.App.Theme;
int fontSize = AppParameters.UI.FontSize;
string tcpIp = AppParameters.Comm.TcpIp;

// 3. 访问默认参数管理器
var defaultManager = AppParameters.Default;

// 4. 访问通讯配置管理器
var commManager = AppParameters.CommConfig;

// 优势:
// ✅ 强类型,编译时检查
// ✅ IntelliSense自动补全
// ✅ 集中管理,易于维护
// ✅ 默认值统一管理
// ✅ 支持多配置文件隔离
```
```

### 2. 配置变更监听

```csharp
// 监听所有配置变更
ParameterAccessor.Default.ParameterChanged += OnParameterChanged;

void OnParameterChanged(object sender, ParameterChangedEventArgs e)
{
    Console.WriteLine($"[{DateTime.Now}] {e.ChangeType}: {e.GroupName}.{e.Key} = {e.NewValue}");
    
    // 根据变更类型处理
    switch (e.ChangeType)
    {
        case ParameterChangeType.Modified:
            ApplySetting(e.GroupName, e.Key, e.NewValue);
            break;
        case ParameterChangeType.Deleted:
            ResetSetting(e.GroupName, e.Key);
            break;
    }
}

void ApplySetting(string group, string key, object value)
{
    if (group == "Display" && key == "Theme")
    {
        ApplyTheme(value.ToString());
    }
}
```

### 3. 配置热更新

```csharp
// 启用热更新(默认启用)
var pm = new ParameterManager("config/app.json");
pm.EnableHotReload = true;

// 监听外部修改
pm.ParameterChanged += (sender, e) =>
{
    Console.WriteLine("配置文件被外部修改并重新加载");
    ReloadSettings();
};

// 当其他程序或手动编辑JSON文件时,自动触发重新加载
```

### 4. 配置文件格式

JSON配置文件结构:

```json
{
  "Groups": [
    {
      "Name": "UserSettings",
      "Parameters": {
        "Theme": {
          "Key": "Theme",
          "Value": "Dark",
          "Type": "System.String",
          "Group": "UserSettings",
          "LastModified": "2024-01-15T10:30:00"
        },
        "FontSize": {
          "Key": "FontSize",
          "Value": 14,
          "Type": "System.Int32",
          "Group": "UserSettings",
          "LastModified": "2024-01-15T10:30:00"
        }
      }
    },
    {
      "Name": "Network",
      "Parameters": {
        "Port": {
          "Key": "Port",
          "Value": 8080,
          "Type": "System.Int32",
          "Group": "Network",
          "LastModified": "2024-01-15T09:00:00"
        }
      }
    }
  ]
}
```

---

## 核心特性

### 1. 线程安全

使用 `ReaderWriterLockSlim` 实现读写锁:
- 多个读取操作可并发执行
- 写入操作独占访问
- 避免读写冲突

```csharp
// 线程安全示例 - 可在多线程环境使用
Task.Run(() => {
    paramManager.SetValue("Data", "Value1", "test1");
});

Task.Run(() => {
    var val = paramManager.GetValue("Data", "Value2", "default");
});
```

### 2. 防抖保存

参数变更后等待300ms合并保存,避免频繁写入:

```csharp
// 连续设置多个参数
paramManager.SetValue("UI", "Theme", "Dark");
paramManager.SetValue("UI", "FontSize", 14);
paramManager.SetValue("UI", "Language", "zh-CN");

// 只触发一次保存操作(300ms后)
```

### 3. 热更新

FileSystemWatcher监控配置文件外部修改:

```csharp
// 手动编辑JSON文件后自动重新加载
// 1. 打开 config/app.json
// 2. 修改某个参数值
// 3. 保存文件
// 4. 自动触发 ParameterChanged 事件
```

### 4. 变更检测

值未变化时不触发保存和事件:

```csharp
paramManager.SetValue("UI", "Theme", "Dark");
paramManager.SetValue("UI", "Theme", "Dark"); // 不会触发保存

paramManager.SetValue("UI", "Theme", "Light"); // 会触发保存
```

### 5. 路径安全

防止路径遍历攻击:

```csharp
// 安全路径
var pm1 = new ParameterManager("config/app.json"); // ✅

// 不安全路径(抛出异常)
var pm2 = new ParameterManager("../../secret.json"); // ❌ SecurityException
```

---

## 注意事项

1. **配置文件格式**: 使用JSON格式,自动序列化和反序列化
2. **类型转换**: GetValue时自动进行类型转换,转换失败返回默认值
3. **自动保存**: SetValue后自动调度保存(300ms防抖)
4. **热更新**: 外部修改文件会自动重新加载并触发事件
5. **线程安全**: 所有操作都是线程安全的
6. **设计器模式**: 设计器模式下跳过文件操作
7. **异常处理**: 加载/保存失败时触发事件,不会中断程序

## 最佳实践

1. **多配置文件**: 使用ParameterAccessor.Register分离不同模块配置
2. **分组视图**: 频繁访问同一分组时使用ParameterSection简化代码
3. **默认值**: 始终提供合理的默认值
4. **变更监听**: 监听ParameterChanged实现配置实时生效
5. **批量操作**: 使用SetBatch/GetBatch提高性能
6. **路径选择**: 用户配置使用%AppData%,应用配置使用程序目录
7. **错误处理**: 订阅SaveFailed和LoadFailed处理异常情况

## 相关控件

- **CommunicationControl**: 通讯控件,使用ParameterManager持久化连接配置
- **FlatLoginControl**: 登录控件,使用ParameterManager存储用户列表
- **所有控件**: 通过AutoPersist属性自动使用ParameterManager保存参数

---

## 配置文件存储位置

### 默认配置

```
%AppData%\IndustrialControls\parameters.json
```

示例路径:
```
C:\Users\用户名\AppData\Roaming\IndustrialControls\parameters.json
```

### 自定义配置

```csharp
// 应用目录
new ParameterManager("config/app.json");

// 用户数据目录
string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string path = Path.Combine(appData, "MyApp", "settings.json");
new ParameterManager(path);
```

---

## 性能建议

### 1. 批量操作

```csharp
// ❌ 避免多次单独设置
paramManager.SetValue("UI", "Theme", "Dark");
paramManager.SetValue("UI", "FontSize", 14);
paramManager.SetValue("UI", "Language", "zh-CN");

// ✅ 使用批量操作(只触发一次保存)
var settings = new Dictionary<string, object>
{
    { "Theme", "Dark" },
    { "FontSize", 14 },
    { "Language", "zh-CN" }
};
paramManager.SetBatch("UI", settings);
```

### 2. 分组视图复用

```csharp
// ❌ 避免重复创建视图
for (int i = 0; i < 100; i++)
{
    var section = paramManager.Section("Data");
    section.Set($"Key{i}", i);
}

// ✅ 复用视图
var section = paramManager.Section("Data");
for (int i = 0; i < 100; i++)
{
    section.Set($"Key{i}", i);
}
```

### 3. 禁用不必要的热更新

```csharp
// 如果不需要外部修改配置文件,禁用热更新提升性能
paramManager.EnableHotReload = false;
```

