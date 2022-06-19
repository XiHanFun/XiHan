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
[AttributeUsage(AttributeTargets.Class)]
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
                .SelectMany(key => context!.ModelState[key]!.Errors.Select(x => new ValidationModel(key, x.ErrorMessage)))
                .ToList<dynamic>();
            context.Result = new JsonResult(ResultResponse.UnprocessableEntity(validResult.Count, validResult));
        }
        else
        {
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
            // 请求IP
            string ip = context.HttpContext.Connection.RemoteIpAddress == null ? string.Empty : context.HttpContext.Connection.RemoteIpAddress.ToString();
            _ILogger.LogInformation($"发出请求【{host + path + queryString}】，请求方法为【{method}】，请求头【{headers}】，请求Cookie【{cookies}】方法，请求IP为【{ip}】");
            // 请求构造函数和方法,调用下一个过滤器
            ActionExecutedContext actionExecuted = await next();
            // 执行结果
            try
            {
                if (actionExecuted.Result != null)
                {
                    var result = JsonConvert.SerializeObject(actionExecuted.Result);
                    _ILogger.LogInformation($"请求结果为【{result}】");
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