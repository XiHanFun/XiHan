#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysEmailService
// Guid:eafc9979-24a0-4a48-9e7a-34dd173e7e47
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/9/3 0:40:09
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Emails.Dtos;

namespace XiHan.Services.Syses.Emails;

/// <summary>
/// ISysEmailService
/// </summary>
public interface ISysEmailService : IBaseService<SysEmail>
{
    /// <summary>
    /// 新增系统机器人
    /// </summary>
    /// <param name="emailCDto"></param>
    /// <returns></returns>
    Task<long> CreateSysEmail(SysEmailCDto emailCDto);

    /// <summary>
    /// 批量删除系统机器人
    /// </summary>
    /// <param name="emailIds"></param>
    /// <returns></returns>
    Task<bool> DeleteSysEmailByIds(long[] emailIds);

    /// <summary>
    /// 修改系统机器人
    /// </summary>
    /// <param name="emailMDto"></param>
    /// <returns></returns>
    Task<bool> ModifySysEmail(SysEmailMDto emailMDto);

    /// <summary>
    /// 查询系统机器人(根据Id)
    /// </summary>
    /// <param name="emailId"></param>
    /// <returns></returns>
    Task<SysEmail> GetSysEmailById(long emailId);

    /// <summary>
    /// 查询系统机器人列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    Task<List<SysEmail>> GetSysEmailList(SysEmailWDto whereDto);

    /// <summary>
    /// 查询系统机器人列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysEmail>> GetSysEmailPageList(PageWhereDto<SysEmailWDto> pageWhere);
}