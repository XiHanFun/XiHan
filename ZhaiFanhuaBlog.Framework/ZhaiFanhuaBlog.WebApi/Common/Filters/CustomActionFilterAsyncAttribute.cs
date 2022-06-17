// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomActionFilterAsyncAttribute
// Guid:17255225-ef95-b047-1def-1fdb88733464
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-05 上午 03:49:37
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ZhaiFanhuaBlog.WebApi.Common.Response;
using ZhaiFanhuaBlog.WebApi.Common.Response.Model;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 请求过滤器属性(一般用于模型验证、记录日志)
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class CustomActionFilterAsyncAttribute : ActionFilterAttribute
{
    // 日志组件
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
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("CustomActionFilterAsyncAttribute.OnActionExecutionAsync Before");
        // 模型验证
        if (!context.ModelState.IsValid)
        {
            var validResult = context.ModelState.Keys
                .SelectMany(key => context!.ModelState[key]!.Errors.Select(x => new ValidationModel(key, x.ErrorMessage)))
                .ToList();
            context.Result = new ObjectResult(validResult);
        }
        else
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
            string message = $"请求路径为【{url}】，请求方式为【{method}】，执行了【{controller}】控制器的【{action}】方法，请求参数为【{para}】";
            _ILogger.LogInformation(message);
            // 请求构造函数和方法
            ActionExecutedContext actionExecuted = await next.Invoke();
            // 执行结果
            var result = JsonConvert.SerializeObject(actionExecuted.Result);
            _ILogger.LogInformation($"执行结果为{result}");
        }
        Console.WriteLine("CustomActionFilterAsyncAttribute.OnActionExecutionAsync After");
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
        // 不为空就处理
        if (context.Result != null)
        {
            if (context.Result is ObjectResult objectResult)
            {
                context.Result = new JsonResult(ResultResponse.OK(objectResult!.Value!));
            }
            else if (context.Result is ContentResult contentResult)
            {
                context.Result = new JsonResult(ResultResponse.OK(contentResult!.Content!));
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new JsonResult(ResultResponse.OK());
            }
            else
            {
                throw new Exception($"未经处理的Result类型：{context.Result.GetType().Name}");
            }
        }
        await next.Invoke();
        Console.WriteLine("CustomActionFilterAsyncAttribute.OnActionExecutionAsync After");
    }
}