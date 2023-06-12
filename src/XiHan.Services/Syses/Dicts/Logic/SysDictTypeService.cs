#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysDictTypeService
// Guid:3f7ca12f-9a86-4e07-b304-1a0f951a133c
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-06-12 下午 04:32:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Dicts.Dtos;
using XiHan.Services.Syses.Users.Dtos;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Dicts.Logic;

/// <summary>
/// 系统字典类型服务
/// </summary>
[AppService(ServiceType = typeof(ISysDictTypeService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysDictTypeService : BaseService<SysDictType>, ISysDictTypeService
{
    private readonly ISysDictDataService _sysDictDataService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysDictDataService"></param>
    public SysDictTypeService(ISysDictDataService sysDictDataService)
    {
        _sysDictDataService = sysDictDataService;
    }

    /// <summary>
    /// 查询所有字典类型
    /// </summary>
    /// <returns></returns>
    public async Task<List<SysDictType>> GetAllDictType()
    {
        return await QueryListAsync();
    }

    /// <summary>
    /// 查询字典类型列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhereDto"></param>
    /// <returns></returns>
    public async Task<BasePageDataDto<SysDictType>> GetDictTypeList(PageWhereDto<SysDictTypeWhereDto> pageWhereDto)
    {
        var whereDto = pageWhereDto.Where;
        var exp = Expressionable.Create<SysDictType>();
        exp.AndIF(whereDto.Name.IsNotEmptyOrNull(), u => u.Name.Contains(whereDto.Name!));
        exp.AndIF(whereDto.Type.IsNotEmptyOrNull(), u => u.Type == whereDto.Type);
        exp.AndIF(whereDto.IsEnable != null, u => u.IsEnable == whereDto.IsEnable);
        exp.AndIF(whereDto.IsOfficial != null, u => u.IsOfficial == whereDto.IsOfficial);

        var result = await QueryPageDataDtoAsync(exp.ToExpression(), pageWhereDto.PageDto);

        return result;
    }
}