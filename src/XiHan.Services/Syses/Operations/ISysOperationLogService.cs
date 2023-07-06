#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysOperationLogService
// Guid:cc608975-d552-481f-a2e6-070d3bd75b2a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-21 下午 05:42:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Operations.Dtos;

namespace XiHan.Services.Syses.Operations;

/// <summary>
/// ISysOperationLogService
/// </summary>
public interface ISysOperationLogService : IBaseService<SysOperationLog>
{
    /// <summary>
    /// 新增操作日志
    /// </summary>
    /// <param name="sysOperationLog"></param>
    /// <returns></returns>
    Task CreateOperationLog(SysOperationLog sysOperationLog);

    /// <summary>
    /// 批量删除操作日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    Task<bool> DeleteOperationLogByIds(long[] logIds);

    /// <summary>
    /// 清空操作日志
    /// </summary>
    Task<bool> CleanOperationLog();

    /// <summary>
    /// 查询操作日志(根据Id)
    /// </summary>
    /// <param name="logId"></param>
    /// <returns></returns>
    Task<SysOperationLog> GetOperationLogById(long logId);

    /// <summary>
    /// 查询操作日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysOperationLog>> GetOperationLogBList(PageWhereDto<SysOperationLogWhereDto> pageWhere);
}