// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteConfigurationService
// Guid:0b165e45-47db-4582-9bdc-cf5260138950
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 05:26:41
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Sites;
using ZhaiFanhuaBlog.IServices.Sites;
using ZhaiFanhuaBlog.Models.Sites;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Sites;

/// <summary>
/// SiteConfigurationService
/// </summary>
public class SiteConfigurationService : BaseService<SiteConfiguration>, ISiteConfigurationService
{
    private readonly ISiteConfigurationRepository _ISiteConfigurationRepository;

    public SiteConfigurationService(ISiteConfigurationRepository iSiteConfigurationRepository)
    {
        _ISiteConfigurationRepository = iSiteConfigurationRepository;
        base._IBaseRepository = iSiteConfigurationRepository;
    }
}