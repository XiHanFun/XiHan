// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootAnnouncementService
// Guid:de4198c2-b99f-4a08-ace2-8e16f06f12c6
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-24 上午 01:15:12
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Core.Services;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// IRootAnnouncementService
/// </summary>
public interface IRootAnnouncementService : IBaseService<RootAnnouncement>, IScopeDependency
{
}