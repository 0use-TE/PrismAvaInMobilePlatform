# 震动功能

## 功能说明

让设备产生震动反馈。

## 所需权限

| 权限 | 值 | 说明 |
|------|-----|------|
| `VIBRATE` | `android.permission.VIBRATE` | 震动权限 |

## Android 权限配置

```xml
<!-- AndroidManifest.xml -->
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
    <uses-permission android:name="android.permission.VIBRATE" />
</manifest>
```

## 完整代码实现

### 基本震动

```csharp
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Prism.Commands;

public class MainViewModel : ViewModelBase
{
    public AsyncDelegateCommand VibrateCommand { get; set; }

    public MainViewModel()
    {
        VibrateCommand = new AsyncDelegateCommand(async () =>
        {
            // ===== 第1步：检查震动权限 =====
            var status = await Permissions.CheckStatusAsync<Permissions.Vibrate>();

            // ===== 第2步：如果未授权，申请权限 =====
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.Vibrate>();

            // ===== 第3步：权限授权后震动 =====
            if (status == PermissionStatus.Granted)
            {
                Microsoft.Maui.Devices.Vibration.Default.Vibrate();
            }
        });
    }
}
```

### XAML 绑定

```xml
<Button Command="{Binding VibrateCommand}">震动</Button>
```

## 核心 API

### Vibration.Default.Vibrate()

```csharp
// 默认震动（通常是 200-500ms）
Microsoft.Maui.Devices.Vibration.Default.Vibrate();

// 指定震动时长
Microsoft.Maui.Devices.Vibration.Default.Vibrate(TimeSpan.FromSeconds(1));

// 使用毫秒
Microsoft.Maui.Devices.Vibration.Default.Vibrate(500);  // 500毫秒
```

### 停止震动

```csharp
// 停止正在进行的震动
Microsoft.Maui.Devices.Vibration.Default.Cancel();
```

## 震动模式示例

```csharp
// 短震动（点击反馈）
Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(50));

// 中等震动（确认反馈）
Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(200));

// 长震动（提醒）
Vibration.Default.Vibrate(TimeSpan.FromSeconds(1));

// 震动 + 暂停模式（Android 原生）
// 需要使用 Android 原生 API
```

### 在 Android 上使用原生震动模式

```csharp
#if ANDROID
using Android.Content;
using Android.OS;

VibrationCommand = new AsyncDelegateCommand(async () =>
{
    var status = await Permissions.CheckStatusAsync<Permissions.Vibrate>();
    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Vibrate>();

    if (status == PermissionStatus.Granted)
    {
        // 方式1：简单震动
        // Vibration.Default.Vibrate(500);

        // 方式2：震动模式（震动-暂停-震动）
        var vibrator = (Android.OS.Vibrator)Android.App.Application.Context
            .GetSystemService(Context.VibratorService);

        // 参数：long数组={震动时长, 暂停时长, 震动时长}
        // -1 表示不重复
        vibrator.Vibrate(new long[] { 0, 200, 100, 200 }, -1);
    }
});
#endif
```

## 带触觉反馈的完整示例

```csharp
public class MainViewModel : ViewModelBase
{
    public AsyncDelegateCommand VibrateCommand { get; set; }

    // 震动强度（0.0 - 1.0）
    public double VibrationStrength { get; set; } = 1.0;

    public MainViewModel()
    {
        VibrateCommand = new AsyncDelegateCommand(async () =>
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Vibrate>();
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.Vibrate>();

            if (status == PermissionStatus.Granted)
            {
#if ANDROID
                var vibrator = (Android.OS.Vibrator)Android.App.Application.Context
                    .GetSystemService(Context.VibratorService);

                // 根据强度计算震动时长（200-500ms）
                var duration = (int)(200 + (VibrationStrength * 300));
                vibrator.Vibrate(duration);
#else
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(300));
#endif
            }
        });
    }
}
```

## 相关文件

- `ViewModels/MainViewModel.cs` - 命令定义
- `Views/MainView.axaml` - 界面绑定
- `AndroidManifest.xml` - 权限声明

## 下一步

- [手电筒](flashlight.md)
- [浏览器](browser.md)
