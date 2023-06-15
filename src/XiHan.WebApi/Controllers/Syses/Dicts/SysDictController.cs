#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysDictController
// Guid:57b61160-85b3-47f9-8fe4-144bf4c1b3f5
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-06-13 下午 11:54:11
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Infrastructures.Responses.Results;
using XiHan.WebApi.Controllers.Bases;
using XiHan.WebApi.Controllers.Syses.Dicts.Dtos;
using XiHan.Models.Syses;
using XiHan.Services.Syses.Dicts;
using XiHan.Services.Syses.Dicts.Dtos;
using XiHan.Application.Common.Swagger;

namespace XiHan.WebApi.Controllers.Syses.Dicts;

/// <summary>
/// 系统字典管理
/// <code>包含：字典类型/字典数据</code>
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
    /// 新增字典类型
    /// </summary>
    /// <param name="cSysDictType"></param>
    /// <returns></returns>
    [HttpPost("Create/Type")]
    [AppLog(Title = "字典类型", BusinessType = BusinessTypeEnum.Create)]
    public async Task<ResultDto> CreateDictType([FromBody] CSysDictTypeDto cSysDictType)
    {
        SysDictType sysDictType = cSysDictType.Adapt<SysDictType>();
        var result = await _sysDictTypeService.CreateDictType(sysDictType);
        return ResultDto.Success(result);
    }

    /// <summary>
    /// 新增字典数据
    /// </summary>
    /// <param name="cSysDictData"></param>
    /// <returns></returns>
    [HttpPost("Create/Data")]
    [AppLog(Title = "字典数据", BusinessType = BusinessTypeEnum.Create)]
    public async Task<ResultDto> CreateDictData([FromBody] CSysDictDataDto cSysDictData)
    {
        SysDictData sysDictData = cSysDictData.Adapt<SysDictData>();
        var result = await _sysDictDataService.CreateDictData(sysDictData);
        return ResultDto.Success(result);
    }

    #endregion

    #region 删除

    /// <summary>
    /// 删除字典类型
    /// </summary>
    /// <returns></returns>
    [HttpDelete("Delete/Type")]
    [AppLog(Title = "字典类型", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ResultDto> DeleteDictType([FromBody] long[] ids)
    {
        var result = await _sysDictTypeService.DeleteDictTypeByIds(ids);
        return ResultDto.Success(result);
    }

    /// <summary>
    /// 删除字典数据
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpDelete("Delete/Data")]
    [AppLog(Title = "字典数据", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ResultDto> DeleteDictData([FromBody] long[] ids)
    {
        var result = await _sysDictDataService.DeleteDictDataByIds(ids);
        return ResultDto.Success(result);
    }

    #endregion

    #region 修改

    /// <summary>
    /// 修改字典类型
    /// </summary>
    /// <param name="cSysDictType"></param>
    /// <returns></returns>
    [HttpPut("Modify/Type")]
    [AppLog(Title = "字典类型", BusinessType = BusinessTypeEnum.Modify)]
    public async Task<ResultDto> ModifyDictData([FromBody] CSysDictTypeDto cSysDictType)
    {
        var sysDictType = cSysDictType.Adapt<SysDictType>();
        var result = await _sysDictTypeService.ModifyDictType(sysDictType);
        return ResultDto.Success(result);
    }

    /// <summary>
    /// 修改字典数据
    /// </summary>
    /// <param name="cSysDictData"></param>
    /// <returns></returns>
    [HttpPut("Modify/Data")]
    [AppLog(Title = "字典数据", BusinessType = BusinessTypeEnum.Modify)]
    public async Task<ResultDto> ModifyDictData([FromBody] CSysDictDataDto cSysDictData)
    {
        var sysDictData = cSysDictData.Adapt<SysDictData>();
        var result = await _sysDictDataService.ModifyDictData(sysDictData);
        return ResultDto.Success(result);
    }

    #endregion

    #region 查询

    /// <summary>
    /// 查询字典类型详情
    /// </summary>
    /// <param name="sysDictTypeId"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("Get/Type/{sysDictTypeId}")]
    public async Task<ResultDto> GetDictTypeById(long sysDictTypeId)
    {
        var result = await _sysDictTypeService.GetDictTypeById(sysDictTypeId);
        return ResultDto.Success(result);
    }

    /// <summary>
    /// 查询字典类型列表
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Get/Type/List")]
    public async Task<ResultDto> GetDictTypeList([FromBody] PageWhereDto<SysDictTypeWhereDto> pageWhere)
    {
        var result = await _sysDictTypeService.GetDictTypeList(pageWhere);
        return ResultDto.Success(result);
    }

    /// <summary>
    /// 查询字典数据详情
    /// </summary>
    /// <param name="sysDictDataId"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("Get/Data/{sysDictDataId}")]
    public async Task<ResultDto> GetDictDataById(long sysDictDataId)
    {
        var result = await _sysDictDataService.GetDictDataById(sysDictDataId);
        return ResultDto.Success(result);
    }

    /// <summary>
    /// 查询字典数据列表(根据字典类型)
    /// </summary>
    /// <param name="sysDictType"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("Get/Data/ByType/{sysDictType}")]
    public async Task<ResultDto> GetDictDataByType(string sysDictType)
    {
        var result = await _sysDictDataService.GetDictDataByType(sysDictType);
        return ResultDto.Success(result);
    }

    /// <summary>
    /// 查询字典数据列表(根据多个字典类型)
    /// </summary>
    /// <param name="sysDictTypes"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Get/Data/ByTypes")]
    public async Task<ResultDto> GetDictDataByTypes([FromBody] string[] sysDictTypes)
    {
        var result = await _sysDictDataService.GetDictDataByTypes(sysDictTypes);
        return ResultDto.Success(result);
    }

    /// <summary>
    /// 查询字典数据列表
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Get/Data/List")]
    public async Task<ResultDto> GetDictDataList([FromBody] PageWhereDto<SysDictDataWhereDto> pageWhere)
    {
        var result = await _sysDictDataService.GetDictDataList(pageWhere);
        return ResultDto.Success(result);
    }

    #endregion

    #region 导出

    /// <summary>
    /// 字典导出
    /// </summary>
    /// <returns></returns>
    [HttpGet("Export/Data")]
    [AppLog(Title = "字典导出", BusinessType = BusinessTypeEnum.Export, IsSaveRequestData = false)]
    public async Task<FileStreamResult> ExportDict()
    {
        var result = await _sysDictTypeService.GetAllDictType();
        var fileName = ExportExcel(result, "SysdictType", "字典类型");
        return ExportExcel(fileName.Item2, fileName.Item1);
        //return ResultDto.Success(new { path = "/Export/" + fileName, fileName = fileName.Item1 });
    }

    #endregion
}