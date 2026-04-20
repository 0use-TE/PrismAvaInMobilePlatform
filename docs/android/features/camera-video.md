# 拍视频功能

## 功能说明

调用 Android 系统摄像头录制视频。

## 所需权限

| 权限 | 值 | 说明 |
|------|-----|------|
| `CAMERA` | `android.permission.CAMERA` | 访问摄像头 |
| `RECORD_AUDIO` | `android.permission.RECORD_AUDIO` | 录制音频 |

## Android 权限配置

```xml
<!-- AndroidManifest.xml -->
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
    <uses-permission android:name="android.permission.CAMERA" />
    <uses-permission android:name="android.permission.RECORD_AUDIO" />
</manifest>
```

## 完整代码实现

### ViewModel 代码

```csharp
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Media;
using Prism.Commands;

public class MainViewModel : ViewModelBase
{
    public AsyncDelegateCommand TakeVideoCommand { get; set; }

    public MainViewModel()
    {
        TakeVideoCommand = new AsyncDelegateCommand(async () =>
        {
            // ===== 第1步：检查摄像头权限 =====
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

            // ===== 第2步：如果未授权，申请权限 =====
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.Camera>();

            // ===== 第3步：权限授权后执行录像 =====
            if (status == PermissionStatus.Granted)
            {
                // 调用系统摄像头录像
                var video = await MediaPicker.Default.CaptureVideoAsync();

                if (video != null)
                {
                    // 获取视频的本地路径
                    string localPath = video.FullPath;
                }
            }
        });
    }
}
```

### XAML 绑定

```xml
<Button Command="{Binding TakeVideoCommand}">拍视频</Button>
```

## 核心 API

### MediaPicker.Default.CaptureVideoAsync()

```csharp
// 返回值：FileBase 类型
var video = await MediaPicker.Default.CaptureVideoAsync();

// video 常用属性
string fullPath = video.FullPath;       // 文件完整路径
string fileName = video.FileName;      // 文件名
string contentType = video.ContentType; // MIME 类型，如 "video/mp4"
```

### 视频文件对象

```csharp
// FileBase 类的常用属性
public string FullPath { get; }        // 完整路径
public string FileName { get; }        // 文件名
public string ContentType { get; }     // MIME类型

// 读取文件内容
public Task<Stream> OpenReadAsync();
```

## 与拍照的区别

| 对比项 | 拍照 | 拍视频 |
|--------|------|--------|
| API | `CapturePhotoAsync()` | `CaptureVideoAsync()` |
| 返回类型 | `FileBase` | `FileBase` |
| 权限 | `CAMERA` | `CAMERA` + `RECORD_AUDIO` |
| ContentType | `image/jpeg` | `video/mp4` |

## 保存视频到应用目录

```csharp
TakeVideoCommand = new AsyncDelegateCommand(async () =>
{
    var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Camera>();

    if (status == PermissionStatus.Granted)
    {
        var video = await MediaPicker.Default.CaptureVideoAsync();

        if (video != null)
        {
            // 读取视频流
            using var stream = await video.OpenReadAsync();

            // 保存到应用数据目录
            var appDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Videos"
            );
            Directory.CreateDirectory(appDir);

            var fileName = $"{Guid.NewGuid()}.mp4";
            var filePath = Path.Combine(appDir, fileName);

            using var fileStream = File.Create(filePath);
            await stream.CopyToAsync(fileStream);
        }
    }
});
```

## 相关文件

- `ViewModels/MainViewModel.cs` - 命令定义
- `Views/MainView.axaml` - 界面绑定
- `AndroidManifest.xml` - 权限声明

## 下一步

- [拍照](camera-photo.md)
- [文件选择](file-picker.md)
