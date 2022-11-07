// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserNoticeRepository
// Guid:182c483b-1bb0-4219-882a-df57eb5a8ca8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:29:15
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Core.Services;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Users;

/// <summary>
/// IUserNoticeRepository
/// </summary>
public interface IUserNoticeRepository : IBaseRepository<UserNotice>, IScopeDependency
{
}