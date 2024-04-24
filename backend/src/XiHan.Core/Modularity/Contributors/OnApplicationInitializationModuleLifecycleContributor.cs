#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:OnApplicationInitializationModuleLifecycleContributor
// Guid:57d87346-3882-4393-9fa6-052b4ffcd962
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 23:41:47
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Core.Application.Abstracts;
using XiHan.Core.Application.Contexts;
using XiHan.Core.Modularity.Abstracts;

namespace XiHan.Core.Modularity.Contributors;

/// <summary>
/// 应用初始化前模块生命周期贡献者
/// </summary>
public class OnApplicationInitializationModuleLifecycleContributor : ModuleLifecycleContributorBase
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    public override async Task InitializeAsync(ApplicationInitializationContext context, IModule module)
    {
        if (module is IOnApplicationInitialization onApplicationInitialization)
        {
            await onApplicationInitialization.OnApplicationInitializationAsync(context);
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    public override void Initialize(ApplicationInitializationContext context, IModule module)
    {
        (module as IOnApplicationInitialization)?.OnApplicationInitialization(context);
    }
}