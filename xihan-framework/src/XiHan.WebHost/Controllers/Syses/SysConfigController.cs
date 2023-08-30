#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysConfigController
// Guid:60fad521-fe18-4a79-b7f6-af60b2fc44cf
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/18 22:49:42
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Responses;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Services.Syses.Configs;
using XiHan.Services.Syses.Configs.Dtos;
using XiHan.WebCore.Common.Swagger;
using XiHan.WebHost.Controllers.Bases;

namespace XiHan.WebHost.Controllers.Syses;

/// <summary>
/// 系统配置管理
/// </summary>
[Authorize]
[ApiGroup(ApiGroupNameEnum.Manage)]
public class SysConfigController : BaseApiController
{
    private readonly ISysConfigService _sysConfigService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysConfigService"></param>
    public SysConfigController(ISysConfigService sysConfigService)
    {
        _sysConfigService = sysConfigService;
    }

    /// <summary>
    /// 新增系统配置
    /// </summary>
    /// <param name="configCDto"></param>
    /// <returns></returns>
    [HttpPost("Create")]
    [AppLog(Module = "系统配置", BusinessType = BusinessTypeEnum.Create)]
    public async Task<ApiResult> CreateDictData([FromBody] SysConfigCDto configCDto)
    {
        long result = await _sysConfigService.CreateSysConfig(configCDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 删除系统配置
    /// </summary>
    /// <param name="configIds"></param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [AppLog(Module = "系统配置", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteSysConfigByIds([FromBody] long[] configIds)
    {
        bool result = await _sysConfigService.DeleteSysConfigByIds(configIds);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 修改系统配置
    /// </summary>
    /// <param name="configMDto"></param>
    /// <returns></returns>
    [HttpPut("Modify")]
    [AppLog(Module = "系统配置", BusinessType = BusinessTypeEnum.Modify)]
    public async Task<ApiResult> ModifySysConfig([FromBody] SysConfigMDto configMDto)
    {
        bool result = await _sysConfigService.ModifySysConfig(configMDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统配置(根据Id)
    /// </summary>
    /// <param name="configId"></param>
    /// <returns></returns>
    [HttpPost("Get/ById")]
    [AppLog(Module = "系统配置", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetSysConfigById([FromBody] long configId)
    {
        Models.Syses.SysConfig result = await _sysConfigService.GetSysConfigById(configId);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统配置值(根据Code)
    /// </summary>
    /// <param name="configCode"></param>
    /// <returns></returns>
    [HttpPost("Get/Value/ByCode")]
    [AppLog(Module = "系统配置", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetSysConfigValueByCode<T>([FromBody] string configCode)
    {
        T? result = await _sysConfigService.GetSysConfigValueByCode<T>(configCode);
        return ApiResult.Success(result!);
    }

    /// <summary>
    /// 查询系统配置分类列表
    /// </summary>
    /// <returns></returns>
    [HttpPost("Get/Type/List")]
    [AppLog(Module = "系统配置", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetSysConfigTypeList()
    {
        List<string> result = await _sysConfigService.GetSysConfigTypeList();
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统配置列表
    /// </summary>
    /// <param name="configWDto"></param>
    /// <returns></returns>
    [HttpPost("GetList")]
    [AppLog(Module = "系统配置", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetSysConfigList([FromBody] SysConfigWDto configWDto)
    {
        List<Models.Syses.SysConfig> result = await _sysConfigService.GetSysConfigList(configWDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统配置列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("GetPageList")]
    [AppLog(Module = "系统配置", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetSysConfigPageList([FromBody] PageWhereDto<SysConfigWDto> pageWhere)
    {
        PageDataDto<Models.Syses.SysConfig> result = await _sysConfigService.GetSysConfigPageList(pageWhere);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 导出系统配置
    /// </summary>
    /// <returns></returns>
    [HttpGet("Export/Data")]
    [AppLog(Module = "系统配置", BusinessType = BusinessTypeEnum.Export, IsSaveRequestData = false)]
    public async Task<ApiResult> ExportDict()
    {
        List<Models.Syses.SysConfig> result = await _sysConfigService.GetListAsync();
        await ExportExcel("系统配置", result, "SysConfig");
        return ApiResult.Success($"系统配置导出成功！");
    }
}