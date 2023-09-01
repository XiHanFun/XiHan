#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysJobService
// Guid:52fd0cf5-e077-4447-b808-bc02e504124d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 1:20:53
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using SqlSugar;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Jobs.Dtos;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Jobs.Logic;

/// <summary>
/// 系统任务服务
/// </summary>
[AppService(ServiceType = typeof(ISysJobService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysJobService : BaseService<SysJob>, ISysJobService
{
    /// <summary>
    /// 校验任务是否唯一
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    private async Task<bool> GetJobUnique(SysJob sysJob)
    {
        bool isUnique = await IsAnyAsync(j => j.Group == sysJob.Group && j.Name == sysJob.Name);
        return isUnique ? throw new CustomException($"任务【{sysJob.Group}-{sysJob.Name}】已存在！") : isUnique;
    }

    /// <summary>
    /// 新增系统任务
    /// </summary>
    /// <param name="jobCDto"></param>
    /// <returns></returns>
    public async Task<long> CreateJob(SysJobCDto jobCDto)
    {
        SysJob sysJob = jobCDto.Adapt<SysJob>();

        _ = await GetJobUnique(sysJob);

        return await AddReturnIdAsync(sysJob);
    }

    /// <summary>
    /// 批量删除系统任务
    /// </summary>
    /// <param name="jobIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteJobByIds(long[] jobIds)
    {
        List<SysJob> jobList = await QueryAsync(d => jobIds.Contains(d.BaseId));
        return await RemoveAsync(jobList);
    }

    /// <summary>
    /// 修改系统任务
    /// </summary>
    /// <param name="jobMDto"></param>
    /// <returns></returns>
    public async Task<bool> ModifyJob(SysJobMDto jobMDto)
    {
        SysJob newSysJob = jobMDto.Adapt<SysJob>();

        return await UpdateAsync(newSysJob);
    }

    /// <summary>
    /// 查询系统任务(根据Id)
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    public async Task<SysJob> GetJobById(long jobId)
    {
        SysJob sysJob = await FindAsync(d => d.BaseId == jobId);

        return sysJob;
    }

    /// <summary>
    /// 查询系统任务列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysJob>> GetJobList(SysJobWDto whereDto)
    {
        Expressionable<SysJob> whereExpression = Expressionable.Create<SysJob>();
        _ = whereExpression.AndIF(whereDto.Group.IsNotEmptyOrNull(), u => u.Group.Contains(whereDto.Group!));
        _ = whereExpression.AndIF(whereDto.Name.IsNotEmptyOrNull(), u => u.Name.Contains(whereDto.Name!));
        _ = whereExpression.AndIF(whereDto.JobType != null, u => u.JobType == whereDto.JobType);
        _ = whereExpression.AndIF(whereDto.TriggerType != null, u => u.TriggerType == whereDto.TriggerType);
        _ = whereExpression.AndIF(whereDto.IsStart != null, u => u.IsStart == whereDto.IsStart);

        return await QueryAsync(whereExpression.ToExpression(), o => new { o.IsStart, o.CreatedTime });
    }

    /// <summary>
    /// 查询系统任务列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysJob>> GetJobPageList(PageWhereDto<SysJobWDto> pageWhere)
    {
        SysJobWDto whereDto = pageWhere.Where;

        Expressionable<SysJob> whereExpression = Expressionable.Create<SysJob>();
        _ = whereExpression.AndIF(whereDto.Group.IsNotEmptyOrNull(), u => u.Group.Contains(whereDto.Group!));
        _ = whereExpression.AndIF(whereDto.Name.IsNotEmptyOrNull(), u => u.Name.Contains(whereDto.Name!));
        _ = whereExpression.AndIF(whereDto.JobType != null, u => u.JobType == whereDto.JobType);
        _ = whereExpression.AndIF(whereDto.TriggerType != null, u => u.TriggerType == whereDto.TriggerType);
        _ = whereExpression.AndIF(whereDto.IsStart != null, u => u.IsStart == whereDto.IsStart);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => new { o.IsStart, o.CreatedTime }, pageWhere.IsAsc);
    }
}