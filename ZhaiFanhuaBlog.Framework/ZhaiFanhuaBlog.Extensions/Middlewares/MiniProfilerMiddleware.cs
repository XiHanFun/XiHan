// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:MiniProfilerMiddleware
// Guid:895a4bbf-54a4-47ca-98d9-78c59cc6b91b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-29 上午 01:49:21
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using ZhaiFanhuaBlog.Core.AppSettings;

namespace ZhaiFanhuaBlog.Extensions.Middlewares;

/// <summary>
/// MiniProfiler性能分析
/// </summary>
public static class MiniProfilerMiddleware
{
    public static IApplicationBuilder UseMiniProfilerMiddleware(this IApplicationBuilder app)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        if (AppSettings.Miniprofiler.IsEnabled)
        {
            // 性能分析
            app.UseMiniProfiler();
        }
        return app;
    }
}