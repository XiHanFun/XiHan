// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomJwtExtension
// Guid:fcc7eece-77f0-4f6c-bc50-fbb21dc9d96f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 02:25:36
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ZhaiFanhuaBlog.Models.Response;
using ZhaiFanhuaBlog.Utils.Config;

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.DependencyInjection;

/// <summary>
/// CustomJwtExtension
/// </summary>
public static class CustomJwtExtension
{
    /// <summary>
    /// JWT服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomJWT(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                         {
                             options.SaveToken = true;
                             options.TokenValidationParameters = new TokenValidationParameters
                             {
                                 //是否验证颁发者
                                 ValidateIssuer = true,
                                 // 颁发者
                                 ValidIssuer = ConfigHelper.Configuration.GetValue<string>("Configuration:Domain"),
                                 // 是否验证签收者
                                 ValidateAudience = true,
                                 // 签收者
                                 ValidAudience = ConfigHelper.Configuration.GetValue<string>("Configuration:Domain"),
                                 // 是否验证签名
                                 ValidateIssuerSigningKey = true,
                                 // 签名
                                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigHelper.Configuration.GetValue<string>("Auth:JWT:IssuerSigningKey"))),
                                 // 是否验证过期时间
                                 ValidateLifetime = true,
                                 // 过期时间容错值,单位为秒,若为0，过期时间一到立即失效
                                 ClockSkew = TimeSpan.FromSeconds(ConfigHelper.Configuration.GetValue<int>("Auth:JWT:ClockSkew")),
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
                                     return Task.FromResult(ResultResponse.Unauthorized());
                                 }
                             };
                         });
        return services;
    }
}