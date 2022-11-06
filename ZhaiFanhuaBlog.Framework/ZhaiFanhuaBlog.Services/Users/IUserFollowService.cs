// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserFollowService
// Guid:1b7c9e03-fce0-1dd3-3322-ff9f6b4de485
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-06 下午 10:34:22
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// IUserFollowService
/// </summary>
public interface IUserFollowService : IBaseService<UserFollow>, IScopeDependency
{
}