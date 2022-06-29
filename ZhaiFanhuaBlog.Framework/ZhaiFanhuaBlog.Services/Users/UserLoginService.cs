// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserLoginService
// Guid:4fe56a1d-1a0f-415f-a38f-677ddafef307
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:04:29
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// UserLoginService
/// </summary>
public class UserLoginService : BaseService<UserLogin>, IUserLoginService
{
    private readonly IUserLoginRepository _IUserLoginRepository;

    public UserLoginService(IUserLoginRepository iUserLoginRepository)
    {
        _IUserLoginRepository = iUserLoginRepository;
        base._IBaseRepository = iUserLoginRepository;
    }
}