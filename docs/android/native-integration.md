# 原生 Android 功能

本项目通过 **Microsoft.Maui.Essentials** 实现了以下原生 Android 功能。

## 功能列表

| 功能 | 命令 | 说明 |
|------|------|------|
| 拍照 | `TakePhotoCommand` | 调用摄像头拍照 |
| 拍视频 | `TakeVideoCommand` | 调用摄像头录像 |
| 选择照片 | `SelectSinglePhotoCommand` | 从相册选择单张图片 |
| 选择多张照片 | `SelectMultiplePhotoCommand` | 从相册选择多张图片 |
| 打开手电筒 | `OpenFlashlightCommand` | 开启闪光灯 |
| 关闭手电筒 | `CloseFlashlightCommand` | 关闭闪光灯 |
| 震动 | `VibrateCommand` | 让设备震动 |
| 打开浏览器 | `OpenUriCommand` | 在系统浏览器中打开网址 |

## 摄像头 - 拍照/录像

```csharp
TakePhotoCommand = new AsyncDelegateCommand(async () =>
{
    // 检查摄像头权限
    var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Camera>();

    if (status == PermissionStatus.Granted)
    {
        // 拍照
        var photo = await MediaPicker.Default.CapturePhotoAsync();
        if (photo != null)
        {
            string localPath = photo.FullPath;
            // 处理照片路径
        }
    }
});

TakeVideoCommand = new AsyncDelegateCommand(async () =>
{
    var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Camera>();

    if (status == PermissionStatus.Granted)
    {
        // 录像
        var video = await MediaPicker.Default.CaptureVideoAsync();
    }
});
```

### Android 权限配置

在 `AndroidManifest.xml` 中添加：

```xml
<manifest>
    <uses-permission android:name="android.permission.CAMERA" />
    <uses-permission android:name="android.permission.RECORD_AUDIO" />
</manifest>
```

## 文件选择

```csharp
// 选择单张图片
SelectSinglePhotoCommand = new AsyncDelegateCommand(async () =>
{
    var result = await FilePicker.Default.PickAsync(new PickOptions
    {
        PickerTitle = "请选择一个文件",
        FileTypes = FilePickerFileType.Images
    });
});

// 选择多张图片
SelectMultiplePhotoCommand = new AsyncDelegateCommand(async () =>
{
    var results = await FilePicker.Default.PickMultipleAsync(new PickOptions
    {
        PickerTitle = "请选择多个文件",
        FileTypes = FilePickerFileType.Images
    });
});
```

## 手电筒

```csharp
// 打开手电筒
OpenFlashlightCommand = new AsyncDelegateCommand(async () =>
{
    var status = await Permissions.CheckStatusAsync<Permissions.Flashlight>();
    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Flashlight>();

    if (status == PermissionStatus.Granted)
        await Microsoft.Maui.Devices.Flashlight.Default.TurnOnAsync();
});

// 关闭手电筒
CloseFlashlightCommand = new AsyncDelegateCommand(async () =>
{
    var status = await Permissions.CheckStatusAsync<Permissions.Flashlight>();
    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Flashlight>();

    if (status == PermissionStatus.Granted)
        await Microsoft.Maui.Devices.Flashlight.Default.TurnOffAsync();
});
```

### Android 权限配置

```xml
<manifest>
    <uses-permission android:name="android.permission.FLASHLIGHT" />
</manifest>
```

## 震动

```csharp
VibrateCommand = new AsyncDelegateCommand(async () =>
{
    var status = await Permissions.CheckStatusAsync<Permissions.Vibrate>();
    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Vibrate>();

    if (status == PermissionStatus.Granted)
        Microsoft.Maui.Devices.Vibration.Default.Vibrate();
});
```

### Android 权限配置

```xml
<manifest>
    <uses-permission android:name="android.permission.VIBRATE" />
</manifest>
```

## 浏览器

```csharp
OpenUriCommand = new AsyncDelegateCommand(async () =>
{
    // 在系统浏览器中打开网址
    await Browser.Default.OpenAsync("https://0use.net");
});
```

## 设备信息

获取当前设备信息：

```csharp
MyDeviceInfo = $"设备类型:{DeviceInfo.Model}\n设备名:{DeviceInfo.Name}";
```

可用属性：
- `DeviceInfo.Model` - 设备型号
- `DeviceInfo.Name` - 设备名称
- `DeviceInfo.Manufacturer` - 制造商
- `DeviceInfo.VersionString` - 系统版本
- `DeviceInfo.Platform` - 平台 (Android)
- `DeviceInfo.Idiom` - 设备类型 (Phone/Tablet)
- `DeviceInfo.DeviceType` - 物理机/虚拟机

## 深色/浅色主题切换

```csharp
SwitchThemeCommand = new DelegateCommand(() =>
{
    if (App.Current == null) return;

    var currentActual = App.Current.ActualThemeVariant;
    if (currentActual == ThemeVariant.Dark)
        App.Current.RequestedThemeVariant = ThemeVariant.Light;
    else
        App.Current.RequestedThemeVariant = ThemeVariant.Dark;
});
```

## 窗口通知

项目实现了 `NotificationService` 通知服务：

```csharp
// 注入服务
private readonly INotificationService _notificationService;

// 设置通知宿主窗口
SetNotificationHostCommand = new DelegateCommand<Control>(control =>
{
    var topLevel = TopLevel.GetTopLevel(control);
    _notificationService.SetHostWindow(topLevel);
});

// 显示通知
_notificationService.ShowInfo("标题", "信息内容");
_notificationService.ShowWarning("标题", "警告内容");
_notificationService.ShowError("标题", "错误内容");
_notificationService.ShowSuccess("标题", "成功内容");

// 带点击回调的通知
_notificationService.ShowInfo("标题", "内容", () =>
{
    // 点击通知后的操作
});

// 自定义超时时间
_notificationService.Show("标题", "内容", null, NotificationType.Info, 5); // 5秒
```

### 通知类型

| 方法 | 类型 | 颜色 |
|------|------|------|
| `ShowInfo` | Information | 蓝 |
| `ShowWarning` | Warning | 黄/橙 |
| `ShowError` | Error | 红 |
| `ShowSuccess` | Success | 绿 |

## 权限检查模式

所有需要权限的功能都采用以下模式：

```csharp
// 1. 检查当前权限状态
var status = await Permissions.CheckStatusAsync<Permissions.XXX>();

// 2. 如果未授权，则申请
if (status != PermissionStatus.Granted)
    status = await Permissions.RequestAsync<Permissions.XXX>();

// 3. 只有授权后才执行操作
if (status == PermissionStatus.Granted)
{
    // 执行需要权限的操作
}
```

## MainActivity 权限初始化

确保 `MainActivity.cs` 中正确初始化了 MAUI Platform：

```csharp
using Microsoft.Maui.ApplicationModel;

namespace PrismAvaInMobilePlatform.Android
{
    [Activity(Label = "你的应用名", ...)]
    public class MainActivity : AvaloniaMainActivity<App>
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);  // 关键：初始化 MAUI Platform
        }

        public override void OnRequestPermissionsResult(...)
        {
            Platform.OnRequestPermissionsResult(...);  // 关键：处理权限结果
            base.OnRequestPermissionsResult(...);
        }
    }
}
```

## 下一步

- 查看 [入门指南](getting-started.md)
