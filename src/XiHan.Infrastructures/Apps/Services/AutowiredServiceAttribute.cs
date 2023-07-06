#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AutowiredServiceAttribute
// Guid:21848b11-9847-4b0e-86a4-9a015eda2c0a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-21 下午 01:01:10
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructures.Apps.Services;

/// <summary>
/// AutowiredServiceAttribute
/// <example> 调用示例：
/// <code>
/// // 将属性注入 Service 实例
/// public class PropertyClass
/// {
///     [AutowiredService]
///     public IService Service { get; set; }
///
///     public PropertyClass(AppAutowiredService appAutowiredService)
///     {
///         appAutowiredService.Autowired(this);
///     }
/// }
/// // 通过字段注入 Service 实例
/// public class FieldClass
/// {
///     [AutowiredService]
///     public IService _service;
///
///     public FieldClass(AppAutowiredService appAutowiredService)
///     {
///         appAutowiredService.Autowired(this);
///     }
/// }
/// </code>
/// </example>
/// </summary>
/// <remarks>由此启发：<see href="https://www.cnblogs.com/loogn/p/10566510.html"/></remarks>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class AutowiredServiceAttribute : Attribute
{
}