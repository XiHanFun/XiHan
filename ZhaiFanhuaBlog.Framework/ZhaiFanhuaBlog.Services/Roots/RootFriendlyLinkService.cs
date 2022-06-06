// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootFriendlyLinkService
// Guid:87c41ce3-1cfb-45c9-b6e9-be84c35c708f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 05:37:29
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IServices.Roots;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// RootFriendlyLinkService
/// </summary>
public class RootFriendlyLinkService : BaseService<RootFriendlyLink>, IRootFriendlyLinkService
{
    private readonly IRootFriendlyLinkRepository _IRootFriendlyLinkRepository;

    public RootFriendlyLinkService(IRootFriendlyLinkRepository iRootFriendlyLinkRepository)
    {
        _IRootFriendlyLinkRepository = iRootFriendlyLinkRepository;
        _iBaseRepository = iRootFriendlyLinkRepository;
    }
}