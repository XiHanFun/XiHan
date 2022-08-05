// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootController
// Guid:69e9d954-b467-45e7-bab2-7d8fd437a433
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-29 下午 10:42:07
// ----------------------------------------------------------------

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZhaiFanhuaBlog.IServices.Roots;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Bases.Response.Model;
using ZhaiFanhuaBlog.Models.Response;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Models.Roots.Init;
using ZhaiFanhuaBlog.Models.Users.Init;
using ZhaiFanhuaBlog.ViewModels.Roots;
using ZhaiFanhuaBlog.WebApi.Common.Extensions.Swagger;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Roots;

/// <summary>
/// 系统管理
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
public class RootController : ControllerBase
{
    private readonly IRootStateService _IRootStateService;
    private readonly IRootAuthorityService _IRootAuthorityService;
    private readonly IRootRoleService _IRootRoleService;
    private readonly IRootRoleAuthorityService _IRootRoleAuthorityService;
    private readonly IRootMenuService _IRootMenuService;
    private readonly IRootRoleMenuService _IRootRoleMenuService;
    private readonly IUserAccountService _IUserAccountService;
    private readonly IUserAccountRoleService _IUserAccountRoleService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iRootStateService"></param>
    /// <param name="iRootAuthorityService"></param>
    /// <param name="iRootRoleService"></param>
    /// <param name="iRootRoleAuthorityService"></param>
    /// <param name="iRootMenuService"></param>
    /// <param name="iRootRoleMenuService"></param>
    /// <param name="iUserAccountService"></param>
    /// <param name="iUserAccountRoleService"></param>
    public RootController(IRootStateService iRootStateService,
        IRootAuthorityService iRootAuthorityService,
        IRootRoleService iRootRoleService,
        IRootRoleAuthorityService iRootRoleAuthorityService,
        IRootMenuService iRootMenuService,
        IRootRoleMenuService iRootRoleMenuService,
        IUserAccountService iUserAccountService,
        IUserAccountRoleService iUserAccountRoleService)
    {
        _IRootStateService = iRootStateService;
        _IRootAuthorityService = iRootAuthorityService;
        _IRootRoleService = iRootRoleService;
        _IRootRoleAuthorityService = iRootRoleAuthorityService;
        _IRootMenuService = iRootMenuService;
        _IRootRoleMenuService = iRootRoleMenuService;
        _IUserAccountService = iUserAccountService;
        _IUserAccountRoleService = iUserAccountRoleService;
    }

    /// <summary>
    /// 初始化状态
    /// </summary>
    /// <returns></returns>
    [HttpPost("Init")]
    public async Task<bool> Init()
    {
        bool result = false;
        result = await _IRootStateService.InitRootStateAsync(RootInitData.RootStateList);
        //result = await _IRootAuthorityService.InitRootAuthorityAsync(UserInitData.RootAuthorityList);
        //result = await _IRootRoleService.InitRootRoleAsync(UserInitData.RootRoleList);
        //result = await _IUserAccountService.InitUserAccountAsync(UserInitData.UserAccountList);
        if (!result) throw new ApplicationException("InitRootAuthorityAsync");
        return true;
    }

    #region 系统权限

    /// <summary>
    /// 新增系统权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cRootAuthorityDto"></param>
    /// <returns></returns>
    [HttpPost("Authority")]
    public async Task<ResultModel> CreateRootAuthority([FromServices] IMapper iMapper, [FromBody] CRootAuthorityDto cRootAuthorityDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var rootAuthority = iMapper.Map<RootAuthority>(cRootAuthorityDto);
            rootAuthority.CreateId = Guid.Parse(user);
            if (await _IRootAuthorityService.CreateRootAuthorityAsync(rootAuthority))
                return ResultResponse.OK("新增系统权限成功");
        }
        return ResultResponse.BadRequest("新增系统权限失败");
    }

    /// <summary>
    /// 删除系统权限
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Authority/{guid}")]
    public async Task<ResultModel> DeleteRootAuthority([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IRootAuthorityService.DeleteRootAuthorityAsync(guid, deleteId))
                return ResultResponse.OK("删除系统权限成功");
        }
        return ResultResponse.BadRequest("删除系统权限失败");
    }

    /// <summary>
    /// 修改系统权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cRootAuthorityDto"></param>
    /// <returns></returns>
    [HttpPut("Authority")]
    public async Task<ResultModel> ModifyRootAuthority([FromServices] IMapper iMapper, [FromBody] CRootAuthorityDto cRootAuthorityDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var rootAuthority = iMapper.Map<RootAuthority>(cRootAuthorityDto);
            rootAuthority.ModifyId = Guid.Parse(user);
            rootAuthority = await _IRootAuthorityService.ModifyRootAuthorityAsync(rootAuthority);
            if (rootAuthority != null)
                return ResultResponse.OK(iMapper.Map<RRootAuthorityDto>(rootAuthority));
        }
        return ResultResponse.BadRequest("修改系统权限失败");
    }

    /// <summary>
    /// 查找系统权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Authority/{guid}")]
    public async Task<ResultModel> FindRootAuthority([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var rootAuthority = await _IRootAuthorityService.FindRootAuthorityAsync(guid);
            if (rootAuthority != null)
                return ResultResponse.OK(iMapper.Map<RRootAuthorityDto>(rootAuthority));
        }
        return ResultResponse.BadRequest("该系统权限不存在");
    }

    /// <summary>
    /// 查询系统权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Authorities")]
    public async Task<ResultModel> QueryRootAuthorities([FromServices] IMapper iMapper)
    {
        var rootAuthority = await _IRootAuthorityService.QueryRootAuthorityAsync();
        if (rootAuthority.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RRootAuthorityDto>>(rootAuthority));
        return ResultResponse.BadRequest("未查询到系统权限");
    }

    #endregion 系统权限

    #region 系统角色

    /// <summary>
    /// 新增系统角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cRootRoleDto"></param>
    /// <returns></returns>
    [HttpPost("Role")]
    public async Task<ResultModel> CreateRootRole([FromServices] IMapper iMapper, [FromBody] CRootRoleDto cRootRoleDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userRole = iMapper.Map<RootRole>(cRootRoleDto);
            userRole.CreateId = Guid.Parse(user);
            if (await _IRootRoleService.CreateRootRoleAsync(userRole))
                return ResultResponse.OK("新增系统角色成功");
        }
        return ResultResponse.BadRequest("新增系统角色失败");
    }

    /// <summary>
    /// 删除系统角色
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Role/{guid}")]
    public async Task<ResultModel> DeleteRootRole([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IRootRoleService.DeleteRootRoleAsync(guid, deleteId))
                return ResultResponse.OK("删除系统角色成功");
        }
        return ResultResponse.BadRequest("删除系统角色失败");
    }

    /// <summary>
    /// 修改系统角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cRootRoleDto"></param>
    /// <returns></returns>
    [HttpPut("Role")]
    public async Task<ResultModel> ModifyRootRole([FromServices] IMapper iMapper, [FromBody] CRootRoleDto cRootRoleDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userRole = iMapper.Map<RootRole>(cRootRoleDto);
            userRole.ModifyId = Guid.Parse(user);
            userRole = await _IRootRoleService.ModifyRootRoleAsync(userRole);
            if (userRole != null)
                return ResultResponse.OK(iMapper.Map<RRootRoleDto>(userRole));
        }
        return ResultResponse.BadRequest("修改系统角色失败");
    }

    /// <summary>
    /// 查找系统角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Role/{guid}")]
    public async Task<ResultModel> FindRootRole([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userRole = await _IRootRoleService.FindRootRoleAsync(guid);
            if (userRole != null)
                return ResultResponse.OK(iMapper.Map<RRootRoleDto>(userRole));
        }
        return ResultResponse.BadRequest("该系统角色不存在");
    }

    /// <summary>
    /// 查询系统角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Roles")]
    public async Task<ResultModel> QueryRootRoles([FromServices] IMapper iMapper)
    {
        var userRole = await _IRootRoleService.QueryRootRoleAsync();
        if (userRole.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RRootRoleDto>>(userRole));
        return ResultResponse.BadRequest("未查询到系统角色");
    }

    #endregion 系统角色

    #region 为系统角色分配权限

    /// <summary>
    /// 新增系统角色权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cRootRoleAuthorityDto"></param>
    /// <returns></returns>
    [HttpPost("Role/Authority")]
    public async Task<ResultModel> CreateRootRoleAuthority([FromServices] IMapper iMapper, [FromBody] CRootRoleAuthorityDto cRootRoleAuthorityDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var rootRoleAuthority = iMapper.Map<RootRoleAuthority>(cRootRoleAuthorityDto);
            rootRoleAuthority.CreateId = Guid.Parse(user);
            if (await _IRootRoleAuthorityService.CreateRootRoleAuthorityAsync(rootRoleAuthority))
                return ResultResponse.OK("新增系统角色权限成功");
        }
        return ResultResponse.BadRequest("新增系统角色权限失败");
    }

    /// <summary>
    /// 删除系统角色权限
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Role/Authority/{guid}")]
    public async Task<ResultModel> DeleteRootRoleAuthority([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IRootRoleAuthorityService.DeleteRootRoleAuthorityAsync(guid, deleteId))
                return ResultResponse.OK("删除系统角色权限成功");
        }
        return ResultResponse.BadRequest("删除系统角色权限失败");
    }

    /// <summary>
    /// 修改系统角色权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cRootRoleAuthorityDto"></param>
    /// <returns></returns>
    [HttpPut("Role/Authority")]
    public async Task<ResultModel> ModifyRootRoleAuthority([FromServices] IMapper iMapper, [FromBody] CRootRoleAuthorityDto cRootRoleAuthorityDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var rootRoleAuthority = iMapper.Map<RootRoleAuthority>(cRootRoleAuthorityDto);
            rootRoleAuthority.ModifyId = Guid.Parse(user);
            rootRoleAuthority = await _IRootRoleAuthorityService.ModifyRootRoleAuthorityAsync(rootRoleAuthority);
            if (rootRoleAuthority != null)
                return ResultResponse.OK(iMapper.Map<RRootRoleAuthorityDto>(rootRoleAuthority));
        }
        return ResultResponse.BadRequest("修改系统角色权限失败");
    }

    /// <summary>
    /// 查找系统角色权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Role/Authority/{guid}")]
    public async Task<ResultModel?> FindRootRoleAuthority([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var rootRoleAuthority = await _IRootRoleAuthorityService.FindRootRoleAuthorityAsync(guid);
            if (rootRoleAuthority != null)
                return ResultResponse.OK(iMapper.Map<RRootRoleAuthorityDto>(rootRoleAuthority));
        }
        return ResultResponse.BadRequest("该系统角色权限不存在");
    }

    /// <summary>
    /// 查询系统角色权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Role/Authorities")]
    public async Task<ResultModel> QueryRootRoleAuthorities([FromServices] IMapper iMapper)
    {
        var rootRoleAuthority = await _IRootRoleAuthorityService.QueryRootRoleAuthorityAsync();
        if (rootRoleAuthority.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RRootRoleAuthorityDto>>(rootRoleAuthority));
        return ResultResponse.BadRequest("未查询到系统角色权限");
    }

    #endregion 为系统角色分配权限

    #region 系统菜单

    /// <summary>
    /// 新增系统菜单
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cRootMenuDto"></param>
    /// <returns></returns>
    [HttpPost("Menu")]
    public async Task<ResultModel> CreateRootMenu([FromServices] IMapper iMapper, [FromBody] CRootMenuDto cRootMenuDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var rootMenu = iMapper.Map<RootMenu>(cRootMenuDto);
            rootMenu.CreateId = Guid.Parse(user);
            if (await _IRootMenuService.CreateRootMenuAsync(rootMenu))
                return ResultResponse.OK("新增系统菜单成功");
        }
        return ResultResponse.BadRequest("新增系统菜单失败");
    }

    /// <summary>
    /// 删除系统菜单
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Menu/{guid}")]
    public async Task<ResultModel> DeleteRootMenu([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IRootMenuService.DeleteRootMenuAsync(guid, deleteId))
                return ResultResponse.OK("删除系统菜单成功");
        }
        return ResultResponse.BadRequest("删除系统菜单失败");
    }

    /// <summary>
    /// 修改系统菜单
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cRootMenuDto"></param>
    /// <returns></returns>
    [HttpPut("Menu")]
    public async Task<ResultModel> ModifyRootMenu([FromServices] IMapper iMapper, [FromBody] CRootMenuDto cRootMenuDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var rootMenu = iMapper.Map<RootMenu>(cRootMenuDto);
            rootMenu.ModifyId = Guid.Parse(user);
            rootMenu = await _IRootMenuService.ModifyRootMenuAsync(rootMenu);
            if (rootMenu != null)
                return ResultResponse.OK(iMapper.Map<RRootMenuDto>(rootMenu));
        }
        return ResultResponse.BadRequest("修改系统菜单失败");
    }

    /// <summary>
    /// 查找系统菜单
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Menu/{guid}")]
    public async Task<ResultModel> FindRootMenu([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var rootMenu = await _IRootMenuService.FindRootMenuAsync(guid);
            if (rootMenu != null)
                return ResultResponse.OK(iMapper.Map<RRootMenuDto>(rootMenu));
        }
        return ResultResponse.BadRequest("该系统菜单不存在");
    }

    /// <summary>
    /// 查询系统菜单
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Menus")]
    public async Task<ResultModel> QueryRootMenus([FromServices] IMapper iMapper)
    {
        var rootMenu = await _IRootMenuService.QueryRootMenuAsync();
        if (rootMenu.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RRootMenuDto>>(rootMenu));
        return ResultResponse.BadRequest("未查询到系统菜单");
    }

    #endregion 系统菜单

    #region 为系统角色分配系统菜单

    /// <summary>
    /// 新增系统角色菜单
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cRootRoleMenuDto"></param>
    /// <returns></returns>
    [HttpPost("Role/Menu")]
    public async Task<ResultModel> CreateRootRoleMenu([FromServices] IMapper iMapper, [FromBody] CRootRoleMenuDto cRootRoleMenuDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var rootRoleAuthority = iMapper.Map<RootRoleMenu>(cRootRoleMenuDto);
            rootRoleAuthority.CreateId = Guid.Parse(user);
            if (await _IRootRoleMenuService.CreateRootRoleMenuAsync(rootRoleAuthority))
                return ResultResponse.OK("新增系统角色菜单成功");
        }
        return ResultResponse.BadRequest("新增系统角色菜单失败");
    }

    /// <summary>
    /// 删除系统角色菜单
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Role/Menu/{guid}")]
    public async Task<ResultModel> DeleteRootRoleMenu([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IRootRoleMenuService.DeleteRootRoleMenuAsync(guid, deleteId))
                return ResultResponse.OK("删除系统角色菜单成功");
        }
        return ResultResponse.BadRequest("删除系统角色菜单失败");
    }

    /// <summary>
    /// 修改系统角色菜单
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cRootRoleMenuDto"></param>
    /// <returns></returns>
    [HttpPut("Role/Menu")]
    public async Task<ResultModel> ModifyRootRoleMenu([FromServices] IMapper iMapper, [FromBody] CRootRoleMenuDto cRootRoleMenuDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var rootRoleAuthority = iMapper.Map<RootRoleMenu>(cRootRoleMenuDto);
            rootRoleAuthority.ModifyId = Guid.Parse(user);
            rootRoleAuthority = await _IRootRoleMenuService.ModifyRootRoleMenuAsync(rootRoleAuthority);
            if (rootRoleAuthority != null)
                return ResultResponse.OK(iMapper.Map<RRootRoleMenuDto>(rootRoleAuthority));
        }
        return ResultResponse.BadRequest("修改系统角色菜单失败");
    }

    /// <summary>
    /// 查找系统角色菜单
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Role/Menu/{guid}")]
    public async Task<ResultModel?> FindRootRoleMenu([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var rootRoleAuthority = await _IRootRoleMenuService.FindRootRoleMenuAsync(guid);
            if (rootRoleAuthority != null)
                return ResultResponse.OK(iMapper.Map<RRootRoleMenuDto>(rootRoleAuthority));
        }
        return ResultResponse.BadRequest("该系统角色菜单不存在");
    }

    /// <summary>
    /// 查询系统角色菜单
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Role/Menus")]
    public async Task<ResultModel> QueryRootRoleMenus([FromServices] IMapper iMapper)
    {
        var rootRoleAuthority = await _IRootRoleMenuService.QueryRootRoleMenuAsync();
        if (rootRoleAuthority.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RRootRoleMenuDto>>(rootRoleAuthority));
        return ResultResponse.BadRequest("未查询到系统角色菜单");
    }

    #endregion 为系统角色分配系统菜单
}