#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ExceptionFilterAsyncAttribute
// Guid:0c556f22-3f97-4ea7-aa0c-78d8d5722cc4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-15 下午 11:13:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Security.Authentication;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Apps.HttpContexts;
using XiHan.Infrastructures.Exceptions;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Models.Syses;
using XiHan.Services.Syses.Operations;
using XiHan.Services.Syses.Operations.Logic;

namespace XiHan.Application.Filters;

/// <summary>
/// 异步异常处理过滤器属性（一般用于捕捉异常）
/// </summary>
[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
public class ExceptionFilterAsyncAttribute : Attribute, IAsyncExceptionFilter
{
    // 日志开关
    private readonly bool _exceptionLogSwitch = AppSettings.LogConfig.Exception.GetValue();

    private readonly ILogger _logger = Log.ForContext<ExceptionFilterAsyncAttribute>();
    private readonly ISysOperationLogService _sysOperationLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysOperationLogService"></param>
    public ExceptionFilterAsyncAttribute(ISysOperationLogService sysOperationLogService)
    {
        _sysOperationLogService = sysOperationLogService;
    }

    /// <summary>
    /// 当异常发生时
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        // 异常默认返回服务器错误，不直接明文显示
        var result = new JsonResult(CustomResult.InternalServerError());

        // 异常是否被处理过，没有则在这里处理
        if (context.ExceptionHandled == false)
        {
            // 自定义异常
            if (context.Exception is CustomException)
                result = new JsonResult(CustomResult.BadRequest(context.Exception.Message));
            // 参数异常
            else if (context.Exception is ArgumentException)
                result = new JsonResult(CustomResult.UnprocessableEntity());
            // 认证授权异常
            else if (context.Exception is AuthenticationException)
                result = new JsonResult(CustomResult.Unauthorized());
            // 禁止访问异常
            else if (context.Exception is UnauthorizedAccessException)
                result = new JsonResult(CustomResult.Forbidden());
            // 数据未找到异常
            else if (context.Exception is FileNotFoundException)
                result = new JsonResult(CustomResult.NotFound());
            // 未实现异常
            else if (context.Exception is NotImplementedException)
                result = new JsonResult(CustomResult.NotImplemented());

            // 控制器信息
            var actionContextInfo = context.GetActionContextInfo();
            // 写入日志
            var info = $"\t 请求Ip：{actionContextInfo.RemoteIp}\n" +
                       $"\t 请求地址：{actionContextInfo.RequestUrl}\n" +
                       $"\t 请求方法：{actionContextInfo.MethodInfo}\n" +
                       $"\t 操作用户：{actionContextInfo.UserId}";

            if (_exceptionLogSwitch)
                _logger.Error(context.Exception, $"系统异常\n{info}");
        }

        // 标记异常已经处理过了
        context.ExceptionHandled = true;
        context.Result = result;

        await Task.CompletedTask;
    }
}