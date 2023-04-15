#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysController
// Guid:d364ed9f-8b49-48cf-939f-5970f2d232fe
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-24 上午 03:08:22
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZhaiFanhuaBlog.Api.Controllers.Bases;
using ZhaiFanhuaBlog.Infrastructure.AppSetting;
using ZhaiFanhuaBlog.Extensions.Common.Swagger;
using ZhaiFanhuaBlog.Extensions.Filters;
using ZhaiFanhuaBlog.Models.Sys;
using ZhaiFanhuaBlog.Services.Sys;
using ZhaiFanhuaBlog.ViewModels.Bases.Results;
using ZhaiFanhuaBlog.ViewModels.Response;
using ZhaiFanhuaBlog.ViewModels.Sys;

namespace ZhaiFanhuaBlog.Api.Controllers;

/// <summary>
/// 系统配置
/// <code>包含：初始化系统/配置/皮肤/日志</code>
/// </summary>
[ApiGroup(ApiGroupNames.Backstage)]
public class SysController : BaseApiController
{
    private readonly ISysConfigurationService _ISysConfigurationService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iSysConfigurationService"></param>
    public SysController(ISysConfigurationService iSysConfigurationService)
    {
        _ISysConfigurationService = iSysConfigurationService;
    }

    /// <summary>
    /// 站点初始化配置
    /// </summary>
    /// <returns></returns>
    [HttpPost("InitData")]
    public async Task<BaseResultDto> InitData([FromServices] IMapper iMapper)
    {
        CSysConfigurationDto configuration = new()
        {
            Name = AppSettings.Sys.Name,
            Description = AppSettings.Sys.Description,
            KeyWord = AppSettings.Sys.KeyWord,
            Domain = AppSettings.Sys.Domain,
            AdminName = AppSettings.Sys.Admin.Name,
            AdminEmail = AppSettings.Sys.Admin.Email
        };
        var siteConfiguration = iMapper.Map<SysConfiguration>(configuration);
        if (await _ISysConfigurationService.CreateSysConfigurationAsync(siteConfiguration))
            return BaseResponseDto.OK("站点初始化配置成功");
        return BaseResponseDto.BadRequest("站点初始化配置失败");
    }
}