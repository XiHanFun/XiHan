## XiHan.Common.Core

曦寒公共核心组件库

## 如何使用

项目引用此 Nuget 包

## 包含功能

### 模块初始化配置

> 假设你有 CommonShared 这个模块，你可以在 CommonShared 项目中定义一个类，实现 IModuleInitializer 接口，然后在 Startup.cs 中调用 RunModuleInitializers 方法，将会自动加载 CommonShared 模块的初始化配置。

> 这样可以将模块的初始化配置分离到模块项目中，使得模块更加独立。

CommonSharedModuleInitializer.cs

```
/// <summary>
/// 公共共享模块初始化
/// </summary>
public class CommonSharedModuleInitializer : IModuleInitializer
{
    /// <summary>
    /// 初始化服务配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // 属性或字段自动注入服务
        services.AddSingleton<AutowiredServiceHandler>();
    }
}
```

Startup.cs

```csharp
services.RunModuleInitializers(configuration);
```