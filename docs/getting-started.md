# 快速开始

## 环境要求

- .NET 10.0 SDK
- Android SDK (API 23+)

## 添加依赖

```xml
<PackageReference Include="Microsoft.Maui.Essentials" Version="10.0.51" />
<PackageReference Include="CommunityToolkit.Mvvm" Version="8.4.0" />
```

## 配置 MainActivity

```csharp
using Avalonia.Android;
using Microsoft.Maui.ApplicationModel;

public class MainActivity : AvaloniaMainActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Platform.Init(this, savedInstanceState);
    }

    public override void OnRequestPermissionsResult(
        int requestCode, string[] permissions, Permission[] grantResults)
    {
        Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }
}
```

## 配置 AndroidManifest.xml

```xml
<!-- 危险权限 -->
<uses-permission android:name="android.permission.CAMERA" />
<uses-permission android:name="android.permission.VIBRATE" />

<!-- API < 33 -->
<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
<!-- API >= 33 -->
<uses-permission android:name="android.permission.READ_MEDIA_IMAGES" />
```

## 相关文档

- [权限配置](android/permissions.md)
- [API 教程](android/tutorials/index.md)