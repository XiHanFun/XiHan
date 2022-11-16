#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBaseService
// Guid:368c93d7-dc11-4f23-a16d-c6bef363e3e0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 10:16:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using System.Linq.Expressions;
using ZhaiFanhuaBlog.ViewModels.Bases.Pages;

namespace ZhaiFanhuaBlog.Services.Bases;

/// <summary>
/// 服务基类接口
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IBaseService<TEntity> where TEntity : class
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
    Task<TEntity> FindAsync(Guid guid);

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
    Task<PageDataDto<TEntity>> QueryPageDataDtoAsync(int currentIndex, int pageSize);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageDto">分页传入实体</param>
    /// <returns></returns>
    Task<PageDataDto<TEntity>> QueryPageDataDtoAsync(BasePageDto pageDto);

    /// <summary>
    /// 分页条件查询
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
    Task<PageDataDto<TEntity>> QueryPageDataDtoAsync(Expression<Func<TEntity, bool>> func, int currentIndex, int pageSize);

    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="func">自定义条件</param>
    /// <param name="pageDto">分页传入实体</param>
    /// <returns></returns>
    Task<PageDataDto<TEntity>> QueryPageDataDtoAsync(Expression<Func<TEntity, bool>> func, BasePageDto pageDto);
}