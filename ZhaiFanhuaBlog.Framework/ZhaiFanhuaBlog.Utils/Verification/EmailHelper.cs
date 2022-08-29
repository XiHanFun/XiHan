// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:EmailHelper
// Guid:6cd058a5-9ec5-4ab3-8c7d-aeab0f513700
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 上午 01:21:27
// ----------------------------------------------------------------

using System.Net.Mail;
using System.Text;

namespace ZhaiFanhuaBlog.Utils.Verification;

/// <summary>
/// 邮件帮助类
/// </summary>
public static class EmailHelper
{
    /// <summary>
    /// 发件人地址
    /// </summary>
    public static string? MailFrom { get; set; } = null;

    /// <summary>
    /// 发件人密码(授权码)
    /// </summary>
    public static string? MailPwd { get; set; } = null;

    /// <summary>
    /// 收件人地址
    /// </summary>
    public static string[]? MailToArray { get; set; }

    /// <summary>
    /// 抄送人地址
    /// </summary>
    public static string[]? MailCcArray { get; set; }

    /// <summary>
    /// 邮件标题
    /// </summary>
    public static string? MailSubject { get; set; } = null;

    /// <summary>
    /// 邮件正文
    /// </summary>
    public static string? MailBody { get; set; } = null;

    /// <summary>
    /// SMTP邮件服务器
    /// </summary>
    public static string? SMTPHost { get; set; } = null;

    /// <summary>
    /// 正文是否是html格式
    /// </summary>
    public static bool IsbodyHtml { get; set; }

    /// <summary>
    /// 附件
    /// </summary>
    public static string[]? AttachmentsPath { get; set; }

    /// <summary>
    /// 发送
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool Send()
    {
        // 使用指定的邮件地址初始化MailAddress实例
        MailAddress mailAddress = new(MailFrom!);
        // 初始化MailMessage实例
        using MailMessage mailMessage = new();
        // 向收件人地址集合添加邮件地址
        if (MailToArray != null)
        {
            for (int i = 0; i < MailToArray.Length; i++)
            {
                mailMessage.To.Add(MailToArray[i].ToString());
            }
        }
        // 向抄送人地址集合添加邮件地址
        if (MailCcArray != null)
        {
            for (int i = 0; i < MailCcArray.Length; i++)
            {
                mailMessage.CC.Add(MailCcArray[i].ToString());
            }
        }
        // 发件人地址
        mailMessage.From = mailAddress;
        // 电子邮件的标题
        mailMessage.Subject = MailSubject;
        // 电子邮件的主题内容使用的编码
        mailMessage.SubjectEncoding = Encoding.UTF8;
        // 电子邮件正文
        mailMessage.Body = MailBody;
        // 电子邮件正文的编码
        mailMessage.BodyEncoding = Encoding.Default;
        mailMessage.Priority = MailPriority.High;
        mailMessage.IsBodyHtml = IsbodyHtml;
        // 在有附件的情况下添加附件
        try
        {
            if (AttachmentsPath != null && AttachmentsPath.Length > 0)
            {
                Attachment? attachment = null;
                foreach (string path in AttachmentsPath)
                {
                    attachment = new Attachment(path);
                    mailMessage.Attachments.Add(attachment);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("在添加附件时有错误:" + ex);
        }
        using SmtpClient smtpClient = new()
        {
            EnableSsl = true,
            // 指定发件人的邮件地址和密码以验证发件人身份
            Credentials = new System.Net.NetworkCredential(MailFrom, MailPwd),
            // 设置SMTP邮件服务器
            Host = SMTPHost!
        };
        try
        {
            // 解决远程证书验证无效
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, error) =>
            {
                return true;
            };
            // 将邮件发送到SMTP邮件服务器
            smtpClient.Send(mailMessage);
            return true;
        }
        catch (SmtpException)
        {
            return false;
        }
    }
}