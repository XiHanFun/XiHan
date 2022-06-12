// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomActionResultFilterAllAttribute
// Guid:82820792-f959-4c30-b6ad-e3d94ec83f76
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-13 上午 03:45:52
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ZhaiFanhuaBlog.ViewModels.Response;
using ZhaiFanhuaBlog.ViewModels.Response.Model;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// CustomActionResultFilterAllAttribute
/// </summary>
public class CustomAllActionResultFilterAttribute : ActionFilterAttribute
{
    // 日志组件
    private readonly ILogger<CustomAllActionResultFilterAttribute> _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    public CustomAllActionResultFilterAttribute(ILogger<CustomAllActionResultFilterAttribute> logger)
    {
        Console.WriteLine(logger.GetType().Name);
        _logger = logger;
    }

    /// <summary>
    /// 在某请求之前执行
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
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
        Console.WriteLine("CustomActionFilterAttribute.OnActionExecuting");
    }

    /// <summary>
    /// 在某请求之后执行
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        // 执行结果
        var result = string.Empty;
        try
        {
            if (context.Result != null)
            {
                // 执行结果
                result = JsonConvert.SerializeObject(context.Result);
            }
        }
        catch (Exception)
        {
            result = "日志未获取到结果，返回的数据无法序列化;";
        }
        _logger.LogInformation($"执行结果为{result}");
        Console.WriteLine("CustomActionFilterAttribute.OnActionExecuted");
    }

    /// <summary>
    /// 在某请求时执行
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
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

    /// <summary>
    /// 在某结果之前执行
    /// </summary>
    /// <param name="context"></param>
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        Console.WriteLine("CustomResultFilterAttribute.OnResultExecuting");
        if (context.Result is MessageModel)
        {
            MessageModel messageModel = (MessageModel)context.Result;
            if (messageModel.Success)
            {
                context.Result = (IActionResult)ResponseResult.OK(messageModel.Data!);
            }
        }
    }

    /// <summary>
    ///在某结果之后执行
    /// </summary>
    /// <param name="context"></param>
    public override void OnResultExecuted(ResultExecutedContext context)
    {
        Console.WriteLine("CustomResultFilterAttribute.OnResultExecuted");
    }

    /// <summary>
    /// 在某结果执行时
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        Console.WriteLine("CustomActionFilterAsyncAttribute.OnActionExecutionAsync Before");
        if (context.Result is MessageModel)
        {
            MessageModel messageModel = (MessageModel)context.Result;
            if (messageModel.Success)
            {
                context.Result = (IActionResult)ResponseResult.OK(messageModel.Data!);
            }
        }
        await next.Invoke();
        Console.WriteLine("CustomActionFilterAsyncAttribute.OnActionExecutionAsync After");
    }
}