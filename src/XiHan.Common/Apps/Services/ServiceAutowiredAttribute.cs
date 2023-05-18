#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ServiceAutowiredAttribute
// Guid:21848b11-9847-4b0e-86a4-9a015eda2c0a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-21 下午 01:01:10
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Common.Apps.Services;

/// <summary>
/// ServiceAutowiredAttribute
/// </summary>
/// <code>
/// // 待注入属性和字段的类
/// public class HomeController : Controller
/// {
///     // 将通过属性注入 Service 实例
///     [ServiceAutowired]
///     public IService Service { get; set; }
///
///     // 将通过字段注入 Service 实例
///     [ServiceAutowired]
///     public IService _service;
///
///     public MyClass(AppServiceAutowired appServiceAutowired)
///     {
///         appServiceAutowired.Autowired(this);
///     }
/// }
/// </code>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ServiceAutowiredAttribute : Attribute
{
}