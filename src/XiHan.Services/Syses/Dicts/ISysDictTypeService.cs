#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysDictTypeService
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
/// ISysDictTypeService
/// </summary>
public interface ISysDictTypeService : IBaseService<SysDictType>
{
    /// <summary>
    /// 新增字典
    /// </summary>
    /// <param name="dictTypeCDto"></param>
    /// <returns></returns>
    Task<long> CreateDictType(SysDictTypeCDto dictTypeCDto);

    /// <summary>
    /// 批量删除字典
    /// </summary>
    /// <param name="dictIds"></param>
    /// <returns></returns>
    Task<bool> DeleteDictTypeByIds(long[] dictIds);

    /// <summary>
    /// 修改字典
    /// </summary>
    /// <param name="dictTypeMDto"></param>
    /// <returns></returns>
    Task<bool> ModifyDictType(SysDictTypeMDto dictTypeMDto);

    /// <summary>
    /// 查询字典
    /// </summary>
    /// <param name="dictId"></param>
    /// <returns></returns>
    Task<SysDictType> GetDictTypeById(long dictId);

    /// <summary>
    /// 查询字典列表(所有)
    /// </summary>
    /// <returns></returns>
    Task<List<SysDictType>> GetDictTypeList();

    /// <summary>
    /// 查询字典列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    Task<List<SysDictType>> GetDictTypeList(SysDictTypeWDto whereDto);

    /// <summary>
    /// 查询字典列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysDictType>> GetDictTypePageList(PageWhereDto<SysDictTypeWDto> pageWhere);
}