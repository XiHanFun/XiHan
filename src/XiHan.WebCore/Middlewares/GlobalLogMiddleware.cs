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
public class GlobalLogMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly ILogger _logger = Log.ForContext<GlobalLogMiddleware>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next"></param>
    public GlobalLogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// 异步调用
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        Stopwatch stopwatch = new();
        bool status = true;

        try
        {
            // 记录访问日志
            await RecordLogVisit();

            stopwatch.Reset();
            await _next(context);
            stopwatch.Stop();
        }
        catch (Exception ex)
        {
            // 记录异常日志
            status = false;
            _logger.Error(ex, ex.Message);
            await RecordLogException(ex);

            // 处理异常
            ApiResult exceptionResult = ex switch
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
                _ => ApiResult.InternalServerError(),
            };
            context.Response.ContentType = "text/json;charset=utf-8";
            context.Response.StatusCode = exceptionResult.Code.GetEnumValueByKey();
            await context.Response.WriteAsync(exceptionResult.SerializeToJson(), Encoding.UTF8);
        }
        finally
        {
            // 记录操作日志
            await RecordLogOperation(stopwatch.ElapsedMilliseconds, status);
        }
    }

    /// <summary>
    /// 记录访问日志
    /// </summary>
    /// <returns></returns>
    private async Task RecordLogVisit()
    {
        // 获取当前请求上下文信息
        Infrastructures.Apps.HttpContexts.UserClientInfo clientInfo = App.ClientInfo;
        Infrastructures.Apps.HttpContexts.UserAddressInfo addressInfo = App.AddressInfo;
        Infrastructures.Apps.HttpContexts.UserAuthInfo authInfo = App.AuthInfo;
        Infrastructures.Apps.HttpContexts.UserActionInfo actionInfo = App.ActionInfo;

        SysLogVisit sysLogVisit = new()
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
        };
        ISysLogVisitService sysLogVisitService = App.GetRequiredService<ISysLogVisitService>();
        _ = await sysLogVisitService.CreateLogVisit(sysLogVisit);
    }

    /// <summary>
    /// 记录异常日志
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    private async Task RecordLogException(Exception ex)
    {
        // 获取当前请求上下文信息
        Infrastructures.Apps.HttpContexts.UserClientInfo clientInfo = App.ClientInfo;
        Infrastructures.Apps.HttpContexts.UserAddressInfo addressInfo = App.AddressInfo;
        Infrastructures.Apps.HttpContexts.UserAuthInfo authInfo = App.AuthInfo;
        Infrastructures.Apps.HttpContexts.UserActionInfo actionInfo = App.ActionInfo;
        StackFrame? stackFrame = new StackTrace(ex, true).GetFrame(0);
        System.Reflection.MethodBase? targetSite = ex.TargetSite;

        SysLogException sysLogException = new()
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

        ISysLogExceptionService sysLogExceptionService = App.GetRequiredService<ISysLogExceptionService>();
        _ = await sysLogExceptionService.CreateLogException(sysLogException);
        _logger.Error(ex, ex.Message);
    }

    /// <summary>
    /// 记录操作日志
    /// </summary>
    /// <param name="elapsed"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    private async Task RecordLogOperation(long elapsed, bool status)
    {
        // 获取当前请求上下文信息
        Infrastructures.Apps.HttpContexts.UserClientInfo clientInfo = App.ClientInfo;
        Infrastructures.Apps.HttpContexts.UserAddressInfo addressInfo = App.AddressInfo;
        Infrastructures.Apps.HttpContexts.UserAuthInfo authInfo = App.AuthInfo;
        Infrastructures.Apps.HttpContexts.UserActionInfo actionInfo = App.ActionInfo;

        SysLogOperation sysLogOperation = new()
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
            ElapsedTime = elapsed,
        };

        ISysLogOperationService sysLogOperationService = App.GetRequiredService<ISysLogOperationService>();
        await sysLogOperationService.CreateLogOperation(sysLogOperation);
    }
}