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
    public static IConfiguration _ConfigurationRoot { get; set; } = new ConfigurationBuilder().Build();

    /// <summary>
    /// 自定义配置文件位置
    /// </summary>
    public static string ConfigurationFile { get; set; } = string.Empty;

    #region 构造函数

    /// <summary>
    /// 通过构造函数
    /// </summary>
    /// <param name="configuration"></param>
    public AppConfig(IConfiguration configuration)
    {
        _ConfigurationRoot = configuration;

        // 获取配置文件
        try
        {
            if (configuration is ConfigurationManager configurationManager)
            {
                var jsonFilePath = configurationManager.Sources
                                   .Where(manager => manager is JsonConfigurationSource)
                                   .Select(manager => manager as JsonConfigurationSource)
                                   .Select(file => file?.Path!);
                if (jsonFilePath.Any())
                {
                    string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;
                    string configurationFile = jsonFilePath.First(name => name.Contains(envName));
                    ConfigurationFile = configurationFile;
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// 通过指定配置文件构造函数
    /// </summary>
    /// <param name="contentPath"></param>
    public AppConfig(string contentPath)
    {
        string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;
        string file = "appsettings.json";
        string configurationFile = $"appsettings.{envName}.json";

        // 根据ASPNETCORE_ENVIRONMENT环境变量来读取不同的配置，如ASPNETCORE_ENVIRONMENT = Test，则会读取appsettings.Test.json文件
        _ConfigurationRoot = new ConfigurationBuilder().SetBasePath(contentPath)
            // 默认读取appsettings.json
            .AddJsonFile(file, false, true)
            // 如果存在环境配置文件，优先使用这里的配置
            .AddJsonFile(configurationFile, false, true)
            .Build();
        ConfigurationFile = configurationFile;
    }

    #endregion 构造函数

    #region 基本方法

    /// <summary>
    /// 获取值
    /// </summary>
    /// <typeparam name="R"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public static R? GetValue<R>(string key)
    {
        return _ConfigurationRoot.GetValue<R>(GetPropertyName(key));
    }

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <typeparam name="R"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public static R? GetSectionValue<R>(string key)
    {
        return _ConfigurationRoot.GetSection(GetPropertyName(key)).Get<R>();
    }

    /// <summary>
    /// 设置对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="value"></param>
    public static void SetValue<T>(T entity, dynamic value)
    {
        JsonHelper jsonHelper = new(ConfigurationFile);
        jsonHelper.Set(GetPropertyName(entity.FullNameOf()), value);
    }

    /// <summary>
    /// 获取属性名称 例如 AppSettings.Database.Initialization => Database:Initialization
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private static string GetPropertyName(string key)
    {
        return key.Replace("AppSettings.", "").Replace(".", ":");
    }

    #endregion 基本方法
}