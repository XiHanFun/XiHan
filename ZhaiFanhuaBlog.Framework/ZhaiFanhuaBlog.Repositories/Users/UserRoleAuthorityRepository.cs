// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserRoleAuthorityRepository
// Guid:a16ee7d2-ea37-4021-90e3-4ea8ce739b7f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 10:09:42
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Users;

/// <summary>
/// UserRoleAuthorityRepository
/// </summary>
public class UserRoleAuthorityRepository : BaseRepository<UserRoleAuthority>, IUserRoleAuthorityRepository
{
}