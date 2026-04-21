# PrismAvaInMobilePlatform

基于 **Avalonia** 的 Android 跨平台应用。

## 技术栈

- Avalonia 11.3
- CommunityToolkit.Mvvm
- Microsoft.Maui.Essentials

## 构建

```bash
dotnet build PrismAvaInMobilePlatform/PrismAvaInMobilePlatform.Android/PrismAvaInMobilePlatform.Android.csproj -c Debug
```

## Android 权限

| 权限 | 用途 |
|------|------|
| `CAMERA` | 拍照/录像 |
| `FLASHLIGHT` | 手电筒 |
| `VIBRATE` | 振动 |
| `INTERNET` | 网络访问 |