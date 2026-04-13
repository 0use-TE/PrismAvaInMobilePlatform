# Android 入门

## 环境要求

- .NET 10.0 SDK
- Android SDK (API 21+)
- 支持 Android 的设备或模拟器

## 安装 Android 工作负载

```bash
dotnet workload install android
```

## 运行 Android 应用

```bash
dotnet run -f net10.0-android
```

## 发布 Release 版本

```bash
dotnet publish -f net10.0-android -c Release
```

## Android 项目结构

```
PrismAvaInMobilePlatform.Android/
├── MainApplication.cs       # Android 入口
├── Activity.cs              # MainActivity
└── ...
```

## 配置 Android 清单

在 `AndroidManifest.xml` 中添加权限:

```xml
<manifest>
  <uses-permission android:name="android.permission.INTERNET" />
  <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
</manifest>
```
