# 原生 Android 集成

本项目通过 **Microsoft.Maui.Essentials** 提供原生 Android 功能访问。

## MAUI Essentials 简介

`Microsoft.Maui.Essentials` 提供了跨平台的统一 API 来访问原生设备功能。在 Android 上会自动调用对应的原生 API。

## 常用功能

### 1. 设备信息 (DeviceInfo)

```csharp
using Microsoft.Maui.Essentials;

public string GetDeviceInfo()
{
    var model = DeviceInfo.Model;         // "Pixel 5"
    var manufacturer = DeviceInfo.Manufacturer; // "Google"
    var version = DeviceInfo.VersionString;    // "14"
    var platform = DeviceInfo.Platform;        // "Android"
    var idiom = DeviceInfo.Idiom;              // "Phone" / "Tablet"
    var deviceType = DeviceInfo.DeviceType;    // "Physical" / "Virtual"

    return $"{manufacturer} {model} (Android {version})";
}
```

### 2. 电池信息 (Battery)

```csharp
using Microsoft.Maui.Essentials;

// 获取电池状态
var level = Battery.ChargeLevel;           // 0.0 - 1.0
var state = Battery.State;                 // Charging, Discharging, Full, etc.
var source = Battery.PowerSource;           // Battery, AC, USB, Wireless

// 监听电池变化
Battery.BatteryInfoChanged += (s, e) =>
{
    var newLevel = e.ChargeLevel;
};
```

### 3. 剪贴板 (Clipboard)

```csharp
using Microsoft.Maui.Essentials;

// 写入剪贴板
await Clipboard.SetTextAsync("要复制的文本");

// 读取剪贴板
var text = await Clipboard.GetTextAsync();
```

### 4. 震动 (Vibration)

```csharp
using Microsoft.Maui.Essentials;

// 震动 500 毫秒
Vibration.Vibrate();

// 自定义震动时长
Vibration.Vibrate(TimeSpan.FromMilliseconds(500));

// 停止震动
Vibration.Cancel();
```

### 5. 浏览器 (Browser)

```csharp
using Microsoft.Maui.Essentials;

// 在系统浏览器中打开 URL
await Browser.OpenAsync("https://example.com", BrowserLaunchMode.System);

// 使用应用内浏览器 (如果可用)
await Browser.OpenAsync("https://example.com", BrowserLaunchMode.InApp);
```

### 6. 地图 (Map)

```csharp
using Microsoft.Maui.Essentials;
using Microsoft.Maui.Essentials.SemanticTokens;

// 打开地图应用
await Map.OpenAsync(47.673988, -122.121513);

// 打开地图并显示标注
await Map.OpenAsync(new Location(47.673988, -122.121513), new MapSpan
{
    Label = "微软总部",
    Radius = new Distance(500)
});
```

### 7. 文件Picker (FilePicker)

```csharp
using Microsoft.Maui.Essentials;

// 选择单个文件
var result = await FilePicker.PickAsync();

// 选择多个文件
var results = await FilePicker.PickMultipleAsync();

// 指定文件类型
var options = new PickOptions
{
    FileTypes = FilePickerFileType.Images,
    PickerTitle = "选择图片"
};
var image = await FilePicker.PickAsync(options);
```

### 8. 短信 (SMS)

```csharp
using Microsoft.Maui.Essentials;

// 打开短信应用
await Sms.ComposeAsync(new SmsMessage
{
    Body = "你好，这是一条短信",
    Recipients = new[] { "13800138000" }
});
```

### 9. 电话 (Phone)

```csharp
using Microsoft.Maui.Essentials;

// 拨打电话
await PhoneDialer.Open("13800138000");
```

### 10. 邮件 (Email)

```csharp
using Microsoft.Maui.Essentials;

// 发送邮件
await Email.ComposeAsync(new EmailMessage
{
    Subject = "主题",
    Body = "正文",
    To = new[] { "example@example.com" },
    Attachments = new[] { new EmailAttachment("/path/to/file.pdf") }
});
```

### 11. 分享 (Share)

```csharp
using Microsoft.Maui.Essentials;

// 分享文本
await Share.RequestAsync(new ShareTextRequest
{
    Text = "分享内容",
    Title = "分享到..."
});

// 分享链接
await Share.RequestAsync(new ShareTextRequest
{
    Text = "看看这个 https://example.com",
    Title = "分享链接"
});

// 分享文件
await Share.RequestAsync(new ShareFileRequest
{
    File = new ShareFile("/path/to/file.pdf"),
    Title = "分享文件"
});
```

### 12. 屏幕方向 (Screen Orientation)

```csharp
using Microsoft.Maui.Essentials;

// 获取当前方向
var orientation = DeviceDisplay.MainDisplayInfo.Orientation;

// 锁定方向
DeviceDisplay.KeepScreenOn = true;
ScreenOrientationLock.Landscape;

// 解锁方向
ScreenOrientationLock.None;
```

### 13. 手电筒 (Flashlight)

```csharp
using Microsoft.Maui.Essentials;

// 打开/关闭手电筒
Flashlight.TurnOnAsync();
Flashlight.TurnOffAsync();
```

### 14. 地理位置 (Geolocation)

```csharp
using Microsoft.Maui.Essentials;

try
{
    var location = await Geolocation.GetLastKnownLocationAsync();
    if (location == null)
    {
        location = await Geolocation.GetLocationAsync(new GeolocationRequest
        {
            DesiredAccuracy = GeolocationAccuracy.High
        });
    }

    var lat = location.Latitude;
    var lng = location.Longitude;
}
catch (Exception ex)
{
    // 权限被拒绝或位置不可用
}
```

### 15. 传感器 (Sensors)

```csharp
using Microsoft.Maui.Essentials;

// 加速度计
Accelerometer.ReadingChanged += (s, e) =>
{
    var x = e.Reading.Acceleration.X;
    var y = e.Reading.Acceleration.Y;
    var z = e.Reading.Acceleration.Z;
};
Accelerometer.Start(SensorSpeed.Game);

// 陀螺仪
Gyroscope.ReadingChanged += (s, e) =>
{
    var rotation = e.Reading.AngularVelocity;
};
Gyroscope.Start(SensorSpeed.Game);

// 磁力计
Magnetometer.ReadingChanged += (s, e) =>
{
    var magnetic = e.Reading.MagneticField;
};
Magnetometer.Start(SensorSpeed.Game);

// 停止传感器
Accelerometer.Stop();
```

## 权限处理

Android 6.0+ 需要运行时权限申请:

```csharp
using Microsoft.Maui.Essentials;
using Android = Microsoft.Maui.Essentials.Platform;

// 检查权限
var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

if (status != PermissionStatus.Granted)
{
    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
}

if (status == PermissionStatus.Granted)
{
    // 有权限，执行操作
}
```

## 调用纯 Android 原生 API

如需调用 MAUI Essentials 未封装的功能，可直接使用 Android API:

```csharp
using Android.Content;
using Android.OS;
using Android.App;

// 示例：获取 Android 系统服务
var vibrator = (Android.OS.Vibrator)Android.App.Application.Context
    .GetSystemService(Context.VibratorService);
vibrator.Vibrate(500);
```

## 下一步

- 查看 [入门指南](getting-started.md)
- 探索 `Microsoft.Maui.Essentials` 完整 API
