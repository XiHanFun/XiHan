#region <<版权版本注释>>

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

namespace XiHan.Infrastructure.Apps.HttpContexts;

/// <summary>
/// 全局请求上下文管理器
/// </summary>
public static class AppHttpContextManager
{
    // 上下文访问器
    private static IHttpContextAccessor? _httpContextAccessor;

    /// <summary>
    /// 配置全局请求上下文信息
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public static void Configure(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 当前上下文
    /// </summary>
    public static HttpContext? Current => _httpContextAccessor?.HttpContext;
}