#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ControllerSetup
// Guid:8e7e9059-8ee8-4afb-a9f7-3f608305ef55
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 03:17:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ZhaiFanhuaBlog.Extensions.Filters;
using ZhaiFanhuaBlog.Utils.Http;

namespace ZhaiFanhuaBlog.Setups;

/// <summary>
/// ControllerSetup
/// </summary>
public static class ControllerSetup
{
    /// <summary>
    /// Controllers服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddControllersSetup(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddControllers(options =>
        {
            // 全局注入过滤器
            options.Filters.Add<AuthorizationFilterAsyncAttribute>();
            options.Filters.Add<ExceptionFilterAsyncAttribute>();
            //options.Filters.Add<ResourceFilterAsyncAttribute>();
            options.Filters.Add<ActionFilterAsyncAttribute>();
            options.Filters.Add<ResultFilterAsyncAttribute>();
        }).ConfigureApiBehaviorOptions(options =>
        {
            //关闭默认模型验证，通过CustomActionFilterAsyncAttribute自定义验证
            options.SuppressModelStateInvalidFilter = true;
        })
        .AddNewtonsoftJson(options =>
        {
            // 忽略循环引用
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            // 首字母小写(驼峰样式，不使用驼峰样式为：new DefaultContractResolver())
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // 时间格式化
            options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            // 忽略Model中为null的属性
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            //设置本地时间而非UTC时间
            options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
        });
        return services;
    }
}