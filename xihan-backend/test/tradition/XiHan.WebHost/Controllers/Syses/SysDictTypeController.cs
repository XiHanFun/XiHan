#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysDictController
// Guid:57b61160-85b3-47f9-8fe4-144bf4c1b3f5
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-13 下午 11:54:11
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Responses;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Services.Syses.Dicts;
using XiHan.Services.Syses.Dicts.Dtos;
using XiHan.WebCore.Common.Swagger;

namespace XiHan.WebHost.Controllers.Syses;

/// <summary>
/// 系统字典管理
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="sysDictTypeService"></param>
//[Authorize]
[ApiGroup(ApiGroupNameEnum.Manage)]
public class SysDictTypeController(ISysDictTypeService sysDictTypeService) : BaseApiController
{
    private readonly ISysDictTypeService _sysDictTypeService = sysDictTypeService;

    /// <summary>
    /// 新增系统字典
    /// </summary>
    /// <param name="dictTypeCDto"></param>
    /// <returns></returns>
    [HttpPost("Create")]
    [AppLog(Module = "系统字典", BusinessType = BusinessTypeEnum.Create)]
    public async Task<ApiResult> CreateDictType([FromBody] SysDictTypeCDto dictTypeCDto)
    {
        var result = await _sysDictTypeService.CreateDictType(dictTypeCDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 删除系统字典
    /// </summary>
    /// <param name="dictIds"></param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [AppLog(Module = "系统字典", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteDictType([FromBody] long[] dictIds)
    {
        var result = await _sysDictTypeService.DeleteDictTypeByIds(dictIds);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 修改系统字典
    /// </summary>
    /// <param name="dictTypeMDto"></param>
    /// <returns></returns>
    [HttpPut("Modify")]
    [AppLog(Module = "系统字典", BusinessType = BusinessTypeEnum.Modify)]
    public async Task<ApiResult> ModifyDictData([FromBody] SysDictTypeMDto dictTypeMDto)
    {
        var result = await _sysDictTypeService.ModifyDictType(dictTypeMDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统字典详情
    /// </summary>
    /// <param name="dictId"></param>
    /// <returns></returns>
    [HttpPost("GetList/ById")]
    [AppLog(Module = "系统字典", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetDictTypeById([FromBody] long dictId)
    {
        var result = await _sysDictTypeService.GetDictTypeById(dictId);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统字典列表
    /// </summary>
    /// <param name="dictTypeWDto"></param>
    /// <returns></returns>
    [HttpPost("GetList")]
    [AppLog(Module = "系统字典", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetDictTypeList([FromBody] SysDictTypeWDto dictTypeWDto)
    {
        var result = await _sysDictTypeService.GetDictTypeList(dictTypeWDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统字典分页列表
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("GetPageList")]
    [AppLog(Module = "系统字典", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetDictTypePageList([FromBody] PageWhereDto<SysDictTypeWDto> pageWhere)
    {
        var result = await _sysDictTypeService.GetDictTypePageList(pageWhere);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 导出系统字典
    /// </summary>
    /// <returns></returns>
    [HttpGet("Export/Data")]
    [AppLog(Module = "系统字典", BusinessType = BusinessTypeEnum.Export, IsSaveRequestData = false)]
    public async Task<ApiResult> ExportDict()
    {
        var result = await _sysDictTypeService.GetDictTypeList();
        await ExportExcel("系统字典", result, "SysDictType");
        return ApiResult.Success($"系统字典导出成功！");
    }
}