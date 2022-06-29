// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserNoticeService
// Guid:18702c40-dd10-4c73-b42a-93cd6a906800
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:04:38
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// UserNoticeService
/// </summary>
public class UserNoticeService : BaseService<UserNotice>, IUserNoticeService
{
    private readonly IUserNoticeRepository _IUserNoticeRepository;

    public UserNoticeService(IUserNoticeRepository iUserNoticeRepository)
    {
        _IUserNoticeRepository = iUserNoticeRepository;
        base._IBaseRepository = iUserNoticeRepository;
    }
}