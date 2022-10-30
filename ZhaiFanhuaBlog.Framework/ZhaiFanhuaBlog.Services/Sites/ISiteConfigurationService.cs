// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ISiteConfigurationService
// Guid:4852cc95-ec03-49e1-baea-98df25740234
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-22 下午 02:34:58
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Sites;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Sites;

/// <summary>
/// ISiteConfigurationService
/// </summary>
public interface ISiteConfigurationService : IBaseService<SiteConfiguration>
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    Task<bool> CreateSiteConfigurationAsync(SiteConfiguration configuration);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<bool> DeleteSiteConfigurationAsync(Guid guid);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    Task<SiteConfiguration> ModifySiteConfigurationAsync(SiteConfiguration configuration);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<SiteConfiguration> FindSiteConfigurationAsync(Guid guid);
}