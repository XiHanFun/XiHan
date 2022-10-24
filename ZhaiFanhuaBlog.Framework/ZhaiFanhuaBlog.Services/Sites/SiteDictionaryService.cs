// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteDictionaryService
// Guid:ff93f0ef-c399-4aa9-ab15-bec004e844eb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 05:38:23
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Sites;
using ZhaiFanhuaBlog.IServices.Sites;
using ZhaiFanhuaBlog.Models.Sites;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Sites;

/// <summary>
/// SiteDictionaryService
/// </summary>
public class SiteDictionaryService : BaseService<SiteDictionary>, ISiteDictionaryService
{
    private readonly ISiteDictionaryRepository _ISiteDictionaryRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iSiteDictionaryRepository"></param>
    public SiteDictionaryService(ISiteDictionaryRepository iSiteDictionaryRepository)
    {
        _ISiteDictionaryRepository = iSiteDictionaryRepository;
        base._IBaseRepository = iSiteDictionaryRepository;
    }

    /// <summary>
    /// 初始化系统状态
    /// </summary>
    /// <param name="SiteDictionarys"></param>
    /// <returns></returns>
    public async Task<bool> InitSiteDictionaryAsync(List<SiteDictionary> SiteDictionarys)
    {
        return await _ISiteDictionaryRepository.CreateBatchAsync(SiteDictionarys);
    }
}