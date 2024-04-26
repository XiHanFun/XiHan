#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:OnPostApplicationInitializationModuleLifecycleContributor
// Guid:8bc733b0-2678-4472-984f-a636d16cd6ea
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 23:44:07
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Core.Application;
using XiHan.Core.Modularity.Abstracts;

namespace XiHan.Core.Modularity.Contributors;

/// <summary>
/// 在应用程序初始化后的模块生命周期贡献者
/// </summary>
public class OnPostApplicationInitializationModuleLifecycleContributor : ModuleLifecycleContributorBase
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    public override async Task InitializeAsync(ApplicationInitializationContext context, IXiHanModule module)
    {
        if (module is IOnPostApplicationInitialization onPostApplicationInitialization)
        {
            await onPostApplicationInitialization.OnPostApplicationInitializationAsync(context);
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    public override void Initialize(ApplicationInitializationContext context, IXiHanModule module)
    {
        (module as IOnPostApplicationInitialization)?.OnPostApplicationInitialization(context);
    }
}