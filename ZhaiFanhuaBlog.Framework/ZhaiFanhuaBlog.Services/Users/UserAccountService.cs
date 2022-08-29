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
/// 用户账户
/// </summary>
public class UserAccountService : BaseService<UserAccount>, IUserAccountService
{
    private readonly IRootStateRepository _IRootStateRepository;
    private readonly IUserAccountRepository _IUserAccountRepository;
    private readonly IUserAccountRoleRepository _IUserAccountRoleRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iConfiguration"></param>
    /// <param name="iRootStateRepository"></param>
    /// <param name="iUserAccountRepository"></param>
    /// <param name="iIUserAccountRoleRepository"></param>
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

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<UserAccount> IsExistenceAsync(Guid guid)
    {
        var userAccount = await _IUserAccountRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (userAccount == null)
            throw new ApplicationException("用户账户不存在");
        return userAccount;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="userAccounts"></param>
    /// <returns></returns>
    public async Task<bool> InitUserAccountAsync(List<UserAccount> userAccounts)
    {
        userAccounts.ForEach(userAccount =>
        {
            userAccount.SoftDeleteLock = false;
        });
        var result = await _IUserAccountRepository.CreateBatchAsync(userAccounts);
        return result;
    }

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="userAccount"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> CreateUserAccountAsync(UserAccount userAccount)
    {
        if (await _IUserAccountRepository.FindAsync(e => e.Name == userAccount.Name || e.Email == userAccount.Email && !e.SoftDeleteLock) != null)
            throw new ApplicationException("用户账户名称或邮箱已注册");
        userAccount.SoftDeleteLock = false;
        var result = await _IUserAccountRepository.CreateAsync(userAccount);
        return result;
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteUserAccountAsync(Guid guid, Guid deleteId)
    {
        var userAccount = await IsExistenceAsync(guid);
        var rootState = await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == -1);
        userAccount.SoftDeleteLock = true;
        userAccount.DeleteId = deleteId;
        userAccount.DeleteTime = DateTime.Now;
        userAccount.StateId = rootState.BaseId;
        return await _IUserAccountRepository.UpdateAsync(userAccount);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="userAccount"></param>
    /// <returns></returns>
    public async Task<UserAccount> ModifyUserAccountAsync(UserAccount userAccount)
    {
        await IsExistenceAsync(userAccount.BaseId);
        userAccount.ModifyTime = DateTime.Now;
        var result = await _IUserAccountRepository.UpdateAsync(userAccount);
        if (result) userAccount = await _IUserAccountRepository.FindAsync(userAccount.BaseId);
        return userAccount;
    }

    /// <summary>
    /// Guid查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<UserAccount> FindUserAccountByGuidAsync(Guid guid)
    {
        var userAccount = await IsExistenceAsync(guid);
        return userAccount;
    }

    /// <summary>
    /// 用户名查找
    /// </summary>
    /// <param name="accountName"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<UserAccount> FindUserAccountByNameAsync(string accountName)
    {
        var userAccount = await _IUserAccountRepository.FindAsync(e => e.Name == accountName && !e.SoftDeleteLock);
        if (userAccount == null)
            throw new ApplicationException("用户账户不存在");
        return userAccount;
    }

    /// <summary>
    /// 邮箱查找
    /// </summary>
    /// <param name="accountEmail"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<UserAccount> FindUserAccountByEmailAsync(string accountEmail)
    {
        var userAccount = await _IUserAccountRepository.FindAsync(e => e.Email == accountEmail && !e.SoftDeleteLock);
        if (userAccount == null)
            throw new ApplicationException("用户账户不存在");
        return userAccount;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    public async Task<List<UserAccount>> QueryUserAccountAsync()
    {
        var userAccount = from userauthority in await _IUserAccountRepository.QueryListAsync(e => !e.SoftDeleteLock)
                          orderby userauthority.CreateTime descending
                          orderby userauthority.Name descending
                          select userauthority;
        return userAccount.ToList();
    }
}