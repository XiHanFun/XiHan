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
    private readonly IUserAccountRoleService _IUserAccountRoleService;
    private readonly IUserAccountService _IUserAccountService;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="iHttpContextAccessor"></param>
    /// <param name="iUserAccountRoleService"></param>
    /// <param name="iUserAccountService"></param>
    public UserController(IHttpContextAccessor iHttpContextAccessor,
        IUserAccountRoleService iUserAccountRoleService,
        IUserAccountService iUserAccountService)
    {
        _IHttpContextAccessor = iHttpContextAccessor;
        _IUserAccountRoleService = iUserAccountRoleService;
        _IUserAccountService = iUserAccountService;
    }

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
    public async Task<ResultModel> QueryUserAccounts([FromServices] IMapper iMapper)
    {
        var userAccount = await _IUserAccountService.QueryUserAccountAsync();
        if (userAccount.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RUserAccountDto>>(userAccount));
        return ResultResponse.BadRequest("未查询到用户账户");
    }

    #endregion 用户账户

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
        var userAccountRole = await _IUserAccountRoleService.QueryUserAccountRoleAsync();
        if (userAccountRole.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RUserAccountRoleDto>>(userAccountRole));
        return ResultResponse.OK("未查询到用户账户角色");
    }

    #endregion 为用户账户分配角色
}