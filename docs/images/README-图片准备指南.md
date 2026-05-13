# 📸 README.md 图片准备指南

本文档说明README.md中需要准备的图片及其规格要求。

## 📁 图片存放位置

所有图片统一存放在: `docs/images/` 目录

## 🖼️ 需要准备的图片清单(共9张)

### 1. architecture-overview.png
**用途**: 项目架构图,展示控件库整体架构

**建议内容**:
- 控件库三层架构图(核心层/控件层/应用层)
- 或控件库功能模块概览图

**推荐尺寸**: 1200x600 像素

**当前状态**: ⏳ 待添加

---

### 2. alarm-display-page.png
**用途**: 报警界面完整展示

**建议内容**:
- AlarmDisplayPage页面截图
- 显示报警列表整体布局
- 包含不同级别的报警(紧急🔴/重要🟠/一般🟡/信息🔵)
- 确认/清除按钮效果
- 统计信息显示

**推荐尺寸**: 900x500 像素

**当前状态**: ⏳ 待添加

---

### 3. alarm-status-controls.png
**用途**: 报警与状态监控控件组合展示

**建议内容**:
- AlarmDisplay控件截图(显示报警列表)
- StatusIndicator控件截图(不同状态颜色)
- 可在同一页面展示两个控件

**推荐尺寸**: 800x400 像素

**当前状态**: ⏳ 待添加

---

### 4. communication-control.png
**用途**: 通讯控件展示

**建议内容**:
- CommunicationControl控件截图
- 展示TCP/串口配置界面
- 连接状态指示
- 数据收发日志(可选)

**推荐尺寸**: 800x500 像素

**当前状态**: ⏳ 待添加

---

### 5. trend-chart.png
**用途**: 趋势图控件展示

**建议内容**:
- TrendChart控件截图
- 多通道数据曲线
- 网格和图例显示

**推荐尺寸**: 900x500 像素

**当前状态**: ⏳ 待添加

---

### 6. device-button.png
**用途**: 设备控制按钮展示

**建议内容**:
- DeviceControlButton控件截图
- 不同状态的按钮(启动/停止/暂停)
- 长按进度条效果

**推荐尺寸**: 600x300 像素

**当前状态**: ⏳ 待添加

---

### 7. login-control.png
**用途**: 登录控件展示

**建议内容**:
- FlatLoginControl控件截图
- 登录界面效果
- 用户管理界面(可选)

**推荐尺寸**: 700x450 像素

**当前状态**: ⏳ 待添加

---

### 8. virtual-keyboard.png
**用途**: 虚拟键盘展示

**建议内容**:
- VirtualKeyboardPanel或VirtualKeyboardForm截图
- 标准键盘布局
- 数字键盘布局(可选)

**推荐尺寸**: 900x400 像素

**当前状态**: ⏳ 待添加

---

### 9. data-input-controls.png
**用途**: 数据输入组件展示

**建议内容**:
- DataInputPage页面截图
- 显示DataInputPanel整体布局
- NumericInputBox和ValidatedTextBox使用示例
- 包含验证错误提示效果(红色边框+提示文字)

**推荐尺寸**: 900x500 像素

**当前状态**: ⏳ 待添加

---

## 🎨 图片规格建议

### 通用要求
- **格式**: PNG(推荐) 或 JPG
- **质量**: 高清,文字清晰可读
- **背景**: 建议使用实际运行界面截图
- **命名**: 使用小写字母和连字符(-)

### 优化建议
1. **截图前准备**:
   - 运行Demo项目
   - 调整好窗口大小
   - 填充示例数据
   - 使用好看的配色主题

2. **截图工具推荐**:
   - Windows: Snipping Tool / Win+Shift+S
   - 第三方: ShareX, Snagit
   - 浏览器: 开发者工具截图

3. **后期处理**:
   - 裁剪多余边缘
   - 添加标注(可选)
   - 压缩图片大小(推荐TinyPNG)

4. **主题一致性**:
   - 建议统一使用FlatDark或FlatLight主题
   - 保持截图风格一致

---

## 📋 截图清单(Demo项目运行)

### 运行Demo项目获取截图

1. **打开Demo项目**:
   ```
   samples\IndustrialControls.Demo\
   ```

2. **需要截图的页面**:
   - ✅ MainForm - 主界面(架构图)
   - ✅ AlarmDisplayPage - 报警显示
   - ✅ StatusIndicatorPage - 状态指示器
   - ✅ CommunicationTestForm - 通讯控件
   - ✅ DataVisualizationPage - 趋势图
   - ✅ DeviceButtonPage - 设备按钮
   - ✅ LoginDemoPage - 登录控件
   - ✅ VirtualKeyboardPage - 虚拟键盘
   - ✅ DataInputPage - 数据输入(文本框)

3. **截图步骤**:
   ```
   1. 运行Demo项目
   2. 导航到对应页面
   3. 填充示例数据(如需要)
   4. 调整窗口到合适大小
   5. 使用截图工具截取控件区域
   6. 保存为对应文件名到 docs/images/
   ```

---

## 🚀 快速替换步骤

当您准备好图片后:

1. **将图片复制到**:
   ```
   c:\Users\13626\Desktop\Winform上位机控件库\docs\images\
   ```

2. **确保文件名匹配**:
   - `architecture-overview.png`
   - `alarm-display-page.png`
   - `alarm-status-controls.png`
   - `communication-control.png`
   - `trend-chart.png`
   - `device-button.png`
   - `login-control.png`
   - `virtual-keyboard.png`
   - `data-input-controls.png`

3. **查看效果**:
   - 在Markdown编辑器中打开README.md
   - 图片会自动显示

---

## 💡 临时方案

如果暂时没有图片,可以使用:

### 方案1: 占位图生成器
访问 https://placeholder.com/ 生成占位图

### 方案2: 纯色占位图
使用画图工具创建纯色图片,添加文字说明

### 方案3: 在线截图
先使用Demo项目截图,后续再优化

---

## ✅ 图片检查清单

准备完图片后,请检查:

- [ ] 所有9张图片已添加到 `docs/images/` 目录
- [ ] README.md中包含所有9个图片链接
- [ ] 文件名完全匹配(区分大小写)
- [ ] 图片清晰,文字可读
- [ ] 图片大小适中(建议每张<500KB)
- [ ] 在Markdown预览中正常显示

---

## 📞 需要帮助?

如果需要协助截图或处理图片,请告知:
1. 您希望截取哪个控件的界面
2. 您偏好的主题(Dark/Light)
3. 需要填充什么示例数据

我会提供更详细的指导! 🎨
