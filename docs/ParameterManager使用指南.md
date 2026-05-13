# ParameterManager 使用指南

> 深入了解参数管理器的高级特性和最佳实践

---

## 1. 概述

**ParameterManager** 是 IndustrialControls 控件库的核心组件之一，提供以下特性：

- **JSON 存储**：配置数据以 JSON 格式持久化到文件
- **分组管理**：支持参数按分组组织
- **类型安全**：支持多种数据类型的安全转换
- **线程安全**：使用读写锁保证多线程访问安全
- **热更新**：支持配置文件变更自动重载

---

## 2. 基础用法

### 2.1 创建实例

```csharp
// 方式1：指定配置文件路径
var paramManager = new ParameterManager("config/appsettings.json");

// 方式2：使用应用程序目录下的配置文件
var paramManager = new ParameterManager(
    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "settings.json")
);
```

### 2.2 设置参数

```csharp
// 设置字符串参数
paramManager.SetValue("Network", "ServerIP", "192.168.1.100");

// 设置数值参数
paramManager.SetValue("Network", "Port", 8080);
paramManager.SetValue("System", "Timeout", 3000);

// 设置布尔参数
paramManager.SetValue("System", "AutoStart", true);
paramManager.SetValue("System", "EnableLogging", false);

// 设置浮点数参数
paramManager.SetValue("Sensor", "Temperature", 25.5f);
paramManager.SetValue("Sensor", "Pressure", 101.325);
```

### 2.3 获取参数

```csharp
// 获取字符串参数（带默认值）
string ip = paramManager.GetValue("Network", "ServerIP", "127.0.0.1");

// 获取数值参数
int port = paramManager.GetValue("Network", "Port", 80);
int timeout = paramManager.GetValue("System", "Timeout", 5000);

// 获取布尔参数
bool autoStart = paramManager.GetValue("System", "AutoStart", false);

// 获取浮点数参数
float temp = paramManager.GetValue("Sensor", "Temperature", 20.0f);
double pressure = paramManager.GetValue("Sensor", "Pressure", 100.0);
```

---

## 3. 高级特性

### 3.1 参数变更监听

```csharp
paramManager.ParameterChanged += (sender, e) =>
{
    switch (e.ChangeType)
    {
        case ParameterChangeType.Added:
            Console.WriteLine($"参数添加: {e.GroupName}.{e.Key} = {e.Value}");
            break;
        case ParameterChangeType.Modified:
            Console.WriteLine($"参数修改: {e.GroupName}.{e.Key} = {e.Value}");
            // 响应参数变更
            if (e.GroupName == "Network" && e.Key == "ServerIP")
            {
                ReconnectToServer((string)e.Value);
            }
            break;
        case ParameterChangeType.Deleted:
            Console.WriteLine($"参数删除: {e.GroupName}.{e.Key}");
            break;
        case ParameterChangeType.GroupDeleted:
            Console.WriteLine($"分组删除: {e.GroupName}");
            break;
        case ParameterChangeType.Reloaded:
            Console.WriteLine("配置文件已重载");
            break;
    }
};
```

### 3.2 热更新功能

```csharp
// 启用热更新（默认启用）
paramManager.EnableHotReload = true;

// 当配置文件被外部修改时，会自动触发 ParameterChanged 事件（ChangeType = Reloaded）
// 可以在此事件中重新加载相关配置
paramManager.ParameterChanged += (sender, e) =>
{
    if (e.ChangeType == ParameterChangeType.Reloaded)
    {
        Console.WriteLine("检测到配置文件变更，正在重新加载...");
        LoadAllSettings();
    }
};
```

### 3.3 批量操作

```csharp
// 获取所有分组名称
var groups = paramManager.GetGroupNames();
foreach (var group in groups)
{
    Console.WriteLine($"分组: {group}");
}

// 获取指定分组下的所有参数键
var keys = paramManager.GetKeys("Network");
foreach (var key in keys)
{
    string value = paramManager.GetValue("Network", key, "");
    Console.WriteLine($"  {key} = {value}");
}

// 检查参数是否存在
bool exists = paramManager.Contains("System", "AutoStart");
if (!exists)
{
    paramManager.SetValue("System", "AutoStart", false);
}
```

### 3.4 删除操作

```csharp
// 删除单个参数
bool deleted = paramManager.Delete("Network", "OldServerIP");

// 删除整个分组
bool groupDeleted = paramManager.DeleteGroup("LegacySettings");
```

---

## 4. 配置文件格式

配置文件采用 JSON 格式存储，结构如下：

```json
{
  "Version": "1.0",
  "LastModified": "2024-01-15T10:30:00",
  "Groups": [
    {
      "Name": "Network",
      "Description": "网络配置",
      "Parameters": {
        "ServerIP": {
          "Key": "ServerIP",
          "Value": "192.168.1.100",
          "Type": "System.String",
          "Group": "Network",
          "Description": "服务器IP地址",
          "LastModified": "2024-01-15T10:30:00"
        },
        "Port": {
          "Key": "Port",
          "Value": 8080,
          "Type": "System.Int32",
          "Group": "Network",
          "Description": "端口号",
          "LastModified": "2024-01-15T10:30:00"
        }
      }
    },
    {
      "Name": "System",
      "Description": "系统配置",
      "Parameters": {
        "AutoStart": {
          "Key": "AutoStart",
          "Value": true,
          "Type": "System.Boolean",
          "Group": "System",
          "Description": "是否自动启动",
          "LastModified": "2024-01-15T10:30:00"
        }
      }
    }
  ]
}
```

---

## 5. 最佳实践

### 5.1 单例模式使用

建议在应用程序中使用单例模式管理 ParameterManager 实例：

```csharp
public static class ConfigManager
{
    private static readonly Lazy<ParameterManager> _instance = 
        new Lazy<ParameterManager>(() => 
            new ParameterManager("config/appsettings.json"));

    public static ParameterManager Instance => _instance.Value;
}

// 使用
var ip = ConfigManager.Instance.GetValue("Network", "ServerIP", "127.0.0.1");
```

### 5.2 配置初始化

在应用程序启动时进行配置初始化：

```csharp
public void InitializeConfiguration()
{
    var paramManager = new ParameterManager("config/appsettings.json");
    
    // 设置默认值（如果参数不存在）
    if (!paramManager.Contains("System", "AutoStart"))
    {
        paramManager.SetValue("System", "AutoStart", false);
    }
    
    if (!paramManager.Contains("Network", "Port"))
    {
        paramManager.SetValue("Network", "Port", 8080);
    }
    
    // 监听参数变更
    paramManager.ParameterChanged += OnParameterChanged;
}

private void OnParameterChanged(object sender, ParameterChangedEventArgs e)
{
    // 处理参数变更
}
```

### 5.3 错误处理

```csharp
var paramManager = new ParameterManager("config/appsettings.json");

// 监听保存失败
paramManager.SaveFailed += (sender, ex) =>
{
    Console.WriteLine($"保存配置失败: {ex.Message}");
    // 可以记录日志或提示用户
};

// 监听加载失败
paramManager.LoadFailed += (sender, ex) =>
{
    Console.WriteLine($"加载配置失败: {ex.Message}");
    // 使用默认配置
};
```

### 5.4 设计模式兼容

ParameterManager 在设计模式下会自动跳过文件操作，避免在 Visual Studio 设计器中运行时出现异常：

```csharp
// 在构造函数中会自动检测设计模式
// 如果处于设计模式，不会创建文件监控和执行文件操作
```

---

## 6. 性能优化

### 6.1 防抖保存

ParameterManager 内置了防抖机制，多个参数的连续修改会合并为一次文件写入：

```csharp
// 连续设置多个参数只会触发一次文件写入
paramManager.SetValue("Network", "ServerIP", "192.168.1.100");
paramManager.SetValue("Network", "Port", 8080);
paramManager.SetValue("System", "AutoStart", true);
// 以上操作会在 300ms 后合并保存一次
```

### 6.2 原子写入

配置文件采用原子写入方式，避免文件损坏：

1. 先写入临时文件（`.tmp`）
2. 备份旧文件（`.bak`）
3. 替换目标文件
4. 删除临时文件

### 6.3 值变更检测

只有当值真正发生变化时才会触发事件和保存操作：

```csharp
// 如果值相同，不会触发事件和保存
paramManager.SetValue("System", "AutoStart", true);
paramManager.SetValue("System", "AutoStart", true); // 不会触发变更
```

---

## 7. 线程安全

ParameterManager 使用 `ReaderWriterLockSlim` 保证线程安全：

```csharp
// 读取操作使用读锁（允许多个线程同时读取）
var value = paramManager.GetValue("Network", "ServerIP");

// 写入操作使用写锁（独占）
paramManager.SetValue("Network", "ServerIP", "192.168.1.101");
```

---

## 8. 常见问题

### 8.1 配置文件无法创建

**问题**：在某些环境下（如 Program Files 目录），配置文件无法创建。

**解决方案**：

```csharp
// 使用可写目录
string configPath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "MyApp",
    "settings.json"
);
var paramManager = new ParameterManager(configPath);
```

### 8.2 热更新不生效

**问题**：修改配置文件后没有自动重载。

**解决方案**：

```csharp
// 确保启用了热更新
paramManager.EnableHotReload = true;

// 检查配置文件目录是否存在
string directory = Path.GetDirectoryName(paramManager.ConfigFilePath);
if (!Directory.Exists(directory))
{
    Directory.CreateDirectory(directory);
}
```

### 8.3 类型转换失败

**问题**：获取参数时类型转换失败。

**解决方案**：

```csharp
// 使用正确的类型获取
int port = paramManager.GetValue("Network", "Port", 80);  // 正确
string portStr = paramManager.GetValue("Network", "Port", "80");  // 可能失败

// 或者先获取为 object，再手动转换
object portObj = paramManager.GetValue<object>("Network", "Port");
if (portObj is long)  // JavaScriptSerializer 会将整数转为 long
{
    int port = Convert.ToInt32(portObj);
}
```

---

## 9. 总结

ParameterManager 提供了完整的参数持久化解决方案，主要特点包括：

1. **易用性**：简单的 Get/Set API
2. **可靠性**：原子写入、错误处理
3. **高性能**：防抖保存、值变更检测
4. **可扩展性**：支持分组管理、自定义类型
5. **安全性**：线程安全、设计模式兼容

建议在应用程序中使用单例模式管理 ParameterManager，并在启动时进行必要的配置初始化。