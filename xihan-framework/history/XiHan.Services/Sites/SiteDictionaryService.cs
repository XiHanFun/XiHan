#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysDictionaryService
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
/// SysDictionaryService
/// </summary>
public class SysDictionaryService : BaseService<SysDicType>, ISysDictionaryService
{
    private readonly ISysDictionaryRepository _ISysDictionaryRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iSysDictionaryRepository"></param>
    public SysDictionaryService(ISysDictionaryRepository iSysDictionaryRepository)
    {
        _ISysDictionaryRepository = iSysDictionaryRepository;
        base._IBaseRepository = iSysDictionaryRepository;
    }

    /// <summary>
    /// 初始化系统状态
    /// </summary>
    /// <param name="SysDictionarys"></param>
    /// <returns></returns>
    public async Task<bool> InitSysDictionaryAsync(List<SysDicType> SysDictionarys)
    {
        return await _ISysDictionaryRepository.CreateBatchAsync(SysDictionarys);
    }
}