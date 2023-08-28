#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ISysLogService
// Guid:fe2ef0ad-5270-42ae-b0bf-365e97029edd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-22 下午 02:38:09
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Infrastructure.Services;
using ZhaiFanhuaBlog.Models.Sys;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Sys;

/// <summary>
/// ISysLogService
/// </summary>
public interface ISysLogService : IBaseService<SysLog>, IScopeDependency
{
}