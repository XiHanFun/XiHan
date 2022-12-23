#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AppConfigManager
// Guid:9e4bb8b4-adfd-4c8b-a519-e4b0dc1706a1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-21 上午 04:57:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using ZhaiFanhuaBlog.Utils.Console;
using ZhaiFanhuaBlog.Utils.Serialize;

namespace ZhaiFanhuaBlog.Infrastructure.Apps.Setting;

/// <summary>
/// AppConfigManager
/// </summary>
public static class AppConfigManager
{
    /// <summary>
    /// 配置文件的根节点
    /// </summary>
    private static IConfiguration ConfigurationRoot { get; set; } = null!;

    /// <summary>
    /// 自定义配置文件位置
    /// </summary>
    private static string ConfigurationFile { get; set; } = string.Empty;

    /// <summary>
    /// 注册日志
    /// </summary>
    /// <param name="configs"></param>
    public static void RegisterLog(IConfigurationBuilder configs)
    {
        ConfigurationRoot = configs.Build();
        if (configs is ConfigurationManager configurationManager)
        {
            var jsonFilePath = configurationManager.Sources
                .OfType<JsonConfigurationSource>()
                .Select(file => file?.Path!);
            if (jsonFilePath.Any())
            {
                string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!.ToString()!;
                string configurationFile = jsonFilePath.First(name => name.Contains(envName));
                ConfigurationFile = configurationFile;
            }
        }
        $"配置注册：{nameof(ConfigurationRoot)}".WriteLineSuccess();
    }

    /// <summary>
    /// 获取值
    /// </summary>
    /// <typeparam name="TREntity"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public static TREntity Get<TREntity>(string key)
    {
        var result = ConfigurationRoot.GetValue<TREntity>(GetPropertyName(key));
        if (result != null)
        {
            return result;
        }
        else
        {
            throw new Exception("配置文件未配置该设置 ");
        }
    }

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <typeparam name="TREntity"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public static TREntity GetSection<TREntity>(string key)
    {
        var result = ConfigurationRoot.GetSection(GetPropertyName(key)).Get<TREntity>();
        if (result != null)
        {
            return result;
        }
        else
        {
            throw new Exception("配置文件未配置该设置 ");
        }
    }

    /// <summary>
    /// 设置对象
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TREntity"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void Set<TEntity, TREntity>(string key, TREntity value)
    {
        JsonHelper jsonHelper = new(ConfigurationFile);
        jsonHelper.Set<TEntity, TREntity>(GetPropertyName(key), value);
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
}