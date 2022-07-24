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

    public async Task<bool> InitUserRoleAuthorityAsync(List<UserRoleAuthority> userRoleAuthorities)
    {
        userRoleAuthorities.ForEach(userRoleAuthoritie =>
        {
            userRoleAuthoritie.SoftDeleteLock = false;
        });
        var result = await _IUserRoleAuthorityRepository.CreateBatchAsync(userRoleAuthorities);
        return result;
    }

    public async Task<bool> CreateUserRoleAuthorityAsync(UserRoleAuthority userRoleAuthority)
    {
        if (await _IUserAuthorityRepository.FindAsync(userRoleAuthority.AuthorityId) == null)
            throw new ApplicationException("用户权限不存在");
        if (await _IUserRoleRepository.FindAsync(userRoleAuthority.RoleId) == null)
            throw new ApplicationException("用户角色不存在");
        if (await _IUserRoleAuthorityRepository.FindAsync(ura => ura.AuthorityId == userRoleAuthority.AuthorityId && ura.RoleId == userRoleAuthority.RoleId) != null)
            throw new ApplicationException("用户角色权限已存在");
        userRoleAuthority.SoftDeleteLock = false;
        var result = await _IUserRoleAuthorityRepository.CreateAsync(userRoleAuthority);
        return result;
    }

    public async Task<bool> DeleteUserRoleAuthorityAsync(Guid guid, Guid deleteId)
    {
        var userRoleAuthority = await _IUserRoleAuthorityRepository.FindAsync(ura => ura.BaseId == guid && ura.SoftDeleteLock == false);
        if (userRoleAuthority == null)
            throw new ApplicationException("用户角色权限不存在");
        var rootState = await _IRootStateRepository.FindAsync(rs => rs.TypeKey == "All" && rs.StateKey == -1);
        userRoleAuthority.SoftDeleteLock = true;
        userRoleAuthority.DeleteId = deleteId;
        userRoleAuthority.DeleteTime = DateTime.Now;
        userRoleAuthority.StateId = rootState.BaseId;
        return await _IUserRoleAuthorityRepository.DeleteAsync(guid);
    }

    public async Task<UserRoleAuthority> ModifyUserRoleAuthorityAsync(UserRoleAuthority userRoleAuthority)
    {
        if (await _IUserRoleAuthorityRepository.FindAsync(ura => ura.BaseId == userRoleAuthority.BaseId && ura.SoftDeleteLock == false) == null)
            throw new ApplicationException("用户角色权限不存在");
        if (await _IUserAuthorityRepository.FindAsync(ua => ua.BaseId == userRoleAuthority.AuthorityId && ua.SoftDeleteLock == false) == null)
            throw new ApplicationException("用户权限不存在");
        if (await _IUserRoleRepository.FindAsync(ur => ur.BaseId == userRoleAuthority.RoleId && ur.SoftDeleteLock == false) == null)
            throw new ApplicationException("用户角色不存在");
        if (await _IUserRoleAuthorityRepository.FindAsync(ura => ura.AuthorityId == userRoleAuthority.AuthorityId && ura.RoleId == userRoleAuthority.RoleId && ura.SoftDeleteLock == false) != null)
            throw new ApplicationException("用户角色权限已存在");
        userRoleAuthority.ModifyTime = DateTime.Now;
        var result = await _IUserRoleAuthorityRepository.UpdateAsync(userRoleAuthority);
        if (result) userRoleAuthority = await _IUserRoleAuthorityRepository.FindAsync(userRoleAuthority.BaseId);
        return userRoleAuthority;
    }

    public async Task<UserRoleAuthority> FindUserRoleAuthorityAsync(Guid guid)
    {
        var userRoleAuthority = await _IUserRoleAuthorityRepository.FindAsync(ura => ura.BaseId == guid && ura.SoftDeleteLock == false);
        if (userRoleAuthority == null)
            throw new ApplicationException("用户角色权限不存在");
        return userRoleAuthority;
    }

    public async Task<List<UserRoleAuthority>> QueryUserRoleAuthoritiesAsync()
    {
        var userRoleAuthority = from userroleauthority in await _IUserRoleAuthorityRepository.QueryAsync(uar => uar.SoftDeleteLock == false)
                                orderby userroleauthority.CreateTime descending
                                select userroleauthority;
        return userRoleAuthority.ToList();
    }
}