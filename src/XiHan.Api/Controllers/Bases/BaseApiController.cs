#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseApiController
// Guid:6c522a26-5ace-4fb9-b35b-636ca94ef20e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-03 上午 12:20:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc;
using XiHan.Extensions.Common.Swagger;

namespace XiHan.Api.Controllers.Bases;

/// <summary>
/// BaseApiController
/// </summary>
[ApiController]
[Route("Api/[controller]")]
[Produces("application/json")]
[ApiGroup(ApiGroupNames.All)]
public class BaseApiController : ControllerBase
{
}