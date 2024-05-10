#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:TransientCachedServiceProvider
// Guid:5627581e-28d8-46d1-b44d-7774cb6b41b1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/27 22:36:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Framework.Core.DependencyInjection;

/// <summary>
/// 瞬时缓存服务提供器
/// </summary>
[ExposeServices(typeof(ITransientCachedServiceProvider))]
public class TransientCachedServiceProvider : CachedServiceProviderBase, ITransientCachedServiceProvider, ITransientDependency
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider"></param>
    public TransientCachedServiceProvider(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }
}