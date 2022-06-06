// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAuthorityRepository
// Guid:ef73c43f-dc36-4973-b4ac-954d1ef60d36
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 10:07:33
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Users;

/// <summary>
/// UserAuthorityRepository
/// </summary>
public class UserAuthorityRepository : BaseRepository<UserAuthority>, IUserAuthorityRepository
{
}