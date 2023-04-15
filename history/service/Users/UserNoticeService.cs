#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserNoticeService
// Guid:18702c40-dd10-4c73-b42a-93cd6a906800
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:04:38
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// 用户通知
/// </summary>
public class UserNoticeService : BaseService<UserNotice>, IUserNoticeService
{
    private readonly IUserNoticeRepository _IUserNoticeRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iUserNoticeRepository"></param>
    public UserNoticeService(IUserNoticeRepository iUserNoticeRepository)
    {
        _IUserNoticeRepository = iUserNoticeRepository;
        base._IBaseRepository = iUserNoticeRepository;
    }
}