// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserCollectService
// Guid:58fd22ec-7e4a-4a07-85f8-095a09c2c7a0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:04:13
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// 用户收藏表
/// </summary>
public class UserCollectService : BaseService<UserCollect>, IUserCollectService
{
    private readonly IUserCollectRepository _IUserCollectRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iUserCollectRepository"></param>
    public UserCollectService(IUserCollectRepository iUserCollectRepository)
    {
        _IUserCollectRepository = iUserCollectRepository;
        base._IBaseRepository = iUserCollectRepository;
    }
}