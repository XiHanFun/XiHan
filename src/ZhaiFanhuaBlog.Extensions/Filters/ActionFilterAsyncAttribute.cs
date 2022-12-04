#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ActionFilterAsyncAttribute
// Guid:17255225-ef95-b047-1def-1fdb88733464
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-05 上午 03:49:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Claims;
using ZhaiFanhuaBlog.Extensions.Response;
using ZhaiFanhuaBlog.Infrastructure.AppSetting;

namespace ZhaiFanhuaBlog.Extensions.Filters;

/// <summary>
/// 异步请求过滤器属性(一般用于模型验证、记录日志、篡改参数、篡改返回值、统一参数验证、实现数据库事务自动开启关闭等)
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class ActionFilterAsyncAttribute : Attribute, IAsyncActionFilter
{
    // 日志开关
    private readonly bool ActionLogSwitch = AppSettings.Logging.Action;

    private readonly ILogger<ActionFilterAsyncAttribute> _ILogger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iLogger"></param>
    public ActionFilterAsyncAttribute(ILogger<ActionFilterAsyncAttribute> iLogger)
    {
        _ILogger = iLogger;
    }

    /// <summary>
    /// 在某请求时执行
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 模型验证
        if (!context.ModelState.IsValid)
        {
            context.Result = new JsonResult(BaseResponseDto.UnprocessableEntity(context));
        }
        else
        {
            // 获取控制器、路由信息
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            // 获取请求的方法
            var method = actionDescriptor?.MethodInfo;
            // 获取 HttpContext 和 HttpRequest 对象
            var httpContext = context.HttpContext;
            var httpRequest = httpContext.Request;
            // 获取客户端 Ip 地址
            var remoteIp = httpContext.Connection.RemoteIpAddress == null ? string.Empty : httpContext.Connection.RemoteIpAddress.ToString();
            // 获取请求的 Url 地址(域名、路径、参数)
            var requestUrl = httpRequest.Host.Value + httpRequest.Path + httpRequest.QueryString.Value ?? string.Empty;
            // 获取请求参数（写入日志，需序列化成字符串后存储），可以自由篡改
            var parameters = context.ActionArguments;
            // 获取操作人（必须授权访问才有值）"UserId" 为你存储的 claims type，jwt 授权对应的是 payload 中存储的键名
            var userId = httpContext.User?.FindFirstValue("UserId");
            // 写入日志
            string info = $"\t 请求Ip：{remoteIp}\n" +
                          $"\t 请求地址：{requestUrl}\n" +
                          $"\t 请求方法：{method}\n" +
                          $"\t 请求参数：{parameters}\n" +
                          $"\t 操作用户：{userId}";
            if (ActionLogSwitch)
                _ILogger.LogInformation($"发起请求\n{info}");
            // 请求构造函数和方法,调用下一个过滤器
            ActionExecutedContext actionExecuted = await next();
            if (actionExecuted.Result != null)
            {
                // 获取返回的结果
                var returnResult = actionExecuted.Result as ActionResult;
                // 判断是否请求成功，没有异常就是请求成功
                var isRequestSucceed = actionExecuted.Exception == null;
                // 请求成功就写入日志
                if (isRequestSucceed && ActionLogSwitch)
                    _ILogger.LogInformation($"请求数据\n{info}\n {JsonConvert.SerializeObject(returnResult)}");
            }
        }
    }
}