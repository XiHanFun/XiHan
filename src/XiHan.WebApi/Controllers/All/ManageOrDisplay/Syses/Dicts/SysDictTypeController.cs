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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHan.Application.Common.Swagger;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Services.Syses.Dicts;
using XiHan.Services.Syses.Dicts.Dtos;
using XiHan.WebApi.Controllers.Bases;

namespace XiHan.WebApi.Controllers.All.ManageOrDisplay.Syses.Dicts;

/// <summary>
/// 系统字典管理
/// </summary>
[Authorize]
[ApiGroup(ApiGroupNames.Manage)]
public class SysDictTypeController : BaseApiController
{
    private readonly ISysDictTypeService _sysDictTypeService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysDictTypeService"></param>
    public SysDictTypeController(ISysDictTypeService sysDictTypeService)
    {
        _sysDictTypeService = sysDictTypeService;
    }

    /// <summary>
    /// 新增字典
    /// </summary>
    /// <param name="dictTypeCDto"></param>
    /// <returns></returns>
    [HttpPost("Create")]
    [AppLog(Module = "系统字典", BusinessType = BusinessTypeEnum.Create)]
    public async Task<CustomResult> CreateDictType([FromBody] SysDictTypeCDto dictTypeCDto)
    {
        var result = await _sysDictTypeService.CreateDictType(dictTypeCDto);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 删除字典
    /// </summary>
    /// <param name="dictIds"></param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [AppLog(Module = "系统字典", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<CustomResult> DeleteDictType([FromBody] long[] dictIds)
    {
        var result = await _sysDictTypeService.DeleteDictTypeByIds(dictIds);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 修改字典
    /// </summary>
    /// <param name="dictTypeCDto"></param>
    /// <returns></returns>
    [HttpPut("Modify")]
    [AppLog(Module = "系统字典", BusinessType = BusinessTypeEnum.Modify)]
    public async Task<CustomResult> ModifyDictData([FromBody] SysDictTypeCDto dictTypeCDto)
    {
        var result = await _sysDictTypeService.ModifyDictType(dictTypeCDto);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 查询字典详情
    /// </summary>
    /// <param name="dictId"></param>
    /// <returns></returns>
    [HttpPost("GetList/ById")]
    [AppLog(Module = "系统字典", BusinessType = BusinessTypeEnum.Get)]
    public async Task<CustomResult> GetDictTypeById([FromBody] long dictId)
    {
        var result = await _sysDictTypeService.GetDictTypeById(dictId);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 查询字典列表
    /// </summary>
    /// <param name="dictTypeWDto"></param>
    /// <returns></returns>
    [HttpPost("GetList")]
    [AppLog(Module = "系统字典", BusinessType = BusinessTypeEnum.Get)]
    public async Task<CustomResult> GetDictTypePageList([FromBody] SysDictTypeWDto dictTypeWDto)
    {
        var result = await _sysDictTypeService.GetDictTypeList(dictTypeWDto);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 查询字典分页列表
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("GetPageList")]
    [AppLog(Module = "系统字典", BusinessType = BusinessTypeEnum.Get)]
    public async Task<CustomResult> GetDictTypePageList([FromBody] PageWhereDto<SysDictTypeWDto> pageWhere)
    {
        var result = await _sysDictTypeService.GetDictTypePageList(pageWhere);
        return CustomResult.Success(result);
    }

    ///// <summary>
    ///// 字典导出
    ///// </summary>
    ///// <returns></returns>
    //[HttpGet("Export/Data")]
    //[AppLog(Module = "字典导出", BusinessType = BusinessTypeEnum.Export, IsSaveRequestData = false)]
    //public async Task<FileStreamResult> ExportDict()
    //{
    //    var result = await _sysDictTypeService.GetAllDictType();
    //    var fileName = ExportExcel(result, "SysDictType", "字典");
    //    return ExportExcel(fileName.Item2, fileName.Item1);
    //    //return ResultDto.Success(new { path = "/Export/" + fileName, fileName = fileName.Item1 });
    //}
}