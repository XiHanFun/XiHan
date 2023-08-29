#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootAuthorityService
// Guid:afebb9aa-504e-42b0-fb43-8fc584cbb4d1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-06 下午 10:30:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Infrastructure.Services;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// IRootAuthorityService
/// </summary>
public interface IRootAuthorityService : IBaseService<RootAuthority>, IScopeDependency
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<RootAuthority> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="userAuthorities"></param>
    /// <returns></returns>
    Task<bool> InitRootAuthorityAsync(List<RootAuthority> userAuthorities);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="rootAuthority"></param>
    /// <returns></returns>
    Task<bool> CreateRootAuthorityAsync(RootAuthority rootAuthority);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteRootAuthorityAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="rootAuthority"></param>
    /// <returns></returns>
    Task<RootAuthority> ModifyRootAuthorityAsync(RootAuthority rootAuthority);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<RootAuthority> FindRootAuthorityAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<RootAuthority>> QueryRootAuthorityAsync();
}