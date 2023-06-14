#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ISysDictDataService
// Guid:101a7507-9827-4fd3-aa83-7328354c3b9a
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-06-12 下午 04:31:49
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Dicts.Dtos;

namespace XiHan.Services.Syses.Dicts;

/// <summary>
/// ISysDictDataService
/// </summary>
public interface ISysDictDataService : IBaseService<SysDictData>
{
    /// <summary>
    /// 新增字典数据
    /// </summary>
    /// <param name="sysDictData"></param>
    /// <returns></returns>
    Task<long> CreateDictData(SysDictData sysDictData);

    /// <summary>
    /// 批量删除字典数据
    /// </summary>
    /// <param name="dictIds"></param>
    /// <returns></returns>
    Task<bool> DeleteDictDataByIds(long[] dictIds);

    /// <summary>
    /// 修改字典数据
    /// </summary>
    /// <param name="sysDictData"></param>
    /// <returns></returns>
    Task<bool> ModifyDictData(SysDictData sysDictData);

    /// <summary>
    /// 修改同步字典类型
    /// </summary>
    /// <param name="old_dictType">旧字典类型</param>
    /// <param name="new_dictType">新字典类型</param>
    /// <returns></returns>
    Task<int> ModifyDictDataType(string old_dictType, string new_dictType);

    /// <summary>
    /// 查询字典数据(根据Id)
    /// </summary>
    /// <param name="dictId"></param>
    /// <returns></returns>
    Task<SysDictData> GetDictDataById(long dictId);

    /// <summary>
    /// 查询字典数据列表(根据字典类型)
    /// </summary>
    /// <param name="dictType"></param>
    /// <returns></returns>
    Task<List<SysDictData>> GetDictDataByType(string dictType);

    /// <summary>
    /// 查询字典数据列表(根据多个字典类型)
    /// </summary>
    /// <param name="dictTypes"></param>
    /// <returns></returns>
    Task<List<SysDictData>> GetDictDataByTypes(string[] dictTypes);

    /// <summary>
    /// 查询字典数据列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysDictData>> GetDictDataList(PageWhereDto<SysDictDataWhereDto> pageWhere);
}