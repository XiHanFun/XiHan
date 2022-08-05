// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootRoleService
// Guid:619a9c65-08b5-b2c7-0e17-57a30f09e61d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-06 下午 10:37:03
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Roots;

namespace ZhaiFanhuaBlog.IServices.Roots;

public interface IRootRoleService : IBaseService<RootRole>
{
    Task<RootRole> IsExistenceAsync(Guid guid);

    Task<bool> InitRootRoleAsync(List<RootRole> userRoles);

    Task<bool> CreateRootRoleAsync(RootRole userRole);

    Task<bool> DeleteRootRoleAsync(Guid guid, Guid deleteId);

    Task<RootRole> ModifyRootRoleAsync(RootRole userRole);

    Task<RootRole> FindRootRoleAsync(Guid guid);

    Task<List<RootRole>> QueryRootRoleAsync();
}