#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:EmailPushService
// Guid:64280e3f-fbb4-4049-bedb-b5f53c93a28b
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-12-07 下午 01:31:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Net.Mail;
using ZhaiFanhuaBlog.Infrastructure.Contexts.Response;
using ZhaiFanhuaBlog.Infrastructure.Contexts.Response.Results;
using ZhaiFanhuaBlog.Infrastructure.Apps.Services;
using ZhaiFanhuaBlog.Infrastructure.Apps.Setting;
using ZhaiFanhuaBlog.Infrastructure.Enums;
using ZhaiFanhuaBlog.Utils.Message.Email;

namespace ZhaiFanhuaBlog.Services.Utils.Message.Impl;

/// <summary>
/// EmailPushService
/// </summary>
[AppService(ServiceType = typeof(IEmailPushService), ServiceLifetime = LifeTimeEnum.Scoped)]
public class EmailPushService : IEmailPushService
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public EmailPushService()
    {
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <returns></returns>
    public async Task<BaseResultDto> SendEmail()
    {
        var subject = "测试";
        var body = "测试";
        List<string> toMail = new() { "" };
        List<string> ccMail = new() { "" };
        List<string> bccMail = new() { "" };
        List<Attachment> attachmentsPath = new()
        {
            new Attachment(@"")
        };
        var model = new EmailModel
        {
            Host = AppSettings.Message.Email.Host.Get(),
            Port = AppSettings.Message.Email.Port.Get(),
            UseSsl = AppSettings.Message.Email.UseSsl.Get(),
            FromMail = AppSettings.Message.Email.From.Address.Get(),
            FromName = AppSettings.Message.Email.From.UserName.Get(),
            FromPassword = AppSettings.Message.Email.From.Password.Get(),
            Subject = subject,
            Body = body,
            ToMail = toMail,
            CcMail = ccMail,
            BccMail = bccMail,
            AttachmentsPath = attachmentsPath,
        };
        if (await EmailHelper.Send(model))
        {
            return BaseResponseDto.Ok("邮件发送成功");
        }
        return BaseResponseDto.BadRequest("邮件发送失败");
    }
}