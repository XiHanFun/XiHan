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
    private readonly IUserAuthorityService _IUserAuthorityService;
    private readonly IUserRoleService _IUserRoleService;
    private readonly IUserRoleAuthorityRepository _IUserRoleAuthorityRepository;

    public UserRoleAuthorityService(IUserAuthorityService iUserAuthorityService,
        IUserRoleService iUserRoleService,
        IUserRoleAuthorityRepository iUserRoleAuthorityRepository)
    {
        base._IBaseRepository = iUserRoleAuthorityRepository;
        _IUserAuthorityService = iUserAuthorityService;
        _IUserRoleService = iUserRoleService;
        _IUserRoleAuthorityRepository = iUserRoleAuthorityRepository;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<UserRoleAuthority> IsExistenceAsync(Guid guid)
    {
        var userRoleAuthority = await _IUserRoleAuthorityRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (userRoleAuthority == null)
            throw new ApplicationException("用户角色权限不存在");
        return userRoleAuthority;
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
        await _IUserAuthorityService.IsExistenceAsync(userRoleAuthority.AuthorityId);
        await _IUserRoleService.IsExistenceAsync(userRoleAuthority.RoleId);
        if (await _IUserRoleAuthorityRepository.FindAsync(e => e.AuthorityId == userRoleAuthority.AuthorityId && e.RoleId == userRoleAuthority.RoleId) != null)
            throw new ApplicationException("用户角色权限已存在");
        userRoleAuthority.SoftDeleteLock = false;
        var result = await _IUserRoleAuthorityRepository.CreateAsync(userRoleAuthority);
        return result;
    }

    public async Task<bool> DeleteUserRoleAuthorityAsync(Guid guid, Guid deleteId)
    {
        var userRoleAuthority = await IsExistenceAsync(guid);
        userRoleAuthority.SoftDeleteLock = true;
        userRoleAuthority.DeleteId = deleteId;
        userRoleAuthority.DeleteTime = DateTime.Now;
        return await _IUserRoleAuthorityRepository.UpdateAsync(userRoleAuthority);
    }

    public async Task<UserRoleAuthority> ModifyUserRoleAuthorityAsync(UserRoleAuthority userRoleAuthority)
    {
        await IsExistenceAsync(userRoleAuthority.BaseId);
        await _IUserAuthorityService.IsExistenceAsync(userRoleAuthority.AuthorityId);
        await _IUserRoleService.IsExistenceAsync(userRoleAuthority.RoleId);
        if (await _IUserRoleAuthorityRepository.FindAsync(e => e.AuthorityId == userRoleAuthority.AuthorityId && e.RoleId == userRoleAuthority.RoleId && e.SoftDeleteLock == false) != null)
            throw new ApplicationException("用户角色权限已存在");
        userRoleAuthority.ModifyTime = DateTime.Now;
        var result = await _IUserRoleAuthorityRepository.UpdateAsync(userRoleAuthority);
        if (result) userRoleAuthority = await _IUserRoleAuthorityRepository.FindAsync(userRoleAuthority.BaseId);
        return userRoleAuthority;
    }

    public async Task<UserRoleAuthority> FindUserRoleAuthorityAsync(Guid guid)
    {
        var userRoleAuthority = await IsExistenceAsync(guid);
        return userRoleAuthority;
    }

    public async Task<List<UserRoleAuthority>> QueryUserRoleAuthorityAsync()
    {
        var userRoleAuthority = from userroleauthority in await _IUserRoleAuthorityRepository.QueryListAsync(e => e.SoftDeleteLock == false)
                                orderby userroleauthority.CreateTime descending
                                select userroleauthority;
        return userRoleAuthority.ToList();
    }
}