# 闪光灯

## 配置

**1. AndroidManifest.xml 声明权限:**
```xml
<uses-permission android:name="android.permission.FLASHLIGHT" />
```

**2. 命名空间:**
```csharp
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
```

## 代码示例

### 打开闪光灯

```csharp
[RelayCommand]
async Task OpenFlashlight()
{
    var status = await Permissions.CheckStatusAsync<Permissions.Flashlight>();
    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Flashlight>();

    if (status == PermissionStatus.Granted)
    {
        await Flashlight.Default.TurnOnAsync();
    }
}
```

### 关闭闪光灯

```csharp
[RelayCommand]
async Task CloseFlashlight()
{
    var status = await Permissions.CheckStatusAsync<Permissions.Flashlight>();
    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Flashlight>();

    if (status == PermissionStatus.Granted)
    {
        await Flashlight.Default.TurnOffAsync();
    }
}
```

## API 说明

| 方法/属性 | 说明 |
|-----------|------|
| `Flashlight.Default` | 单例实例 |
| `TurnOnAsync()` | 打开闪光灯 |
| `TurnOffAsync()` | 关闭闪光灯 |
| `Flashlight.Default.IsSupported` | 是否支持闪光灯 |

## 注意事项

- 并非所有设备都支持闪光灯控制
- 使用前应检查 `IsSupported` 属性
- 闪光灯是共享资源，使用后应及时关闭