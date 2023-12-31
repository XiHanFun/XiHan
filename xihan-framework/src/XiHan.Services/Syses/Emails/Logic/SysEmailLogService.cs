#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysEmailLogService
// Guid:65179156-6084-40b4-ace3-0cda5ee49eeb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 1:16:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Emails.Dtos;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Emails.Logic;

/// <summary>
/// 系统邮件日志服务
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
[AppService(ServiceType = typeof(ISysEmailLogService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysEmailLogService() : BaseService<SysEmailLog>, ISysEmailLogService
{
    /// <summary>
    /// 新增邮件日志
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    public async Task<bool> CreateEmailLog(SysEmailLog log)
    {
        return await AddAsync(log);
    }

    /// <summary>
    /// 批量删除邮件日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteEmailLogByIds(long[] ids)
    {
        return await RemoveAsync(ids);
    }

    /// <summary>
    /// 清空邮件日志
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CleanEmailLog()
    {
        return await CleanAsync();
    }

    /// <summary>
    /// 查询系统邮件日志(根据Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<SysEmailLog> GetEmailLogById(long id)
    {
        return await FindAsync(d => d.BaseId == id);
    }

    /// <summary>
    /// 查询系统邮件日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysEmailLog>> GetEmailLogList(SysEmailLogWDto whereDto)
    {
        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        Expressionable<SysEmailLog> whereExpression = Expressionable.Create<SysEmailLog>();
        _ = whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        _ = whereExpression.AndIF(whereDto.IsSend != null, u => u.IsSend == whereDto.IsSend);
        _ = whereExpression.AndIF(whereDto.IsSuccess != null, u => u.IsSuccess == whereDto.IsSuccess);

        return await QueryAsync(whereExpression.ToExpression(), o => o.CreatedTime, false);
    }

    /// <summary>
    /// 查询系统邮件日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysEmailLog>> GetEmailLogPageList(PageWhereDto<SysEmailLogWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        Expressionable<SysEmailLog> whereExpression = Expressionable.Create<SysEmailLog>();
        _ = whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        _ = whereExpression.AndIF(whereDto.IsSend != null, u => u.IsSend == whereDto.IsSend);
        _ = whereExpression.AndIF(whereDto.IsSuccess != null, u => u.IsSuccess == whereDto.IsSuccess);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime,
            pageWhere.IsAsc);
    }
}