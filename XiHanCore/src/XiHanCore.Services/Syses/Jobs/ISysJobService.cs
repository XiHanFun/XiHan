#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysJobService
// Guid:89a6ccae-e128-4c93-baef-27b4b6f4615d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 1:20:46
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHanCore.Infrastructures.Responses.Pages;
using XiHanCore.Models.Syses;
using XiHanCore.Services.Bases;
using XiHanCore.Services.Syses.Jobs.Dtos;

namespace XiHanCore.Services.Syses.Jobs;

/// <summary>
/// ISysJobService
/// </summary>
public interface ISysJobService : IBaseService<SysJob>
{
    /// <summary>
    /// 新增系统任务
    /// </summary>
    /// <param name="jobCDto"></param>
    /// <returns></returns>
    Task<long> CreateJob(SysJobCDto jobCDto);

    /// <summary>
    /// 批量删除系统任务
    /// </summary>
    /// <param name="jobIds"></param>
    /// <returns></returns>
    Task<bool> DeleteJobByIds(long[] jobIds);

    /// <summary>
    /// 修改系统任务
    /// </summary>
    /// <param name="jobMDto"></param>
    /// <returns></returns>
    Task<bool> ModifyJob(SysJobMDto jobMDto);

    /// <summary>
    /// 查询系统任务(根据Id)
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    Task<SysJob> GetJobById(long jobId);

    /// <summary>
    /// 查询系统任务列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    Task<List<SysJob>> GetJobList(SysJobWDto whereDto);

    /// <summary>
    /// 查询系统任务列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysJob>> GetJobPageList(PageWhereDto<SysJobWDto> pageWhere);
}