﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysEmailLogService
// Guid:d8291498-8a92-40a9-a37b-4dd187725363
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 1:14:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Emails.Dtos;

namespace XiHan.Services.Syses.Emails;

/// <summary>
/// ISysEmailLogService
/// </summary>
public interface ISysEmailLogService : IBaseService<SysEmailLog>
{
    /// <summary>
    /// 新增邮件日志
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    Task<bool> CreateEmailLog(SysEmailLog log);

    /// <summary>
    /// 批量删除邮件日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<bool> DeleteEmailLogByIds(long[] ids);

    /// <summary>
    /// 清空邮件日志
    /// </summary>
    /// <returns></returns>
    Task<bool> CleanEmailLog();

    /// <summary>
    /// 查询系统邮件日志(根据Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<SysEmailLog> GetEmailLogById(long id);

    /// <summary>
    /// 查询系统邮件日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    Task<List<SysEmailLog>> GetEmailLogList(SysEmailLogWDto whereDto);

    /// <summary>
    /// 查询系统邮件日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysEmailLog>> GetEmailLogPageList(PageWhereDto<SysEmailLogWDto> pageWhere);
}