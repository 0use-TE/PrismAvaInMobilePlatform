# Android 入门

## 环境要求

- .NET 10.0 SDK
- Android SDK (API 21+)

## 安装 Android 工作负载

```bash
dotnet workload install android
```

## NuGet 包依赖

本项目使用以下原生功能包：

| 包名 | 版本 | 说明 |
|------|------|------|
| `Microsoft.Maui.Essentials` | 10.0.51 | 原生设备功能（摄像头、文件选择等） |

其他核心依赖：

| 包名 | 版本 | 说明 |
|------|------|------|
| `Avalonia` | 11.3.13 | 跨平台 UI 框架 |
| `Prism.DryIoc.Avalonia` | 9.0.537.11130 | MVVM + 依赖注入 |
| `Semi.Avalonia` | 11.3.7.3 | UI 主题 |

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
│   ├── ViewModels/MainViewModel.cs     # 主视图模型
│   ├── Views/MainView.axaml            # 主视图
│   ├── Services/                        # 服务层
│   └── App.axaml.cs                    # DI 注册
├── PrismAvaInMobilePlatform.Android/   # Android 特定代码
│   ├── MainActivity.cs                  # 入口 Activity
│   └── AndroidManifest.xml              # 权限配置
```

## AndroidManifest.xml 权限配置

```xml
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
    <!-- 摄像头 -->
    <uses-permission android:name="android.permission.CAMERA" />
    <uses-permission android:name="android.permission.RECORD_AUDIO" />

    <!-- 手电筒 -->
    <uses-permission android:name="android.permission.FLASHLIGHT" />

    <!-- 震动 -->
    <uses-permission android:name="android.permission.VIBRATE" />

    <!-- 网络 -->
    <uses-permission android:name="android.permission.INTERNET" />
</manifest>
```

## MainActivity 配置

MainActivity 是 Android 应用的入口点，需要正确初始化 **MAUI Platform** 才能使用原生功能。

### 完整 MainActivity 代码

```csharp
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Microsoft.Maui.ApplicationModel;
using PrismAvaInMobilePlatform;

namespace PrismAvaInMobilePlatform.Android
{
    [Activity(
        Label = "PrismAvaInMobilePlatform",
        Theme = "@style/MyTheme.Main",
        Icon = "@mipmap/ic_launcher",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : AvaloniaMainActivity<App>
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // ===== 关键：初始化 MAUI Platform =====
            // 必须调用此方法才能使用 Microsoft.Maui.Essentials 的功能
            Platform.Init(this, savedInstanceState);
        }

        // ===== 关键：处理权限请求结果 =====
        public override void OnRequestPermissionsResult(
            int requestCode,
            string[] permissions,
            Permission[] grantResults)
        {
            // 将权限结果传递给 MAUI
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            // 调用基类方法确保正常流程
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // 处理文件选择等 Activity 结果
            Platform.OnActivityResult(requestCode, resultCode, data);
        }
    }
}
```

### 关键点解释

| 代码 | 说明 |
|------|------|
| `Platform.Init(this, savedInstanceState)` | 初始化 MAUI Platform，必须在 `OnCreate` 中调用 |
| `Platform.OnRequestPermissionsResult(...)` | 将权限请求结果回调给 MAUI 处理 |
| `Platform.OnActivityResult(...)` | 处理文件选择器等返回的 Activity 结果 |

### 如果不调用会怎样？

如果不调用 `Platform.Init()`，以下功能将无法使用：

- 权限申请（`Permissions.RequestAsync`）会一直返回 `Disabled`
- `FilePicker` 文件选择会失败
- `MediaPicker` 拍照录像会失败
- `Browser` 打开浏览器会失败

## 权限功能列表

| 功能 | 所需权限 | 文档 |
|------|----------|------|
| 拍照 | `CAMERA` | [教程](features/camera-photo.md) |
| 拍视频 | `CAMERA` + `RECORD_AUDIO` | [教程](features/camera-video.md) |
| 文件选择 | 无（Android 11+）| [教程](features/file-picker.md) |
| 手电筒 | `FLASHLIGHT` | [教程](features/flashlight.md) |
| 震动 | `VIBRATE` | [教程](features/vibration.md) |
| 浏览器 | `INTERNET` | [教程](features/browser.md) |

## 常见问题

### 编译错误：找不到 `Platform.Init`

确保已安装 `Microsoft.Maui.Essentials` NuGet 包：

```bash
dotnet add package Microsoft.Maui.Essentials --version 10.0.51
```

### 权限申请没有反应

检查 `MainActivity` 中是否正确调用了 `Platform.Init()` 和 `Platform.OnRequestPermissionsResult()`。

### 模拟器上功能不工作

部分功能（如手电筒）在模拟器上可能不工作，请在真机上测试。

## 下一步

- [拍照功能](features/camera-photo.md)
- [拍视频功能](features/camera-video.md)
- [文件选择](features/file-picker.md)
