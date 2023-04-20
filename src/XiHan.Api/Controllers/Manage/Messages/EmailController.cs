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
using XiHan.Api.Controllers.Bases;
using XiHan.Extensions.Common.Swagger;
using XiHan.Infrastructure.Contexts.Results;
using XiHan.Services.Syses.Messages.EmailPush;
using XiHan.Utils.Messages.Email;

namespace XiHan.Api.Controllers.Manage.Messages;

/// <summary>
/// 邮件推送
/// <code>包含：SMTP</code>
/// </summary>
[AllowAnonymous]
[ApiGroup(ApiGroupNames.Backstage)]
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
    public async Task<BaseResultDto> SendEmail(EmailToModel emailTo)
    {
        return await _emailPushService.SendEmail(emailTo);
    }
}