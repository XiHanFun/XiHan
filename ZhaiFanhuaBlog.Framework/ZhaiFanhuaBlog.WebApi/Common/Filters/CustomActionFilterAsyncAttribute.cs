// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomActionFilterAsyncAttribute
// Guid:17255225-ef95-b047-1def-1fdb88733464
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-05 上午 03:49:37
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Security.Claims;
using ZhaiFanhuaBlog.Models.Bases.Response.Model;
using ZhaiFanhuaBlog.Models.Response;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 异步请求过滤器属性(一般用于模型验证、记录日志、篡改参数、篡改返回值、统一参数验证、实现数据库事务自动开启关闭等)
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class CustomActionFilterAsyncAttribute : Attribute, IAsyncActionFilter
{
    private readonly ILogger<CustomActionFilterAsyncAttribute> _ILogger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    public CustomActionFilterAsyncAttribute(ILogger<CustomActionFilterAsyncAttribute> logger)
    {
        _ILogger = logger;
    }

    /// <summary>
    /// 在某请求时执行
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("CustomActionFilterAsyncAttribute.OnActionExecutionAsync Before");
        // 模型验证
        if (!context.ModelState.IsValid)
        {
            var validResult = context.ModelState.Keys
                .SelectMany(key => context!.ModelState[key]!.Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList<dynamic>();
            context.Result = new JsonResult(ResultResponse.UnprocessableEntity(validResult.Count, validResult));
        }
        else
        {
            // 获取控制器、路由信息
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            // 获取请求的方法
            var method = actionDescriptor!.MethodInfo;
            // 获取 HttpContext 和 HttpRequest 对象
            var httpContext = context.HttpContext;
            var httpRequest = httpContext.Request;
            // 获取客户端 Ipv4 地址
            var remoteIPv4 = httpContext.Connection.RemoteIpAddress == null ? string.Empty : httpContext.Connection.RemoteIpAddress.ToString();
            // 获取请求的 Url 地址(协议、域名、路径、参数)
            var requestUrl = httpRequest.Protocol + httpRequest.Host.Value + httpRequest.Path + httpRequest.QueryString.Value ?? string.Empty;
            // 获取请求参数（写入日志，需序列化成字符串后存储），可以自由篡改
            var parameters = context.ActionArguments;
            // 获取操作人（必须授权访问才有值）"userId" 为你存储的 claims type，jwt 授权对应的是 payload 中存储的键名
            var userId = httpContext.User?.FindFirstValue("userId");
            // 请求时间
            var requestedTime = DateTimeOffset.Now;
            // 写入日志
            string info = $"\n" +
                   $"\t 【请求IP】：{remoteIPv4}\n" +
                   $"\t 【请求地址】：{requestUrl}\n" +
                   $"\t 【请求方法】：{method}\n";
            _ILogger.LogInformation(info);
            //============== 这里是执行方法之后获取数据 ====================
            // 请求构造函数和方法,调用下一个过滤器
            ActionExecutedContext actionExecuted = await next();
            try
            {
                if (actionExecuted.Result != null)
                {
                    // 获取返回的结果
                    var returnResult = actionExecuted.Result as ActionResult;
                    // 判断是否请求成功，没有异常就是请求成功
                    var isRequestSucceed = actionExecuted.Exception == null;
                    // 其他操作，如写入日志
                    _ILogger.LogInformation($"请求结果为【{JsonConvert.SerializeObject(returnResult)}】");
                }
            }
            catch (Exception)
            {
                throw new Exception("日志未获取到结果，返回的数据无法序列化;");
            }
        }
        Console.WriteLine("CustomActionFilterAsyncAttribute.OnActionExecutionAsync After");
    }
}