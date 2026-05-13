# ParameterManager（参数管理器）

**命名空间**：`IndustrialControls.Core`  
**类型**：`ParameterManager`（JSON 等本地持久化、分组键值、可选热重载）

## 用途

保存通讯口、窗口布局、登录用户、业务参数等；与 `ParameterAccessor`、`LoginUserStore`、通讯控件参数保存等配合使用。

## 详细文档

完整配置路径、热重载、事件与线程约定请参阅：

**[ParameterManager 使用指南](../ParameterManager使用指南.md)**

## API 摘要

| 分类 | 说明 |
|------|------|
| 读写 | `GetValue<T>(group, key, default)`、`SetValue<T>(group, key, value)` |
| 删除 | `Delete`、`DeleteGroup` |
| 查询 | `Contains`、`GetGroupNames`、`GetKeys` |
| 持久化 | `LoadFromFile`、`SaveToFile` |
| 属性 | `ConfigFilePath`、`EnableHotReload` |
| 事件 | `ParameterChanged`、`SaveFailed`、`LoadFailed` |

## 另见

- [FlatLoginControl](./FlatLoginControl.md)  
- [CommunicationControl](./CommunicationControl.md)  
- [文档索引](./README.md)  
