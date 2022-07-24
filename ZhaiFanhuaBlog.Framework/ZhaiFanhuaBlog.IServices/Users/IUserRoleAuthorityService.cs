// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserRoleAuthorityAuthorityService
// Guid:619a9c65-08b5-b2c7-0e17-57a30f09e61d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-06 下午 10:37:03
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Users;

namespace ZhaiFanhuaBlog.IServices.Users;

public interface IUserRoleAuthorityService : IBaseService<UserRoleAuthority>
{
    Task<bool> InitUserRoleAuthorityAsync(List<UserRoleAuthority> userRoleAuthorities);

    Task<bool> CreateUserRoleAuthorityAsync(UserRoleAuthority userRole);

    Task<bool> DeleteUserRoleAuthorityAsync(Guid guid, Guid deleteId);

    Task<UserRoleAuthority> ModifyUserRoleAuthorityAsync(UserRoleAuthority userRole);

    Task<UserRoleAuthority> FindUserRoleAuthorityAsync(Guid guid);

    Task<List<UserRoleAuthority>> QueryUserRoleAuthoritiesAsync();
}