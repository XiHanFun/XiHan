// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomResultFilterAttribute
// Guid:6fd6dc24-0725-4101-b170-5156bb9c9940
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-24 上午 12:52:43
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.Filters;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 结果过滤器属性(一般用于结果封装)
/// </summary>
public class CustomResultFilterAttribute : Attribute, IResultFilter
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    public void OnResultExecuting(ResultExecutingContext context)
    {
        Console.WriteLine("CustomResultFilterAttribute.OnResultExecuting");
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    public void OnResultExecuted(ResultExecutedContext context)
    {
        Console.WriteLine("CustomResultFilterAttribute.OnResultExecuted");
    }
}