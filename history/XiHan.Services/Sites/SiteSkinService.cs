#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysSkinService
// Guid:395446ac-3ea4-48a9-9f80-d5b88ee15c7c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 05:31:26
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Sys;
using ZhaiFanhuaBlog.Repositories.Sys;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Sys;

/// <summary>
/// SysSkinService
/// </summary>
public class SysSkinService : BaseService<SysSkin>, ISysSkinService
{
    private readonly ISysSkinRepository _ISysSkinRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iSysSkinRepository"></param>
    public SysSkinService(ISysSkinRepository iSysSkinRepository)
    {
        _ISysSkinRepository = iSysSkinRepository;
        base._IBaseRepository = iSysSkinRepository;
    }
}