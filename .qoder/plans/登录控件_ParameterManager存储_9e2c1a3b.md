# 登录控件（ParameterManager 本地存储）实施计划

## 目标

- 提供可复用的 WinForms **登录用户控件**，数据读写走现有 **`ParameterManager` / `ParameterSection`**（与 `ParameterAccessor.Default` 或多文件注册实例兼容）。
- 支持 **添加用户、删除用户**（管理窗体中一并提供 **修改密码** 以便运维）。
- **扁平化 UI**（无边框主按钮、白卡片、细边框输入框、微软雅黑），与库内 `FlatLightTheme` 色板一致。

## 技术方案

| 项 | 说明 |
|----|------|
| 存储位置 | 指定 `ParameterManager` + 分组名（默认分组 `LoginUsers`）下单键 `CredentialList`，值为 **JSON 数组**（与 `ParameterManager` 所用 `JavaScriptSerializer` 一致）。 |
| 凭据模型 | 每项：`UserName`、`SaltBase64`、`HashBase64`；口令为 **随机盐 + UTF-8 密码** 的 **SHA256**（仅防明文，非硬件级安全）。 |
| 并发 | `LoginUserStore` 内对「读-改-写」序列使用私有锁，避免多线程下覆盖 JSON。 |
| 控件 API | `FlatLoginControl`：`ParameterManager`（可空则 `Default`）、`ParameterGroupName`、`LoginSucceeded` / `LoginFailed` 事件；「用户管理」打开独立 `Form`。 |

## 交付物

1. `src/IndustrialControls/Controls/Login/LoginUserStore.cs` — 持久化与校验逻辑。
2. `src/IndustrialControls/Controls/Login/FlatLoginControl.cs` — 登录 UI。
3. `src/IndustrialControls/Controls/Login/LoginUserManagementForm.cs` — 用户管理 UI。
4. `samples/.../Pages/LoginDemoPage.*` + `MainForm` 新标签 + `AppParameters` 中 `LoginUsers` 分组引用（演示绑定默认管理器）。

## 验证

- `msbuild` 解决方案或主库 + Demo 工程 Release|Debug 无编译错误。
- Demo 页：添加用户 → 登录成功；删除用户 → 无法再登录。
