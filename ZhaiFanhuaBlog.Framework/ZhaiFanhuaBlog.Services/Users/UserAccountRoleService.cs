// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAccountRoleService
// Guid:ebcc7798-6eb8-4aa4-b489-17c9eab06f6f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:03:54
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// UserAccountRoleService
/// </summary>
public class UserAccountRoleService : BaseService<UserAccountRole>, IUserAccountRoleService
{
    private readonly IUserAccountService _IUserAccountService;
    private readonly IUserRoleService _IUserRoleService;
    private readonly IUserAccountRoleRepository _IUserAccountRoleRepository;

    public UserAccountRoleService(IUserAccountService iUserAccountService,
        IUserRoleService iUserRoleService,
        IUserAccountRoleRepository iUserAccountRoleRepository)
    {
        base._IBaseRepository = iUserAccountRoleRepository;
        _IUserAccountService = iUserAccountService;
        _IUserRoleService = iUserRoleService;
        _IUserAccountRoleRepository = iUserAccountRoleRepository;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<UserAccountRole> IsExistenceAsync(Guid guid)
    {
        var userAccountRole = await _IUserAccountRoleRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (userAccountRole == null)
            throw new ApplicationException("用户账户角色不存在");
        return userAccountRole;
    }

    public async Task<bool> InitUserAccountAsync(List<UserAccountRole> userAccountRoles)
    {
        userAccountRoles.ForEach(userAccountRole =>
        {
            userAccountRole.SoftDeleteLock = false;
        });
        var result = await _IUserAccountRoleRepository.CreateBatchAsync(userAccountRoles);
        return result;
    }

    public async Task<bool> CreateUserAccountRoleAsync(UserAccountRole userAccountRole)
    {
        await _IUserAccountService.IsExistenceAsync(userAccountRole.AccountId);
        await _IUserRoleService.IsExistenceAsync(userAccountRole.RoleId);
        if (await _IUserAccountRoleRepository.FindAsync(e => e.AccountId == userAccountRole.AccountId && e.RoleId == userAccountRole.RoleId && !e.SoftDeleteLock) != null)
            throw new ApplicationException("用户账户角色已存在");
        userAccountRole.SoftDeleteLock = false;
        var result = await _IUserAccountRoleRepository.CreateAsync(userAccountRole);
        return result;
    }

    public async Task<bool> DeleteUserAccountRoleAsync(Guid guid, Guid deleteId)
    {
        var userAccountRole = await IsExistenceAsync(guid);
        userAccountRole.SoftDeleteLock = true;
        userAccountRole.DeleteId = deleteId;
        userAccountRole.DeleteTime = DateTime.Now;
        return await _IUserAccountRoleRepository.UpdateAsync(userAccountRole);
    }

    public async Task<UserAccountRole> ModifyUserAccountRoleAsync(UserAccountRole userAccountRole)
    {
        await IsExistenceAsync(userAccountRole.BaseId);
        await _IUserAccountService.IsExistenceAsync(userAccountRole.AccountId);
        await _IUserRoleService.IsExistenceAsync(userAccountRole.RoleId);
        if (await _IUserAccountRoleRepository.FindAsync(e => e.AccountId == userAccountRole.AccountId && e.RoleId == userAccountRole.RoleId && !e.SoftDeleteLock) != null)
            throw new ApplicationException("用户账户角色已存在");
        userAccountRole.ModifyTime = DateTime.Now;
        var result = await _IUserAccountRoleRepository.UpdateAsync(userAccountRole);
        if (result) userAccountRole = await _IUserAccountRoleRepository.FindAsync(userAccountRole.BaseId);
        return userAccountRole;
    }

    public async Task<UserAccountRole> FindUserAccountRoleAsync(Guid guid)
    {
        var userAccountRole = await IsExistenceAsync(guid);
        return userAccountRole;
    }

    public async Task<List<UserAccountRole>> QueryUserAccountRoleAsync()
    {
        var userAccountRole = from useraccountrole in await _IUserAccountRoleRepository.QueryAsync(e => !e.SoftDeleteLock)
                              orderby useraccountrole.CreateTime descending
                              select useraccountrole;
        return userAccountRole.ToList();
    }
}