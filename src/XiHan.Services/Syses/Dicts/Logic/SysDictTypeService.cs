﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysDictTypeService
// Guid:3f7ca12f-9a86-4e07-b304-1a0f951a133c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-06-12 下午 04:32:05
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
    /// 校验字典类型是否唯一
    /// </summary>
    /// <param name="sysDictType">字典类型</param>
    /// <returns></returns>
    public async Task<bool> CheckDictTypeUnique(SysDictType sysDictType)
    {
        var isUnique = true;
        if (await IsAnyAsync(f => f.Type == sysDictType.Type))
        {
            isUnique = false;
            throw new CustomException($"已存在字典类型【{sysDictType.Type}】");
        }
        return isUnique;
    }

    /// <summary>
    /// 新增字典类型
    /// </summary>
    /// <param name="sysDictType"></param>
    /// <returns></returns>
    public async Task<long> CreateDictType(SysDictType sysDictType)
    {
        // 校验类型是否唯一
        if (await CheckDictTypeUnique(sysDictType))
        {
            return await AddReturnIdAsync(sysDictType);
        }
        else
        {
            throw new CustomException($"不可重复新增！");
        }
    }

    /// <summary>
    /// 批量删除字典类型
    /// </summary>
    /// <param name="dictIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteDictTypeByIds(long[] dictIds)
    {
        var sysDictTypeList = await QueryAsync(s => dictIds.Contains(s.BaseId));

        // 系统内置字典
        var isOfficialCount = sysDictTypeList.Where(s => s.IsOfficial).ToList().Count;
        if (isOfficialCount > 0) throw new CustomException($"存在系统内置字典，不能删除！");

        // 已分配字典
        var sysDictDataList = await _sysDictDataService.QueryAsync(f => sysDictTypeList.Select(s => s.Type).ToList().Contains(f.Type));
        if (sysDictDataList.Any())
            foreach (var sysDictData in sysDictDataList)
            {
                var sysDictType = sysDictTypeList.First(s => s.Type == sysDictData.Type);
                throw new CustomException($"字典【{sysDictType.Name}】已分配值【{sysDictData.Value}】,不能删除！");
            }

        return await RemoveAsync(s => dictIds.Contains(s.BaseId));
    }

    /// <summary>
    /// 修改字典类型
    /// </summary>
    /// <param name="sysDictType"></param>
    /// <returns></returns>
    public async Task<bool> ModifyDictType(SysDictType sysDictType)
    {
        var oldDictType = await FindAsync(x => x.BaseId == sysDictType.BaseId);

        if (sysDictType.Type != oldDictType.Type)
        {
            // 校验类型是否唯一
            if (await CheckDictTypeUnique(sysDictType))
            {
                // 同步修改 SysDictData 表里面的 Type 值
                await _sysDictDataService.ModifyDictDataType(oldDictType.Type, sysDictType.Type);
            }
            else
            {
                throw new CustomException($"已存在字典类型【{sysDictType.Type}】,不可修改为此类型！");
            }
        }
        return await UpdateAsync(sysDictType);
    }

    /// <summary>
    /// 查询字典类型
    /// </summary>
    /// <param name="dictId"></param>
    /// <returns></returns>
    public async Task<SysDictType> GetDictTypeById(long dictId)
    {
        return await FindAsync(f => f.BaseId == dictId);
    }

    /// <summary>
    /// 查询所有字典类型
    /// </summary>
    /// <returns></returns>
    public async Task<List<SysDictType>> GetAllDictType()
    {
        return await QueryAllAsync();
    }

    /// <summary>
    /// 查询字典类型列表
    /// </summary>
    /// <param name="sysDictTypeWhere"></param>
    /// <returns></returns>
    public async Task<List<SysDictType>> GetDictTypeList(SysDictTypeWhereDto sysDictTypeWhere)
    {
        var whereExpression = Expressionable.Create<SysDictType>();
        whereExpression.AndIF(sysDictTypeWhere.Name.IsNotEmptyOrNull(), u => u.Name.Contains(sysDictTypeWhere.Name!));
        whereExpression.AndIF(sysDictTypeWhere.Type.IsNotEmptyOrNull(), u => u.Type == sysDictTypeWhere.Type);
        whereExpression.AndIF(sysDictTypeWhere.IsEnable != null, u => u.IsEnable == sysDictTypeWhere.IsEnable);
        whereExpression.AndIF(sysDictTypeWhere.IsOfficial != null, u => u.IsOfficial == sysDictTypeWhere.IsOfficial);

        return await QueryAsync(whereExpression.ToExpression(), o => o.Type);
    }

    /// <summary>
    /// 查询字典类型列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysDictType>> GetDictTypeList(PageWhereDto<SysDictTypeWhereDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        var whereExpression = Expressionable.Create<SysDictType>();
        whereExpression.AndIF(whereDto.Name.IsNotEmptyOrNull(), u => u.Name.Contains(whereDto.Name!));
        whereExpression.AndIF(whereDto.Type.IsNotEmptyOrNull(), u => u.Type == whereDto.Type);
        whereExpression.AndIF(whereDto.IsEnable != null, u => u.IsEnable == whereDto.IsEnable);
        whereExpression.AndIF(whereDto.IsOfficial != null, u => u.IsOfficial == whereDto.IsOfficial);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.Type);
    }
}