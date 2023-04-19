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
using Serilog;
using XiHan.Utils.Consoles;
using XiHan.Utils.Serializes;

namespace XiHan.Infrastructure.Apps.Setting;

/// <summary>
/// 全局配置管理器
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
    /// 注册配置
    /// </summary>
    /// <param name="config"></param>
    public static void RegisterConfig(IConfigurationBuilder config)
    {
        ConfigurationRoot = config.Build();
        if (config is ConfigurationManager configurationManager)
        {
            var jsonFilePath = configurationManager.Sources
                .OfType<JsonConfigurationSource>()
                .Select(file => file?.Path!)
                .ToList();
            if (jsonFilePath.Any() && jsonFilePath.Remove("appsettings.json"))
            {
                try
                {
                    var configurationFile = jsonFilePath.First(name => name.Contains("appsettings"));
                    var envName = configurationFile.Split('.')[1].ToString();
                    ConfigurationFile = configurationFile;
                    var infoMsg = $"配置注册：环境{envName}，配置中心{ConfigurationRoot}，文件名称{ConfigurationFile}";
                    Log.Information(infoMsg);
                    infoMsg.WriteLineSuccess();
                }
                catch (Exception ex)
                {
                    var errorMsg = $"配置注册出错，配置文件未找到！";
                    Log.Error(ex, errorMsg);
                    errorMsg.WriteLineError();
                }
            }
        }
    }

    /// <summary>
    /// 获取值
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    public static TKey GetValue<TKey>(string key)
    {
        var result = ConfigurationRoot.GetValue<TKey>(GetPropertyName(key));
        return result != null ? result : throw new ArgumentNullException($"配置文件未配置该设置【{key}】");
    }

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    public static TKey GetSection<TKey>(string key)
    {
        var result = ConfigurationRoot.GetSection(GetPropertyName(key)).Get<TKey>();
        return result != null ? result : throw new ArgumentNullException($"配置文件未配置该设置【{key}】");
    }

    /// <summary>
    /// 设置对象
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void Set<TKey, TValue>(string key, TValue value)
    {
        JsonHelper jsonHelper = new(ConfigurationFile);
        jsonHelper.Set<TKey, TValue>(GetPropertyName(key), value);
    }

    /// <summary>
    /// 获取属性名称 例如 AppSettings.Database.Inited => Database:Inited
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private static string GetPropertyName(string key)
    {
        return key.Replace("AppSettings.", "").Replace(".", ":");
    }
}