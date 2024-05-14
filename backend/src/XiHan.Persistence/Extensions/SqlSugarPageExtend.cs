#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SqlSugarPageExtend
// Guid:021d5434-d83d-4cfe-ac84-1355f8c5da7c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 8:50:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Communication.Https.Entities;

namespace XiHan.Persistence.Extensions;

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
    /// <param name="pageInfo">分页传入实体</param>
    /// <returns>分页后的List数据</returns>
    public static async Task<PageResponse<TEntity>> ToPageDataDto<TEntity>(this ISugarQueryable<TEntity> entities, PageInfo pageInfo)
        where TEntity : class, new()
    {
        List<TEntity> data = await entities.ToPageListAsync(pageInfo.CurrentIndex, pageInfo.PageSize);
        return new PageResponse<TEntity>(pageInfo, data);
    }
}