// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomAuthorizationFilterAsyncAttribute
// Guid:40387d18-5714-4ff2-96aa-164a967419fb
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-07-19 下午 02:47:58
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ZhaiFanhuaBlog.Models.Response;
using ZhaiFanhuaBlog.Utils.Config;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 异步授权过滤器属性（一般用于验证授权）
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class CustomAuthorizationFilterAsyncAttribute : Attribute, IAsyncAuthorizationFilter
{
    // 日志开关
    private readonly bool AuthorizationLogSwitch = ConfigHelper.Configuration.GetValue<bool>("Logging:Switch:Authorization");

    private readonly ILogger<CustomActionFilterAsyncAttribute> _ILogger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iLogger"></param>
    public CustomAuthorizationFilterAsyncAttribute(ILogger<CustomActionFilterAsyncAttribute> iLogger)
    {
        _ILogger = iLogger;
    }

    /// <summary>
    /// 在某授权时执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        // 获取控制器、路由信息
        var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        // 获取请求的方法
        var method = actionDescriptor!.MethodInfo;
        // 获取 Action 类型
        var methodType = actionDescriptor.MethodInfo;
        // 获取 HttpContext 对象
        var httpContext = context.HttpContext;
        var httpRequest = httpContext.Request;
        // 获取控制器类型
        var controllerType = actionDescriptor!.ControllerTypeInfo;
        // 获取客户端 Ip 地址
        var remoteIp = httpContext.Connection.RemoteIpAddress == null ? string.Empty : httpContext.Connection.RemoteIpAddress.ToString();
        // 获取请求的 Url 地址(域名、路径、参数)
        var requestUrl = httpRequest.Host.Value + httpRequest.Path + httpRequest.QueryString.Value ?? string.Empty;
        // 是否匿名访问
        var allowAnonymouse = context.Filters.Any(filter => filter is IAllowAnonymousFilter)
                            || controllerType.IsDefined(typeof(AllowAnonymousAttribute), true)
                            || methodType.IsDefined(typeof(AllowAnonymousAttribute), true);
        // 不是匿名才处理权限检查
        if (!allowAnonymouse)
        {
            var Identities = httpContext.User.Identities;
            // 验证权限
            if (Identities == null)
            {
                // 返回未授权
                context.Result = new JsonResult(ResultResponse.Unauthorized());
                // 写入日志
                string info = $"\t 请求Ip：{remoteIp}\n" +
                       $"\t 请求地址：{requestUrl}\n" +
                       $"\t 请求方法：{method}";
                if (AuthorizationLogSwitch)
                    _ILogger.LogInformation($"请求未授权\n{info}");
            }
        }
        // 否则直接跳过处理
        else await Task.CompletedTask;
    }
}