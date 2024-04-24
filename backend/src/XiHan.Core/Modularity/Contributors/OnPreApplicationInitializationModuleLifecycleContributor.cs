#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:OnPreApplicationInitializationModuleLifecycleContributor
// Guid:274aee1d-4eb9-4803-b1a3-6152be150ebb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 23:43:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Core.Application.Abstracts;
using XiHan.Core.Application.Contexts;
using XiHan.Core.Modularity.Abstracts;

namespace XiHan.Core.Modularity.Contributors;

/// <summary>
/// 在应用程序初始化之前的模块生命周期贡献者
/// </summary>
public class OnPreApplicationInitializationModuleLifecycleContributor : ModuleLifecycleContributorBase
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    public override async Task InitializeAsync(ApplicationInitializationContext context, IModule module)
    {
        if (module is IOnPreApplicationInitialization onPreApplicationInitialization)
        {
            await onPreApplicationInitialization.OnPreApplicationInitializationAsync(context);
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    public override void Initialize(ApplicationInitializationContext context, IModule module)
    {
        (module as IOnPreApplicationInitialization)?.OnPreApplicationInitialization(context);
    }
}