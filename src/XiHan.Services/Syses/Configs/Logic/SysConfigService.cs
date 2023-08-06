#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysConfigService
// Guid:a6456028-97a0-415c-9c00-1985bb3e9f3a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/5 3:10:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using XiHan.Infrastructures.Apps.Caches;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Configs.Dtos;

namespace XiHan.Services.Syses.Configs.Logic;

/// <summary>
/// 系统配置服务
/// </summary>
[AppService(ServiceType = typeof(ISysConfigService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysConfigService : BaseService<SysConfig>, ISysConfigService
{
    private readonly IAppCacheService _appCacheService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="appCacheService"></param>
    public SysConfigService(IAppCacheService appCacheService)
    {
        _appCacheService = appCacheService;
    }

    public async Task<long> CeateSysConfig(SysConfigCDto configCDto)
    {
        var sysConfig = configCDto.Adapt<SysConfig>();

        return await AddReturnIdAsync(sysConfig);
    }
}