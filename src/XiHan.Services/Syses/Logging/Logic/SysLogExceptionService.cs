#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLogExceptionService
// Guid:65179156-6084-40b4-ace3-0cda5ee49eeb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 1:16:48
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
/// 系统异常日志服务
/// </summary>
[AppService(ServiceType = typeof(ISysLogExceptionService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysLogExceptionService : BaseService<SysLogException>, ISysLogExceptionService
{
    private readonly IAppCacheService _appCacheService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="appCacheService"></param>
    public SysLogExceptionService(IAppCacheService appCacheService)
    {
        _appCacheService = appCacheService;
    }

    /// <summary>
    /// 新增系统异常日志
    /// </summary>
    /// <param name="logException"></param>
    /// <returns></returns>
    public async Task<bool> CreateLogException(SysLogException logException)
    {
        return await AddAsync(logException);
    }

    /// <summary>
    /// 批量删除系统异常日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteLogExceptionByIds(long[] logIds)
    {
        return await RemoveAsync(logIds);
    }

    /// <summary>
    /// 清空系统异常日志
    /// </summary>
    /// <returns></returns>
    public async Task<bool> ClearLogException()
    {
        return await ClearAsync();
    }

    /// <summary>
    /// 查询系统系统异常日志(根据异常Id)
    /// </summary>
    /// <param name="exceptionId"></param>
    /// <returns></returns>
    public async Task<SysLogException> GetLogExceptionById(long exceptionId)
    {
        var key = $"GetLogExceptionById_{exceptionId}";
        if (_appCacheService.Get(key) is SysLogException sysLogException) return sysLogException;
        sysLogException = await FindAsync(d => d.BaseId == exceptionId);
        _appCacheService.SetWithMinutes(key, sysLogException, 30);

        return sysLogException;
    }

    /// <summary>
    /// 查询系统系统异常日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysLogException>> GetLogExceptionList(SysLogExceptionWDto whereDto)
    {
        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        var whereExpression = Expressionable.Create<SysLogException>();
        whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        whereExpression.AndIF(whereDto.LogLevel.IsNotEmptyOrNull(), u => u.LogLevel == whereDto.LogLevel!);

        return await QueryAsync(whereExpression.ToExpression(), o => o.CreatedTime, false);
    }

    /// <summary>
    /// 查询系统系统异常日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysLogException>> GetLogExceptionPageList(PageWhereDto<SysLogExceptionWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        var whereExpression = Expressionable.Create<SysLogException>();
        whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        whereExpression.AndIF(whereDto.LogLevel.IsNotEmptyOrNull(), u => u.LogLevel == whereDto.LogLevel!);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime, false);
    }
}