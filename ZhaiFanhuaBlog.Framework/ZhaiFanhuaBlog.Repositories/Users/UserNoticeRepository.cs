#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserNoticeRepository
// Guid:9cbaa0ac-ffb1-4464-93b1-c4cb488c4e80
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 10:09:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Users;

/// <summary>
/// UserNoticeRepository
/// </summary>
public class UserNoticeRepository : BaseRepository<UserNotice>, IUserNoticeRepository
{
}