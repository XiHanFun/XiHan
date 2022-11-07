﻿// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ISiteLogRepository
// Guid:b142ceb3-885d-4885-9694-33cd6da95bf8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:22:34
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Core.Services;
using ZhaiFanhuaBlog.Models.Sites;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Sites;

/// <summary>
/// ISiteLogRepository
/// </summary>
public interface ISiteLogRepository : IBaseRepository<SiteLog>, IScopeDependency
{
}