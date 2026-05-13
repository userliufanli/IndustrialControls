# IndustrialControls 文档中心

> Winform 上位机控件库 - 专业的工业控制界面组件库

---

## 📋 项目简介

**IndustrialControls** 是一个专为工业上位机应用设计的 WinForms 控件库，提供丰富的 UI 组件和核心功能模块，帮助开发者快速构建专业的工业控制界面。

### ✨ 核心特性

| 特性 | 描述 |
|------|------|
| **专业控件** | 状态指示器、设备按钮、报警显示、虚拟键盘等工业专用控件 |
| **通信支持** | 内置 TCP/UDP/串口通信模块，支持参数持久化和自动重连 |
| **主题系统** | 支持亮色/暗色主题切换，统一的视觉风格 |
| **参数管理** | 强大的参数持久化管理，支持热更新 |
| **线程安全** | 所有核心模块均支持多线程访问 |

---

## 📚 文档目录

### 总览与手册

| 文档 | 说明 |
|------|------|
| [**IndustrialControls使用手册**](./IndustrialControls使用手册.md) | **详细使用手册**：环境、引用、参数系统、JSON、事件、Demo、常见问题 |

### 新手入门

| 文档 | 说明 |
|------|------|
| [快速入门指南](./快速入门指南.md) | 5分钟快速上手，创建第一个应用 |
| [API使用说明书](./API使用说明书.md) | 所有控件的完整 API 参考 |

### 核心功能

| 文档 | 说明 |
|------|------|
| [ParameterManager使用指南](./ParameterManager使用指南.md) | 参数管理器的使用方法 |

---

## 🎯 推荐阅读路径

```
1. IndustrialControls使用手册.md → 环境与参数体系总览（建议先读）
        ↓
2. 快速入门指南.md    → 最短示例与项目结构
        ↓
3. API使用说明书.md    → 各控件 API 细则
        ↓
4. ParameterManager使用指南.md → 参数管理器进阶（与源码冲突时以源码为准）
```

---

## 📂 项目结构

```
IndustrialControls/
├── src/
│   └── IndustrialControls/
│       ├── Controls/          # 控件目录
│       │   ├── StatusIndicator/    # 状态指示器
│       │   ├── DeviceButton/       # 设备控制按钮
│       │   ├── Alarm/              # 报警显示
│       │   ├── Communication/      # 通信控制
│       │   ├── DataInput/          # 数据输入
│       │   ├── DataVisualization/  # 数据可视化
│       │   └── VirtualKeyboard/    # 虚拟键盘
│       ├── Core/               # 核心模块
│       │   ├── ParameterManager.cs # 参数管理器
│       │   └── BaseControl.cs      # 控件基类
│       ├── Theme/              # 主题系统
│       └── Utilities/          # 工具类
├── samples/
│   └── IndustrialControls.Demo/   # 演示项目
└── docs/                       # 文档目录
```

---

## 🛠️ 技术栈

- **框架**: .NET Framework 4.8
- **UI框架**: Windows Forms (WinForms)
- **开发工具**: Visual Studio 2022
- **语言**: C# 8.0

---

## 📞 联系与反馈

如有问题或建议，欢迎提交 Issue 或联系开发团队。