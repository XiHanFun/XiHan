// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomResourceFilterAsyncAttribute
// Guid:3a91fd16-3f9f-956d-3bfa-56b4f252b06c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-05 上午 03:40:46
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Security.Claims;
using ZhaiFanhuaBlog.Utils.Config;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 异步资源过滤器属性（一般用于缓存、阻止模型（值）绑定操作等）
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class CustomResourceFilterAsyncAttribute : Attribute, IAsyncResourceFilter
{
    // 日志开关
    private readonly bool ResourceSwitch = ConfigHelper.Configuration.GetValue<bool>("Logging:Switch:Resource");

    private readonly IMemoryCache _IMemoryCache;
    private readonly ILogger<CustomResourceFilterAsyncAttribute> _ILogger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="memoryCache"></param>
    public CustomResourceFilterAsyncAttribute(IMemoryCache memoryCache, ILogger<CustomResourceFilterAsyncAttribute> logger)
    {
        _IMemoryCache = memoryCache;
        _ILogger = logger;
    }

    /// <summary>
    /// 在某资源执行时
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        Console.WriteLine("CustomResourceFilterAsyncAttribute.OnResourceExecutionAsync Before");
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
        var requestUrl = httpRequest.Host.Value + httpRequest.Path + httpRequest.QueryString.Value ?? string.Empty;
        // 获取操作人（必须授权访问才有值）"userId" 为你存储的 claims type，jwt 授权对应的是 payload 中存储的键名
        var userId = httpContext.User?.FindFirstValue("UserId");
        // 请求时间
        var requestedTime = DateTimeOffset.Now;
        // 写入日志
        string info = $"\n" +
                $"\t 【请求IP】：{remoteIp}\n" +
                $"\t 【请求地址】：{requestUrl}\n" +
                $"\t 【请求方法】：{method}\n" +
                $"\t 【请求时间】：{requestedTime}\n" +
                $"\t 【操作人】：{userId}\n";
        // 若存在此资源，直接返回缓存资源
        if (_IMemoryCache.TryGetValue(requestUrl, out object value))
        {
            // 请求构造函数和方法
            context.Result = value as ActionResult;
            if (ResourceSwitch)
                _ILogger.LogInformation($"取出缓存" + info, context.Result);
        }
        else
        {
            // 请求构造函数和方法,调用下一个过滤器
            ResourceExecutedContext resourceExecuted = await next();
            // 执行结果
            try
            {
                // 若不存在此资源，缓存请求后的资源（请求构造函数和方法）
                if (resourceExecuted.Result != null)
                {
                    TimeSpan SyncTimeout = TimeSpan.FromMinutes(ConfigHelper.Configuration.GetValue<int>("Cache:SyncTimeout"));
                    var result = resourceExecuted.Result as ActionResult;
                    _IMemoryCache.Set(requestUrl, result, SyncTimeout);
                    if (ResourceSwitch)
                        _ILogger.LogInformation($"开始缓存", JsonConvert.SerializeObject(result));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        Console.WriteLine("CustomResourceFilterAsyncAttribute.OnResourceExecutionAsync After");
    }
}