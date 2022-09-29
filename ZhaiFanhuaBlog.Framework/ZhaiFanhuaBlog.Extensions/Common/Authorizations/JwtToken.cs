// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:JwtToken
// Guid:df38addc-198d-4f69-aca1-5f3cc1c1c01b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-29 上午 02:32:25
// ----------------------------------------------------------------

using Microsoft.IdentityModel.Tokens;
using SqlSugar;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZhaiFanhuaBlog.Core.AppSettings;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Utils.Object;

namespace ZhaiFanhuaBlog.Extensions.Common.Authorizations;

/// <summary>
/// JwtToken
/// </summary>
public static class JwtToken
{
    /// <summary>
    /// 颁发JWT字符串
    /// </summary>
    /// <param name="tokenModel"></param>
    /// <returns></returns>
    public static string IssueJwt(UserAccount userAccount)
    {
        try
        {
            // 读取配置
            var issuer = AppSettings.Auth.JWT.Issuer;
            var audience = AppSettings.Auth.JWT.Audience;
            var symmetricKey = AppSettings.Auth.JWT.SymmetricKey;
            var expires = AppSettings.Auth.JWT.Expires;

            // Nuget引入：Microsoft.IdentityModel.Tokens
            var claims = new List<Claim>
            {
                new Claim("UserId", userAccount.BaseId.ToString()),
                new Claim("UserName", userAccount.Name),
                new Claim("NickName", userAccount.NickName ?? userAccount.Name),
            };
            // 为了解决一个用户多个角色(比如：Admin,System)，用下边的方法
            //claims.AddRange(userAccount.RootRoles!.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            // 秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            SymmetricSecurityKey signingKey = new(Encoding.UTF8.GetBytes(symmetricKey));
            SigningCredentials credentials = new(signingKey, SecurityAlgorithms.HmacSha256);

            // Nuget引入：System.IdentityModel.Tokens.Jwt
            JwtSecurityToken securityToken = new(
                // 自定义选项
                claims: claims,
                // 颁发者
                issuer: issuer,
                // 签收者
                audience: audience,
                // 秘钥
                signingCredentials: credentials,
                // 生效时间
                notBefore: DateTime.Now,
                // 过期时间
                expires: DateTime.Now.AddMinutes(expires)
            );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return accessToken;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// 解析JWT字符串
    /// </summary>
    /// <param name="jwtStr"></param>
    /// <returns></returns>
    public static UserAccount SerializeJwt(string jwtStr)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        UserAccount userAccount = new();

        // 开始Token校验
        if (jwtStr.IsNotEmptyOrNull() && jwtHandler.CanReadToken(jwtStr))
        {
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            List<Claim> claims = jwtToken.Claims.ToList();
            string userid = claims.Where(claim => claim.Type == "UserId").Select(claim => claim.Value).FirstOrDefault()!;
            List<string> roleName = claims.Where(claim => claim.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            userAccount = new UserAccount
            {
                BaseId = new Guid(userid),
                RootRoles = new List<RootRole>(),
            };
        }
        return userAccount;
    }
}