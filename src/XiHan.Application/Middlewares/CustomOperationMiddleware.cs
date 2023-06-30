#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:CustomOperationMiddleware
// Guid:a43904c8-cd77-4c25-bcde-5262c3b263ed
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-06-30 下午 03:08:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Infos;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Models.Syses;
using XiHan.Services.Syses.Operations;
using XiHan.Utils.Extensions;
using XiHan.Utils.Serializes;

namespace XiHan.Application.Middlewares;

/// <summary>
/// 自定义操作中间件
/// </summary>
public class CustomOperationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ISysOperationLogService _sysOperationLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next"></param>
    /// <param name="sysOperationLogService"></param>
    public CustomOperationMiddleware(RequestDelegate next, ISysOperationLogService sysOperationLogService)
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
        // 获取当前请求上下文信息
        var httpContextInfo = new HttpContextInfoHelper(context);
        var clientInfo = httpContextInfo.ClientInfo;
        var addressInfo = httpContextInfo.AddressInfo;
        var authInfo = httpContextInfo.AuthInfo;

        // 记录日志
        var sysOperationLog = new SysOperationLog()
        {
            IsAjaxRequest = clientInfo.IsAjaxRequest,
            RequestMethod = clientInfo.RequestMethod,
            RequestUrl = clientInfo.RequestUrl,
            Location = addressInfo.Country + " " + addressInfo.State + " " + addressInfo.PrefectureLevelCity,
            Referrer = clientInfo.Referer,
            Agent = clientInfo.Agent,
            Ip = clientInfo.RemoteIPv4,
            Status = true,
        };

        try
        {
            await _next(context);

            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var logAttribute = endpoint.Metadata.GetMetadata<AppLogAttribute>();
                if (logAttribute != null)
                {
                    sysOperationLog.BusinessType = logAttribute.BusinessType.GetEnumValueByKey();
                    sysOperationLog.Module = logAttribute.Module;
                    sysOperationLog.QueryString = logAttribute.IsSaveRequestData ? sysOperationLog.QueryString : string.Empty;
                    sysOperationLog.ResponseResult = logAttribute.IsSaveResponseData ? sysOperationLog.ResponseResult : string.Empty;
                }
            }
        }
        catch (Exception exception)
        {
            sysOperationLog.Status = false;
            sysOperationLog.ErrorMsg = exception.Message;
        }
        finally
        {
            await _sysOperationLogService.CreateOperationLog(sysOperationLog);
        }
    }
}