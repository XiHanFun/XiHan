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

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using XiHan.Extensions.Common.Swagger;
using XiHan.Common.Apps.Configs;
using XiHan.Utils.Consoles;
using XiHan.Utils.Infos;

namespace XiHan.Extensions.Setups.Services;

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
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // 配置Swagger，从路由、控制器和模型构建对象
        services.AddSwaggerGen(options =>
        {
            // 配置Swagger文档信息
            SwaggerInfoConfig(options);
            // 配置Swagger文档请求 带JWT Token
            SwaggerJwtConfig(options);
        });
        return services;
    }

    /// <summary>
    /// Swagger文档信息配置
    /// </summary>
    /// <param name="options"></param>
    internal static void SwaggerInfoConfig(SwaggerGenOptions options)
    {
        // 需要暴露的分组
        var publishGroup = AppSettings.Swagger.PublishGroup.GetSection();
        // 利用枚举反射加载出每个分组的接口文档，Skip(1)是因为Enum第一个FieldInfo是内置的一个Int值
        typeof(ApiGroupNames).GetFields().Skip(1).ToList().ForEach(group =>
        {
            // 获取枚举值上的特性
            if (!publishGroup.Any(pgroup => pgroup.ToLower() == group.Name.ToLower())) return;
            // 获取分组信息
            var info = group.GetCustomAttributes(typeof(GroupInfoAttribute), true).OfType<GroupInfoAttribute>().FirstOrDefault();
            // 添加文档介绍
            options.SwaggerDoc(group.Name, new OpenApiInfo
            {
                Title = info?.Title,
                Version = info?.Version,
                Description = info?.Description + $" Powered by {EnvironmentInfoHelper.FrameworkDescription} on {SystemInfoHelper.OperatingSystem}",
                Contact = new OpenApiContact
                {
                    Name = AppSettings.Syses.Admin.Name.GetValue(),
                    Email = AppSettings.Syses.Admin.Email.GetValue(),
                    Url = new Uri(AppSettings.Syses.Domain.GetValue())
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });
            // 根据相对路径排序
            //options.OrderActionsBy(o => o.RelativePath);
        });

        // 核心逻辑代码，指定分组被加载时回调进入，也就是swagger右上角下拉框内的分组加载时，每一个分组加载时都会遍历所有控制器的 action 进入一次这个方法体内，返回true就暴露，否则隐藏
        options.DocInclusionPredicate((docName, apiDescription) =>
        {
            // 反射获取基类 ApiController 的 ApiGroupAttribute 信息
            var controllerAttributeList = ((ControllerActionDescriptor)apiDescription.ActionDescriptor).ControllerTypeInfo.BaseType?
                .GetCustomAttributes(typeof(ApiGroupAttribute), true).OfType<ApiGroupAttribute>()
                .ToList();
            // 反射获取派生类 Action 的 ApiGroupAttribute 信息
            var actionAttributeList = apiDescription.ActionDescriptor.EndpointMetadata
                .Where(x => x is ApiGroupAttribute).OfType<ApiGroupAttribute>()
                .ToList();

            // 所有含 ApiGroupAttribute 的集合
            var apiGroupAttributeList = new List<ApiGroupAttribute>();
            // 为空时插入空，减少 if 判断
            var emptyAttribute = Array.Empty<ApiGroupAttribute>().ToList();
            apiGroupAttributeList.AddRange(controllerAttributeList ?? emptyAttribute);
            apiGroupAttributeList.AddRange(actionAttributeList);

            // 判断所有的分组名称是否含有此名称
            if (apiGroupAttributeList.Any())
            {
                var containList = new List<bool>();
                // 遍历判断是否包含这个分组
                apiGroupAttributeList.ForEach(attribute =>
                {
                    containList.Add(attribute.GroupNames.Any(x => x.ToString() == docName));
                });
                // 若有，则为该分组名称分配此 Action
                if (containList.Any(c => c))
                {
                    return true;
                }
            }
            return false;
        });

        // 枚举添加摘要
        options.UseInlineDefinitionsForEnums();

        try
        {
            // 生成注释文档，必须在 OperationFilter<AppendAuthorizeToSummaryOperationFilter>() 之前，否则没有(Auth)标签
            Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(xmlPath =>
            {
                // 默认的第二个参数是false，这个是controller的注释，true时会显示注释，否则只显示方法注释
                options.IncludeXmlComments(xmlPath, true);
            });
        }
        catch (Exception ex)
        {
            var errorMsg = $"Swagger 文档加载失败！";
            Log.Error(ex, errorMsg);
            errorMsg.WriteLineError();
        }
    }

    /// <summary>
    /// Swagger文档中请求带JWT Token
    /// </summary>
    /// <param name="options"></param>
    internal static void SwaggerJwtConfig(SwaggerGenOptions options)
    {
        // 定义安全方案
        var securitySchemeOauth2 = new OpenApiSecurityScheme
        {
            Description = "在下框中输入<code>{token}</code>进行身份验证",
            // JWT默认的参数名称
            Name = "Authorization",
            // 标识承载令牌的Bearer认证的数据格式，该信息主要是用于文档
            BearerFormat = "JWT",
            // 认证主题，在Type=Http时，只能是Basic和Bearer
            Scheme = "Bearer",
            // 表示认证信息发在Http请求的哪个位置
            In = ParameterLocation.Header,
            // 表示认证方式，有ApiKey，Http，OAuth2，OpenIdConnect四种，其中ApiKey是用的最多的
            Type = SecuritySchemeType.Http,
        };

        // 定义认证，方案名称必须是 oauth2
        options.AddSecurityDefinition("oauth2", securitySchemeOauth2);
        // 注册全局认证，即所有的接口都可以使用认证
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                // 必须与上面声明的一致，否则小绿锁混乱,即API全部会加小绿锁
                securitySchemeOauth2,
                Array.Empty<string>()
            }
        });

        // 文档中显示安全小绿锁，在对应的 Action 上添加[Authorize]
        options.OperationFilter<AddResponseHeadersFilter>();
        // 安全小绿锁旁标记 Auth 标签
        options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
        // 添加请求头的 Header 中的 token,传递到后台
        options.OperationFilter<SecurityRequirementsOperationFilter>();
    }
}