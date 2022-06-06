// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAccountService
// Guid:514a3309-e0b7-4b94-ae33-07f9ed9c1d55
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:03:32
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// UserAccountService
/// </summary>
public class UserAccountService : BaseService<UserAccount>, IUserAccountService
{
    private readonly IUserAccountRepository _IUserAccountRepository;

    public UserAccountService(IUserAccountRepository iUserAccountRepository)
    {
        _IUserAccountRepository = iUserAccountRepository;
        base._iBaseRepository = iUserAccountRepository;
    }
}