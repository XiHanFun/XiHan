#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysDictDataService
// Guid:101a7507-9827-4fd3-aa83-7328354c3b9a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-12 下午 04:31:49
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
    /// 新增字典项
    /// </summary>
    /// <param name="dictDataCDto"></param>
    /// <returns></returns>
    Task<long> CreateDictData(SysDictDataCDto dictDataCDto);

    /// <summary>
    /// 批量删除字典项
    /// </summary>
    /// <param name="dictIds"></param>
    /// <returns></returns>
    Task<bool> DeleteDictDataByIds(long[] dictIds);

    /// <summary>
    /// 修改字典数项
    /// </summary>
    /// <param name="dictDataCDto"></param>
    /// <returns></returns>
    Task<bool> ModifyDictData(SysDictDataCDto dictDataCDto);

    /// <summary>
    /// 查询字典项(根据Id)
    /// </summary>
    /// <param name="dictId"></param>
    /// <returns></returns>
    Task<SysDictData> GetDictDataById(long dictId);

    /// <summary>
    /// 查询字典项列表(根据字典编码)
    /// </summary>
    /// <param name="dictCode"></param>
    /// <returns></returns>
    Task<List<SysDictData>> GetDictDataListByType(string dictCode);

    /// <summary>
    /// 查询字典项列表(根据多个字典编码)
    /// </summary>
    /// <param name="dictCodes"></param>
    /// <returns></returns>
    Task<List<SysDictData>> GetDictDataListByTypes(string[] dictCodes);

    /// <summary>
    /// 查询字典项列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysDictData>> GetDictDataPageList(PageWhereDto<SysDictDataWDto> pageWhere);
}