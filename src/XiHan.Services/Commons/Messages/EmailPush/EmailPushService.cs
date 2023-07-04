#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:EmailPushService
// Guid:64280e3f-fbb4-4049-bedb-b5f53c93a28b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-07 下午 01:31:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Net.Mail;
using Mapster;
using Microsoft.Extensions.Logging;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Subscriptions.Robots.Email;

namespace XiHan.Services.Commons.Messages.EmailPush;

/// <summary>
/// EmailPushService
/// </summary>
[AppService(ServiceType = typeof(IEmailPushService), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class EmailPushService : BaseService<SysEmail>, IEmailPushService
{
    private readonly ILogger<EmailPushService> _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    public EmailPushService(ILogger<EmailPushService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <returns></returns>
    [AppLog(Module = "发送邮件", BusinessType = BusinessTypeEnum.Other)]
    public async Task<CustomResult> SendEmail(EmailToModel emailTo)
    {
        var sysEmail = await GetFirstAsync(e => e.CreatedBy != null);
        var emailFrom = sysEmail.Adapt<EmailFromModel>();
        var subject = "测试";
        var body = "测试";
        List<string> toMail = new() { "" };
        List<string> ccMail = new() { "" };
        List<string> bccMail = new() { "" };
        List<Attachment> attachmentsPath = new()
        {
            new Attachment(@"")
        };
        var emailToModel=new EmailToModel {
            Subject = subject,
            Body = body,
            ToMail = toMail,
            CcMail = ccMail,
            BccMail = bccMail,
            AttachmentsPath = attachmentsPath,
        };
        var emailRobot = new EmailRobot(emailFrom);
        var logoInfo = string.Empty;
        if (await emailRobot.Send(emailTo))
        {
            logoInfo = "邮件发送成功";
            _logger.LogInformation(logoInfo);
            return CustomResult.Success(logoInfo);
        }

        logoInfo = "邮件发送失败";
        _logger.LogError(logoInfo);
        return CustomResult.BadRequest("邮件发送失败");
    }
}