#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysConfigurationService
// Guid:0b165e45-47db-4582-9bdc-cf5260138950
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 05:26:41
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Sys;
using ZhaiFanhuaBlog.Repositories.Sys;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Sys;

/// <summary>
/// SysConfigurationService
/// </summary>
public class SysConfigurationService : BaseService<SysConfiguration>, ISysConfigurationService
{
    private readonly ISysConfigurationRepository _ISysConfigurationRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iSysConfigurationRepository"></param>
    public SysConfigurationService(ISysConfigurationRepository iSysConfigurationRepository)
    {
        base._IBaseRepository = iSysConfigurationRepository;
        _ISysConfigurationRepository = iSysConfigurationRepository;
    }

    /// <summary>
    /// 新增系统配置
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> CreateSysConfigurationAsync(SysConfiguration configuration)
    {
        var siteConfigurationCreated = await _ISysConfigurationRepository.FindAsync(c => c.Name == configuration.Name);
        if (siteConfigurationCreated != null)
            throw new ApplicationException("添加失败，该系统名称已存在");
        var result = await _ISysConfigurationRepository.CreateAsync(configuration);
        return result;
    }

    /// <summary>
    /// 删除系统配置
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> DeleteSysConfigurationAsync(Guid guid)
    {
        var siteConfigurationCreated = await _ISysConfigurationRepository.FindAsync(guid);
        if (siteConfigurationCreated == null)
            throw new ApplicationException("删除失败，该系统配置不存在");
        var siteConfiguration = await _ISysConfigurationRepository.DeleteAsync(guid);
        if (!siteConfiguration)
            throw new ApplicationException("删除失败，系统配置删除发生错误");
        return siteConfiguration;
    }

    /// <summary>
    /// 修改系统配置
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<SysConfiguration> ModifySysConfigurationAsync(SysConfiguration configuration)
    {
        var siteConfigurationCreated = await _ISysConfigurationRepository.FindAsync(configuration.BaseId);
        if (siteConfigurationCreated == null)
            throw new ApplicationException("修改失败，该系统配置不存在");
        if (siteConfigurationCreated.Name == configuration.Name)
            throw new ApplicationException("修改失败，该系统名称不能与未修改前相同");
        configuration.ModifyTime = DateTime.Now;
        if (!await _ISysConfigurationRepository.UpdateAsync(configuration))
            throw new ApplicationException("修改失败，系统配置修改发生错误");
        return configuration;
    }

    /// <summary>
    /// 查找系统配置
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<SysConfiguration> FindSysConfigurationAsync(Guid guid)
    {
        var siteConfigurationCreated = await _ISysConfigurationRepository.FindAsync(guid);
        if (siteConfigurationCreated == null)
            throw new ApplicationException("未查询到任何系统配置");
        return siteConfigurationCreated;
    }
}