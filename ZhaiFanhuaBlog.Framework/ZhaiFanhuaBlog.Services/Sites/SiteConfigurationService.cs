// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteConfigurationService
// Guid:0b165e45-47db-4582-9bdc-cf5260138950
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 05:26:41
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Sites;
using ZhaiFanhuaBlog.IServices.Sites;
using ZhaiFanhuaBlog.Models.Sites;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Sites;

/// <summary>
/// SiteConfigurationService
/// </summary>
public class SiteConfigurationService : BaseService<SiteConfiguration>, ISiteConfigurationService
{
    private readonly ISiteConfigurationRepository _ISiteConfigurationRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iSiteConfigurationRepository"></param>
    public SiteConfigurationService(ISiteConfigurationRepository iSiteConfigurationRepository)
    {
        base._IBaseRepository = iSiteConfigurationRepository;
        _ISiteConfigurationRepository = iSiteConfigurationRepository;
    }

    /// <summary>
    /// 新增网站配置
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> CreateSiteConfigurationAsync(SiteConfiguration configuration)
    {
        var siteConfigurationCreated = await _ISiteConfigurationRepository.FindAsync(c => c.Name == configuration.Name);
        if (siteConfigurationCreated != null)
            throw new ApplicationException("添加失败，该网站名称已存在");
        var result = await _ISiteConfigurationRepository.CreateAsync(configuration);
        return result;
    }

    /// <summary>
    /// 删除网站配置
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> DeleteSiteConfigurationAsync(Guid guid)
    {
        var siteConfigurationCreated = await _ISiteConfigurationRepository.FindAsync(guid);
        if (siteConfigurationCreated == null)
            throw new ApplicationException("删除失败，该网站配置不存在");
        var siteConfiguration = await _ISiteConfigurationRepository.DeleteAsync(guid);
        if (!siteConfiguration)
            throw new ApplicationException("删除失败，网站配置删除发生错误");
        return siteConfiguration;
    }

    /// <summary>
    /// 修改网站配置
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<SiteConfiguration> ModifySiteConfigurationAsync(SiteConfiguration configuration)
    {
        var siteConfigurationCreated = await _ISiteConfigurationRepository.FindAsync(configuration.BaseId);
        if (siteConfigurationCreated == null)
            throw new ApplicationException("修改失败，该网站配置不存在");
        if (siteConfigurationCreated.Name == configuration.Name)
            throw new ApplicationException("修改失败，该网站名称不能与未修改前相同");
        configuration.ModifyTime = DateTime.Now;
        if (!await _ISiteConfigurationRepository.UpdateAsync(configuration))
            throw new ApplicationException("修改失败，网站配置修改发生错误");
        return configuration;
    }

    /// <summary>
    /// 查找网站配置
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<SiteConfiguration> FindSiteConfigurationAsync(Guid guid)
    {
        var siteConfigurationCreated = await _ISiteConfigurationRepository.FindAsync(guid);
        if (siteConfigurationCreated == null)
            throw new ApplicationException("未查询到任何网站配置");
        return siteConfigurationCreated;
    }
}