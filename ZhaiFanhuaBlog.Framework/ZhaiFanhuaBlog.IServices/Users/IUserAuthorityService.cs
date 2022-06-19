// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserAuthorityService
// Guid:afebb9aa-504e-42b0-fb43-8fc584cbb4d1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-06 下午 10:30:20
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Users;

namespace ZhaiFanhuaBlog.IServices.Users;

public interface IUserAuthorityService : IBaseService<UserAuthority>
{
    Task<bool> CreateUserAuthorityAsync(UserAuthority userAuthority);

    Task<bool> DeleteUserAuthorityAsync(Guid guid);

    Task<UserAuthority> ModifyUserAuthorityAsync(UserAuthority userAuthority);

    Task<UserAuthority> FindUserAuthorityAsync(Guid guid);

    Task<List<UserAuthority>> QueryUserAuthoritiesAsync();
}