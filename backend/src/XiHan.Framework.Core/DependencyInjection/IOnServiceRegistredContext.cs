#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IOnServiceRegistredContext
// Guid:dec7a3b7-5d26-4c4b-a57d-6175dfa73d5d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 21:39:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Framework.Core.Collections;
using XiHan.Framework.Core.DynamicProxy;

namespace XiHan.Framework.Core.DependencyInjection;

/// <summary>
/// 服务注册上下文接口
/// </summary>
public interface IOnServiceRegistredContext
{
    /// <summary>
    /// 服务拦截器列表
    /// </summary>
    ITypeList<IInterceptor> Interceptors { get; }

    /// <summary>
    /// 实现类型
    /// </summary>
    Type ImplementationType { get; }
}