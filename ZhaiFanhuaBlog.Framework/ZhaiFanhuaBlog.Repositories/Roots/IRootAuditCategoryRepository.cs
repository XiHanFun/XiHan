// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootAuditCategoryRepository
// Guid:a656775b-dec0-4321-a2ab-c5b042fe1265
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:18:59
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Repositories.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Repositories.Roots;

/// <summary>
/// IRootAuditCategoryRepository
/// </summary>
public interface IRootAuditCategoryRepository : IBaseRepository<RootAuditCategory>, IScopeDependency
{
}