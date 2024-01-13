#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysExceptionLogService
// Guid:d8291498-8a92-40a9-a37b-4dd187725363
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 1:14:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHanCore.Infrastructures.Responses.Pages;
using XiHanCore.Models.Syses;
using XiHanCore.Services.Bases;
using XiHanCore.Services.Syses.Logging.Dtos;

namespace XiHanCore.Services.Syses.Logging;

/// <summary>
/// ISysExceptionLogService
/// </summary>
public interface ISysExceptionLogService : IBaseService<SysExceptionLog>
{
    /// <summary>
    /// 新增系统异常日志
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    Task<bool> CreateExceptionLog(SysExceptionLog log);

    /// <summary>
    /// 批量删除系统异常日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<bool> DeleteExceptionLogByIds(long[] ids);

    /// <summary>
    /// 清空系统异常日志
    /// </summary>
    /// <returns></returns>
    Task<bool> CleanExceptionLog();

    /// <summary>
    /// 查询系统异常日志(根据异常Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<SysExceptionLog> GetExceptionLogById(long id);

    /// <summary>
    /// 查询系统异常日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    Task<List<SysExceptionLog>> GetExceptionLogList(SysExceptionLogWDto whereDto);

    /// <summary>
    /// 查询系统异常日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysExceptionLog>> GetExceptionLogPageList(PageWhereDto<SysExceptionLogWDto> pageWhere);
}