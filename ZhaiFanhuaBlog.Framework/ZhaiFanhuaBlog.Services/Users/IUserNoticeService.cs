#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserNoticeService
// Guid:9e75f763-e0fe-eb1f-cc30-34529b6b247d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-06 下午 10:34:26
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Core.Services;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// IUserNoticeService
/// </summary>
public interface IUserNoticeService : IBaseService<UserNotice>, IScopeDependency
{
}