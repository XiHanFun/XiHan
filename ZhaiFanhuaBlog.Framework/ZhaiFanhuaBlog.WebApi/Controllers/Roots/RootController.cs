// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootController
// Guid:69e9d954-b467-45e7-bab2-7d8fd437a433
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-29 下午 10:42:07
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using ZhaiFanhuaBlog.IServices.Roots;
using ZhaiFanhuaBlog.WebApi.Common.Extensions.Swagger;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Roots;

/// <summary>
/// 系统管理
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
public class RootController : ControllerBase
{
    private readonly IRootStateService _IRootStateService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iRootStateService"></param>
    public RootController(IRootStateService iRootStateService)
    {
        _IRootStateService = iRootStateService;
    }

    /// <summary>
    /// 初始化状态
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<bool> InitRootState()
    {
        return await _IRootStateService.InitRootStatesAsync();
    }
}