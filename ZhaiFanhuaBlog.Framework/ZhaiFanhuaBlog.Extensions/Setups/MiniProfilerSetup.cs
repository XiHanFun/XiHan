// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:MiniProfilerSetup
// Guid:5b0b173b-f1bc-4274-a2ce-04b12a18f1bd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-07 上午 01:58:14
// ----------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using ZhaiFanhuaBlog.Core.AppSettings;

namespace ZhaiFanhuaBlog.Setups;

/// <summary>
/// MiniProfilerSetup
/// </summary>
public static class MiniProfilerSetup
{
    /// <summary>
    /// MiniProfiler扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMiniProfilerSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        if (AppSettings.Miniprofiler.IsEnabled)
        {
            services.AddMiniProfiler(options => options.RouteBasePath = @"/profiler");
        }
        return services;
    }
}