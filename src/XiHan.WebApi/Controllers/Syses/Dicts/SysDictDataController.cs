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

using Microsoft.AspNetCore.Mvc;
using XiHan.Application.Common.Swagger;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Services.Syses.Dicts;
using XiHan.Services.Syses.Dicts.Dtos;
using XiHan.WebApi.Controllers.Bases;

namespace XiHan.WebApi.Controllers.Syses.Dicts;

/// <summary>
/// 系统字典项管理
/// </summary>
//[Authorize]
[ApiGroup(ApiGroupNames.Manage)]
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
    /// 新增字典项
    /// </summary>
    /// <param name="dictDataCDto"></param>
    /// <returns></returns>
    [HttpPost("Create")]
    [AppLog(Module = "系统字典项", BusinessType = BusinessTypeEnum.Create)]
    public async Task<CustomResult> CreateDictData([FromBody] SysDictDataCDto dictDataCDto)
    {
        var result = await _sysDictDataService.CreateDictData(dictDataCDto);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 删除字典项
    /// </summary>
    /// <param name="dictDataIds"></param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [AppLog(Module = "系统字典项", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<CustomResult> DeleteDictData([FromBody] long[] dictDataIds)
    {
        var result = await _sysDictDataService.DeleteDictDataByIds(dictDataIds);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 修改字典项
    /// </summary>
    /// <param name="dictDataCDto"></param>
    /// <returns></returns>
    [HttpPut("Modify")]
    [AppLog(Module = "系统字典项", BusinessType = BusinessTypeEnum.Modify)]
    public async Task<CustomResult> ModifyDictData([FromBody] SysDictDataCDto dictDataCDto)
    {
        var result = await _sysDictDataService.ModifyDictData(dictDataCDto);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 查询字典项详情
    /// </summary>
    /// <param name="dictDataId"></param>
    /// <returns></returns>
    [HttpPost("Get/ById")]
    [AppLog(Module = "系统字典项", BusinessType = BusinessTypeEnum.Get)]
    public async Task<CustomResult> GetDictDataById([FromBody] long dictDataId)
    {
        var result = await _sysDictDataService.GetDictDataById(dictDataId);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 查询字典项列表(根据字典编码)
    /// </summary>
    /// <param name="dictCode"></param>
    /// <returns></returns>
    [HttpPost("GetList/ByTypeCode")]
    [AppLog(Module = "系统字典项", BusinessType = BusinessTypeEnum.Get)]
    public async Task<CustomResult> GetDictDataListByTypeCode([FromBody] string dictCode)
    {
        var result = await _sysDictDataService.GetDictDataListByType(dictCode);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 查询字典项列表(根据多个字典)
    /// </summary>
    /// <param name="dictCodes"></param>
    /// <returns></returns>
    [HttpPost("GetList/ByTypes")]
    [AppLog(Module = "系统字典项", BusinessType = BusinessTypeEnum.Get)]
    public async Task<CustomResult> GetDictDataListByTypes([FromBody] string[] dictCodes)
    {
        var result = await _sysDictDataService.GetDictDataListByTypes(dictCodes);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 查询字典项分页列表
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("GetPageList")]
    [AppLog(Module = "系统字典项", BusinessType = BusinessTypeEnum.Get)]
    public async Task<CustomResult> GetDictDataPageList([FromBody] PageWhereDto<SysDictDataWDto> pageWhere)
    {
        var result = await _sysDictDataService.GetDictDataPageList(pageWhere);
        return CustomResult.Success(result);
    }
}