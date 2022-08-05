// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootRoleMenuService
// Guid:8aee44e2-4110-4af6-a221-7e12392fe261
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-08-05 下午 05:20:54
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Roots;

namespace ZhaiFanhuaBlog.IServices.Roots;

/// <summary>
/// IRootRoleMenuService
/// </summary>
public interface IRootRoleMenuService : IBaseService<RootRoleMenu>
{
    Task<RootRoleMenu> IsExistenceAsync(Guid guid);

    Task<bool> InitRootRoleMenuAsync(List<RootRoleMenu> RootRoleMenus);

    Task<bool> CreateRootRoleMenuAsync(RootRoleMenu RootRoleMenu);

    Task<bool> DeleteRootRoleMenuAsync(Guid guid, Guid deleteId);

    Task<RootRoleMenu> ModifyRootRoleMenuAsync(RootRoleMenu RootRoleMenu);

    Task<RootRoleMenu> FindRootRoleMenuAsync(Guid guid);

    Task<List<RootRoleMenu>> QueryRootRoleMenuAsync();
}