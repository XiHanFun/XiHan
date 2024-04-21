#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AppConfigProvider
// Guid:ba4933f3-b274-4929-9d2b-166628f05f67
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 14:14:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using XiHan.Common.Utilities.Extensions;
using XiHan.Core.System.Text.Json;

namespace XiHan.Infrastructure.Core.Apps.Configs;

/// <summary>
/// 全局配置供应器
/// </summary>
public static class AppConfigProvider
{
    /// <summary>
    /// 全局配置
    /// </summary>
    public static IConfiguration ConfigurationRoot { get; set; } = null!;

    /// <summary>
    /// 自定义配置文件位置
    /// </summary>
    public static string ConfigurationFile { get; set; } = string.Empty;

    /// <summary>
    /// 注册配置
    /// </summary>
    /// <param name="config"></param>
    public static void RegisterConfig(IConfigurationBuilder config)
    {
        if (config is not ConfigurationManager configurationManager)
            return;

        List<string> jsonFilePath = configurationManager.Sources
            .OfType<JsonConfigurationSource>()
            .Select(file => file.Path!)
            .ToList();
        if (jsonFilePath.Count == 0 || !jsonFilePath.Remove("appsettings.json"))
            return;

        try
        {
            var configurationFile = jsonFilePath.First(name => name.Contains("appsettings"));
            var envName = configurationFile.Split('.')[1];
            ConfigurationRoot = config.Build();
            ConfigurationFile = configurationFile;
            var infoMsg = $"配置注册：环境{envName}，配置中心{ConfigurationRoot}，文件名称{ConfigurationFile}";
            infoMsg.WriteLineSuccess();
        }
        catch (Exception)
        {
            const string errorMsg = $"配置注册出错，配置文件未找到！";
            errorMsg.WriteLineError();
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
        return result != null ? result : throw new ArgumentNullException($"配置文件未配置该设置【{key}】或配置出错！");
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
        return result != null ? result : throw new ArgumentNullException($"配置文件未配置该设置【{key}】或配置出错！");
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
    /// 获取属性名称
    /// 例如 AppSettings.Database.Initialized => Database:Initialized
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private static string GetPropertyName(string key)
    {
        return key.Replace("AppSettings.", string.Empty).Replace(".", ":");
    }
}