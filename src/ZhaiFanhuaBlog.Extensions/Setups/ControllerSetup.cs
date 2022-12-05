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
using System.Text.Json;
using System.Text.Json.Serialization;
using ZhaiFanhuaBlog.Extensions.Common.Controller;
using ZhaiFanhuaBlog.Extensions.Filters;

namespace ZhaiFanhuaBlog.Extensions.Setups;

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
        .AddJsonOptions(options =>
        {
            // 格式化日期时间格式，需要自己创建指定的转换类DatetimeJsonConverter
            options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
            //设置bool获取格式
            options.JsonSerializerOptions.Converters.Add(new BoolJsonConverter());
            // 忽略循环引用
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            // 数据格式首字母小写(驼峰样式)，null则为不改变大小写
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            // 获取或设置要在转义字符串时使用的编码器，不转义字符
            options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            // 允许额外符号
            options.JsonSerializerOptions.AllowTrailingCommas = true;
            // 反序列化过程中属性名称是否使用不区分大小写的比较
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
        });

        return services;
    }
}