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
    #region 新增

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> AddAsync(TEntity entity);

    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> AddAsync(TEntity[] entities);

    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> AddAsync(List<TEntity> entities);

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
    /// 批量新增或更新
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> AddOrUpdateAsync(List<TEntity> entities);

    #endregion

    #region 删除

    /// <summary>
    /// 根据Id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> RemoveAsync(long id);

    /// <summary>
    /// 根据Id批量删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<bool> RemoveAsync(long[] ids);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> RemoveAsync(TEntity entity);

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> RemoveAsync(List<TEntity> entities);

    /// <summary>
    /// 自定义条件删除
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    Task<bool> RemoveAsync(Expression<Func<TEntity, bool>> whereExpression);

    #endregion

    #region 修改

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
    Task<bool> UpdateAsync(TEntity[] entities);

    /// <summary>
    /// 批量修改
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(List<TEntity> entities);

    #endregion

    #region 查找查询

    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// 根据Id查找
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> FindAsync(long id);

    /// <summary>
    /// 自定义条件查找
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <returns></returns>
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> QueryAllAsync();

    /// <summary>
    /// 自定义条件查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <returns></returns>
    Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 自定义条件查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <param name="orderExpression">自定义排序条件</param>
    /// <param name="isOrderAsc">是否正序排序</param>
    /// <returns></returns>
    Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderExpression, bool isOrderAsc = true);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="totalCount">查询到的总数</param>
    /// <returns></returns>
    Task<List<TEntity>> QueryAsync(int currentIndex, int pageSize, RefAsync<int> totalCount);

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="totalCount">查询到的总数</param>
    /// <returns></returns>
    Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression, int currentIndex, int pageSize, RefAsync<int> totalCount);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <returns></returns>
    Task<PageDataDto<TEntity>> QueryPageAsync(int currentIndex, int pageSize);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="page">分页实体</param>
    /// <returns></returns>
    Task<PageDataDto<TEntity>> QueryPageAsync(PageDto page);

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <returns></returns>
    Task<PageDataDto<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> whereExpression, int currentIndex, int pageSize);

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <param name="page">分页实体</param>
    /// <returns></returns>
    Task<PageDataDto<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> whereExpression, PageDto page);

    /// <summary>
    /// 自定义条件分页排序查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="orderExpression">自定义排序条件</param>
    /// <param name="isOrderAsc">是否正序排序</param>
    /// <returns></returns>
    Task<PageDataDto<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> whereExpression, int currentIndex, int pageSize, Expression<Func<TEntity, object>> orderExpression, bool isOrderAsc = true);

    /// <summary>
    /// 自定义条件分页排序查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <param name="page">分页实体</param>
    /// <param name="orderExpression">自定义排序条件</param>
    /// <param name="isOrderAsc">是否正序排序</param>
    /// <returns></returns>
    Task<PageDataDto<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> whereExpression, PageDto page, Expression<Func<TEntity, object>> orderExpression, bool isOrderAsc = true);

    #endregion
}