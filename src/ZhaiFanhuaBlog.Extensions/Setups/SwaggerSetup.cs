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
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
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
    /// Swagger 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // 用于最小API
        services.AddEndpointsApiExplorer();

        // 利用枚举反射加载出每个分组的Doc配置Swagger，从路由、控制器和模型构建对象
        services.AddSwaggerGen(options =>
        {
            // 配置Swagger文档信息
            SwaggerInfoConfig(options);
            // 配置Swagger文档请求 带JWT Token
            SwaggerJWTConfig(options);
        });

        // 指定Swagger接口文档中参数序列化组件为Newtonsoft.Json
        services.AddSwaggerGenNewtonsoftSupport();

        return services;
    }

    /// <summary>
    /// Swagger文档信息配置
    /// </summary>
    /// <param name="options"></param>
    public static void SwaggerInfoConfig(SwaggerGenOptions options)
    {
        // 需要暴露的分组
        var publishGroup = AppSettings.Swagger.PublishGroup;
        // 遍历ApiGroupNames所有枚举值生成多个接口文档，Skip(1)是因为Enum第一个FieldInfo是内置的一个Int值
        typeof(ApiGroupNames).GetFields().Skip(1).ToList().ForEach(group =>
        {
            // 获取枚举值上的特性
            if (publishGroup.Any(pgroup => pgroup.ToLower() == group.Name.ToLower()))
            {
                var info = group.GetCustomAttributes(typeof(GroupInfoAttribute), false).OfType<GroupInfoAttribute>().FirstOrDefault();
                // 添加文档介绍
                options.SwaggerDoc(group.Name, new OpenApiInfo
                {
                    Title = info?.Title,
                    Version = info?.Version,
                    Description = info?.Description + $" Powered by {EnvironmentInfoHelper.FrameworkDescription} on {SystemInfoHelper.OperatingSystem}",
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
            }
        });

        // 核心逻辑代码，利用反射的类进行判断标签值，判断接口归于哪个分组
        options.DocInclusionPredicate((docName, apiDescription) =>
        {
            // 反射拿到值
            var actionlist = apiDescription.ActionDescriptor.EndpointMetadata.Where(x => x is ApiGroupAttribute);
            if (actionlist.Any())
            {
                // 判断是否包含这个分组
                var actionfilter = actionlist.FirstOrDefault() as ApiGroupAttribute;
                return actionfilter!.GroupNames.Any(x => x.ToString() == docName);
            }
            return false;
        });

        // 枚举添加摘要
        options.UseInlineDefinitionsForEnums();

        // 生成注释文档，必须在 OperationFilter<AppendAuthorizeToSummaryOperationFilter>() 之前，否则没有(Auth)标签
        Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(xmlPath =>
        {
            // 默认的第二个参数是false，这个是controller的注释，true时会显示注释，否则只显示方法注释
            options.IncludeXmlComments(xmlPath, true);
        });
    }

    /// <summary>
    /// Swagger文档中请求带JWT Token
    /// </summary>
    /// <param name="options"></param>
    public static void SwaggerJWTConfig(SwaggerGenOptions options)
    {
        // 定义安全方案
        var securityScheme = new OpenApiSecurityScheme
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
        };
        // 定义认证方式 OAuth 方案名称必须是oauth2
        options.AddSecurityDefinition("oauth2", securityScheme);
        // 注册全局认证，即所有的接口都可以使用认证
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                // 必须与上面声明的一致，否则小绿锁混乱,即API全部会加小绿锁
                securityScheme,
                Array.Empty<string>()
            }
        });

        // 文档中显示安全小绿锁
        options.OperationFilter<AddResponseHeadersFilter>();
        // 添加请求头的Header中的token,传递到后台
        options.OperationFilter<SecurityRequirementsOperationFilter>();
        // 安全小绿锁旁标记 Auth 标签
        options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
    }
}