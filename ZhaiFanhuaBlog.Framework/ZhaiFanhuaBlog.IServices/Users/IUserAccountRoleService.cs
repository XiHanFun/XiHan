// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserAccountRoleService
// Guid:1442d253-c415-4808-930e-6eb3f0417fc1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 下午 05:21:36
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Users;

namespace ZhaiFanhuaBlog.IServices.Users;

public interface IUserAccountRoleService : IBaseService<UserAccountRole>
{
    Task<UserAccountRole> IsExistenceAsync(Guid guid);

    Task<bool> InitUserAccountRoleAsync(List<UserAccountRole> userAccountRoles);

    Task<bool> CreateUserAccountRoleAsync(UserAccountRole userAccountRole);

    Task<bool> DeleteUserAccountRoleAsync(Guid guid, Guid deleteId);

    Task<UserAccountRole> ModifyUserAccountRoleAsync(UserAccountRole userAccountRole);

    Task<UserAccountRole> FindUserAccountRoleAsync(Guid guid);

    Task<List<UserAccountRole>> QueryUserAccountRoleAsync();
}