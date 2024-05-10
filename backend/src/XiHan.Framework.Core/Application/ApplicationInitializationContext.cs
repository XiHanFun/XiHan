#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ApplicationInitializationContext
// Guid:5a8648c4-82bd-41ad-bd1a-967d24b63ca1
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 上午 10:52:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using XiHan.Framework.Core.DependencyInjection;
using XiHan.Framework.Core.Verification;

namespace XiHan.Framework.Core.Application;

/// <summary>
/// 应用初始化上下文
/// </summary>
public class ApplicationInitializationContext : IServiceProviderAccessor
{
    /// <summary>
    /// 服务提供者
    /// </summary>
    public IServiceProvider ServiceProvider { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationInitializationContext([NotNull] IServiceProvider serviceProvider)
    {
        CheckHelper.NotNull(serviceProvider, nameof(serviceProvider));

        ServiceProvider = serviceProvider;
    }
}