#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:OnApplicationShutdownModuleLifecycleContributor
// Guid:dbc06a3a-7e0d-4011-ad82-4b14beb57cbc
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 23:44:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Core.Application.Abstracts;
using XiHan.Core.Application.Contexts;
using XiHan.Core.Modularity.Abstracts;

namespace XiHan.Core.Modularity.Contributors;

/// <summary>
/// 应用程序关闭生命周期贡献者
/// </summary>
public class OnApplicationShutdownModuleLifecycleContributor : ModuleLifecycleContributorBase
{
    /// <summary>
    /// 关闭
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    public override async Task ShutdownAsync(ApplicationShutdownContext context, IModule module)
    {
        if (module is IOnApplicationShutdown onApplicationShutdown)
        {
            await onApplicationShutdown.OnApplicationShutdownAsync(context);
        }
    }

    /// <summary>
    /// 关闭
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    public override void Shutdown(ApplicationShutdownContext context, IModule module)
    {
        (module as IOnApplicationShutdown)?.OnApplicationShutdown(context);
    }
}