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
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.WebApi.Common.Extensions.Swagger;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Roots;

/// <summary>
/// 系统管理
/// </summary>
[Route("api/[controller]"), Produces("application/json")]
[ApiController, ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
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
    public async Task<bool> Init()
    {
        List<RootState> rootStates = new()
        {
            // All 总状态
            new RootState
            {
                TypeKey = "All",
                TypeName = "总状态",
                StateKey = -1,
                StateName = "异常",
            },
            new RootState
            {
                TypeKey = "All",
                TypeName = "总状态",
                StateKey = 0,
                StateName = "删除",
            },
            new RootState
            {
                TypeKey = "All",
                TypeName = "总状态",
                StateKey = 1,
                StateName = "正常",
            },
            new RootState
            {
                TypeKey = "All",
                TypeName = "总状态",
                StateKey = 2,
                StateName = "审核",
            }
        };

        //await _IRootStateService.InitUserAuthorityAsync();

        return await _IRootStateService.InitRootStateAsync(rootStates);
    }
}