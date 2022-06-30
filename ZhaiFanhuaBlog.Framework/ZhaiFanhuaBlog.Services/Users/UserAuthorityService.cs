// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAuthorityService
// Guid:02502f6a-01bf-49ba-857a-7fc267bd04dc
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 10:50:02
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Roots;
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
    private readonly IRootStateRepository _IRootStateRepository;
    private readonly IUserAuthorityRepository _IUserAuthorityRepository;
    private readonly IUserRoleAuthorityRepository _IUserRoleAuthorityRepository;

    public UserAuthorityService(IUserAuthorityRepository iUserAuthorityRepository, IRootStateRepository iRootStateRepository, IUserRoleAuthorityRepository iUserRoleAuthorityRepository)
    {
        base._IBaseRepository = iUserAuthorityRepository;
        _IUserAuthorityRepository = iUserAuthorityRepository;
        _IRootStateRepository = iRootStateRepository;
        _IUserRoleAuthorityRepository = iUserRoleAuthorityRepository;
    }

    public async Task<bool> CreateUserAuthorityAsync(UserAuthority userAuthority)
    {
        if (userAuthority.ParentId != null && await _IUserAuthorityRepository.FindAsync(userAuthority.ParentId) == null)
            throw new ApplicationException("父级权限不存在");
        if (await _IUserAuthorityRepository.FindAsync(ua => ua.Name == userAuthority.Name) != null)
            throw new ApplicationException("权限名称已存在");
        userAuthority.SoftDeleteLock = false;
        userAuthority.StateGuid = (await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == 1)).BaseId;
        var result = await _IUserAuthorityRepository.CreateAsync(userAuthority);
        return result;
    }

    public async Task<bool> DeleteUserAuthorityAsync(Guid guid)
    {
        var userAuthority = await _IUserAuthorityRepository.FindAsync(guid);
        if (userAuthority == null)
            throw new ApplicationException("权限不存在");
        if ((await _IUserAuthorityRepository.QueryAsync(e => e.ParentId == guid)).Count != 0)
            throw new ApplicationException("该权限下有子权限，不能删除");
        if ((await _IUserRoleAuthorityRepository.QueryAsync(e => e.AuditId == userAuthority.BaseId)).Count != 0)
            throw new ApplicationException("该权限已有角色使用，不能删除");
        if (userAuthority.SoftDeleteLock)
        {
            userAuthority.StateGuid = (await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == 0)).BaseId;
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
        if (await _IUserAuthorityRepository.FindAsync(ua => ua.Name == userAuthority.Name) != null)
            throw new ApplicationException("权限名称已存在");
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
        var userAuthority = from userauthority in await _IUserAuthorityRepository.QueryAsync()
                            join rootstate in await _IRootStateRepository.QueryAsync() on userauthority.StateGuid equals rootstate.BaseId
                            where rootstate.StateKey == 1
                            orderby userauthority.ParentId descending
                            orderby userauthority.CreateTime descending
                            orderby userauthority.Name descending
                            select userauthority;
        return userAuthority.ToList();
    }
}