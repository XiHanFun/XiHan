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

using Microsoft.Extensions.DependencyInjection;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using XiHan.Utils.Serializes.Converters;
using XiHan.Web.Filters;

namespace XiHan.Web.Setups.Services;

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
    /// <exception cref="ArgumentNullException"></exception>
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
            //关闭默认模型验证，通过 ActionFilterAsyncAttribute 自定义验证
            options.SuppressModelStateInvalidFilter = true;
        })
        .AddJsonOptions(options =>
        {
            // 序列化格式
            options.JsonSerializerOptions.WriteIndented = true;
            // 忽略循环引用
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            // 布尔类型
            options.JsonSerializerOptions.Converters.Add(new BooleanJsonConverter());
            // 数字类型
            options.JsonSerializerOptions.Converters.Add(new IntJsonConverter());
            options.JsonSerializerOptions.Converters.Add(new LongJsonConverter());
            options.JsonSerializerOptions.Converters.Add(new DecimalJsonConverter());
            // 日期类型
            options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
            // 允许额外符号
            options.JsonSerializerOptions.AllowTrailingCommas = true;
            // 属性名称不使用不区分大小写的比较
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
            // 数据格式首字母小写 JsonNamingPolicy.CamelCase驼峰样式，null则为不改变大小写
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            // 获取或设置要在转义字符串时使用的编码器，不转义字符
            options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        });

        return services;
    }
}