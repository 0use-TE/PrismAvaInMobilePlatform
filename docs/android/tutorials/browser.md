# 浏览器

打开网页链接，不需要特殊权限。

## 代码示例

```csharp
[RelayCommand]
async Task OpenUri()
{
    await Browser.Default.OpenAsync(
        "https://0use.net",
        new BrowserLaunchOptions
        {
            LaunchMode = BrowserLaunchMode.SystemPreferred,
            Title = "网站标题",
            ToolbarColor = Color.FromArgb("#FF512DA8"),
            ToolbarControlsColor = Color.FromArgb("#FFFFFFFF")
        });
}
```

## 简化写法

```csharp
[RelayCommand]
async Task OpenUri()
{
    await Browser.Default.OpenAsync("https://0use.net");
}
```

## BrowserLaunchOptions

| 选项 | 说明 |
|------|------|
| `LaunchMode.External` | 外部浏览器打开 |
| `LaunchMode.SystemPreferred` | 应用内打开 (如果支持) |
| `Title` | 页面标题 |
| `ToolbarColor` | 工具栏颜色 |
| `ToolbarControlsColor` | 工具栏按钮颜色 |

## 注意事项

- `BrowserLaunchMode.SystemPreferred` 可能在某些设备上不可用
- 外部浏览器打开会离开应用
- 不需要任何权限声明