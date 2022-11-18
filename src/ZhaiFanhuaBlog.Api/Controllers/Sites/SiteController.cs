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
    private IConfiguration _IConfiguration;
    private readonly ISiteConfigurationService _ISiteConfigurationService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="iSiteConfigurationService"></param>
    public SiteController(IConfiguration configuration, ISiteConfigurationService iSiteConfigurationService)
    {
        _IConfiguration = configuration;
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
            Name = _IConfiguration.GetValue<string>("Configuration:Name") ?? string.Empty,
            Description = _IConfiguration.GetValue<string>("Configuration:Description") ?? string.Empty,
            KeyWord = _IConfiguration.GetValue<string>("Configuration:KeyWord") ?? string.Empty,
            Domain = _IConfiguration.GetValue<string>("Configuration:Domain") ?? string.Empty,
            AdminName = _IConfiguration.GetValue<string>("Configuration:Admin:Name") ?? string.Empty
        };
        var siteConfiguration = iMapper.Map<SiteConfiguration>(configuration);
        if (await _ISiteConfigurationService.CreateSiteConfigurationAsync(siteConfiguration))
            return BaseResponseDto.OK("站点初始化配置成功");
        return BaseResponseDto.BadRequest("站点初始化配置失败");
    }
}