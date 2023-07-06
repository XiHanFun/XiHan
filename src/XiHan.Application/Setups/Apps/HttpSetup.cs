#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:HttpSetup
// Guid:720163fd-1e5b-4ae9-aa30-16727fca3fe3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-11 下午 04:06:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting;

namespace XiHan.Application.Setups.Apps;

/// <summary>
/// HttpSetup
/// </summary>
public static class HttpSetup
{
    /// <summary>
    /// Http 应用扩展
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IApplicationBuilder UseHttpSetup(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        // 环境变量，开发环境
        if (env.IsDevelopment())
        {
            // 生成异常页面
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // 使用HSTS的中间件，该中间件添加了严格传输安全头
            app.UseHsts();
        }

        // Nginx 反向代理获取真实IP
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
        // 强制https跳转
        app.UseHttpsRedirection();

        return app;
    }
}