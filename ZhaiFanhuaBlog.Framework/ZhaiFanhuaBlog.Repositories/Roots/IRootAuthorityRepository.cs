﻿// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootAuthorityRepository
// Guid:498edbce-445c-4609-83b3-613527673216
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:27:27
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Repositories.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Repositories.Roots;

/// <summary>
/// IRootAuthorityRepository
/// </summary>
public interface IRootAuthorityRepository : IBaseRepository<RootAuthority>, IScopeDependency
{
}