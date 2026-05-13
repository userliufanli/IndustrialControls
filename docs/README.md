# IndustrialControls 文档中心

欢迎查阅 **IndustrialControls** 完整技术文档。本目录提供从快速入门到高级应用的全面指导，帮助您高效构建工业上位机应用程序。

> **提示**: 本文档与源码保持同步更新。如发现文档与 `src/IndustrialControls` 实现存在差异，请以实际代码为准并及时反馈。

## 📖 快速导航

| 目标 | 推荐文档 | 预计阅读时间 |
|------|----------|-------------|
| **快速开始** | [入门指南](getting-started.md) | 5 分钟 |
| **设计规范** | [设计约定](design-conventions.md) | 10 分钟 |
| **参数管理** | [参数与配置](parameters.md) | 15 分钟 |
| **控件参考** | [控件总览](controls.md) | 20 分钟 |

## 📚 文档目录

### 入门指南
- [环境与引用](getting-started.md) — 运行 Demo、在宿主项目中引用类库、目录结构说明

### 设计开发
- [设计约定](design-conventions.md) — `partial` 类、Designer 使用规范、运行时控件挂载原则

### 核心功能
- [参数与配置管理](parameters.md) — `ParameterManager`、`ParameterAccessor`、JSON 持久化、分组策略、`AppParameters` 模式、事件与热重载
- [控件参考总览](controls.md) — 控件命名空间、能力索引、通信模式说明、交叉引用

## 🎯 使用场景

- **新项目开发**: 按顺序阅读上述文档，建立完整的知识体系
- **控件使用**: 直接查阅 [控件总览](controls.md) 获取 API 参考
- **参数配置**: 参考 [参数与配置](parameters.md) 实现数据持久化
- **设计器集成**: 遵循 [设计约定](design-conventions.md) 避免常见陷阱

## 🔗 相关链接

- 仓库根目录 [README.md](../README.md) — 项目简介与最快运行步骤
- 源码目录: `src/IndustrialControls/`
- 演示项目: `samples/IndustrialControls.Demo/`
- 模板工程: `samples/IndustrialControls.Template/`
