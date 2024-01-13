#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysVisitLogService
// Guid:25be1572-c261-4a15-a74b-e1891ac15f88
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/21 1:57:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHanCore.Infrastructures.Responses.Pages;
using XiHanCore.Models.Syses;
using XiHanCore.Services.Syses.Logging.Dtos;

namespace XiHanCore.Services.Syses.Logging;

/// <summary>
/// ISysVisitLogService
/// </summary>
public interface ISysVisitLogService
{
    /// <summary>
    /// 新增系统访问日志
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    Task<bool> CreateVisitLog(SysVisitLog log);

    /// <summary>
    /// 批量删除系统访问日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<bool> DeleteVisitLogByIds(long[] ids);

    /// <summary>
    /// 清空系统访问日志
    /// </summary>
    /// <returns></returns>
    Task<bool> CleanVisitLog();

    /// <summary>
    /// 查询系统系统访问日志(根据访问Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<SysVisitLog> GetVisitLogById(long id);

    /// <summary>
    /// 查询系统系统访问日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    Task<List<SysVisitLog>> GetVisitLogList(SysVisitLogWDto whereDto);

    /// <summary>
    /// 查询系统系统访问日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysVisitLog>> GetVisitLogPageList(PageWhereDto<SysVisitLogWDto> pageWhere);
}