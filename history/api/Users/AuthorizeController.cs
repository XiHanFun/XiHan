#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AuthorizeController
// Guid:92b337bd-3cfb-a825-5519-5568afeec06e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:47:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZhaiFanhuaBlog.Api.Controllers.Bases;
using ZhaiFanhuaBlog.Extensions.Common.Auth;
using ZhaiFanhuaBlog.Extensions.Common.Swagger;
using ZhaiFanhuaBlog.Services.Users;
using ZhaiFanhuaBlog.Utils.Encryptions;
using ZhaiFanhuaBlog.ViewModels.Bases.Results;
using ZhaiFanhuaBlog.ViewModels.Response;
using ZhaiFanhuaBlog.ViewModels.Users;

namespace ZhaiFanhuaBlog.Api.Controllers.Users;

/// <summary>
/// 登录授权
/// <code>包含：JWT登录授权/第三方登录</code>
/// </summary>
[ApiGroup(ApiGroupNames.Authorize)]
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
    /// 用户名称登录获取Token
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [ApiGroup(ApiGroupNames.Reception)]
    [HttpPost("Login/Token/Name")]
    public async Task<BaseResultDto> GetTokenByName([FromServices] IMapper iMapper, CUserAccountLoginByNameDto cUserAccountLoginByNameDto)
    {
        // 根据用户名获取用户
        var userAccount = await _IUserAccountService.FindUserAccountByNameAsync(cUserAccountLoginByNameDto.UserName);
        if (userAccount == null)
        {
            throw new ApplicationException("该用户名称账号不存在，请先注册账号");
        }
        if (userAccount.Password != cUserAccountLoginByNameDto.Password.ToMD5())
        {
            throw new ApplicationException("密码错误，请重新登录");
        }
        var userAccountDto = iMapper.Map<RUserAccountDto>(userAccount);
        var tokenModel = iMapper.Map<TokenModel>(userAccountDto);
        var token = JwtTokenUtil.JwtIssue(tokenModel);
        userAccount.LastLoginTime = DateTime.Now;
        await _IUserAccountService.UpdateAsync(userAccount);
        return BaseResponseDto.OK(token);
    }

    /// <summary>
    /// 用户邮箱登录获取Token
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [ApiGroup(ApiGroupNames.Reception)]
    [HttpPost("Login/Token/Email")]
    public async Task<BaseResultDto> GetTokenByEmail([FromServices] IMapper iMapper, CUserAccountLoginByEmailDto cUserAccountLoginByEmailDto)
    {
        // 根据邮箱获取用户
        var userAccount = await _IUserAccountService.FindUserAccountByEmailAsync(cUserAccountLoginByEmailDto.UserEmail);
        if (userAccount == null)
        {
            throw new ApplicationException("该邮箱账号不存在，请先注册账号");
        }
        if (userAccount.Password != cUserAccountLoginByEmailDto.Password.ToMD5())
        {
            throw new ApplicationException("密码错误，请重新登录");
        }
        var userAccountDto = iMapper.Map<RUserAccountDto>(userAccount);
        var tokenModel = iMapper.Map<TokenModel>(userAccountDto);
        var token = JwtTokenUtil.JwtIssue(tokenModel);
        userAccount.LastLoginTime = DateTime.Now;
        await _IUserAccountService.UpdateAsync(userAccount);
        return BaseResponseDto.OK(token);
    }

    /// <summary>
    /// 刷新Token
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [ApiGroup(ApiGroupNames.Reception)]
    [HttpPost("Login/Token/Refresh")]
    public async Task<BaseResultDto> GetTokenByRefresh([FromServices] IMapper iMapper, string token)
    {
        if (JwtTokenUtil.JwtTokenSafeVerify(token))
        {
            // 获取原用户信息
            var tokenModel = JwtTokenUtil.JwtSerialize(token);
            if (tokenModel != null)
            {
                var userAccountRefresh = await _IUserAccountService.FindUserAccountByGuidAsync(tokenModel.UserId);
                var userAccountDtoRefresh = iMapper.Map<RUserAccountDto>(userAccountRefresh);
                var tokenModelRefresh = iMapper.Map<TokenModel>(userAccountDtoRefresh);
                var tokenRefresh = JwtTokenUtil.JwtIssue(tokenModelRefresh);
                return BaseResponseDto.OK(tokenRefresh);
            }
        }
        return BaseResponseDto.UnprocessableEntity("Token不合法");
    }

    /// <summary>
    /// 用户登出
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [ApiGroup(ApiGroupNames.Reception)]
    [HttpPost("Logout")]
    public async Task<BaseResultDto> Logout()
    {
        return await Task.FromResult(BaseResponseDto.Continue());
    }
}