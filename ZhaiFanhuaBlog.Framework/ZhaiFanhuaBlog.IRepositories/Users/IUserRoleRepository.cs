// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserRoleRepository
// Guid:84bce558-3290-4ac1-ad26-a98fcaa55b70
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:29:49
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Bases;
using ZhaiFanhuaBlog.Models.Users;

namespace ZhaiFanhuaBlog.IRepositories.Users;

/// <summary>
/// IUserRoleRepository
/// </summary>
public interface IUserRoleRepository : IBaseRepository<UserRole>
{
}