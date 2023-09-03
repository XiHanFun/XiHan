#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysBotService
// Guid:c6b130b6-6e3a-4b38-bfba-f606efbc80d0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/9/3 0:40:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using SqlSugar;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Models.Syses.Enums;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Bots.Dtos;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Bots.Logic;

/// <summary>
/// 系统机器人服务
/// </summary>
[AppService(ServiceType = typeof(ISysBotService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysBotService : BaseService<SysBot>, ISysBotService
{
    /// <summary>
    /// 校验机器人项是否唯一
    /// </summary>
    /// <param name="sysBot"></param>
    /// <returns></returns>
    private async Task<bool> CheckBotUnique(SysBot sysBot)
    {
        bool isUnique = await IsAnyAsync(f => f.Title == sysBot.Title || f.BotType == sysBot.BotType);
        return isUnique ? throw new CustomException($"机器人类别为【{sysBot.BotType.GetEnumDescriptionByKey()}】的【{sysBot.Title}】已存在!") : isUnique;
    }

    /// <summary>
    /// 新增系统机器人
    /// </summary>
    /// <param name="botCDto"></param>
    /// <returns></returns>
    public async Task<long> CreateSysBot(SysBotCDto botCDto)
    {
        SysBot sysBot = botCDto.Adapt<SysBot>();

        _ = await CheckBotUnique(sysBot);

        return await AddReturnIdAsync(sysBot);
    }

    /// <summary>
    /// 批量删除系统机器人
    /// </summary>
    /// <param name="botIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteSysBotByIds(long[] botIds)
    {
        List<SysBot> sysBotList = await QueryAsync(d => botIds.Contains(d.BaseId));

        // 禁止删除正在使用的配置
        List<SysBot> deleteList = sysBotList.Where(c => !c.IsEnabled).ToList();
        return await RemoveAsync(deleteList);
    }

    /// <summary>
    /// 修改系统机器人
    /// </summary>
    /// <param name="botMDto"></param>
    /// <returns></returns>
    public async Task<bool> ModifySysBot(SysBotMDto botMDto)
    {
        SysBot sysBot = botMDto.Adapt<SysBot>();

        // 禁止修改系统参数
        SysBot oldSysBot = await FindAsync(c => c.BaseId == sysBot.BaseId);

        _ = await CheckBotUnique(sysBot);
        return await UpdateAsync(sysBot);
    }

    /// <summary>
    /// 查询系统机器人(根据Id)
    /// </summary>
    /// <param name="botId"></param>
    /// <returns></returns>
    public async Task<SysBot> GetSysBotById(long botId)
    {
        SysBot sysBot = await FindAsync(d => d.BaseId == botId);
        return sysBot;
    }

    /// <summary>
    /// 查询系统机器人分类列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<EnumDescDto>> GetSysBotTypeList()
    {
        List<EnumDescDto> dtos = typeof(BotTypeEnum).GetEnumInfos().ToList();
        return await Task.FromResult(dtos);
    }

    /// <summary>
    /// 查询系统机器人列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysBot>> GetSysBotList(SysBotWDto whereDto)
    {
        Expressionable<SysBot> whereExpression = Expressionable.Create<SysBot>();
        _ = whereExpression.AndIF(whereDto.BotType != null, u => u.BotType == whereDto.BotType);
        _ = whereExpression.AndIF(whereDto.Title.IsNotEmptyOrNull(), u => u.Title.Contains(whereDto.Title!));
        _ = whereExpression.AndIF(whereDto.IsEnabled != null, u => u.IsEnabled == whereDto.IsEnabled);

        return await QueryAsync(whereExpression.ToExpression(), o => o.Title, false);
    }

    /// <summary>
    /// 查询系统机器人列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysBot>> GetSysBotPageList(PageWhereDto<SysBotWDto> pageWhere)
    {
        SysBotWDto whereDto = pageWhere.Where;

        Expressionable<SysBot> whereExpression = Expressionable.Create<SysBot>();
        _ = whereExpression.AndIF(whereDto.BotType != null, u => u.BotType == whereDto.BotType);
        _ = whereExpression.AndIF(whereDto.Title.IsNotEmptyOrNull(), u => u.Title.Contains(whereDto.Title!));
        _ = whereExpression.AndIF(whereDto.IsEnabled != null, u => u.IsEnabled == whereDto.IsEnabled);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.Title, pageWhere.IsAsc);
    }
}