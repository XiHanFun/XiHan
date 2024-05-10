#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DisableXiHanFeaturesAttribute
// Guid:dbcd61cf-4de2-47a7-b781-392cef553b6a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/26 23:54:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Framework.Core.Application;

/// <summary>
/// 禁用曦寒功能特性
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class DisableXiHanFeaturesAttribute : Attribute
{
    /// <summary>
    /// 框架将不会为该类注册任何拦截器
    /// 这将导致所有依赖拦截器的功能无法工作
    /// </summary>
    public bool DisableInterceptors { get; set; } = true;

    /// <summary>
    /// 框架中间件将跳过该类
    /// 这将导致所有依赖中间件的功能无法工作
    /// </summary>
    public bool DisableMiddleware { get; set; } = true;

    /// <summary>
    /// 框架不会为该类移除所有内置过滤器
    /// 这将导致所有依赖过滤器的功能无法工作
    /// </summary>
    public bool DisableMvcFilters { get; set; } = true;
}