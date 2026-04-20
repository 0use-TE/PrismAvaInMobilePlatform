# 浏览器功能

## 功能说明

在系统默认浏览器中打开指定网址。

## 所需权限

| 权限 | 值 | 说明 |
|------|-----|------|
| `INTERNET` | `android.permission.INTERNET` | 访问网络 |

## Android 权限配置

```xml
<!-- AndroidManifest.xml -->
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
    <uses-permission android:name="android.permission.INTERNET" />
</manifest>
```

## 完整代码实现

### 打开网址

```csharp
using Microsoft.Maui.ApplicationModel;
using Prism.Commands;

public class MainViewModel : ViewModelBase
{
    public AsyncDelegateCommand OpenUriCommand { get; set; }

    public MainViewModel()
    {
        OpenUriCommand = new AsyncDelegateCommand(async () =>
        {
            // 在系统默认浏览器中打开网址
            await Browser.Default.OpenAsync("https://0use.net");
        });
    }
}
```

### XAML 绑定

```xml
<Button Command="{Binding OpenUriCommand}">打开网址</Button>
```

## 核心 API

### Browser.Default.OpenAsync()

```csharp
// 基本用法：打开网址
await Browser.Default.OpenAsync("https://example.com");

// 带启动模式
await Browser.Default.OpenAsync("https://example.com", BrowserLaunchMode.System);
```

### BrowserLaunchMode 启动模式

```csharp
public enum BrowserLaunchMode
{
    // 在系统浏览器中打开（会切换到浏览器应用）
    System,

    // 在应用内浏览器打开（如果可用）
    InApp,

    // 外部应用方式（通过 Intent）
    External
}
```

## 打开特定网址的命令

```csharp
public AsyncDelegateCommand OpenBaiduCommand { get; set; }
public AsyncDelegateCommand OpenGoogleCommand { get; set; }
public AsyncDelegateCommand OpenGithubCommand { get; set; }

public MainViewModel()
{
    OpenBaiduCommand = new AsyncDelegateCommand(async () =>
    {
        await Browser.Default.OpenAsync("https://www.baidu.com");
    });

    OpenGoogleCommand = new AsyncDelegateCommand(async () =>
    {
        await Browser.Default.OpenAsync("https://www.google.com");
    });

    OpenGithubCommand = new AsyncDelegateCommand(async () =>
    {
        await Browser.Default.OpenAsync("https://github.com");
    });
}
```

### XAML

```xml
<StackPanel Spacing="5">
    <Button Command="{Binding OpenBaiduCommand}">百度</Button>
    <Button Command="{Binding OpenGoogleCommand}">Google</Button>
    <Button Command="{Binding OpenGithubCommand}">GitHub</Button>
</StackPanel>
```

## 带用户确认的浏览器跳转

```csharp
public AsyncDelegateCommand<string> OpenUrlCommand { get; set; }

public MainViewModel()
{
    OpenUrlCommand = new AsyncDelegateCommand<string>(async (url) =>
    {
        if (string.IsNullOrWhiteSpace(url))
            return;

        // 简单的 URL 验证
        if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            url = "https://" + url;

        await Browser.Default.OpenAsync(url);
    });
}
```

### XAML

```xml
<TextBox x:Name="UrlTextBox" PlaceholderText="输入网址" />
<Button Command="{Binding OpenUrlCommand}"
        CommandParameter="{Binding Text, ElementName=UrlTextBox}">
    跳转
</Button>
```

## 检测浏览器是否可用

```csharp
public AsyncDelegateCommand TestBrowserCommand { get; set; }

public MainViewModel()
{
    TestBrowserCommand = new AsyncDelegateCommand(async () =>
    {
        try
        {
            // 测试能否打开浏览器（不会真正打开）
            var uri = new Uri("https://example.com");
            var supportsBrowser = await Browser.CanOpenAsync(uri);
            // 注意：CanOpenAsync 在某些平台可能不可用
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"浏览器不可用: {ex.Message}");
        }
    });
}
```

## 相关文件

- `ViewModels/MainViewModel.cs` - 命令定义
- `Views/MainView.axaml` - 界面绑定
- `AndroidManifest.xml` - 权限声明

## 下一步

- [手电筒](flashlight.md)
- [震动](vibration.md)
