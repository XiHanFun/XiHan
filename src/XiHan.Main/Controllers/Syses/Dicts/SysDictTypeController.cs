#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysDictTypeController
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
    /// <param name="sysDictTypeDto"></param>
    /// <returns></returns>
    [HttpPost("Create/Type")]
    [AppLog(Title = "字典类型", LogType = LogTypeEnum.Insert)]
    public async Task<BaseResultDto> CreateDictType([FromBody] CSysDictTypeDto sysDictTypeDto)
    {
        SysDictType sysDictType = sysDictTypeDto.Adapt<SysDictType>();
        sysDictType = sysDictType.ToCreated();
        var result = await _sysDictTypeService.CreateDictType(sysDictType);
        return BaseResultDto.Success(result);
    }

    /// <summary>
    /// 新增字典数据
    /// </summary>
    /// <param name="sysDictDataDto"></param>
    /// <returns></returns>
    [HttpPost("Create/Data")]
    [AppLog(Title = "字典数据", LogType = LogTypeEnum.Insert)]
    public async Task<BaseResultDto> CreateDictData([FromBody] CSysDictDataDto sysDictDataDto)
    {
        SysDictData sysDictData = sysDictDataDto.Adapt<SysDictData>();
        sysDictData = sysDictData.ToCreated();
        var result = await _sysDictDataService.CreateDictData(sysDictData);
        return BaseResultDto.Success(result);
    }

    /// <summary>
    /// 查询字典数据列表
    /// </summary>
    /// <param name="pageWhereDto"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Get/Data/List")]
    public async Task<BaseResultDto> GetDictDataList([FromBody] PageWhereDto<SysDictDataWhereDto> pageWhereDto)
    {
        var result = await _sysDictDataService.GetDictDataList(pageWhereDto);
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