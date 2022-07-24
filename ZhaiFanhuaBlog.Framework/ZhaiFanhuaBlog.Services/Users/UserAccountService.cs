// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAccountService
// Guid:514a3309-e0b7-4b94-ae33-07f9ed9c1d55
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:03:32
// ----------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// UserAccountService
/// </summary>
public class UserAccountService : BaseService<UserAccount>, IUserAccountService
{
    private readonly IRootStateRepository _IRootStateRepository;
    private readonly IUserAccountRepository _IUserAccountRepository;
    private readonly IUserAccountRoleRepository _IUserAccountRoleRepository;

    public UserAccountService(IConfiguration iConfiguration,
        IRootStateRepository iRootStateRepository,
        IUserAccountRepository iUserAccountRepository,
        IUserAccountRoleRepository iIUserAccountRoleRepository)
    {
        base._IBaseRepository = iUserAccountRepository;
        _IRootStateRepository = iRootStateRepository;
        _IUserAccountRepository = iUserAccountRepository;
        _IUserAccountRoleRepository = iIUserAccountRoleRepository;
    }

    public async Task<bool> InitUserAccountAsync(List<UserAccount> userAccounts)
    {
        userAccounts.ForEach(userAccount =>
        {
            userAccount.SoftDeleteLock = false;
        });
        var result = await _IUserAccountRepository.CreateBatchAsync(userAccounts);
        return result;
    }

    public async Task<bool> CreateUserAccountAsync(UserAccount userAccount)
    {
        if (await _IUserAccountRepository.FindAsync(ua => ua.Name == userAccount.Name || ua.Email == userAccount.Email && ua.SoftDeleteLock == false) != null)
            throw new ApplicationException("用户账户名称或邮箱已注册");
        userAccount.SoftDeleteLock = false;
        var result = await _IUserAccountRepository.CreateAsync(userAccount);
        return result;
    }

    public async Task<bool> DeleteUserAccountAsync(Guid guid, Guid deleteId)
    {
        var userAccount = await _IUserAccountRepository.FindAsync(guid);
        if (userAccount == null)
            throw new ApplicationException("用户账户不存在");
        var rootState = await _IRootStateRepository.FindAsync(rs => rs.TypeKey == "All" && rs.StateKey == -1);
        userAccount.SoftDeleteLock = true;
        userAccount.DeleteId = deleteId;
        userAccount.DeleteTime = DateTime.Now;
        userAccount.StateId = rootState.BaseId;
        return await _IUserAccountRepository.DeleteAsync(guid);
    }

    public async Task<UserAccount> ModifyUserAccountAsync(UserAccount userAccount)
    {
        if (await _IUserAccountRepository.FindAsync(userAccount.BaseId) == null)
            throw new ApplicationException("用户账户不存在");
        userAccount.ModifyTime = DateTime.Now;
        var result = await _IUserAccountRepository.UpdateAsync(userAccount);
        if (result) userAccount = await _IUserAccountRepository.FindAsync(userAccount.BaseId);
        return userAccount;
    }

    public async Task<UserAccount> FindUserAccountByGuidAsync(Guid guid)
    {
        var userAccount = await _IUserAccountRepository.FindAsync(ua => ua.BaseId == guid && ua.SoftDeleteLock == false);
        if (userAccount == null)
            throw new ApplicationException("用户账户不存在");
        return userAccount;
    }

    public async Task<UserAccount> FindUserAccountByNameAsync(string accountName)
    {
        var userAccount = await _IUserAccountRepository.FindAsync(ua => ua.Name == accountName && ua.SoftDeleteLock == false);
        if (userAccount == null)
            throw new ApplicationException("用户账户不存在");
        return userAccount;
    }

    public async Task<UserAccount> FindUserAccountByEmailAsync(string accountEmail)
    {
        var userAccount = await _IUserAccountRepository.FindAsync(ua => ua.Email == accountEmail && ua.SoftDeleteLock == false);
        if (userAccount == null)
            throw new ApplicationException("用户账户不存在");
        return userAccount;
    }

    public async Task<List<UserAccount>> QueryUserAccountAsync()
    {
        var userAccount = from userauthority in await _IUserAccountRepository.QueryAsync(ua => ua.SoftDeleteLock == false)
                          orderby userauthority.CreateTime descending
                          orderby userauthority.Name descending
                          select userauthority;
        return userAccount.ToList();
    }
}