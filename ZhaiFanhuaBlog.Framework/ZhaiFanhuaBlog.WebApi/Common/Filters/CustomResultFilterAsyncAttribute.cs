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
using ZhaiFanhuaBlog.ViewModels.Response;
using ZhaiFanhuaBlog.ViewModels.Response.Model;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 异步结果过滤器属性(一般用于结果封装)
/// </summary>
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
        if (context.Result != null)
        {
            context.Result = (IActionResult)ResponseResult.OK((JsonResult)context.Result);
        }
        await next.Invoke();
        Console.WriteLine("CustomActionFilterAsyncAttribute.OnActionExecutionAsync After");
    }
}