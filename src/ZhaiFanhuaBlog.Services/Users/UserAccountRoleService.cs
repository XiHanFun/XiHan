#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAccountRoleService
// Guid:ebcc7798-6eb8-4aa4-b489-17c9eab06f6f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:03:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Users;
using ZhaiFanhuaBlog.Services.Bases;
using ZhaiFanhuaBlog.Services.Roots;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// UserAccountRoleService
/// </summary>
public class UserAccountRoleService : BaseService<UserAccountRole>, IUserAccountRoleService
{
    private readonly IUserAccountService _IUserAccountService;
    private readonly IRootRoleService _IRootRoleService;
    private readonly IUserAccountRoleRepository _IUserAccountRoleRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iUserAccountService"></param>
    /// <param name="iRootRoleService"></param>
    /// <param name="iUserAccountRoleRepository"></param>
    public UserAccountRoleService(IUserAccountService iUserAccountService,
        IRootRoleService iRootRoleService,
        IUserAccountRoleRepository iUserAccountRoleRepository)
    {
        _IBaseRepository = iUserAccountRoleRepository;
        _IUserAccountService = iUserAccountService;
        _IRootRoleService = iRootRoleService;
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
        {
            throw new ApplicationException("用户账户角色不存在");
        }
        return userAccountRole;
    }

    /// <summary>
    /// 初始化用户账户角色关联
    /// </summary>
    /// <param name="userAccountRoles"></param>
    /// <returns></returns>
    public async Task<bool> InitUserAccountRoleAsync(List<UserAccountRole> userAccountRoles)
    {
        userAccountRoles.ForEach(userAccountRole =>
        {
            userAccountRole.SoftDeleteLock = false;
        });
        var result = await _IUserAccountRoleRepository.CreateBatchAsync(userAccountRoles);
        return result;
    }

    /// <summary>
    /// 新增用户账户角色关联
    /// </summary>
    /// <param name="userAccountRole"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> CreateUserAccountRoleAsync(UserAccountRole userAccountRole)
    {
        await _IUserAccountService.IsExistenceAsync(userAccountRole.AccountId);
        await _IRootRoleService.IsExistenceAsync(userAccountRole.RoleId);
        if (await _IUserAccountRoleRepository.FindAsync(e => e.AccountId == userAccountRole.AccountId && e.RoleId == userAccountRole.RoleId && !e.SoftDeleteLock) != null)
        {
            throw new ApplicationException("用户账户角色已存在");
        }
        userAccountRole.SoftDeleteLock = false;
        var result = await _IUserAccountRoleRepository.CreateAsync(userAccountRole);
        return result;
    }

    /// <summary>
    /// 删除用户账户角色关联
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteUserAccountRoleAsync(Guid guid, Guid deleteId)
    {
        var userAccountRole = await IsExistenceAsync(guid);
        userAccountRole.SoftDeleteLock = true;
        userAccountRole.DeleteId = deleteId;
        userAccountRole.DeleteTime = DateTime.Now;
        return await _IUserAccountRoleRepository.UpdateAsync(userAccountRole);
    }

    /// <summary>
    /// 修改用户账户角色关联
    /// </summary>
    /// <param name="userAccountRole"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<UserAccountRole> ModifyUserAccountRoleAsync(UserAccountRole userAccountRole)
    {
        await IsExistenceAsync(userAccountRole.BaseId);
        await _IUserAccountService.IsExistenceAsync(userAccountRole.AccountId);
        await _IRootRoleService.IsExistenceAsync(userAccountRole.RoleId);
        if (await _IUserAccountRoleRepository.FindAsync(e => e.AccountId == userAccountRole.AccountId && e.RoleId == userAccountRole.RoleId && !e.SoftDeleteLock) != null)
        {
            throw new ApplicationException("用户账户角色已存在");
        }
        userAccountRole.ModifyTime = DateTime.Now;
        var result = await _IUserAccountRoleRepository.UpdateAsync(userAccountRole);
        if (result) userAccountRole = await _IUserAccountRoleRepository.FindAsync(userAccountRole.BaseId);
        return userAccountRole;
    }

    /// <summary>
    /// 查找用户账户角色关联
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<UserAccountRole> FindUserAccountRoleAsync(Guid guid)
    {
        var userAccountRole = await IsExistenceAsync(guid);
        return userAccountRole;
    }

    /// <summary>
    /// 查询用户账户角色关联
    /// </summary>
    /// <returns></returns>
    public async Task<List<UserAccountRole>> QueryUserAccountRoleAsync()
    {
        var userAccountRole = from useraccountrole in await _IUserAccountRoleRepository.QueryListAsync(e => !e.SoftDeleteLock)
                              orderby useraccountrole.CreateTime descending
                              select useraccountrole;
        return userAccountRole.ToList();
    }
}