# 项目介绍

## 概述

PrismAvaInMobilePlatform 是一个基于 **Avalonia** 的 Android 跨平台应用模板，通过 Microsoft.Maui.Essentials 调用原生 Android 功能。

## 技术栈

| 分类 | 技术 |
|------|------|
| UI 框架 | Avalonia 11.3 |
| MVVM | Prism.DryIoc 9.0 |
| 原生功能 | Microsoft.Maui.Essentials |
| UI 主题 | Semi.Avalonia |
| 日志 | Serilog |

## 需要权限的原生功能

| 功能 | 所需权限 | 说明 |
|------|----------|------|
| 拍照 | `CAMERA` | 调用摄像头拍照 |
| 拍视频 | `CAMERA` + `RECORD_AUDIO` | 调用摄像头录像 |
| 文件选择 | 无（Android 11+）| 从相册选择图片 |
| 手电筒 | `FLASHLIGHT` | 控制闪光灯开关 |
| 震动 | `VIBRATE` | 设备震动反馈 |
| 浏览器 | `INTERNET` | 在系统浏览器打开网址 |

## 下一步

- [Android 入门](android/getting-started.md)
- [拍照功能](android/features/camera-photo.md)
