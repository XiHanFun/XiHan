﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:AppHttpContextManager
// Guid:620d8a3e-e2ba-4e99-99ec-51fa46c521be
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-16 上午 04:49:10
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Reflection;

namespace XiHan.Infrastructures.Apps.HttpContexts;

/// <summary>
/// 全局请求上下文管理器
/// </summary>
public static class AppHttpContextManager
{
    private static Func<object>? _asyncLocalAccessor;
    private static Func<object, object>? _holderAccessor;
    private static Func<object, HttpContext>? _httpContextAccessor;

    /// <summary>
    /// 当前上下文
    /// </summary>
    public static HttpContext? Current => GetHttpContextCurrent();

    /// <summary>
    /// 获取当前 HttpContext 对象
    /// </summary>
    private static HttpContext? GetHttpContextCurrent()
    {
        var asyncLocal = (_asyncLocalAccessor ??= CreateAsyncLocalAccessor())();
        var holder = (_holderAccessor ??= CreateHolderAccessor(asyncLocal))(asyncLocal);
        return (_httpContextAccessor ??= CreateHttpContextAccessor(holder))(holder);

        // 创建异步本地访问器
        static Func<object> CreateAsyncLocalAccessor()
        {
            var fieldInfo =
                typeof(HttpContextAccessor).GetField("_httpContextCurrent",
                    BindingFlags.Static | BindingFlags.NonPublic)!;
            var field = Expression.Field(null, fieldInfo);
            return Expression.Lambda<Func<object>>(field).Compile();
        }

        // 创建常驻 HttpContext 访问器
        static Func<object, object> CreateHolderAccessor(object asyncLocal)
        {
            var holderType = asyncLocal.GetType().GetGenericArguments()[0];
            var method = typeof(AsyncLocal<>).MakeGenericType(holderType).GetProperty("Value")!.GetGetMethod()!;
            var target = Expression.Parameter(typeof(object));
            var convert = Expression.Convert(target, asyncLocal.GetType());
            var getValue = Expression.Call(convert, method);
            return Expression.Lambda<Func<object, object>>(getValue, target).Compile();
        }

        // 获取 HttpContext 访问器
        static Func<object, HttpContext> CreateHttpContextAccessor(object holder)
        {
            var target = Expression.Parameter(typeof(object));
            var convert = Expression.Convert(target, holder.GetType());
            var field = Expression.Field(convert, "Context");
            var convertAsResult = Expression.Convert(field, typeof(HttpContext));
            return Expression.Lambda<Func<object, HttpContext>>(convertAsResult, target).Compile();
        }
    }
}