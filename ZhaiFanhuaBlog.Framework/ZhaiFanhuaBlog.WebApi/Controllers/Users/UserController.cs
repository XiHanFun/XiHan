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
using System.Net;
using System.Security.Claims;
using System.Text;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Bases.Response.Model;
using ZhaiFanhuaBlog.Models.Response;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Utils.Encryptions;
using ZhaiFanhuaBlog.ViewModels.Users;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Users;

/// <summary>
/// 用户管理
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
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
    /// <param name="cUserAuthorityDto"></param>
    /// <returns></returns>
    [HttpPost("Authority")]
    public async Task<ResultModel> CreateUserAuthority([FromServices] IMapper iMapper, [FromBody] CUserAuthorityDto cUserAuthorityDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userAuthority = iMapper.Map<UserAuthority>(cUserAuthorityDto);
            userAuthority.CreateId = Guid.Parse(user);
            if (await _IUserAuthorityService.CreateUserAuthorityAsync(userAuthority))
                return ResultResponse.OK("新增用户权限成功");
        }
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
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IUserAuthorityService.DeleteUserAuthorityAsync(guid, deleteId))
                return ResultResponse.OK("删除用户权限成功");
        }
        return ResultResponse.BadRequest("删除用户权限失败");
    }

    /// <summary>
    /// 修改用户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cUserAuthorityDto"></param>
    /// <returns></returns>
    [HttpPut("Authority")]
    public async Task<ResultModel> ModifyUserAuthority([FromServices] IMapper iMapper, [FromBody] CUserAuthorityDto cUserAuthorityDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userAuthority = iMapper.Map<UserAuthority>(cUserAuthorityDto);
            userAuthority.ModifyId = Guid.Parse(user);
            userAuthority = await _IUserAuthorityService.ModifyUserAuthorityAsync(userAuthority);
            if (userAuthority != null)
                return ResultResponse.OK(iMapper.Map<RUserAuthorityDto>(userAuthority));
        }
        return ResultResponse.BadRequest("修改用户权限失败");
    }

    /// <summary>
    /// 查找用户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Authority/{guid}")]
    public async Task<ResultModel> FindUserAuthority([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userAuthority = await _IUserAuthorityService.FindUserAuthorityAsync(guid);
            if (userAuthority != null)
                return ResultResponse.OK(iMapper.Map<RUserAuthorityDto>(userAuthority));
        }
        return ResultResponse.BadRequest("该用户权限不存在");
    }

    /// <summary>
    /// 查询用户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Authorities")]
    public async Task<ResultModel> QueryUserAuthority([FromServices] IMapper iMapper)
    {
        var userAuthority = await _IUserAuthorityService.QueryUserAuthorityAsync();
        if (userAuthority.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RUserAuthorityDto>>(userAuthority));
        return ResultResponse.BadRequest("未查询到用户权限");
    }

    #endregion 用户权限

    #region 用户角色

    /// <summary>
    /// 新增用户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cUserRoleDto"></param>
    /// <returns></returns>
    [HttpPost("Role")]
    public async Task<ResultModel> CreateUserRole([FromServices] IMapper iMapper, [FromBody] CUserRoleDto cUserRoleDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userRole = iMapper.Map<UserRole>(cUserRoleDto);
            userRole.CreateId = Guid.Parse(user);
            if (await _IUserRoleService.CreateUserRoleAsync(userRole))
                return ResultResponse.OK("新增用户角色成功");
        }
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
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IUserRoleService.DeleteUserRoleAsync(guid, deleteId))
                return ResultResponse.OK("删除用户角色成功");
        }
        return ResultResponse.BadRequest("删除用户角色失败");
    }

    /// <summary>
    /// 修改用户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cUserRoleDto"></param>
    /// <returns></returns>
    [HttpPut("Role")]
    public async Task<ResultModel> ModifyUserRole([FromServices] IMapper iMapper, [FromBody] CUserRoleDto cUserRoleDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userRole = iMapper.Map<UserRole>(cUserRoleDto);
            userRole.ModifyId = Guid.Parse(user);
            userRole = await _IUserRoleService.ModifyUserRoleAsync(userRole);
            if (userRole != null)
                return ResultResponse.OK(iMapper.Map<RUserRoleDto>(userRole));
        }
        return ResultResponse.BadRequest("修改用户角色失败");
    }

    /// <summary>
    /// 查找用户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Role/{guid}")]
    public async Task<ResultModel> FindUserRole([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userRole = await _IUserRoleService.FindUserRoleAsync(guid);
            if (userRole != null)
                return ResultResponse.OK(iMapper.Map<RUserRoleDto>(userRole));
        }
        return ResultResponse.BadRequest("该用户角色不存在");
    }

    /// <summary>
    /// 查询用户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Roles")]
    public async Task<ResultModel> QueryUserRole([FromServices] IMapper iMapper)
    {
        var userRole = await _IUserRoleService.QueryUserRoleAsync();
        if (userRole.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RUserRoleDto>>(userRole));
        return ResultResponse.BadRequest("未查询到用户角色");
    }

    #endregion 用户角色

    #region 用户账户

    /// <summary>
    /// 新增用户账户
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cUserAccountDto"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Account")]
    public async Task<ResultModel> CreateUserAccount([FromServices] IMapper iMapper, [FromBody] CUserAccountDto cUserAccountDto)
    {
        var userAccount = iMapper.Map<UserAccount>(cUserAccountDto);
        // 密码加密
        userAccount.Password = MD5Helper.EncryptMD5(Encoding.UTF8, cUserAccountDto.Password);
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
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IUserAccountService.DeleteUserAccountAsync(guid, deleteId))
                return ResultResponse.OK("删除用户账户成功");
        }
        return ResultResponse.BadRequest("删除用户账户失败");
    }

    /// <summary>
    /// 修改用户账户
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cUserAccountDto"></param>
    /// <returns></returns>
    [HttpPut("Account")]
    public async Task<ResultModel> ModifyUserAccount([FromServices] IMapper iMapper, [FromBody] CUserAccountDto cUserAccountDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userAccount = iMapper.Map<UserAccount>(cUserAccountDto);
            // 密码加密
            userAccount.Password = MD5Helper.EncryptMD5(Encoding.UTF8, cUserAccountDto.Password);
            userAccount.ModifyId = Guid.Parse(user);
            userAccount = await _IUserAccountService.ModifyUserAccountAsync(userAccount);
            if (userAccount != null)
                return ResultResponse.OK(iMapper.Map<RUserAccountDto>(userAccount));
        }
        return ResultResponse.BadRequest("修改用户账户失败");
    }

    /// <summary>
    /// 查找用户账户
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Account/{guid}")]
    public async Task<ResultModel> FindUserAccount([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userAccount = await _IUserAccountService.FindUserAccountByGuidAsync(guid);
            if (userAccount != null)
                return ResultResponse.OK(iMapper.Map<RUserAccountDto>(userAccount));
        }
        return ResultResponse.BadRequest("该用户账户不存在");
    }

    /// <summary>
    /// 查询用户账户
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Accounts")]
    [AllowAnonymous]
    public async Task<ResultModel> QueryUserAccount([FromServices] IMapper iMapper)
    {
        var userAccounts = await _IUserAccountService.QueryUserAccountAsync();
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
    /// <param name="cUserRoleAuthorityDto"></param>
    /// <returns></returns>
    [HttpPost("Role/Authority")]
    public async Task<ResultModel> CreateUserRoleAuthority([FromServices] IMapper iMapper, [FromBody] CUserRoleAuthorityDto cUserRoleAuthorityDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userRoleAuthority = iMapper.Map<UserRoleAuthority>(cUserRoleAuthorityDto);
            userRoleAuthority.CreateId = Guid.Parse(user);
            if (await _IUserRoleAuthorityService.CreateUserRoleAuthorityAsync(userRoleAuthority))
                return ResultResponse.OK("新增用户角色权限成功");
        }
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
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IUserRoleAuthorityService.DeleteUserRoleAuthorityAsync(guid, deleteId))
                return ResultResponse.OK("删除用户角色权限成功");
        }
        return ResultResponse.BadRequest("删除用户角色权限失败");
    }

    /// <summary>
    /// 修改用户角色权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cUserRoleAuthorityDto"></param>
    /// <returns></returns>
    [HttpPut("Role/Authority")]
    public async Task<ResultModel> ModifyUserRoleAuthority([FromServices] IMapper iMapper, [FromBody] CUserRoleAuthorityDto cUserRoleAuthorityDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userRoleAuthority = iMapper.Map<UserRoleAuthority>(cUserRoleAuthorityDto);
            userRoleAuthority.ModifyId = Guid.Parse(user);
            userRoleAuthority = await _IUserRoleAuthorityService.ModifyUserRoleAuthorityAsync(userRoleAuthority);
            if (userRoleAuthority != null)
                return ResultResponse.OK(iMapper.Map<RUserRoleAuthorityDto>(userRoleAuthority));
        }
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
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userRoleAuthority = await _IUserRoleAuthorityService.FindUserRoleAuthorityAsync(guid);
            if (userRoleAuthority != null)
                return ResultResponse.OK(iMapper.Map<RUserRoleAuthorityDto>(userRoleAuthority));
        }
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
    /// <param name="cUserAccountRoleDto"></param>
    /// <returns></returns>
    [HttpPost("Account/Role")]
    public async Task<ResultModel> CreateUserAccountRole([FromServices] IMapper iMapper, [FromBody] CUserAccountRoleDto cUserAccountRoleDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userAccountRole = iMapper.Map<UserAccountRole>(cUserAccountRoleDto);
            userAccountRole.CreateId = Guid.Parse(user);
            if (await _IUserAccountRoleService.CreateUserAccountRoleAsync(userAccountRole))
                return ResultResponse.OK("新增用户账户角色成功");
        }
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
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IUserAccountRoleService.DeleteUserAccountRoleAsync(guid, deleteId))
                return ResultResponse.OK("删除用户账户角色成功");
        }
        return ResultResponse.BadRequest("删除用户账户角色失败");
    }

    /// <summary>
    /// 修改用户账户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cUserAccountRoleDto"></param>
    /// <returns></returns>
    [HttpPut("Account/Role")]
    public async Task<ResultModel> ModifyUserAccountRole([FromServices] IMapper iMapper, [FromBody] CUserAccountRoleDto cUserAccountRoleDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userAccountRole = iMapper.Map<UserAccountRole>(cUserAccountRoleDto);
            userAccountRole.ModifyId = Guid.Parse(user);
            userAccountRole = await _IUserAccountRoleService.ModifyUserAccountRoleAsync(userAccountRole);
            if (userAccountRole != null)
                return ResultResponse.OK(iMapper.Map<RUserAccountRoleDto>(userAccountRole));
        }
        return ResultResponse.BadRequest("修改用户账户角色失败");
    }

    /// <summary>
    /// 查找用户账户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Account/Role/{guid}")]
    public async Task<ResultModel> FindUserAccountRole([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var userAccountRole = await _IUserAccountRoleService.FindUserAccountRoleAsync(guid);
            if (userAccountRole != null)
                return ResultResponse.OK(iMapper.Map<RUserAccountRoleDto>(userAccountRole));
        }
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
        var userAccountRoles = await _IUserAccountRoleService.QueryUserAccountRoleAsync();
        if (userAccountRoles.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RUserAccountRoleDto>>(userAccountRoles));
        return ResultResponse.OK("未查询到用户账户角色");
    }

    #endregion 为用户账户分配角色
}