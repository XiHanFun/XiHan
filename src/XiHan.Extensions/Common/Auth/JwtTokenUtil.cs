#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:JwtTokenUtil
// Guid:df38addc-198d-4f69-aca1-5f3cc1c1c01b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-29 上午 02:32:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XiHan.Infrastructure.Apps.Setting;
using XiHan.Utils.Consoles;
using XiHan.Utils.Objects;

namespace XiHan.Extensions.Common.Auth;

/// <summary>
/// JwtTokenUtil
/// </summary>
public static class JwtTokenUtil
{
    /// <summary>
    /// Token颁发
    /// </summary>
    /// <param name="tokenModel"></param>
    /// <returns></returns>
    public static string TokenIssue(TokenModel tokenModel)
    {
        AuthJwtSetting authJwtSetting = GetAuthJwtSetting();

        // Nuget引入：Microsoft.IdentityModel.Tokens
        var claims = new List<Claim>
            {
                new Claim("UserId", tokenModel.UserId.ToString()),
                new Claim("UserName", tokenModel.UserName),
                new Claim("NickName", tokenModel.NickName),
                new Claim("Issuer", authJwtSetting.Issuer),
                new Claim("Audience", authJwtSetting.Audience),
            };
        // 为了解决一个用户多个角色(比如：Admin,System)，用下边的方法
        List<string> sysRolesClaim = new(tokenModel.SysRoles.Split(','));
        claims.AddRange(sysRolesClaim.Select(role => new Claim("SysRole", role)));

        // 秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authJwtSetting.SymmetricKey));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature);

        // Nuget引入：System.IdentityModel.Tokens.Jwt
        JwtSecurityToken securityToken = new(
            // 自定义选项
            claims: claims,
            // 颁发者
            issuer: authJwtSetting.Issuer,
            // 签收者
            audience: authJwtSetting.Audience,
            // 秘钥
            signingCredentials: credentials,
            // 生效时间
            notBefore: DateTime.UtcNow,
            // 过期时间
            expires: DateTime.UtcNow.AddMinutes(authJwtSetting.Expires)
        );
        var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return accessToken;
    }

    /// <summary>
    /// Token安全验证，刷新Token用
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static bool TokenIsSafeVerify(string token)
    {
        AuthJwtSetting authJwtSetting = GetAuthJwtSetting();
        var jwtHandler = new JwtSecurityTokenHandler();
        var symmetricKey = authJwtSetting.SymmetricKey;

        // 秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricKey));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature);
        try
        {
            token = token.ParseToString().Replace("Bearer ", "");
            // 读取旧token
            var jwtToken = jwtHandler.ReadJwtToken(token);
            var verifyResult = jwtToken.RawSignature == JwtTokenUtilities.CreateEncodedSignature(jwtToken.RawHeader + "." + jwtToken.RawPayload, credentials);
            return verifyResult;
        }
        catch (Exception ex)
        {
            var errorMsg = $"Token 被篡改或无效，无法通过安全验证！";
            Log.Error(ex, errorMsg);
            errorMsg.WriteLineError();
            return false;
        }
    }

    /// <summary>
    /// Token验证
    /// </summary>
    /// <returns></returns>
    public static TokenValidationParameters TokenVerify()
    {
        AuthJwtSetting authJwtSetting = GetAuthJwtSetting();
        // 签名密钥
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authJwtSetting.SymmetricKey));
        // 令牌验证参数
        var tokenValidationParameters = new TokenValidationParameters
        {
            // 是否验证签名
            ValidateIssuerSigningKey = true,
            // 签名
            IssuerSigningKey = signingKey,
            //是否验证颁发者
            ValidateIssuer = true,
            // 颁发者
            ValidIssuer = authJwtSetting.Issuer,
            // 是否验证签收者
            ValidateAudience = true,
            // 签收者
            ValidAudience = authJwtSetting.Audience,
            // 是否验证失效时间
            ValidateLifetime = true,
            // 过期时间容错值,单位为秒,若为0，过期时间一到立即失效
            ClockSkew = TimeSpan.FromSeconds(authJwtSetting.ClockSkew),
            // 需要过期时间
            RequireExpirationTime = true,
        };
        return tokenValidationParameters;
    }

    /// <summary>
    /// Token解析
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static TokenModel TokenSerialize(string token)
    {
        try
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            token = token.ParseToString().Replace("Bearer ", "");

            // 开始Token校验
            if (token.IsEmptyOrNull() || !jwtHandler.CanReadToken(token))
            {
                throw new ArgumentException("token为空或无法解析！", nameof(token));
            }

            var jwtToken = jwtHandler.ReadJwtToken(token);
            List<Claim> claims = jwtToken.Claims.ToList();

            // 分离参数
            var userIdClaim = claims.FirstOrDefault(claim => claim.Type == "UserId");
            var userNameClaim = claims.FirstOrDefault(claim => claim.Type == "UserName");
            var nickNameClaim = claims.FirstOrDefault(claim => claim.Type == "NickName");
            var sysRolesClaim = claims.Where(claim => claim.Type == "SysRole").ToList();

            var userId = userIdClaim!.Value.ParseToGuid();
            var userName = userNameClaim!.Value;
            var nickName = nickNameClaim!.Value;
            var sysRoles = sysRolesClaim.Select(c => c.Value).ToList();

            var tokenModel = new TokenModel
            {
                UserId = userId,
                UserName = userName,
                NickName = nickName,
                SysRoles = string.Join(',', sysRoles),
            };
            return tokenModel;
        }
        catch (Exception ex)
        {
            var errorMsg = $"JwtToken 字符串解析失败";
            Log.Error(ex, errorMsg);
            errorMsg.WriteLineError();
            throw;
        }
    }

    /// <summary>
    /// 获取 AuthJwt 配置
    /// </summary>
    /// <returns></returns>
    public static AuthJwtSetting GetAuthJwtSetting()
    {
        try
        {
            // 读取配置
            var authJwtSetting = new AuthJwtSetting
            {
                Issuer = AppSettings.Auth.Jwt.Issuer.GetValue(),
                Audience = AppSettings.Auth.Jwt.Audience.GetValue(),
                SymmetricKey = AppSettings.Auth.Jwt.SymmetricKey.GetValue(),
                ClockSkew = AppSettings.Auth.Jwt.ClockSkew.GetValue(),
                Expires = AppSettings.Auth.Jwt.Expires.GetValue(),
            };
            // 判断结果
            authJwtSetting.GetPropertyInfos().ForEach(setting =>
            {
                if (setting.PropertyValue.IsNullOrZero()) throw new ArgumentNullException(nameof(setting.PropertyName));
            });
            return authJwtSetting;
        }
        catch (Exception ex)
        {
            var errorMsg = $"获取 AppSettings.Auth.Jwt 配置出错！";
            Log.Error(ex, errorMsg);
            errorMsg.WriteLineError();
            throw;
        }
    }
}

/// <summary>
/// AuthJwt 配置
/// </summary>
public class AuthJwtSetting
{
    /// <summary>
    /// 颁发者
    /// </summary>
    public string Issuer { get; init; } = string.Empty;

    /// <summary>
    /// 签收者
    /// </summary>
    public string Audience { get; init; } = string.Empty;

    /// <summary>
    /// 秘钥
    /// </summary>
    public string SymmetricKey { get; init; } = string.Empty;

    /// <summary>
    /// 过期时间容错值
    /// </summary>
    public int ClockSkew { get; init; }

    /// <summary>
    /// 过期时间
    /// </summary>
    public int Expires { get; init; }
}

/// <summary>
/// Token 模型
/// </summary>
public class TokenModel
{
    /// <summary>
    /// 用户主键
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; init; } = string.Empty;

    /// <summary>
    /// 用户昵称
    /// </summary>
    public string NickName { get; init; } = string.Empty;

    /// <summary>
    /// 用户角色
    /// </summary>
    public string SysRoles { get; init; } = string.Empty;
}