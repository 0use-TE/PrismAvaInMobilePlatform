# 文件选择器

文件选择器不需要危险权限，使用系统Picker直接选择。

## 代码示例

### 选择单张图片

```csharp
[RelayCommand]
async Task SelectSinglePhoto()
{
    var result = await FilePicker.Default.PickAsync(new PickOptions
    {
        PickerTitle = "请选择一张图片",
        FileTypes = FilePickerFileType.Images
    });

    if (result != null)
    {
        string fileName = result.FileName;
        string contentType = result.ContentType;
        using var stream = await result.OpenReadAsync();
        // 处理图片...
    }
}
```

### 选择多张图片

```csharp
[RelayCommand]
async Task SelectMultiplePhoto()
{
    var results = await FilePicker.Default.PickMultipleAsync(new PickOptions
    {
        PickerTitle = "请选择多张图片",
        FileTypes = FilePickerFileType.Images
    });

    foreach (var result in results)
    {
        string fileName = result.FileName;
        // 处理每张图片...
    }
}
```

## 文件类型过滤

```csharp
// 图片类型
FilePickerFileType.Images

// 视频类型
FilePickerFileType.Videos

// 音频类型
FilePickerFileType.Audio

// 自定义类型
var customTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
{
    { DevicePlatform.Android, new[] { "image/*" } }
});
```

## PickOptions 配置

| 属性 | 说明 |
|------|------|
| `PickerTitle` | 选择器标题 |
| `FileTypes` | 文件类型过滤 |

## 注意事项

- 系统Picker会自动处理权限
- 不需要手动请求存储权限
- `OpenReadAsync()` 返回流，使用 `using` 正确释放