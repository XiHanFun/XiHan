// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SwaggerInfo
// Guid:fb9017cd-48ca-4cbc-93cf-f9372a9606af
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-02 上午 11:48:18
// ----------------------------------------------------------------

using Microsoft.OpenApi.Models;

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.Swagger;

/// <summary>
/// SwaggerInfo
/// </summary>
internal class SwaggerInfo
{
    /// <summary>
    /// URL前缀
    /// </summary>
    public string? UrlPrefix { get; set; }

    /// <summary>
    /// 分组名称
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// <see cref="Microsoft.OpenApi.Models.OpenApiInfo"/>
    /// </summary>
    public OpenApiInfo? OpenApiInfo { get; set; }

    /// <summary>
    /// Swagger分组信息，将进行遍历使用
    /// </summary>
    internal static readonly List<SwaggerInfo> SwaggerInfos = new()
        {
            new SwaggerInfo
            {
                UrlPrefix = SwaggerGroup.Reception,
                GroupName = SwaggerGroup.Reception,
                OpenApiInfo = new OpenApiInfo
                {
                    Version= SwaggerVersion.v1,
                    Title = "前台接口",
                    Description = "这是用于普通用户浏览的博客前台接口",
                }
            },
            new SwaggerInfo
            {
                UrlPrefix = SwaggerGroup.Backstage,
                GroupName = SwaggerGroup.Backstage,
                OpenApiInfo = new OpenApiInfo
                {
                    Version= SwaggerVersion.v1,
                    Title = "后台接口",
                    Description = "这是用于管理的博客后台接口"
                }
            },
             new SwaggerInfo
            {
                UrlPrefix = SwaggerGroup.Authorize,
                GroupName = SwaggerGroup.Authorize,
                OpenApiInfo = new OpenApiInfo
                {
                    Version= SwaggerVersion.v1,
                    Title = "授权接口",
                    Description = "这是用于登录的博客授权接口"
                }
            },
              new SwaggerInfo
            {
                UrlPrefix = SwaggerGroup.Test,
                GroupName = SwaggerGroup.Test,
                OpenApiInfo = new OpenApiInfo
                {
                    Version= SwaggerVersion.v1,
                    Title = "测试接口",
                    Description = "这是用于测试的博客测试接口"
                }
            }
        };
}