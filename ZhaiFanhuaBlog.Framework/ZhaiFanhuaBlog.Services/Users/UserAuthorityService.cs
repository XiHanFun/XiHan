// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAuthorityService
// Guid:02502f6a-01bf-49ba-857a-7fc267bd04dc
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 10:50:02
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// 账户权限
/// </summary>
public class UserAuthorityService : BaseService<UserAuthority>, IUserAuthorityService
{
    private readonly IUserAuthorityRepository _IUserAuthorityRepository;

    public UserAuthorityService(IUserAuthorityRepository iUserAuthorityRepository)
    {
        _IUserAuthorityRepository = iUserAuthorityRepository;
        base._iBaseRepository = iUserAuthorityRepository;
    }

    public async Task<UserAuthority> CreateUserAuthorityAsync(UserAuthority userAuthority)
    {
        userAuthority.TypeKey = "UserAuthority";
        userAuthority.StateKey = 1;
        var result = await _IUserAuthorityRepository.CreateAsync(userAuthority);
        if (result) userAuthority = await _IUserAuthorityRepository.FindAsync(userAuthority.BaseId);
        return userAuthority;
    }
}