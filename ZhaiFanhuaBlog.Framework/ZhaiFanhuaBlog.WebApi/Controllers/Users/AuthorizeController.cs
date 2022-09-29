// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AuthorizeController
// Guid:92b337bd-3cfb-a825-5519-5568afeec06e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:47:21
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ZhaiFanhuaBlog.Extensions.Common.Authorizations;
using ZhaiFanhuaBlog.Extensions.Common.Swagger;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Utils.Encryptions;
using ZhaiFanhuaBlog.ViewModels.Bases.Results;
using ZhaiFanhuaBlog.ViewModels.Response;
using ZhaiFanhuaBlog.ViewModels.Users;
using ZhaiFanhuaBlog.WebApi.Controllers.Bases;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Users;

/// <summary>
/// 登录授权
/// <code>包含：JWT登录授权/第三方登录</code>
/// </summary>
[ApiExplorerSettings(GroupName = SwaggerGroup.Authorize)]
public class AuthorizeController : BaseApiController
{
    private readonly IHttpContextAccessor _IHttpContextAccessor;
    private readonly IUserAccountService _IUserAccountService;

    /// <summary>
    /// 构造函数注入
    /// </summary>
    /// <param name="iHttpContextAccessor"></param>
    /// <param name="iUserAccountService"></param>
    public AuthorizeController(IHttpContextAccessor iHttpContextAccessor, IUserAccountService iUserAccountService)
    {
        _IHttpContextAccessor = iHttpContextAccessor;
        _IUserAccountService = iUserAccountService;
    }

    /// <summary>
    /// 用户名称登录
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Login/AccountName")]
    public async Task<BaseResultDto> LoginByAccountName(CUserAccountLoginByNameDto cUserAccountLoginByNameDto)
    {
        // 根据用户名获取用户
        var userAccount = await _IUserAccountService.FindUserAccountByNameAsync(cUserAccountLoginByNameDto.Name);
        if (userAccount == null)
            throw new ApplicationException("该用户名账号不存在，请先注册账号");
        if (userAccount.Password != MD5Helper.EncryptMD5(Encoding.UTF8, cUserAccountLoginByNameDto.Password))
            throw new ApplicationException("密码错误，请重新登录");
        var token = JwtToken.IssueJwt(userAccount);
        // Swagger 登录
        _IHttpContextAccessor.HttpContext!.SigninToSwagger(token);
        return BaseResponseDto.OK(token);
    }

    /// <summary>
    /// 用户邮箱登录
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Login/AccountEmail")]
    public async Task<BaseResultDto> LoginByAccountEmail(CUserAccountLoginByEmailDto cUserAccountLoginByEmailDto)
    {
        // 根据邮箱获取用户
        var userAccount = await _IUserAccountService.FindUserAccountByEmailAsync(cUserAccountLoginByEmailDto.Email);
        if (userAccount == null)
            throw new ApplicationException("该邮箱账号不存在，请先注册账号");
        if (userAccount.Password != MD5Helper.EncryptMD5(Encoding.UTF8, cUserAccountLoginByEmailDto.Password))
            throw new ApplicationException("密码错误，请重新登录");
        var token = JwtToken.IssueJwt(userAccount);
        userAccount.LastLoginTime = DateTime.Now;
        // Swagger 登录
        _IHttpContextAccessor.HttpContext!.SigninToSwagger(token);
        return BaseResponseDto.OK(token);
    }

    /// <summary>
    /// 用户登出
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpPost("Logout")]
    public async Task<BaseResultDto> Logout()
    {
        // Swagger 登出
        _IHttpContextAccessor.HttpContext!.SignoutToSwagger();
        return await Task.FromResult(BaseResponseDto.Continue());
    }
}