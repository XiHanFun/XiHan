// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SwaggerDocumentFilter
// Guid:d06a5814-de2a-432a-aaa9-c658aa7272f8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-04 下午 08:30:04
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.Swagger;

/// <summary>
/// SwaggerDocumentFilter
/// </summary>
public class SwaggerDocumentFilter : IDocumentFilter
{
    /// <summary>
    /// Swagger自定义描述信息
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var tags = new List<OpenApiTag>
        {
            new OpenApiTag {
                Name = "Blog",
                Description = "博客管理接口",
                ExternalDocs = new OpenApiExternalDocs { Description = "包含：分类/文章/标签/评论/点赞/评论点赞" }
            },
            new OpenApiTag {
                Name = "Root",
                Description = "系统管理接口",
                ExternalDocs = new OpenApiExternalDocs { Description = "包含：初始化系统/菜单/角色/权限/公告/状态" }
            },
            new OpenApiTag {
                Name = "Site",
                Description = "网站配置接口",
                ExternalDocs = new OpenApiExternalDocs { Description = "包含：初始化网站/配置/皮肤/日志" }
            },
            new OpenApiTag {
                Name = "User",
                Description = "用户管理接口",
                ExternalDocs = new OpenApiExternalDocs { Description = "包含：账户/收藏/关注/通知/第三方登录/统计" }
            },
            new OpenApiTag {
                Name = "Authorize",
                Description = "登录授权接口",
                ExternalDocs = new OpenApiExternalDocs { Description = "包含：登录授权" }
            },
            new OpenApiTag {
                Name = "Test",
                Description = "系统测试接口",
                ExternalDocs = new OpenApiExternalDocs { Description = "包含：测试工具/客户端信息/IP信息" }
            },
        };

        // 实现添加自定义描述时过滤不属于同一个分组的API
        var groupName = context.ApiDescriptions.FirstOrDefault()?.GroupName;
        var apis = context.ApiDescriptions.GetType().GetField("_source", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(context.ApiDescriptions) as IEnumerable<ApiDescription>;
        var controllers = apis!.Where(x => x.GroupName != groupName).Select(x => ((ControllerActionDescriptor)x.ActionDescriptor).ControllerName).Distinct();
        // 按照Name升序排序
        swaggerDoc.Tags = tags.Where(x => !controllers!.Contains(x.Name)).OrderBy(x => x.Name).ToList();
    }
}