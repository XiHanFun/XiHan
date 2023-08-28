#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ISysConfigurationService
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
/// ISysConfigurationService
/// </summary>
public interface ISysConfigurationService : IBaseService<SysConfiguration>, IScopeDependency
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    Task<bool> CreateSysConfigurationAsync(SysConfiguration configuration);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<bool> DeleteSysConfigurationAsync(Guid guid);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    Task<SysConfiguration> ModifySysConfigurationAsync(SysConfiguration configuration);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<SysConfiguration> FindSysConfigurationAsync(Guid guid);
}