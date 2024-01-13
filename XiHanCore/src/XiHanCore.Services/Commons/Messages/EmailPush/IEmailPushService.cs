#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IEmailPushService
// Guid:a4bee990-b81b-4883-a722-908a55905543
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-12-07 下午 01:32:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHanCore.Infrastructures.Responses;
using XiHanCore.Models.Syses;
using XiHanCore.Services.Bases;

namespace XiHanCore.Services.Commons.Messages.EmailPush;

/// <summary>
/// IEmailPushService
/// </summary>
public interface IEmailPushService : IBaseService<SysEmail>
{
    /// <summary>
    /// 发送验证码邮件
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="userEmail"></param>
    /// <param name="captcha"></param>
    /// <returns></returns>
    Task<ApiResult> SendCaptchaMail(string userName, string userEmail, string captcha);
}