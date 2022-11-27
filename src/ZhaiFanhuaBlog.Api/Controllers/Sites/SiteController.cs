#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteController
// Guid:d364ed9f-8b49-48cf-939f-5970f2d232fe
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-24 上午 03:08:22
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZhaiFanhuaBlog.Api.Controllers.Bases;
using ZhaiFanhuaBlog.Core.AppSettings;
using ZhaiFanhuaBlog.Extensions.Common.Swagger;
using ZhaiFanhuaBlog.Extensions.Filters;
using ZhaiFanhuaBlog.Models.Sites;
using ZhaiFanhuaBlog.Services.Sites;
using ZhaiFanhuaBlog.ViewModels.Bases.Results;
using ZhaiFanhuaBlog.ViewModels.Response;
using ZhaiFanhuaBlog.ViewModels.Sites;

namespace ZhaiFanhuaBlog.Api.Controllers;

/// <summary>
/// 网站配置
/// <code>包含：初始化网站/配置/皮肤/日志</code>
/// </summary>
[ApiGroup(ApiGroupNames.Backstage)]
public class SiteController : BaseApiController
{
    private readonly ISiteConfigurationService _ISiteConfigurationService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iSiteConfigurationService"></param>
    public SiteController(ISiteConfigurationService iSiteConfigurationService)
    {
        _ISiteConfigurationService = iSiteConfigurationService;
    }

    /// <summary>
    /// 站点初始化配置
    /// </summary>
    /// <returns></returns>
    [HttpPost("InitData")]
    public async Task<BaseResultDto> InitData([FromServices] IMapper iMapper)
    {
        CSiteConfigurationDto configuration = new()
        {
            Name = AppSettings.Site.Name,
            Description = AppSettings.Site.Description,
            KeyWord = AppSettings.Site.KeyWord,
            Domain = AppSettings.Site.Domain,
            AdminName = AppSettings.Site.Admin.Name,
            AdminEmail = AppSettings.Site.Admin.Email
        };
        var siteConfiguration = iMapper.Map<SiteConfiguration>(configuration);
        if (await _ISiteConfigurationService.CreateSiteConfigurationAsync(siteConfiguration))
            return BaseResponseDto.OK("站点初始化配置成功");
        return BaseResponseDto.BadRequest("站点初始化配置失败");
    }
}