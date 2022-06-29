// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserRoleAuthorityService
// Guid:fa73716d-d139-4da8-9eda-e6aca30bd5d0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:03:43
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// UserRoleAuthorityService
/// </summary>
public class UserRoleAuthorityService : BaseService<UserRoleAuthority>, IUserRoleAuthorityService
{
    private readonly IUserRoleAuthorityRepository _IUserRoleAuthorityRepository;

    public UserRoleAuthorityService(IUserRoleAuthorityRepository iUserRoleAuthorityRepository)
    {
        _IUserRoleAuthorityRepository = iUserRoleAuthorityRepository;
        base._IBaseRepository = iUserRoleAuthorityRepository;
    }
}