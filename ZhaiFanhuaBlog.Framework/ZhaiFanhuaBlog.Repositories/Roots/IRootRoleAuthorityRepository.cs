// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootRoleAuthorityRepository
// Guid:34c35905-76dc-4c78-aa5f-6db51ae28430
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:30:13
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Repositories.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Repositories.Roots;

/// <summary>
/// IRootRoleAuthorityRepository
/// </summary>
public interface IRootRoleAuthorityRepository : IBaseRepository<RootRoleAuthority>, IScopeDependency
{
}