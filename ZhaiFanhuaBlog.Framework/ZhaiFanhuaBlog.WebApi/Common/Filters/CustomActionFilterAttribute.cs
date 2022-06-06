// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomActionFilterAttribute
// Guid:b5f9558b-b744-b361-b103-eb24d6c0d20e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-05 上午 03:55:25
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 请求过滤器属性(一般用于记录日志)
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class CustomActionFilterAttribute : Attribute, IActionFilter
{
    // 日志组件
    private readonly ILogger<CustomActionFilterAttribute> _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    public CustomActionFilterAttribute(ILogger<CustomActionFilterAttribute> logger)
    {
        Console.WriteLine(logger.GetType().Name);
        _logger = logger;
    }

    /// <summary>
    /// 在某请求之前执行
    /// </summary>
    /// <param name="context"></param>
    public void OnActionExecuting(ActionExecutingContext context)
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
    public void OnActionExecuted(ActionExecutedContext context)
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
}