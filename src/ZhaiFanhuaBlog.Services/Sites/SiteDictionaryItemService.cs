#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteDictionaryItemService
// Guid:ff93f0ef-c399-4aa9-ab15-bec004e844eb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 05:38:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Sites;
using ZhaiFanhuaBlog.Repositories.Sites;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Sites;

/// <summary>
/// SiteDictionaryItemService
/// </summary>
public class SiteDictionaryItemService : BaseService<SiteDictionaryInfo>, ISiteDictionaryItemService
{
    private readonly ISiteDictionaryItemRepository _ISiteDictionaryItemRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iSiteDictionaryItemRepository"></param>
    public SiteDictionaryItemService(ISiteDictionaryItemRepository iSiteDictionaryItemRepository)
    {
        _ISiteDictionaryItemRepository = iSiteDictionaryItemRepository;
        base._IBaseRepository = iSiteDictionaryItemRepository;
    }

    /// <summary>
    /// 初始化系统状态
    /// </summary>
    /// <param name="SiteDictionaryItems"></param>
    /// <returns></returns>
    public async Task<bool> InitSiteDictionaryItemAsync(List<SiteDictionaryInfo> SiteDictionaryItems)
    {
        return await _ISiteDictionaryItemRepository.CreateBatchAsync(SiteDictionaryItems);
    }
}