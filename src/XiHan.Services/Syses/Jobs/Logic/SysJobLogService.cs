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
using XiHan.Infrastructures.Apps.Caches;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Jobs.Dtos;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Jobs.Logic;

/// <summary>
/// 系统任务日志服务
/// </summary>
[AppService(ServiceType = typeof(ISysJobLogService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysJobLogService : BaseService<SysJobLog>, ISysJobLogService
{
    private readonly IAppCacheService _appCacheService;
    private readonly ISysJobService _sysJobService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="appCacheService"></param>
    /// <param name="sysJobService"></param>
    public SysJobLogService(IAppCacheService appCacheService, ISysJobService sysJobService)
    {
        _appCacheService = appCacheService;
        _sysJobService = sysJobService;
    }

    /// <summary>
    /// 新增任务日志
    /// </summary>
    /// <param name="jobLog"></param>
    /// <returns></returns>
    public async Task<SysJobLog> CreateJobLog(SysJobLog jobLog)
    {
        //获取任务信息
        var sysJob = await _sysJobService.GetSingleAsync(j => j.BaseId == jobLog.JobId);
        if (sysJob != null)
        {
            jobLog.JobName = sysJob.JobName;
            jobLog.JobGroup = sysJob.JobGroup;
        }
        _ = await AddAsync(jobLog);
        return jobLog;
    }

    /// <summary>
    /// 批量删除操作日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteOperationLogByIds(long[] logIds)
    {
        return await RemoveAsync(logIds);
    }

    /// <summary>
    /// 清空操作日志
    /// </summary>
    public async Task<bool> ClearOperationLog()
    {
        return await ClearAsync();
    }

    /// <summary>
    /// 查询系统任务日志(根据任务Id)
    /// </summary>
    /// <param name="jobsId"></param>
    /// <returns></returns>
    public async Task<SysJobLog> GetJobLogByJobId(long jobsId)
    {
        var key = $"GetJobLogByJobId_{jobsId}";
        if (_appCacheService.Get(key) is SysJobLog sysJobLog) return sysJobLog;
        sysJobLog = await FindAsync(d => d.JobId == jobsId);
        _appCacheService.SetWithMinutes(key, sysJobLog, 30);

        return sysJobLog;
    }

    /// <summary>
    /// 查询系统任务日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysJobLog>> GetJobLogList(SysJobLogWDto whereDto)
    {
        var whereExpression = Expressionable.Create<SysJobLog>();
        whereExpression.AndIF(whereDto.JobName.IsNotEmptyOrNull(), u => u.JobName.Contains(whereDto.JobName!));
        whereExpression.AndIF(whereDto.JobGroup.IsNotEmptyOrNull(), u => u.JobGroup.Contains(whereDto.JobGroup!));
        whereExpression.AndIF(whereDto.RunResult != null, u => u.RunResult == whereDto.RunResult);

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

        var whereExpression = Expressionable.Create<SysJobLog>();
        whereExpression.AndIF(whereDto.JobName.IsNotEmptyOrNull(), u => u.JobName.Contains(whereDto.JobName!));
        whereExpression.AndIF(whereDto.JobGroup.IsNotEmptyOrNull(), u => u.JobGroup.Contains(whereDto.JobGroup!));
        whereExpression.AndIF(whereDto.RunResult != null, u => u.RunResult == whereDto.RunResult);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime, false);
    }
}