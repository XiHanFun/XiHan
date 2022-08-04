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

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<UserRole> IsExistenceAsync(Guid guid)
    {
        var userRole = await _IUserRoleRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (userRole == null)
            throw new ApplicationException("用户角色不存在");
        return userRole;
    }

    public async Task<bool> InitUserRoleAsync(List<UserRole> userRoles)
    {
        userRoles.ForEach(userRole =>
        {
            userRole.SoftDeleteLock = false;
        });
        var result = await _IUserRoleRepository.CreateBatchAsync(userRoles);
        return result;
    }

    public async Task<bool> CreateUserRoleAsync(UserRole userRole)
    {
        if (userRole.ParentId != null && await _IUserRoleRepository.FindAsync(e => e.ParentId == userRole.ParentId && !e.SoftDeleteLock) == null)
            throw new ApplicationException("父级用户角色不存在");
        if (await _IUserRoleRepository.FindAsync(e => e.Name == userRole.Name) != null)
            throw new ApplicationException("用户角色名称已存在");
        userRole.SoftDeleteLock = false;
        var result = await _IUserRoleRepository.CreateAsync(userRole);
        return result;
    }

    public async Task<bool> DeleteUserRoleAsync(Guid guid, Guid deleteId)
    {
        var userRole = await IsExistenceAsync(guid);
        if ((await _IUserRoleRepository.QueryListAsync(e => e.ParentId == userRole.ParentId && !e.SoftDeleteLock)).Count != 0)
            throw new ApplicationException("该用户角色下有子用户角色，不能删除");
        if ((await _IUserAccountRoleRepository.QueryListAsync(e => e.RoleId == userRole.BaseId)).Count != 0)
            throw new ApplicationException("该用户角色已有用户账户使用，不能删除");
        var rootState = await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == -1);
        userRole.SoftDeleteLock = true;
        userRole.DeleteId = deleteId;
        userRole.DeleteTime = DateTime.Now;
        userRole.StateId = rootState.BaseId;
        return await _IUserRoleRepository.UpdateAsync(userRole);
    }

    public async Task<UserRole> ModifyUserRoleAsync(UserRole userRole)
    {
        await IsExistenceAsync(userRole.BaseId);
        if (userRole.ParentId != null && await _IUserRoleRepository.FindAsync(e => e.ParentId == userRole.ParentId && !e.SoftDeleteLock) == null)
            throw new ApplicationException("父级用户角色不存在");
        if (await _IUserRoleRepository.FindAsync(e => e.Name == userRole.Name) != null)
            throw new ApplicationException("用户角色名称已存在");
        var result = await _IUserRoleRepository.UpdateAsync(userRole);
        if (result) userRole = await _IUserRoleRepository.FindAsync(userRole.BaseId);
        return userRole;
    }

    public async Task<UserRole> FindUserRoleAsync(Guid guid)
    {
        var userRole = await IsExistenceAsync(guid);
        return userRole;
    }

    public async Task<List<UserRole>> QueryUserRoleAsync()
    {
        var userRole = from userrole in await _IUserRoleRepository.QueryListAsync(e => !e.SoftDeleteLock)
                       orderby userrole.CreateTime descending
                       orderby userrole.Name descending
                       select userrole;
        return userRole.ToList();
    }
}