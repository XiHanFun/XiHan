#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IServiceProviderAccessor
// Guid:2fe15bfb-fd9e-467e-a908-654133e75040
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 上午 10:54:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.DependencyInjection;

/// <summary>
/// 服务提供者访问器接口
/// </summary>
public interface IServiceProviderAccessor
{
    /// <summary>
    /// 服务提供者
    /// </summary>
    IServiceProvider ServiceProvider { get; }
}