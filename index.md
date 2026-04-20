---
_layout: landing
---

# PrismAvaInMobilePlatform

基于 **Avalonia** 的 Android 跨平台应用模板。

## 技术栈

- Avalonia 11.3 + Prism.DryIoc 9.0
- Microsoft.Maui.Essentials (原生 Android 功能)
- Semi.Avalonia 主题

## 需要权限的功能

| 功能 | 权限 | 文档 |
|------|------|------|
| 拍照 | `CAMERA` | [教程](docs/android/features/camera-photo.md) |
| 拍视频 | `CAMERA` + `RECORD_AUDIO` | [教程](docs/android/features/camera-video.md) |
| 文件选择 | 无 | [教程](docs/android/features/file-picker.md) |
| 手电筒 | `FLASHLIGHT` | [教程](docs/android/features/flashlight.md) |
| 震动 | `VIBRATE` | [教程](docs/android/features/vibration.md) |
| 浏览器 | `INTERNET` | [教程](docs/android/features/browser.md) |

## 快速开始

```bash
dotnet run -f net10.0-android
```

## 文档

- [项目介绍](docs/introduction.md)
- [Android 入门](docs/android/getting-started.md)
