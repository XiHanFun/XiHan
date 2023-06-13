#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ResultFilterAsyncAttribute
// Guid:0c941b38-e677-4251-a014-2e96fa572311
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-18 上午 01:48:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Text.Json;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Responses.Results;

namespace XiHan.Web.Filters;

/// <summary>
/// 异步结果过滤器属性(一般用于返回统一模型数据)
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class ResultFilterAsyncAttribute : Attribute, IAsyncResultFilter
{
    // 日志开关
    private readonly bool _resultLogSwitch = AppSettings.LogConfig.Result.GetValue();

    private readonly ILogger _logger = Log.ForContext<ResultFilterAsyncAttribute>();

    /// <summary>
    /// 构造函数
    /// </summary>
    public ResultFilterAsyncAttribute()
    {
    }

    /// <summary>
    /// 在某结果执行时
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        // 不为空就做处理
        if (context.Result is BaseResultDto resultModel)
        {
            // 如果是通用数据类返回结果，则转换为json结果
            context.Result = new JsonResult(resultModel);
        }
        else if (context.Result is ObjectResult objectResult)
        {
            // 如果是对象结果，则转换为json结果
            context.Result = new JsonResult(objectResult.Value);
        }
        else if (context.Result is ContentResult contentResult)
        {
            // 如果是内容结果，则转换为json结果
            context.Result = new JsonResult(contentResult.Content);
        }
        else if (context.Result is JsonResult jsonResult)
        {
            // 如果是json结果，则转换为json结果
            context.Result = new JsonResult(jsonResult.Value);
        }
        else
        {
            // 其他结果，则转换为json结果
            throw new Exception($"未经处理的Result类型：{context.Result.GetType().Name}");
        }
        // 请求构造函数和方法,调用下一个过滤器
        var resultExecuted = await next();
        // 执行结果
        // 获取控制器、路由信息
        var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        // 获取请求的方法
        var method = actionDescriptor!.MethodInfo;
        // 获取 HttpContext 和 HttpRequest 对象
        var httpContext = context.HttpContext;
        var httpRequest = httpContext.Request;
        // 获取客户端 Ip 地址
        var remoteIp = httpContext.Connection.RemoteIpAddress == null ? string.Empty : httpContext.Connection.RemoteIpAddress.ToString();
        // 获取请求的 Url 地址(域名、路径、参数)
        var requestUrl = httpRequest.Host.Value + httpRequest.Path + httpRequest.QueryString.Value;
        // 写入日志
        var info = $"\t 请求Ip：{remoteIp}\n" +
                   $"\t 请求地址：{requestUrl}\n" +
                   $"\t 请求方法：{method}";
        var result = JsonSerializer.Serialize(resultExecuted.Result);
        if (_resultLogSwitch)
        {
            _logger.Information($"返回数据\n{info}\n{result}");
        }
    }
}