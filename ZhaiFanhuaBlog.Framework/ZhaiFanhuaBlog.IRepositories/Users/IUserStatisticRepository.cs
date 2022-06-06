// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserStatisticRepository
// Guid:42337634-f391-46a0-a3df-29c53c6f6162
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:30:28
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Bases;
using ZhaiFanhuaBlog.Models.Users;

namespace ZhaiFanhuaBlog.IRepositories.Users;

/// <summary>
/// IUserStatisticRepository
/// </summary>
public interface IUserStatisticRepository : IBaseRepository<UserStatistic>
{
}