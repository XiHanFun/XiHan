#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:EmailController
// Guid:c5460f65-c73d-4a45-a8bf-7818e17b587d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-07 下午 02:16:29
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHan.Application.Common.Swagger;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Services.Commons.Messages.EmailPush;
using XiHan.Subscriptions.Robots.Email;
using XiHan.WebApi.Controllers.Bases;

namespace XiHan.WebApi.Controllers.Manage.Messages;

/// <summary>
/// 邮件推送
/// <code>包含：SMTP</code>
/// </summary>
[AllowAnonymous]
[ApiGroup(ApiGroupNames.Manage)]
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
    [HttpPost("Send")]
    public async Task<CustomResult> SendEmail(EmailToModel emailTo)
    {
        return await _emailPushService.SendEmail(emailTo);
    }
}