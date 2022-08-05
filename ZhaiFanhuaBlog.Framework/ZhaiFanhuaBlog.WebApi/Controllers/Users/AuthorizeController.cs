// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AuthorizeController
// Guid:92b337bd-3cfb-a825-5519-5568afeec06e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:47:21
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Bases.Response.Model;
using ZhaiFanhuaBlog.Models.Response;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Utils.Config;
using ZhaiFanhuaBlog.Utils.Encryptions;
using ZhaiFanhuaBlog.ViewModels.Users;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Users;

/// <summary>
/// 登录授权
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthorizeController : ControllerBase
{
    private readonly IConfiguration _IConfiguration;
    private readonly IUserAccountService _IUserAccountService;

    /// <summary>
    /// 构造函数注入
    /// </summary>
    /// <param name="iUserAccountService"></param>
    public AuthorizeController(IUserAccountService iUserAccountService)
    {
        _IConfiguration = ConfigHelper.Configuration!;
        _IUserAccountService = iUserAccountService;
    }

    /// <summary>
    /// 用户名获取Token
    /// </summary>
    /// <returns></returns>
    [HttpPost("Token/AccountName")]
    public async Task<ResultModel> GetTokenByAccountName(CUserAccountLoginByNameDto cUserAccountLoginByNameDto)
    {
        // 根据用户名获取用户
        var userAccount = await _IUserAccountService.FindUserAccountByNameAsync(cUserAccountLoginByNameDto.Name);
        if (userAccount == null)
            throw new ApplicationException("该用户名账号不存在，请先注册账号");
        if (userAccount.Password != MD5Helper.EncryptMD5(Encoding.UTF8, cUserAccountLoginByNameDto.Password))
            throw new ApplicationException("密码错误，请重新登录");
        return ResultResponse.OK(GetToken(userAccount));
    }

    /// <summary>
    /// 邮箱获取Token
    /// </summary>
    /// <returns></returns>
    [HttpPost("Token/AccountEmail")]
    public async Task<ResultModel> GetTokenByAccountEmail(CUserAccountLoginByEmailDto cUserAccountLoginByEmailDto)
    {
        // 根据邮箱获取用户
        var userAccount = await _IUserAccountService.FindUserAccountByEmailAsync(cUserAccountLoginByEmailDto.Email);
        if (userAccount == null)
            throw new ApplicationException("该邮箱账号不存在，请先注册账号");
        if (userAccount.Password != MD5Helper.EncryptMD5(Encoding.UTF8, cUserAccountLoginByEmailDto.Password))
            throw new ApplicationException("密码错误，请重新登录");
        return ResultResponse.OK(GetToken(userAccount));
    }

    /// <summary>
    /// 获取Token
    /// </summary>
    /// <param name="userAccount"></param>
    /// <returns></returns>
    private string GetToken(UserAccount userAccount)
    {
        try
        {
            var AccountClaims = new Claim[]{
                new Claim("UserId", userAccount.BaseId.ToString()),
                new Claim("UserName", userAccount.Name),
                new Claim("NickName", userAccount.NickName ?? userAccount.Name),
                //new Claim("RootRole", userAccount.RootRoles!.FirstOrDefault()!.Name!.ToString()??"")
            };
            // Nuget引入：Microsoft.IdentityModel.Tokens
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_IConfiguration.GetValue<string>("Auth:JWT:IssuerSigningKey")));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            // Nuget引入：System.IdentityModel.Tokens.Jwt
            JwtSecurityToken token = new(
                issuer: _IConfiguration["Configuration:Domain"],
                audience: _IConfiguration["Configuration:Domain"],
                claims: AccountClaims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(_IConfiguration.GetValue<int>("Auth:JWT:Expires")),
                signingCredentials: credentials
            );
            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
}