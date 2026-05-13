# 参数与配置

控件库通过 **`ParameterManager`** 将分组键值持久化到 **JSON** 文件，并提供 **`ParameterAccessor`**、**`ParameterSection`** 简化多文件与默认路径场景。

## 设计目标

- 读写接口简单、类型明确（`GetValue<T>` / `SetValue<T>`、`Section` 上的 `Get` / `Set` 等）。  
- 支持多配置文件（例如全局参数与通讯参数分文件）。  
- 自动保存带**防抖**；可选**文件热重载**；内部**线程安全**（界面更新仍建议在 UI 线程）。

## 推荐模式：`AppParameters`

在 **IndustrialControls.Demo** 中，`AppParameters` 演示了推荐做法：

1. **`ParameterAccessor.Default`** — 对应默认 JSON（路径可用 `ParameterAccessor.GetDefaultConfigPath()` 查询）。  
2. **`ParameterAccessor.Section("分组名")`** — 得到 **`ParameterSection`**，在静态嵌套类里用 `Get` / `Set` 封装业务字段。  
3. **`ParameterAccessor.Register("逻辑名", 文件路径)`** — 注册第二套及更多配置文件，再对该 `ParameterManager` 调用 `.Section("分组名")`。

业务窗体中直接读写封装好的属性即可，避免在界面散落字符串键名。

## 核心类型速查

| 类型 | 作用 |
|------|------|
| `ParameterAccessor` | 静态入口：`Default`、`Section`、`Register`、`GetDefaultConfigPath` 等 |
| `ParameterSection` | 绑定一个管理器实例与一个分组名，对该分组下的键读写 |
| `ParameterManager` | 完整能力：读写、删除分组、加载/保存文件、热重载、`ParameterChanged` 事件等 |

## 事件与线程

- 订阅 **`ParameterChanged`** 可感知增删改及磁盘 **重载**（`ChangeType` 含 `Reloaded` 等）。  
- 若在非 UI 线程收到回调，更新 WinForms 控件时请使用 **`Invoke` / `BeginInvoke`**（Demo 中 `ParameterManagerTestPage` 可参考）。

## 与内置控件的关系

例如通讯、登录等场景可能与某一 `ParameterManager` 实例共享同一 JSON 路径；同一路径应避免多个互不感知的 `ParameterManager` 实例互相覆盖写入。具体键名与分组以 **Demo 源码** 为准。

更完整的 API 表与 JSON 结构说明，可直接阅读 `src/IndustrialControls/Core/` 下源码与 XML 注释。
