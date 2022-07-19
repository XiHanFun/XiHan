// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAccountService
// Guid:514a3309-e0b7-4b94-ae33-07f9ed9c1d55
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:03:32
// ----------------------------------------------------------------

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

    public UserAccountService(IRootStateRepository iRootStateRepository, IUserAccountRepository iUserAccountRepository, IUserAccountRoleRepository iIUserAccountRoleRepository)
    {
        base._IBaseRepository = iUserAccountRepository;
        _IRootStateRepository = iRootStateRepository;
        _IUserAccountRepository = iUserAccountRepository;
        _IUserAccountRoleRepository = iIUserAccountRoleRepository;
    }

    public async Task<bool> CreateUserAccountAsync(UserAccount userAccount)
    {
        if (await _IUserAccountRepository.FindAsync(ua => ua.Name == userAccount.Name || ua.Email == userAccount.Email) != null)
            throw new ApplicationException("账户名称或邮箱已注册");
        userAccount.SoftDeleteLock = true;
        userAccount.StateGuid = (await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == 1)).BaseId;
        var result = await _IUserAccountRepository.CreateAsync(userAccount);
        return result;
    }

    public async Task<bool> DeleteUserAccountAsync(Guid guid)
    {
        var userAccount = await _IUserAccountRepository.FindAsync(guid);
        if (userAccount == null)
            throw new ApplicationException("账户不存在");
        if (userAccount.SoftDeleteLock)
        {
            userAccount.StateGuid = (await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == 0)).BaseId;
            userAccount.DeleteTime = DateTime.Now;
            return await _IUserAccountRepository.UpdateAsync(userAccount);
        }
        else
        {
            return await _IUserAccountRepository.DeleteAsync(guid);
        }
    }

    public async Task<UserAccount> ModifyUserAccountAsync(UserAccount userAccount)
    {
        if (await _IUserAccountRepository.FindAsync(userAccount.BaseId) == null)
            throw new ApplicationException("账户不存在");
        userAccount.ModifyTime = DateTime.Now;
        var result = await _IUserAccountRepository.UpdateAsync(userAccount);
        if (result) userAccount = await _IUserAccountRepository.FindAsync(userAccount.BaseId);
        return userAccount;
    }

    public async Task<UserAccount> FindUserAccountByGuidAsync(Guid guid)
    {
        var userAccount = await _IUserAccountRepository.FindAsync(guid);
        return userAccount;
    }

    public async Task<UserAccount> FindUserAccountByNameAsync(string accountName)
    {
        var userAccount = await _IUserAccountRepository.FindAsync(ua => ua.Name == accountName);
        return userAccount;
    }

    public async Task<UserAccount> FindUserAccountByEmailAsync(string accountEmail)
    {
        var userAccount = await _IUserAccountRepository.FindAsync(ua => ua.Email == accountEmail);
        return userAccount;
    }

    public async Task<List<UserAccount>> QueryUserAccountsAsync()
    {
        var userAccount = from userauthority in await _IUserAccountRepository.QueryAsync()
                          join rootstate in await _IRootStateRepository.QueryAsync() on userauthority.StateGuid equals rootstate.BaseId
                          where rootstate.StateKey == 1
                          orderby userauthority.CreateTime descending
                          orderby userauthority.Name descending
                          select userauthority;
        return userAccount.ToList();
    }
}