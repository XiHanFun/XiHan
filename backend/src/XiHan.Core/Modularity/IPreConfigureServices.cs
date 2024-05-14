#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IPreConfigureServices
// Guid:c7e16e66-bb50-4ed9-996e-e804f2821738
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 上午 10:38:49
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.Modularity;

/// <summary>
/// 服务配置前接口
/// </summary>
public interface IPreConfigureServices
{
    /// <summary>
    /// 服务配置前，异步
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task PreConfigureServicesAsync(ServiceConfigurationContext context);

    /// <summary>
    /// 服务配置前
    /// </summary>
    /// <param name="context"></param>
    void PreConfigureServices(ServiceConfigurationContext context);
}