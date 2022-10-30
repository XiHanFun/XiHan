// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ISiteDictionaryItemService
// Guid:8b2a6d03-4267-4457-a19c-1fddedf0356c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-24 上午 01:19:02
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Sites;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Sites;

/// <summary>
/// ISiteDictionaryItemService
/// </summary>
public interface ISiteDictionaryItemService : IBaseService<SiteDictionaryItem>
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="SiteDictionaryItems"></param>
    /// <returns></returns>
    Task<bool> InitSiteDictionaryItemAsync(List<SiteDictionaryItem> SiteDictionaryItems);
}