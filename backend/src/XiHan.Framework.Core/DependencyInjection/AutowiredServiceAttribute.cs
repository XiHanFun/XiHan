#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AutowiredServiceAttribute
// Guid:c13ba899-c064-4ade-96f5-616c96e74146
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 14:33:55
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Framework.Core.DependencyInjection;

/// <summary>
/// AutowiredServiceAttribute
/// <example>
/// 调用示例：
/// <code>
/// // 通过属性注入 Service 实例
/// public class PropertyClass
/// {
///     [AutowiredService]
///     public IService Service { get; set; }
///
///     public PropertyClass(AutowiredServiceHandler autowiredServiceHandler)
///     {
///         autowiredServiceHandler.Autowired(this);
///     }
/// }
/// // 通过字段注入 Service 实例
/// public class FieldClass
/// {
///     [AutowiredService]
///     public IService _service;
///
///     public FieldClass(AutowiredServiceHandler autowiredServiceHandler)
///     {
///         autowiredServiceHandler.Autowired(this);
///     }
/// }
/// </code>
/// </example>
/// </summary>
/// <remarks>由此启发：<see href="https://www.cnblogs.com/loogn/p/10566510.html"/></remarks>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class AutowiredServiceAttribute : Attribute;