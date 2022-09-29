// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SwaggerMiddleware
// Guid:40c713e1-7cdf-42da-9e08-84d1bff1489f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-29 上午 01:03:07
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using ZhaiFanhuaBlog.Core.AppSettings;
using ZhaiFanhuaBlog.Extensions.Common.Swagger;

namespace ZhaiFanhuaBlog.Extensions.Middlewares;

/// <summary>
/// SwaggerMiddleware
/// </summary>
public static class SwaggerMiddleware
{
    /// <summary>
    /// Swagger应用扩展
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app, Func<Stream> streamHtml)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            // 根据分组遍历展示
            SwaggerInfo.SwaggerInfos.ForEach(swaggerinfo =>
            {
                //切换版本操作,参数一是使用的哪个json文件,参数二是个名字
                options.SwaggerEndpoint($"/swagger/{swaggerinfo.UrlPrefix}/swagger.json", swaggerinfo.OpenApiInfo?.Title);
            });
            // 性能分析
            if (AppConfig.Configuration.GetValue<bool>("MiniProfiler:IsEnabled"))
            {
                if (streamHtml.Invoke() == null)
                {
                    var errorMsg = "文件index.html的属性，必须设置为嵌入的资源";
                    Log.Error(errorMsg);
                    throw new Exception(errorMsg);
                }
                // 将swagger首页，设置成自定义的页面，写法：{ 项目名.index.html}
                options.IndexStream = streamHtml;
            }

            // API页面标题
            options.DocumentTitle = $"{AppConfig.Configuration.GetValue<string>("Configuration:Name")}接口文档";
            // API文档仅展开标记 List：列表式（展开子类），默认值;Full：完全展开;None：列表式（不展开子类）
            options.DocExpansion(DocExpansion.None);
            // 模型的默认扩展深度，设置为 -1 完全隐藏模型
            options.DefaultModelsExpandDepth(-1);
            // API前缀设置为空
            options.RoutePrefix = string.Empty;
        });
        return app;
    }
}