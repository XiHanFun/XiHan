## XiHan.Core

曦寒核心组件库

## 如何使用

项目引用此 Nuget 包

## 包含功能

### 模块化自动注入

假设开发一个模块 XiHan.AI ，模块中包含了一些 AI 服务等，可以通过模块化自动注入功能，将模块中的服务，控制器等自动注入到 ASP.NET Core 的依赖注入容器中。

创建模块类，继承 XiHanModule 即可。

```csharp
/// <summary>
/// 人工智能模块
/// </summary>
public partial class XiHanAIModule : XiHanModule
{
    /// <summary>
    /// 配置服务
    /// </summary>
    /// <param name="context"></param>
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}
```