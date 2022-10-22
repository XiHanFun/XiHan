// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:EmailHelper
// Guid:6cd058a5-9ec5-4ab3-8c7d-aeab0f513700
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 上午 01:21:27
// ----------------------------------------------------------------

using System.Net;
using System.Net.Mail;
using System.Text;

namespace ZhaiFanhuaBlog.Utils.Mail;

/// <summary>
/// 邮件帮助类
/// </summary>
public static class EmailHelper
{
    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="model">发送参数</param>
    public static async Task Send(SendModel model)
    {
        try
        {
            // 初始化连接实例
            using SmtpClient client = new(model.Host, model.Port)
            {
                Credentials = new NetworkCredential(model.FromMail, model.FromPassword),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };
            // 初始化邮件实例
            using MailMessage message = new()
            {
                From = new MailAddress(model.FromMail, model.FromName, model.Coding)
            };
            // 收件人地址集合
            model.ToMail.ForEach(itemtomail => message.To.Add(itemtomail));
            // 抄送人地址集合
            model.CcMail.ForEach(itemccmail => message.CC.Add(itemccmail));
            //在有附件的情况下添加附件
            model.AttachmentsPath.ForEach(itempath => message.Attachments.Add(new Attachment(itempath)));
            // 邮件主题
            message.Subject = model.Subject;
            // 主题内容编码
            message.SubjectEncoding = model.Coding;
            // 邮件正文
            message.Body = model.Body;
            // 邮件正文编码
            message.BodyEncoding = model.Coding;
            // 优先级（高）
            message.Priority = MailPriority.High;
            // 使用默认证书
            client.UseDefaultCredentials = true;
            // 网页形式
            message.IsBodyHtml = true;
            //// 解决远程证书验证无效
            //ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, error) =>
            //{
            //    return true;
            //};

            // 将邮件发送到SMTP邮件服务器
            await client.SendMailAsync(message);
        }
        catch (SmtpException ex)
        {
            throw new Exception("邮件发送出错:" + ex);
        }
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    public class SendModel
    {
        /// <summary>
        /// 服务
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 587;

        /// <summary>
        /// 发送者邮箱
        /// </summary>
        public string FromMail { get; set; } = string.Empty;

        /// <summary>
        /// 发送者名称
        /// </summary>
        public string FromName { get; set; } = string.Empty;

        /// <summary>
        /// 发送者邮箱密码
        /// </summary>
        public string FromPassword { get; set; } = string.Empty;

        /// <summary>
        /// 发送主题
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// 发送内容
        /// </summary>
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// 内容编码
        /// </summary>
        public Encoding Coding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 接收者邮箱
        /// </summary>
        public List<string> ToMail { get; set; } = new List<string>();

        /// <summary>
        /// 抄送给邮箱
        /// </summary>
        public List<string> CcMail { get; set; } = new List<string>();

        /// <summary>
        /// 附件
        /// </summary>
        public List<string> AttachmentsPath { get; set; } = new List<string>();
    }
}