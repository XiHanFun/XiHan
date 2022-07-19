// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomResourceFilterAsyncAttribute
// Guid:3a91fd16-3f9f-956d-3bfa-56b4f252b06c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-05 上午 03:40:46
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 异步资源过滤器属性（一般用于缓存、阻止模型（值）绑定操作等）
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class CustomResourceFilterAsyncAttribute : Attribute, IAsyncResourceFilter
{
    private readonly IConfiguration _IConfiguration;
    private readonly ILogger<CustomResourceFilterAsyncAttribute> _ILogger;
    private readonly IMemoryCache _IMemoryCache;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="memoryCache"></param>
    /// <param name="config"></param>
    public CustomResourceFilterAsyncAttribute(IConfiguration config, ILogger<CustomResourceFilterAsyncAttribute> logger, IMemoryCache memoryCache)
    {
        _IConfiguration = config;
        _ILogger = logger;
        _IMemoryCache = memoryCache;
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
        // 获取 HttpContext 和 HttpRequest 对象
        var httpContext = context.HttpContext;
        var httpRequest = httpContext.Request;
        // 获取客户端 Ipv4 地址
        var remoteIPv4 = httpContext.Connection.RemoteIpAddress == null ? string.Empty : httpContext.Connection.RemoteIpAddress.ToString();
        // 获取请求的 Url 地址(协议、域名、路径、参数)
        var requestUrl = httpRequest.Protocol + httpRequest.Host.Value + httpRequest.Path + httpRequest.QueryString.Value ?? string.Empty;
        // 若存在此资源，直接返回缓存资源
        if (_IMemoryCache.TryGetValue(requestUrl, out object value))
        {
            // 请求构造函数和方法
            context.Result = value as ActionResult;
            _ILogger.LogInformation($"资源【{requestUrl}】已缓存结果【{context.Result}】");
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
                    TimeSpan SyncTimeout = TimeSpan.FromMinutes(_IConfiguration.GetValue<int>("Cache:SyncTimeout"));
                    var result = resourceExecuted.Result as ActionResult;
                    _IMemoryCache.Set(requestUrl, result, SyncTimeout);
                    _ILogger.LogInformation($"资源【{requestUrl}】开始缓存【{JsonConvert.SerializeObject(result)}】");
                    _ILogger.LogInformation($"请求结果为【{JsonConvert.SerializeObject(result)}】");
                }
            }
            catch (Exception)
            {
                throw new Exception("日志未获取到结果，返回的数据无法序列化;");
            }
        }
        Console.WriteLine("CustomResourceFilterAsyncAttribute.OnResourceExecutionAsync After");
    }
}