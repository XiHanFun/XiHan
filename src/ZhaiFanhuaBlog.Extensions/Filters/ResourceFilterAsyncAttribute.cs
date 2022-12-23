#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ResourceFilterAsyncAttribute
// Guid:3a91fd16-3f9f-956d-3bfa-56b4f252b06c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-05 上午 03:40:46
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text.Json;
using ZhaiFanhuaBlog.Infrastructure.Apps.Setting;

namespace ZhaiFanhuaBlog.Extensions.Filters;

/// <summary>
/// 异步资源过滤器属性（一般用于缓存、阻止模型（值）绑定操作等）
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class ResourceFilterAsyncAttribute : Attribute, IAsyncResourceFilter
{
    // 日志开关
    private readonly bool ResourceLogSwitch = AppSettings.Logging.Resource.Get();

    private readonly IMemoryCache _IMemoryCache;
    private readonly ILogger<ResourceFilterAsyncAttribute> _ILogger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iMemoryCache"></param>
    /// <param name="iLogger"></param>
    public ResourceFilterAsyncAttribute(IMemoryCache iMemoryCache, ILogger<ResourceFilterAsyncAttribute> iLogger)
    {
        _IMemoryCache = iMemoryCache;
        _ILogger = iLogger;
    }

    /// <summary>
    /// 在某资源执行时
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
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
        // 写入日志
        var info = $"\t 请求Ip：{remoteIp}\n" +
                   $"\t 请求地址：{requestUrl}\n" +
                   $"\t 请求方法：{method}\n" +
                   $"\t 操作用户：{userId}";
        // 若存在此资源，直接返回缓存资源
        if (_IMemoryCache.TryGetValue(requestUrl + method, out var value))
        {
            // 请求构造函数和方法
            context.Result = value as ActionResult;
            if (ResourceLogSwitch)
                _ILogger.LogInformation($"缓存数据\n{info}\n{context.Result}");
        }
        else
        {
            // 请求构造函数和方法,调用下一个过滤器
            var resourceExecuted = await next();
            // 执行结果
            try
            {
                // 若不存在此资源，缓存请求后的资源（请求构造函数和方法）
                if (resourceExecuted.Result != null)
                {
                    var SyncTimeout = TimeSpan.FromMinutes(AppSettings.Cache.SyncTimeout.Get());
                    var result = resourceExecuted.Result as ActionResult;
                    _IMemoryCache.Set(requestUrl + method, result, SyncTimeout);
                    if (ResourceLogSwitch)
                    {
                        _ILogger.LogInformation($"请求缓存\n{info}\n{JsonSerializer.Serialize(result)}");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}