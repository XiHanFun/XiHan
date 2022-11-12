#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ISiteDictionaryRepository
// Guid:a21a1940-6a27-4732-b0a5-37841f6de408
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:19:26
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Core.Services;
using ZhaiFanhuaBlog.Models.Sites;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Sites;

/// <summary>
/// ISiteDictionaryRepository
/// </summary>
public interface ISiteDictionaryRepository : IBaseRepository<SiteDictionary>, IScopeDependency
{
}