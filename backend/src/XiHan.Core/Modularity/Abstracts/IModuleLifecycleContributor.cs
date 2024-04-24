#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IModuleLifecycleContributor
// Guid:b75ec1da-dac0-4d71-9f4c-6b7e2ecf928f
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 02:47:09
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using XiHan.Core.Application.Contexts;
using XiHan.Core.DependencyInjection.Abstracts;

namespace XiHan.Core.Modularity.Abstracts;

/// <summary>
/// 模块生命周期贡献者接口
/// </summary>
public interface IModuleLifecycleContributor : ITransientDependency
{
    /// <summary>
    /// 初始化，异步
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    Task InitializeAsync([NotNull] ApplicationInitializationContext context, [NotNull] IModule module);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    void Initialize([NotNull] ApplicationInitializationContext context, [NotNull] IModule module);

    /// <summary>
    /// 关闭，异步
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    Task ShutdownAsync([NotNull] ApplicationShutdownContext context, [NotNull] IModule module);

    /// <summary>
    /// 关闭
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    void Shutdown([NotNull] ApplicationShutdownContext context, [NotNull] IModule module);
}