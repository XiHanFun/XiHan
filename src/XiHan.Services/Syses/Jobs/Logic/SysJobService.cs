#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysJobService
// Guid:52fd0cf5-e077-4447-b808-bc02e504124d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 1:20:53
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Exceptions;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Dicts.Dtos;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Jobs.Logic;

/// <summary>
/// 系统任务服务
/// </summary>
[AppService(ServiceType = typeof(ISysJobService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysJobService : BaseService<SysJob>, ISysJobService
{
    ///// <summary>
    ///// 新增字典项
    ///// </summary>
    ///// <param name="dictDataCDto"></param>
    ///// <returns></returns>
    //public async Task<long> CreateDictData(SysDictDataCDto dictDataCDto)
    //{
    //    var sysDictData = dictDataCDto.Adapt<SysDictData>();
    //    if (!await Context.Queryable<SysDictType>().AnyAsync(t => t.TypeCode == sysDictData.TypeCode && t.IsEnable))
    //        throw new CustomException($"该字典不存在!");

    //    _ = await CheckDictDataUnique(sysDictData);

    //    return await AddReturnIdAsync(sysDictData);
    //}

    ///// <summary>
    ///// 批量删除字典项
    ///// </summary>
    ///// <param name="dictIds"></param>
    ///// <returns></returns>
    //public async Task<bool> DeleteDictDataByIds(long[] dictIds)
    //{
    //    var dictDataList = await QueryAsync(d => dictIds.Contains(d.BaseId));
    //    return await RemoveAsync(dictDataList);
    //}

    ///// <summary>
    ///// 修改字典项
    ///// </summary>
    ///// <param name="dictDataCDto"></param>
    ///// <returns></returns>
    //public async Task<bool> ModifyDictData(SysDictDataCDto dictDataCDto)
    //{
    //    var sysDictData = dictDataCDto.Adapt<SysDictData>();

    //    _ = await CheckDictDataUnique(sysDictData);

    //    return await UpdateAsync(sysDictData);
    //}

    ///// <summary>
    ///// 查询字典项(根据Id)
    ///// </summary>
    ///// <param name="dictDataId"></param>
    ///// <returns></returns>
    //public async Task<SysDictData> GetDictDataById(long dictDataId)
    //{
    //    var key = $"GetDictDataById_{dictDataId}";
    //    if (_appCacheService.Get(key) is SysDictData sysDictData) return sysDictData;
    //    sysDictData = await FindAsync(d => d.BaseId == dictDataId && d.IsEnable);
    //    _appCacheService.SetWithMinutes(key, sysDictData, 30);

    //    return sysDictData;
    //}

    ///// <summary>
    ///// 查询字典项列表(根据分页条件)
    ///// </summary>
    ///// <param name="pageWhere"></param>
    ///// <returns></returns>
    //public async Task<PageDataDto<SysDictData>> GetDictDataPageList(PageWhereDto<SysDictDataWDto> pageWhere)
    //{
    //    var whereDto = pageWhere.Where;

    //    var whereExpression = Expressionable.Create<SysDictData>();
    //    whereExpression.AndIF(whereDto.TypeCode != null, u => u.TypeCode == whereDto.TypeCode);
    //    whereExpression.AndIF(whereDto.Value.IsNotEmptyOrNull(), u => u.Value == whereDto.Value);
    //    whereExpression.AndIF(whereDto.Label.IsNotEmptyOrNull(), u => u.Label.Contains(whereDto.Label!));
    //    whereExpression.AndIF(whereDto.IsDefault != null, u => u.IsDefault == whereDto.IsDefault);
    //    whereExpression.AndIF(whereDto.IsEnable != null, u => u.IsEnable == whereDto.IsEnable);

    //    return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.SortOrder);
    //}
}