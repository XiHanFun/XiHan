// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserAccountService
// Guid:095a90e6-1cd8-0a94-f223-a288beff4acd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-06 下午 10:31:05
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Users;

namespace ZhaiFanhuaBlog.IServices.Users;

public interface IUserAccountService : IBaseService<UserAccount>
{
    Task<UserAccount> IsExistenceAsync(Guid guid);

    Task<bool> InitUserAccountAsync(List<UserAccount> userAccounts);

    Task<bool> CreateUserAccountAsync(UserAccount userAccount);

    Task<bool> DeleteUserAccountAsync(Guid guid, Guid deleteId);

    Task<UserAccount> ModifyUserAccountAsync(UserAccount userAccount);

    Task<UserAccount> FindUserAccountByGuidAsync(Guid guid);

    Task<UserAccount> FindUserAccountByNameAsync(string accountName);

    Task<UserAccount> FindUserAccountByEmailAsync(string accountEmail);

    Task<List<UserAccount>> QueryUserAccountAsync();
}