#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysBotController
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
using XiHan.Models.Syses;
using XiHan.Services.Syses.Bots;
using XiHan.Services.Syses.Bots.Dtos;
using XiHan.WebCore.Common.Swagger;

namespace XiHan.WebHost.Controllers.Syses;

/// <summary>
/// 系统机器人管理
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="sysBotService"></param>
[Authorize]
[ApiGroup(ApiGroupNameEnum.Manage)]
public class SysBotController(ISysBotService sysBotService) : BaseApiController
{
    private readonly ISysBotService _sysBotService = sysBotService;

    /// <summary>
    /// 新增系统机器人
    /// </summary>
    /// <param name="botCDto"></param>
    /// <returns></returns>
    [HttpPost("Create")]
    [AppLog(Module = "系统机器人", BusinessType = BusinessTypeEnum.Create)]
    public async Task<ApiResult> CreateDictData([FromBody] SysBotCDto botCDto)
    {
        var result = await _sysBotService.CreateSysBot(botCDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 删除系统机器人
    /// </summary>
    /// <param name="botIds"></param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [AppLog(Module = "系统机器人", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteSysBotByIds([FromBody] long[] botIds)
    {
        var result = await _sysBotService.DeleteSysBotByIds(botIds);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 修改系统机器人
    /// </summary>
    /// <param name="botMDto"></param>
    /// <returns></returns>
    [HttpPut("Modify")]
    [AppLog(Module = "系统机器人", BusinessType = BusinessTypeEnum.Modify)]
    public async Task<ApiResult> ModifySysBot([FromBody] SysBotMDto botMDto)
    {
        var result = await _sysBotService.ModifySysBot(botMDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统机器人(根据Id)
    /// </summary>
    /// <param name="botId"></param>
    /// <returns></returns>
    [HttpPost("Get/ById")]
    [AppLog(Module = "系统机器人", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetSysBotById([FromBody] long botId)
    {
        var result = await _sysBotService.GetSysBotById(botId);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统机器人分类列表
    /// </summary>
    /// <returns></returns>
    [HttpPost("Get/Type/List")]
    [AppLog(Module = "系统机器人", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetSysBotTypeList()
    {
        var result = await _sysBotService.GetSysBotTypeList();
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统机器人列表
    /// </summary>
    /// <param name="botWDto"></param>
    /// <returns></returns>
    [HttpPost("GetList")]
    [AppLog(Module = "系统机器人", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetSysBotList([FromBody] SysBotWDto botWDto)
    {
        var result = await _sysBotService.GetSysBotList(botWDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统机器人列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("GetPageList")]
    [AppLog(Module = "系统机器人", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetSysBotPageList([FromBody] PageWhereDto<SysBotWDto> pageWhere)
    {
        var result = await _sysBotService.GetSysBotPageList(pageWhere);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 导出系统机器人
    /// </summary>
    /// <returns></returns>
    [HttpGet("Export/Data")]
    [AppLog(Module = "系统机器人", BusinessType = BusinessTypeEnum.Export, IsSaveRequestData = false)]
    public async Task<ApiResult> ExportDict()
    {
        List<SysBot> result = await _sysBotService.GetListAsync();
        await ExportExcel("系统机器人", result, "SysBot");
        return ApiResult.Success($"系统机器人导出成功！");
    }
}