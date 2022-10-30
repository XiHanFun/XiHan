// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserOauthService
// Guid:3988fb93-8d52-4da4-882a-dfd39aaa987a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:04:46
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// 用户第三方授权
/// </summary>
public class UserOauthService : BaseService<UserOauth>, IUserOauthService
{
    private readonly IUserOauthRepository _IUserOauthRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iUserOauthRepository"></param>
    public UserOauthService(IUserOauthRepository iUserOauthRepository)
    {
        _IUserOauthRepository = iUserOauthRepository;
        base._IBaseRepository = iUserOauthRepository;
    }
}