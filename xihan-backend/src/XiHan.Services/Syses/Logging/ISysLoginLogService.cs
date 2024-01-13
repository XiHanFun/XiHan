#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysLoginLogService
// Guid:b76c9bed-1830-43d7-9775-c56203578b8e
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-19 下午 02:54:42
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Logging.Dtos;

namespace XiHan.Services.Syses.Logging;

/// <summary>
/// ISysLoginLogService
/// </summary>
public interface ISysLoginLogService : IBaseService<SysLoginLog>
{
    /// <summary>
    /// 新增系统登陆日志
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    Task CreateLoginLog(SysLoginLog log);

    /// <summary>
    /// 批量删除系统登陆日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<bool> DeleteLoginLogByIds(long[] ids);

    /// <summary>
    /// 清空系统登陆日志
    /// </summary>
    /// <returns></returns>
    Task<bool> CleanLoginLog();

    /// <summary>
    /// 查询系统登陆日志(根据Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<SysLoginLog> GetLoginLogById(long id);

    /// <summary>
    /// 查询系统登陆日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    Task<List<SysLoginLog>> GetLoginLogList(SysLoginLogWDto whereDto);

    /// <summary>
    /// 查询系统登陆日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysLoginLog>> GetLoginLogPageList(PageWhereDto<SysLoginLogWDto> pageWhere);
}