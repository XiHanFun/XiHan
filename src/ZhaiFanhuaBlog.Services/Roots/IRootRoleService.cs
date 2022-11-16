#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootRoleService
// Guid:619a9c65-08b5-b2c7-0e17-57a30f09e61d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-06 下午 10:37:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Core.Services;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// IRootRoleService
/// </summary>
public interface IRootRoleService : IBaseService<RootRole>, IScopeDependency
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<RootRole> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="userRoles"></param>
    /// <returns></returns>
    Task<bool> InitRootRoleAsync(List<RootRole> userRoles);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="userRole"></param>
    /// <returns></returns>
    Task<bool> CreateRootRoleAsync(RootRole userRole);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteRootRoleAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="userRole"></param>
    /// <returns></returns>
    Task<RootRole> ModifyRootRoleAsync(RootRole userRole);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<RootRole> FindRootRoleAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<RootRole>> QueryRootRoleAsync();
}