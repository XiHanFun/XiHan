// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomResultFilterAsyncAttribute
// Guid:12d7985c-76a1-44b0-9a02-79c73caeac38
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-02-04 下午 07:34:46
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.Filters;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 异步结果过滤器属性(一般用于结果封装)
/// </summary>
public class CustomResultFilterAsyncAttribute : Attribute, IAsyncResultFilter
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        throw new NotImplementedException();
    }
}