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
using ZhaiFanhuaBlog.Models.Roots;
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

    public UserRoleService(IRootStateRepository iRootStateRepository, IUserRoleRepository iUserRoleRepository)
    {
        _IRootStateRepository = iRootStateRepository;
        _IUserRoleRepository = iUserRoleRepository;
        base._IBaseRepository = iUserRoleRepository;
    }

    public async Task<bool> CreateUserRoleAsync(UserRole userRole)
    {
        if (userRole.ParentId != null && await _IUserRoleRepository.FindAsync(userRole.ParentId) == null)
            throw new ApplicationException("父级角色不存在");
        RootState rootState = new RootState();
        userRole.SoftDeleteLock = false;
        var result = await _IUserRoleRepository.CreateAsync(userRole);
        return result;
    }

    public async Task<bool> DeleteUserRoleAsync(Guid guid)
    {
        var userRole = await _IUserRoleRepository.FindAsync(guid);
        if (userRole == null)
            throw new ApplicationException("角色不存在");
        if (userRole.SoftDeleteLock)
        {
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
            throw new ApplicationException("权限不存在");
        if (userRole.ParentId != null && await _IUserRoleRepository.FindAsync(userRole.ParentId) == null)
            throw new ApplicationException("父级权限不存在");
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

    public async Task<List<UserRole>> QueryUserAuthoritiesAsync()
    {
        var userRole = from ur in await _IUserRoleRepository.QueryAsync()
                       where ur.DeleteTime != null
                       orderby ur.CreateTime descending
                       orderby ur.Name descending
                       select ur;
        return userRole.ToList();
    }
}