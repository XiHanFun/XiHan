// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBaseRepository
// Guid:7850231b-660e-4660-8747-5da8c607c53c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:04:18
// ----------------------------------------------------------------

using SqlSugar;
using System.Linq.Expressions;

namespace ZhaiFanhuaBlog.IRepositories.Bases;

/// <summary>
/// 仓储基类接口
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IBaseRepository<TEntity> where TEntity : class, new()
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> CreateAsync(TEntity entity);

    /// <summary>
    /// 新增返回Guid
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<Guid> CreateReturnGuidAsync(TEntity entity);

    /// <summary>
    /// 新增或更新
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> CreateOrUpdateAsync(TEntity entity);

    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> CreateBatchAsync(TEntity[] entities);

    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> CreateBatchAsync(List<TEntity> entities);

    /// <summary>
    /// 批量新增或更新
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> CreateOrUpdateBatchAsync(List<TEntity> entities);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(Guid guid);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(TEntity entity);

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="guids"></param>
    /// <returns></returns>
    Task<bool> DeleteBatchAsync(Guid[] guids);

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> DeleteBatchAsync(List<TEntity> entities);

    /// <summary>
    /// 条件删除
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> func);

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(TEntity entity);

    /// <summary>
    /// 批量更新
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> UpdateBatchAsync(TEntity[] entities);

    /// <summary>
    /// 批量更新
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> UpdateBatchAsync(List<TEntity> entities);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<TEntity> FindAsync(Guid? guid);

    /// <summary>
    /// 条件查找
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> QueryListAsync();

    /// <summary>
    /// 条件查询
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> func);

    /// <summary>
    /// 按页查询
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalCount"></param>
    /// <returns></returns>
    Task<List<TEntity>> QueryPageListAsync(int pageIndex, int pageSize, RefAsync<int> totalCount);

    /// <summary>
    /// 按页条件查询
    /// </summary>
    /// <param name="func"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalCount"></param>
    /// <returns></returns>
    Task<List<TEntity>> QueryPageListAsync(Expression<Func<TEntity, bool>> func, int pageIndex, int pageSize, RefAsync<int> totalCount);
}