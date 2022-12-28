#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBaseRepository
// Guid:7850231b-660e-4660-8747-5da8c607c53c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:04:18
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using System.Linq.Expressions;
using ZhaiFanhuaBlog.Infrastructure.Contexts.Response.Pages;

namespace ZhaiFanhuaBlog.Repositories.Bases;

/// <summary>
/// 仓储基类接口
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IBaseRepository<TEntity> : ISimpleClient<TEntity> where TEntity : class, new()
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
    Task<bool> DeleteByGuidAsync(Guid guid);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    new Task<bool> DeleteAsync(TEntity entity);

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="guids"></param>
    /// <returns></returns>
    Task<bool> DeleteByGuidBatchAsync(Guid[] guids);

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
    new Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> func);

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    new Task<bool> UpdateAsync(TEntity entity);

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
    Task<TEntity> FindByGuidAsync(Guid guid);

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
    /// 分页查询
    /// </summary>
    /// <param name="currentIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalCount"></param>
    /// <returns></returns>
    Task<List<TEntity>> QueryPageListAsync(int currentIndex, int pageSize, RefAsync<int> totalCount);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <returns></returns>
    Task<BasePageDataDto<TEntity>> QueryPageDataDtoAsync(int currentIndex, int pageSize);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageDto">分页传入实体</param>
    /// <returns></returns>
    Task<BasePageDataDto<TEntity>> QueryPageDataDtoAsync(BasePageDto pageDto);

    /// <summary>
    /// 按页条件查询
    /// </summary>
    /// <param name="func"></param>
    /// <param name="currentIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalCount"></param>
    /// <returns></returns>
    Task<List<TEntity>> QueryPageListAsync(Expression<Func<TEntity, bool>> func, int currentIndex, int pageSize, RefAsync<int> totalCount);

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="func">自定义条件</param>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <returns></returns>
    Task<BasePageDataDto<TEntity>> QueryPageDataDtoAsync(Expression<Func<TEntity, bool>> func, int currentIndex, int pageSize);

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="func">自定义条件</param>
    /// <param name="pageDto">分页传入实体</param>
    /// <returns></returns>
    Task<BasePageDataDto<TEntity>> QueryPageDataDtoAsync(Expression<Func<TEntity, bool>> func, BasePageDto pageDto);
}