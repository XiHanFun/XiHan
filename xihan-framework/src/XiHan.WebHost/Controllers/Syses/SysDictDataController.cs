#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysDictDataController
// Guid:2239c7d7-7eea-4dae-9ee1-d7d9b2b2dd78
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-10 上午 04:37:19
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Responses;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Services.Syses.Dicts;
using XiHan.Services.Syses.Dicts.Dtos;
using XiHan.WebCore.Common.Swagger;

namespace XiHan.WebHost.Controllers.Syses;

/// <summary>
/// 系统字典项管理
/// </summary>
[Authorize]
[ApiGroup(ApiGroupNameEnum.Manage)]
public class SysDictDataController : BaseApiController
{
    private readonly ISysDictDataService _sysDictDataService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysDictDataService"></param>
    public SysDictDataController(ISysDictDataService sysDictDataService)
    {
        _sysDictDataService = sysDictDataService;
    }

    /// <summary>
    /// 新增系统字典项
    /// </summary>
    /// <param name="dictDataCDto"></param>
    /// <returns></returns>
    [HttpPost("Create")]
    [AppLog(Module = "系统字典项", BusinessType = BusinessTypeEnum.Create)]
    public async Task<ApiResult> CreateDictData([FromBody] SysDictDataCDto dictDataCDto)
    {
        var result = await _sysDictDataService.CreateDictData(dictDataCDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 删除系统字典项
    /// </summary>
    /// <param name="dictDataIds"></param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [AppLog(Module = "系统字典项", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteDictData([FromBody] long[] dictDataIds)
    {
        var result = await _sysDictDataService.DeleteDictDataByIds(dictDataIds);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 修改系统字典项
    /// </summary>
    /// <param name="dictDataMDto"></param>
    /// <returns></returns>
    [HttpPut("Modify")]
    [AppLog(Module = "系统字典项", BusinessType = BusinessTypeEnum.Modify)]
    public async Task<ApiResult> ModifyDictData([FromBody] SysDictDataMDto dictDataMDto)
    {
        var result = await _sysDictDataService.ModifyDictData(dictDataMDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统字典项详情
    /// </summary>
    /// <param name="dictDataId"></param>
    /// <returns></returns>
    [HttpPost("Get/ById")]
    [ApiGroup(ApiGroupNameEnum.Display)]
    public async Task<ApiResult> GetDictDataById([FromBody] long dictDataId)
    {
        var result = await _sysDictDataService.GetDictDataById(dictDataId);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统字典项列表(根据字典编码)
    /// </summary>
    /// <param name="dictCode"></param>
    /// <returns></returns>
    [HttpPost("GetList/ByTypeCode")]
    [AppLog(Module = "系统字典项", BusinessType = BusinessTypeEnum.Get)]
    [ApiGroup(ApiGroupNameEnum.Display)]
    public async Task<ApiResult> GetDictDataListByTypeCode([FromBody] string dictCode)
    {
        var result = await _sysDictDataService.GetDictDataListByType(dictCode);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统字典项列表(根据多个字典)
    /// </summary>
    /// <param name="dictCodes"></param>
    /// <returns></returns>
    [HttpPost("GetList/ByTypes")]
    [AppLog(Module = "系统字典项", BusinessType = BusinessTypeEnum.Get)]
    [ApiGroup(ApiGroupNameEnum.Display)]
    public async Task<ApiResult> GetDictDataListByTypes([FromBody] string[] dictCodes)
    {
        var result = await _sysDictDataService.GetDictDataListByTypes(dictCodes);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统字典项分页列表
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("GetPageList")]
    [AppLog(Module = "系统字典项", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetDictDataPageList([FromBody] PageWhereDto<SysDictDataWDto> pageWhere)
    {
        var result = await _sysDictDataService.GetDictDataPageList(pageWhere);
        return ApiResult.Success(result);
    }
}