#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ISysSkinService
// Guid:4852cc95-ec03-49e1-baea-98df25740234
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-22 下午 02:34:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Infrastructure.Services;
using ZhaiFanhuaBlog.Models.Sys;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Sys;

/// <summary>
/// ISysSkinService
/// </summary>
public interface ISysSkinService : IBaseService<SysSkin>, IScopeDependency
{
}