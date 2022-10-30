// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserStatisticRepository
// Guid:0d75fb48-0e3f-49c1-8d9d-cb9edabce4fd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 10:09:52
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Users;

/// <summary>
/// UserStatisticRepository
/// </summary>
public class UserStatisticRepository : BaseRepository<UserStatistic>, IUserStatisticRepository
{
}