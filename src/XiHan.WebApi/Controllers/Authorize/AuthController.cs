#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AuthController
// Guid:d8862f12-0e46-46cd-8278-099dc1dfce92
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-19 上午 11:20:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Models.Syses;
using XiHan.Services.Syses.Logging;
using XiHan.Services.Syses.Menus;
using XiHan.Services.Syses.Permissions;
using XiHan.Services.Syses.Roles;
using XiHan.Services.Syses.Users;
using XiHan.Services.Syses.Users.Dtos;
using XiHan.Utils.Encryptions;
using XiHan.Utils.Extensions;
using XiHan.WebApi.Controllers.Bases;
using XiHan.WebCore.Common.Swagger;
using XiHan.WebCore.Handlers;

namespace XiHan.WebApi.Controllers.Authorize;

/// <summary>
/// 系统登录授权管理
/// <code>包含：JWT登录授权/三方登录</code>
/// </summary>
[AllowAnonymous]
[ApiGroup(ApiGroupNames.Authorize)]
public class AuthController : BaseApiController
{
    private readonly ISysUserService _sysUserService;
    private readonly ISysRoleService _sysRoleService;
    private readonly ISysPermissionService _sysPermissionService;
    private readonly ISysMenuService _sysMenuService;
    private readonly ISysLogLoginService _sysLogLoginService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysUserService"></param>
    /// <param name="sysRoleService"></param>
    /// <param name="sysPermissionService"></param>
    /// <param name="sysMenuService"></param>
    /// <param name="sysLogLoginService"></param>
    public AuthController(ISysUserService sysUserService, ISysRoleService sysRoleService, ISysPermissionService sysPermissionService,
        ISysMenuService sysMenuService, ISysLogLoginService sysLogLoginService)
    {
        _sysUserService = sysUserService;
        _sysRoleService = sysRoleService;
        _sysPermissionService = sysPermissionService;
        _sysMenuService = sysMenuService;
        _sysLogLoginService = sysLogLoginService;
    }

    /// <summary>
    /// 获取 Token 通过账户
    /// </summary>
    /// <param name="loginByAccountCDto"></param>
    /// <returns></returns>
    [HttpPost("GetToken/ByAccount")]
    public async Task<CustomResult> GetTokenByAccount([FromBody] SysUserLoginByAccountCDto loginByAccountCDto)
    {
        var sysUser = await _sysUserService.GetUserByAccount(loginByAccountCDto.Account);
        return await GetToken(sysUser, loginByAccountCDto.Password);
    }

    /// <summary>
    /// 获取 Token 通过邮箱
    /// </summary>
    /// <param name="loginByEmailCDto"></param>
    /// <returns></returns>
    [HttpPost("GetToken/ByEmail")]
    public async Task<CustomResult> GetTokenByEmail([FromBody] SysUserLoginByEmailCDto loginByEmailCDto)
    {
        var sysUser = await _sysUserService.GetUserByEmail(loginByEmailCDto.Email);
        return await GetToken(sysUser, loginByEmailCDto.Password);
    }

    /// <summary>
    /// 获取 Token 并记录登录日志
    /// </summary>
    /// <param name="sysUser"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private async Task<CustomResult> GetToken(SysUser sysUser, string password)
    {
        var token = string.Empty;
        var sysLogLogin = new SysLogLogin();

        var clientInfo = App.ClientInfo;
        var addressInfo = App.AddressInfo;

        try
        {
            if (sysUser == null) throw new Exception("登录失败，用户不存在！");
            if (sysUser.Password != Md5EncryptionHelper.Encrypt(DesEncryptionHelper.Encrypt(password))) throw new Exception("登录失败，密码错误！");

            sysLogLogin.Status = true;
            sysLogLogin.Message = "登录成功！";
            sysLogLogin.Account = sysUser.Account;
            sysLogLogin.RealName = sysUser.RealName;

            var userRoleIds = await _sysRoleService.GetUserRoleIdsByUserId(sysUser.BaseId);

            TokenModel tokenModel = new()
            {
                UserId = sysUser.BaseId,
                UserName = sysUser.Account,
                NickName = sysUser.NickName,
                SysRoles = userRoleIds.Select(e => e.ToString()).ToArray().GetArrayStr()
            };
            token = JwtHandler.TokenIssue(tokenModel);
        }
        catch (Exception ex)
        {
            sysLogLogin.Status = false;
            sysLogLogin.Message = ex.Message;
        }
        sysLogLogin.LoginIp = clientInfo.RemoteIPv4;
        sysLogLogin.Location = addressInfo.Country + "|" + addressInfo.State + "|" + addressInfo.PrefectureLevelCity + "|" + addressInfo.DistrictOrCounty + "|" + addressInfo.Operator;
        sysLogLogin.Browser = clientInfo.BrowserName + clientInfo.BrowserVersion;
        sysLogLogin.OS = clientInfo.OSName + clientInfo.OSVersion;
        sysLogLogin.Agent = clientInfo.Agent;

        await _sysLogLoginService.AddAsync(sysLogLogin);

        if (sysLogLogin.Status) return CustomResult.Success(token);
        return CustomResult.BadRequest(sysLogLogin.Message);
    }
}