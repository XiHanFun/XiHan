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
using XiHan.Infrastructures.Enums;
using XiHan.Infrastructures.Extensions;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Main.Controllers.Bases;
using XiHan.Main.Controllers.Syses.Dicts.Dtos;
using XiHan.Models.Syses;
using XiHan.Services.Syses.Dicts;
using XiHan.Services.Syses.Dicts.Dtos;
using XiHan.Web.Common.Swagger;

namespace XiHan.Main.Controllers.Syses.Dicts;

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

    /// <summary>
    /// 新增字典类型
    /// </summary>
    /// <param name="cSysDictType"></param>
    /// <returns></returns>
    [HttpPost("Create/Type")]
    [AppLog(Title = "新增字典类型", BusinessType = BusinessTypeEnum.Insert)]
    public async Task<BaseResultDto> CreateDictType([FromBody] CSysDictTypeDto cSysDictType)
    {
        SysDictType sysDictType = cSysDictType.Adapt<SysDictType>();
        var result = await _sysDictTypeService.CreateDictType(sysDictType);
        return BaseResultDto.Success(result);
    }

    /// <summary>
    /// 新增字典数据
    /// </summary>
    /// <param name="cSysDictData"></param>
    /// <returns></returns>
    [HttpPost("Create/Data")]
    [AppLog(Title = "新增字典数据", BusinessType = BusinessTypeEnum.Insert)]
    public async Task<BaseResultDto> CreateDictData([FromBody] CSysDictDataDto cSysDictData)
    {
        SysDictData sysDictData = cSysDictData.Adapt<SysDictData>();
        var result = await _sysDictDataService.CreateDictData(sysDictData);
        return BaseResultDto.Success(result);
    }

    /// <summary>
    /// 删除字典类型
    /// </summary>
    /// <returns></returns>
    [HttpDelete("Delete/Type")]
    [AppLog(Title = "删除字典类型", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<BaseResultDto> DeleteDictType([FromBody] long[] ids)
    {
        var result = await _sysDictTypeService.DeleteDictTypeByIds(ids);
        return BaseResultDto.Success(result);
    }

    /// <summary>
    /// 查询字典数据列表
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Get/Data/List")]
    public async Task<BaseResultDto> GetDictDataList([FromBody] PageWhereDto<SysDictDataWhereDto> pageWhere)
    {
        var result = await _sysDictDataService.GetDictDataList(pageWhere);
        return BaseResultDto.Success(result);
    }

    /// <summary>
    /// 查询字典数据列表(根据字典类型)
    /// </summary>
    /// <param name="sysDictType"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Get/Data/ByType")]
    public async Task<BaseResultDto> GetDictDataByType([FromBody] string sysDictType)
    {
        var result = await _sysDictDataService.GetDictDataByType(sysDictType);
        return BaseResultDto.Success(result);
    }

    /// <summary>
    /// 查询字典数据列表(根据多个字典类型)
    /// </summary>
    /// <param name="sysDictTypes"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Get/Data/ByTypes")]
    public async Task<BaseResultDto> GetDictDataByTypes([FromBody] string[] sysDictTypes)
    {
        var result = await _sysDictDataService.GetDictDataByTypes(sysDictTypes);
        return BaseResultDto.Success(result);
    }
}