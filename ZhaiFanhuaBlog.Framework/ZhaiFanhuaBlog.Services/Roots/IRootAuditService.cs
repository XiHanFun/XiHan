#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootAuditService
// Guid:67f75e28-407b-4a4d-17a2-feac945abade
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-02 下午 05:50:49
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Core.Services;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// IRootAuditService
/// </summary>
public interface IRootAuditService : IBaseService<RootAudit>, IScopeDependency
{
}