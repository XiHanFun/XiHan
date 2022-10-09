// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:JwtTokenTool
// Guid:df38addc-198d-4f69-aca1-5f3cc1c1c01b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-29 上午 02:32:25
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SqlSugar;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZhaiFanhuaBlog.Core.AppSettings;
using ZhaiFanhuaBlog.Utils.Object;

namespace ZhaiFanhuaBlog.Extensions.Common.Authorizations;

/// <summary>
/// JwtTokenTool
/// </summary>
public static class JwtTokenUtil
{
    /// <summary>
    /// 颁发JWT字符串
    /// </summary>
    /// <param name="tokenModel"></param>
    /// <returns></returns>
    public static string IssueJwtAccess(TokenModel tokenModel)
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
                new Claim("UserId", tokenModel.UserId.ToString()),
                new Claim("UserName", tokenModel.UserName??string.Empty),
                new Claim("NickName", tokenModel.NickName ?? string.Empty),
            };
            // 为了解决一个用户多个角色(比如：Admin,System)，用下边的方法
            List<string> RootRolesClaim = new(tokenModel.RootRoles.Split(','));
            claims.AddRange(RootRolesClaim.Select(role => new Claim("RootRole", role)));

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
    /// 颁发JWT字符串
    /// </summary>
    /// <param name="tokenModel"></param>
    /// <returns></returns>
    public static string IssueJwtRefresh(TokenModel tokenModel)
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
                new Claim("UserId", tokenModel.UserId.ToString()),
                new Claim("UserName", tokenModel.UserName??string.Empty),
                new Claim("NickName", tokenModel.NickName ?? string.Empty),
            };

            // 为了解决一个用户多个角色(比如：Admin,System)，用下边的方法
            List<string> RootRolesClaim = new(tokenModel.RootRoles.Split(','));
            claims.AddRange(RootRolesClaim.Select(role => new Claim("RootRole", role)));

            //用户标识
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identity.AddClaims(claims);

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
    /// <param name="token"></param>
    /// <returns></returns>
    public static TokenModel SerializeJwt(string token)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        TokenModel tokenModel = new();

        // 开始Token校验
        if (token.IsNotEmptyOrNull() && jwtHandler.CanReadToken(token))
        {
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);
            List<Claim> claims = jwtToken.Claims.ToList();

            // 分离参数
            Claim UserIdClaim = claims.FirstOrDefault(claim => claim.Type == "UserId")!;
            Claim UserNameClaim = claims.FirstOrDefault(claim => claim.Type == "UserName")!;
            Claim NickNameClaim = claims.FirstOrDefault(claim => claim.Type == "NickName")!;
            List<Claim> RootRolesClaim = claims.Where(claim => claim.Type == "RootRole").ToList();

            var userId = new Guid(UserIdClaim.Value);
            var userName = UserNameClaim.Value;
            var nickName = NickNameClaim.Value;
            List<string> rootRoles = RootRolesClaim.Select(c => c.Value).ToList();

            tokenModel = new TokenModel
            {
                UserId = userId,
                UserName = userName,
                NickName = nickName,
                RootRoles = string.Join(',', rootRoles),
            };
        }
        return tokenModel;
    }

    /// <summary>
    /// 安全验证Token，刷新Token用
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static bool SafeVerifyJwt(string token)
    {
        try
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var symmetricKey = AppSettings.Auth.JWT.SymmetricKey;
            // 秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            SymmetricSecurityKey signingKey = new(Encoding.UTF8.GetBytes(symmetricKey));
            SigningCredentials credentials = new(signingKey, SecurityAlgorithms.HmacSha256);
            // 读取旧token
            var jwt = jwtHandler.ReadJwtToken(token);
            return jwt.RawSignature == JwtTokenUtilities.CreateEncodedSignature(jwt.RawHeader + "." + jwt.RawPayload, credentials);
        }
        catch (Exception)
        {
            throw new ApplicationException("Token无效，请重新登录！");
        }
    }
}

/// <summary>
/// 令牌
/// </summary>
public class TokenModel
{
    /// <summary>
    /// 用户主键
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 用户昵称
    /// </summary>
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 用户角色
    /// </summary>
    public string RootRoles { get; set; } = string.Empty;
}