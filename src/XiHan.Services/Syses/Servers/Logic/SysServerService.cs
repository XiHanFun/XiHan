#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysServerService
// Guid:7efb1829-56cb-4c55-b1f9-839b3081bb85
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-20 上午 10:58:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Infos;
using XiHan.Services.Syses.Servers.Dtos;

namespace XiHan.Services.Syses.Servers.Logic;

/// <summary>
/// 系统服务器监控服务
/// </summary>
[AppService(ServiceType = typeof(ISysServerService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysServerService : ISysServerService
{
    /// <summary>
    /// 获取服务器信息
    /// </summary>
    /// <returns></returns>
    public ServerInfoRDto GetServerInfo()
    {
        return new ServerInfoRDto();
    }

    /// <summary>
    /// 获取系统信息
    /// </summary>
    /// <returns></returns>
    public SystemInfoHelper GetSystemInfo()
    {
        return new SystemInfoHelper();
    }

    /// <summary>
    /// 获取环境信息
    /// </summary>
    /// <returns></returns>
    public EnvironmentInfoHelper GetEnvironmentInfo()
    {
        return new EnvironmentInfoHelper();
    }

    /// <summary>
    /// 获取应用信息
    /// </summary>
    /// <returns></returns>
    public ApplicationInfoHelper GetApplicationInfo()
    {
        return new ApplicationInfoHelper();
    }
}