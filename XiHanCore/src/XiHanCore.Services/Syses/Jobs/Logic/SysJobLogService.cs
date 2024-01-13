#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysJobLogService
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
using XiHanCore.Services.Syses.Jobs.Dtos;
using XiHanCore.Utils.Extensions;

namespace XiHanCore.Services.Syses.Jobs.Logic;

/// <summary>
/// 系统任务日志服务
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="sysJobService"></param>
[AppService(ServiceType = typeof(ISysJobLogService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysJobLogService(ISysJobService sysJobService) : BaseService<SysJobLog>, ISysJobLogService
{
    private readonly ISysJobService _sysJobService = sysJobService;

    /// <summary>
    /// 新增任务日志
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    public async Task<SysJobLog> CreateJobLog(SysJobLog log)
    {
        //获取任务信息
        var sysJob = await _sysJobService.GetSingleAsync(j => j.BaseId == log.JobId);
        if (sysJob != null)
        {
            log.JobName = sysJob.Name;
            log.JobGroup = sysJob.Group;
        }

        _ = await AddAsync(log);
        return log;
    }

    /// <summary>
    /// 批量删除任务日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteJobLogByIds(long[] ids)
    {
        return await RemoveAsync(ids);
    }

    /// <summary>
    /// 清空任务日志
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CleanJobLog()
    {
        return await CleanAsync();
    }

    /// <summary>
    /// 查询系统任务日志(根据任务Id)
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    public async Task<SysJobLog> GetJobLogByJobId(long jobId)
    {
        return await FindAsync(d => d.JobId == jobId);
    }

    /// <summary>
    /// 查询系统任务日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysJobLog>> GetJobLogList(SysJobLogWDto whereDto)
    {
        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        Expressionable<SysJobLog> whereExpression = Expressionable.Create<SysJobLog>();
        _ = whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        _ = whereExpression.AndIF(whereDto.JobName.IsNotEmptyOrNull(), u => u.JobName.Contains(whereDto.JobName!));
        _ = whereExpression.AndIF(whereDto.JobGroup.IsNotEmptyOrNull(), u => u.JobGroup.Contains(whereDto.JobGroup!));
        _ = whereExpression.AndIF(whereDto.IsSuccess != null, u => u.IsSuccess == whereDto.IsSuccess);

        return await QueryAsync(whereExpression.ToExpression(), o => o.CreatedTime, false);
    }

    /// <summary>
    /// 查询系统任务日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysJobLog>> GetJobLogPageList(PageWhereDto<SysJobLogWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        Expressionable<SysJobLog> whereExpression = Expressionable.Create<SysJobLog>();
        _ = whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        _ = whereExpression.AndIF(whereDto.JobName.IsNotEmptyOrNull(), u => u.JobName.Contains(whereDto.JobName!));
        _ = whereExpression.AndIF(whereDto.JobGroup.IsNotEmptyOrNull(), u => u.JobGroup.Contains(whereDto.JobGroup!));
        _ = whereExpression.AndIF(whereDto.IsSuccess != null, u => u.IsSuccess == whereDto.IsSuccess);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime,
            pageWhere.IsAsc);
    }
}