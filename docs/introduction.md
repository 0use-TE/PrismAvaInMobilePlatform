# 项目介绍

## 概述

PrismAvaInMobilePlatform 是一个基于 **Avalonia** 的 Android 跨平台应用，集成了多种原生设备功能。

## 技术栈

| 分类 | 技术 |
|------|------|
| UI 框架 | Avalonia 11.3 |
| MVVM | Prism.DryIoc 9.0 |
| 原生功能 | Microsoft.Maui.Essentials |
| UI 主题 | Semi.Avalonia |
| 日志 | Serilog |

## 已实现功能

| 功能模块 | 说明 |
|----------|------|
| **摄像头** | 拍照、录像 |
| **文件选择** | 从相册选择单张/多张照片 |
| **手电筒** | 开关闪光灯 |
| **震动** | 设备震动反馈 |
| **浏览器** | 在系统浏览器打开网址 |
| **设备信息** | 获取设备型号、名称等 |
| **主题切换** | 深色/浅色主题切换 |
| **通知** | 窗口通知（信息/警告/错误/成功） |

## 核心文件

| 文件 | 说明 |
|------|------|
| `ViewModels/MainViewModel.cs` | 主视图模型，包含所有功能命令 |
| `Services/NotificationService.cs` | 通知服务 |
| `Android/MainActivity.cs` | Android 入口 |

## 下一步

- [Android 入门](android/getting-started.md)
- [原生 Android 功能](android/native-integration.md)
