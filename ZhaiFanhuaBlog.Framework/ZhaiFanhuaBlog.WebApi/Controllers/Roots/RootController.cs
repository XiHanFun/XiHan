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
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Models.Roots.Init;
using ZhaiFanhuaBlog.Models.Users.Init;
using ZhaiFanhuaBlog.WebApi.Common.Extensions.Swagger;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Roots;

/// <summary>
/// 系统管理
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class RootController : ControllerBase
{
    private readonly IRootStateService _IRootStateService;
    private readonly IUserAuthorityService _IUserAuthorityService;
    private readonly IUserRoleService _IUserRoleService;
    private readonly IUserRoleAuthorityService _IUserRoleAuthorityService;
    private readonly IUserAccountService _IUserAccountService;
    private readonly IUserAccountRoleService _IUserAccountRoleService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iRootStateService"></param>
    /// <param name="iUserAuthorityService"></param>
    /// <param name="iUserRoleService"></param>
    /// <param name="iUserRoleAuthorityService"></param>
    /// <param name="iUserAccountRoleService"></param>
    /// <param name="iUserAccountService"></param>
    public RootController(IRootStateService iRootStateService,
        IUserAuthorityService iUserAuthorityService,
        IUserRoleService iUserRoleService,
        IUserRoleAuthorityService iUserRoleAuthorityService,
        IUserAccountService iUserAccountService,
        IUserAccountRoleService iUserAccountRoleService)
    {
        _IRootStateService = iRootStateService;
        _IUserAuthorityService = iUserAuthorityService;
        _IUserRoleService = iUserRoleService;
        _IUserRoleAuthorityService = iUserRoleAuthorityService;
        _IUserAccountService = iUserAccountService;
        _IUserAccountRoleService = iUserAccountRoleService;
    }

    /// <summary>
    /// 初始化状态
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<bool> Init()
    {
        bool result = false;
        result = await _IRootStateService.InitRootStateAsync(RootInitData.RootStateList);
        result = await _IRootStateService.InitRootStateAsync(RootInitData.RootStateList);
        result = await _IUserAuthorityService.InitUserAuthorityAsync(UserInitData.UserAuthorityList);
        result = await _IUserRoleService.InitUserRoleAsync(UserInitData.UserRoleList);
        result = await _IUserAccountService.InitUserAccountAsync(UserInitData.UserAccountList);
        if (!result) throw new ApplicationException("InitUserAuthorityAsync");
        return true;
    }
}