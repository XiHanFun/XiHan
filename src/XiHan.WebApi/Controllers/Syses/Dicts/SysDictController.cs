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

using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHan.Application.Common.Swagger;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Models.Syses;
using XiHan.Services.Syses.Dicts;
using XiHan.Services.Syses.Dicts.Dtos;
using XiHan.WebApi.Controllers.Bases;
using XiHan.WebApi.Controllers.Syses.Dicts.Dtos;

namespace XiHan.WebApi.Controllers.Syses.Dicts;

/// <summary>
/// 系统字典管理
/// <code>包含：字典/字典项</code>
/// </summary>
//[Authorize]
[ApiGroup(ApiGroupNames.Manage)]
[Route("Sys/Dict")]
public class SysDictController : BaseApiController
{
    private readonly ISysDictTypeService _sysDictTypeService;
    private readonly ISysDictDataService _sysDictDataService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysDictTypeService"></param>
    /// <param name="sysDictDataService"></param>
    public SysDictController(ISysDictTypeService sysDictTypeService, ISysDictDataService sysDictDataService)
    {
        _sysDictTypeService = sysDictTypeService;
        _sysDictDataService = sysDictDataService;
    }

    #region 新增

    /// <summary>
    /// 新增字典
    /// </summary>
    /// <param name="cSysDictType"></param>
    /// <returns></returns>
    [HttpPost("Create/Type")]
    [AppLog(Module = "字典", BusinessType = BusinessTypeEnum.Create)]
    public async Task<CustomResult> CreateDictType([FromBody] CSysDictTypeDto cSysDictType)
    {
        var sysDictType = cSysDictType.Adapt<SysDictType>();
        var result = await _sysDictTypeService.CreateDictType(sysDictType);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 新增字典项
    /// </summary>
    /// <param name="cSysDictData"></param>
    /// <returns></returns>
    [HttpPost("Create/Data")]
    [AppLog(Module = "字典项", BusinessType = BusinessTypeEnum.Create)]
    public async Task<CustomResult> CreateDictData([FromBody] CSysDictDataDto cSysDictData)
    {
        var sysDictData = cSysDictData.Adapt<SysDictData>();
        var result = await _sysDictDataService.CreateDictData(sysDictData);
        return CustomResult.Success(result);
    }

    #endregion

    #region 删除

    /// <summary>
    /// 删除字典
    /// </summary>
    /// <param name="dictIds"></param>
    /// <returns></returns>
    [HttpDelete("Delete/Type/ByIds")]
    [AppLog(Module = "字典", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<CustomResult> DeleteDictType([FromBody] long[] dictIds)
    {
        var result = await _sysDictTypeService.DeleteDictTypeByIds(dictIds);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 删除字典项
    /// </summary>
    /// <param name="dictDataIds"></param>
    /// <returns></returns>
    [HttpDelete("Delete/Data/ByIds")]
    [AppLog(Module = "字典项", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<CustomResult> DeleteDictData([FromBody] long[] dictDataIds)
    {
        var result = await _sysDictDataService.DeleteDictDataByIds(dictDataIds);
        return CustomResult.Success(result);
    }

    #endregion

    #region 修改

    /// <summary>
    /// 修改字典
    /// </summary>
    /// <param name="cSysDictType"></param>
    /// <returns></returns>
    [HttpPut("Modify/Type")]
    [AppLog(Module = "字典", BusinessType = BusinessTypeEnum.Modify)]
    public async Task<CustomResult> ModifyDictData([FromBody] CSysDictTypeDto cSysDictType)
    {
        var sysDictType = cSysDictType.Adapt<SysDictType>();
        var result = await _sysDictTypeService.ModifyDictType(sysDictType);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 修改字典项
    /// </summary>
    /// <param name="cSysDictData"></param>
    /// <returns></returns>
    [HttpPut("Modify/Data")]
    [AppLog(Module = "字典项", BusinessType = BusinessTypeEnum.Modify)]
    public async Task<CustomResult> ModifyDictData([FromBody] CSysDictDataDto cSysDictData)
    {
        var sysDictData = cSysDictData.Adapt<SysDictData>();
        var result = await _sysDictDataService.ModifyDictData(sysDictData);
        return CustomResult.Success(result);
    }

    #endregion

    #region 查询

    /// <summary>
    /// 查询字典详情
    /// </summary>
    /// <param name="dictId"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Get/Type/ById")]
    public async Task<CustomResult> GetDictTypeById([FromBody] long dictId)
    {
        var result = await _sysDictTypeService.GetDictTypeById(dictId);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 查询字典分页列表
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Get/Type/PageList")]
    public async Task<CustomResult> GetDictTypePageList([FromBody] PageWhereDto<SysDictTypeWhereDto> pageWhere)
    {
        var result = await _sysDictTypeService.GetDictTypePageList(pageWhere);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 查询字典项详情
    /// </summary>
    /// <param name="dictDataId"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Get/Data/ById")]
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
    [AllowAnonymous]
    [HttpPost("Get/Data/ByTypeCode")]
    public async Task<CustomResult> GetDictDataByType([FromBody] string dictCode)
    {
        var result = await _sysDictDataService.GetDictDataByType(dictCode);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 查询字典项列表(根据多个字典)
    /// </summary>
    /// <param name="dictCodes"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Get/Data/ByTypes")]
    public async Task<CustomResult> GetDictDataByTypes([FromBody] string[] dictCodes)
    {
        var result = await _sysDictDataService.GetDictDataByTypes(dictCodes);
        return CustomResult.Success(result);
    }

    /// <summary>
    /// 查询字典项分页列表
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Get/Data/PageList")]
    public async Task<CustomResult> GetDictDataPageList([FromBody] PageWhereDto<SysDictDataWhereDto> pageWhere)
    {
        var result = await _sysDictDataService.GetDictDataPageList(pageWhere);
        return CustomResult.Success(result);
    }

    #endregion

    #region 导出

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

    #endregion
}