#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IInterceptor
// Guid:5cd5e378-9a91-4b2b-a2a9-679f9eca179d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 21:39:49
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Framework.Core.DynamicProxy;

/// <summary>
/// 拦截器接口
/// </summary>
public interface IInterceptor
{
    /// <summary>
    /// 异步拦截
    /// </summary>
    /// <param name="invocation"></param>
    /// <returns></returns>
    Task InterceptAsync(IMethodInvocation invocation);
}