#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysLogJobService
// Guid:d8291498-8a92-40a9-a37b-4dd187725363
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 1:14:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Logging.Dtos;

namespace XiHan.Services.Syses.Logging;

/// <summary>
/// ISysLogJobService
/// </summary>
public interface ISysLogJobService : IBaseService<SysLogJob>
{
    /// <summary>
    /// 新增任务日志
    /// </summary>
    /// <param name="logJob"></param>
    /// <returns></returns>
    Task<SysLogJob> CreateLogJob(SysLogJob logJob);

    /// <summary>
    /// 批量删除任务日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    Task<bool> DeleteLogJobByIds(long[] logIds);

    /// <summary>
    /// 清空任务日志
    /// </summary>
    /// <returns></returns>
    Task<bool> ClearLogJob();

    /// <summary>
    /// 查询系统任务日志(根据任务Id)
    /// </summary>
    /// <param name="jobsId"></param>
    /// <returns></returns>
    Task<SysLogJob> GetLogJobByJobId(long jobsId);

    /// <summary>
    /// 查询系统任务日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    Task<List<SysLogJob>> GetLogJobList(SysLogJobWDto whereDto);

    /// <summary>
    /// 查询系统任务日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysLogJob>> GetLogJobPageList(PageWhereDto<SysLogJobWDto> pageWhere);
}