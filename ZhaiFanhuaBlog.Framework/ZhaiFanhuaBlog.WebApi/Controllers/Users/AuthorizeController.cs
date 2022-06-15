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
using ZhaiFanhuaBlog.Utils.Encryptions;
using ZhaiFanhuaBlog.ViewModels.Response;
using ZhaiFanhuaBlog.ViewModels.Response.Model;

namespace ZhaiFanhuaBlog.WebApi.Controllers;

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
    /// 构造函数注入32000001
47/    /// </summary>

    /// <param name="configuration"></param>
    /// <param name="userAccountService"></param>
    public AuthorizeController(IConfiguration configuration, IUserAccountService userAccountService)
    {
        _IConfiguration = configuration;
        _IUserAccountService = userAccountService;
    }

    /// <summary>
    /// 用户名获取 Token
    /// </summary>
    /// <param name="accountName"></param>
    /// <param name="accountPassword"></param>
    /// <returns></returns>
    [HttpPost("Get/Token/{AccountName}/{AccountPassword}")]
    public async Task<MessageModel> GetTokenByAccountName(string accountName, string accountPassword)
    {
        try
        {
            if (string.IsNullOrEmpty(accountName) || string.IsNullOrEmpty(accountPassword))
                throw new Exception("请输入账号或密码!");
            if (accountName.Length > 10 || accountPassword.Length > 64)
                throw new Exception("账号或密码长度不匹配!");
            // 获取用户
            var account = await _IUserAccountService.FindAsync(u => u.Name == accountName);
            if (account == null)
                throw new Exception("账号不存在，请先注册账号!");
            if (account.Password != MD5Helper.EncryptMD5(Encoding.UTF8, accountPassword))
                throw new Exception("密码错误，请重新登录!");
            var AccountClaims = new Claim[]{
                new Claim(ClaimTypes.NameIdentifier, account.BaseId.ToString()),
                new Claim(ClaimTypes.Name, account.Name??""),
               // new Claim(ClaimTypes.Role, account.Role)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_IConfiguration["Auth:JWT:IssuerSigningKey"]));
            var token = new JwtSecurityToken(
                issuer: _IConfiguration["Auth:JWT:SiteDomain"],
                audience: _IConfiguration["Auth:JWT:SiteDomain"],
                claims: AccountClaims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(_IConfiguration.GetValue<int>("Auth:JWT:Expires")),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );
            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return ResultResponse.OK(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}