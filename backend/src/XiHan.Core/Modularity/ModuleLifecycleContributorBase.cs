#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ModuleLifecycleContributorBase
// Guid:4e2ce123-8b33-4574-90e8-036486739c7a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 23:40:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Core.Application;

namespace XiHan.Core.Modularity;

/// <summary>
/// 模块生命周期贡献者基类
/// </summary>
public abstract class ModuleLifecycleContributorBase : IModuleLifecycleContributor
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    public virtual Task InitializeAsync(ApplicationInitializationContext context, IXiHanModule module)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    public virtual void Initialize(ApplicationInitializationContext context, IXiHanModule module)
    {
    }

    /// <summary>
    /// 关闭
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    public virtual Task ShutdownAsync(ApplicationShutdownContext context, IXiHanModule module)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 关闭
    /// </summary>
    /// <param name="context"></param>
    /// <param name="module"></param>
    public virtual void Shutdown(ApplicationShutdownContext context, IXiHanModule module)
    {
    }
}