#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:EmailController
// Guid:c5460f65-c73d-4a45-a8bf-7818e17b587d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-12-07 下午 02:16:29
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Responses;
using XiHan.Services.Commons.Messages.EmailPush;
using XiHan.WebHost.Controllers.Bases;
using XiHan.WebCore.Common.Swagger;

namespace XiHan.WebHost.Controllers.Common.Messages;

/// <summary>
/// 邮件推送管理
/// <code>包含：SMTP</code>
/// </summary>
[AllowAnonymous]
[ApiGroup(ApiGroupNameEnum.Common)]
public class EmailController : BaseApiController
{
    private readonly IEmailPushService _emailPushService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="emailPushService"></param>
    public EmailController(IEmailPushService emailPushService)
    {
        _emailPushService = emailPushService;
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <returns></returns>
    [HttpPost("SendEmail")]
    [AppLog("发送验证码邮件", BusinessTypeEnum.Other)]
    public async Task<ApiResult> SendEmail()
    {
        return await _emailPushService.SendCaptchaMail("zhaifanhua", "xxxxxx@qq.com", "325948");
    }
}