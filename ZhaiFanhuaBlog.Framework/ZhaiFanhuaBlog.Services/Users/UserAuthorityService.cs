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

    public UserAuthorityService(IUserAuthorityRepository iUserAuthorityRepository,
        IRootStateRepository iRootStateRepository,
        IUserRoleAuthorityRepository iUserRoleAuthorityRepository)
    {
        base._IBaseRepository = iUserAuthorityRepository;
        _IUserAuthorityRepository = iUserAuthorityRepository;
        _IRootStateRepository = iRootStateRepository;
        _IUserRoleAuthorityRepository = iUserRoleAuthorityRepository;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<UserAuthority> IsExistenceAsync(Guid guid)
    {
        var userAuthority = await _IUserAuthorityRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (userAuthority == null)
            throw new ApplicationException("用户权限不存在");
        return userAuthority;
    }

    public async Task<bool> InitUserAuthorityAsync(List<UserAuthority> userAuthorities)
    {
        userAuthorities.ForEach(userAuthority =>
        {
            userAuthority.SoftDeleteLock = false;
        });
        var result = await _IUserAuthorityRepository.CreateBatchAsync(userAuthorities);
        return result;
    }

    public async Task<bool> CreateUserAuthorityAsync(UserAuthority userAuthority)
    {
        if (userAuthority.ParentId != null && await _IUserAuthorityRepository.FindAsync(e => e.ParentId == userAuthority.ParentId && e.SoftDeleteLock == false) == null)
            throw new ApplicationException("父级用户权限不存在");
        if (await _IUserAuthorityRepository.FindAsync(e => e.Name == userAuthority.Name) != null)
            throw new ApplicationException("用户权限名称已存在");
        userAuthority.SoftDeleteLock = false;
        var result = await _IUserAuthorityRepository.CreateAsync(userAuthority);
        return result;
    }

    public async Task<bool> DeleteUserAuthorityAsync(Guid guid, Guid deleteId)
    {
        var userAuthority = await IsExistenceAsync(guid);
        if ((await _IUserAuthorityRepository.QueryListAsync(e => e.ParentId == userAuthority.ParentId && e.SoftDeleteLock == false)).Count != 0)
            throw new ApplicationException("该用户权限下有子用户权限，不能删除");
        if ((await _IUserRoleAuthorityRepository.QueryListAsync(e => e.AuthorityId == userAuthority.BaseId)).Count != 0)
            throw new ApplicationException("该用户权限已有用户角色使用，不能删除");
        var rootState = await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == -1);
        userAuthority.SoftDeleteLock = true;
        userAuthority.DeleteId = deleteId;
        userAuthority.DeleteTime = DateTime.Now;
        userAuthority.StateId = rootState.BaseId;
        return await _IUserAuthorityRepository.UpdateAsync(userAuthority);
    }

    public async Task<UserAuthority> ModifyUserAuthorityAsync(UserAuthority userAuthority)
    {
        await IsExistenceAsync(userAuthority.BaseId);
        if (userAuthority.ParentId != null && await _IUserAuthorityRepository.FindAsync(e => e.ParentId == userAuthority.ParentId && e.SoftDeleteLock == false) == null)
            throw new ApplicationException("父级用户权限不存在");
        if (await _IUserAuthorityRepository.FindAsync(e => e.Name == userAuthority.Name) != null)
            throw new ApplicationException("用户权限名称已存在");
        userAuthority.ModifyTime = DateTime.Now;
        var result = await _IUserAuthorityRepository.UpdateAsync(userAuthority);
        if (result) userAuthority = await _IUserAuthorityRepository.FindAsync(userAuthority.BaseId);
        return userAuthority;
    }

    public async Task<UserAuthority> FindUserAuthorityAsync(Guid guid)
    {
        var userAuthority = await IsExistenceAsync(guid);
        return userAuthority;
    }

    public async Task<List<UserAuthority>> QueryUserAuthorityAsync()
    {
        var userAuthority = from userauthority in await _IUserAuthorityRepository.QueryListAsync(e => e.SoftDeleteLock == false)
                            orderby userauthority.CreateTime descending
                            orderby userauthority.Name descending
                            select userauthority;
        return userAuthority.ToList();
    }
}