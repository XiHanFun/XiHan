// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootRoleMenuRepository
// Guid:ab67d4b7-d7aa-44e1-83ac-19c2764c73fe
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-08-05 下午 05:14:01
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Core.Services;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Roots;

/// <summary>
/// IRootRoleMenuRepository
/// </summary>
public interface IRootRoleMenuRepository : IBaseRepository<RootRoleMenu>, IScopeDependency
{
}