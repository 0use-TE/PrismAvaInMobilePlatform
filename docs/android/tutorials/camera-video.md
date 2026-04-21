# 相机录像

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
async Task TakeVideo()
{
    // 1. 检查并请求权限
    var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Camera>();

    if (status != PermissionStatus.Granted)
        return;

    // 2. 调用录像 API
    var video = await MediaPicker.Default.CaptureVideoAsync();

    if (video != null)
    {
        // 3. 处理视频
        string localPath = video.FullPath;
        // 使用视频...
    }
}
```

## API 说明

| 方法/属性 | 说明 |
|-----------|------|
| `MediaPicker.Default` | 单例实例 |
| `CaptureVideoAsync()` | 打开相机录像，返回 `FileResult` |
| `video.FullPath` | 视频完整路径 |
| `video.ContentType` | MIME 类型 (video/mp4 等) |

## 注意事项

- 录像会持续进行，直到用户停止
- 视频文件通常保存在临时目录
- 大文件可能需要存储权限才能保存