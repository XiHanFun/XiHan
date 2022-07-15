// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogController
// Guid:c644eda7-96b2-4216-8596-c6490c107585
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-03 上午 02:45:57
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZhaiFanhuaBlog.WebApi.Common.Extensions.Swagger;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Blogs;

/// <summary>
/// 博客管理
/// </summary>
[Authorize]
[Route("api/[controller]"), Produces("application/json")]
[ApiController, ApiExplorerSettings(GroupName = SwaggerGroup.Reception)]
public class BlogController : ControllerBase
{
}