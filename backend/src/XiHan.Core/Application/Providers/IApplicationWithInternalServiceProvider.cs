#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IApplicationWithInternalServiceProvider
// Guid:3418e8ed-905a-4f29-9770-36a6593fab9b
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 上午 11:25:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Core.Application.Abstracts;

namespace XiHan.Core.Application.Providers;

/// <summary>
/// 具有内部服务的应用程序提供器接口
/// </summary>
public interface IApplicationWithInternalServiceProvider : IApplication
{
    /// <summary>
    /// 创建服务提供器，但不初始化模块。
    /// 多次调用将返回相同的服务提供器，而不会再次创建
    /// </summary>
    IServiceProvider CreateServiceProvider();

    /// <summary>
    /// 创建服务提供商并初始化所有模块，异步
    /// 如果之前调用过 <see cref="CreateServiceProvider"/> 方法，它不会重新创建，而是使用之前的那个
    /// </summary>
    Task InitializeAsync();

    /// <summary>
    /// 创建服务提供商并初始化所有模块
    /// 如果之前调用过 <see cref="CreateServiceProvider"/> 方法，它不会重新创建，而是使用之前的那个
    /// </summary>
    void Initialize();
}