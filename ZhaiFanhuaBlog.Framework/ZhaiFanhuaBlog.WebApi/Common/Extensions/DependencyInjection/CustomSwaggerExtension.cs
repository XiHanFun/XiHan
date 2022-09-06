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
using ZhaiFanhuaBlog.Utils.Config;
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
    /// <returns></returns>
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        // 用于最小API Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        // 配置Swagger,从路由、控制器和模型构建对象
        services.AddSwaggerGen(options =>
        {
            //生成多个文档显示
            SwaggerInfo.SwaggerInfos.ForEach(swaggerinfo =>
            {
                //添加文档介绍
                options.SwaggerDoc(swaggerinfo.UrlPrefix, new OpenApiInfo
                {
                    Version = $"The Current Version: Full-{swaggerinfo.OpenApiInfo?.Version}",
                    Title = swaggerinfo.OpenApiInfo?.Title,
                    Description = swaggerinfo.OpenApiInfo?.Description + $" Powered by {EnvironmentInfoHelper.FrameworkDescription} on {SystemInfoHelper.OperatingSystem}",
                    Contact = new OpenApiContact
                    {
                        Name = ConfigHelper.Configuration.GetValue<string>("Configuration:Admin:Name"),
                        Email = ConfigHelper.Configuration.GetValue<string>("Configuration:Admin:Email")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
                // 根据相对路径排序
                options.OrderActionsBy(o => o.RelativePath);
            });
            // 枚举添加摘要
            options.UseInlineDefinitionsForEnums();
            // 应用Controller的API文档描述信息
            options.DocumentFilter<SwaggerDocumentFilter>();
            // 生成注释文件
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            // 默认的第二个参数是false，这个是controller的注释，true时会显示注释，否则只显示方法注释
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);

            // 文档中显示安全小绿锁
            options.OperationFilter<AddResponseHeadersFilter>();
            options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            // 添加请求头的Header中的token,传递到后台
            options.OperationFilter<SecurityRequirementsOperationFilter>();

            // 定义JwtBearer认证，必须是 oauth2
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "在下框中输入{token}进行身份验证",
                // 携带认证信息的参数名，比如Jwt默认是Authorization
                Name = "Authorization",
                // Bearer认证的数据格式，默认为Bearer Token（中间有一个空格）
                BearerFormat = "JWT",
                // 认证主题，只对Type=Http生效，只能是basic和bearer
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
        });
        // 指定Swagger接口文档中参数序列化组件为Newtonsoft.Json
        services.AddSwaggerGenNewtonsoftSupport();

        return services;
    }

    /// <summary>
    /// Swagger应用扩展
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            SwaggerInfo.SwaggerInfos.ForEach(swaggerinfo =>
            {
                //切换版本操作,参数一是使用的哪个json文件,参数二是个名字
                options.SwaggerEndpoint($"/swagger/{swaggerinfo.UrlPrefix}/swagger.json", swaggerinfo.OpenApiInfo?.Title);
            });
            // 模型的默认扩展深度，设置为 -1 完全隐藏模型
            options.DefaultModelsExpandDepth(-1);
            // API前缀设置为空
            options.RoutePrefix = string.Empty;
            // API页面标题
            options.DocumentTitle = $"{ConfigHelper.Configuration.GetValue<string>("Configuration:Name")}接口文档";
            // API文档仅展开标记 List：列表式（展开子类），默认值;Full：完全展开;None：列表式（不展开子类）
            options.DocExpansion(DocExpansion.None);
        });
        return app;
    }
}