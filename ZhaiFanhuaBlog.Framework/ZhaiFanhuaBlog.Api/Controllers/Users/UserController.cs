#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserController
// Guid:03069dd5-18ca-4109-b7da-4691b785bd11
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-15 下午 05:38:40
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZhaiFanhuaBlog.Api.Controllers.Bases;
using ZhaiFanhuaBlog.Extensions.Common.Swagger;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Users;
using ZhaiFanhuaBlog.Utils.Encryptions;
using ZhaiFanhuaBlog.ViewModels.Bases.Pages;
using ZhaiFanhuaBlog.ViewModels.Bases.Results;
using ZhaiFanhuaBlog.ViewModels.Response;
using ZhaiFanhuaBlog.ViewModels.Users;

namespace ZhaiFanhuaBlog.Api.Controllers.Users;

/// <summary>
/// 用户管理
/// <code>包含：账户/收藏/关注/通知/统计</code>
/// </summary>
[Authorize]
[ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
public class UserController : BaseApiController
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
    [HttpPost("Account")]
    public async Task<BaseResultDto> CreateUserAccount([FromServices] IMapper iMapper, [FromBody] CUserAccountDto cUserAccountDto)
    {
        var userAccount = iMapper.Map<UserAccount>(cUserAccountDto);
        // 密码加密
        userAccount.Password = cUserAccountDto.Password.ToMD5();
        userAccount.RegisterIp = _IHttpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        if (await _IUserAccountService.CreateUserAccountAsync(userAccount))
            return BaseResponseDto.OK("新增用户账户成功");
        return BaseResponseDto.BadRequest("新增用户账户失败");
    }

    /// <summary>
    /// 删除用户账户
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Account/{guid}")]
    public async Task<BaseResultDto> DeleteUserAccount([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        Guid deleteId = Guid.Parse(user);
        if (await _IUserAccountService.DeleteUserAccountAsync(guid, deleteId))
            return BaseResponseDto.OK("删除用户账户成功");
        return BaseResponseDto.BadRequest("删除用户账户失败");
    }

    /// <summary>
    /// 修改用户账户
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cUserAccountDto"></param>
    /// <returns></returns>
    [HttpPut("Account")]
    public async Task<BaseResultDto> ModifyUserAccount([FromServices] IMapper iMapper, [FromBody] CUserAccountDto cUserAccountDto)
    {
        var user = User.FindFirstValue("UserId");
        var userAccount = iMapper.Map<UserAccount>(cUserAccountDto);
        // 密码加密
        userAccount.Password = cUserAccountDto.Password.ToMD5();
        userAccount.ModifyId = Guid.Parse(user);
        userAccount = await _IUserAccountService.ModifyUserAccountAsync(userAccount);
        if (userAccount != null)
            return BaseResponseDto.OK(iMapper.Map<RUserAccountDto>(userAccount));
        return BaseResponseDto.BadRequest("修改用户账户失败");
    }

    /// <summary>
    /// 查找用户账户
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Account/{guid}")]
    public async Task<BaseResultDto> FindUserAccount([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");

        //var auser = JwtTokenTool.SerializeJwt(_IHttpContextAccessor.HttpContext!.Request.Headers["Authorization"].ToString()["Bearer ".Length..].Trim());

        var userAccount = await _IUserAccountService.FindUserAccountByGuidAsync(guid);
        if (userAccount != null)
            return BaseResponseDto.OK(iMapper.Map<RUserAccountDto>(userAccount));
        return BaseResponseDto.BadRequest("该用户账户不存在");
    }

    /// <summary>
    /// 查询用户账户
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="pageDto"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Accounts")]
    public async Task<BaseResultDto> QueryUserAccounts([FromServices] IMapper iMapper, [FromBody] BasePageDto pageDto)
    {
        var userAccount = await _IUserAccountService.QueryPageDataDtoAsync(pageDto);
        return BaseResponseDto.OK(userAccount);
        //if (userAccount.Datas != null)
        //{
        //    var result = iMapper.Map<List<RUserAccountDto>>(userAccount.Datas);
        //    return BaseResponseDto.OK(result);
        //}
        //return BaseResponseDto.BadRequest("未查询到用户账户");
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
    public async Task<BaseResultDto> CreateUserAccountRole([FromServices] IMapper iMapper, [FromBody] CUserAccountRoleDto cUserAccountRoleDto)
    {
        var user = User.FindFirstValue("UserId");
        var userAccountRole = iMapper.Map<UserAccountRole>(cUserAccountRoleDto);
        userAccountRole.CreateId = Guid.Parse(user);
        if (await _IUserAccountRoleService.CreateUserAccountRoleAsync(userAccountRole))
            return BaseResponseDto.OK("新增用户账户角色成功");
        return BaseResponseDto.BadRequest("新增用户账户角色失败");
    }

    /// <summary>
    /// 删除用户账户角色
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Account/Role/{guid}")]
    public async Task<BaseResultDto> DeleteUserAccountRole([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        Guid deleteId = Guid.Parse(user);
        if (await _IUserAccountRoleService.DeleteUserAccountRoleAsync(guid, deleteId))
            return BaseResponseDto.OK("删除用户账户角色成功");
        return BaseResponseDto.BadRequest("删除用户账户角色失败");
    }

    /// <summary>
    /// 修改用户账户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cUserAccountRoleDto"></param>
    /// <returns></returns>
    [HttpPut("Account/Role")]
    public async Task<BaseResultDto> ModifyUserAccountRole([FromServices] IMapper iMapper, [FromBody] CUserAccountRoleDto cUserAccountRoleDto)
    {
        var user = User.FindFirstValue("UserId");
        var userAccountRole = iMapper.Map<UserAccountRole>(cUserAccountRoleDto);
        userAccountRole.ModifyId = Guid.Parse(user);
        userAccountRole = await _IUserAccountRoleService.ModifyUserAccountRoleAsync(userAccountRole);
        if (userAccountRole != null)
            return BaseResponseDto.OK(iMapper.Map<RUserAccountRoleDto>(userAccountRole));
        return BaseResponseDto.BadRequest("修改用户账户角色失败");
    }

    /// <summary>
    /// 查找用户账户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Account/Role/{guid}")]
    public async Task<BaseResultDto> FindUserAccountRole([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        var userAccountRole = await _IUserAccountRoleService.FindUserAccountRoleAsync(guid);
        if (userAccountRole != null)
            return BaseResponseDto.OK(iMapper.Map<RUserAccountRoleDto>(userAccountRole));
        return BaseResponseDto.BadRequest("该用户账户角色不存在");
    }

    /// <summary>
    /// 查询用户账户角色
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Account/Roles")]
    public async Task<BaseResultDto> QueryUserAccountRoles([FromServices] IMapper iMapper)
    {
        var userAccountRole = await _IUserAccountRoleService.QueryUserAccountRoleAsync();
        if (userAccountRole.Count != 0)
            return BaseResponseDto.OK(iMapper.Map<List<RUserAccountRoleDto>>(userAccountRole));
        return BaseResponseDto.BadRequest("未查询到用户账户角色");
    }

    #endregion 为用户账户分配角色
}