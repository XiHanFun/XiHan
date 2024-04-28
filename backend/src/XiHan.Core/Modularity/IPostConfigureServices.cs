#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IPostConfigureServices
// Guid:0e7c466c-04f1-4d7a-b05c-7855cba64ce1
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 上午 10:38:57
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.Modularity;

/// <summary>
/// 服务配置后接口
/// </summary>
public interface IPostConfigureServices
{
    /// <summary>
    /// 服务配置后，异步
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task PostConfigureServicesAsync(ServiceConfigurationContext context);

    /// <summary>
    /// 服务配置后
    /// </summary>
    /// <param name="context"></param>
    void PostConfigureServices(ServiceConfigurationContext context);
}