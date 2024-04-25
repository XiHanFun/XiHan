#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ServiceRegistrationActionList
// Guid:d2d19695-1a0d-44a3-8abb-f8af5599df44
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 21:49:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Core.DependencyInjection.Contexts;

namespace XiHan.Core.DependencyInjection;

/// <summary>
/// 注册服务时的操作列表
/// </summary>
public class ServiceRegistrationActionList : List<Action<IOnServiceRegistredContext>>
{
    /// <summary>
    /// 是否禁用类拦截器
    /// </summary>
    public bool IsClassInterceptorsDisabled { get; set; }
}