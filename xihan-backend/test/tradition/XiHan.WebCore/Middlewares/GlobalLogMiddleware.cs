#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:GlobalLogMiddleware
// Guid:a43904c8-cd77-4c25-bcde-5262c3b263ed
// Author:Administrator
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-30 下午 03:08:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
using System.Security.Authentication;
using System.Text;
using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Apps.HttpContexts;
using XiHan.Infrastructures.Responses;
using XiHan.Models.Syses;
using XiHan.Services.Syses.Logging;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;
using XiHan.Utils.Serializes;

namespace XiHan.WebCore.Middlewares;

/// <summary>
/// 全局日志中间件
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="next"></param>
public class GlobalLogMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    private static readonly ILogger Logger = Log.ForContext<GlobalLogMiddleware>();

    /// <summary>
    /// 异步调用
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        Stopwatch stopwatch = new();
        var status = true;

        try
        {
            // 记录访问日志
            await RecordVisitLog();

            stopwatch.Reset();
            await _next(context);
            stopwatch.Stop();
        }
        catch (Exception ex)
        {
            // 记录异常日志
            status = false;
            Logger.Error(ex, ex.Message);
            await RecordExceptionLog(ex);

            // 处理异常
            var exceptionResult = ex switch
            {
                // 参数异常
                ArgumentException => ApiResult.UnprocessableEntity(),
                // 认证授权异常
                AuthenticationException => ApiResult.Unauthorized(),
                // 禁止访问异常
                UnauthorizedAccessException => ApiResult.Forbidden(),
                // 数据未找到异常
                FileNotFoundException => ApiResult.NotFound(),
                // 未实现异常
                NotImplementedException => ApiResult.NotImplemented(),
                // 自定义异常
                CustomException => ApiResult.BadRequest(ex.Message),
                // 其他异常默认返回服务器错误，不直接明文显示
                _ => ApiResult.InternalServerError()
            };
            context.Response.ContentType = "text/json;charset=utf-8";
            context.Response.StatusCode = exceptionResult.Code.GetEnumValueByKey();
            await context.Response.WriteAsync(exceptionResult.SerializeTo(), Encoding.UTF8);
        }
        finally
        {
            // 记录操作日志
            await RecordOperationLog(stopwatch.ElapsedMilliseconds, status);
        }
    }

    /// <summary>
    /// 记录访问日志
    /// </summary>
    /// <returns></returns>
    private static async Task RecordVisitLog()
    {
        // 获取当前请求上下文信息
        var httpCurrent = App.HttpContextCurrent;

        var clientInfo = httpCurrent.GetClientInfo();
        var addressInfo = httpCurrent.GetAddressInfo();
        var authInfo = httpCurrent.GetAuthInfo();
        var actionInfo = await httpCurrent.GetActionInfo();

        SysVisitLog sysVisitLog = new()
        {
            // 访问信息
            IsAjaxRequest = clientInfo.IsAjaxRequest,
            Language = clientInfo.Language,
            Referrer = clientInfo.Referer,
            Agent = clientInfo.Agent,
            DeviceType = clientInfo.DeviceType,
            Os = clientInfo.OsName + clientInfo.OsVersion,
            Browser = clientInfo.BrowserName + clientInfo.BrowserVersion,
            Ip = addressInfo.RemoteIPv4,
            Location = addressInfo.Country + "|" + addressInfo.State + "|" + addressInfo.PrefectureLevelCity,
            RealName = authInfo.RealName,
            RequestMethod = actionInfo.RequestMethod,
            RequestUrl = actionInfo.RequestUrl
        };
        var sysVisitLogService = App.GetRequiredService<ISysVisitLogService>();
        _ = await sysVisitLogService.CreateVisitLog(sysVisitLog);
    }

    /// <summary>
    /// 记录异常日志
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    private static async Task RecordExceptionLog(Exception ex)
    {
        // 获取当前请求上下文信息
        var httpCurrent = App.HttpContextCurrent;

        var clientInfo = httpCurrent.GetClientInfo();
        var addressInfo = httpCurrent.GetAddressInfo();
        var authInfo = httpCurrent.GetAuthInfo();
        var actionInfo = await httpCurrent.GetActionInfo();
        var stackFrame = new StackTrace(ex, true).GetFrame(0);
        var targetSite = ex.TargetSite;

        SysExceptionLog sysExceptionLog = new()
        {
            // 访问信息
            IsAjaxRequest = clientInfo.IsAjaxRequest,
            Language = clientInfo.Language,
            Referrer = clientInfo.Referer,
            Agent = clientInfo.Agent,
            DeviceType = clientInfo.DeviceType,
            Os = clientInfo.OsName + clientInfo.OsVersion,
            Browser = clientInfo.BrowserName + clientInfo.BrowserVersion,
            Ip = addressInfo.RemoteIPv4,
            Location = addressInfo.Country + "|" + addressInfo.State + "|" + addressInfo.PrefectureLevelCity,
            RealName = authInfo.RealName,
            RequestMethod = actionInfo.RequestMethod,
            RequestUrl = actionInfo.RequestUrl,
            // 异常信息
            Level = LogEventLevel.Error.ToString(),
            Thread = Environment.CurrentManagedThreadId,
            FileName = stackFrame?.GetFileName(),
            LineNumber = stackFrame?.GetFileLineNumber() ?? 0,
            ClassName = stackFrame?.GetMethod()?.DeclaringType?.FullName?.Split('+')[0],
            Event = targetSite?.DeclaringType?.FullName,
            Message = ex.Message,
            StackTrace = ex.StackTrace
        };

        var sysExceptionLogService = App.GetRequiredService<ISysExceptionLogService>();
        _ = await sysExceptionLogService.CreateExceptionLog(sysExceptionLog);
        Logger.Error(ex, ex.Message);
    }

    /// <summary>
    /// 记录操作日志
    /// </summary>
    /// <param name="elapsed"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    private static async Task RecordOperationLog(long elapsed, bool status)
    {
        // 获取当前请求上下文信息
        var httpCurrent = App.HttpContextCurrent;

        var clientInfo = httpCurrent.GetClientInfo();
        var addressInfo = httpCurrent.GetAddressInfo();
        var authInfo = httpCurrent.GetAuthInfo();
        var actionInfo = await httpCurrent.GetActionInfo();

        SysOperationLog sysOperationLog = new()
        {
            // 访问信息
            IsAjaxRequest = clientInfo.IsAjaxRequest,
            Language = clientInfo.Language,
            Referrer = clientInfo.Referer,
            Agent = clientInfo.Agent,
            DeviceType = clientInfo.DeviceType,
            Os = clientInfo.OsName + clientInfo.OsVersion,
            Browser = clientInfo.BrowserName + clientInfo.BrowserVersion,
            Ip = addressInfo.RemoteIPv4,
            Location = addressInfo.Country + "|" + addressInfo.State + "|" + addressInfo.PrefectureLevelCity,
            RealName = authInfo.RealName,
            RequestMethod = actionInfo.RequestMethod,
            RequestUrl = actionInfo.RequestUrl,
            // 操作信息
            Module = actionInfo.Module,
            BusinessType = actionInfo.BusinessType,
            RequestParameters = actionInfo.RequestParameters,
            ResponseResult = actionInfo.ResponseResult,
            Status = status,
            ElapsedTime = elapsed
        };

        var sysOperationLogService = App.GetRequiredService<ISysOperationLogService>();
        await sysOperationLogService.CreateOperationLog(sysOperationLog);
    }
}