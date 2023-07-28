#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysJobsLogService
// Guid:d8291498-8a92-40a9-a37b-4dd187725363
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 1:14:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Jobs.Dtos;

namespace XiHan.Services.Syses.Jobs;

/// <summary>
/// ISysJobsLogService
/// </summary>
public interface ISysJobsLogService : IBaseService<SysJobsLog>
{
    /// <summary>
    /// 新增任务日志
    /// </summary>
    /// <param name="jobsLog"></param>
    /// <returns></returns>
    Task<SysJobsLog> CreateJobsLog(SysJobsLog jobsLog);

    /// <summary>
    /// 批量删除操作日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    Task<bool> DeleteOperationLogByIds(long[] logIds);

    /// <summary>
    /// 清空操作日志
    /// </summary>
    Task<bool> ClearOperationLog();

    /// <summary>
    /// 查询系统任务日志(根据任务Id)
    /// </summary>
    /// <param name="jobsId"></param>
    /// <returns></returns>
    Task<SysJobsLog> GetJobsLogByJobId(long jobsId);

    /// <summary>
    /// 查询系统任务日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    Task<List<SysJobsLog>> GetJobsLogList(SysJobsLogWDto whereDto);

    /// <summary>
    /// 查询系统任务日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysJobsLog>> GetJobsLogPageList(PageWhereDto<SysJobsLogWDto> pageWhere);
}