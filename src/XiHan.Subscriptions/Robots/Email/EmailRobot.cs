#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:EmailRobot
// Guid:6cd058a5-9ec5-4ab3-8c7d-aeab0f513700
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 上午 01:21:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Net;
using System.Net.Mail;
using XiHan.Utils.Consoles;

namespace XiHan.Subscriptions.Robots.Email;

/// <summary>
/// 邮件机器人
/// </summary>
public class EmailRobot
{
    private readonly EmailFromModel _fromModel;
    private readonly EmailToModel _toModel;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fromModel"></param>
    /// <param name="toModel"></param>
    public EmailRobot(EmailFromModel fromModel, EmailToModel toModel)
    {
        _fromModel = fromModel;
        _toModel = toModel;
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    public async Task<bool> Send()
    {
        // 初始化邮件实例
        using MailMessage message = new()
        {
            // 来源或发送者
            From = new MailAddress(_fromModel.FromMail, _fromModel.FromName, _fromModel.Coding),
            Sender = new MailAddress(_fromModel.FromMail, _fromModel.FromName, _fromModel.Coding),
            // 邮件主题
            Subject = _toModel.Subject,
            SubjectEncoding = _fromModel.Coding,
            // 邮件正文
            Body = _toModel.Body,
            BodyEncoding = _fromModel.Coding,
            // 优先级（高）
            Priority = MailPriority.High,
            // 网页形式
            IsBodyHtml = true,
        };
        // 收件人地址集合
        _toModel.ToMail.ForEach(to => message.To.Add(to));
        // 抄送人地址集合
        _toModel.CcMail.ForEach(cc => message.CC.Add(cc));
        // 密送人地址集合
        _toModel.BccMail.ForEach(bcc => message.Bcc.Add(bcc));
        // 在有附件的情况下添加附件
        _toModel.AttachmentsPath.ForEach(path => message.Attachments.Add(path));

        // 初始化连接实例
        using SmtpClient client = new(_fromModel.Host, _fromModel.Port)
        {
            Credentials = new NetworkCredential(_fromModel.FromMail, _fromModel.FromPassword),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = _fromModel.UseSsl,
            Timeout = 5 * 1000
        };
        try
        {
            // 解决远程证书验证无效
            //client.ServerCertificateValidationCallback = (sender, cert, chain, error) => true;
            // 将邮件发送到SMTP邮件服务器
            client.Send(message);
            return true;
        }
        catch (SmtpFailedRecipientsException ex)
        {
            foreach (var t in ex.InnerExceptions)
            {
                var status = t.StatusCode;
                if (status == SmtpStatusCode.MailboxBusy || status == SmtpStatusCode.MailboxUnavailable)
                {
                    "Delivery failed - retrying in 5 seconds.".WriteLineError();
                    await Task.Delay(5000);
                    client.Send(message);
                }
                else
                {
                    $"Failed to deliver message to {t.FailedRecipient}".WriteLineError();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Exception caught in RetryIfBusy(): {ex}");
        }
        return false;
    }
}