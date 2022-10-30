// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootRoleAuthorityService
// Guid:619a9c65-08b5-b2c7-0e17-57a30f09e61d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-06 下午 10:37:03
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// IRootRoleAuthorityService
/// </summary>
public interface IRootRoleAuthorityService : IBaseService<RootRoleAuthority>
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<RootRoleAuthority> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="rootRoleAuthorities"></param>
    /// <returns></returns>
    Task<bool> InitRootRoleAuthorityAsync(List<RootRoleAuthority> rootRoleAuthorities);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="userRole"></param>
    /// <returns></returns>
    Task<bool> CreateRootRoleAuthorityAsync(RootRoleAuthority userRole);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteRootRoleAuthorityAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="userRole"></param>
    /// <returns></returns>
    Task<RootRoleAuthority> ModifyRootRoleAuthorityAsync(RootRoleAuthority userRole);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<RootRoleAuthority> FindRootRoleAuthorityAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<RootRoleAuthority>> QueryRootRoleAuthorityAsync();
}