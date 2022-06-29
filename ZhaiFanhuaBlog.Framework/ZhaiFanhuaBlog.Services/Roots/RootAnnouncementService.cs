// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootAnnouncementService
// Guid:32e8ccae-f1d8-4ba8-aee2-5383aa3dd3b4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 05:32:59
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IServices.Roots;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// RootAnnouncementService
/// </summary>
public class RootAnnouncementService : BaseService<RootAnnouncement>, IRootAnnouncementService
{
    private readonly IRootAnnouncementRepository _IRootAnnouncementRepository;

    public RootAnnouncementService(IRootAnnouncementRepository iRootAnnouncementRepository)
    {
        _IRootAnnouncementRepository = iRootAnnouncementRepository;
        base._IBaseRepository = iRootAnnouncementRepository;
    }
}