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
using XiHan.Application.Common.Swagger;
using XiHan.Application.Handlers;
using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Apps.HttpContexts;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Models.Syses;
using XiHan.Services.Syses.Logins;
using XiHan.Services.Syses.Menus;
using XiHan.Services.Syses.Permissions;
using XiHan.Services.Syses.Roles;
using XiHan.Services.Syses.Users;
using XiHan.Services.Syses.Users.Dtos;
using XiHan.Utils.Encryptions;
using XiHan.Utils.Extensions;
using XiHan.WebApi.Controllers.Bases;

namespace XiHan.WebApi.Controllers.Authorize;

/// <summary>
/// 系统登录授权管理
/// <code>包含：JWT登录授权/三方登录</code>
/// </summary>
[AllowAnonymous]
[ApiGroup(ApiGroupNames.Authorize)]
public class AuthController : BaseApiController
{
    private static string _secretKey = AppSettings.Syses.Domain.GetValue();
    private readonly ISysUserService _sysUserService;
    private readonly ISysRoleService _sysRoleService;
    private readonly ISysPermissionService _sysPermissionService;
    private readonly ISysMenuService _sysMenuService;
    private readonly ISysLoginLogService _sysLoginLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysUserService"></param>
    /// <param name="sysRoleService"></param>
    /// <param name="sysPermissionService"></param>
    /// <param name="sysMenuService"></param>
    /// <param name="sysLoginLogService"></param>
    public AuthController(ISysUserService sysUserService, ISysRoleService sysRoleService, ISysPermissionService sysPermissionService,
        ISysMenuService sysMenuService, ISysLoginLogService sysLoginLogService)
    {
        _sysUserService = sysUserService;
        _sysRoleService = sysRoleService;
        _sysPermissionService = sysPermissionService;
        _sysMenuService = sysMenuService;
        _sysLoginLogService = sysLoginLogService;
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
        var sysLoginLog = new SysLoginLog();

        var contextInfo = new HttpContextInfoHelper(App.HttpContext);
        var clientInfo = contextInfo.ClientInfo;
        var addressInfo = contextInfo.AddressInfo;

        try
        {
            if (sysUser == null) throw new Exception("登录失败，用户不存在！");
            if (sysUser.Password != Md5EncryptionHelper.Encrypt(AesEncryptionHelper.Encrypt(password, _secretKey))) throw new Exception("登录失败，密码错误！");

            sysLoginLog.Status = true;
            sysLoginLog.Message = "登录成功！";
            sysLoginLog.Account = sysUser.Account;
            sysLoginLog.RealName = sysUser.RealName;

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
            sysLoginLog.Status = false;
            sysLoginLog.Message = ex.Message;
        }
        sysLoginLog.LoginIp = clientInfo.RemoteIPv4;
        sysLoginLog.Location = addressInfo.Country + "|" + addressInfo.State + "|" + addressInfo.PrefectureLevelCity + "|" + addressInfo.DistrictOrCounty + "|" + addressInfo.Operator;
        sysLoginLog.Browser = clientInfo.UaName;
        sysLoginLog.OsName = clientInfo.OsName;
        sysLoginLog.Agent = clientInfo.Agent;

        await _sysLoginLogService.AddAsync(sysLoginLog);

        if (sysLoginLog.Status) return CustomResult.Success(token);
        return CustomResult.BadRequest(sysLoginLog.Message);
    }
}