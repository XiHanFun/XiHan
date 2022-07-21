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

    public async Task<bool> CreateUserAccountRoleAsync(UserAccountRole userAccountRole)
    {
        if (await _IUserAccountRepository.FindAsync(userAccountRole.AccountId) == null)
            throw new ApplicationException("账户不存在");
        if (await _IUserRoleRepository.FindAsync(userAccountRole.RoleId) == null)
            throw new ApplicationException("角色不存在");
        if (await _IUserAccountRoleRepository.FindAsync(uar => uar.AccountId == userAccountRole.AccountId && uar.RoleId == userAccountRole.RoleId) != null)
            throw new ApplicationException("账户角色已存在");
        userAccountRole.SoftDeleteLock = false;
        userAccountRole.StateGuid = (await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == 1)).BaseId;
        var result = await _IUserAccountRoleRepository.CreateAsync(userAccountRole);
        return result;
    }

    public async Task<bool> DeleteUserAccountRoleAsync(Guid guid)
    {
        var userAccountRole = await _IUserAccountRoleRepository.FindAsync(guid);
        if (userAccountRole == null)
            throw new ApplicationException("账户角色不存在");
        if (userAccountRole.SoftDeleteLock)
        {
            userAccountRole.StateGuid = (await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == 0)).BaseId;
            userAccountRole.DeleteTime = DateTime.Now;
            return await _IUserAccountRoleRepository.UpdateAsync(userAccountRole);
        }
        else
        {
            return await _IUserAccountRoleRepository.DeleteAsync(guid);
        }
    }

    public async Task<UserAccountRole> ModifyUserAccountRoleAsync(UserAccountRole userAccountRole)
    {
        if (await _IUserAccountRoleRepository.FindAsync(userAccountRole.BaseId) == null)
            throw new ApplicationException("账户角色不存在");
        if (await _IUserAccountRepository.FindAsync(userAccountRole.AccountId) == null)
            throw new ApplicationException("账户不存在");
        if (await _IUserRoleRepository.FindAsync(userAccountRole.RoleId) == null)
            throw new ApplicationException("角色不存在");
        if (await _IUserAccountRoleRepository.FindAsync(uar => uar.AccountId == userAccountRole.AccountId && uar.RoleId == userAccountRole.RoleId) != null)
            throw new ApplicationException("账户角色已存在");
        userAccountRole.ModifyTime = DateTime.Now;
        var result = await _IUserAccountRoleRepository.UpdateAsync(userAccountRole);
        if (result) userAccountRole = await _IUserAccountRoleRepository.FindAsync(userAccountRole.BaseId);
        return userAccountRole;
    }

    public async Task<UserAccountRole> FindUserAccountRoleAsync(Guid guid)
    {
        var userAccountRole = await _IUserAccountRoleRepository.FindAsync(guid);
        return userAccountRole;
    }

    public async Task<List<UserAccountRole>> QueryUserAccountRolesAsync()
    {
        var userAccountRoles = from useraccountroles in await _IUserAccountRoleRepository.QueryAsync()
                               join rootstates in await _IRootStateRepository.QueryAsync() on useraccountroles.StateGuid equals rootstates.BaseId
                               join useraccounts in await _IUserAccountRepository.QueryAsync() on useraccountroles.AccountId equals useraccounts.BaseId
                               join userroles in await _IUserRoleRepository.QueryAsync() on useraccountroles.RoleId equals userroles.BaseId
                               where rootstates.StateKey.Equals(1)
                               orderby useraccountroles.CreateTime descending
                               select useraccountroles;
        return userAccountRoles.ToList();
    }
}