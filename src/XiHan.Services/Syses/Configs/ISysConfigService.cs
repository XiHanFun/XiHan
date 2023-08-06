#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysConfigService
// Guid:dccfc4de-97c7-4fd0-89e2-34327fe15fba
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/5 3:10:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Configs.Dtos;

namespace XiHan.Services.Syses.Configs;

/// <summary>
/// ISysConfigService
/// </summary>
public interface ISysConfigService : IBaseService<SysConfig>
{
    /// <summary>
    /// 新增系统配置
    /// </summary>
    /// <param name="configCDto"></param>
    /// <returns></returns>
    Task<long> CreateSysConfig(SysConfigCDto configCDto);

    /// <summary>
    /// 批量删除系统配置
    /// </summary>
    /// <param name="configIds"></param>
    /// <returns></returns>
    Task<bool> DeleteSysConfigByIds(long[] configIds);

    /// <summary>
    /// 修改系统配置
    /// </summary>
    /// <param name="sysConfigCDto"></param>
    /// <returns></returns>
    Task<bool> ModifySysConfig(SysConfigCDto sysConfigCDto);

    /// <summary>
    /// 查询系统配置(根据Id)
    /// </summary>
    /// <param name="configId"></param>
    /// <returns></returns>
    Task<SysConfig> GetSysConfigById(long configId);

    /// <summary>
    /// 查询系统配置值(根据Code)
    /// </summary>
    /// <param name="configCode"></param>
    /// <returns></returns>
    Task<T> GetSysConfigValueByCode<T>(string configCode);

    /// <summary>
    /// 查询系统配置分类列表
    /// </summary>
    /// <returns></returns>
    Task<List<string>> GetSysConfigTypeList();

    /// <summary>
    /// 查询系统配置列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysConfig>> GetSysConfigPageList(PageWhereDto<SysConfigWDto> pageWhere);
}