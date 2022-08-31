// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteController
// Guid:d364ed9f-8b49-48cf-939f-5970f2d232fe
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-24 上午 03:08:22
// ----------------------------------------------------------------

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZhaiFanhuaBlog.IServices.Sites;
using ZhaiFanhuaBlog.Models.Bases.Response.Model;
using ZhaiFanhuaBlog.Models.Response;
using ZhaiFanhuaBlog.Models.Sites;
using ZhaiFanhuaBlog.ViewModels.Sites;
using ZhaiFanhuaBlog.WebApi.Common.Extensions.Swagger;
using ZhaiFanhuaBlog.WebApi.Common.Filters;

namespace ZhaiFanhuaBlog.WebApi.Controllers;

/// <summary>
/// 网站配置
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
public class SiteController : ControllerBase
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
    [HttpPost("Configuration/Init")]
    [TypeFilter(typeof(CustomActionFilterAsyncAttribute))]
    public async Task<ResultModel> Init([FromServices] IMapper iMapper)
    {
        CSiteConfigurationDto configuration = new()
        {
            Name = _IConfiguration.GetValue<string>("Configuration:Name"),
            Description = _IConfiguration.GetValue<string>("Configuration:Description"),
            KeyWord = _IConfiguration.GetValue<string>("Configuration:KeyWord"),
            Domain = _IConfiguration.GetValue<string>("Configuration:Domain"),
            AdminName = _IConfiguration.GetValue<string>("Configuration:Admin:Name")
        };
        var siteConfiguration = iMapper.Map<SiteConfiguration>(configuration);
        if (await _ISiteConfigurationService.CreateSiteConfigurationAsync(siteConfiguration))
            return ResultResponse.OK("站点初始化配置成功");
        return ResultResponse.BadRequest("站点初始化配置失败");
    }
}