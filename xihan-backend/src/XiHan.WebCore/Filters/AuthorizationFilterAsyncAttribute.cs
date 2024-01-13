#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AuthorizationFilterAsyncAttribute
// Guid:40387d18-5714-4ff2-96aa-164a967419fb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-07-19 下午 02:47:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Authentication;

namespace XiHan.WebCore.Filters;

/// <summary>
/// 异步授权过滤器属性(一般用于验证授权)
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class AuthorizationFilterAsyncAttribute : Attribute, IAsyncAuthorizationFilter
{
    /// <summary>
    /// 在某授权时执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        // 控制器信息
        if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
        {
            // 是否授权访问
            var isAuthorize = context.Filters.Any(filter => filter is IAuthorizationFilter)
                              || actionDescriptor.ControllerTypeInfo.IsDefined(typeof(AuthorizeAttribute), true)
                              || actionDescriptor.MethodInfo.IsDefined(typeof(AuthorizeAttribute), true);
            // 授权访问就进行权限检查
            if (isAuthorize)
            {
                var identities = context.HttpContext.User.Identities;
                // 验证权限
                if (identities == null)
                {
                    // 认证参数异常
                    throw new AuthenticationException();
                }
                else
                {
                }
            }
            // 匿名访问直接跳过处理
            else
            {
                await Task.CompletedTask;
            }
        }
    }
}