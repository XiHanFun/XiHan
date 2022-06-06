// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserRoleService
// Guid:16ffe501-5310-4ea5-b534-97f826f7c04c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:02:45
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// UserRoleService
/// </summary>
public class UserRoleService : BaseService<UserRole>, IUserRoleService
{
    private readonly IUserRoleRepository _IUserRoleRepository;

    public UserRoleService(IUserRoleRepository iUserRoleRepository)
    {
        _IUserRoleRepository = iUserRoleRepository;
        base._iBaseRepository = iUserRoleRepository;
    }
}