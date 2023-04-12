#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:EmailHelper
// Guid:6cd058a5-9ec5-4ab3-8c7d-aeab0f513700
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 上午 01:21:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Net;
using System.Net.Mail;
using XiHan.Utils.Consoles;

namespace XiHan.Utils.Messages.Email;

/// <summary>
/// 邮件帮助类
/// </summary>
public static class EmailHelper
{
    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="model">发送参数</param>
    public static async Task<bool> Send(EmailModel model)
    {
        // 初始化邮件实例
        using MailMessage message = new()
        {
            // 来源或发送者
            From = new MailAddress(model.FromMail, model.FromName, model.Coding),
            Sender = new MailAddress(model.FromMail, model.FromName, model.Coding),
            // 邮件主题
            Subject = model.Subject,
            SubjectEncoding = model.Coding,
            // 邮件正文
            Body = model.Body,
            BodyEncoding = model.Coding,
            // 优先级（高）
            Priority = MailPriority.High,
            // 网页形式
            IsBodyHtml = true,
        };
        // 收件人地址集合
        model.ToMail.ForEach(to => message.To.Add(to));
        // 抄送人地址集合
        model.CcMail.ForEach(cc => message.CC.Add(cc));
        // 密送人地址集合
        model.BccMail.ForEach(bcc => message.Bcc.Add(bcc));
        // 在有附件的情况下添加附件
        model.AttachmentsPath.ForEach(path => message.Attachments.Add(path));

        // 初始化连接实例
        using SmtpClient client = new(model.Host, model.Port)
        {
            Credentials = new NetworkCredential(model.FromMail, model.FromPassword),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = model.UseSsl,
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
            for (var i = 0; i < ex.InnerExceptions.Length; i++)
            {
                var status = ex.InnerExceptions[i].StatusCode;
                if (status == SmtpStatusCode.MailboxBusy || status == SmtpStatusCode.MailboxUnavailable)
                {
                    "Delivery failed - retrying in 5 seconds.".WriteLineError();
                    await Task.Delay(5000);
                    client.Send(message);
                }
                else
                {
                    $"Failed to deliver message to {ex.InnerExceptions[i].FailedRecipient}".WriteLineError();
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