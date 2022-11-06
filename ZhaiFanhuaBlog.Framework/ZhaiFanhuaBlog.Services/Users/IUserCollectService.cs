﻿// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserCollectService
// Guid:0fc098a6-9247-9181-e013-8b9535edbd44
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-06 下午 10:33:09
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// IUserCollectService
/// </summary>
public interface IUserCollectService : IBaseService<UserCollect>, IScopeDependency
{
}