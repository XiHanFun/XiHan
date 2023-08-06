#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysConfigService
// Guid:a6456028-97a0-415c-9c00-1985bb3e9f3a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/5 3:10:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using SqlSugar;
using XiHan.Infrastructures.Apps.Caches;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Configs.Dtos;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Configs.Logic;

/// <summary>
/// 系统配置服务
/// </summary>
[AppService(ServiceType = typeof(ISysConfigService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysConfigService : BaseService<SysConfig>, ISysConfigService
{
    private readonly IAppCacheService _appCacheService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="appCacheService"></param>
    public SysConfigService(IAppCacheService appCacheService)
    {
        _appCacheService = appCacheService;
    }

    /// <summary>
    /// 校验配置项是否唯一
    /// </summary>
    /// <param name="sysConfig"></param>
    /// <returns></returns>
    private async Task<bool> CheckConfigUnique(SysConfig sysConfig)
    {
        var isUnique = await IsAnyAsync(f => f.TypeCode == sysConfig.TypeCode && (f.Code == sysConfig.Code || f.Name == sysConfig.Name));
        if (isUnique) throw new CustomException($"配置类【{sysConfig.TypeCode}】下已存在配置项【{sysConfig.Code}({sysConfig.Name})】!");
        return isUnique;
    }

    /// <summary>
    /// 新增系统配置
    /// </summary>
    /// <param name="configCDto"></param>
    /// <returns></returns>
    public async Task<long> CreateSysConfig(SysConfigCDto configCDto)
    {
        var sysConfig = configCDto.Adapt<SysConfig>();

        _ = await CheckConfigUnique(sysConfig);

        return await AddReturnIdAsync(sysConfig);
    }

    /// <summary>
    /// 批量删除系统配置
    /// </summary>
    /// <param name="configIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteSysConfigByIds(long[] configIds)
    {
        var sysConfigList = await QueryAsync(d => configIds.Contains(d.BaseId));

        // 禁止删除系统参数
        var deleteList = sysConfigList.Where(c => !c.IsOfficial).ToList();

        _appCacheService.Remove(deleteList.Select(c => c.Code));
        return await RemoveAsync(deleteList);
    }

    /// <summary>
    /// 修改系统配置
    /// </summary>
    /// <param name="sysConfigCDto"></param>
    /// <returns></returns>
    public async Task<bool> ModifySysConfig(SysConfigCDto sysConfigCDto)
    {
        var sysSysConfig = sysConfigCDto.Adapt<SysConfig>();

        _ = await CheckConfigUnique(sysSysConfig);

        return await UpdateAsync(sysSysConfig);
    }

    /// <summary>
    /// 查询系统配置(根据Id)
    /// </summary>
    /// <param name="configId"></param>
    /// <returns></returns>
    public async Task<SysConfig> GetSysConfigById(long configId)
    {
        var key = $"GetSysConfigById_{configId}";
        if (_appCacheService.Get(key) is SysConfig sysConfig) return sysConfig;
        sysConfig = await FindAsync(d => d.BaseId == configId);

        _appCacheService.SetWithMinutes(key, sysConfig, 30);
        return sysConfig;
    }

    /// <summary>
    /// 查询系统配置值(根据Code)
    /// </summary>
    /// <param name="configCode"></param>
    /// <returns></returns>
    public async Task<T> GetSysConfigValueByCode<T>(string configCode)
    {
        var key = $"GetSysConfigValueByCode_{configCode}";
        var value = _appCacheService.Get<string>(key);
        if (value.IsNotEmptyOrNull()) return value.CastTo<T>()!;

        var sysConfig = await FindAsync(d => d.Code == configCode);

        _appCacheService.SetWithMinutes(key, sysConfig.Value, 30);
        return sysConfig.Value.CastTo<T>()!;
    }

    /// <summary>
    /// 查询系统配置分类列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<string>> GetSysConfigTypeList()
    {
        var typeList = await Context.Queryable<SysConfig>()
            .GroupBy(c => c.TypeCode)
            .Select(c => c.TypeCode)
            .ToListAsync();

        return typeList;
    }

    /// <summary>
    /// 查询系统配置列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysConfig>> GetSysConfigPageList(PageWhereDto<SysConfigWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        var whereExpression = Expressionable.Create<SysConfig>();
        whereExpression.AndIF(whereDto.TypeCode != null, u => u.TypeCode == whereDto.TypeCode);
        whereExpression.AndIF(whereDto.Code != null, u => u.Code == whereDto.Code);
        whereExpression.AndIF(whereDto.Name.IsNotEmptyOrNull(), u => u.Name.Contains(whereDto.Name!));
        whereExpression.AndIF(whereDto.IsOfficial != null, u => u.IsOfficial == whereDto.IsOfficial);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.SortOrder);
    }
}