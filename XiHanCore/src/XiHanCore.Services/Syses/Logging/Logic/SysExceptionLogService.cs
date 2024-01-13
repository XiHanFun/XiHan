#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysExceptionLogService
// Guid:65179156-6084-40b4-ace3-0cda5ee49eeb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 1:16:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHanCore.Infrastructures.Apps.Services;
using XiHanCore.Infrastructures.Responses.Pages;
using XiHanCore.Models.Syses;
using XiHanCore.Services.Bases;
using XiHanCore.Services.Syses.Logging.Dtos;
using XiHanCore.Utils.Extensions;

namespace XiHanCore.Services.Syses.Logging.Logic;

/// <summary>
/// 系统异常日志服务
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
[AppService(ServiceType = typeof(ISysExceptionLogService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysExceptionLogService() : BaseService<SysExceptionLog>, ISysExceptionLogService
{
    /// <summary>
    /// 新增系统异常日志
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    public async Task<bool> CreateExceptionLog(SysExceptionLog log)
    {
        return await AddAsync(log);
    }

    /// <summary>
    /// 批量删除系统异常日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteExceptionLogByIds(long[] ids)
    {
        return await RemoveAsync(ids);
    }

    /// <summary>
    /// 清空系统异常日志
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CleanExceptionLog()
    {
        return await CleanAsync();
    }

    /// <summary>
    /// 查询系统异常日志(根据Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<SysExceptionLog> GetExceptionLogById(long id)
    {
        return await FindAsync(d => d.BaseId == id);
    }

    /// <summary>
    /// 查询系统异常日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysExceptionLog>> GetExceptionLogList(SysExceptionLogWDto whereDto)
    {
        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        Expressionable<SysExceptionLog> whereExpression = Expressionable.Create<SysExceptionLog>();
        _ = whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        _ = whereExpression.AndIF(whereDto.Level.IsNotEmptyOrNull(), u => u.Level == whereDto.Level!);

        return await QueryAsync(whereExpression.ToExpression(), o => o.CreatedTime, false);
    }

    /// <summary>
    /// 查询系统异常日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysExceptionLog>> GetExceptionLogPageList(PageWhereDto<SysExceptionLogWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        Expressionable<SysExceptionLog> whereExpression = Expressionable.Create<SysExceptionLog>();
        _ = whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        _ = whereExpression.AndIF(whereDto.Level.IsNotEmptyOrNull(), u => u.Level == whereDto.Level!);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime,
            pageWhere.IsAsc);
    }
}