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
    Task<bool> CreateAsync(TEntity entity);

    Task<bool> DeleteAsync(Guid guid);

    Task<bool> DeleteBatchAsync(Guid[] guids);

    Task<bool> UpdateAsync(TEntity entity);

    Task<TEntity> FindAsync(Guid? guid);

    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func);

    Task<List<TEntity>> QueryAsync();

    Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func);

    Task<List<TEntity>> QueryAsync(int pageIndex, int pageSize, RefAsync<int> totalCount);

    Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int pageIndex, int pageSize, RefAsync<int> totalCount);
}