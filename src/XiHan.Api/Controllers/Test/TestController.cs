#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TestController
// Guid:845e3ab1-519a-407f-bd95-1204e9506dbd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-17 上午 04:42:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using XiHan.Api.Controllers.Bases;
using XiHan.Extensions.Common.Swagger;
using XiHan.Extensions.Filters;
using XiHan.Infrastructure.Apps;
using XiHan.Infrastructure.Responses.Results;
using XiHan.Utils.Infos;

namespace XiHan.Api.Controllers.Test;

/// <summary>
/// 系统测试
/// <code>包含：工具/客户端信息/IP信息/授权信息</code>
/// </summary>
[EnableCors("AllowAll")]
[ApiGroup(ApiGroupNames.Test)]
public class TestController : BaseApiController
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TestController()
    {
    }

    /// <summary>
    /// 客户端信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("ClientInfo")]
    public ActionResult<BaseResultDto> ClientInfo()
    {
        // 获取 HttpContext 和 HttpRequest 对象
        var httpContext = App.HttpContext!;
        HttpContexInfotHelper clientInfoHelper = new(httpContext);
        return BaseResultDto.Success(clientInfoHelper);
    }

    /// <summary>
    /// 过时
    /// </summary>
    /// <returns></returns>
    [Obsolete]
    [HttpGet("Obsolete")]
    public string Obsolete()
    {
        return "过时接口";
    }

    /// <summary>
    /// 授权
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("Authorize")]
    public string Authorize()
    {
        return "授权接口";
    }

    /// <summary>
    /// 未实现或异常
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("Exception")]
    public string Exception()
    {
        throw new NotImplementedException("这是一个未实现或异常的接口");
    }

    /// <summary>
    /// 日志
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    [HttpGet("LogInfo")]
    [TypeFilter(typeof(ActionFilterAsyncAttribute))]
    public ActionResult<BaseResultDto> LogInfo(string log)
    {
        return BaseResultDto.Success($"测试日志写入:{log}");
    }

    /// <summary>
    /// 资源过滤器
    /// </summary>
    /// <returns></returns>
    [HttpGet("ResourceFilterAttribute")]
    [TypeFilter(typeof(ResourceFilterAsyncAttribute))]
    public ActionResult<BaseResultDto> ResourceFilterAttribute()
    {
        return BaseResultDto.Success(DateTime.Now);
    }

    /// <summary>
    /// 异步资源过滤器
    /// </summary>
    /// <returns></returns>
    [HttpGet("ResourceFilterAsyncAttribute")]
    [TypeFilter(typeof(ResourceFilterAsyncAttribute))]
    public ActionResult<BaseResultDto> ResourceFilterAsyncAttribute()
    {
        return BaseResultDto.Success(DateTime.Now);
    }
}