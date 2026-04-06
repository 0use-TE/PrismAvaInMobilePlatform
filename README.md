# PrismAvaInMobilePlatform

跨平台移动端权限演示应用，基于 Avalonia UI + Microsoft.Maui 实现。

## 项目结构

```
PrismAvaInMobilePlatform/
├── PrismAvaInMobilePlatform/          # 核心共享项目
│   ├── ViewModels/                    # ViewModel 层
│   ├── Views/                         # 视图层
│   └── Services/                      # 服务层
├── PrismAvaInMobilePlatform.Android/  # Android 平台项目
└── PrismAvaInMobilePlatform.Desktop/  # 桌面平台项目
```

## Android 权限说明

本应用在 `AndroidManifest.xml` 中声明了以下权限：

| 权限 | 用途 | 敏感级别 |
|------|------|----------|
| `INTERNET` | 网络访问 | 普通 |
| `CAMERA` | 拍照/录像 | 危险 |
| `FLASHLIGHT` | 控制闪光灯 | 普通 |
| `VIBRATE` | 设备振动 | 普通 |
| `READ_EXTERNAL_STORAGE` | 读取外部存储 | 危险 |
| `WRITE_EXTERNAL_STORAGE` | 写入外部存储 | 危险 |
| `READ_MEDIA_AUDIO` | 读取音频文件 | 危险 |
| `READ_MEDIA_IMAGES` | 读取图片文件 | 危险 |
| `READ_MEDIA_VIDEO` | 读取视频文件 | 危险 |

### 危险权限说明

Android 6.0（API 23）及以上版本，以下权限需要在运行时动态申请：

| 权限 | 说明 |
|------|------|
| `CAMERA` | 拍照和录制视频 |
| `READ_EXTERNAL_STORAGE` / `WRITE_EXTERNAL_STORAGE` | 文件读写（API < 33） |
| `READ_MEDIA_*` | 媒体文件读取（API ≥ 33） |

### Android 13+（API 33）权限变化

Android 13 引入了细化的媒体权限，应用需要使用 `READ_MEDIA_AUDIO`、`READ_MEDIA_IMAGES`、`READ_MEDIA_VIDEO` 替代原来的 `READ_EXTERNAL_STORAGE`。

项目 `TargetFramework` 为 `net10.0-android`，最低支持 API 23。

## 运行时权限请求

应用使用 `Microsoft.Maui.Permissions` API 进行运行时权限检查与申请：

```csharp
// 检查权限状态
var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

// 请求权限
if (status != PermissionStatus.Granted)
    status = await Permissions.RequestAsync<Permissions.Camera>();

if (status == PermissionStatus.Granted)
{
    // 权限已授予，执行对应操作
}
```

### 权限请求流程

1. **检查权限** — `CheckStatusAsync<T>()` 查询当前权限状态
2. **请求权限** — `RequestAsync<T>()` 弹出系统权限对话框
3. **处理结果** — `PermissionStatus.Granted`（已授权）、`Denied`（拒绝）、`PermanentlyDenied`（永久拒绝，需手动设置）等

### 完整权限请求示例

```csharp
TakePhotoCommand = new AsyncDelegateCommand(async () =>
{
    var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Camera>();

    if (status == PermissionStatus.Granted)
    {
        var photo = await MediaPicker.Default.CapturePhotoAsync();
        // 处理照片
    }
});
```

## 应用功能

| 功能 | 所需权限 | 平台 |
|------|----------|------|
| 拍照 | `CAMERA` | Android |
| 录像 | `CAMERA` | Android |
| 选择单张图片 | —（使用系统Picker） | 全平台 |
| 选择多张图片 | —（使用系统Picker） | 全平台 |
| 闪光灯开关 | `FLASHLIGHT` | Android |
| 设备振动 | `VIBRATE` | Android |
| 浏览器打开链接 | — | 全平台 |
| 深色/浅色主题切换 | — | 全平台 |

## 平台说明

- **Android** — 支持相机、闪光灯、振动等硬件权限
- **Desktop** — 桌面平台，权限功能不可用
- **Browser** — Web 平台，权限功能不可用

## 构建

```bash
# Android APK
dotnet build PrismAvaInMobilePlatform.Android/PrismAvaInMobilePlatform.Android.csproj -c Debug

# 桌面端
dotnet build PrismAvaInMobilePlatform.Desktop/PrismAvaInMobilePlatform.Desktop.csproj -c Debug
```
