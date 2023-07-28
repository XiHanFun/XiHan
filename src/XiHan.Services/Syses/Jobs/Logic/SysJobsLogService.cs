#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysJobsLogService
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
using XiHan.Services.Syses.Jobs;
using XiHan.Services.Syses.Jobs.Dtos;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Jobs.Logic;

/// <summary>
/// 系统任务日志服务
/// </summary>
[AppService(ServiceType = typeof(ISysJobsLogService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysJobsLogService : BaseService<SysJobsLog>, ISysJobsLogService
{
    private readonly IAppCacheService _appCacheService;
    private readonly ISysJobsService _sysJobsService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="appCacheService"></param>
    /// <param name="sysJobsService"></param>
    public SysJobsLogService(IAppCacheService appCacheService, ISysJobsService sysJobsService)
    {
        _appCacheService = appCacheService;
        _sysJobsService = sysJobsService;
    }

    /// <summary>
    /// 新增任务日志
    /// </summary>
    /// <param name="jobsLog"></param>
    /// <returns></returns>
    public async Task<SysJobsLog> CreateJobsLog(SysJobsLog jobsLog)
    {
        //获取任务信息
        var sysJobs = await _sysJobsService.GetSingleAsync(j => j.BaseId == jobsLog.JobId);
        if (sysJobs != null)
        {
            jobsLog.JobName = sysJobs.JobName;
            jobsLog.JobGroup = sysJobs.JobGroup;
        }
        _ = await AddAsync(jobsLog);
        return jobsLog;
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
    public async Task<SysJobsLog> GetJobsLogByJobId(long jobsId)
    {
        var key = $"GetJobsLogByJobId_{jobsId}";
        if (_appCacheService.Get(key) is SysJobsLog sysJobsLog) return sysJobsLog;
        sysJobsLog = await FindAsync(d => d.JobId == jobsId);
        _appCacheService.SetWithMinutes(key, sysJobsLog, 30);

        return sysJobsLog;
    }

    /// <summary>
    /// 查询系统任务日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysJobsLog>> GetJobsLogList(SysJobsLogWDto whereDto)
    {
        var whereExpression = Expressionable.Create<SysJobsLog>();
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
    public async Task<PageDataDto<SysJobsLog>> GetJobsLogPageList(PageWhereDto<SysJobsLogWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        var whereExpression = Expressionable.Create<SysJobsLog>();
        whereExpression.AndIF(whereDto.JobName.IsNotEmptyOrNull(), u => u.JobName.Contains(whereDto.JobName!));
        whereExpression.AndIF(whereDto.JobGroup.IsNotEmptyOrNull(), u => u.JobGroup.Contains(whereDto.JobGroup!));
        whereExpression.AndIF(whereDto.RunResult != null, u => u.RunResult == whereDto.RunResult);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime, false);
    }
}