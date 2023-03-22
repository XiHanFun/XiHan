#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SwaggerSetup
// Guid:40c713e1-7cdf-42da-9e08-84d1bff1489f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-29 上午 01:03:07
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using XiHan.Extensions.Common.Swagger;
using XiHan.Infrastructure.Apps.Setting;

namespace XiHan.Extensions.Setups.Application;

/// <summary>
/// SwaggerSetup
/// </summary>
public static class SwaggerSetup
{
    /// <summary>
    /// Swagger应用扩展
    /// </summary>
    /// <param name="app"></param>
    /// <param name="streamHtml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="Exception"></exception>
    public static IApplicationBuilder UseSwaggerSetup(this IApplicationBuilder app, Func<Stream> streamHtml)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        try
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // 站点名称
                var siteName = AppSettings.Syses.Name.Get();
                // 路由前缀
                var routePrefix = AppSettings.Swagger.RoutePrefix.Get();
                // 性能分析开关
                var isEnabledMiniprofiler = AppSettings.Miniprofiler.IsEnabled.Get();
                // 需要暴露的分组
                var publishGroup = AppSettings.Swagger.PublishGroup.GetSection();

                // 根据分组遍历展示
                typeof(ApiGroupNames).GetFields().Skip(1).ToList().ForEach(group =>
                {
                    // 获取枚举值上的特性
                    if (publishGroup.Any(pgroup => pgroup.ToLower() == group.Name.ToLower()))
                    {
                        var info = group.GetCustomAttributes(typeof(GroupInfoAttribute), false).OfType<GroupInfoAttribute>().FirstOrDefault();
                        // 切换分组操作,参数一是使用的哪个json文件,参数二是个名字
                        options.SwaggerEndpoint($"/swagger/{group.Name}/swagger.json", info?.Title);
                    }
                });

                // 性能分析
                if (isEnabledMiniprofiler)
                {
                    if (streamHtml.Invoke() == null)
                    {
                        var errorMsg = "文件index.html的属性，必须设置为嵌入的资源";

                        throw new Exception(errorMsg);
                    }
                    // 将swagger首页，设置成自定义的页面，写法：{ 项目名.index.html}
                    options.IndexStream = streamHtml;
                    options.HeadContent = @"<style>.opblock-summary-description{font-weight: bold;text-align: right;}</style>";
                }

                // API页面标题
                options.DocumentTitle = $"{siteName} - 接口文档";
                // API文档仅展开标记 List：列表式（展开子类），默认值;Full：完全展开;None：列表式（不展开子类）
                options.DocExpansion(DocExpansion.None);
                // 模型的默认扩展深度，设置为 -1 完全隐藏模型
                options.DefaultModelsExpandDepth(-1);
                // API前缀设置为空
                options.RoutePrefix = routePrefix;
            });
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
        }

        return app;
    }
}