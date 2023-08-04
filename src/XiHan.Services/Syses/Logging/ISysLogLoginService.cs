#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysLogLoginService
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
/// ISysLogLoginService
/// </summary>
public interface ISysLogLoginService : IBaseService<SysLogLogin>
{
    /// <summary>
    /// 新增登录日志
    /// </summary>
    /// <param name="loginLog"></param>
    /// <returns></returns>
    Task CreateLogLogin(SysLogLogin loginLog);

    /// <summary>
    /// 批量删除登录日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    Task<bool> DeleteLogLoginByIds(long[] logIds);

    /// <summary>
    /// 清空登录日志
    /// </summary>
    /// <returns></returns>
    Task<bool> CleanLogLogin();

    /// <summary>
    /// 查询登录日志(根据Id)
    /// </summary>
    /// <param name="logId"></param>
    /// <returns></returns>
    Task<SysLogLogin> GetLogLoginById(long logId);

    /// <summary>
    /// 查询登录日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysLogLogin>> GetLogLoginBList(PageWhereDto<SysLogLoginWDto> pageWhere);
}