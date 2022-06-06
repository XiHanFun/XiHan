// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomActionFilterAsyncAttribute
// Guid:17255225-ef95-b047-1def-1fdb88733464
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-05 上午 03:49:37
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 请求过滤器属性(一般用于记录日志)
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class CustomActionFilterAsyncAttribute : Attribute, IAsyncActionFilter
{
    // 日志组件
    private readonly ILogger<CustomActionFilterAsyncAttribute> _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    public CustomActionFilterAsyncAttribute(ILogger<CustomActionFilterAsyncAttribute> logger)
    {
        _logger = logger;
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
        // 请求域名
        string host = context.HttpContext.Request.Host.Value;
        // 请求路径
        string path = context.HttpContext.Request.Path;
        // 请求参数
        string para = context.HttpContext.Request.QueryString.Value ?? string.Empty;
        // 请求地址
        string url = (host + path + para).ToLower();
        // 请求方式
        string method = context.HttpContext.Request.Method;
        // 请求控制器
        var controller = context.Controller.ToString();
        // 请求方法
        var action = context.ActionDescriptor.DisplayName;
        _logger.LogInformation($"请求路径为【{url}】，请求方式为【{method}】，执行了【{controller}】控制器的【{action}】方法，请求参数为【{para}】");
        // 请求构造函数和方法
        ActionExecutedContext actionExecuted = await next.Invoke();
        // 执行结果
        var result = JsonConvert.SerializeObject(actionExecuted.Result);
        _logger.LogInformation($"执行结果为{result}");
        Console.WriteLine("CustomActionFilterAsyncAttribute.OnActionExecutionAsync After");
    }
}