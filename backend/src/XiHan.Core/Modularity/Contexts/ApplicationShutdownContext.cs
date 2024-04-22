#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ApplicationShutdownContext
// Guid:f8f6b474-258c-42e1-b2a5-66692906a275
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 上午 11:17:35
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using XiHan.Core.Verification;

namespace XiHan.Core.Modularity.Contexts;

/// <summary>
/// 应用关闭上下文
/// </summary>
public class ApplicationShutdownContext
{
    /// <summary>
    /// 服务提供者
    /// </summary>
    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationShutdownContext([NotNull] IServiceProvider serviceProvider)
    {
        CheckHelper.NotNull(serviceProvider, nameof(serviceProvider));

        ServiceProvider = serviceProvider;
    }
}