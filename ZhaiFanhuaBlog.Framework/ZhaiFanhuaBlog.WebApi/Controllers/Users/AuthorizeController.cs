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
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Utils.Encryptions;
using ZhaiFanhuaBlog.ViewModels.Users;
using ZhaiFanhuaBlog.WebApi.Common.Extensions.Swagger;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Users;

/// <summary>
/// 登录授权
/// </summary>
[AllowAnonymous]
[Route("api/[controller]"), Produces("application/json")]
[ApiController, ApiExplorerSettings(GroupName = SwaggerGroup.Authorize)]
public class AuthorizeController : ControllerBase
{
    private readonly IConfiguration _IConfiguration;
    private readonly IUserAccountService _IUserAccountService;

    /// <summary>
    /// 构造函数注入
    /// </summary>
    /// <param name="iCconfiguration"></param>
    /// <param name="iUserAccountService"></param>
    public AuthorizeController(IConfiguration iCconfiguration,
        IUserAccountService iUserAccountService)
    {
        _IConfiguration = iCconfiguration;
        _IUserAccountService = iUserAccountService;
    }

    /// <summary>
    /// 用户名获取Token
    /// </summary>
    /// <returns></returns>
    [HttpPost("Token/AccountName")]
    public async Task<string> GetTokenByAccountName(CUserAccountLoginByNameDto cDto)
    {
        // 根据用户名获取用户
        var userAccount = await _IUserAccountService.FindAsync(u => u.Name == cDto.Name);
        if (userAccount == null)
            throw new Exception("该用户名账号不存在，请先注册账号");
        if (userAccount.Password != MD5Helper.EncryptMD5(Encoding.UTF8, cDto.Password))
            throw new Exception("密码错误，请重新登录");
        return GetToken(userAccount);
    }

    /// <summary>
    /// 邮箱获取Token
    /// </summary>
    /// <returns></returns>
    [HttpPost("Token/AccountEmail")]
    public async Task<string> GetTokenByAccountEmail(CUserAccountLoginByEmailDto cDto)
    {
        // 根据邮箱获取用户
        var userAccount = await _IUserAccountService.FindAsync(u => u.Name == cDto.Email);
        if (userAccount == null)
            throw new Exception("该邮箱账号不存在，请先注册账号");
        if (userAccount.Password != MD5Helper.EncryptMD5(Encoding.UTF8, cDto.Password))
            throw new Exception("密码错误，请重新登录");
        return GetToken(userAccount);
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
                //new Claim("UserRole", userAccount.UserRoles!.FirstOrDefault()!.Name!.ToString()??"")
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_IConfiguration["Auth:JWT:IssuerSigningKey"]));
            var token = new JwtSecurityToken(
                issuer: _IConfiguration["Configuration:Domain"],
                audience: _IConfiguration["Configuration:Domain"],
                claims: AccountClaims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(_IConfiguration.GetValue<int>("Auth:JWT:Expires")),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
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