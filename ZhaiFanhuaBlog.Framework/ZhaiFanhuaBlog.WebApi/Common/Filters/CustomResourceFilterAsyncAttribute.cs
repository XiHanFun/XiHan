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
/// 异步资源过滤器属性（一般用于缓存）
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class CustomResourceFilterAsyncAttribute : Attribute, IAsyncResourceFilter
{
    private readonly ILogger<CustomResourceFilterAsyncAttribute> _ILogger;
    private readonly IMemoryCache _IMemoryCache;
    private readonly IConfiguration _IConfiguration;

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
        // 请求域名
        string host = context.HttpContext.Request.Host.Value;
        // 请求路径
        string path = context.HttpContext.Request.Path;
        // 请求参数
        string queryString = context.HttpContext.Request.QueryString.Value ?? string.Empty;
        // 若存在此资源，直接返回缓存资源
        if (_IMemoryCache.TryGetValue(host + path + queryString, out object value))
        {
            // 请求构造函数和方法
            context.Result = value as ActionResult;
            _ILogger.LogInformation($"资源【{host + path + queryString}】已缓存结果【{context.Result}】");
        }
        else
        {
            // 调用下一个过滤器
            ResourceExecutedContext resourceExecuted = await next();
            // 若不存在此资源，缓存请求后的资源（请求构造函数和方法）
            TimeSpan SyncTimeout = TimeSpan.FromSeconds(_IConfiguration.GetValue<int>("Cache:SyncTimeout"));
            _IMemoryCache.Set(host + path + queryString, resourceExecuted.Result as ActionResult, SyncTimeout);
            _ILogger.LogInformation($"资源【{host + path + queryString}】开始缓存【{resourceExecuted.Result}】");
        }
        Console.WriteLine("CustomResourceFilterAsyncAttribute.OnResourceExecutionAsync After");
    }
}