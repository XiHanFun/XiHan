// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserCollectCategoryService
// Guid:8d79d8c4-0bab-4ab2-8559-70eee3c88d04
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:04:05
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// UserCollectCategoryService
/// </summary>
public class UserCollectCategoryService : BaseService<UserCollectCategory>, IUserCollectCategoryService
{
    private readonly IUserCollectCategoryRepository _IUserCollectCategoryRepository;

    public UserCollectCategoryService(IUserCollectCategoryRepository iUserCollectCategoryRepository)
    {
        _IUserCollectCategoryRepository = iUserCollectCategoryRepository;
        base._iBaseRepository = iUserCollectCategoryRepository;
    }
}