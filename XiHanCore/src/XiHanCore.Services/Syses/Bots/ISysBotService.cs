#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysBotService
// Guid:eafc9979-24a0-4a48-9e7a-34dd173e7e47
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/9/3 0:40:09
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHanCore.Infrastructures.Responses.Pages;
using XiHanCore.Models.Syses;
using XiHanCore.Services.Bases;
using XiHanCore.Services.Syses.Bots.Dtos;
using XiHanCore.Utils.Extensions;

namespace XiHanCore.Services.Syses.Bots;

/// <summary>
/// ISysBotService
/// </summary>
public interface ISysBotService : IBaseService<SysBot>
{
    /// <summary>
    /// 新增系统机器人
    /// </summary>
    /// <param name="botCDto"></param>
    /// <returns></returns>
    Task<long> CreateSysBot(SysBotCDto botCDto);

    /// <summary>
    /// 批量删除系统机器人
    /// </summary>
    /// <param name="botIds"></param>
    /// <returns></returns>
    Task<bool> DeleteSysBotByIds(long[] botIds);

    /// <summary>
    /// 修改系统机器人
    /// </summary>
    /// <param name="botMDto"></param>
    /// <returns></returns>
    Task<bool> ModifySysBot(SysBotMDto botMDto);

    /// <summary>
    /// 查询系统机器人(根据Id)
    /// </summary>
    /// <param name="botId"></param>
    /// <returns></returns>
    Task<SysBot> GetSysBotById(long botId);

    /// <summary>
    /// 查询系统机器人分类列表
    /// </summary>
    /// <returns></returns>
    Task<List<EnumDescDto>> GetSysBotTypeList();

    /// <summary>
    /// 查询系统机器人列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    Task<List<SysBot>> GetSysBotList(SysBotWDto whereDto);

    /// <summary>
    /// 查询系统机器人列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysBot>> GetSysBotPageList(PageWhereDto<SysBotWDto> pageWhere);
}