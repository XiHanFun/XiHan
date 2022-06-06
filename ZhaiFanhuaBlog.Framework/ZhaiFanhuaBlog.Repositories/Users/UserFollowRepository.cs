// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserFollowRepository
// Guid:ea5c1d3b-760f-441d-a819-27de4286eead
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 10:08:36
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Users;

/// <summary>
/// UserFollowRepository
/// </summary>
public class UserFollowRepository : BaseRepository<UserFollow>, IUserFollowRepository
{
}