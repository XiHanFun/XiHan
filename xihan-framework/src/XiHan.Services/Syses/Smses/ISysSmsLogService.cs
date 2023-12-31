#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysSmsLogService
// Guid:d8291498-8a92-40a9-a37b-4dd187725363
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 1:14:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Smses.Dtos;

namespace XiHan.Services.Syses.Smses;

/// <summary>
/// ISysSmsLogService
/// </summary>
public interface ISysSmsLogService : IBaseService<SysSmsLog>
{
    /// <summary>
    /// 新增短信日志
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    Task<bool> CreateSmsLog(SysSmsLog log);

    /// <summary>
    /// 批量删除短信日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<bool> DeleteSmsLogByIds(long[] ids);

    /// <summary>
    /// 清空短信日志
    /// </summary>
    /// <returns></returns>
    Task<bool> CleanSmsLog();

    /// <summary>
    /// 查询系统短信日志(根据Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<SysSmsLog> GetSmsLogById(long id);

    /// <summary>
    /// 查询系统短信日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    Task<List<SysSmsLog>> GetSmsLogList(SysSmsLogWDto whereDto);

    /// <summary>
    /// 查询系统短信日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysSmsLog>> GetSmsLogPageList(PageWhereDto<SysSmsLogWDto> pageWhere);
}