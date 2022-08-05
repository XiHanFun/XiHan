// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteController
// Guid:d364ed9f-8b49-48cf-939f-5970f2d232fe
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-24 上午 03:08:22
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using ZhaiFanhuaBlog.IServices.Sites;
using ZhaiFanhuaBlog.Models.Bases.Response.Model;
using ZhaiFanhuaBlog.Models.Response;
using ZhaiFanhuaBlog.Models.Sites;
using ZhaiFanhuaBlog.WebApi.Common.Filters;

namespace ZhaiFanhuaBlog.WebApi.Controllers;

/// <summary>
/// 网站配置
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Site")]
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
    /// 站点初始化
    /// </summary>
    /// <returns></returns>
    [HttpPost("Configuration")]
    [TypeFilter(typeof(CustomActionFilterAsyncAttribute))]
    public async Task<ResultModel> CreateSiteConfiguration()
    {
        SiteConfiguration configuration = new()
        {
            Name = _IConfiguration.GetValue<string>("Configuration:SiteName"),
            Description = _IConfiguration.GetValue<string>("Configuration:SiteDescription"),
            KeyWord = _IConfiguration.GetValue<string>("Configuration:KeyWord"),
            Domain = _IConfiguration.GetValue<string>("Configuration:SiteDomain"),
            AdminName = _IConfiguration.GetValue<string>("Configuration:AdminName")
        };
        var result = await _ISiteConfigurationService.CreateSiteConfigurationAsync(configuration);
        if (result)
            return ResultResponse.OK("新增系统权限成功");
        return ResultResponse.BadRequest("添加网站配置失败");
    }
}