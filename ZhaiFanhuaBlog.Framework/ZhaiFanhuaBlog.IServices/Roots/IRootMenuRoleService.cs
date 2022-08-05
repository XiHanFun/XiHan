// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootMenuRoleService
// Guid:8aee44e2-4110-4af6-a221-7e12392fe261
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-08-05 下午 05:20:54
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Roots;

namespace ZhaiFanhuaBlog.IServices.Roots;

/// <summary>
/// IRootMenuRoleService
/// </summary>
public interface IRootMenuRoleService : IBaseService<RootMenuRole>
{
    Task<RootMenuRole> IsExistenceAsync(Guid guid);

    Task<bool> InitRootMenuRoleAsync(List<RootMenuRole> RootMenuRoles);

    Task<bool> CreateRootMenuRoleAsync(RootMenuRole RootMenuRole);

    Task<bool> DeleteRootMenuRoleAsync(Guid guid, Guid deleteId);

    Task<RootMenuRole> ModifyRootMenuRoleAsync(RootMenuRole RootMenuRole);

    Task<RootMenuRole> FindRootMenuRoleAsync(Guid guid);

    Task<List<RootMenuRole>> QueryRootMenuRoleAsync();
}