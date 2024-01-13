#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLoginLogService
// Guid:20f98d51-1827-4403-9021-b4fe7953a683
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-19 下午 02:55:04
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
/// 系统登陆日志服务
/// </summary>
[AppService(ServiceType = typeof(ISysLoginLogService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysLoginLogService : BaseService<SysLoginLog>, ISysLoginLogService
{
    /// <summary>
    /// 新增系统登陆日志
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    public async Task CreateLoginLog(SysLoginLog log)
    {
        _ = await AddAsync(log);
    }

    /// <summary>
    /// 批量删除系统登陆日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteLoginLogByIds(long[] ids)
    {
        return await RemoveAsync(ids);
    }

    /// <summary>
    /// 清空系统登陆日志
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CleanLoginLog()
    {
        return await CleanAsync();
    }

    /// <summary>
    /// 查询系统登陆日志(根据Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<SysLoginLog> GetLoginLogById(long id)
    {
        return await FindAsync(id);
    }

    /// <summary>
    /// 查询系统登陆日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysLoginLog>> GetLoginLogList(SysLoginLogWDto whereDto)
    {
        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        Expressionable<SysLoginLog> whereExpression = Expressionable.Create<SysLoginLog>();
        _ = whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        _ = whereExpression.AndIF(whereDto.Ip.IsNotEmptyOrNull(), l => l.Ip!.Contains(whereDto.Ip!));
        _ = whereExpression.AndIF(whereDto.Account.IsNotEmptyOrNull(), l => l.Account!.Contains(whereDto.Account!));
        _ = whereExpression.AndIF(whereDto.RealName.IsNotEmptyOrNull(), l => l.RealName!.Contains(whereDto.RealName!));
        _ = whereExpression.AndIF(whereDto.IsSuccess != null, l => l.IsSuccess == whereDto.IsSuccess);

        return await QueryAsync(whereExpression.ToExpression(), o => o.CreatedTime, false);
    }

    /// <summary>
    /// 查询系统登陆日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysLoginLog>> GetLoginLogPageList(PageWhereDto<SysLoginLogWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        Expressionable<SysLoginLog> whereExpression = Expressionable.Create<SysLoginLog>();
        _ = whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        _ = whereExpression.AndIF(whereDto.Ip.IsNotEmptyOrNull(), l => l.Ip!.Contains(whereDto.Ip!));
        _ = whereExpression.AndIF(whereDto.Account.IsNotEmptyOrNull(), l => l.Account!.Contains(whereDto.Account!));
        _ = whereExpression.AndIF(whereDto.RealName.IsNotEmptyOrNull(), l => l.RealName!.Contains(whereDto.RealName!));
        _ = whereExpression.AndIF(whereDto.IsSuccess != null, l => l.IsSuccess == whereDto.IsSuccess);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime,
            pageWhere.IsAsc);
    }
}