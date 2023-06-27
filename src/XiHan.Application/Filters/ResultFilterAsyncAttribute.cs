#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ResultFilterAsyncAttribute
// Guid:0c941b38-e677-4251-a014-2e96fa572311
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-18 上午 01:48:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Text.Json;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Apps.HttpContexts;
using XiHan.Infrastructures.Responses.Results;

namespace XiHan.Application.Filters;

/// <summary>
/// 异步结果过滤器属性(一般用于返回统一模型数据)
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class ResultFilterAsyncAttribute : Attribute, IAsyncResultFilter
{
    // 日志开关
    private readonly bool _resultLogSwitch = AppSettings.LogConfig.Result.GetValue();

    private readonly ILogger _logger = Log.ForContext<ResultFilterAsyncAttribute>();

    /// <summary>
    /// 构造函数
    /// </summary>
    public ResultFilterAsyncAttribute()
    {
    }

    /// <summary>
    /// 在某结果执行时
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        context.Result = context.Result switch
        {
            // 不为空就做处理
            CustomResult customResult =>
                // 如果是 CustomResult，则转换为 JsonResult
                new JsonResult(customResult),
            ContentResult contentResult =>
                // 如果是 ContentResult，则转换为 JsonResult
                new JsonResult(contentResult.Content),
            ObjectResult objectResult =>
                // 如果是 ObjectResult，则转换为 JsonResult
                new JsonResult(objectResult.Value),
            JsonResult jsonResult =>
                // 如果是 JsonResult，则转换为 JsonResult
                new JsonResult(jsonResult.Value),
            _ => context.Result
        };
        // 请求构造函数和方法,调用下一个过滤器
        var resultExecuted = await next();

        // 控制器信息
        var actionContextInfo = context.GetActionContextInfo();
        // 写入日志
        var info = $"\t 请求Ip：{actionContextInfo.RemoteIp}\n" +
                   $"\t 请求地址：{actionContextInfo.RequestUrl}\n" +
                   $"\t 请求方法：{actionContextInfo.MethodInfo}\n" +
                   $"\t 操作用户：{actionContextInfo.UserId}";
        // 执行结果
        var result = JsonSerializer.Serialize(resultExecuted.Result);
        if (_resultLogSwitch) _logger.Information($"返回数据\n{info}\n{result}");
    }
}