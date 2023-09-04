#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:EmailBot
// Guid:6cd058a5-9ec5-4ab3-8c7d-aeab0f513700
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-09 上午 01:21:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using MailKit.Net.Smtp;
using MimeKit;
using XiHan.Utils.Extensions;

namespace XiHan.Subscriptions.Bots.Email;

/// <summary>
/// 邮件服务
/// </summary>
public class EmailBot
{
    private readonly EmailFromModel _fromModel;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fromModel"></param>
    public EmailBot(EmailFromModel fromModel)
    {
        _fromModel = fromModel;
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    public async Task<bool> SendMail(EmailToModel toModel)
    {
        MimeMessage message = new()
        {
            // 来源
            Sender = new MailboxAddress(_fromModel.FromMail, _fromModel.FromMail)
        };
        // 发件人地址集合
        message.From.Add(new MailboxAddress(_fromModel.FromMail, _fromModel.FromMail));
        // 收件人地址集合
        toModel.ToMail.ForEach(to => message.To.Add(new MailboxAddress(to, to)));
        // 抄送人地址集合
        toModel.CcMail.ForEach(cc => message.Cc.Add(new MailboxAddress(cc, cc)));
        // 密送人地址集合
        toModel.BccMail.ForEach(bcc => message.Bcc.Add(new MailboxAddress(bcc, bcc)));
        // 邮件主题
        message.Subject = toModel.Subject;
        // 邮件日期
        message.Date = DateTime.Now;
        // 邮件正文
        BodyBuilder bodyBuilder = new()
        {
            HtmlBody = toModel.IsBodyHtml ? toModel.Body : null,
            TextBody = toModel.IsBodyHtml ? null : toModel.Body
        };
        //在有附件的情况下添加附件
        try
        {
            if (toModel.AttachmentsPath != null && toModel.AttachmentsPath.Count > 0)
                toModel.AttachmentsPath.ForEach(attachmentFile =>
                {
                    MimePart attachment = new()
                    {
                        Content = new MimeContent(attachmentFile.ContentStream),
                        // 读取文件只能用绝对路径
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = attachmentFile.Name
                    };
                    bodyBuilder.Attachments.Add(attachment);
                });
        }
        catch (Exception ex)
        {
            throw new Exception($"在添加附件时有错误:{ex}");
        }

        message.Body = bodyBuilder.ToMessageBody();

        try
        {
            using SmtpClient client = new();
            // 解决远程证书验证无效
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            // 异步连接
            await client.ConnectAsync(_fromModel.SmtpHost, _fromModel.SmtpPort, _fromModel.UseSsl);
            // 异步登录
            await client.AuthenticateAsync(_fromModel.FromUserName, _fromModel.FromPassword);
            // 异步发送
            var result = await client.SendAsync(message);
            // 异步断连
            await client.DisconnectAsync(true);

            return true;
        }
        catch (Exception ex)
        {
            $"Failed to send email:{ex.Message}".WriteLineError();
        }

        return false;
    }
}