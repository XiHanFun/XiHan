#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:EmailPushService
// Guid:64280e3f-fbb4-4049-bedb-b5f53c93a28b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-12-07 下午 01:31:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using Serilog;
using XiHanCore.Infrastructures.Apps.Services;
using XiHanCore.Infrastructures.Responses;
using XiHanCore.Models.Syses;
using XiHanCore.Services.Bases;
using XiHanCore.Services.Syses.Users;
using XiHanCore.Subscriptions.Bots.Email;
using XiHanCore.Utils.Exceptions;

namespace XiHanCore.Services.Commons.Messages.EmailPush;

/// <summary>
/// EmailPushService
/// </summary>
[AppService(ServiceType = typeof(IEmailPushService), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class EmailPushService : BaseService<SysEmail>, IEmailPushService
{
    private readonly ILogger _logger = Log.ForContext<EmailPushService>();
    private readonly ISysUserService _sysUserService;
    private readonly EmailBot _emailBot;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysUserService"></param>
    /// <exception cref="CustomException"></exception>
    public EmailPushService(ISysUserService sysUserService)
    {
        _sysUserService = sysUserService;
        var emailFrom = GetEmailFrom().Result ?? throw new CustomException("未添加邮件推送配置或配置不可用！");
        _emailBot = new EmailBot(emailFrom);
    }

    /// <summary>
    /// 获取连接对象
    /// </summary>
    /// <returns></returns>
    private async Task<EmailFromModel> GetEmailFrom()
    {
        var sysEmail = await GetFirstAsync(e => e.IsEnabled);
        var emailFromModel = sysEmail.Adapt<EmailFromModel>();
        return emailFromModel;
    }

    #region Email

    /// <summary>
    /// 发送验证码邮件
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="userEmail"></param>
    /// <param name="captcha"></param>
    /// <returns></returns>
    public async Task<ApiResult> SendCaptchaMail(string userName, string userEmail, string captcha)
    {
        var body =
            @"<section style='background: linear-gradient(left , rgb(183, 244, 250) 1% , rgb(171, 174, 253) 100%);background: -o-linear-gradient(left , rgb(183, 244, 250) 1% , rgb(171, 174, 253) 100%);background: -ms-linear-gradient(left , rgb(183, 244, 250) 1% , rgb(171, 174, 253) 100%);background: -moz-linear-gradient(left , rgb(183, 244, 250) 1% , rgb(171, 174, 253) 100%);background: -webkit-linear-gradient(left , rgb(183, 244, 250) 1% , rgb(171, 174, 253) 100%);margin-top:10px;margin-bottom: 10px;'>
							<section style='border-style: solid;border-width: 1px;border-color: #afafaf;box-sizing: border-box;'>
								<section style='border-style:solid;border-left:none;border-top:none;border-width:1px;border-color:#afafaf;padding:0px 5px 5px 0px;display:inline-block;float:left;'>
									<section style='width: 5px; height: 5px; background-color: #afafaf; border-width: 0px; border-color: #ef0c0c;'></section>
								</section>
								<section style='border-style:solid;border-right:none;border-top:none;border-width:1px;border-color:#afafaf;padding:0px 0px 5px 5px;display:inline-block;float:right;'>
									<section style='width: 5px; height: 5px; background-color: #afafaf; border-width: 0px; border-color: #3e3e3e;'><br/></section>
								</section>
								<section style='Clean: both;box-sizing: border-box;'></section>
								<section style='background: linear-gradient(left , rgb(183, 250, 191) 1% , rgb(253, 232, 219) 100%);background: -o-linear-gradient(left , rgb(183, 250, 191) 1% , rgb(253, 232, 219) 100%);background: -ms-linear-gradient(left , rgb(183, 250, 191) 1% , rgb(253, 232, 219) 100%);background: -moz-linear-gradient(left , rgb(183, 250, 191) 1% , rgb(253, 232, 219) 100%);background: -webkit-linear-gradient(left , rgb(183, 250, 191) 1% , rgb(253, 232, 219) 100%);margin: -5px 5px; border-style: solid; border-width: 1px; border-color: #afafaf; box-sizing: border-box; padding: 10px 15px; text-align: left;'>
									<p style='letter-spacing: 2px; line-height: normal; text-align: left;'><span style='font-size: 10px; color: #4c4c4c; font-family: &quot;lucida Grande&quot;, Verdana, &quot;Microsoft YaHei&quot;;'>亲爱的【<span style='font-family: &quot;lucida Grande&quot;, Verdana, &quot;Microsoft YaHei&quot;; font-size: 10px; color: #0052ff;'>" +
            userName + @"</span></span><span style='font-size: 10px;'>】：</span></p>
									<p style='letter-spacing: 2px; line-height: normal; text-align: left;'><span style='font-size: 10px;  color: #4c4c4c;'>&nbsp;&nbsp;&nbsp;&nbsp;感谢您注册和使用曦寒。</span><span style='color: #4c4c4c; font-family: &quot;lucida Grande&quot;, Verdana, &quot;Microsoft YaHei&quot;; font-size: 10px;'>您的帐号正在进行身份验证，请将以下内容填入对应的邮箱验证码输入框。</span>
									</p>
									<p style='letter-spacing: 2px; line-height: normal; text-align: center;'><span style='font-family: &quot;lucida Grande&quot;, Verdana, &quot;Microsoft YaHei&quot;; font-size: 20px; color: #ff2941;'>" +
            captcha + @"</span></p>
									<p style='letter-spacing: 2px; line-height: normal; text-align: left;'><span style='font-size: 12px; text-align: center; color: #000000; font-family: &quot;lucida Grande&quot;, Verdana, &quot;Microsoft YaHei&quot;;'>&nbsp;&nbsp;&nbsp;</span><span style='font-size: 10px;'><span style='text-align: center; color: #000000; font-family: &quot;lucida Grande&quot;, Verdana, &quot;Microsoft YaHei&quot;;'>&nbsp;<span style='color: #4c4c4c; font-family: &quot;lucida Grande&quot;, Verdana, &quot;Microsoft YaHei&quot;;'>为了保障您帐号的安全性，</span>请您尽快完成验证</span><span style='color: #4c4c4c; font-family: &quot;lucida Grande&quot;, Verdana, &quot;Microsoft YaHei&quot;; text-align: center;'>确认</span><span style='color: #000000; font-family: &quot;lucida Grande&quot;, Verdana, &quot;Microsoft YaHei&quot;; text-align: center;'>。</span></span>
									</p>
									<p style='letter-spacing: 2px; text-align: right; line-height: normal;'><span style='font-size: 10px;'>曦寒</span></p>
									<p style='letter-spacing: 2px; text-align: right; line-height: normal;'><span style='font-size: 10px;'>" +
            DateTime.Now.ToShortDateString() + "," + DateTime.Now.ToShortTimeString() + @"</span></p>
								</section>
								<section style='border-style:solid;border-left:none;border-bottom:none;border-width:1px;border-color:#afafaf;padding:5px 5px 0px 0px;display:inline-block;float:left;'>
									<section style='width: 5px; height: 5px; background-color: #afafaf; border-width: 0px; border-color: #3e3e3e;'></section>
								</section>
								<section style='border-style:solid;border-right:none;border-bottom:none;border-width:1px;border-color:#afafaf;padding:5px 0px 0px 5px;display:inline-block;float:right;'>
									<section style='width: 5px; height: 5px; background-color: #afafaf; border-width: 0px; border-color: #3e3e3e;'></section>
								</section>
								<section style='Clean: both;box-sizing: border-box;'></section>
							</section>
						</section>
						<section class='_editor'>
							<p style='text-align: center;'><span style='font-size: 10px;'>Copyright &copy;<time>2016-" +
            DateTime.Now.Year +
            @"</time>&nbsp;&nbsp;<a href=string.Emptyhttps://www.zhaifanhua.comstring.Empty>ZhaiFanhua</a>&nbsp;All Rights Reserved.</span></p>
						</section>";

        EmailToModel emailTo = new()
        {
            Subject = "曦寒账号验证",
            Body = body,
            IsBodyHtml = true,
            ToMail = [userEmail]
        };
        return await SendEmail(emailTo);
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <returns></returns>
    private async Task<ApiResult> SendEmail(EmailToModel emailTo)
    {
        string? logoInfo;
        if (await _emailBot.SendMail(emailTo))
        {
            logoInfo = "邮件发送成功！";
            _logger.Information(logoInfo);
            return ApiResult.Success(logoInfo);
        }

        logoInfo = "邮件发送失败！";
        _logger.Error(logoInfo);
        return ApiResult.BadRequest(logoInfo);
    }

    #endregion
}