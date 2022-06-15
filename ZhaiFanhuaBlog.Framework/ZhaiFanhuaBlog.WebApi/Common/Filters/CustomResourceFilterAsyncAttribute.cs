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

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 异步资源过滤器属性（一般用于缓存）
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class CustomResourceFilterAsyncAttribute : Attribute, IAsyncResourceFilter
{
    // 内存缓存
    private readonly IMemoryCache _memoryCache;

    private readonly IConfiguration _config;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="memoryCache"></param>
    /// <param name="config"></param>
    public CustomResourceFilterAsyncAttribute(IMemoryCache memoryCache, IConfiguration config)
    {
        _memoryCache = memoryCache;
        _config = config;
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
        string para = context.HttpContext.Request.QueryString.Value ?? string.Empty;
        // 请求地址
        string url = (host + path + para).ToLower();
        // 若存在此资源，直接返回缓存资源
        if (_memoryCache.TryGetValue(url, out object value))
        {
            // 请求构造函数和方法
            context.Result = value as JsonResult;
        }
        else
        {
            // 请求构造函数和方法
            _ = await next.Invoke();
            // 若不存在此资源，缓存请求后的资源（请求构造函数和方法）
            _memoryCache.Set(url, context.Result, TimeSpan.FromDays(1));
        }
        Console.WriteLine("CustomResourceFilterAsyncAttribute.OnResourceExecutionAsync After");
    }
}