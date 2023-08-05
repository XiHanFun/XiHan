#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AuthSetup
// Guid:fcc7eece-77f0-4f6c-bc50-fbb21dc9d96f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-30 上午 02:25:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Utils.Extensions;
using XiHan.WebCore.Handlers;

namespace XiHan.WebCore.Setups.Services;

/// <summary>
/// AuthSetup
/// </summary>
public static class AuthSetup
{
    /// <summary>
    /// AuthJwt 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddAuthSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // 身份验证(默认用JwtBearer认证)
        services.AddAuthentication(options =>
            {
                // 默认身份验证方案
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // 默认质询方案
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                // 默认禁止方案
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddJwtBearer(options =>
            {
                // 配置鉴权逻辑，添加JwtBearer服务
                options.SaveToken = true;
                options.TokenValidationParameters = JwtHandler.GetTokenVerifyParams();
                options.Events = new JwtBearerEvents
                {
                    // 认证失败时
                    OnAuthenticationFailed = context =>
                    {
                        var jwtHandler = new JwtSecurityTokenHandler();
                        var token = context.Request.Headers["Authorization"].ParseToString().Replace("Bearer ", "");
                        context.Response.StatusCode = 401;
                        // 若Token为空、伪造无法读取
                        if (token.IsNotEmptyOrNull() && jwtHandler.CanReadToken(token))
                        {
                            var jwtToken = jwtHandler.ReadJwtToken(token);

                            if (jwtToken.Issuer != JwtHandler.GetAuthJwtSetting().Issuer)
                                context.Response.Headers.Add("Token-Error-Iss", "Issuer is wrong!");

                            if (jwtToken.Audiences.FirstOrDefault() != JwtHandler.GetAuthJwtSetting().Audience)
                                context.Response.Headers.Add("Token-Error-Aud", "Audience is wrong!");
                            // 返回自定义的未授权模型数据
                            return context.HttpContext.Response.WriteAsJsonAsync(CustomResult.Unauthorized("授权为空或因伪造无法读取！"));
                        }

                        // 如果过期，则把是否过期添加到返回头信息中
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                            // 返回自定义的未授权模型数据
                            return context.HttpContext.Response.WriteAsJsonAsync(CustomResult.Unauthorized("授权已过期！"));
                        }

                        return context.HttpContext.Response.WriteAsJsonAsync(CustomResult.Unauthorized());
                    },
                    // 未授权时
                    OnChallenge = context =>
                    {
                        context.Response.StatusCode = 401;
                        // 将Token错误添加到返回头信息中
                        context.HttpContext.Response.Headers.Add("Token-Error", context.ErrorDescription);
                        // 返回自定义的未授权模型数据
                        return context.HttpContext.Response.WriteAsJsonAsync(CustomResult.Unauthorized("未授权！"));
                    }
                };
            })
            .AddQQ(options =>
            {
                options.ClientId = AppSettings.Auth.Qq.ClientId.GetValue();
                options.ClientSecret = AppSettings.Auth.Qq.ClientSecret.GetValue();
            })
            .AddWeixin(options =>
            {
                options.ClientId = AppSettings.Auth.WeChat.ClientId.GetValue();
                options.ClientSecret = AppSettings.Auth.WeChat.ClientSecret.GetValue();
            })
            .AddAlipay(options =>
            {
                options.ClientId = AppSettings.Auth.Alipay.ClientId.GetValue();
                options.ClientSecret = AppSettings.Auth.Alipay.ClientSecret.GetValue();
            })
            .AddGitHub(options =>
            {
                options.ClientId = AppSettings.Auth.Github.ClientId.GetValue();
                options.ClientSecret = AppSettings.Auth.Github.ClientSecret.GetValue();
                options.Scope.Add("user:email");
            })
            .AddGitLab(options =>
            {
                options.ClientId = AppSettings.Auth.Gitlab.ClientId.GetValue();
                options.ClientSecret = AppSettings.Auth.Gitlab.ClientSecret.GetValue();
            })
            .AddGitee(options =>
            {
                options.ClientId = AppSettings.Auth.Gitee.ClientId.GetValue();
                options.ClientSecret = AppSettings.Auth.Gitee.ClientSecret.GetValue();
            });
        // 认证授权
        services.AddAuthorization();
        return services;
    }
}