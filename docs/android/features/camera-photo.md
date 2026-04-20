# 拍照功能

## 功能说明

调用 Android 系统摄像头拍照，获取照片文件路径。

## 所需权限

| 权限 | 值 | 说明 |
|------|-----|------|
| `CAMERA` | `android.permission.CAMERA` | 访问摄像头 |

## Android 权限配置

```xml
<!-- AndroidManifest.xml -->
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
    <uses-permission android:name="android.permission.CAMERA" />
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
    public AsyncDelegateCommand TakePhotoCommand { get; set; }

    public MainViewModel()
    {
        TakePhotoCommand = new AsyncDelegateCommand(async () =>
        {
            // ===== 第1步：检查摄像头权限 =====
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

            // ===== 第2步：如果未授权，申请权限 =====
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.Camera>();

            // ===== 第3步：权限授权后执行拍照 =====
            if (status == PermissionStatus.Granted)
            {
                // 调用系统摄像头拍照
                var photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // 获取照片的本地路径
                    string localPath = photo.FullPath;

                    // 也可以读取文件内容
                    // using var stream = await photo.OpenReadAsync();
                }
            }
        });
    }
}
```

### XAML 绑定

```xml
<Button Command="{Binding TakePhotoCommand}">拍照</Button>
```

## 核心 API

### MediaPicker.Default.CapturePhotoAsync()

```csharp
// 返回值：FileBase 类型
var photo = await MediaPicker.Default.CapturePhotoAsync();

// photo 常用属性
string fullPath = photo.FullPath;       // 文件完整路径
string fileName = photo.FileName;      // 文件名
string contentType = photo.ContentType; // MIME 类型，如 "image/jpeg"
```

### 照片文件对象

```csharp
// FileBase 类的常用属性
public string FullPath { get; }        // 完整路径
public string FileName { get; }        // 文件名
public string ContentType { get; }     // MIME类型

// 读取文件内容
public Task<Stream> OpenReadAsync();
```

## 处理流程图

```
用户点击按钮
      │
      ▼
CheckStatusAsync(Camera)  ──→ 已授权 ──→ 拍照
      │                                  │
      ▼                                  ▼
  未授权？                         保存照片路径
      │
      ▼
RequestAsync(Camera)  ──→ 授权成功 ──→ 拍照
      │                                  │
      ▼                                  ▼
   授权失败                          保存照片路径
```

## 进阶用法

### 保存照片到指定目录

```csharp
TakePhotoCommand = new AsyncDelegateCommand(async () =>
{
    var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Camera>();

    if (status == PermissionStatus.Granted)
    {
        var photo = await MediaPicker.Default.CapturePhotoAsync();

        if (photo != null)
        {
            // 读取照片流
            using var stream = await photo.OpenReadAsync();

            // 保存到应用数据目录
            var appDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Photos"
            );
            Directory.CreateDirectory(appDir);

            var fileName = $"{Guid.NewGuid()}.jpg";
            var filePath = Path.Combine(appDir, fileName);

            using var fileStream = File.Create(filePath);
            await stream.CopyToAsync(fileStream);
        }
    }
});
```

### 带错误处理的完整版本

```csharp
TakePhotoCommand = new AsyncDelegateCommand(async () =>
{
    try
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

        if (status == PermissionStatus.Denied)
        {
            // 权限被拒绝，尝试重新请求
            status = await Permissions.RequestAsync<Permissions.Camera>();
        }

        if (status == PermissionStatus.Granted)
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                string localPath = photo.FullPath;
                // 处理照片...
            }
        }
        else if (status == PermissionStatus.Denied)
        {
            // 用户拒绝授权，提示去设置开启
            await Launcher.OpenAsync(SettingsUri);
        }
    }
    catch (Exception ex)
    {
        // 处理异常
        System.Diagnostics.Debug.WriteLine($"拍照失败: {ex.Message}");
    }
});
```

## 相关文件

- `ViewModels/MainViewModel.cs` - 命令定义
- `Views/MainView.axaml` - 界面绑定
- `AndroidManifest.xml` - 权限声明

## 下一步

- [拍视频](camera-video.md)
- [文件选择](file-picker.md)
