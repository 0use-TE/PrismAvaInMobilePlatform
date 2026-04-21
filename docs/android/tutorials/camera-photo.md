# 相机拍照

## 配置

**1. AndroidManifest.xml 声明权限:**
```xml
<uses-permission android:name="android.permission.CAMERA" />
```

**2. 命名空间:**
```csharp
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Media;
```

## 代码示例

```csharp
[RelayCommand]
async Task TakePhoto()
{
    // 1. 检查并请求权限
    var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Camera>();

    if (status != PermissionStatus.Granted)
        return;

    // 2. 调用拍照 API
    var photo = await MediaPicker.Default.CapturePhotoAsync();

    if (photo != null)
    {
        // 3. 处理照片
        string localPath = photo.FullPath;
        // 使用照片...
    }
}
```

## 完整示例 (ViewModel)

```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Media;

public partial class MainViewModel : ViewModelBase
{
    [RelayCommand]
    async Task TakePhoto()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
            status = await Permissions.RequestAsync<Permissions.Camera>();

        if (status == PermissionStatus.Granted)
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo != null)
            {
                // photo.FullPath 包含照片路径
                // photo.ContentType 包含 MIME 类型
            }
        }
    }
}
```

## API 说明

| 方法/属性 | 说明 |
|-----------|------|
| `MediaPicker.Default` | 单例实例 |
| `CapturePhotoAsync()` | 打开相机拍照，返回 `FileResult` |
| `photo.FullPath` | 照片完整路径 |
| `photo.ContentType` | MIME 类型 (image/jpeg 等) |

## 注意事项

- 权限请求应在用户触发操作时进行 (如点击按钮)
- 捕获的照片通常保存在临时目录
- 需要 `WRITE_EXTERNAL_STORAGE` 权限保存到公共目录