#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:GlobalExceptionMiddleware
// Guid:f1883802-3d84-42fa-a485-04967479c44d
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-06-21 上午 09:06:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using Serilog;
using XiHan.Services.Syses.Operations;

namespace XiHan.Application.Middlewares;

/// <summary>
/// 全局异常处理中间件
/// </summary>
public class GlobalExceptionMiddleware
{
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
    /// 执行委托
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// 处理异常
    /// </summary>
    /// <param name="context"></param>
    /// <param name="ex"></param>
    /// <returns></returns>
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    { }
}