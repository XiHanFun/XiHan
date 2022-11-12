#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SwaggerHttpContextExtension
// Guid:d023a6c7-cf49-4725-932e-9baa3f7094e9
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-29 下午 06:15:10
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;

namespace ZhaiFanhuaBlog.Extensions.Common.Swagger;

/// <summary>
/// SwaggerHttpContextExtension
/// </summary>
public static class SwaggerHttpContextExtension
{
    /// <summary>
    /// 设置规范化文档自动登录
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="accessToken"></param>
    public static void SigninToSwagger(this HttpContext httpContext, string accessToken)
    {
        httpContext.Response.Headers["access-token"] = accessToken;
    }

    /// <summary>
    /// 设置规范化文档退出登录
    /// </summary>
    /// <param name="httpContext"></param>
    public static void SignoutToSwagger(this HttpContext httpContext)
    {
        httpContext.Response.Headers["access-token"] = "invalid_token";
    }
}