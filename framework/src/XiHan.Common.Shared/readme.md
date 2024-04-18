# XiHan.Common.Shared

曦寒公共共享组件库

## 如何使用

项目引用此 Nuget 包

依赖注入

```csharp
services.RunModuleInitializers(configuration)
```

## 包含功能

### 属性或字段自动注入服务

> 在需要注入的属性或字段上加 AutowiredService 这个 Attribute 后，构造函数注入AutowiredServiceHandler，通过 AutowiredServiceHandler.Autowired(this) 方法，将会自动注入对应的 Service 实例。

通过属性注入 Service 实例

```csharp
public class PropertyClass
{
    [AutowiredService]
    public IService Service { get; set; }

    public PropertyClass(AutowiredServiceHandler autowiredServiceHandler)
    {
        autowiredServiceHandler.Autowired(this);
    }
}
```

通过字段注入 Service 实例

```csharp
public class FieldClass
{
    [AutowiredService]
    public IService _service;

    public FieldClass(AutowiredServiceHandler autowiredServiceHandler)
    {
        autowiredServiceHandler.Autowired(this);
    }
}
```




