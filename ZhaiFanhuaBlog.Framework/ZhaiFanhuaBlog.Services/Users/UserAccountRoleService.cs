// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAccountRoleService
// Guid:ebcc7798-6eb8-4aa4-b489-17c9eab06f6f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:03:54
// ----------------------------------------------------------------

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
    private readonly IUserAccountRoleRepository _IUserAccountRoleRepository;

    public UserAccountRoleService(IUserAccountRoleRepository iUserAccountRoleRepository)
    {
        _IUserAccountRoleRepository = iUserAccountRoleRepository;
        base._iBaseRepository = iUserAccountRoleRepository;
    }
}