#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SqlSugarPageExtend
// Guid:7df68cfd-b7e5-4669-87cc-e262241a8631
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/5 1:36:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Infrastructures.Responses.Pages;

namespace XiHan.Repositories.Extensions;

/// <summary>
/// SqlSugar分页拓展
/// </summary>
public static class SqlSugarPageExtend
{
    /// <summary>
    /// 处理IQueryable数据后分页数据(还未在数据库中查询)
    /// 推荐针对部分列的增改
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="currentIndex">当前页标</param>
    /// <param name="pageSize">每页大小</param>
    /// <returns>分页后的List数据</returns>
    public static async Task<PageDataDto<TEntity>> ToPageDataDto<TEntity>(this ISugarQueryable<TEntity> entities,
        int currentIndex, int pageSize) where TEntity : class, new()
    {
        RefAsync<int> totalCount = 0;
        List<TEntity> data = await entities.ToPageListAsync(currentIndex, pageSize, totalCount);
        PageDataDto<TEntity> pageDataDto = new()
        {
            PageInfo = new PageInfoDto(currentIndex, pageSize, totalCount),
            Data = data
        };
        return pageDataDto;
    }

    /// <summary>
    /// 处理IQueryable数据后分页数据(还未在数据库中查询)
    /// 推荐针对部分列的增改
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="pageDto">分页传入实体</param>
    /// <returns>分页后的List数据</returns>
    public static async Task<PageDataDto<TEntity>> ToPageDataDto<TEntity>(this ISugarQueryable<TEntity> entities,
        PageDto pageDto) where TEntity : class, new()
    {
        RefAsync<int> totalCount = 0;
        List<TEntity> data = await entities.ToPageListAsync(pageDto.CurrentIndex, pageDto.PageSize, totalCount);
        PageDataDto<TEntity> pageDataDto = new()
        {
            PageInfo = new PageInfoDto(pageDto.CurrentIndex, pageDto.PageSize, totalCount),
            Data = data
        };
        return pageDataDto;
    }
}