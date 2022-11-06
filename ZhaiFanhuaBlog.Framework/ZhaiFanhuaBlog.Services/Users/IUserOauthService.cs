// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserOauthService
// Guid:10b16c1e-ad04-798d-343f-66e4ef22035b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-06 下午 10:38:33
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// IUserOauthService
/// </summary>
public interface IUserOauthService : IBaseService<UserOauth>, IScopeDependency
{
}