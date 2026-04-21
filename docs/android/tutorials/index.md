# MAUI API 教程

本节详细介绍各 Android 功能 API 的使用方法。

## 目录

- [相机拍照](camera-photo.md) - 拍摄照片
- [相机录像](camera-video.md) - 录制视频
- [文件选择器](file-picker.md) - 选择图片文件
- [闪光灯](flashlight.md) - 控制手电筒
- [振动](vibration.md) - 设备振动
- [浏览器](browser.md) - 打开网页链接

## 通用模式

每个 API 调用的标准流程:

1. **检查权限** - `Permissions.CheckStatusAsync<T>()`
2. **请求权限** - `Permissions.RequestAsync<T>()`
3. **调用 API** - 执行实际操作

```csharp
// 标准模板
var status = await Permissions.CheckStatusAsync<T>();
if (status != PermissionStatus.Granted)
    status = await Permissions.RequestAsync<T>();

if (status == PermissionStatus.Granted)
{
    // 调用 API
}
```

## 必要条件

确保已添加包引用:
```xml
<PackageReference Include="Microsoft.Maui.Essentials" Version="10.0.51" />
```

并引入命名空间:
```csharp
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;
```