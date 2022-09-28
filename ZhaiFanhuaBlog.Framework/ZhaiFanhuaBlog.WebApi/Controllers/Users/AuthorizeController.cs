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
using ZhaiFanhuaBlog.Core.AppSettings;
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
/// <code>包含：登录授权</code>
/// </summary>
[AllowAnonymous]
[ApiExplorerSettings(GroupName = SwaggerGroup.Authorize)]
public class AuthorizeController : BaseApiController
{
    private readonly IConfiguration _IConfiguration;
    private readonly IUserAccountService _IUserAccountService;

    /// <summary>
    /// 构造函数注入
    /// </summary>
    /// <param name="iUserAccountService"></param>
    public AuthorizeController(IUserAccountService iUserAccountService)
    {
        _IConfiguration = AppConfig.Configuration!;
        _IUserAccountService = iUserAccountService;
    }

    /// <summary>
    /// 用户名获取Token
    /// </summary>
    /// <returns></returns>
    [HttpPost("Token/AccountName")]
    public async Task<BaseResultDto> GetTokenByAccountName(CUserAccountLoginByNameDto cUserAccountLoginByNameDto)
    {
        // 根据用户名获取用户
        var userAccount = await _IUserAccountService.FindUserAccountByNameAsync(cUserAccountLoginByNameDto.Name);
        if (userAccount == null)
            throw new ApplicationException("该用户名账号不存在，请先注册账号");
        if (userAccount.Password != MD5Helper.EncryptMD5(Encoding.UTF8, cUserAccountLoginByNameDto.Password))
            throw new ApplicationException("密码错误，请重新登录");
        return BaseResponseDto.OK(JwtToken.IssueJwt(userAccount));
    }

    /// <summary>
    /// 邮箱获取Token
    /// </summary>
    /// <returns></returns>
    [HttpPost("Token/AccountEmail")]
    public async Task<BaseResultDto> GetTokenByAccountEmail(CUserAccountLoginByEmailDto cUserAccountLoginByEmailDto)
    {
        // 根据邮箱获取用户
        var userAccount = await _IUserAccountService.FindUserAccountByEmailAsync(cUserAccountLoginByEmailDto.Email);
        if (userAccount == null)
            throw new ApplicationException("该邮箱账号不存在，请先注册账号");
        if (userAccount.Password != MD5Helper.EncryptMD5(Encoding.UTF8, cUserAccountLoginByEmailDto.Password))
            throw new ApplicationException("密码错误，请重新登录");
        return BaseResponseDto.OK(JwtToken.IssueJwt(userAccount));
    }
}