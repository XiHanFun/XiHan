// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAccountRepository
// Guid:4b0344b9-5385-4ab8-8308-8819c33345d1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 10:07:20
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Users;

/// <summary>
/// UserAccountRepository
/// </summary>
public class UserAccountRepository : BaseRepository<UserAccount>, IUserAccountRepository
{
}