#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:MiniProfilerSetup
// Guid:5b0b173b-f1bc-4274-a2ce-04b12a18f1bd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-09-07 上午 01:58:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using XiHan.Infrastructures.Apps.Configs;

namespace XiHan.WebCore.Setups.Services;

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
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddMiniProfilerSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        var isEnabledMiniprofiler = AppSettings.Miniprofiler.IsEnabled.GetValue();
        if (isEnabledMiniprofiler)
            services.AddMiniProfiler(options =>
            {
                // 指定 MiniProfiler 的路由基础路径
                options.RouteBasePath = @"/Profiler";
                // 指定 MiniProfiler 的颜色方案
                options.ColorScheme = StackExchange.Profiling.ColorScheme.Auto;
                // 指定 MiniProfiler 弹出窗口的位置
                options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.BottomLeft;
                // 指定是否在 MiniProfiler 弹出窗口中显示子操作的执行时间
                options.PopupShowTimeWithChildren = true;
                // 指定是否在 MiniProfiler 弹出窗口中显示执行时间很短的操作
                options.PopupShowTrivial = true;
                // 指定 SQL 查询语句格式化器
                options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();
                // 控制是否跟踪数据库连接的打开和关闭操作
                options.TrackConnectionOpenClose = true;
            });

        return services;
    }
}