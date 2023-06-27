#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ActionFilterAsyncAttribute
// Guid:17255225-ef95-b047-1def-1fdb88733464
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-05 上午 03:49:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Text.Json;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Apps.HttpContexts;

namespace XiHan.Application.Filters;

/// <summary>
/// 异步请求过滤器属性(一般用于模型验证、记录日志、篡改参数、篡改返回值、统一参数验证、实现数据库事务自动开启关闭等)
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class ActionFilterAsyncAttribute : Attribute, IAsyncActionFilter
{
    // 日志开关
    private readonly bool _actionLogSwitch = AppSettings.LogConfig.Action.GetValue();

    private readonly ILogger _logger = Log.ForContext<ActionFilterAsyncAttribute>();
    
    /// <summary>
    /// 构造函数
    /// </summary>
    public ActionFilterAsyncAttribute()
    {
    }

    /// <summary>
    /// 在某请求时执行
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 模型验证
        var modelState = context.ModelState;
        if (!modelState.IsValid)
        {
            context.Result = new JsonResult(modelState.GetValidationErrors());
        }
        else
        {
            // 控制器信息
            var actionContextInfo = context.GetActionContextInfo();
            // 写入日志
            var info = $"\t 请求Ip：{actionContextInfo.RemoteIp}\n" +
                       $"\t 请求地址：{actionContextInfo.RequestUrl}\n" +
                       $"\t 请求方法：{actionContextInfo.MethodInfo}\n" +
                       $"\t 操作用户：{actionContextInfo.UserId}";
            if (_actionLogSwitch)
                _logger.Information($"发起请求\n{info}");
            // 请求构造函数和方法,调用下一个过滤器
            var actionExecuted = await next();
            if (actionExecuted.Result != null)
            {
                // 获取返回的结果
                var returnResult = actionExecuted.Result as ActionResult;
                // 判断是否请求成功，没有异常就是请求成功
                var requestException = actionExecuted.Exception;
                if (requestException != null)
                {
                    // 请求成功就写入日志
                    if (_actionLogSwitch)
                        _logger.Information($"请求结果\n{info}\n {JsonSerializer.Serialize(returnResult)}");
                }
                else
                {
                }
            }
        }
    }
}