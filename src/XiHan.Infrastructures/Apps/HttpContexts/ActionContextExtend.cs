#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ActionContextExtend
// Guid:8c7b13d5-b33f-426b-8ee8-c3ac4f254416
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-06-15 上午 05:10:50
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;
using System.Security.Claims;
using XiHan.Infrastructures.Responses.Results;

namespace XiHan.Infrastructures.Apps.HttpContexts;

/// <summary>
/// 控制器上下文拓展
/// </summary>
public static class ActionContextExtend
{
    /// <summary>
    /// 获取模型验证出错字段
    /// </summary>
    /// <param name="modelState"></param>
    /// <returns></returns>
    public static CustomResult GetValidationErrors(this ModelStateDictionary modelState)
    {
        return CustomResult.UnprocessableEntity(modelState);
    }

    /// <summary>
    /// 获取控制器上下文信息
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static ActionContextInfo GetActionContextInfo(this ActionContext context)
    {
        // 获取控制器、路由信息
        var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        // 获取请求的方法
        var methodInfo = actionDescriptor?.MethodInfo;
        // 获取 HttpContext 和 HttpRequest 对象
        var httpContext = context.HttpContext;
        var httpRequest = httpContext.Request;
        // 获取客户端 Ip 地址
        var remoteIp = httpContext.Connection.RemoteIpAddress == null
            ? string.Empty
            : httpContext.Connection.RemoteIpAddress.ToString();
        // 获取请求的 Url 地址(域名、路径、参数)
        var requestUrl = httpRequest.Host.Value + httpRequest.Path + httpRequest.QueryString.Value;
        // 获取请求参数（写入日志，需序列化成字符串后存储），可以自由篡改
        //var parameters = context.ActionArguments;
        // 获取操作人（必须授权访问才有值）"UserId" 为你存储的 claims type，jwt 授权对应的是 payload 中存储的键名
        var userId = httpContext.User.FindFirstValue("UserId");

        var actionContextInfo = new ActionContextInfo
        {
            HttpRequest = httpRequest,
            MethodInfo = methodInfo,
            MethodReturnType = methodInfo?.ReturnType,
            ControllerType = actionDescriptor?.ControllerTypeInfo,
            RemoteIp = remoteIp,
            RequestUrl = requestUrl,
            UserId = userId
        };

        return actionContextInfo;
    }
}

/// <summary>
/// 控制器上下文信息类
/// </summary>
public class ActionContextInfo
{
    /// <summary>
    /// 请求
    /// </summary>
    public HttpRequest? HttpRequest { get; init; }

    /// <summary>
    /// 请求的方法名称
    /// </summary>
    public MethodInfo? MethodInfo { get; init; }

    /// <summary>
    /// Action 类型
    /// </summary>
    public Type? MethodReturnType { get; set; }

    /// <summary>
    /// 控制器类型
    /// </summary>
    public TypeInfo? ControllerType { get; init; }

    /// <summary>
    /// 请求的方法名称
    /// </summary>
    public string? RemoteIp { get; init; }

    /// <summary>
    /// 请求的 Url 地址(域名、路径、参数)
    /// </summary>
    public string? RequestUrl { get; init; }

    /// <summary>
    /// 请求参数
    /// </summary>
    public Dictionary<string, object?>? ActionArguments { get; set; }

    /// <summary>
    /// 操作人Id
    /// </summary>
    public string? UserId { get; init; }

    /// <summary>
    /// 操作人名称
    /// </summary>
    public string? UserName { get; init; }
}