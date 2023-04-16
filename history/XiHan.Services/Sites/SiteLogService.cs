#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysLogService
// Guid:05457439-9e81-4b06-b601-26099de73285
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 05:29:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Sys;
using ZhaiFanhuaBlog.Repositories.Sys;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Sys;

/// <summary>
/// SysLogService
/// </summary>
public class SysLogService : BaseService<SysLog>, ISysLogService
{
    private readonly ISysLogRepository _ISysLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iSysLogRepository"></param>
    public SysLogService(ISysLogRepository iSysLogRepository)
    {
        _ISysLogRepository = iSysLogRepository;
        base._IBaseRepository = iSysLogRepository;
    }
}