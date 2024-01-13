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
using XiHanCore.Infrastructures.Apps.Logging;
using XiHanCore.Infrastructures.Responses;
using XiHanCore.Services.Commons.Messages.EmailPush;
using XiHanCore.WebCore.Common.Swagger;

namespace XiHanCore.WebHost.Controllers.Commons.Messages;

/// <summary>
/// 邮件推送管理
/// <code>包含：SMTP</code>
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="emailPushService"></param>
[AllowAnonymous]
[ApiGroup(ApiGroupNameEnum.Common)]
public class EmailController(IEmailPushService emailPushService) : BaseApiController
{
    private readonly IEmailPushService _emailPushService = emailPushService;

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