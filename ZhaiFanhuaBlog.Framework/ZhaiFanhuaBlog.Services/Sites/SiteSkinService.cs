// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteSkinService
// Guid:395446ac-3ea4-48a9-9f80-d5b88ee15c7c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 05:31:26
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Sites;
using ZhaiFanhuaBlog.IServices.Sites;
using ZhaiFanhuaBlog.Models.Sites;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Sites;

/// <summary>
/// SiteSkinService
/// </summary>
public class SiteSkinService : BaseService<SiteSkin>, ISiteSkinService
{
    private readonly ISiteSkinRepository _ISiteSkinRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iSiteSkinRepository"></param>
    public SiteSkinService(ISiteSkinRepository iSiteSkinRepository)
    {
        _ISiteSkinRepository = iSiteSkinRepository;
        base._IBaseRepository = iSiteSkinRepository;
    }
}