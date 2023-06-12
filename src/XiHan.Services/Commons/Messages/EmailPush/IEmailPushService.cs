#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IEmailPushService
// Guid:a4bee990-b81b-4883-a722-908a55905543
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-07 下午 01:32:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses.Results;
using XiHan.Subscriptions.Robots.Email;

namespace XiHan.Services.Commons.Messages.EmailPush;

/// <summary>
/// IEmailPushService
/// </summary>
public interface IEmailPushService
{
    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <returns></returns>
    Task<BaseResultDto> SendEmail(EmailToModel emailTo);
}