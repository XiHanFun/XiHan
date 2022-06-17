// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomResultFilterAsyncAttribute
// Guid:12d7985c-76a1-44b0-9a02-79c73caeac38
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-02-04 下午 07:34:46
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ZhaiFanhuaBlog.WebApi.Common.Response;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 异步结果过滤器属性(一般用于结果封装)
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class CustomResultFilterAsyncAttribute : Attribute, IAsyncResultFilter
{
    /// <summary>
    /// 在某结果执行时
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        Console.WriteLine("CustomActionFilterAsyncAttribute.OnActionExecutionAsync Before");
        // 不为空就处理
        if (context.Result != null)
        {
            if (context.Result is ObjectResult objectResult)
            {
                context.Result = new JsonResult(ResultResponse.OK(objectResult!.Value!));
            }
            else if (context.Result is ContentResult contentResult)
            {
                context.Result = new JsonResult(ResultResponse.OK(contentResult!.Content!));
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new JsonResult(ResultResponse.OK());
            }
            else
            {
                throw new Exception($"未经处理的Result类型：{context.Result.GetType().Name}");
            }
        }
        await next.Invoke();
        Console.WriteLine("CustomActionFilterAsyncAttribute.OnActionExecutionAsync After");
    }
}