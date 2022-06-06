// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:JWTExtension
// Guid:fcc7eece-77f0-4f6c-bc50-fbb21dc9d96f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 02:25:36
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ZhaiFanhuaBlog.ViewModels.Response;

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.DependencyInjection;

/// <summary>
/// JWTExtension
/// </summary>
public static class JWTExtension
{
    /// <summary>
    /// JWT扩展
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomJWT(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                         {
                             options.TokenValidationParameters = new TokenValidationParameters
                             {
                                 ValidateIssuer = true,
                                 ValidateAudience = true,
                                 ValidateLifetime = true,
                                 ValidateIssuerSigningKey = true,
                                 ValidIssuer = config["Auth:JWT:SiteDomain"],
                                 ValidAudience = config["Auth:JWT:SiteDomain"],
                                 ClockSkew = TimeSpan.FromSeconds(config.GetValue<int>("Auth:JWT:ClockSkew")),
                                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Auth:JWT:IssuerSigningKey"]))
                             };
                             options.Events = new JwtBearerEvents
                             {
                                 OnAuthenticationFailed = context =>
                                 {
                                     // 如果过期，则把是否过期添加到返回头信息中
                                     if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                     {
                                         context.Response.Headers.Add("Token-Expired", "true");
                                     }
                                     return Task.CompletedTask;
                                 },
                                 OnChallenge = context =>
                                 {
                                     // 跳过默认的处理逻辑，返回下面的模型数据
                                     context.HandleResponse();
                                     return Task.FromResult(ResultMessage<object>.Unauthorized());
                                 }
                             };
                         });
        return services;
    }
}