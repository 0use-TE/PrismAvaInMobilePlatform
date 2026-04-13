# Android 入门

## 环境要求

- .NET 10.0 SDK
- Android SDK (API 21+)

## 安装 Android 工作负载

```bash
dotnet workload install android
```

## 运行应用

```bash
dotnet run -f net10.0-android
```

## 发布 Release 版本

```bash
dotnet publish -f net10.0-android -c Release
```

## 项目结构

```
PrismAvaInMobilePlatform/
├── PrismAvaInMobilePlatform/           # 共享代码
│   ├── ViewModels/MainViewModel.cs     # 主视图模型（含原生功能）
│   ├── Views/MainView.axaml            # 主视图
│   └── Services/NotificationService.cs  # 通知服务
├── PrismAvaInMobilePlatform.Android/   # Android 特定代码
│   ├── MainActivity.cs                  # 入口Activity
│   └── AndroidManifest.xml              # 权限配置
```

## AndroidManifest.xml 权限配置

```xml
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
    <uses-permission android:name="android.permission.CAMERA" />
    <uses-permission android:name="android.permission.RECORD_AUDIO" />
    <uses-permission android:name="android.permission.FLASHLIGHT" />
    <uses-permission android:name="android.permission.VIBRATE" />
</manifest>
```

## 已实现的功能

- 拍照 / 拍视频
- 选择照片（单选/多选）
- 手电筒控制
- 震动反馈
- 浏览器跳转
- 设备信息显示
- 深色/浅色主题切换
- 窗口通知

## 下一步

- [原生 Android 功能](native-integration.md)
