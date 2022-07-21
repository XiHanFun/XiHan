// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserRoleService
// Guid:16ffe501-5310-4ea5-b534-97f826f7c04c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:02:45
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// UserRoleService
/// </summary>
public class UserRoleService : BaseService<UserRole>, IUserRoleService
{
    private readonly IRootStateRepository _IRootStateRepository;
    private readonly IUserRoleRepository _IUserRoleRepository;
    private readonly IUserAccountRoleRepository _IUserAccountRoleRepository;

    public UserRoleService(IRootStateRepository iRootStateRepository,
        IUserRoleRepository iUserRoleRepository,
        IUserAccountRoleRepository iUserAccountRoleRepository)
    {
        base._IBaseRepository = iUserRoleRepository;
        _IRootStateRepository = iRootStateRepository;
        _IUserRoleRepository = iUserRoleRepository;
        _IUserAccountRoleRepository = iUserAccountRoleRepository;
    }

    public async Task<bool> CreateUserRoleAsync(UserRole userRole)
    {
        if (userRole.ParentId != null && await _IUserRoleRepository.FindAsync(userRole.ParentId) == null)
            throw new ApplicationException("父级角色不存在");
        if (await _IUserRoleRepository.FindAsync(ur => ur.Name == userRole.Name) != null)
            throw new ApplicationException("角色名称已存在");
        userRole.SoftDeleteLock = false;
        userRole.StateGuid = (await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == 1)).BaseId;
        var result = await _IUserRoleRepository.CreateAsync(userRole);
        return result;
    }

    public async Task<bool> DeleteUserRoleAsync(Guid guid)
    {
        var userRole = await _IUserRoleRepository.FindAsync(guid);
        if (userRole == null)
            throw new ApplicationException("角色不存在");
        if ((await _IUserRoleRepository.QueryAsync(e => e.ParentId == guid)).Count != 0)
            throw new ApplicationException("该角色下有子角色，不能删除");
        if ((await _IUserAccountRoleRepository.QueryAsync(e => e.RoleId == userRole.BaseId)).Count != 0)
            throw new ApplicationException("该角色已有账户使用，不能删除");
        if (userRole.SoftDeleteLock)
        {
            userRole.StateGuid = (await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == 0)).BaseId;
            userRole.DeleteTime = DateTime.Now;
            return await _IUserRoleRepository.UpdateAsync(userRole);
        }
        else
        {
            return await _IUserRoleRepository.DeleteAsync(guid);
        }
    }

    public async Task<UserRole> ModifyUserRoleAsync(UserRole userRole)
    {
        if (await _IUserRoleRepository.FindAsync(userRole.BaseId) == null)
            throw new ApplicationException("角色不存在");
        if (userRole.ParentId != null && await _IUserRoleRepository.FindAsync(userRole.ParentId) == null)
            throw new ApplicationException("父级角色不存在");
        if (await _IUserRoleRepository.FindAsync(ur => ur.Name == userRole.Name) != null)
            throw new ApplicationException("角色名称已存在");
        userRole.ModifyTime = DateTime.Now;
        var result = await _IUserRoleRepository.UpdateAsync(userRole);
        if (result) userRole = await _IUserRoleRepository.FindAsync(userRole.BaseId);
        return userRole;
    }

    public async Task<UserRole> FindUserRoleAsync(Guid guid)
    {
        var userRole = await _IUserRoleRepository.FindAsync(guid);
        return userRole;
    }

    public async Task<List<UserRole>> QueryUserRolesAsync()
    {
        var userRole = from userrole in await _IUserRoleRepository.QueryAsync()
                       join rootstate in await _IRootStateRepository.QueryAsync() on userrole.StateGuid equals rootstate.BaseId
                       where rootstate.StateKey == 1
                       orderby userrole.ParentId descending
                       orderby userrole.CreateTime descending
                       orderby userrole.Name descending
                       select userrole;
        return userRole.ToList();
    }
}