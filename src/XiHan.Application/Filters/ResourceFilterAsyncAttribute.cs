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
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using System.Text.Json;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Apps.HttpContexts;

namespace XiHan.Application.Filters;

/// <summary>
/// 异步资源过滤器属性（一般用于缓存、阻止模型（值）绑定操作等）
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class ResourceFilterAsyncAttribute : Attribute, IAsyncResourceFilter
{
    // 日志开关
    private readonly bool _resourceLogSwitch = AppSettings.LogConfig.Resource.GetValue();

    private readonly ILogger _logger = Log.ForContext<ResourceFilterAsyncAttribute>();

    // 缓存时间
    private readonly int _syncTimeout = AppSettings.Cache.SyncTimeout.GetValue();

    private readonly IMemoryCache _memoryCache;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="memoryCache"></param>
    public ResourceFilterAsyncAttribute(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    /// <summary>
    /// 在某资源执行时
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        // 控制器信息
        var actionContextInfo = context.GetActionContextInfo();
        // 写入日志
        var info = $"\t 请求Ip：{actionContextInfo.RemoteIp}\n" +
            $"\t 请求地址：{actionContextInfo.RequestUrl}\n" +
            $"\t 请求方法：{actionContextInfo.MethodInfo}\n" +
            $"\t 操作用户：{actionContextInfo.UserId}";
        // 若存在此资源，直接返回缓存资源
        if (_memoryCache.TryGetValue(actionContextInfo.RequestUrl + actionContextInfo.MethodInfo, out var value))
        {
            // 请求构造函数和方法
            context.Result = value as ActionResult;
            if (_resourceLogSwitch)
                _logger.Information($"缓存数据\n{info}\n{context.Result}");
        }
        else
        {
            // 请求构造函数和方法,调用下一个过滤器
            var resourceExecuted = await next();
            // 执行结果，若不存在此资源，缓存请求后的资源（请求构造函数和方法）
            if (resourceExecuted.Result != null)
            {
                var result = resourceExecuted.Result as ActionResult;
                _memoryCache.Set(actionContextInfo.RequestUrl + actionContextInfo.MethodInfo, result,
                    TimeSpan.FromMinutes(_syncTimeout));
                if (_resourceLogSwitch) _logger.Information($"请求缓存\n{info}\n{JsonSerializer.Serialize(result)}");
            }
        }
    }
}