# 文档中心

本目录为 **IndustrialControls** 开发文档。仓库总览（编译、目录、功能摘要）见根目录 **[README.md](../README.md)**。

---

## 建议阅读顺序

1. **[IndustrialControls 使用手册](./IndustrialControls使用手册.md)** — 环境、引用、参数体系、JSON、事件、Demo、常见问题（主文档）。  
2. **[快速入门指南](./快速入门指南.md)** — 最短示例，便于对照运行 Demo。  
3. **[控件与核心模块文档索引](./controls/README.md)** — 各控件独立说明（属性、事件、命名空间）。  
4. **[ParameterManager 使用指南](./ParameterManager使用指南.md)** — 参数管理器进阶（与使用手册第 4–7 章互补；与源码冲突时以源码为准）。

---

## 文档一览

| 文档 | 说明 |
|------|------|
| [IndustrialControls 使用手册](./IndustrialControls使用手册.md) | 集成与运维向主手册 |
| [快速入门指南](./快速入门指南.md) | 入门代码片段与下一步指引 |
| [controls/README.md](./controls/README.md) | 控件专题索引与各篇链接 |
| [ParameterManager 使用指南](./ParameterManager使用指南.md) | `ParameterManager` 深入用法与最佳实践 |

---

## 本目录结构

```
docs/
├── README.md                         # 本页（导航）
├── IndustrialControls使用手册.md
├── 快速入门指南.md
├── ParameterManager使用指南.md
├── controls/                         # 控件专题（每控件一篇）
│   ├── README.md
│   └── *.md
└── .cursorrules                      # WinForms 设计器协作约定（摘要）
```
