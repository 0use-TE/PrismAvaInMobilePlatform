---
_layout: landing
---

# PrismAvaInMobilePlatform

基于 **Avalonia** + **Prism** 的 Android 跨平台应用模板。

## 技术栈

| 分类 | 技术 |
|------|------|
| 框架 | Avalonia 11.3 + Prism 9.0 |
| 平台 | .NET 10.0 Android |
| UI 主题 | Semi.Avalonia |
| 依赖注入 | DryIoc |
| 日志 | Serilog |

## 文档

- [项目介绍](docs/introduction.md)
- [Android 入门](docs/android/getting-started.md)
- [原生 Android 集成](docs/android/native-integration.md)

## 快速开始

```bash
dotnet run -f net10.0-android
```

## 发布 Android 应用

```bash
dotnet publish -f net10.0-android -c Release
```
