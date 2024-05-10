﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ServiceDescriptorExtensions
// Guid:f8acd9ca-1bc8-4c55-a198-9631c808bb7d
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 下午 04:24:02
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;

namespace XiHan.Framework.Core.Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 服务描述器扩展方法
/// 包括该服务的类型、实现和生存期
/// </summary>
public static class ServiceDescriptorExtensions
{
    /// <summary>
    /// 规范化键控服务和非键控服务之间的实现实例数据
    /// </summary>
    /// <param name="descriptor">
    /// The <see cref="ServiceDescriptor"/> to normalize.
    /// </param>
    /// <returns>
    /// 来自服务描述器的适当实现类型
    /// </returns>
    public static object? NormalizedImplementationInstance(this ServiceDescriptor descriptor) => descriptor.IsKeyedService ? descriptor.KeyedImplementationInstance : descriptor.ImplementationInstance;

    /// <summary>
    /// 规范化键控和非键控服务之间的实现类型数据
    /// </summary>
    /// <param name="descriptor">
    /// The <see cref="ServiceDescriptor"/> to normalize.
    /// </param>
    /// <returns>
    /// 来自服务描述器的适当实现类型
    /// </returns>
    public static Type? NormalizedImplementationType(this ServiceDescriptor descriptor) => descriptor.IsKeyedService ? descriptor.KeyedImplementationType : descriptor.ImplementationType;
}