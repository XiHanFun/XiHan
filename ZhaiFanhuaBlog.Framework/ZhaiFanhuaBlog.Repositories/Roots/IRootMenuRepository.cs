// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootMenuRepository
// Guid:4f360023-1765-4f30-aabb-50fc35ec08fd
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-08-05 下午 05:13:37
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Repositories.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Repositories.Roots;

/// <summary>
/// IRootMenuRepository
/// </summary>
public interface IRootMenuRepository : IBaseRepository<RootMenu>, IScopeDependency
{
}