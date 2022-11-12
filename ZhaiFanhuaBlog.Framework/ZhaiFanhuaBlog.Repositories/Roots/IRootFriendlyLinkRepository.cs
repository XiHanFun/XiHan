#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootFriendlyLinkRepository
// Guid:703c45e6-9c20-42de-9c7c-aa8268267f4a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:19:12
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Core.Services;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Roots;

/// <summary>
/// IRootFriendlyLinkRepository
/// </summary>
public interface IRootFriendlyLinkRepository : IBaseRepository<RootFriendlyLink>, IScopeDependency
{
}