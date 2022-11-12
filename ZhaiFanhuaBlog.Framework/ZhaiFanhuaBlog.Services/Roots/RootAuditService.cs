#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootAuditService
// Guid:2c22d64d-5b3b-42fa-a7b1-8b8baf1b365f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 05:34:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Repositories.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// RootAuditService
/// </summary>
public class RootAuditService : BaseService<RootAudit>, IRootAuditService
{
    private readonly IRootAuditRepository _IRootAuditRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iRootAuditRepository"></param>
    public RootAuditService(IRootAuditRepository iRootAuditRepository)
    {
        _IRootAuditRepository = iRootAuditRepository;
        base._IBaseRepository = iRootAuditRepository;
    }
}