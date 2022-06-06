// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserRoleRepository
// Guid:54dbb62b-368b-4cd7-bb71-d223642c0f5f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 10:09:29
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Users;

/// <summary>
/// UserRoleRepository
/// </summary>
public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
{
}