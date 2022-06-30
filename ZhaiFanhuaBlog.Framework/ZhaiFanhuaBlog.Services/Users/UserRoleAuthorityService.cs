// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserRoleAuthorityService
// Guid:fa73716d-d139-4da8-9eda-e6aca30bd5d0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:03:43
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// UserRoleAuthorityService
/// </summary>
public class UserRoleAuthorityService : BaseService<UserRoleAuthority>, IUserRoleAuthorityService
{
    private readonly IRootStateRepository _IRootStateRepository;
    private readonly IUserAuthorityRepository _IUserAuthorityRepository;
    private readonly IUserRoleRepository _IUserRoleRepository;
    private readonly IUserRoleAuthorityRepository _IUserRoleAuthorityRepository;

    public UserRoleAuthorityService(IRootStateRepository iRootStateRepository, IUserAuthorityRepository iUserAuthorityRepository, IUserRoleRepository iUserRoleRepository, IUserRoleAuthorityRepository iUserRoleAuthorityRepository)
    {
        base._IBaseRepository = iUserRoleAuthorityRepository;
        _IUserAuthorityRepository = iUserAuthorityRepository;
        _IRootStateRepository = iRootStateRepository;
        _IUserRoleRepository = iUserRoleRepository;
        _IUserRoleAuthorityRepository = iUserRoleAuthorityRepository;
    }

    public async Task<bool> CreateUserRoleAuthorityAsync(UserRoleAuthority userRoleAuthority)
    {
        if (await _IUserAuthorityRepository.FindAsync(userRoleAuthority.AuthorityId) == null)
            throw new ApplicationException("权限不存在");
        if (await _IUserRoleRepository.FindAsync(userRoleAuthority.RoleId) == null)
            throw new ApplicationException("角色不存在");
        if (await _IUserRoleAuthorityRepository.FindAsync(ura => ura.AuthorityId == userRoleAuthority.AuthorityId && ura.RoleId == userRoleAuthority.RoleId) != null)
            throw new ApplicationException("角色权限已存在");
        userRoleAuthority.SoftDeleteLock = false;
        userRoleAuthority.StateGuid = (await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == 1)).BaseId;
        var result = await _IUserRoleAuthorityRepository.CreateAsync(userRoleAuthority);
        return result;
    }

    public async Task<bool> DeleteUserRoleAuthorityAsync(Guid guid)
    {
        var userRoleAuthority = await _IUserRoleAuthorityRepository.FindAsync(guid);
        if (userRoleAuthority == null)
            throw new ApplicationException("角色权限不存在");
        if (userRoleAuthority.SoftDeleteLock)
        {
            userRoleAuthority.StateGuid = (await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == 0)).BaseId;
            userRoleAuthority.DeleteTime = DateTime.Now;
            return await _IUserRoleAuthorityRepository.UpdateAsync(userRoleAuthority);
        }
        else
        {
            return await _IUserRoleAuthorityRepository.DeleteAsync(guid);
        }
    }

    public async Task<UserRoleAuthority> ModifyUserRoleAuthorityAsync(UserRoleAuthority userRoleAuthority)
    {
        if (await _IUserRoleAuthorityRepository.FindAsync(userRoleAuthority.BaseId) == null)
            throw new ApplicationException("角色权限不存在");
        if (await _IUserAuthorityRepository.FindAsync(userRoleAuthority.AuthorityId) == null)
            throw new ApplicationException("权限不存在");
        if (await _IUserRoleRepository.FindAsync(userRoleAuthority.RoleId) == null)
            throw new ApplicationException("角色不存在");
        if (await _IUserRoleAuthorityRepository.FindAsync(ura => ura.AuthorityId == userRoleAuthority.AuthorityId && ura.RoleId == userRoleAuthority.RoleId) != null)
            throw new ApplicationException("角色权限已存在");
        userRoleAuthority.ModifyTime = DateTime.Now;
        var result = await _IUserRoleAuthorityRepository.UpdateAsync(userRoleAuthority);
        if (result) userRoleAuthority = await _IUserRoleAuthorityRepository.FindAsync(userRoleAuthority.BaseId);
        return userRoleAuthority;
    }

    public async Task<UserRoleAuthority> FindUserRoleAuthorityAsync(Guid guid)
    {
        var userRoleAuthority = await _IUserRoleAuthorityRepository.FindAsync(guid);
        return userRoleAuthority;
    }

    public async Task<List<UserRoleAuthority>> QueryUserRoleAuthoritiesAsync()
    {
        var userRoleAuthorities = new List<UserRoleAuthority>();
        var userRoleAuthority = from userroleauthority in await _IUserRoleAuthorityRepository.QueryAsync()
                                join rootstate in await _IRootStateRepository.QueryAsync() on userroleauthority.StateGuid equals rootstate.BaseId
                                where rootstate.StateKey.Equals(1)
                                orderby userroleauthority.CreateTime descending
                                select userroleauthority;

        foreach (var item in userRoleAuthority.ToList())
        {
            item.UserRoles = await _IUserRoleRepository.QueryAsync(e => e.BaseId == item.RoleId);
            item.UserAuthorities = await _IUserAuthorityRepository.QueryAsync(e => e.BaseId == item.AuthorityId);
            userRoleAuthorities.Add(item);
        }
        return userRoleAuthorities.ToList();
    }
}