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
    private readonly IRootStateRepository _IRootStateRepository;
    private readonly IUserAccountRepository _IUserAccountRepository;
    private readonly IUserRoleRepository _IUserRoleRepository;
    private readonly IUserAccountRoleRepository _IUserAccountRoleRepository;

    public UserAccountRoleService(IRootStateRepository iRootStateRepository,
        IUserAccountRepository iUserAccountRepository,
        IUserRoleRepository iUserRoleRepository,
        IUserAccountRoleRepository iUserAccountRoleRepository)
    {
        base._IBaseRepository = iUserAccountRoleRepository;
        _IRootStateRepository = iRootStateRepository;
        _IUserAccountRepository = iUserAccountRepository;
        _IUserRoleRepository = iUserRoleRepository;
        _IUserAccountRoleRepository = iUserAccountRoleRepository;
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
        if (await _IUserAccountRepository.FindAsync(ua => ua.BaseId == userAccountRole.AccountId && ua.SoftDeleteLock == false) == null)
            throw new ApplicationException("用户账户不存在");
        if (await _IUserRoleRepository.FindAsync(ur => ur.BaseId == userAccountRole.RoleId && ur.SoftDeleteLock == false) == null)
            throw new ApplicationException("用户角色不存在");
        if (await _IUserAccountRoleRepository.FindAsync(uar => uar.AccountId == userAccountRole.AccountId && uar.RoleId == userAccountRole.RoleId && uar.SoftDeleteLock == false) != null)
            throw new ApplicationException("用户账户角色已存在");
        userAccountRole.SoftDeleteLock = false;
        var result = await _IUserAccountRoleRepository.CreateAsync(userAccountRole);
        return result;
    }

    public async Task<bool> DeleteUserAccountRoleAsync(Guid guid, Guid deleteId)
    {
        var userAccountRole = await _IUserAccountRoleRepository.FindAsync(ua => ua.BaseId == guid && ua.SoftDeleteLock == false);
        if (userAccountRole == null)
            throw new ApplicationException("用户账户角色不存在");
        userAccountRole.SoftDeleteLock = true;
        userAccountRole.DeleteId = deleteId;
        userAccountRole.DeleteTime = DateTime.Now;
        return await _IUserAccountRoleRepository.UpdateAsync(userAccountRole);
    }

    public async Task<UserAccountRole> ModifyUserAccountRoleAsync(UserAccountRole userAccountRole)
    {
        if (await _IUserAccountRoleRepository.FindAsync(uar => uar.BaseId == userAccountRole.BaseId && uar.SoftDeleteLock == false) == null)
            throw new ApplicationException("用户账户角色不存在");
        if (await _IUserAccountRepository.FindAsync(ua => ua.BaseId == userAccountRole.AccountId && ua.SoftDeleteLock == false) == null)
            throw new ApplicationException("用户账户不存在");
        if (await _IUserRoleRepository.FindAsync(ur => ur.BaseId == userAccountRole.RoleId && ur.SoftDeleteLock == false) == null)
            throw new ApplicationException("用户角色不存在");
        if (await _IUserAccountRoleRepository.FindAsync(uar => uar.AccountId == userAccountRole.AccountId && uar.RoleId == userAccountRole.RoleId && uar.SoftDeleteLock == false) != null)
            throw new ApplicationException("用户账户角色已存在");
        userAccountRole.ModifyTime = DateTime.Now;
        var result = await _IUserAccountRoleRepository.UpdateAsync(userAccountRole);
        if (result) userAccountRole = await _IUserAccountRoleRepository.FindAsync(userAccountRole.BaseId);
        return userAccountRole;
    }

    public async Task<UserAccountRole> FindUserAccountRoleAsync(Guid guid)
    {
        var userAccountRole = await _IUserAccountRoleRepository.FindAsync(uar => uar.BaseId == guid && uar.SoftDeleteLock == false);
        if (userAccountRole == null)
            throw new ApplicationException("用户账户角色不存在");
        return userAccountRole;
    }

    public async Task<List<UserAccountRole>> QueryUserAccountRoleAsync()
    {
        var userAccountRole = from useraccountrole in await _IUserAccountRoleRepository.QueryAsync(uar => uar.SoftDeleteLock == false)
                              orderby useraccountrole.CreateTime descending
                              select useraccountrole;
        return userAccountRole.ToList();
    }
}