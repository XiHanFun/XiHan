// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserStatisticService
// Guid:278566cb-87c9-dfb5-efc9-43c2792502c5
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-06 下午 10:30:05
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// IUserStatisticService
/// </summary>
public interface IUserStatisticService : IBaseService<UserStatistic>, IScopeDependency
{
}