// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ISiteDictionaryItemRepository
// Guid:a21a1940-6a27-4732-b0a5-37841f6de408
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:19:26
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Sites;
using ZhaiFanhuaBlog.Repositories.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Repositories.Sites;

/// <summary>
/// ISiteDictionaryItemRepository
/// </summary>
public interface ISiteDictionaryItemRepository : IBaseRepository<SiteDictionaryItem>, IScopeDependency
{
}