#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AuthJwtSetup
// Guid:fcc7eece-77f0-4f6c-bc50-fbb21dc9d96f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 02:25:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ZhaiFanhuaBlog.Extensions.Response;
using ZhaiFanhuaBlog.Infrastructure.AppSetting;
using ZhaiFanhuaBlog.Utils.Object;

namespace ZhaiFanhuaBlog.Extensions.Setups;

/// <summary>
/// AuthJwtSetup
/// </summary>
public static class AuthJwtSetup
{
    /// <summary>
    /// AuthJwt 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAuthJwtSetup(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // 读取配置
        var issuer = AppSettings.Auth.JWT.Issuer.Get();
        var audience = AppSettings.Auth.JWT.Audience.Get();
        var symmetricKey = AppSettings.Auth.JWT.SymmetricKey.Get();
        var clockSkew = AppSettings.Auth.JWT.ClockSkew.Get();

        // 签名密钥
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricKey));
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
            ValidIssuer = issuer,
            // 是否验证签收者
            ValidateAudience = true,
            // 签收者
            ValidAudience = audience,
            // 是否验证失效时间
            ValidateLifetime = true,
            // 过期时间容错值,单位为秒,若为0，过期时间一到立即失效
            ClockSkew = TimeSpan.FromSeconds(clockSkew),
            // 需要过期时间
            RequireExpirationTime = true,
        };

        // 身份验证（Bearer）
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        // 配置鉴权逻辑，添加JwtBearer服务
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = tokenValidationParameters;
            options.Events = new JwtBearerEvents
            {
                // 认证失败时
                OnAuthenticationFailed = context =>
                {
                    var jwtHandler = new JwtSecurityTokenHandler();
                    var token = context.Request.Headers["Authorization"].ObjToString().Replace("Bearer ", "");

                    // 若Token为空、伪造无法读取
                    if (token.IsNotEmptyOrNull() && jwtHandler.CanReadToken(token))
                    {
                        var jwtToken = jwtHandler.ReadJwtToken(token);

                        if (jwtToken.Issuer != issuer)
                        {
                            context.Response.Headers.Add("Token-Error-Iss", "Issuer is wrong!");
                        }

                        if (jwtToken.Audiences.FirstOrDefault() != audience)
                        {
                            context.Response.Headers.Add("Token-Error-Aud", "Audience is wrong!");
                        }
                        // 返回自定义的未授权模型数据
                        return Task.FromResult(BaseResponseDto.Unauthorized("授权为空或因伪造无法读取"));
                    }

                    // 如果过期，则把是否过期添加到返回头信息中
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                        // 返回自定义的未授权模型数据
                        return Task.FromResult(BaseResponseDto.Unauthorized("授权已过期"));
                    }
                    return Task.FromResult(BaseResponseDto.Unauthorized());
                },
                // 未授权时
                OnChallenge = context =>
                {
                    // 将Token错误添加到返回头信息中
                    context.Response.Headers.Add("Token-Error", context.ErrorDescription);
                    // 返回自定义的未授权模型数据
                    return Task.FromResult(BaseResponseDto.Unauthorized("未授权"));
                }
            };
        });
        // 认证授权
        services.AddAuthorization();
        return services;
    }
}