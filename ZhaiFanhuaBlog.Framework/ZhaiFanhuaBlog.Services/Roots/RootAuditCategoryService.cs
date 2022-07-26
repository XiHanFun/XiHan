// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootAuditCategoryService
// Guid:0c28440b-c5c0-4507-bc91-7b0f0c6f272b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 05:35:32
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IServices.Roots;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// RootAuditCategoryService
/// </summary>
public class RootAuditCategoryService : BaseService<RootAuditType>, IRootAuditCategoryService
{
    private readonly IRootAuditCategoryRepository _IRootAuditCategoryRepository;

    public RootAuditCategoryService(IRootAuditCategoryRepository iRootAuditCategoryRepository)
    {
        _IRootAuditCategoryRepository = iRootAuditCategoryRepository;
        base._IBaseRepository = iRootAuditCategoryRepository;
    }
}