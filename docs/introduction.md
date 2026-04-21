# 简介

基于 **Avalonia + Microsoft.Maui.Essentials** 的 Android 跨平台应用模板。

## 技术栈

| 技术 | 用途 |
|------|------|
| Avalonia 12.0 | 跨平台 UI 框架 |
| CommunityToolkit.Mvvm | MVVM 模式支持 |
| Microsoft.Maui.Essentials | 原生 Android API |

## 项目结构

```
PrismAvaInMobilePlatform/
├── PrismAvaInMobilePlatform/           # 共享库
│   ├── ViewModels/                      # ViewModel 层
│   ├── Views/                            # 视图层
│   └── App.axaml                         # 应用入口
├── PrismAvaInMobilePlatform.Android/   # Android 平台
│   ├── MainActivity.cs                   # 入口 Activity
│   └── Properties/AndroidManifest.xml   # 权限配置
└── PrismAvaInMobilePlatform.Desktop/    # 桌面平台 (预览用)
```

## 核心功能

本模板演示了 Android 常用硬件 API 的调用方式：

- **相机** - 拍照、录像
- **文件选择** - 单选/多选图片
- **闪光灯** - 开/关控制
- **振动** - 设备振动
- **浏览器** - 打开 URL

## 相关文档

- [快速开始](getting-started.md) - 项目构建与权限配置
- [Android 权限配置](android/permissions.md) - 权限声明与请求
- [MAUI API 使用教程](android/tutorials/index.md) - 各功能 API 详解