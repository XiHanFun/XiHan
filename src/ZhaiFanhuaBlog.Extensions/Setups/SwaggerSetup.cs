#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SwaggerSetup
// Guid:3848533d-fd63-44ea-96a0-b7d9511eecd8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-25 下午 03:53:33
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using ZhaiFanhuaBlog.Core.AppSettings;
using ZhaiFanhuaBlog.Extensions.Common.Swagger;
using ZhaiFanhuaBlog.Utils.Info;

namespace ZhaiFanhuaBlog.Setups;

/// <summary>
/// SwaggerSetup
/// </summary>
public static class SwaggerSetup
{
    /// <summary>
    /// Swagger服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // 用于最小API Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        // 配置Swagger,从路由、控制器和模型构建对象
        services.AddSwaggerGen(options =>
        {
            string version = AppSettings.Swagger.Version;
            //生成多个文档显示
            SwaggerInfo.SwaggerInfos.ForEach(swaggerinfo =>
            {
                //添加文档介绍
                options.SwaggerDoc(swaggerinfo.UrlPrefix, new OpenApiInfo
                {
                    Version = version,
                    Title = swaggerinfo.OpenApiInfo?.Title,
                    Description = swaggerinfo.OpenApiInfo?.Description + $" Powered by {EnvironmentInfoHelper.FrameworkDescription} on {SystemInfoHelper.OperatingSystem}",
                    Contact = new OpenApiContact
                    {
                        Name = AppSettings.Site.Admin.Name,
                        Email = AppSettings.Site.Admin.Email,
                        Url = new Uri(AppSettings.Site.Domain)
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
                // 根据相对路径排序
                //options.OrderActionsBy(o => o.RelativePath);
                // 添加其他的地址域名
                //options.AddServer(new OpenApiServer() { Url = "http://localhost:9708", Description = "地址1" });
                //options.AddServer(new OpenApiServer() { Url = "http://127.0.0.1:9708", Description = "地址2" });
            });

            // 定义认证方式 OAuth 方案名称必须是oauth2
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "在下框中输入<code>{token}</code>进行身份验证",
                // JWT默认的参数名称
                Name = "Authorization",
                // Bearer认证的数据格式
                BearerFormat = "JWT",
                // 认证主题，只对Type=Http生效，只能是Basic和Bearer
                Scheme = "Bearer",
                // 表示认证信息发在Http请求的哪个位置
                In = ParameterLocation.Header,
                // 表示认证方式，有ApiKey，Http，OAuth2，OpenIdConnect四种，其中ApiKey是用的最多的
                Type = SecuritySchemeType.Http,
            });

            // 注册全局认证（所有的接口都可以使用认证）
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    // 声明一个Scheme，注意下面的Id要和上面AddSecurityDefinition中的参数name一致
                    new OpenApiSecurityScheme
                    {
                        Reference=new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="oauth2",
                        }
                    },
                    Array.Empty<string>()
                }
            });

            //// WebApi 注释文件
            //var xmlWebApiPath = Path.Combine(AppContext.BaseDirectory, "ZhaiFanhuaBlog.WebApi.xml");
            //// 默认的第二个参数是false，这个是controller的注释，true时会显示注释，否则只显示方法注释
            //options.IncludeXmlComments(xmlWebApiPath, true);

            // 生成注释文档，必须在 OperationFilter<AppendAuthorizeToSummaryOperationFilter>() 之前，否则没有（Auth）标签
            Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(xmlPath =>
            {
                // 默认的第二个参数是false，这个是controller的注释，true时会显示注释，否则只显示方法注释
                options.IncludeXmlComments(xmlPath, true);
            });

            // 枚举添加摘要
            options.UseInlineDefinitionsForEnums();
            // 文档中显示安全小绿锁
            options.OperationFilter<AddResponseHeadersFilter>();
            // 安全小绿锁旁标记 Auth 标签
            options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            // 添加请求头的Header中的token,传递到后台
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        // 指定Swagger接口文档中参数序列化组件为Newtonsoft.Json
        services.AddSwaggerGenNewtonsoftSupport();

        return services;
    }
}