#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:MiniProfilerSetup
// Guid:5b0b173b-f1bc-4274-a2ce-04b12a18f1bd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-07 上午 01:58:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using XiHan.Infrastructure.Apps.Setting;

namespace XiHan.Extensions.Setups.Services;

/// <summary>
/// MiniProfilerSetup
/// </summary>
public static class MiniProfilerSetup
{
    /// <summary>
    /// MiniProfiler 扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMiniProfilerSetup(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var isEnabledMiniprofiler = AppSettings.Miniprofiler.IsEnabled.Get();
        if (!isEnabledMiniprofiler) return services;
        services.AddMiniProfiler(options =>
        {
            options.RouteBasePath = @"/profiler";
            options.ColorScheme = StackExchange.Profiling.ColorScheme.Auto;
            options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.BottomLeft;
            options.PopupShowTimeWithChildren = true;
            options.PopupShowTrivial = true;
            options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();
        });
        return services;
    }
}