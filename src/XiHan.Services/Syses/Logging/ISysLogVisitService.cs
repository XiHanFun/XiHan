#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysLogVisitService
// Guid:25be1572-c261-4a15-a74b-e1891ac15f88
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/21 1:57:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Syses.Logging.Dtos;

namespace XiHan.Services.Syses.Logging;

/// <summary>
/// ISysLogVisitService
/// </summary>
public interface ISysLogVisitService
{
    /// <summary>
    /// 新增系统访问日志
    /// </summary>
    /// <param name="logVisit"></param>
    /// <returns></returns>
    Task<bool> CreateLogVisit(SysLogVisit logVisit);

    /// <summary>
    /// 批量删除系统访问日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    Task<bool> DeleteLogVisitByIds(long[] logIds);

    /// <summary>
    /// 清空系统访问日志
    /// </summary>
    /// <returns></returns>
    Task<bool> ClearLogVisit();

    /// <summary>
    /// 查询系统系统访问日志(根据访问Id)
    /// </summary>
    /// <param name="visitId"></param>
    /// <returns></returns>
    Task<SysLogVisit> GetLogVisitById(long visitId);

    /// <summary>
    /// 查询系统系统访问日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    Task<List<SysLogVisit>> GetLogVisitList(SysLogVisitWDto whereDto);

    /// <summary>
    /// 查询系统系统访问日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysLogVisit>> GetLogVisitPageList(PageWhereDto<SysLogVisitWDto> pageWhere);
}