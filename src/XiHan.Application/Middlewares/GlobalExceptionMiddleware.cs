#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:GlobalExceptionMiddleware
// Guid:a43904c8-cd77-4c25-bcde-5262c3b263ed
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-06-30 下午 03:08:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using Serilog;
using System.Diagnostics;
using System.Security.Authentication;
using System.Text;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Apps.HttpContexts;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Exceptions;
using XiHan.Infrastructures.Infos;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Models.Syses;
using XiHan.Services.Syses.Operations;
using XiHan.Utils.Extensions;
using XiHan.Utils.Serializes;

namespace XiHan.Application.Middlewares;

/// <summary>
/// 全局异常中间件
/// </summary>
public class GlobalExceptionMiddleware
{
    // 日志开关
    private readonly bool _exceptionLogSwitch = AppSettings.LogConfig.Exception.GetValue();

    private readonly RequestDelegate _next;
    private readonly ISysOperationLogService _sysOperationLogService;
    private readonly ILogger _logger = Log.ForContext<GlobalExceptionMiddleware>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next"></param>
    /// <param name="sysOperationLogService"></param>
    public GlobalExceptionMiddleware(RequestDelegate next, ISysOperationLogService sysOperationLogService)
    {
        _next = next;
        _sysOperationLogService = sysOperationLogService;
    }

    /// <summary>
    /// 异步调用
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = new Stopwatch();

        try
        {
            stopwatch.Reset();
            await _next(context);
            stopwatch.Stop();
        }
        catch (Exception ex)
        {
            // 记录操作日志
            await HandleExceptionAsync(context, stopwatch.ElapsedMilliseconds, ex);

            // 处理异常
            var exceptionResult = ex switch
            {
                // 参数异常
                ArgumentException => CustomResult.UnprocessableEntity(),
                // 认证授权异常
                AuthenticationException => CustomResult.Unauthorized(),
                // 禁止访问异常
                UnauthorizedAccessException => CustomResult.Forbidden(),
                // 数据未找到异常
                FileNotFoundException => CustomResult.NotFound(),
                // 未实现异常
                NotImplementedException => CustomResult.NotImplemented(),
                // 自定义异常
                CustomException => CustomResult.BadRequest(ex.Message),
                // 异常默认返回服务器错误，不直接明文显示
                _ => CustomResult.InternalServerError(),
            };

            context.Response.ContentType = "text/json;charset=utf-8";
            context.Response.StatusCode = exceptionResult.Code.GetEnumValueByKey();
            await context.Response.WriteAsync(exceptionResult.SerializeToJson(), Encoding.UTF8);
        }
    }

    /// <summary>
    /// 处理异常
    /// </summary>
    /// <param name="context"></param>
    /// <param name="elapsedMilliseconds"></param>
    /// <param name="ex"></param>
    /// <returns></returns>
    private async Task HandleExceptionAsync(HttpContext context, long elapsedMilliseconds, Exception ex)
    {
        // 获取当前请求上下文信息
        var httpContextInfo = new HttpContextInfoHelper(context);
        var clientInfo = httpContextInfo.ClientInfo;
        var addressInfo = httpContextInfo.AddressInfo;
        var authInfo = httpContextInfo.AuthInfo;
        var requestParameters = await context.GetRequestParameters();

        // 记录日志
        var sysOperationLog = new SysOperationLog
        {
            IsAjaxRequest = clientInfo.IsAjaxRequest,
            RequestMethod = clientInfo.RequestMethod,
            RequestUrl = clientInfo.RequestUrl,
            Location = addressInfo.Country + " " + addressInfo.State + " " + addressInfo.PrefectureLevelCity,
            Referrer = clientInfo.Referer,
            Agent = clientInfo.Agent,
            Ip = clientInfo.RemoteIPv4,
            Status = false,
            ErrorMsg = ex.Message,
            ElapsedTime = elapsedMilliseconds,
        };

        var endpoint = context.GetEndpoint();
        if (endpoint != null)
        {
            var logAttribute = endpoint.Metadata.GetMetadata<AppLogAttribute>();
            if (logAttribute != null)
            {
                sysOperationLog.BusinessType = logAttribute.BusinessType.GetEnumValueByKey();
                sysOperationLog.Module = logAttribute.Module;
                sysOperationLog.RequestParameters = logAttribute.IsSaveRequestData ? requestParameters : string.Empty;
            }
        }

        await _sysOperationLogService.CreateOperationLog(sysOperationLog);

        if (_exceptionLogSwitch)
            _logger.Error(ex, ex.Message);
    }
}