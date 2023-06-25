#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AuthorizationFilterAsyncAttribute
// Guid:40387d18-5714-4ff2-96aa-164a967419fb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-19 下午 02:47:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Security.Authentication;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Apps.HttpContexts;

namespace XiHan.Application.Filters;

/// <summary>
/// 异步授权过滤器属性（一般用于验证授权）
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class AuthorizationFilterAsyncAttribute : Attribute, IAsyncAuthorizationFilter
{
    // 日志开关
    private readonly bool _authorizationLogSwitch = AppSettings.LogConfig.Authorization.GetValue();

    private readonly ILogger _logger = Log.ForContext<AuthorizationFilterAsyncAttribute>();

    /// <summary>
    /// 在某授权时执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        // 控制器信息
        var actionContextInfo = context.GetActionContextInfo();
        // 是否授权访问
        var isAuthorize = context.Filters.Any(filter => filter is IAuthorizationFilter)
                            || actionContextInfo.ControllerType!.IsDefined(typeof(AuthorizeAttribute), true)
                            || actionContextInfo.MethodInfo!.IsDefined(typeof(AuthorizeAttribute), true);
        // 写入日志
        var info = $"\t 请求Ip：{actionContextInfo.RemoteIp}\n" +
                   $"\t 请求地址：{actionContextInfo.RequestUrl}\n" +
                   $"\t 请求方法：{actionContextInfo.MethodInfo}\n" +
                   $"\t 操作用户：{actionContextInfo.UserId}";
        // 授权访问就进行权限检查
        if (isAuthorize)
        {
            var identities = context.HttpContext.User.Identities;
            // 验证权限
            if (identities == null)
            {
                if (_authorizationLogSwitch)
                    _logger.Information($"认证参数异常\n{info}");
                // 认证参数异常
                throw new AuthenticationException();
            }
            else
            {
            }
        }
        // 匿名访问直接跳过处理
        else await Task.CompletedTask;
    }
}