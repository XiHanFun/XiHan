// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserAccountService
// Guid:095a90e6-1cd8-0a94-f223-a288beff4acd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-06 下午 10:31:05
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// IUserAccountService
/// </summary>
public interface IUserAccountService : IBaseService<UserAccount>
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<UserAccount> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="userAccounts"></param>
    /// <returns></returns>
    Task<bool> InitUserAccountAsync(List<UserAccount> userAccounts);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="userAccount"></param>
    /// <returns></returns>
    Task<bool> CreateUserAccountAsync(UserAccount userAccount);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteUserAccountAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="userAccount"></param>
    /// <returns></returns>
    Task<UserAccount> ModifyUserAccountAsync(UserAccount userAccount);

    /// <summary>
    /// Guid查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<UserAccount> FindUserAccountByGuidAsync(Guid guid);

    /// <summary>
    /// 用户名查找
    /// </summary>
    /// <param name="accountName"></param>
    /// <returns></returns>
    Task<UserAccount> FindUserAccountByNameAsync(string accountName);

    /// <summary>
    /// 邮箱查找
    /// </summary>
    /// <param name="accountEmail"></param>
    /// <returns></returns>
    Task<UserAccount> FindUserAccountByEmailAsync(string accountEmail);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<UserAccount>> QueryUserAccountAsync();
}