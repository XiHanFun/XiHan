#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ApplicationWithInternalServiceProvider
// Guid:7ae70b99-4656-405f-ac77-e65c516811e5
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 上午 11:36:38
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Core.Microsoft.Extensions.DependencyInjection;

namespace XiHan.Core.Application.Providers;

/// <summary>
/// 具有内部服务的应用程序提供器
/// </summary>
public class ApplicationWithInternalServiceProvider : ApplicationBase, IApplicationWithInternalServiceProvider
{
    /// <summary>
    /// 作用域服务
    /// </summary>
    public IServiceScope? ServiceScope { get; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="startupModuleType"></param>
    /// <param name="optionsAction"></param>
    public ApplicationWithInternalServiceProvider([NotNull] Type startupModuleType, Action<ApplicationCreationOptions>? optionsAction)
        : this(startupModuleType, new ServiceCollection(), optionsAction)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="startupModuleType"></param>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    private ApplicationWithInternalServiceProvider(
        [NotNull] Type startupModuleType,
        [NotNull] IServiceCollection services,
        Action<ApplicationCreationOptions>? optionsAction) : base(startupModuleType, services, optionsAction)
    {
        Services.AddSingleton<IApplicationWithInternalServiceProvider>(this);
    }

    /// <summary>
    /// 创建服务提供器，但不初始化模块。
    /// 多次调用将返回相同的服务提供器，而不会再次创建
    /// </summary>
    public IServiceProvider CreateServiceProvider()
    {
        if (ServiceProvider != null)
        {
            return ServiceProvider;
        }

        ServiceScope = Services.BuildServiceProviderFromFactory().CreateScope();
        SetServiceProvider(ServiceScope.ServiceProvider);

        return ServiceProvider!;
    }

    /// <summary>
    /// 创建服务提供商并初始化所有模块，异步
    /// 如果之前调用过 <see cref="CreateServiceProvider"/> 方法，它不会重新创建，而是使用之前的那个
    /// </summary>
    public async Task InitializeAsync()
    {
        CreateServiceProvider();
        await InitializeModulesAsync();
    }

    /// <summary>
    /// 创建服务提供商并初始化所有模块，异步
    /// 如果之前调用过 <see cref="CreateServiceProvider"/> 方法，它不会重新创建，而是使用之前的那个
    /// </summary>
    public void Initialize()
    {
        CreateServiceProvider();
        InitializeModules();
    }

    /// <summary>
    /// 释放
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();
        ServiceScope?.Dispose();
    }
}