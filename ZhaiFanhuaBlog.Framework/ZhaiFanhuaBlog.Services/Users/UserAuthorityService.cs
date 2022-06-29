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
/// UserAuthorityService
/// </summary>
public class UserAuthorityService : BaseService<UserAuthority>, IUserAuthorityService
{
    private readonly IUserAuthorityRepository _IUserAuthorityRepository;

    public UserAuthorityService(IUserAuthorityRepository iUserAuthorityRepository)
    {
        _IUserAuthorityRepository = iUserAuthorityRepository;
        base._IBaseRepository = iUserAuthorityRepository;
    }

    public async Task<bool> CreateUserAuthorityAsync(UserAuthority userAuthority)
    {
        if (userAuthority.ParentId != null && await _IUserAuthorityRepository.FindAsync(userAuthority.ParentId) == null)
            throw new ApplicationException("父级权限不存在");
        userAuthority.SoftDeleteLock = false;
        var result = await _IUserAuthorityRepository.CreateAsync(userAuthority);
        return result;
    }

    public async Task<bool> DeleteUserAuthorityAsync(Guid guid)
    {
        var userAuthority = await _IUserAuthorityRepository.FindAsync(guid);
        if (userAuthority == null)
            throw new ApplicationException("权限不存在");
        if (userAuthority.SoftDeleteLock)
        {
            userAuthority.DeleteTime = DateTime.Now;
            return await _IUserAuthorityRepository.UpdateAsync(userAuthority);
        }
        else
        {
            return await _IUserAuthorityRepository.DeleteAsync(guid);
        }
    }

    public async Task<UserAuthority> ModifyUserAuthorityAsync(UserAuthority userAuthority)
    {
        if (await _IUserAuthorityRepository.FindAsync(userAuthority.BaseId) == null)
            throw new ApplicationException("权限不存在");
        if (userAuthority.ParentId != null && await _IUserAuthorityRepository.FindAsync(userAuthority.ParentId) == null)
            throw new ApplicationException("父级权限不存在");
        userAuthority.ModifyTime = DateTime.Now;
        var result = await _IUserAuthorityRepository.UpdateAsync(userAuthority);
        if (result) userAuthority = await _IUserAuthorityRepository.FindAsync(userAuthority.BaseId);
        return userAuthority;
    }

    public async Task<UserAuthority> FindUserAuthorityAsync(Guid guid)
    {
        var userAuthority = await _IUserAuthorityRepository.FindAsync(guid);
        return userAuthority;
    }

    public async Task<List<UserAuthority>> QueryUserAuthoritiesAsync()
    {
        var userAuthority = from rs in await _IUserAuthorityRepository.QueryAsync()
                            where rs.DeleteTime != null
                            orderby rs.CreateTime descending
                            orderby rs.Name descending
                            select rs;
        return userAuthority.ToList();
    }
}