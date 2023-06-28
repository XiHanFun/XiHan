#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseRepository
// Guid:90f7fb47-4210-4453-8208-34fddae801b4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// AddTime:2022-05-08 下午 09:35:56
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using SqlSugar.IOC;
using System.Linq.Expressions;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Repositories.Entities;

namespace XiHan.Repositories.Bases;

/// <summary>
/// 仓库基类
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <remarks>
/// 方法命名规范：
/// 新增 Add
/// 删除 Remove
/// 逻辑删除 SoftRemove
/// 修改 Update
/// 查找 Find
/// 查询 Query
/// </remarks>
public class BaseRepository<TEntity> : SimpleClient<TEntity>, IBaseRepository<TEntity> where TEntity : class, new()
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context"></param>
    protected BaseRepository(ISqlSugarClient? context = null) : base(context)
    {
        Context = DbScoped.SugarScope;
    }

    #region 新增

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<bool> AddAsync(TEntity entity)
    {
        entity.ToCreated();
        return await base.InsertAsync(entity);
    }

    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> AddAsync(TEntity[] entities)
    {
        entities.ToList().ForEach(entity => entity.ToCreated());
        return await base.InsertRangeAsync(entities);
    }

    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> AddAsync(List<TEntity> entities)
    {
        entities.ForEach(entity => entity.ToCreated());
        return await base.InsertRangeAsync(entities);
    }

    /// <summary>
    /// 新增返回Id
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<long> AddReturnIdAsync(TEntity entity)
    {
        entity.ToCreated();
        return await base.InsertReturnBigIdentityAsync(entity);
    }

    /// <summary>
    /// 新增或更新
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<bool> AddOrUpdateAsync(TEntity entity)
    {
        entity.ToCreated();
        entity.ToModified();
        return await base.InsertOrUpdateAsync(entity);
    }

    /// <summary>
    /// 批量新增或更新
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> AddOrUpdateAsync(TEntity[] entities)
    {
        var entityList = entities.ToList();
        entityList.ForEach(entity =>
        {
            entity.ToCreated();
            entity.ToModified();
        });
        return await base.InsertOrUpdateAsync(entityList);
    }

    /// <summary>
    /// 批量新增或更新
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> AddOrUpdateAsync(List<TEntity> entities)
    {
        entities.ForEach(entity =>
        {
            entity.ToCreated();
            entity.ToModified();
        });
        return await base.InsertOrUpdateAsync(entities);
    }

    #endregion

    #region 删除

    /// <summary>
    /// 根据Id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<bool> RemoveAsync(long id)
    {
        return await base.DeleteByIdAsync(id);
    }

    /// <summary>
    /// 根据Id批量删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public virtual async Task<bool> RemoveAsync(long[] ids)
    {
        dynamic[] newIds = ids.Select(x => x as dynamic).ToArray();
        return await base.DeleteByIdsAsync(newIds);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<bool> RemoveAsync(TEntity entity)
    {
        return await base.DeleteAsync(entity);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> RemoveAsync(List<TEntity> entities)
    {
        return await base.DeleteAsync(entities);
    }

    /// <summary>
    /// 自定义条件删除
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public virtual async Task<bool> RemoveAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return await base.DeleteAsync(whereExpression);
    }

    #endregion

    #region 逻辑删除

    /// <summary>
    /// 根据Id逻辑删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<bool> SoftRemoveAsync(long id)
    {
        var entity = await GetByIdAsync(id);
        entity.ToDeleted();
        return await base.UpdateAsync(entity);
    }

    /// <summary>
    /// 逻辑删除
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<bool> SoftRemoveAsync(TEntity entity)
    {
        entity.ToDeleted();
        return await base.UpdateAsync(entity);
    }

    /// <summary>
    /// 批量逻辑删除
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> SoftRemoveAsync(List<TEntity> entities)
    {
        entities.ForEach(entity => entity.ToDeleted());
        return await base.UpdateRangeAsync(entities);
    }

    /// <summary>
    /// 自定义条件逻辑删除
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public virtual async Task<bool> SoftRemoveAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        var entities = await GetListAsync(whereExpression);
        entities.ForEach(entity => entity.ToDeleted());
        return await base.UpdateRangeAsync(entities);
    }

    #endregion

    #region 修改

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual new async Task<bool> UpdateAsync(TEntity entity)
    {
        entity.ToModified();
        return await base.UpdateAsync(entity);
    }

    /// <summary>
    /// 修改某列
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public virtual new async Task<bool> UpdateAsync(Expression<Func<TEntity, TEntity>> columns,
        Expression<Func<TEntity, bool>> whereExpression)
    {
        return await base.UpdateAsync(columns, whereExpression);
    }

    /// <summary>
    /// 批量修改
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> UpdateAsync(TEntity[] entities)
    {
        entities.ToList().ForEach(entity => entity.ToModified());
        return await base.UpdateRangeAsync(entities);
    }

    /// <summary>
    /// 批量修改
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> UpdateAsync(List<TEntity> entities)
    {
        entities.ForEach(entity => entity.ToModified());
        return await base.UpdateRangeAsync(entities);
    }

    #endregion

    #region 查找查询

    /// <summary>
    /// 根据Id查找
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> FindAsync(long id)
    {
        return await base.GetByIdAsync(id);
    }

    /// <summary>
    /// 自定义条件查找
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <returns></returns>
    public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return await base.GetFirstAsync(whereExpression);
    }

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> QueryAllAsync()
    {
        return await base.GetListAsync();
    }

    /// <summary>
    /// 自定义条件查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return await base.GetListAsync(whereExpression);
    }

    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Context.Queryable<TEntity>().Where(expression).AnyAsync();
    }

    /// <summary>
    /// 自定义条件查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <param name="orderExpression">自定义排序条件</param>
    /// <param name="isOrderAsc">是否正序排序</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression,
        Expression<Func<TEntity, object>> orderExpression, bool isOrderAsc = true)
    {
        return await Context.Queryable<TEntity>().Where(whereExpression)
            .OrderBy(orderExpression, isOrderAsc ? OrderByType.Asc : OrderByType.Desc)
            .ToListAsync();
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="totalCount">查询到的总数</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> QueryAsync(int currentIndex, int pageSize, RefAsync<int> totalCount)
    {
        return await Context.Queryable<TEntity>()
            .ToPageListAsync(currentIndex, pageSize, totalCount);
    }

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="totalCount">查询到的总数</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression,
        int currentIndex, int pageSize, RefAsync<int> totalCount)
    {
        return await Context.Queryable<TEntity>().Where(whereExpression)
            .ToPageListAsync(currentIndex, pageSize, totalCount);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <returns></returns>
    public virtual async Task<PageDataDto<TEntity>> QueryPageAsync(int currentIndex, int pageSize)
    {
        return await Context.Queryable<TEntity>()
            .ToPageDataDto(currentIndex, pageSize);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="page">分页实体</param>
    /// <returns></returns>
    public virtual async Task<PageDataDto<TEntity>> QueryPageAsync(PageDto page)
    {
        return await Context.Queryable<TEntity>()
            .ToPageDataDto(page);
    }

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <returns></returns>
    public virtual async Task<PageDataDto<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> whereExpression,
        int currentIndex, int pageSize)
    {
        return await Context.Queryable<TEntity>().Where(whereExpression)
            .ToPageDataDto(currentIndex, pageSize);
    }

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <param name="page">分页实体</param>
    /// <returns></returns>
    public virtual async Task<PageDataDto<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> whereExpression,
        PageDto page)
    {
        return await Context.Queryable<TEntity>().Where(whereExpression)
            .ToPageDataDto(page);
    }

    /// <summary>
    /// 自定义条件分页排序查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="orderExpression">自定义排序条件</param>
    /// <param name="isOrderAsc">是否正序排序</param>
    /// <returns></returns>
    public virtual async Task<PageDataDto<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> whereExpression,
        int currentIndex, int pageSize, Expression<Func<TEntity, object>> orderExpression, bool isOrderAsc = true)
    {
        return await Context.Queryable<TEntity>().Where(whereExpression)
            .OrderBy(orderExpression, isOrderAsc ? OrderByType.Asc : OrderByType.Desc)
            .ToPageDataDto(currentIndex, pageSize);
    }

    /// <summary>
    /// 自定义条件分页排序查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <param name="page">分页实体</param>
    /// <param name="orderExpression">自定义排序条件</param>
    /// <param name="isOrderAsc">是否正序排序</param>
    /// <returns></returns>
    public virtual async Task<PageDataDto<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> whereExpression,
        PageDto page, Expression<Func<TEntity, object>> orderExpression, bool isOrderAsc = true)
    {
        return await Context.Queryable<TEntity>().Where(whereExpression)
            .OrderBy(orderExpression, isOrderAsc ? OrderByType.Asc : OrderByType.Desc)
            .ToPageDataDto(page);
    }

    #endregion
}