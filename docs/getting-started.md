# 快速开始

## 环境要求

- .NET 10.0 SDK 或更高版本
- Android SDK (API 23+)
- 支持 Android 开发的 IDE (Visual Studio / Rider / VS Code)

## 创建项目

本项目是 **Avalonia Android 模板**，展示如何使用 MAUI API。

### 方式一：克隆项目

```bash
git clone https://github.com/你的用户名/PrismAvaInMobilePlatform.git
cd PrismAvaInMobilePlatform
```

### 方式二：创建新的 Avalonia Android 项目

```bash
# 安装 Avalonia 模板
dotnet new install Avalonia.Templates

# 创建 Avalonia Android 项目
dotnet new avalonia.android -o YourAppName
cd YourAppName

# 添加 MAUI Essentials
dotnet add package Microsoft.Maui.Essentials
dotnet add package CommunityToolkit.Mvvm
```

## 安装依赖

```bash
dotnet restore
```

## 项目构建

### Android APK

```bash
dotnet build PrismAvaInMobilePlatform/PrismAvaInMobilePlatform.Android/PrismAvaInMobilePlatform.Android.csproj -c Debug
```

APK 输出路径: `bin/Debug/net10.0-android/`

### 桌面预览

```bash
dotnet run --project PrismAvaInMobilePlatform/PrismAvaInMobilePlatform.Desktop/PrismAvaInMobilePlatform.Desktop.csproj -c Debug
```

## Android 权限配置

### 1. 配置 MainActivity

编辑 `PrismAvaInMobilePlatform.Android/MainActivity.cs`:

```csharp
using Android.App;
using Android.Content.PM;
using Android.OS;
using Avalonia.Android;
using Microsoft.Maui.ApplicationModel;
using PrismAvaInMobilePlatform;

namespace PrismAvaInMobilePlatform.Android
{
    [Activity(
        Label = "YourApp",
        Theme = "@style/MyTheme.NoActionBar",
        Icon = "@drawable/icon",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
    public class MainActivity : AvaloniaMainActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // 初始化 MAUI 权限处理
            Platform.Init(this, savedInstanceState);
        }

        public override void OnRequestPermissionsResult(
            int requestCode,
            string[] permissions,
            Permission[] grantResults)
        {
            // 将权限结果传递给 MAUI
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
```

### 2. 声明权限

编辑 `PrismAvaInMobilePlatform.Android/Properties/AndroidManifest.xml`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
    <!-- 网络权限 (普通权限，自动授予) -->
    <uses-permission android:name="android.permission.INTERNET" />

    <!-- 相机 (危险权限，需运行时请求) -->
    <uses-permission android:name="android.permission.CAMERA" />

    <!-- 闪光灯 (普通权限) -->
    <uses-permission android:name="android.permission.FLASHLIGHT" />

    <!-- 振动 (普通权限) -->
    <uses-permission android:name="android.permission.VIBRATE" />

    <!-- 存储权限 - 根据 API 版本选择 -->
    <!-- API < 33 使用旧版存储权限 -->
    <uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
    <uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />

    <!-- API >= 33 使用新版媒体权限 -->
    <uses-permission android:name="android.permission.READ_MEDIA_IMAGES" />
    <uses-permission android:name="android.permission.READ_MEDIA_VIDEO" />
    <uses-permission android:name="android.permission.READ_MEDIA_AUDIO" />
</manifest>
```

## 添加 MAUI 包

如需添加更多 MAUI 功能:

```bash
dotnet add PrismAvaInMobilePlatform/PrismAvaInMobilePlatform/PrismAvaInMobilePlatform.csproj package Microsoft.Maui.[功能名]
```

## 下一步

- [配置 Android 权限](android/permissions.md) - 了解权限声明与请求
- [API 教程](android/tutorials/index.md) - 学习各功能用法