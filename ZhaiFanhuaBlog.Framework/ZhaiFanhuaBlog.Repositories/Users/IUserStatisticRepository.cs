// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserStatisticRepository
// Guid:42337634-f391-46a0-a3df-29c53c6f6162
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:30:28
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Repositories.Users;

/// <summary>
/// IUserStatisticRepository
/// </summary>
public interface IUserStatisticRepository : IBaseRepository<UserStatistic>, IScopeDependency
{
}