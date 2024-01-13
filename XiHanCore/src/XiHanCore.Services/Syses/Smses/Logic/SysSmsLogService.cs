#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysSmsLogService
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
using XiHanCore.Services.Syses.Smses.Dtos;
using XiHanCore.Utils.Extensions;

namespace XiHanCore.Services.Syses.Smses.Logic;

/// <summary>
/// 系统短信日志服务
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
[AppService(ServiceType = typeof(ISysSmsLogService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysSmsLogService() : BaseService<SysSmsLog>, ISysSmsLogService
{
    /// <summary>
    /// 新增短信日志
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    public async Task<bool> CreateSmsLog(SysSmsLog log)
    {
        return await AddAsync(log);
    }

    /// <summary>
    /// 批量删除短信日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteSmsLogByIds(long[] ids)
    {
        return await RemoveAsync(ids);
    }

    /// <summary>
    /// 清空短信日志
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CleanSmsLog()
    {
        return await CleanAsync();
    }

    /// <summary>
    /// 查询系统短信日志(根据Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<SysSmsLog> GetSmsLogById(long id)
    {
        return await FindAsync(d => d.BaseId == id);
    }

    /// <summary>
    /// 查询系统短信日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysSmsLog>> GetSmsLogList(SysSmsLogWDto whereDto)
    {
        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        Expressionable<SysSmsLog> whereExpression = Expressionable.Create<SysSmsLog>();
        _ = whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        _ = whereExpression.AndIF(whereDto.IsSend != null, u => u.IsSend == whereDto.IsSend);
        _ = whereExpression.AndIF(whereDto.IsSuccess != null, u => u.IsSuccess == whereDto.IsSuccess);

        return await QueryAsync(whereExpression.ToExpression(), o => o.CreatedTime, false);
    }

    /// <summary>
    /// 查询系统短信日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysSmsLog>> GetSmsLogPageList(PageWhereDto<SysSmsLogWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        Expressionable<SysSmsLog> whereExpression = Expressionable.Create<SysSmsLog>();
        _ = whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        _ = whereExpression.AndIF(whereDto.IsSend != null, u => u.IsSend == whereDto.IsSend);
        _ = whereExpression.AndIF(whereDto.IsSuccess != null, u => u.IsSuccess == whereDto.IsSuccess);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime,
            pageWhere.IsAsc);
    }
}