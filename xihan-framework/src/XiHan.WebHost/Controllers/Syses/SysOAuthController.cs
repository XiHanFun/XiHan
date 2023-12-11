#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysOAuthController
// Guid:be5e08e3-d0bd-4a4d-99c4-ccab666281ea
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/29 21:43:13
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using XiHan.Services.Syses.Logging;
using XiHan.Services.Syses.Roles;
using XiHan.Services.Syses.Users;
using XiHan.WebCore.Common.Swagger;

namespace XiHan.WebHost.Controllers.Syses;

/// <summary>
/// 系统三方登录管理
/// <code>包含：三方登录</code>
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="sysUserService"></param>
/// <param name="sysRoleService"></param>
/// <param name="sysLogLoginService"></param>
[AllowAnonymous]
[ApiGroup(ApiGroupNameEnum.Authorize)]
public class SysOAuthController(ISysUserService sysUserService, ISysRoleService sysRoleService,
    ISysLogLoginService sysLogLoginService) : BaseApiController
{
    private readonly ISysUserService _sysUserService = sysUserService;
    private readonly ISysRoleService _sysRoleService = sysRoleService;
    private readonly ISysLogLoginService _sysLogLoginService = sysLogLoginService;

    ///// <summary>
    ///// 三方登录
    ///// </summary>
    ///// <param name="loginByAccountCDto"></param>
    ///// <returns></returns>
    //[HttpPost("SignIn")]
    //[AppLog(Module = "系统登录授权", BusinessType = BusinessTypeEnum.Get)]
    //public async Task<ApiResult> SignIn([FromBody] SysUserLoginByAccountCDto loginByAccountCDto)
    //{
    //    SysUser sysUser = await _sysUserService.GetUserByAccount(loginByAccountCDto.Account);
    //    return await GetTokenAndRecordLogLogin(sysUser, loginByAccountCDto.Password);
    //}

    ///// <summary>
    ///// 获取 Token 并记录登录日志
    ///// </summary>
    ///// <param name="sysUser"></param>
    ///// <param name="password"></param>
    ///// <returns></returns>
    //private async Task<ApiResult> GetTokenAndRecordLogLogin(SysUser sysUser, string password)
    //{
    //    string token = string.Empty;

    //    // 获取当前请求上下文信息
    //    Infrastructures.Apps.HttpContexts.UserClientInfo clientInfo = App.ClientInfo;
    //    Infrastructures.Apps.HttpContexts.UserAddressInfo addressInfo = App.AddressInfo;
    //    SysLogLogin sysLogLogin = new()
    //    {
    //        Ip = addressInfo.RemoteIPv4,
    //        Location = addressInfo.Country + "|" + addressInfo.State + "|" + addressInfo.PrefectureLevelCity + "|" + addressInfo.DistrictOrCounty + "|" + addressInfo.Operator,
    //        Browser = clientInfo.BrowserName + clientInfo.BrowserVersion,
    //        Os = clientInfo.OsName + clientInfo.OsVersion,
    //        Agent = clientInfo.Agent
    //    };

    //    try
    //    {
    //        if (sysUser == null)
    //        {
    //            throw new Exception("登录失败，用户不存在！");
    //        }

    //        if (sysUser.Password != Md5HashEncryptionHelper.Encrypt(DesEncryptionHelper.Encrypt(password)))
    //        {
    //            throw new Exception("登录失败，密码错误！");
    //        }

    //        sysLogLogin.IsSuccess = true;
    //        sysLogLogin.Message = "登录成功！";
    //        sysLogLogin.Account = sysUser.Account;
    //        sysLogLogin.RealName = sysUser.RealName;
    //        sysLogLogin.Account = sysUser.Account;
    //        sysLogLogin.RealName = sysUser.RealName;

    //        List<long> userRoleIds = await _sysRoleService.GetUserRoleIdsByUserId(sysUser.BaseId);
    //        token = JwtHandler.TokenIssue(new TokenModel()
    //        {
    //            UserId = sysUser.BaseId,
    //            Account = sysUser.Account,
    //            NickName = sysUser.NickName,
    //            UserRole = userRoleIds,
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        sysLogLogin.IsSuccess = false;
    //        sysLogLogin.Message = ex.Message;
    //    }

    //    _ = await _sysLogLoginService.AddAsync(sysLogLogin);
    //    return sysLogLogin.IsSuccess ? ApiResult.Success(token) : ApiResult.BadRequest(sysLogLogin.Message);
    //}
}