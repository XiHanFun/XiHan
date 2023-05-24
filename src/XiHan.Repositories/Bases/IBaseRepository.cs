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
using XiHan.Infrastructures.Responses.Pages;

namespace XiHan.Repositories.Bases;

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
    Task<bool> AddAsync(TEntity entity);

    /// <summary>
    /// 新增返回Id
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<long> AddReturnIdAsync(TEntity entity);

    /// <summary>
    /// 新增或更新
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> AddOrUpdateAsync(TEntity entity);

    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> AddBatchAsync(TEntity[] entities);

    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> AddBatchAsync(List<TEntity> entities);

    /// <summary>
    /// 批量新增或更新
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> AddOrUpdateBatchAsync(List<TEntity> entities);

    /// <summary>
    /// 根据Id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> RemoveByIdAsync(long id);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> RemoveAsync(TEntity entity);

    /// <summary>
    /// 根据Id批量删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<bool> RemoveByIdBatchAsync(long[] ids);

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> RemoveBatchAsync(List<TEntity> entities);

    /// <summary>
    /// 自定义条件删除
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    Task<bool> RemoveAsync(Expression<Func<TEntity, bool>> func);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    new Task<bool> UpdateAsync(TEntity entity);

    /// <summary>
    /// 批量修改
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> UpdateBatchAsync(TEntity[] entities);

    /// <summary>
    /// 批量修改
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> UpdateBatchAsync(List<TEntity> entities);

    /// <summary>
    /// 根据Id查找
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> FindByIdAsync(long id);

    /// <summary>
    /// 自定义条件查找
    /// </summary>
    /// <param name="func">自定义条件</param>
    /// <returns></returns>
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func);

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> QueryListAsync();

    /// <summary>
    /// 自定义条件查询
    /// </summary>
    /// <param name="func">自定义条件</param>
    /// <returns></returns>
    Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> func);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="totalCount">查询到的总数</param>
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
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="func">自定义条件</param>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="totalCount">查询到的总数</param>
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