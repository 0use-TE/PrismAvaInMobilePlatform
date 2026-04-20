# 文件选择功能

## 功能说明

从设备相册中选择单张或多张照片。

## 所需权限

FilePicker 在 Android 11+ 通常不需要权限（ scoped storage）。

如需访问外部存储的特定目录，可能需要：

| 权限 | 值 | 说明 |
|------|-----|------|
| `READ_EXTERNAL_STORAGE` | `android.permission.READ_EXTERNAL_STORAGE` | 读取外部存储（Android 10 及以下）|

## Android 权限配置（仅 Android 10 以下需要）

```xml
<!-- AndroidManifest.xml -->
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
    <!-- Android 10 及以下需要 -->
    <uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
</manifest>
```

## 完整代码实现

### 选择单张照片

```csharp
using Microsoft.Maui.Storage;
using Prism.Commands;

public class MainViewModel : ViewModelBase
{
    public AsyncDelegateCommand SelectSinglePhotoCommand { get; set; }

    public MainViewModel()
    {
        SelectSinglePhotoCommand = new AsyncDelegateCommand(async () =>
        {
            // 调用文件选择器
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "请选择一个文件",
                FileTypes = FilePickerFileType.Images  // 只显示图片
            });

            if (result != null)
            {
                string filePath = result.FullPath;
                string fileName = result.FileName;
                string contentType = result.ContentType;
            }
        });
    }
}
```

### 选择多张照片

```csharp
public AsyncDelegateCommand SelectMultiplePhotoCommand { get; set; }

public MainViewModel()
{
    SelectMultiplePhotoCommand = new AsyncDelegateCommand(async () =>
    {
        var results = await FilePicker.Default.PickMultipleAsync(new PickOptions
        {
            PickerTitle = "请选择多个文件",
            FileTypes = FilePickerFileType.Images
        });

        if (results != null)
        {
            foreach (var file in results)
            {
                string filePath = file.FullPath;
                string fileName = file.FileName;
            }
        }
    });
}
```

### XAML 绑定

```xml
<Button Command="{Binding SelectSinglePhotoCommand}">选择单张照片</Button>
<Button Command="{Binding SelectMultiplePhotoCommand}">选择多张照片</Button>
```

## 核心 API

### FilePicker.Default.PickAsync()

```csharp
// 选择单个文件
var result = await FilePicker.Default.PickAsync(options);

// result 是 FileResult 类型
public class FileResult
{
    public string FullPath { get; }     // 完整路径
    public string FileName { get; }     // 文件名
    public string ContentType { get; }  // MIME类型
}
```

### FilePicker.Default.PickMultipleAsync()

```csharp
// 选择多个文件
var results = await FilePicker.Default.PickMultipleAsync(options);

// 返回 IEnumerable<FileResult>
foreach (var file in results)
{
    // 处理每个文件
}
```

### PickOptions 配置

```csharp
var options = new PickOptions
{
    PickerTitle = "选择图片",           // 选择器标题
    FileTypes = FilePickerFileType.Images  // 文件类型过滤
};
```

### FilePickerFileType 预设

```csharp
// 预设的文件类型
FilePickerFileType.Images    // 图片：.png, .gif, .jpg, .jpeg
FilePickerFileType.Pdf      // PDF 文档
FilePickerFileType.Videos   // 视频：.mp4, .mov, .avi
FilePickerFileType.Audio    // 音频：.mp3, .wav, .m4a

// 自定义文件类型
var customTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
{
    { DevicePlatform.Android, new[] { "application/pdf", "application/msword" } },
    { DevicePlatform.iOS, new[] { "public.pdf", "public.msword" } }
});
```

## 自定义文件类型选择

```csharp
SelectSinglePhotoCommand = new AsyncDelegateCommand(async () =>
{
    var result = await FilePicker.Default.PickAsync(new PickOptions
    {
        PickerTitle = "选择文档",
        FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.Android, new[] { "application/pdf", "application/msword",
                                              "application/vnd.openxmlformats-officedocument.wordprocessingml.document" } }
        })
    });
});
```

## 完整权限检查版本（兼容旧版 Android）

```csharp
public AsyncDelegateCommand SelectSinglePhotoCommand { get; set; }

public MainViewModel()
{
    SelectSinglePhotoCommand = new AsyncDelegateCommand(async () =>
    {
#if ANDROID
        // Android 10 及以下需要存储权限
        if (OperatingSystem.IsAndroidVersionAtLeast(30))
        {
            // Android 11+ 使用 Scoped Storage，不需要权限
            await PickPhotoAsync();
        }
        else
        {
            // Android 10 及以下
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.StorageRead>();

            if (status == PermissionStatus.Granted)
                await PickPhotoAsync();
        }
#else
        await PickPhotoAsync();
#endif
    });
}

private async Task PickPhotoAsync()
{
    var result = await FilePicker.Default.PickAsync(new PickOptions
    {
        PickerTitle = "请选择照片",
        FileTypes = FilePickerFileType.Images
    });

    if (result != null)
    {
        // 处理选中的文件
    }
}
```

## 相关文件

- `ViewModels/MainViewModel.cs` - 命令定义
- `Views/MainView.axaml` - 界面绑定

## 下一步

- [拍照](camera-photo.md)
- [拍视频](camera-video.md)
