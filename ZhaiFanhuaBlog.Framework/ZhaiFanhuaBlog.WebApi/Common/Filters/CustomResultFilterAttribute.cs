// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomResultFilterAttribute
// Guid:6fd6dc24-0725-4101-b170-5156bb9c9940
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-24 上午 12:52:43
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ZhaiFanhuaBlog.ViewModels.Response;
using ZhaiFanhuaBlog.ViewModels.Response.Enum;
using ZhaiFanhuaBlog.ViewModels.Response.Model;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 结果过滤器属性(一般用于结果封装)
/// </summary>
public class CustomResultFilterAttribute : Attribute, IResultFilter
{
    /// <summary>
    /// 在某结果之前执行
    /// </summary>
    /// <param name="context"></param>
    public void OnResultExecuting(ResultExecutingContext context)
    {
        Console.WriteLine("CustomResultFilterAttribute.OnResultExecuting");
        if (context.Result is MessageModel)
        {
            MessageModel messageModel = (MessageModel)context.Result;
            if (messageModel.Success)
            {
                context.Result = (IActionResult)ResponseResult.OK(messageModel.Data!);
            }
        }
    }

    /// <summary>
    ///在某结果之后执行
    /// </summary>
    /// <param name="context"></param>
    public void OnResultExecuted(ResultExecutedContext context)
    {
        Console.WriteLine("CustomResultFilterAttribute.OnResultExecuted");
    }
}