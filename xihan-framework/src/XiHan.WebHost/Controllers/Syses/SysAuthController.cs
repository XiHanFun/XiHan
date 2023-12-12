#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysAuthController
// Guid:d8862f12-0e46-46cd-8278-099dc1dfce92
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-19 上午 11:20:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHan.Infrastructures.Apps.HttpContexts;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Responses;
using XiHan.Models.Syses;
using XiHan.Models.Syses.Enums;
using XiHan.Services.Syses.Logging;
using XiHan.Services.Syses.Menus;
using XiHan.Services.Syses.Permissions;
using XiHan.Services.Syses.Roles;
using XiHan.Services.Syses.Users;
using XiHan.Services.Syses.Users.Dtos;
using XiHan.Utils.Encryptions;
using XiHan.WebCore.Common.Swagger;
using XiHan.WebCore.Handlers;

namespace XiHan.WebHost.Controllers.Syses;

/// <summary>
/// 系统登录授权管理
/// <code>包含：JWT登录授权</code>
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="sysUserService"></param>
/// <param name="sysRoleService"></param>
/// <param name="sysPermissionService"></param>
/// <param name="sysMenuService"></param>
/// <param name="sysLogLoginService"></param>
[AllowAnonymous]
[ApiGroup(ApiGroupNameEnum.Authorize)]
public class SysAuthController(ISysUserService sysUserService, ISysRoleService sysRoleService,
    ISysPermissionService sysPermissionService,
    ISysMenuService sysMenuService, ISysLogLoginService sysLogLoginService) : BaseApiController
{
    private readonly ISysUserService _sysUserService = sysUserService;
    private readonly ISysRoleService _sysRoleService = sysRoleService;
    private readonly ISysPermissionService _sysPermissionService = sysPermissionService;
    private readonly ISysMenuService _sysMenuService = sysMenuService;
    private readonly ISysLogLoginService _sysLogLoginService = sysLogLoginService;

    /// <summary>
    /// 登录(通过账户)
    /// </summary>
    /// <param name="loginByAccountCDto"></param>
    /// <returns></returns>
    [HttpPost("SignIn/ByAccount")]
    [AppLog(Module = "系统登录授权", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> SignInByAccount([FromBody] SysUserLoginByAccountCDto loginByAccountCDto)
    {
        var sysUser = await _sysUserService.GetUserByAccount(loginByAccountCDto.Account);
        return await GetTokenAndRecordLogLogin(sysUser, loginByAccountCDto.Password);
    }

    /// <summary>
    /// 登录(通过邮箱)
    /// </summary>
    /// <param name="loginByEmailCDto"></param>
    /// <returns></returns>
    [HttpPost("SignIn/ByEmail")]
    [AppLog(Module = "系统登录授权", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> SignInByEmail([FromBody] SysUserLoginByEmailCDto loginByEmailCDto)
    {
        var sysUser = await _sysUserService.GetUserByEmail(loginByEmailCDto.Email);
        return await GetTokenAndRecordLogLogin(sysUser, loginByEmailCDto.Password);
    }

    /// <summary>
    /// 注销
    /// </summary>
    /// <returns></returns>
    [HttpPost("SignOut")]
    [AppLog(Module = "系统登录授权", BusinessType = BusinessTypeEnum.Get)]
    public new async Task<ApiResult> SignOut()
    {
        await Task.Run(() => { HttpContext.SignoutToSwagger(); });
        return ApiResult.Continue();
    }

    /// <summary>
    /// 获取 Token 并记录登录日志
    /// </summary>
    /// <param name="sysUser"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private async Task<ApiResult> GetTokenAndRecordLogLogin(SysUser sysUser, string password)
    {
        var token = string.Empty;

        // 获取当前请求上下文信息
        var clientInfo = HttpContext.GetClientInfo();
        var addressInfo = HttpContext.GetAddressInfo();
        SysLogLogin sysLogLogin = new()
        {
            Ip = addressInfo.RemoteIPv4,
            Location = addressInfo.Country + "|" + addressInfo.State + "|" + addressInfo.PrefectureLevelCity + "|" +
                       addressInfo.DistrictOrCounty + "|" + addressInfo.Operator,
            Browser = clientInfo.BrowserName + clientInfo.BrowserVersion,
            Os = clientInfo.OsName + clientInfo.OsVersion,
            Agent = clientInfo.Agent
        };

        try
        {
            if (sysUser == null) throw new Exception("登录失败，用户不存在！");

            if (sysUser.Password != Md5HashEncryptionHelper.Encrypt(DesEncryptionHelper.Encrypt(password)))
                throw new Exception("登录失败，密码错误！");

            sysLogLogin.IsSuccess = true;
            sysLogLogin.Message = "登录成功！";
            sysLogLogin.Account = sysUser.Account;
            sysLogLogin.RealName = sysUser.RealName;
            sysLogLogin.Account = sysUser.Account;
            sysLogLogin.RealName = sysUser.RealName;

            var userRoleIds = await _sysRoleService.GetUserRoleIdsByUserId(sysUser.BaseId);
            token = JwtHandler.TokenIssue(new TokenModel()
            {
                UserId = sysUser.BaseId,
                Account = sysUser.Account,
                NickName = sysUser.NickName,
                UserRole = userRoleIds,
                IsSuperAdmin = sysUser.IsSuperAdmin()
            });
        }
        catch (Exception ex)
        {
            sysLogLogin.IsSuccess = false;
            sysLogLogin.Message = ex.Message;
        }

        // 记录登录日志
        _ = await _sysLogLoginService.AddAsync(sysLogLogin);

        // 验证成功就设置响应报文头，并返回 Token 令牌
        if (sysLogLogin.IsSuccess)
        {
            HttpContext.SigninToSwagger(token);
            return ApiResult.Success(token);
        }

        return ApiResult.BadRequest(sysLogLogin.Message);
    }
}