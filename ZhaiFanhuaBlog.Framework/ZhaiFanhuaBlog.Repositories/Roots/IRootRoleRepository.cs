// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootRoleRepository
// Guid:84bce558-3290-4ac1-ad26-a98fcaa55b70
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:29:49
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Core.Services;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Roots;

/// <summary>
/// IRootRoleRepository
/// </summary>
public interface IRootRoleRepository : IBaseRepository<RootRole>, IScopeDependency
{
}