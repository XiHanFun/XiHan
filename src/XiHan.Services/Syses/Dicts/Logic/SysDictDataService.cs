#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysDictDataService
// Guid:3f7ca12f-9a86-4e07-b304-1a0f951a133c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-06-12 下午 04:32:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Infrastructures.Apps.Caches;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Exceptions;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Dicts.Dtos;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Dicts.Logic;

/// <summary>
/// 系统字典数据服务
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
    /// 校验字典值是否唯一
    /// </summary>
    /// <param name="sysDictData">字典类型</param>
    /// <returns></returns>
    public async Task<bool> CheckDictValueUnique(SysDictData sysDictData)
    {
        var isUnique = true;
        if (await IsAnyAsync(f => f.Type == sysDictData.Type && f.Value == sysDictData.Value))
        {
            isUnique = false;
            throw new CustomException($"字典【{sysDictData.Type}】下已存在字典数据值【{sysDictData.Value}】!");
        }
        return isUnique;
    }

    /// <summary>
    /// 新增字典数据
    /// </summary>啊
    /// <param name="sysDictData"></param>
    /// <returns></returns>
    public async Task<long> CreateDictData(SysDictData sysDictData)
    {
        await CheckDictValueUnique(sysDictData);

        return await AddReturnIdAsync(sysDictData);
    }

    /// <summary>
    /// 批量删除字典数据
    /// </summary>
    /// <param name="dictIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteDictDataByIds(long[] dictIds)
    {
        return await RemoveAsync(dictIds);
    }

    /// <summary>
    /// 修改字典数据
    /// </summary>
    /// <param name="sysDictData"></param>
    /// <returns></returns>
    public async Task<bool> ModifyDictData(SysDictData sysDictData)
    {
        await CheckDictValueUnique(sysDictData);

        return await UpdateAsync(sysDictData);
    }

    /// <summary>
    /// 修改同步字典类型
    /// </summary>
    /// <param name="oldDictType">旧字典类型</param>
    /// <param name="newDictType">新字典类型</param>
    /// <returns></returns>
    public async Task<int> ModifyDictDataType(string oldDictType, string newDictType)
    {
        // 只更新 Type 字段
        return await Context.Updateable<SysDictData>()
            .SetColumns(t => new SysDictData() { Type = newDictType })
            .Where(f => f.Type == oldDictType)
            .ExecuteCommandAsync();
    }

    /// <summary>
    /// 查询字典数据(根据Id)
    /// </summary>
    /// <param name="dictId"></param>
    /// <returns></returns>
    public async Task<SysDictData> GetDictDataById(long dictId)
    {
        string key = $"GetDictDataById_{dictId}";
        if (_appCacheService.Get(key) is not SysDictData sysDictData)
        {
            sysDictData = await FindAsync(dictId);
            _appCacheService.SetWithMinutes(key, sysDictData, 30);
        }

        return sysDictData;
    }

    /// <summary>
    /// 查询字典数据列表(根据字典类型)
    /// </summary>
    /// <param name="dictType"></param>
    /// <returns></returns>
    public async Task<List<SysDictData>> GetDictDataByType(string dictType)
    {
        string key = $"GetDictDataByType_{dictType}";
        if (_appCacheService.Get(key) is not List<SysDictData> list)
        {
            list = await QueryAsync(f => f.IsEnable && f.Type == dictType, o => o.SortOrder);
            _appCacheService.SetWithMinutes(key, list, 30);
        }

        return list;
    }

    /// <summary>
    /// 查询字典数据列表(根据多个字典类型)
    /// </summary>
    /// <param name="dictTypes"></param>
    /// <returns></returns>
    public async Task<List<SysDictData>> GetDictDataByTypes(string[] dictTypes)
    {
        string key = $"GetDictDataByType_{dictTypes.GetArrayStr("_")}";
        if (_appCacheService.Get(key) is not List<SysDictData> list)
        {
            list = await QueryAsync(f => f.IsEnable && dictTypes.Contains(f.Type), o => o.SortOrder);
            _appCacheService.SetWithMinutes(key, list, 30);
        }
        return list;
    }

    /// <summary>
    /// 查询字典数据列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysDictData>> GetDictDataList(PageWhereDto<SysDictDataWhereDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        var whereExpression = Expressionable.Create<SysDictData>();
        whereExpression.AndIF(whereDto.Type.IsNotEmptyOrNull(), u => u.Type == whereDto.Type);
        whereExpression.AndIF(whereDto.Label.IsNotEmptyOrNull(), u => u.Label.Contains(whereDto.Label!));
        whereExpression.AndIF(whereDto.IsDefault != null, u => u.IsDefault == whereDto.IsDefault);
        whereExpression.AndIF(whereDto.IsEnable != null, u => u.IsEnable == whereDto.IsEnable);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.SortOrder);
    }
}