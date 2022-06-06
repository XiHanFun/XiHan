// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomResourceFilterAttribute
// Guid:2c9385f8-1404-8eb2-3007-2952df30a92e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-05 上午 03:50:20
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 资源过滤器属性（一般用于缓存）
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class CustomResourceFilterAttribute : Attribute, IResourceFilter
{
    // 内存缓存
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="memoryCache"></param>
    public CustomResourceFilterAttribute(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    /// <summary>
    /// 在某资源之前执行
    /// </summary>
    /// <param name="context"></param>
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        Console.WriteLine("CustomResourceFilterAttribute.OnResourceExecuting");
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
            context.Result = value as ActionResult;
        }
    }

    /// <summary>
    /// 在某资源之后执行
    /// </summary>
    /// <param name="context"></param>
    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        // 请求域名
        string host = context.HttpContext.Request.Host.Value;
        // 请求路径
        string path = context.HttpContext.Request.Path;
        // 请求参数
        string para = context.HttpContext.Request.QueryString.Value ?? string.Empty;
        // 请求地址
        string url = (host + path + para).ToLower();
        // 若不存在此资源，缓存请求后的资源（请求构造函数和方法）
        _memoryCache.Set(url, context.Result, TimeSpan.FromDays(1));
        Console.WriteLine("CustomResourceFilterAttribute.OnResourceExecuted");
    }
}