#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BasePageDto
// Guid:a345ade2-5c23-474d-b6b5-ea29490d57b0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-02 上午 01:03:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace ZhaiFanhuaBlog.ViewModels.Bases.Pages;

/// <summary>
/// 分页拓展
/// </summary>
public static class PageExpand
{
    /// <summary>
    /// 获取List的分页后的数据
    /// </summary>
    /// <typeparam name="TEntity">数据源类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="currentIndex">当前页标</param>
    /// <param name="pageSize">每页大小</param>
    /// <param name="defaultFirstInex">默认起始下标</param>
    /// <returns>分页后的List数据</returns>
    public static List<TEntity> ToPageList<TEntity>(this IList<TEntity> entities, int currentIndex, int pageSize, int defaultFirstInex = 1) where TEntity : class, new()
    {
        return entities.Skip((currentIndex - defaultFirstInex) * pageSize).Take(pageSize).ToList();
    }

    /// <summary>
    /// 获取List分页后的数据
    /// </summary>
    /// <typeparam name="TEntity">数据源类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="pageDto">分页传入实体</param>
    /// <param name="defaultFirstInex">默认起始下标</param>
    /// <returns>分页后的List数据</returns>
    public static List<TEntity> ToPageList<TEntity>(this IList<TEntity> entities, BasePageDto pageDto, int defaultFirstInex = 1) where TEntity : class, new()
    {
        return entities.Skip((pageDto.CurrentIndex - defaultFirstInex) * pageDto.PageSize).Take(pageDto.PageSize).ToList();
    }

    /// <summary>
    /// IQueryable数据进行分页（IQueryable：还未在数据库中查询）
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="currentIndex">当前页标</param>
    /// <param name="pageSize">每页大小</param>
    /// <param name="defaultFirstInex">默认起始下标</param>
    /// <returns>分页后的List数据</returns>
    public static List<TEntity> ToPageList<TEntity>(this IQueryable<TEntity> entities, int currentIndex, int pageSize, int defaultFirstInex = 1) where TEntity : class, new()
    {
        return entities.Skip((currentIndex - defaultFirstInex) * pageSize).Take(pageSize).ToList();
    }

    /// <summary>
    /// IQueryable数据进行分页（IQueryable：还未在数据库中查询）
    /// </summary>
    /// <typeparam name="TEntity">数据源类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="pageDto">分页传入实体</param>
    /// <param name="defaultFirstInex">默认起始下标</param>
    public static List<TEntity> ToPageList<TEntity>(this IQueryable<TEntity> entities, BasePageDto pageDto, int defaultFirstInex = 1) where TEntity : class, new()
    {
        return entities.Skip((pageDto.CurrentIndex - defaultFirstInex) * pageDto.PageSize).Take(pageDto.PageSize).ToList();
    }

    /// <summary>
    /// 处理IQueryable数据后分页数据，无Datas数据（IQueryable：还未在数据库中查询）
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="currentIndex">当前页标</param>
    /// <param name="pageSize">每页大小</param>
    /// <returns>分页后的List数据</returns>
    public static PageDataDto<TEntity> ToPageDto<TEntity>(this IQueryable<TEntity> entities, int currentIndex, int pageSize) where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            Page = new PageDto(currentIndex, pageSize, entities.Count()),
        };
        return pageDataDto;
    }

    /// <summary>
    /// 处理IQueryable数据后分页数据，无Datas数据（IQueryable：还未在数据库中查询）
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="pageDto">分页传入实体</param>
    /// <returns>分页后的List数据</returns>
    public static PageDataDto<TEntity> ToPageDto<TEntity>(this IQueryable<TEntity> entities, BasePageDto pageDto) where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            Page = new PageDto(pageDto.CurrentIndex, pageDto.PageSize, entities.Count()),
        };
        return pageDataDto;
    }

    /// <summary>
    /// 获取Dto数据
    /// </summary>
    /// <typeparam name="TEntity">数据源类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="currentIndex">当前页标</param>
    /// <param name="pageSize">每页大小</param>
    /// <param name="defaultFirstInex">默认起始下标</param>
    /// <returns>分页后的Dto结果</returns>
    public static PageDataDto<TEntity> ToPageDataDto<TEntity>(this IList<TEntity> entities, int currentIndex, int pageSize, int defaultFirstInex = 1) where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            Page = new PageDto(currentIndex, pageSize, entities.Count),
            Datas = entities.ToPageList(currentIndex, pageSize, defaultFirstInex)
        };
        return pageDataDto;
    }

    /// <summary>
    /// 获取Dto数据
    /// </summary>
    /// <typeparam name="TEntity">数据源类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="pageDto">分页传入实体</param>
    /// <param name="defaultFirstInex">默认起始下标</param>
    /// <returns>分页后的Dto结果</returns>
    public static PageDataDto<TEntity> ToPageDataDto<TEntity>(this IList<TEntity> entities, BasePageDto pageDto, int defaultFirstInex = 1) where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            Page = new PageDto(pageDto.CurrentIndex, pageDto.PageSize, entities.Count),
            Datas = entities.ToPageList(pageDto, defaultFirstInex)
        };
        return pageDataDto;
    }

    /// <summary>
    /// 处理IQueryable数据后分页数据（IQueryable：还未在数据库中查询）
    /// 推荐针对部分列的增改
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="currentIndex">当前页标</param>
    /// <param name="pageSize">每页大小</param>
    /// <param name="defaultFirstInex">默认起始下标</param>
    /// <returns>分页后的List数据</returns>
    public static PageDataDto<TEntity> ToPageDataDto<TEntity>(this IQueryable<TEntity> entities, int currentIndex, int pageSize, int defaultFirstInex = 1) where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            Page = new PageDto(currentIndex, pageSize, entities.Count()),
            Datas = entities.ToPageList(currentIndex, pageSize, defaultFirstInex)
        };
        return pageDataDto;
    }

    /// <summary>
    /// 处理IQueryable数据后分页数据（IQueryable：还未在数据库中查询）
    /// 推荐针对部分列的增改
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="pageDto">分页传入实体</param>
    /// <param name="defaultFirstInex">默认起始下标</param>
    /// <returns>分页后的List数据</returns>
    public static PageDataDto<TEntity> ToPageDataDto<TEntity>(this IQueryable<TEntity> entities, BasePageDto pageDto, int defaultFirstInex = 1) where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            Page = new PageDto(pageDto.CurrentIndex, pageDto.PageSize, entities.Count()),
            Datas = entities.ToPageList(pageDto, defaultFirstInex)
        };
        return pageDataDto;
    }

    /// <summary>
    /// 获取全部信息，该信息被分页器包裹 [IList]
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <returns>分页后的所有数据</returns>
    public static PageDataDto<TEntity> ToAllPageDataDto<TEntity>(this IList<TEntity> entities) where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            Page = new PageDto(),
            Datas = entities.ToList()
        };
        pageDataDto.Page.CurrentIndex = 1;
        pageDataDto.Page.TotalCount = pageDataDto.Datas.Count;
        pageDataDto.Page.PageSize = pageDataDto.Page.TotalCount;
        pageDataDto.Page.PageCount = 1;
        return pageDataDto;
    }

    /// <summary>
    /// 获取全部信息，该信息被分页器包裹 [IQueryable]
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <returns>分页后的所有数据</returns>
    public static PageDataDto<TEntity> ToAllPageDataDto<TEntity>(this IQueryable<TEntity> entities) where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            Page = new PageDto(),
            Datas = entities.ToList()
        };
        pageDataDto.Page.CurrentIndex = 1;
        pageDataDto.Page.TotalCount = pageDataDto.Datas.Count;
        pageDataDto.Page.PageSize = pageDataDto.Page.TotalCount;
        pageDataDto.Page.PageCount = 1;
        return pageDataDto;
    }

    #region SqlSugar拓展

    /// <summary>
    /// 处理IQueryable数据后分页数据（IQueryable：还未在数据库中查询）
    /// 推荐针对部分列的增改
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="currentIndex">当前页标</param>
    /// <param name="pageSize">每页大小</param>
    /// <returns>分页后的List数据</returns>
    public static async Task<PageDataDto<TEntity>> ToPageDataDto<TEntity>(this ISugarQueryable<TEntity> entities, int currentIndex, int pageSize) where TEntity : class, new()
    {
        RefAsync<int> totalCount = 0;
        var datas = await entities.ToPageListAsync(currentIndex, pageSize, totalCount);
        PageDataDto<TEntity> pageDataDto = new()
        {
            Page = new PageDto(currentIndex, pageSize, totalCount),
            Datas = datas,
        };
        return pageDataDto;
    }

    /// <summary>
    /// 处理IQueryable数据后分页数据（IQueryable：还未在数据库中查询）
    /// 推荐针对部分列的增改
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="pageDto">分页传入实体</param>
    /// <returns>分页后的List数据</returns>
    public static async Task<PageDataDto<TEntity>> ToPageDataDto<TEntity>(this ISugarQueryable<TEntity> entities, BasePageDto pageDto) where TEntity : class, new()
    {
        RefAsync<int> totalCount = 0;
        var datas = await entities.ToPageListAsync(pageDto.CurrentIndex, pageDto.PageSize, totalCount);
        PageDataDto<TEntity> pageDataDto = new()
        {
            Page = new PageDto(pageDto.CurrentIndex, pageDto.PageSize, totalCount),
            Datas = datas,
        };
        return pageDataDto;
    }

    #endregion SqlSugar拓展
}