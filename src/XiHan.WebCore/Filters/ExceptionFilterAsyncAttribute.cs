#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ExceptionFilterAsyncAttribute
// Guid:0c556f22-3f97-4ea7-aa0c-78d8d5722cc4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-06-15 下午 11:13:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Security.Authentication;
using XiHan.Infrastructures.Apps.HttpContexts;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Utils.Exceptions;
using XiHan.WebCore.Middlewares;

namespace XiHan.WebCore.Filters;

/// <summary>
/// 异步异常处理过滤器属性(一般用于捕捉异常)
/// </summary>
/// <remarks>已弃用(2023-07-03)，推荐<see cref="GlobalLogMiddleware" />全局日志中间件</remarks>
[Obsolete("已弃用，推荐 GlobalExceptionMiddleware 全局日志中间件")]
[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
public class ExceptionFilterAsyncAttribute : Attribute, IAsyncExceptionFilter
{
    private static readonly ILogger Logger = Log.ForContext<ExceptionFilterAsyncAttribute>();

    /// <summary>
    /// 当异常发生时
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        // 异常是否被处理过，没有则在这里处理
        if (context.ExceptionHandled == false)
        {
            context.Result = context.Exception switch
            {
                // 参数异常
                ArgumentException => new JsonResult(ApiResult.UnprocessableEntity()),
                // 认证授权异常
                AuthenticationException => new JsonResult(ApiResult.Unauthorized()),
                // 禁止访问异常
                UnauthorizedAccessException => new JsonResult(ApiResult.Forbidden()),
                // 数据未找到异常
                FileNotFoundException => new JsonResult(ApiResult.NotFound()),
                // 未实现异常
                NotImplementedException => new JsonResult(ApiResult.NotImplemented()),
                // 自定义异常
                CustomException => new JsonResult(ApiResult.BadRequest(context.Exception.Message)),
                // 异常默认返回服务器错误，不直接明文显示
                _ => new JsonResult(ApiResult.InternalServerError())
            };

            // 控制器信息
            var actionContextInfo = context.GetActionContextInfo();
            // 写入日志
            var info = $"\t 请求Ip：{actionContextInfo.RemoteIp}\n" +
                $"\t 请求地址：{actionContextInfo.RequestUrl}\n" +
                $"\t 请求方法：{actionContextInfo.MethodInfo}\n" +
                $"\t 操作用户：{actionContextInfo.UserId}";
            Logger.Error(context.Exception, $"系统异常\n{info}");
        }

        // 标记异常已经处理过了
        context.ExceptionHandled = true;

        await Task.CompletedTask;
    }
}