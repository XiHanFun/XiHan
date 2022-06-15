// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomControllerExtension
// Guid:8e7e9059-8ee8-4afb-a9f7-3f608305ef55
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 03:17:15
// ----------------------------------------------------------------

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ZhaiFanhuaBlog.WebApi.Common.Filters;

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.DependencyInjection;

/// <summary>
/// CustomControllerExtension
/// </summary>
public static class CustomControllerExtension
{
    /// <summary>
    /// Controllers扩展
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomControllers(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers(options =>
        {
            // 全局注入过滤器
            options.Filters.Add<CustomActionFilterAsyncAttribute>();
            options.Filters.Add<CustomExceptionFilterAsyncAttribute>();
            options.Filters.Add<CustomResultFilterAsyncAttribute>();
            //options.Filters.Add<CustomResourceFilterAsyncAttribute>();
        })
        .AddNewtonsoftJson(options =>
        {
            // 首字母小写(驼峰样式)
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // 时间格式化
            options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            // 忽略循环引用
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            // 忽略空值
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        });
        services.AddEndpointsApiExplorer();
        return services;
    }
}