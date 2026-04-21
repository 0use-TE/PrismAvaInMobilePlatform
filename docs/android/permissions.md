# Android 权限配置

## 权限分类

| 类型 | 说明 | 处理方式 |
|------|------|----------|
| 普通权限 | 不涉及隐私 | 自动授予，无需请求 |
| 危险权限 | 涉及隐私数据 | 需运行时请求 |

### 普通权限 (自动授予)

- `INTERNET` - 网络访问
- `FLASHLIGHT` - 闪光灯
- `VIBRATE` - 振动

### 危险权限 (需请求)

- `CAMERA` - 相机
- `READ_EXTERNAL_STORAGE` - 读取存储 (API < 33)
- `WRITE_EXTERNAL_STORAGE` - 写入存储 (API < 33)
- `READ_MEDIA_IMAGES` - 读取图片 (API >= 33)
- `READ_MEDIA_VIDEO` - 读取视频 (API >= 33)
- `READ_MEDIA_AUDIO` - 读取音频 (API >= 33)

## 存储权限版本差异

### API < 33 (Android 12 及以下)

使用旧版存储权限:

```xml
<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
```

### API >= 33 (Android 13+)

使用新版媒体权限:

```xml
<uses-permission android:name="android.permission.READ_MEDIA_IMAGES" />
<uses-permission android:name="android.permission.READ_MEDIA_VIDEO" />
<uses-permission android:name="android.permission.READ_MEDIA_AUDIO" />
```

### 完整示例 (兼容所有版本)

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
    <!-- 普通权限 -->
    <uses-permission android:name="android.permission.INTERNET" />
    <uses-permission android:name="android.permission.FLASHLIGHT" />
    <uses-permission android:name="android.permission.VIBRATE" />

    <!-- 危险权限 -->
    <uses-permission android:name="android.permission.CAMERA" />

    <!-- API < 33 存储权限 -->
    <uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
    <uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />

    <!-- API >= 33 媒体权限 -->
    <uses-permission android:name="android.permission.READ_MEDIA_IMAGES" />
    <uses-permission android:name="android.permission.READ_MEDIA_VIDEO" />
    <uses-permission android:name="android.permission.READ_MEDIA_AUDIO" />
</manifest>
```

## 运行时权限请求

使用 `Microsoft.Maui.ApplicationModel.Permissions`:

```csharp
using Microsoft.Maui.ApplicationModel;
using static Microsoft.Maui.ApplicationModel.Permissions;

public async Task CheckAndRequestAsync<T>() where T : Permissions.BaseRequest
{
    // 1. 检查当前状态
    var status = await Permissions.CheckStatusAsync<T>();

    if (status != PermissionStatus.Granted)
    {
        // 2. 请求权限
        status = await Permissions.RequestAsync<T>();
    }

    // 3. 处理结果
    switch (status)
    {
        case PermissionStatus.Granted:
            // 权限已授予
            break;
        case PermissionStatus.Denied:
            // 用户拒绝
            break;
        case PermissionStatus.PermanentlyDenied:
            // 永久拒绝，需引导用户去设置开启
            break;
    }
}
```

## 常用权限对照表

| 功能 | 权限 | MAUI 类 |
|------|------|---------|
| 拍照/录像 | `CAMERA` | `Permissions.Camera` |
| 闪光灯 | `FLASHLIGHT` | `Permissions.Flashlight` |
| 振动 | `VIBRATE` | `Permissions.Vibrate` |
| 读取图片 (旧) | `READ_EXTERNAL_STORAGE` | `Permissions.StorageRead` |
| 读取图片 (新) | `READ_MEDIA_IMAGES` | `Permissions.Photos` |
| 读取视频 (新) | `READ_MEDIA_VIDEO` | `Permissions.Videos` |
| 读取音频 (新) | `READ_MEDIA_AUDIO` | `Permissions.Microphone` |

## MainActivity 权限处理

必须在 MainActivity 中处理权限回调:

```csharp
public override void OnRequestPermissionsResult(
    int requestCode,
    string[] permissions,
    Permission[] grantResults)
{
    Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
}
```

如果不调用 `Platform.Init()` 和 `Platform.OnRequestPermissionsResult()`，MAUI 权限 API 将无法正常工作。