#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysDictDataService
// Guid:3f7ca12f-9a86-4e07-b304-1a0f951a133c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-12 下午 04:32:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using SqlSugar;
using XiHan.Infrastructures.Apps.Caches;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Dicts.Dtos;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Dicts.Logic;

/// <summary>
/// 系统字典项服务
/// </summary>
[AppService(ServiceType = typeof(ISysDictDataService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysDictDataService : BaseService<SysDictData>, ISysDictDataService
{
    private readonly IAppCacheService _appCacheService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="appCacheService"></param>
    public SysDictDataService(IAppCacheService appCacheService)
    {
        _appCacheService = appCacheService;
    }

    /// <summary>
    /// 校验字典项值是否唯一
    /// </summary>
    /// <param name="sysDictData"></param>
    /// <returns></returns>
    private async Task<bool> CheckDictDataUnique(SysDictData sysDictData)
    {
        var isUnique = await IsAnyAsync(f => f.TypeCode == sysDictData.TypeCode && f.Value == sysDictData.Value);
        if (isUnique) throw new CustomException($"字典【{sysDictData.TypeCode}】已存在字典项【{sysDictData.Value}】!");
        return isUnique;
    }

    /// <summary>
    /// 新增字典项
    /// </summary>
    /// <param name="dictDataCDto"></param>
    /// <returns></returns>
    public async Task<long> CreateDictData(SysDictDataCDto dictDataCDto)
    {
        var sysDictData = dictDataCDto.Adapt<SysDictData>();

        if (!await Context.Queryable<SysDictType>().AnyAsync(t => t.Code == sysDictData.TypeCode && t.IsEnable))
            throw new CustomException($"该字典不存在或不可用!");

        _ = await CheckDictDataUnique(sysDictData);

        return await AddReturnIdAsync(sysDictData);
    }

    /// <summary>
    /// 批量删除字典项
    /// </summary>
    /// <param name="dictIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteDictDataByIds(long[] dictIds)
    {
        var dictDataList = await QueryAsync(d => dictIds.Contains(d.BaseId));
        return await RemoveAsync(dictDataList);
    }

    /// <summary>
    /// 修改字典项
    /// </summary>
    /// <param name="dictDataMDto"></param>
    /// <returns></returns>
    public async Task<bool> ModifyDictData(SysDictDataMDto dictDataMDto)
    {
        var sysDictData = dictDataMDto.Adapt<SysDictData>();

        if (!await Context.Queryable<SysDictType>().AnyAsync(t => t.Code == sysDictData.TypeCode && t.IsEnable))
            throw new CustomException($"该字典不存在或不可用!");

        _ = await CheckDictDataUnique(sysDictData);

        return await UpdateAsync(sysDictData);
    }

    /// <summary>
    /// 查询字典项(根据Id)
    /// </summary>
    /// <param name="dictDataId"></param>
    /// <returns></returns>
    public async Task<SysDictData> GetDictDataById(long dictDataId)
    {
        var key = $"GetDictDataById_{dictDataId}";
        if (_appCacheService.Get(key) is SysDictData sysDictData) return sysDictData;
        sysDictData = await FindAsync(d => d.BaseId == dictDataId && d.IsEnable);

        _appCacheService.SetWithMinutes(key, sysDictData, 30);
        return sysDictData;
    }

    /// <summary>
    /// 查询字典项列表(根据字典编码)
    /// </summary>
    /// <param name="dictCode"></param>
    /// <returns></returns>
    public async Task<List<SysDictData>> GetDictDataListByType(string dictCode)
    {
        var key = $"GetDictDataByType_{dictCode}";
        if (_appCacheService.Get(key) is List<SysDictData> list) return list;
        list = await Context.Queryable<SysDictType>()
            .LeftJoin<SysDictData>((t, d) => t.Code == d.TypeCode)
            .Where((t, d) => t.Code == dictCode && d.IsEnable)
            .Select((t, d) => d)
            .ToListAsync();
        list = list.OrderBy(d => d.SortOrder).ToList();

        _appCacheService.SetWithMinutes(key, list, 30);
        return list;
    }

    /// <summary>
    /// 查询字典项列表(根据多个字典编码)
    /// </summary>
    /// <param name="dictCodes"></param>
    /// <returns></returns>
    public async Task<List<SysDictData>> GetDictDataListByTypes(string[] dictCodes)
    {
        var key = $"GetDictDataByType_{dictCodes.GetArrayStr(',')}";
        if (_appCacheService.Get(key) is List<SysDictData> list) return list;
        list = await Context.Queryable<SysDictType>()
           .LeftJoin<SysDictData>((t, d) => t.Code == d.TypeCode)
           .Where((t, d) => dictCodes.Contains(t.Code) && d.IsEnable)
           .Select((t, d) => d)
           .ToListAsync();
        list = list.OrderBy(d => d.SortOrder).ToList();

        _appCacheService.SetWithMinutes(key, list, 30);
        return list;
    }

    /// <summary>
    /// 查询字典项列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysDictData>> GetDictDataPageList(PageWhereDto<SysDictDataWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        var whereExpression = Expressionable.Create<SysDictData>();
        whereExpression.AndIF(whereDto.TypeCode != null, u => u.TypeCode == whereDto.TypeCode);
        whereExpression.AndIF(whereDto.Label.IsNotEmptyOrNull(), u => u.Label.Contains(whereDto.Label!));
        whereExpression.AndIF(whereDto.IsDefault != null, u => u.IsDefault == whereDto.IsDefault);
        whereExpression.AndIF(whereDto.IsEnable != null, u => u.IsEnable == whereDto.IsEnable);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.SortOrder, pageWhere.IsAsc);
    }
}