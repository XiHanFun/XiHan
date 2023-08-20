#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ActionFilterAsyncAttribute
// Guid:17255225-ef95-b047-1def-1fdb88733464
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-01-05 上午 03:49:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using XiHan.Infrastructures.Responses;
using XiHan.WebCore.Common.Actions;

namespace XiHan.WebCore.Filters;

/// <summary>
/// 异步请求过滤器属性(一般用于模型验证、记录日志、篡改参数、篡改返回值、统一参数验证、实现数据库事务自动开启关闭等)
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class ActionFilterAsyncAttribute : Attribute, IAsyncActionFilter
{
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
            // 获取模型验证出错字段
            var validationErrors = new ValidationDto(modelState);

            context.Result = new JsonResult(ApiResult.UnprocessableEntity(validationErrors));
        }
        else
        {
            // 请求构造函数和方法,调用下一个过滤器
            var actionExecuted = await next();
            // 判断是否请求成功，没有异常就是请求成功
            var requestException = actionExecuted.Exception;
            if (requestException != null)
            {
                await Task.CompletedTask;
            }
        }
    }
}