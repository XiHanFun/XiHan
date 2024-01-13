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
using XiHan.Services.Syses.Logging.Dtos;

namespace XiHan.Services.Syses.Logging;

/// <summary>
/// ISysOperationLogService
/// </summary>
public interface ISysOperationLogService : IBaseService<SysOperationLog>
{
    /// <summary>
    /// 新增系统操作日志
    /// </summary>
    /// <param name="logOperation"></param>
    /// <returns></returns>
    Task CreateOperationLog(SysOperationLog logOperation);

    /// <summary>
    /// 批量删除系统操作日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<bool> DeleteOperationLogByIds(long[] ids);

    /// <summary>
    /// 清空系统操作日志
    /// </summary>
    /// <returns></returns>
    Task<bool> CleanOperationLog();

    /// <summary>
    /// 查询系统操作日志(根据Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<SysOperationLog> GetOperationLogById(long id);

    /// <summary>
    /// 查询系统操作日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    Task<List<SysOperationLog>> GetOperationLogList(SysOperationLogWDto whereDto);

    /// <summary>
    /// 查询系统操作日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysOperationLog>> GetOperationLogPageList(PageWhereDto<SysOperationLogWDto> pageWhere);
}