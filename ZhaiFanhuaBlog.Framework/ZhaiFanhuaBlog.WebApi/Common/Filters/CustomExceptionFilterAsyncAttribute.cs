// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomExceptionFilterAsyncAttribute
// Guid:0c556f22-3f97-4ea7-aa0c-78d8d5722cc4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-15 下午 11:13:23
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ZhaiFanhuaBlog.Models.Response;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 异步异常处理过滤器属性（一般用于捕捉异常）
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class CustomExceptionFilterAsyncAttribute : Attribute, IAsyncExceptionFilter
{
    private readonly ILogger<CustomExceptionFilterAsyncAttribute> _ILogger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    public CustomExceptionFilterAsyncAttribute(ILogger<CustomExceptionFilterAsyncAttribute> logger)
    {
        _ILogger = logger;
    }

    /// <summary>
    /// 当异常发生时
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        Console.WriteLine("CustomExceptionFilterAsyncAttribute.OnExceptionAsync Before");
        // 异常是否被处理过，没有则在这里处理
        if (context.ExceptionHandled == false)
        {
            if (context.Exception is ApplicationException)
            {
                // 应用程序业务级异常
                // 判断是否Ajax请求，是就返回Json
                //if (this.IsAjaxRequest(context.HttpContext.Request))
                //{
                context.Result = new JsonResult(ResultResponse.BadRequest(context.Exception.Message));
                //}
            }
            else
            {
                // 系统级别异常，不直接明文显示
                context.Result = new JsonResult(ResultResponse.InternalServerError());
            }
            // 请求IP
            string ip = context.HttpContext.Connection.RemoteIpAddress == null ? string.Empty : context.HttpContext.Connection.RemoteIpAddress.ToString();
            // 请求域名
            string host = context.HttpContext.Request.Host.Value;
            // 请求路径
            string path = context.HttpContext.Request.Path;
            // 请求参数
            string queryString = context.HttpContext.Request.QueryString.Value ?? string.Empty;
            // 请求方法
            string method = context.HttpContext.Request.Method;
            // 请求头
            string headers = JsonConvert.SerializeObject(context.HttpContext.Request.Headers);
            // 请求Cookie
            string cookies = JsonConvert.SerializeObject(context.HttpContext.Request.Cookies);
            string error = $"------------------\n" +
                    $"\t 【请求IP】：{ip}\n" +
                    $"\t 【请求地址】：{host + path + queryString}\n" +
                    $"\t 【请求方法】：{method}\n" +
                    $"\t 【请求头】：{headers}\n" +
                    $"\t 【请求Cookie】：{cookies}";
            _ILogger.LogError(context.Exception, error);
        }
        // 标记异常已经处理过了
        context.ExceptionHandled = true;
        await Task.CompletedTask;
        Console.WriteLine("CustomExceptionFilterAsyncAttribute.OnExceptionAsync After");
    }

    /// <summary>
    /// 判断是否Ajax请求
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private bool IsAjaxRequest(HttpRequest request)
    {
        string header = request.Headers["X-Request-With"];
        return "XMLHttpRequest".Equals(header);
    }
}