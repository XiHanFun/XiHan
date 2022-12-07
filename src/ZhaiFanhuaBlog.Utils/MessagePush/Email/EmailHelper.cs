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
using ZhaiFanhuaBlog.Utils.Console;

namespace ZhaiFanhuaBlog.Utils.MessagePush.Email;

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
        // 初始化连接实例
        using SmtpClient client = new(model.Host, model.Port)
        {
            Credentials = new NetworkCredential(model.FromMail, model.FromPassword),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = model.UseSsl,
            //若已经指定了 Credentials，此值必须为 false，否则出现一下错误：
            // Mailbox name not allowed. The server response was: authentication is required.
            // UseDefaultCredentials = false,
            Timeout = 5 * 1000
        };
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

        try
        {
            // 将邮件发送到SMTP邮件服务器
            await client.SendMailAsync(message);
            return true;
        }
        catch (SmtpFailedRecipientsException ex)
        {
            for (int i = 0; i < ex.InnerExceptions.Length; i++)
            {
                SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                if (status == SmtpStatusCode.MailboxBusy || status == SmtpStatusCode.MailboxUnavailable)
                {
                    ConsoleHelper.WriteLineError("Delivery failed - retrying in 5 seconds.");
                    Thread.Sleep(5000);
                    await client.SendMailAsync(message);
                }
                else
                {
                    ConsoleHelper.WriteLineError($"Failed to deliver message to {ex.InnerExceptions[i].FailedRecipient}");
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Exception caught in RetryIfBusy(): {ex}");
        }
        return false;
    }
}