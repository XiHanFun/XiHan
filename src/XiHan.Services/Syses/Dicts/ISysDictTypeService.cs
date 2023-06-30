#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ISysDictTypeService
// Guid:101a7507-9827-4fd3-aa83-7328354c3b9a
// Author:zhaifanhua
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
/// ISysDictTypeService
/// </summary>
public interface ISysDictTypeService : IBaseService<SysDictType>
{
    /// <summary>
    /// 校验字典类型是否唯一
    /// </summary>
    /// <param name="sysDictType">字典类型</param>
    /// <returns></returns>
    Task<bool> CheckDictTypeUnique(SysDictType sysDictType);

    /// <summary>
    /// 新增字典类型
    /// </summary>
    /// <param name="sysDictType"></param>
    /// <returns></returns>
    Task<long> CreateDictType(SysDictType sysDictType);

    /// <summary>
    /// 批量删除字典类型
    /// </summary>
    /// <param name="dictIds"></param>
    /// <returns></returns>
    Task<bool> DeleteDictTypeByIds(long[] dictIds);

    /// <summary>
    /// 修改字典类型
    /// </summary>
    /// <param name="sysDictType"></param>
    /// <returns></returns>
    Task<bool> ModifyDictType(SysDictType sysDictType);

    /// <summary>
    /// 查询字典类型
    /// </summary>
    /// <param name="dictId"></param>
    /// <returns></returns>
    Task<SysDictType> GetDictTypeById(long dictId);

    /// <summary>
    /// 查询所有字典类型
    /// </summary>
    /// <returns></returns>
    Task<List<SysDictType>> GetAllDictType();

    /// <summary>
    /// 查询字典类型列表
    /// </summary>
    /// <param name="sysDictTypeWhere"></param>
    /// <returns></returns>
    Task<List<SysDictType>> GetDictTypeList(SysDictTypeWhereDto sysDictTypeWhere);

    /// <summary>
    /// 查询字典类型列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysDictType>> GetDictTypeList(PageWhereDto<SysDictTypeWhereDto> pageWhere);
}