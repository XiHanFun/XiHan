#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IConventionalRegistrar
// Guid:d476c5b1-7098-4eac-8791-7907775c7b5c
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 04:47:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace XiHan.Core.DependencyInjection;

/// <summary>
/// 常规注册器接口
/// </summary>
public interface IConventionalRegistrar
{
    /// <summary>
    /// 添加程序集
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    void AddAssembly(IServiceCollection services, Assembly assembly);

    /// <summary>
    /// 添加多个类型
    /// </summary>
    /// <param name="services"></param>
    /// <param name="types"></param>
    void AddTypes(IServiceCollection services, params Type[] types);

    /// <summary>
    /// 添加类型
    /// </summary>
    /// <param name="services"></param>
    /// <param name="type"></param>
    void AddType(IServiceCollection services, Type type);
}