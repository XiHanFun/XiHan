#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IRepository
// Guid:7850231b-660e-4660-8747-5da8c607c53c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 09:04:18
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Linq.Expressions;
using XiHan.Common.Shared.Pages.Dtos;

namespace XiHan.Domain.Bases.SeedWorks;

/// <summary>
/// 仓储抽象接口
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity> where TEntity : IAggregateRoot
{
    #region 工作单元

    /// <summary>
    /// 工作单元
    /// </summary>
    IUnitOfWork UnitOfWork { get; }

    #endregion

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
    Task<bool> AddAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// 新增返回Id
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<long> AddReturnIdAsync(TEntity entity);

    #endregion

    #region 新增或更新

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
    Task<bool> AddOrUpdateAsync(IEnumerable<TEntity> entities);

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
    Task<bool> RemoveAsync(IEnumerable<long> ids);

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
    Task<bool> RemoveAsync(IEnumerable<TEntity> entities);

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
    Task<bool> UpdateAsync(TEntity entity);

    /// <summary>
    /// 修改某列
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 批量修改
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(IEnumerable<TEntity> entities);

    #endregion

    #region 查找

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

    #endregion

    #region 查询

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
    /// 是否存在
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<bool> IsAnyAsync(Expression<Func<TEntity, bool>> expression);

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
    Task<List<TEntity>> QueryAsync(int currentIndex, int pageSize, ref int totalCount);

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <param name="currentIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="totalCount">查询到的总数</param>
    /// <returns></returns>
    Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression, int currentIndex, int pageSize, ref int totalCount);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="page">分页实体</param>
    /// <returns></returns>
    Task<PageResponseDataDto<TEntity>> QueryPageAsync(PageInfoDto page);

    /// <summary>
    /// 分页排序查询
    /// </summary>
    /// <param name="page">分页实体</param>
    /// <param name="orderExpression">自定义排序条件</param>
    /// <param name="isOrderAsc">是否正序排序</param>
    /// <returns></returns>
    Task<PageResponseDataDto<TEntity>> QueryPageAsync(PageInfoDto page, Expression<Func<TEntity, object>> orderExpression, bool isOrderAsc = true);

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <param name="page">分页实体</param>
    /// <returns></returns>
    Task<PageResponseDataDto<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> whereExpression, PageInfoDto page);

    /// <summary>
    /// 自定义条件分页排序查询
    /// </summary>
    /// <param name="whereExpression">自定义条件</param>
    /// <param name="page">分页实体</param>
    /// <param name="orderExpression">自定义排序条件</param>
    /// <param name="isOrderAsc">是否正序排序</param>
    /// <returns></returns>
    Task<PageResponseDataDto<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> whereExpression, PageInfoDto page, Expression<Func<TEntity, object>> orderExpression, bool isOrderAsc = true);

    #endregion
}