// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserStatisticService
// Guid:9a1cdf47-a302-4a2e-970d-bd58d7b706e6
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:04:54
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// UserStatisticService
/// </summary>
public class UserStatisticService : BaseService<UserStatistic>, IUserStatisticService
{
    private readonly IUserStatisticRepository _IUserStatisticRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iUserStatisticRepository"></param>
    public UserStatisticService(IUserStatisticRepository iUserStatisticRepository)
    {
        _IUserStatisticRepository = iUserStatisticRepository;
        base._IBaseRepository = iUserStatisticRepository;
    }
}