#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLogVisitService
// Guid:cc745d02-3511-4de9-8da8-14888c33747e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/21 1:57:56
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Infrastructures.Apps.Caches;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Logging.Dtos;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Logging.Logic;

/// <summary>
/// 系统访问日志服务
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="appCacheService"></param>
[AppService(ServiceType = typeof(ISysLogVisitService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysLogVisitService(IAppCacheService appCacheService) : BaseService<SysLogVisit>, ISysLogVisitService
{
    private readonly IAppCacheService _appCacheService = appCacheService;

    /// <summary>
    /// 新增系统访问日志
    /// </summary>
    /// <param name="logVisit"></param>
    /// <returns></returns>
    public async Task<bool> CreateLogVisit(SysLogVisit logVisit)
    {
        return await AddAsync(logVisit);
    }

    /// <summary>
    /// 批量删除系统访问日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteLogVisitByIds(long[] logIds)
    {
        return await RemoveAsync(logIds);
    }

    /// <summary>
    /// 清空系统访问日志
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CleanLogVisit()
    {
        return await CleanAsync();
    }

    /// <summary>
    /// 查询系统系统访问日志(根据访问Id)
    /// </summary>
    /// <param name="visitId"></param>
    /// <returns></returns>
    public async Task<SysLogVisit> GetLogVisitById(long visitId)
    {
        var key = $"GetLogVisitById_{visitId}";
        if (_appCacheService.Get(key) is SysLogVisit sysLogVisit) return sysLogVisit;

        sysLogVisit = await FindAsync(d => d.BaseId == visitId);
        _ = _appCacheService.SetWithMinutes(key, sysLogVisit, 30);

        return sysLogVisit;
    }

    /// <summary>
    /// 查询系统系统访问日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysLogVisit>> GetLogVisitList(SysLogVisitWDto whereDto)
    {
        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        Expressionable<SysLogVisit> whereExpression = Expressionable.Create<SysLogVisit>();
        _ = whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);

        return await QueryAsync(whereExpression.ToExpression(), o => o.CreatedTime, false);
    }

    /// <summary>
    /// 查询系统系统访问日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysLogVisit>> GetLogVisitPageList(PageWhereDto<SysLogVisitWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        Expressionable<SysLogVisit> whereExpression = Expressionable.Create<SysLogVisit>();
        _ = whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime,
            pageWhere.IsAsc);
    }
}