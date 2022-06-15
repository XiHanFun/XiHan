// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomExceptionFilterAsyncAttribute
// Guid:0c556f22-3f97-4ea7-aa0c-78d8d5722cc4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-15 下午 11:13:23
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ZhaiFanhuaBlog.ViewModels.Response;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters
{
    /// <summary>
    /// 异步异常处理过滤器属性（一般用于捕捉异常）
    /// </summary>
    public class CustomExceptionFilterAsyncAttribute : Attribute, IAsyncExceptionFilter
    {
        // 日志组件
        private readonly ILogger<CustomExceptionFilterAsyncAttribute> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public CustomExceptionFilterAsyncAttribute(ILogger<CustomExceptionFilterAsyncAttribute> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 当异常发生时
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            Console.WriteLine("CustomExceptionFilterAsyncAttribute.OnExceptionAsync Before");
            // 异常是否被处理过，没有则在这里处理
            if (context.ExceptionHandled == false)
            {
                // 写入日志
                _logger.LogError(context.HttpContext.Request.Path, context.Exception);
                //// 判断是否Ajax请求，是就返回Json
                //if (this.IsAjaxRequest(context.HttpContext.Request))
                //{
                context.Result = new JsonResult(ResultResponse.BadRequest(context.Exception.Message));
                //}
            }
            // 标记异常已经处理过了
            context.ExceptionHandled = true;
            Console.WriteLine("CustomExceptionFilterAsyncAttribute.OnExceptionAsync After");
        }

        /// <summary>
        /// 判断是否Ajax请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool IsAjaxRequest(HttpRequest request)
        {
            string header = request.Headers["X-Request-With"];
            return "XMLHttpRequest".Equals(header);
        }
    }
}