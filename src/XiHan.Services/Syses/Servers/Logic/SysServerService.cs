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
    public Dictionary<string, object?> GetServerInfo()
    {
        var result = new Dictionary<string, object?>();
        var systemInfoProperties = typeof(SystemInfoHelper).GetProperties().ToList();
        var applicationInfoProperties = typeof(ApplicationInfoHelper).GetProperties().ToList();

        systemInfoProperties.ForEach(property =>
        {
            result.Add(property.Name, property.GetValue(null));
        });
        applicationInfoProperties.ForEach(property =>
        {
            result.Add(property.Name, property.GetValue(null));
        });
        return result;
    }
}