// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserController
// Guid:03069dd5-18ca-4109-b7da-4691b785bd11
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-15 下午 05:38:40
// ----------------------------------------------------------------

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Bases.Response.Model;
using ZhaiFanhuaBlog.Models.Response;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Utils.Encryptions;
using ZhaiFanhuaBlog.ViewModels.Users;
using ZhaiFanhuaBlog.WebApi.Common.Extensions.Swagger;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Users;

/// <summary>
/// 用户管理
/// </summary>
[Authorize]
[Route("api/[controller]"), Produces("application/json")]
[ApiController, ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
public class UserController : ControllerBase
{
    private readonly IHttpContextAccessor _IHttpContextAccessor;
    private readonly IUserAuthorityService _IUserAuthorityService;
    private readonly IUserRoleAuthorityService _IUserRoleAuthorityService;
    private readonly IUserRoleService _IUserRoleService;
    private readonly IUserAccountRoleService _IUserAccountRoleService;
    private readonly IUserAccountService _IUserAccountService;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="iHttpContextAccessor"></param>
    /// <param name="iUserAuthorityService"></param>
    /// <param name="iUserRoleAuthorityService"></param>
    /// <param name="iUserRoleService"></param>
    /// <param name="iUserAccountRoleService"></param>
    /// <param name="iUserAccountService"></param>
    public UserController(IHttpContextAccessor iHttpContextAccessor,
        IUserAuthorityService iUserAuthorityService,
        IUserRoleAuthorityService iUserRoleAuthorityService,
        IUserRoleService iUserRoleService,
        IUserAccountRoleService iUserAccountRoleService,
        IUserAccountService iUserAccountService)
    {
        _IHttpContextAccessor = iHttpContextAccessor;
        _IUserAuthorityService = iUserAuthorityService;
        _IUserRoleAuthorityService = iUserRoleAuthorityService;
        _IUserRoleService = iUserRoleService;
        _IUserAccountRoleService = iUserAccountRoleService;
        _IUserAccountService = iUserAccountService;
    }

    #region 用户权限

    /// <summary>
    /// 新增用户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cDto"></param>
    /// <returns></returns>
    [HttpPost("Authority")]
    public async Task<ResultModel> CreateUserAuthority([FromServices] IMapper iMapper, [FromBody] CUserAuthorityDto cDto)
    {
        var userAuthority = iMapper.Map<UserAuthority>(cDto);
        userAuthority.CreateId = Guid.Parse(User.FindFirst("UserId")!.Value);
        if (await _IUserAuthorityService.CreateUserAuthorityAsync(userAuthority))
            return ResultResponse.OK("新增用户权限成功");
        return ResultResponse.BadRequest("新增用户权限失败");
    }

    /// <summary>
    /// 删除用户权限
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Authority/{guid}")]
    public async Task<ResultModel> DeleteUserAuthority([FromRoute] Guid guid)
    {
        if (await _IUserAuthorityService.DeleteUserAuthorityAsync(guid))
            return ResultResponse.OK("删除用户权限成功");
        return ResultResponse.BadRequest("删除用户权限失败");
    }

    /// <summary>
    /// 修改用户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <param name="cDto"></param>
    /// <returns></returns>
    [HttpPut("Authority/{guid}")]
    public async Task<ResultModel> ModifyUserAuthority([FromServices] IMapper iMapper, [FromRoute] Guid guid, [FromBody] CUserAuthorityDto cDto)
    {
        var userAuthority = await _IUserAuthorityService.FindUserAuthorityAsync(guid);
        userAuthority.ParentId = cDto.ParentId;
        userAuthority.Name = cDto.Name;
        userAuthority.Type = cDto.Type;
        userAuthority.Description = cDto.Description;
        userAuthority.ModifyId = Guid.Parse(User.FindFirst("UserId")!.Value);
        userAuthority = await _IUserAuthorityService.ModifyUserAuthorityAsync(userAuthority);
        if (userAuthority != null)
            return ResultResponse.OK(iMapper.Map<RUserAuthorityDto>(userAuthority));
        return ResultResponse.BadRequest("修改用户权限失败");
    }

    /// <summary>
    /// 查找用户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Authority/{guid}")]
    public async Task<ResultModel?> FindUserAuthority([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var userAuthority = await _IUserAuthorityService.FindUserAuthorityAsync(guid);
        if (userAuthority != null)
            return ResultResponse.OK(iMapper.Map<RUserAuthorityDto>(userAuthority));
        return ResultResponse.BadRequest("该用户权限不存在");
    }

    /// <summary>
    /// 查询用户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Authorities")]
    public async Task<ResultModel> QueryUserAuthorities([FromServices] IMapper iMapper)
    {
        var userAuthorities = await _IUserAuthorityService.QueryUserAuthoritiesAsync();
        if (userAuthorities.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RUserAuthorityDto>>(userAuthorities));
        return ResultResponse.BadRequest("未查询到用户权限");
    }

    #endregion 用户权限

    #region 用户角色

    /// <summary>
    /// 新增用户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cDto"></param>
    /// <returns></returns>
    [HttpPost("Role")]
    public async Task<ResultModel> CreateUserRole([FromServices] IMapper iMapper, [FromBody] CUserRoleDto cDto)
    {
        var userRole = iMapper.Map<UserRole>(cDto);
        userRole.CreateId = Guid.Parse(User.FindFirst("UserId")!.Value);
        if (await _IUserRoleService.CreateUserRoleAsync(userRole))
            return ResultResponse.OK("新增用户角色成功");
        return ResultResponse.BadRequest("新增用户角色失败");
    }

    /// <summary>
    /// 删除用户角色
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Role/{guid}")]
    public async Task<ResultModel> DeleteUserRole([FromRoute] Guid guid)
    {
        if (await _IUserRoleService.DeleteUserRoleAsync(guid))
            return ResultResponse.OK("删除用户角色成功");
        return ResultResponse.BadRequest("删除用户角色失败");
    }

    /// <summary>
    /// 修改用户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <param name="cDto"></param>
    /// <returns></returns>
    [HttpPut("Role/{guid}")]
    public async Task<ResultModel> ModifyUserRole([FromServices] IMapper iMapper, [FromRoute] Guid guid, [FromBody] CUserRoleDto cDto)
    {
        var userRole = await _IUserRoleService.FindUserRoleAsync(guid);
        userRole.ParentId = cDto.ParentId;
        userRole.Name = cDto.Name;
        userRole.Description = cDto.Description;
        userRole.ModifyId = Guid.Parse(User.FindFirst("UserId")!.Value);
        userRole = await _IUserRoleService.ModifyUserRoleAsync(userRole);
        if (userRole != null)
            return ResultResponse.OK(iMapper.Map<RUserRoleDto>(userRole));
        return ResultResponse.BadRequest("修改用户角色失败");
    }

    /// <summary>
    /// 查找用户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Role/{guid}")]
    public async Task<ResultModel?> FindUserRole([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var userRole = await _IUserRoleService.FindUserRoleAsync(guid);
        if (userRole != null)
            return ResultResponse.OK(iMapper.Map<RUserRoleDto>(userRole));
        return ResultResponse.BadRequest("该用户角色不存在");
    }

    /// <summary>
    /// 查询用户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Roles")]
    public async Task<ResultModel> QueryUserRoles([FromServices] IMapper iMapper)
    {
        var userRoles = await _IUserRoleService.QueryUserRolesAsync();
        if (userRoles.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RUserRoleDto>>(userRoles));
        return ResultResponse.BadRequest("未查询到用户角色");
    }

    #endregion 用户角色

    #region 用户账户

    /// <summary>
    /// 新增用户账户
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cDto"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Account")]
    public async Task<ResultModel> CreateUserAccount([FromServices] IMapper iMapper, [FromBody] CUserAccountDto cDto)
    {
        // 密码加密
        var userAccount = iMapper.Map<UserAccount>(cDto);
        userAccount.Password = MD5Helper.EncryptMD5(Encoding.UTF8, cDto.Password);
        userAccount.RegisterIp = _IHttpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.GetAddressBytes();
        if (await _IUserAccountService.CreateUserAccountAsync(userAccount))
            return ResultResponse.OK("新增用户账户成功");
        return ResultResponse.BadRequest("新增用户账户失败");
    }

    /// <summary>
    /// 删除用户账户
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Account/{guid}")]
    public async Task<ResultModel> DeleteUserAccount([FromRoute] Guid guid)
    {
        if (await _IUserAccountService.DeleteUserAccountAsync(guid))
            return ResultResponse.OK("删除用户账户成功");
        return ResultResponse.BadRequest("删除用户账户失败");
    }

    /// <summary>
    /// 修改用户账户
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <param name="cDto"></param>
    /// <returns></returns>
    [HttpPut("Account/{guid}")]
    public async Task<ResultModel> ModifyUserAccount([FromServices] IMapper iMapper, [FromRoute] Guid guid, [FromBody] CUserAccountDto cDto)
    {
        var userAccount = await _IUserAccountService.FindUserAccountByGuidAsync(guid);
        userAccount.Name = cDto.Name;
        userAccount.ModifyId = Guid.Parse(User.FindFirst("UserId")!.Value);
        userAccount = await _IUserAccountService.ModifyUserAccountAsync(userAccount);
        if (userAccount != null)
            return ResultResponse.OK(iMapper.Map<RUserAccountDto>(userAccount));
        return ResultResponse.BadRequest("修改用户账户失败");
    }

    /// <summary>
    /// 查找用户账户
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Account/{guid}")]
    public async Task<ResultModel?> FindUserAccount([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var userAccount = await _IUserAccountService.FindUserAccountByGuidAsync(guid);
        if (userAccount != null)
            return ResultResponse.OK(iMapper.Map<RUserAccountDto>(userAccount));
        return ResultResponse.BadRequest("该用户账户不存在");
    }

    /// <summary>
    /// 查询用户账户
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Accounts")]
    [AllowAnonymous]
    public async Task<ResultModel> QueryUserAccounts([FromServices] IMapper iMapper)
    {
        var userAccounts = await _IUserAccountService.QueryUserAccountsAsync();
        if (userAccounts.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RUserAccountDto>>(userAccounts));
        return ResultResponse.BadRequest("未查询到用户账户");
    }

    #endregion 用户账户

    #region 为用户角色分配权限

    /// <summary>
    /// 新增用户角色权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cDto"></param>
    /// <returns></returns>
    [HttpPost("Role/Authority")]
    public async Task<ResultModel> CreateUserRoleAuthority([FromServices] IMapper iMapper, [FromBody] CUserRoleAuthorityDto cDto)
    {
        var userRoleAuthority = iMapper.Map<UserRoleAuthority>(cDto);
        userRoleAuthority.CreateId = Guid.Parse(User.FindFirst("UserId")!.Value);
        if (await _IUserRoleAuthorityService.CreateUserRoleAuthorityAsync(userRoleAuthority))
            return ResultResponse.OK("新增用户角色权限成功");
        return ResultResponse.BadRequest("新增用户角色权限失败");
    }

    /// <summary>
    /// 删除用户角色权限
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Role/Authority/{guid}")]
    public async Task<ResultModel> DeleteUserRoleAuthority([FromRoute] Guid guid)
    {
        if (await _IUserRoleAuthorityService.DeleteUserRoleAuthorityAsync(guid))
            return ResultResponse.OK("删除用户角色权限成功");
        return ResultResponse.BadRequest("删除用户角色权限失败");
    }

    /// <summary>
    /// 修改用户角色权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <param name="cDto"></param>
    /// <returns></returns>
    [HttpPut("Role/Authority/{guid}")]
    public async Task<ResultModel> ModifyUserRoleAuthority([FromServices] IMapper iMapper, [FromRoute] Guid guid, [FromBody] CUserRoleAuthorityDto cDto)
    {
        var userRoleAuthority = iMapper.Map<UserRoleAuthority>(cDto);
        userRoleAuthority.ModifyId = Guid.Parse(User.FindFirst("UserId")!.Value);
        if (await _IUserRoleAuthorityService.ModifyUserRoleAuthorityAsync(userRoleAuthority) != null)
            return ResultResponse.OK(iMapper.Map<RUserRoleAuthorityDto>(userRoleAuthority));
        return ResultResponse.BadRequest("修改用户角色权限失败");
    }

    /// <summary>
    /// 查找用户角色权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Role/Authority/{guid}")]
    public async Task<ResultModel?> FindUserRoleAuthority([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var userRoleAuthority = await _IUserRoleAuthorityService.FindUserRoleAuthorityAsync(guid);
        if (userRoleAuthority != null)
            return ResultResponse.OK(iMapper.Map<RUserRoleAuthorityDto>(userRoleAuthority));
        return ResultResponse.BadRequest("该用户角色权限不存在");
    }

    /// <summary>
    /// 查询用户角色权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Role/Authorities")]
    public async Task<ResultModel> QueryUserRoleAuthorities([FromServices] IMapper iMapper)
    {
        var userRoleAuthorities = await _IUserRoleAuthorityService.QueryUserRoleAuthoritiesAsync();
        if (userRoleAuthorities.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RUserRoleAuthorityDto>>(userRoleAuthorities));
        return ResultResponse.BadRequest("未查询到用户角色权限");
    }

    #endregion 为用户角色分配权限

    #region 为用户账户分配角色

    /// <summary>
    /// 新增用户账户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cDto"></param>
    /// <returns></returns>
    [HttpPost("Account/Role")]
    public async Task<ResultModel> CreateUserAccountRole([FromServices] IMapper iMapper, [FromBody] CUserAccountRoleDto cDto)
    {
        var userAccountRole = iMapper.Map<UserAccountRole>(cDto);
        userAccountRole.CreateId = Guid.Parse(User.FindFirst("UserId")!.Value);
        if (await _IUserAccountRoleService.CreateUserAccountRoleAsync(userAccountRole))
            return ResultResponse.OK("新增用户账户角色成功");
        return ResultResponse.BadRequest("新增用户账户角色失败");
    }

    /// <summary>
    /// 删除用户账户角色
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Account/Role/{guid}")]
    public async Task<ResultModel> DeleteUserAccountRole([FromRoute] Guid guid)
    {
        if (await _IUserAccountRoleService.DeleteUserAccountRoleAsync(guid))
            return ResultResponse.OK("删除用户账户角色成功");
        return ResultResponse.BadRequest("删除用户账户角色失败");
    }

    /// <summary>
    /// 修改用户账户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <param name="cDto"></param>
    /// <returns></returns>
    [HttpPut("Account/Role/{guid}")]
    public async Task<ResultModel> ModifyUserAccountRole([FromServices] IMapper iMapper, [FromRoute] Guid guid, [FromBody] CUserAccountRoleDto cDto)
    {
        var userAccountRole = iMapper.Map<UserAccountRole>(cDto);
        userAccountRole.ModifyId = Guid.Parse(User.FindFirst("UserId")!.Value);
        if (await _IUserAccountRoleService.ModifyUserAccountRoleAsync(userAccountRole) != null)
            return ResultResponse.OK(iMapper.Map<RUserAccountRoleDto>(userAccountRole));
        return ResultResponse.BadRequest("修改用户账户角色失败");
    }

    /// <summary>
    /// 查找用户账户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Account/Role/{guid}")]
    public async Task<ResultModel?> FindUserAccountRole([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var userAccountRole = await _IUserAccountRoleService.FindUserAccountRoleAsync(guid);
        if (userAccountRole != null)
            return ResultResponse.OK(iMapper.Map<RUserAccountRoleDto>(userAccountRole));
        return ResultResponse.BadRequest("该用户账户角色不存在");
    }

    /// <summary>
    /// 查询用户账户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Account/Roles")]
    public async Task<ResultModel> QueryUserAccountRoles([FromServices] IMapper iMapper)
    {
        var userAccountRoles = await _IUserAccountRoleService.QueryUserAccountRolesAsync();
        if (userAccountRoles.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RUserAccountRoleDto>>(userAccountRoles));
        return ResultResponse.OK("未查询到用户账户角色");
    }

    #endregion 为用户账户分配角色
}