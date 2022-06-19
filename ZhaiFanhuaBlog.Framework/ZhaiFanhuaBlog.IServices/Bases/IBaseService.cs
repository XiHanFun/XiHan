// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBaseService
// Guid:368c93d7-dc11-4f23-a16d-c6bef363e3e0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 10:16:45
// ----------------------------------------------------------------

using SqlSugar;
using System.Linq.Expressions;

namespace ZhaiFanhuaBlog.IServices.Bases;

/// <summary>
/// 服务基类接口
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IBaseService<TEntity> where TEntity : class
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