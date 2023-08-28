#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysDictionaryItemService
// Guid:ff93f0ef-c399-4aa9-ab15-bec004e844eb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 05:38:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Sys;
using ZhaiFanhuaBlog.Repositories.Sys;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Sys;

/// <summary>
/// SysDictionaryItemService
/// </summary>
public class SysDictionaryItemService : BaseService<SysDicData>, ISysDictionaryItemService
{
    private readonly ISysDictionaryItemRepository _ISysDictionaryItemRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iSysDictionaryItemRepository"></param>
    public SysDictionaryItemService(ISysDictionaryItemRepository iSysDictionaryItemRepository)
    {
        _ISysDictionaryItemRepository = iSysDictionaryItemRepository;
        base._IBaseRepository = iSysDictionaryItemRepository;
    }

    /// <summary>
    /// 初始化系统状态
    /// </summary>
    /// <param name="SysDictionaryItems"></param>
    /// <returns></returns>
    public async Task<bool> InitSysDictionaryItemAsync(List<SysDicData> SysDictionaryItems)
    {
        return await _ISysDictionaryItemRepository.CreateBatchAsync(SysDictionaryItems);
    }
}