#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLogJobService
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
using XiHan.Services.Syses.Logging.Dtos;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Logging.Logic;

/// <summary>
/// 系统任务日志服务
/// </summary>
[AppService(ServiceType = typeof(ISysLogJobService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysLogJobService : BaseService<SysLogJob>, ISysLogJobService
{
    private readonly IAppCacheService _appCacheService;
    private readonly ISysJobService _sysJobService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="appCacheService"></param>
    /// <param name="sysJobService"></param>
    public SysLogJobService(IAppCacheService appCacheService, ISysJobService sysJobService)
    {
        _appCacheService = appCacheService;
        _sysJobService = sysJobService;
    }

    /// <summary>
    /// 新增任务日志
    /// </summary>
    /// <param name="logJob"></param>
    /// <returns></returns>
    public async Task<SysLogJob> CreateLogJob(SysLogJob logJob)
    {
        //获取任务信息
        var sysJob = await _sysJobService.GetSingleAsync(j => j.BaseId == logJob.JobId);
        if (sysJob != null)
        {
            logJob.JobName = sysJob.JobName;
            logJob.JobGroup = sysJob.JobGroup;
        }
        _ = await AddAsync(logJob);
        return logJob;
    }

    /// <summary>
    /// 批量删除任务日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteLogJobByIds(long[] logIds)
    {
        return await RemoveAsync(logIds);
    }

    /// <summary>
    /// 清空任务日志
    /// </summary>
    /// <returns></returns>
    public async Task<bool> ClearLogJob()
    {
        return await ClearAsync();
    }

    /// <summary>
    /// 查询系统任务日志(根据任务Id)
    /// </summary>
    /// <param name="jobsId"></param>
    /// <returns></returns>
    public async Task<SysLogJob> GetLogJobByJobId(long jobsId)
    {
        var key = $"GetLogJobByJobId_{jobsId}";
        if (_appCacheService.Get(key) is SysLogJob sysLogJob) return sysLogJob;
        sysLogJob = await FindAsync(d => d.JobId == jobsId);
        _appCacheService.SetWithMinutes(key, sysLogJob, 30);

        return sysLogJob;
    }

    /// <summary>
    /// 查询系统任务日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysLogJob>> GetLogJobList(SysLogJobWDto whereDto)
    {
        var whereExpression = Expressionable.Create<SysLogJob>();
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
    public async Task<PageDataDto<SysLogJob>> GetLogJobPageList(PageWhereDto<SysLogJobWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        var whereExpression = Expressionable.Create<SysLogJob>();
        whereExpression.AndIF(whereDto.JobName.IsNotEmptyOrNull(), u => u.JobName.Contains(whereDto.JobName!));
        whereExpression.AndIF(whereDto.JobGroup.IsNotEmptyOrNull(), u => u.JobGroup.Contains(whereDto.JobGroup!));
        whereExpression.AndIF(whereDto.RunResult != null, u => u.RunResult == whereDto.RunResult);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime, false);
    }
}