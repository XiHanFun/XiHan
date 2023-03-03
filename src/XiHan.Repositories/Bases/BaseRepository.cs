#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseRepository
// Guid:90f7fb47-4210-4453-8208-34fddae801b4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:35:56
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using SqlSugar.IOC;
using System.Linq.Expressions;
using XiHan.Infrastructure.Contexts.Pages;

namespace XiHan.Repositories.Bases;

/// <summary>
/// 仓库基类
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class BaseRepository<TEntity> : SimpleClient<TEntity>, IBaseRepository<TEntity> where TEntity : class, new()
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context"></param>
    public BaseRepository(ISqlSugarClient? context = null) : base(context)
    {
        base.Context = DbScoped.SugarScope;
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<bool> CreateAsync(TEntity entity)
    {
        return await base.InsertAsync(entity);
    }

    /// <summary>
    /// 新增返回Guid
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<Guid> CreateReturnGuidAsync(TEntity entity)
    {
        return Guid.Parse((await base.InsertReturnBigIdentityAsync(entity)).ToString());
    }

    /// <summary>
    /// 新增或更新
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<bool> CreateOrUpdateAsync(TEntity entity)
    {
        return await base.InsertOrUpdateAsync(entity);
    }

    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> CreateBatchAsync(TEntity[] entities)
    {
        return await base.InsertRangeAsync(entities);
    }

    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> CreateBatchAsync(List<TEntity> entities)
    {
        return await base.InsertRangeAsync(entities);
    }

    /// <summary>
    /// 批量新增或更新
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> CreateOrUpdateBatchAsync(List<TEntity> entities)
    {
        return await base.InsertOrUpdateAsync(entities);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public virtual async Task<bool> DeleteByGuidAsync(Guid guid)
    {
        return await base.DeleteByIdAsync(guid);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual new async Task<bool> DeleteAsync(TEntity entity)
    {
        return await base.DeleteAsync(entity);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="guids"></param>
    /// <returns></returns>
    public virtual async Task<bool> DeleteByGuidBatchAsync(Guid[] guids)
    {
        object[] newguids = guids.Select(x => x as dynamic).ToArray();
        return await base.DeleteByIdsAsync(newguids);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> DeleteBatchAsync(List<TEntity> entities)
    {
        return await base.DeleteAsync(entities);
    }

    /// <summary>
    /// 自定义条件删除
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public virtual new async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> func)
    {
        return await base.DeleteAsync(func);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual new async Task<bool> UpdateAsync(TEntity entity)
    {
        return await base.UpdateAsync(entity);
    }

    /// <summary>
    /// 批量修改
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> UpdateBatchAsync(TEntity[] entities)
    {
        return await base.UpdateRangeAsync(entities);
    }

    /// <summary>
    /// 批量修改
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> UpdateBatchAsync(List<TEntity> entities)
    {
        return await base.UpdateRangeAsync(entities);
    }

    /// <summary>
    /// Guid查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> FindByGuidAsync(Guid guid)
    {
        return await base.GetByIdAsync(guid);
    }

    /// <summary>
    /// 自定义条件查找
    /// </summary>
    /// <param name="func">自定义条件</param>
    /// <returns></returns>
    public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func)
    {
        return await base.GetFirstAsync(func);
    }

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> QueryListAsync()
    {
        return await base.GetListAsync();
    }

    /// <summary>
    /// 自定义条件查询
    /// </summary>
    /// <param name="func">自定义条件</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> func)
    {
        return await base.GetListAsync(func);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="totalCount">查询到的总数</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> QueryPageListAsync(int currentIndex, int pageSize, RefAsync<int> totalCount)
    {
        return await base.Context.Queryable<TEntity>().ToPageListAsync(currentIndex, pageSize, totalCount);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <returns></returns>
    public virtual async Task<BasePageDataDto<TEntity>> QueryPageDataDtoAsync(int currentIndex, int pageSize)
    {
        return await base.Context.Queryable<TEntity>().ToPageDataDto(currentIndex, pageSize);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageDto">分页传入实体</param>
    /// <returns></returns>
    public virtual async Task<BasePageDataDto<TEntity>> QueryPageDataDtoAsync(BasePageDto pageDto)
    {
        return await base.Context.Queryable<TEntity>().ToPageDataDto(pageDto.CurrentIndex, pageDto.PageSize);
    }

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="func">自定义条件</param>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="totalCount">查询到的总数</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> QueryPageListAsync(Expression<Func<TEntity, bool>> func, int currentIndex, int pageSize, RefAsync<int> totalCount)
    {
        return await base.Context.Queryable<TEntity>().Where(func).ToPageListAsync(currentIndex, pageSize, totalCount);
    }

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="func">自定义条件</param>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <returns></returns>
    public virtual async Task<BasePageDataDto<TEntity>> QueryPageDataDtoAsync(Expression<Func<TEntity, bool>> func, int currentIndex, int pageSize)
    {
        return await base.Context.Queryable<TEntity>().Where(func).ToPageDataDto(currentIndex, pageSize);
    }

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="func">自定义条件</param>
    /// <param name="pageDto">分页传入实体</param>
    /// <returns></returns>
    public virtual async Task<BasePageDataDto<TEntity>> QueryPageDataDtoAsync(Expression<Func<TEntity, bool>> func, BasePageDto pageDto)
    {
        return await base.Context.Queryable<TEntity>().Where(func).ToPageDataDto(pageDto.CurrentIndex, pageDto.PageSize);
    }
}