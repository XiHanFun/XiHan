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
    protected IBaseRepository<TEntity> _iBaseRepository;

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<bool> CreateAsync(TEntity entity)
    {
        return await _iBaseRepository.CreateAsync(entity);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(Guid guid)
    {
        return await _iBaseRepository.DeleteAsync(guid);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="guids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteBatchAsync(Guid[] guids)
    {
        return await _iBaseRepository.DeleteBatchAsync(guids);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(TEntity entity)
    {
        return await _iBaseRepository.UpdateAsync(entity);
    }

    /// <summary>
    /// Guid查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<TEntity> FindAsync(Guid? guid)
    {
        return await _iBaseRepository.FindAsync(guid);
    }

    /// <summary>
    /// 自定义条件查找
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func)
    {
        return await _iBaseRepository.FindAsync(func);
    }

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <returns></returns>
    public async Task<List<TEntity>> QueryAsync()
    {
        return await _iBaseRepository.QueryAsync();
    }

    /// <summary>
    /// 自定义条件查询
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
    {
        return await _iBaseRepository.QueryAsync(func);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalCount"></param>
    /// <returns></returns>
    public async Task<List<TEntity>> QueryAsync(int pageIndex, int pageSize, RefAsync<int> totalCount)
    {
        return await _iBaseRepository.QueryAsync(pageIndex, pageSize, totalCount);
    }

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="func"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalCount"></param>
    /// <returns></returns>
    public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int pageIndex, int pageSize, RefAsync<int> totalCount)
    {
        return await _iBaseRepository.QueryAsync(func, pageIndex, pageSize, totalCount);
    }
}