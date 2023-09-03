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

using SqlSugar;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Bots.Logic;

/// <summary>
/// 系统机器人服务
/// </summary>
[AppService(ServiceType = typeof(ISysBotService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysBotService : BaseService<SysBot>, ISysBotService
{
    ///// <summary>
    ///// 校验机器人项是否唯一
    ///// </summary>
    ///// <param name="sysBot"></param>
    ///// <returns></returns>
    //private async Task<bool> CheckBotUnique(SysBot sysBot)
    //{
    //    bool isUnique = await IsAnyAsync(f => f.TypeCode == sysBot.TypeCode && (f.Code == sysBot.Code || f.Name == sysBot.Name));
    //    return isUnique ? throw new CustomException($"机器人类【{sysBot.TypeCode}】下已存在机器人项【{sysBot.Code}({sysBot.Name})】!") : isUnique;
    //}

    ///// <summary>
    ///// 新增系统机器人
    ///// </summary>
    ///// <param name="configCDto"></param>
    ///// <returns></returns>
    //public async Task<long> CreateSysBot(SysBotCDto configCDto)
    //{
    //    SysBot sysBot = configCDto.Adapt<SysBot>();

    //    _ = await CheckBotUnique(sysBot);

    //    return await AddReturnIdAsync(sysBot);
    //}

    ///// <summary>
    ///// 批量删除系统机器人
    ///// </summary>
    ///// <param name="configIds"></param>
    ///// <returns></returns>
    //public async Task<bool> DeleteSysBotByIds(long[] configIds)
    //{
    //    List<SysBot> sysBotList = await QueryAsync(d => configIds.Contains(d.BaseId));

    //    // 禁止删除系统参数
    //    List<SysBot> deleteList = sysBotList.Where(c => !c.IsOfficial).ToList();

    //    _appCacheService.Remove(deleteList.Select(c => c.Code));
    //    return await RemoveAsync(deleteList);
    //}

    ///// <summary>
    ///// 修改系统机器人
    ///// </summary>
    ///// <param name="configMDto"></param>
    ///// <returns></returns>
    //public async Task<bool> ModifySysBot(SysBotMDto configMDto)
    //{
    //    SysBot sysBot = configMDto.Adapt<SysBot>();

    //    // 禁止修改系统参数
    //    SysBot oldSysBot = await FindAsync(c => c.BaseId == sysBot.BaseId);
    //    if (oldSysBot.IsOfficial)
    //    {
    //        throw new CustomException("禁止修改系统内置参数！");
    //    }

    //    _ = await CheckBotUnique(sysBot);

    //    return await UpdateAsync(sysBot);
    //}

    ///// <summary>
    ///// 查询系统机器人(根据Id)
    ///// </summary>
    ///// <param name="configId"></param>
    ///// <returns></returns>
    //public async Task<SysBot> GetSysBotById(long configId)
    //{
    //    string key = $"GetSysBotById_{configId}";
    //    if (_appCacheService.Get(key) is SysBot sysBot)
    //    {
    //        return sysBot;
    //    }

    //    sysBot = await FindAsync(d => d.BaseId == configId);

    //    _ = _appCacheService.SetWithMinutes(key, sysBot, 30);
    //    return sysBot;
    //}

    ///// <summary>
    ///// 查询系统机器人值(根据Code)
    ///// </summary>
    ///// <param name="configCode"></param>
    ///// <returns></returns>
    //public async Task<T> GetSysBotValueByCode<T>(string configCode)
    //{
    //    string key = $"GetSysBotValueByCode_{configCode}";
    //    string? value = _appCacheService.Get<string>(key);
    //    if (value.IsNotEmptyOrNull())
    //    {
    //        return value.CastTo<T>()!;
    //    }

    //    SysBot sysBot = await FindAsync(d => d.Code == configCode);

    //    _ = _appCacheService.SetWithMinutes(key, sysBot.Value, 30);
    //    return sysBot.Value.CastTo<T>()!;
    //}

    ///// <summary>
    ///// 查询系统机器人分类列表
    ///// </summary>
    ///// <returns></returns>
    //public async Task<List<string>> GetSysBotTypeList()
    //{
    //    List<string> typeList = await Context.Queryable<SysBot>()
    //        .GroupBy(c => c.TypeCode)
    //        .ToListAsync(c => c.TypeCode);

    //    return typeList;
    //}

    ///// <summary>
    ///// 查询系统机器人列表
    ///// </summary>
    ///// <param name="whereDto"></param>
    ///// <returns></returns>
    //public async Task<List<SysBot>> GetSysBotList(SysBotWDto whereDto)
    //{
    //    Expressionable<SysBot> whereExpression = Expressionable.Create<SysBot>();
    //    _ = whereExpression.AndIF(whereDto.TypeCode != null, u => u.TypeCode == whereDto.TypeCode);
    //    _ = whereExpression.AndIF(whereDto.Code != null, u => u.Code == whereDto.Code);
    //    _ = whereExpression.AndIF(whereDto.Name.IsNotEmptyOrNull(), u => u.Name.Contains(whereDto.Name!));
    //    _ = whereExpression.AndIF(whereDto.IsOfficial != null, u => u.IsOfficial == whereDto.IsOfficial);

    //    return await QueryAsync(whereExpression.ToExpression(), o => new { o.TypeCode, o.SortOrder }, false);
    //}

    ///// <summary>
    ///// 查询系统机器人列表(根据分页条件)
    ///// </summary>
    ///// <param name="pageWhere"></param>
    ///// <returns></returns>
    //public async Task<PageDataDto<SysBot>> GetSysBotPageList(PageWhereDto<SysBotWDto> pageWhere)
    //{
    //    SysBotWDto whereDto = pageWhere.Where;

    //    Expressionable<SysBot> whereExpression = Expressionable.Create<SysBot>();
    //    _ = whereExpression.AndIF(whereDto.TypeCode != null, u => u.TypeCode == whereDto.TypeCode);
    //    _ = whereExpression.AndIF(whereDto.Code != null, u => u.Code == whereDto.Code);
    //    _ = whereExpression.AndIF(whereDto.Name.IsNotEmptyOrNull(), u => u.Name.Contains(whereDto.Name!));
    //    _ = whereExpression.AndIF(whereDto.IsOfficial != null, u => u.IsOfficial == whereDto.IsOfficial);

    //    return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => new { o.TypeCode, o.SortOrder }, pageWhere.IsAsc);
    //}
}