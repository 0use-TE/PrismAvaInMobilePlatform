# 振动

## 配置

**1. AndroidManifest.xml 声明权限:**
```xml
<uses-permission android:name="android.permission.VIBRATE" />
```

**2. 命名空间:**
```csharp
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
```

## 代码示例

### 振动一下

```csharp
[RelayCommand]
async Task Vibrate()
{
    var status = await Permissions.CheckStatusAsync<Permissions.Vibrate>();
    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Vibrate>();

    if (status == PermissionStatus.Granted)
    {
        Vibration.Default.Vibrate();
    }
}
```

### 自定义振动时长

```csharp
// 振动 500 毫秒
Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(500));

// 振动模式: 等待0.5秒，振动1秒，等待0.5秒，振动1秒...
Vibration.Default.Vibrate(new[] { 0, 500, 500, 1000 });
```

## API 说明

| 方法/属性 | 说明 |
|-----------|------|
| `Vibration.Default` | 单例实例 |
| `Vibrate()` | 振动默认时长 |
| `Vibrate(TimeSpan)` | 振动指定时长 |
| `Vibrate(long[])` | 振动模式 (数组为 关闭-开启 交替) |
| `Cancel()` | 取消振动 |

## 注意事项

- 振动需要硬件支持
- 某些设备可能不支持自定义时长
- 长时间振动会消耗电量