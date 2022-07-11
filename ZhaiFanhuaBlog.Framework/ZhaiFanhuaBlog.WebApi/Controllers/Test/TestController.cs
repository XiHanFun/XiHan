// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TestController
// Guid:845e3ab1-519a-407f-bd95-1204e9506dbd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-17 上午 04:42:29
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZhaiFanhuaBlog.WebApi.Common.Extensions.Swagger;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Test;

/// <summary>
/// 测试接口
/// </summary>
[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = SwaggerGroup.Test)]
public class TestController : ControllerBase
{
    /// <summary>
    /// 测试1
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    [HttpPost("Test1")]
    public string Test1(string? str)
    {
        return "测试字符串：" + str;
    }

    /// <summary>
    /// 测试2  过时接口
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    [HttpPost("Test2")]
    [Obsolete]
    public string Test2(string? str)
    {
        return "测试字符串：" + str;
    }
}