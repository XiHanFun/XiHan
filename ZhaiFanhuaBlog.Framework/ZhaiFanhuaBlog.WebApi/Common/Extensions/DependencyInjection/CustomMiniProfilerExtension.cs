// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomMiniProfilerExtension
// Guid:5b0b173b-f1bc-4274-a2ce-04b12a18f1bd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-07 上午 01:58:14
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Utils.Config;

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.DependencyInjection;

/// <summary>
/// CustomMiniProfilerExtension
/// </summary>
public static class CustomMiniProfilerExtension
{
    /// <summary>
    /// MiniProfiler扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomMiniProfiler(this IServiceCollection services)
    {
        if (ConfigHelper.Configuration.GetValue<bool>("MiniProfiler:IsEnabled"))
        {
            services.AddMiniProfiler(options => options.RouteBasePath = @"/profiler");
        }
        return services;
    }
}