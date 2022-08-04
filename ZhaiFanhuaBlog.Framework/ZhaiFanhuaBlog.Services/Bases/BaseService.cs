// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseService
// Guid:26bf5f09-21b1-40cf-9bb7-25402f70baf2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 10:19:56
// ----------------------------------------------------------------

using SqlSugar;
using System.Linq.Expressions;
using ZhaiFanhuaBlog.IRepositories.Bases;
using ZhaiFanhuaBlog.IServices.Bases;

namespace ZhaiFanhuaBlog.Services.Bases;

/// <summary>
/// 服务基类
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
{
    //通过在子类的构造函数中注入
    protected IBaseRepository<TEntity>? _IBaseRepository;

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<bool> CreateAsync(TEntity entity)
    {
        return await _IBaseRepository!.CreateAsync(entity);
    }

    /// <summary>
    /// 新增返回Guid
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<Guid> CreateReturnGuidAsync(TEntity entity)
    {
        return await _IBaseRepository!.CreateReturnGuidAsync(entity);
    }

    /// <summary>
    /// 新增或更新
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<bool> CreateOrUpdateAsync(TEntity entity)
    {
        return await _IBaseRepository!.CreateOrUpdateAsync(entity);
    }

    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> CreateBatchAsync(TEntity[] entities)
    {
        return await _IBaseRepository!.CreateBatchAsync(entities);
    }

    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> CreateBatchAsync(List<TEntity> entities)
    {
        return await _IBaseRepository!.CreateBatchAsync(entities);
    }

    /// <summary>
    /// 批量新增或更新
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> CreateOrUpdateBatchAsync(List<TEntity> entities)
    {
        return await _IBaseRepository!.CreateOrUpdateBatchAsync(entities);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public virtual async Task<bool> DeleteAsync(Guid guid)
    {
        return await _IBaseRepository!.DeleteAsync(guid);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<bool> DeleteAsync(TEntity entity)
    {
        return await _IBaseRepository!.DeleteAsync(entity);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="guids"></param>
    /// <returns></returns>
    public virtual async Task<bool> DeleteBatchAsync(Guid[] guids)
    {
        return await _IBaseRepository!.DeleteBatchAsync(guids);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> DeleteBatchAsync(List<TEntity> entities)
    {
        return await _IBaseRepository!.DeleteBatchAsync(entities);
    }

    /// <summary>
    /// 自定义条件删除
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> func)
    {
        return await _IBaseRepository!.DeleteAsync(func);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<bool> UpdateAsync(TEntity entity)
    {
        return await _IBaseRepository!.UpdateAsync(entity);
    }

    /// <summary>
    /// 批量修改
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> UpdateBatchAsync(TEntity[] entities)
    {
        return await _IBaseRepository!.UpdateBatchAsync(entities);
    }

    /// <summary>
    /// 批量修改
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<bool> UpdateBatchAsync(List<TEntity> entities)
    {
        return await _IBaseRepository!.UpdateBatchAsync(entities);
    }

    /// <summary>
    /// Guid查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> FindAsync(Guid? guid)
    {
        return await _IBaseRepository!.FindAsync(guid);
    }

    /// <summary>
    /// 自定义条件查找
    /// </summary>
    /// <param name="func">自定义条件</param>
    /// <returns></returns>
    public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func)
    {
        return await _IBaseRepository!.FindAsync(func);
    }

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> QueryListAsync()
    {
        return await _IBaseRepository!.QueryListAsync();
    }

    /// <summary>
    /// 自定义条件查询
    /// </summary>
    /// <param name="func">自定义条件</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> func)
    {
        return await _IBaseRepository!.QueryListAsync(func);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="totalCount">查询到的总数</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> QueryPageListAsync(int pageIndex, int pageSize, RefAsync<int> totalCount)
    {
        return await _IBaseRepository!.QueryPageListAsync(pageIndex, pageSize, totalCount);
    }

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="func">自定义条件</param>
    /// <param name="pageIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="totalCount">查询到的总数</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> QueryPageListAsync(Expression<Func<TEntity, bool>> func, int pageIndex, int pageSize, RefAsync<int> totalCount)
    {
        return await _IBaseRepository!.QueryPageListAsync(func, pageIndex, pageSize, totalCount);
    }
}