#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteLogService
// Guid:05457439-9e81-4b06-b601-26099de73285
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 05:29:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Sites;
using ZhaiFanhuaBlog.Repositories.Sites;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Sites;

/// <summary>
/// SiteLogService
/// </summary>
public class SiteLogService : BaseService<SiteLog>, ISiteLogService
{
    private readonly ISiteLogRepository _ISiteLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iSiteLogRepository"></param>
    public SiteLogService(ISiteLogRepository iSiteLogRepository)
    {
        _ISiteLogRepository = iSiteLogRepository;
        base._IBaseRepository = iSiteLogRepository;
    }
}