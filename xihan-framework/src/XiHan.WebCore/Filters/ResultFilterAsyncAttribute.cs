#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ResultFilterAsyncAttribute
// Guid:0c941b38-e677-4251-a014-2e96fa572311
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-06-18 上午 01:48:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using XiHan.Infrastructures.Apps.HttpContexts;
using XiHan.Infrastructures.Responses;

namespace XiHan.WebCore.Filters;

/// <summary>
/// 异步结果过滤器属性(一般用于返回统一模型数据)
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class ResultFilterAsyncAttribute : Attribute, IAsyncResultFilter
{
    /// <summary>
    /// 是否忽略
    /// </summary>
    public bool IsIgnore { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public ResultFilterAsyncAttribute(bool isIgnore = false)
    {
        IsIgnore = isIgnore;
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
        if (IsIgnore || context.Result == null || context.HttpContext.IsDownloadFile()) return;

        // 结果不为空就做序列化处理
        context.Result = context.Result switch
        {
            // 如果是 CustomResult，则转换为 JsonResult
            ApiResult customResult => new JsonResult(customResult),
            // 如果是 ContentResult，则转换为 JsonResult
            ContentResult contentResult => new JsonResult(contentResult.Content),
            // 如果是 ObjectResult，则转换为 JsonResult
            ObjectResult objectResult => new JsonResult(objectResult.Value),
            // 如果是 JsonResult，则转换为 JsonResult
            JsonResult jsonResult => new JsonResult(jsonResult.Value),
            // 如果是 EmptyResult，则转换为 JsonResult
            EmptyResult => new JsonResult(null),
            // 其他直接返回
            _ => context.Result
        };
        // 请求构造函数和方法,调用下一个过滤器
        var resultExecuted = await next();
        // 执行结果
        _ = JsonSerializer.Serialize(resultExecuted.Result);

        await Task.CompletedTask;
    }
}