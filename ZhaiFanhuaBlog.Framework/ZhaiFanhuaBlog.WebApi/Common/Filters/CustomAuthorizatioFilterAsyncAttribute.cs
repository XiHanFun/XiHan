// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomAuthorizatioFilterAsyncAttribute
// Guid:40387d18-5714-4ff2-96aa-164a967419fb
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-07-19 下午 02:47:58
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ZhaiFanhuaBlog.Models.Response;
using ZhaiFanhuaBlog.Models.Users;

namespace ZhaiFanhuaBlog.WebApi.Common.Filters;

/// <summary>
/// 异步授权过滤器属性（一般用于验证授权）
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class CustomAuthorizatioFilterAsyncAttribute : Attribute, IAsyncAuthorizationFilter
{
    /// <summary>
    /// 在某授权时执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        Console.WriteLine("CustomAuthorizatioFilterAsyncAttribute.OnAuthorizationAsync Before");
        // 获取 HttpContext 对象
        var httpContext = context.HttpContext;
        var httpResponse = httpContext.Response;
        // 获取控制器信息
        var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        // 获取控制器类型
        var controllerType = actionDescriptor!.ControllerTypeInfo;
        // 获取 Action 类型
        var methodType = actionDescriptor.MethodInfo;
        // 是否匿名访问
        var allowAnonymouse = context.Filters.Any(filter => filter is IAllowAnonymousFilter)
                            || controllerType.IsDefined(typeof(AllowAnonymousAttribute), true)
                            || methodType.IsDefined(typeof(AllowAnonymousAttribute), true);
        // 不是匿名才处理权限检查
        if (!allowAnonymouse)
        {
            var Identities = httpContext.User.Identities;
            // 验证权限
            if (Identities == null)
            {
                // 返回未授权
                context.Result = new JsonResult(ResultResponse.Unauthorized());
            }
        }
        // 否则直接跳过处理
        else await Task.CompletedTask;
        Console.WriteLine("CustomAuthorizatioFilterAsyncAttribute.OnAuthorizationAsync After");
    }
}