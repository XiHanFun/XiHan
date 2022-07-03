// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomSwaggerExtension
// Guid:3848533d-fd63-44ea-96a0-b7d9511eecd8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-25 下午 03:53:33
// ----------------------------------------------------------------

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using ZhaiFanhuaBlog.Utils.Info;
using ZhaiFanhuaBlog.WebApi.Common.Extensions.Swagger;

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.DependencyInjection;

/// <summary>
/// CustomSwaggerExtension
/// </summary>
public static class CustomSwaggerExtension
{
    /// <summary>
    /// Swagger服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration config)
    {
        // 用于最小API
        services.AddEndpointsApiExplorer();
        // 配置Swagger
        services.AddSwaggerGen(options =>
        {
            //生成多个文档显示
            SwaggerInfo.SwaggerInfos.ForEach(swaggerinfo =>
            {
                //添加文档介绍
                options.SwaggerDoc(swaggerinfo.UrlPrefix, new OpenApiInfo
                {
                    Version = $"The Current Version: Full-{swaggerinfo.OpenApiInfo!.Version}",
                    Title = swaggerinfo.OpenApiInfo!.Title,
                    Description = swaggerinfo.OpenApiInfo!.Description + $" Powered by {EnvironmentInfoHelper.FrameworkDescription} on {SystemInfoHelper.OperatingSystem}",
                    Contact = new OpenApiContact
                    {
                        Name = config.GetValue<string>("Configuration:Admin:Name"),
                        Email = config.GetValue<string>("Configuration:Admin:Email")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
            });
            // 文档中显示小绿锁
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "在下框中请求头中输入【Bearer token】进行身份验证",
                Name = "Authorization",
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            options.OperationFilter<AddResponseHeadersFilter>();
            options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            options.OperationFilter<SecurityRequirementsOperationFilter>();
            // 生成注释文件
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);
        });
        return services;
    }

    /// <summary>
    /// Swagger应用扩展
    /// </summary>
    /// <param name="app"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IConfiguration config)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            SwaggerInfo.SwaggerInfos.ForEach(swaggerinfo =>
            {
                //切换版本操作,参数一是使用的哪个json文件,参数二是个名字
                options.SwaggerEndpoint($"/swagger/{swaggerinfo.UrlPrefix}/swagger.json", swaggerinfo.OpenApiInfo!.Title);
                // API页面Title
                options.DocumentTitle = $"{swaggerinfo.GroupName}-{config.GetValue<string>("Configuration:Name")}接口文档";
            });
            // 模型的默认扩展深度，设置为 -1 完全隐藏模型
            options.DefaultModelsExpandDepth(-1);
            // API文档仅展开标记
            options.DocExpansion(DocExpansion.None);
            // API前缀设置为空
            options.RoutePrefix = string.Empty;
        });
        return app;
    }
}