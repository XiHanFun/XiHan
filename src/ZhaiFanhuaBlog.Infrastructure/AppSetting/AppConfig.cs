#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AppConfig
// Guid:9e4bb8b4-adfd-4c8b-a519-e4b0dc1706a1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-21 上午 04:57:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using ZhaiFanhuaBlog.Utils.Object;
using ZhaiFanhuaBlog.Utils.Serialize;

namespace ZhaiFanhuaBlog.Infrastructure.AppSetting;

/// <summary>
/// AppConfig
/// </summary>
public class AppConfig
{
    /// <summary>
    /// 配置文件的根节点
    /// </summary>
    public static IConfiguration _IConfiguration { get; set; } = new ConfigurationBuilder().Build();

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configuration"></param>
    public AppConfig(IConfiguration configuration)
    {
        _IConfiguration = configuration;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="contentPath"></param>
    public AppConfig(string contentPath)
    {
        string path = "appsettings.json";
        // 根据ASPNETCORE_ENVIRONMENT环境变量来读取不同的配置，如ASPNETCORE_ENVIRONMENT = Test，则会读取appsettings.Test.json文件
        string envpath = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";

        _IConfiguration = new ConfigurationBuilder().SetBasePath(contentPath)
                        // 直接读目录里的json文件，而不是 bin 文件夹下的，所以不用修改复制属性
                        .Add(new JsonConfigurationSource { Path = path, Optional = false, ReloadOnChange = true })
                        .Add(new JsonConfigurationSource { Path = envpath, Optional = false, ReloadOnChange = true })
                        .Build();
    }

    #endregion 构造函数

    #region 基本方法

    /// <summary>
    /// 获取属性名称
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="tentity"></param>
    /// <returns></returns>
    public static string GetPropertyName<TEntity>(TEntity tentity)
    {
        return tentity.FullNameOf().Replace("AppSettings.", "");
    }

    /// <summary>
    /// 获取值
    /// </summary>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <returns></returns>
    public static R? GetStringValue<R, T>(T t)
    {
        return _IConfiguration.GetValue<R>(GetPropertyName(t).Replace(".", ":"));
    }

    /// <summary>
    /// 获取对象值
    /// </summary>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <returns></returns>
    public static R? GetSectionValue<R, T>(T t)
    {
        return _IConfiguration.GetSection(GetPropertyName(t).Replace(".", ":")).Get<R>();
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="value"></param>
    public static void SetValue<T>(T t, dynamic value)
    {
        string path = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";
        JsonHelper jsonHelper = new(path);
        jsonHelper.Set(GetPropertyName(t), value);
    }

    #endregion 基本方法
}