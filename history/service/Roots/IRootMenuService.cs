#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootMenuService
// Guid:7df2f5f6-5b13-441a-903c-727e3c6fdc6f
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-08-05 下午 05:20:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Infrastructure.Services;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// IRootMenuService
/// </summary>
public interface IRootMenuService : IBaseService<RootMenu>, IScopeDependency
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<RootMenu> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="RootMenus"></param>
    /// <returns></returns>
    Task<bool> InitRootMenuAsync(List<RootMenu> RootMenus);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="RootMenu"></param>
    /// <returns></returns>
    Task<bool> CreateRootMenuAsync(RootMenu RootMenu);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteRootMenuAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="RootMenu"></param>
    /// <returns></returns>
    Task<RootMenu> ModifyRootMenuAsync(RootMenu RootMenu);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<RootMenu> FindRootMenuAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<RootMenu>> QueryRootMenuAsync();
}