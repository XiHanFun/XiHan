#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ResourceFilterAsyncAttribute
// Guid:3a91fd16-3f9f-956d-3bfa-56b4f252b06c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-01-05 上午 03:40:46
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using XiHanCore.Infrastructures.Apps;
using XiHanCore.Infrastructures.Apps.Caches;

namespace XiHanCore.WebCore.Filters;

/// <summary>
/// 异步资源过滤器属性(一般用于缓存、阻止模型(值)绑定操作等)
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class ResourceFilterAsyncAttribute(int syncTimeout = 30) : Attribute, IAsyncResourceFilter
{
    // 缓存时间
    private readonly int _syncTimeout = syncTimeout;

    /// <summary>
    /// 在某资源执行时
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        var appCacheService = App.GetRequiredService<IAppCacheService>();

        var apiResourceCacheKey = GetCacheKey(context.HttpContext);

        // 若存在此资源，直接返回缓存资源
        if (appCacheService.Exists(apiResourceCacheKey))
        {
            context.Result = appCacheService.Get(apiResourceCacheKey) as ActionResult;
        }
        else
        {
            // 请求构造函数和方法,调用下一个过滤器
            var resourceExecuted = await next();
            // 执行结果，若不存在此资源，缓存请求后的资源(请求构造函数和方法)
            if (resourceExecuted.Result != null)
            {
                var result = resourceExecuted.Result as ActionResult;
                _ = appCacheService.SetWithMinutes(apiResourceCacheKey, result!, _syncTimeout);
            }
        }
    }

    /// <summary>
    /// 获取每个唯一请求的缓存 key
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private static string GetCacheKey(HttpContext context)
    {
        var requestId = context.TraceIdentifier.Split(':')[0];
        return $"{requestId}_{context.Request.Path.ToString().ToLowerInvariant()}";
    }
}