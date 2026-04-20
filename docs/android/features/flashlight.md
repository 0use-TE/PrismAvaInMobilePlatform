# 手电筒功能

## 功能说明

控制设备闪光灯（手电筒）的开关。

## 所需权限

| 权限 | 值 | 说明 |
|------|-----|------|
| `FLASHLIGHT` | `android.permission.FLASHLIGHT` | 访问闪光灯 |
| `CAMERA` | `android.permission.CAMERA` | 部分设备需要（相机权限）|

## Android 权限配置

```xml
<!-- AndroidManifest.xml -->
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
    <uses-permission android:name="android.permission.FLASHLIGHT" />
    <uses-permission android:name="android.permission.CAMERA" />
</manifest>
```

## 完整代码实现

### 打开手电筒

```csharp
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Prism.Commands;

public class MainViewModel : ViewModelBase
{
    public AsyncDelegateCommand OpenFlashlightCommand { get; set; }

    public MainViewModel()
    {
        OpenFlashlightCommand = new AsyncDelegateCommand(async () =>
        {
            // ===== 第1步：检查闪光灯权限 =====
            var status = await Permissions.CheckStatusAsync<Permissions.Flashlight>();

            // ===== 第2步：如果未授权，申请权限 =====
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.Flashlight>();

            // ===== 第3步：权限授权后打开闪光灯 =====
            if (status == PermissionStatus.Granted)
            {
                await Microsoft.Maui.Devices.Flashlight.Default.TurnOnAsync();
            }
        });
    }
}
```

### 关闭手电筒

```csharp
public AsyncDelegateCommand CloseFlashlightCommand { get; set; }

public MainViewModel()
{
    CloseFlashlightCommand = new AsyncDelegateCommand(async () =>
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Flashlight>();
        if (status != PermissionStatus.Granted)
            status = await Permissions.RequestAsync<Permissions.Flashlight>();

        if (status == PermissionStatus.Granted)
        {
            await Microsoft.Maui.Devices.Flashlight.Default.TurnOffAsync();
        }
    });
}
```

### XAML 绑定

```xml
<Button Command="{Binding OpenFlashlightCommand}">打开手电筒</Button>
<Button Command="{Binding CloseFlashlightCommand}">关闭手电筒</Button>
```

## 核心 API

### Flashlight.Default

```csharp
// 判断闪光灯是否可用
bool isAvailable = Microsoft.Maui.Devices.Flashlight.Default.IsSupported;

// 打开闪光灯
await Microsoft.Maui.Devices.Flashlight.Default.TurnOnAsync();

// 关闭闪光灯
await Microsoft.Maui.Devices.Flashlight.Default.TurnOffAsync();
```

### 检查闪光灯是否支持

```csharp
OpenFlashlightCommand = new AsyncDelegateCommand(async () =>
{
    // 先检查设备是否支持闪光灯
    if (!Microsoft.Maui.Devices.Flashlight.Default.IsSupported)
    {
        // 设备不支持闪光灯
        return;
    }

    var status = await Permissions.CheckStatusAsync<Permissions.Flashlight>();
    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Flashlight>();

    if (status == PermissionStatus.Granted)
    {
        await Microsoft.Maui.Devices.Flashlight.Default.TurnOnAsync();
    }
});
```

## 带状态显示的完整示例

```csharp
public class MainViewModel : ViewModelBase
{
    private bool _isFlashlightOn;
    public bool IsFlashlightOn
    {
        get => _isFlashlightOn;
        set => SetProperty(ref _isFlashlightOn, value);
    }

    public AsyncDelegateCommand ToggleFlashlightCommand { get; set; }

    public MainViewModel()
    {
        ToggleFlashlightCommand = new AsyncDelegateCommand(async () =>
        {
            if (!Microsoft.Maui.Devices.Flashlight.Default.IsSupported)
                return;

            var status = await Permissions.CheckStatusAsync<Permissions.Flashlight>();
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.Flashlight>();

            if (status == PermissionStatus.Granted)
            {
                if (IsFlashlightOn)
                {
                    await Microsoft.Maui.Devices.Flashlight.Default.TurnOffAsync();
                    IsFlashlightOn = false;
                }
                else
                {
                    await Microsoft.Maui.Devices.Flashlight.Default.TurnOnAsync();
                    IsFlashlightOn = true;
                }
            }
        });
    }
}
```

### XAML 显示状态

```xml
<StackPanel>
    <Button Command="{Binding ToggleFlashlightCommand}"
            Content="{Binding IsFlashlightOn, Converter={x:Static BoolConverters.ToOnOffString}}"/>
    <TextBlock Text="{Binding IsFlashlightOn, StringFormat='手电筒状态: {0}'}" />
</StackPanel>
```

## 相关文件

- `ViewModels/MainViewModel.cs` - 命令定义
- `Views/MainView.axaml` - 界面绑定
- `AndroidManifest.xml` - 权限声明

## 下一步

- [震动](vibration.md)
- [浏览器](browser.md)
