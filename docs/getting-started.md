# 入门指南

## 环境配置

### 系统要求

- **.NET 10.0 SDK** 或更高版本
- **Android SDK** (用于 Android 开发)
- **WebAssembly 支持** (用于浏览器开发)

### 安装步骤

1. 安装 .NET 10.0 SDK
   ```bash
   dotnet --version  # 确认版本为 10.0.x
   ```

2. (可选) 安装 Android SDK
   - 使用 Android Studio 或 Visual Studio 的 Android 工作负载
   - 确保 `ANDROID_HOME` 环境变量已设置

## 运行项目

### 运行桌面/浏览器版本

```bash
cd PrismAvaInMobilePlatform
dotnet run
```

### 运行 Android 版本

```bash
dotnet run -f net10.0-android
```

### 发布 Android 应用

```bash
dotnet publish -f net10.0-android -c Release
```

## 项目结构说明

```
PrismAvaInMobilePlatform/
├── PrismAvaInMobilePlatform/           # 核心共享项目
│   ├── App.xaml(.cs)                   # 应用入口
│   ├── Models/                         # 数据模型
│   ├── Services/                       # 业务服务
│   │   └── DataPersistenceServices/    # 数据持久化服务
│   ├── ViewModels/                     # 视图模型
│   └── Views/                          # 视图 (XAML)
├── PrismAvaInMobilePlatform.Android/   # Android 特定项目
└── PrismAvaInMobilePlatform.Desktop/   # 桌面特定项目
```

## 添加新页面

### 1. 创建视图

在 `Views` 文件夹下创建新的 XAML 文件:

```xml
<UserControl xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             x:Class="PrismAvaInMobilePlatform.Views.NewPage">
  <StackPanel>
    <TextBlock Text="Hello, World!" />
  </StackPanel>
</UserControl>
```

### 2. 创建视图模型

```csharp
using Prism.Navigation;
using Prism.Mvvm;

namespace PrismAvaInMobilePlatform.ViewModels;

public class NewPageViewModel : BindableBase, INavigationAware
{
    public void OnNavigatedFrom(INavigationParameters parameters) { }

    public void OnNavigatedTo(INavigationParameters parameters) { }
}
```

### 3. 注册视图和视图模型

在 `App.xaml.cs` 或模块初始化类中注册:

```csharp
ContainerRegistry.RegisterForNavigation<NewPage, NewPageViewModel>();
```

### 4. 导航到新页面

```csharp
NavigationService.NavigateAsync("NewPage");
```

## 依赖注入

使用构造函数注入获取服务:

```csharp
public class MyViewModel
{
    private readonly IMyService _myService;

    public MyViewModel(IMyService myService)
    {
        _myService = myService;
    }
}
```

## 日志使用

项目集成了 Serilog，可以直接通过 `ILogger` 使用:

```csharp
public class MyService
{
    private readonly ILogger _logger;

    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
    }

    public void DoSomething()
    {
        _logger.Information("执行操作...");
    }
}
```

日志会自动输出到:
- Debug 输出窗口
- 文件 (在应用数据目录下)

## 常见问题

### Android 构建失败

确保已正确安装 Android SDK:
```bash
dotnet workload install android
```

### Browser 版本无法运行

确保安装 WebAssembly 工作负载:
```bash
dotnet workload install wasm-tools
```

## 下一步

- 查看 [项目介绍](introduction.md) 了解更多架构细节
- 探索现有代码了解项目实践
