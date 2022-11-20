#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AppSettings
// Guid:075a4b94-d8d4-4b4e-8e13-83ae6b03e16c
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-07-22 下午 12:21:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Configuration;

namespace ZhaiFanhuaBlog.Core.AppSettings;

/// <summary>
/// AppSettings
/// </summary>
public class AppSettings
{
    /// <summary>
    /// 配置文件的根节点
    /// </summary>
    public static IConfiguration _IConfiguration = new ConfigurationBuilder().Build();

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configuration"></param>
    public AppSettings(IConfiguration configuration)
    {
        _IConfiguration = configuration;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="contentPath"></param>
    public AppSettings(string contentPath)
    {
        // 获取环境名称
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        // 根据ASPNETCORE_ENVIRONMENT环境变量来读取不同的配置，如ASPNETCORE_ENVIRONMENT = Test，则会读取appsettings.Test.json文件
        _IConfiguration = new ConfigurationBuilder().SetBasePath(contentPath)
            // 默认读取appsettings.json
            .AddJsonFile("appsettings.json")
            // 如果存在环境配置文件，优先使用这里的配置
            .AddJsonFile($"appsettings.{environmentName}.json", true, true)
            .Build();
    }

    #endregion 构造函数

    #region 基本方法

    public static bool GetBoolValue(string key)
    {
        return _IConfiguration.GetValue<bool>(key);
    }

    public static int GetIntValue(string key)
    {
        return _IConfiguration.GetValue<int>(key);
    }

    public static string GetStringValue(string key)
    {
        return _IConfiguration.GetValue<string>(key) ?? string.Empty;
    }

    public static DateTime GetDateTimeValue(string key)
    {
        return _IConfiguration.GetValue<DateTime>(key);
    }

    public static string[] GetStringArrayValue(string key)
    {
        return _IConfiguration.GetSection(key).Get<string[]>() ?? Array.Empty<string>();
    }

    #endregion 基本方法

    #region 读取配置

    public static string EnvironmentName => GetStringValue("EnvironmentName");

    /// <summary>
    /// 日志
    /// </summary>
    public static class Logging
    {
        public static bool Authorization => GetBoolValue("Logging:Authorization");
        public static bool Resource => GetBoolValue("Logging:Resource");
        public static bool Action => GetBoolValue("Logging:Action");
        public static bool Result => GetBoolValue("Logging:Result");
        public static bool Exception => GetBoolValue("Logging:Exception");
    }

    /// <summary>
    /// 站点
    /// </summary>
    public static class Site
    {
        public static string Name => GetStringValue("Site:Name");
        public static string Description => GetStringValue("Site:Description");
        public static string KeyWord => GetStringValue("Site:KeyWord");
        public static string Domain => GetStringValue("Site:Domain");
        public static DateTime UpdateTime => GetDateTimeValue("Site:UpdateTime");

        public static class Admin
        {
            public static string Name => GetStringValue("Site:Admin:Name");
            public static string Email => GetStringValue("Site:Admin:Email");
        }
    }

    /// <summary>
    /// 密匙
    /// </summary>
    public static class Encryptions
    {
        public static string AesKey => GetStringValue("Encryptions:AesKey");
        public static string DesKey => GetStringValue("Encryptions:DesKey");
    }

    /// <summary>
    /// 跨域
    /// </summary>
    public static class Cors
    {
        public static bool IsEnabled => GetBoolValue("Cors:IsEnabled");
        public static string PolicyName => GetStringValue("Cors:PolicyName");
        public static string[] Origins => GetStringArrayValue("Cors:Origins");
    }

    /// <summary>
    /// 数据库
    /// </summary>
    public static class Database
    {
        public static bool Console => GetBoolValue("Database:Console");
        public static bool Initialization => GetBoolValue("Database:Initialization");
        public static string Type => GetStringValue("Database:Type");

        public static class Connectionstring
        {
            public static string MySql => GetStringValue("Database:Connectionstring:MySql");
            public static string SqlServer => GetStringValue("Database:Connectionstring:SqlServer");
            public static string Sqlite => GetStringValue("Database:Connectionstring:Sqlite");
            public static string Oracle => GetStringValue("Database:Connectionstring:Oracle");
            public static string PostgreSQL => GetStringValue("Database:Connectionstring:PostgreSQL");
        }
    }

    /// <summary>
    /// 缓存
    /// </summary>
    public static class Cache
    {
        public static int SyncTimeout => GetIntValue("Cache:SyncTimeout");

        public static class MemoryCache
        {
            public static bool IsEnabled => GetBoolValue("Cache:MemoryCache:IsEnabled");
        }

        public static class Distributedcache
        {
            public static bool IsEnabled => GetBoolValue("Cache:Distributedcache:IsEnabled");

            public static class Redis
            {
                public static string ConnectionString => GetStringValue("Cache:Distributedcache:Redis:ConnectionString");
                public static string InstanceName => GetStringValue("Cache:Distributedcache:Redis:InstanceName");
            }
        }

        public static class Responsecache
        {
            public static bool IsEnabled => GetBoolValue("Cache:Responsecache:IsEnabled");
        }
    }

    /// <summary>
    /// 文档
    /// </summary>
    public static class Swagger
    {
        public static string RoutePrefix => GetStringValue("Swagger:RoutePrefix");
        public static string[] PublishGroup => GetStringArrayValue("Swagger:PublishGroup");
    }

    /// <summary>
    /// 性能分析
    /// </summary>
    public static class Miniprofiler
    {
        public static bool IsEnabled => GetBoolValue("Miniprofiler:IsEnabled");
    }

    /// <summary>
    /// 授权
    /// </summary>
    public static class Auth
    {
        public static class JWT
        {
            public static string Issuer => GetStringValue("Auth:JWT:Issuer");
            public static string Audience => GetStringValue("Auth:JWT:Audience");
            public static string SymmetricKey => GetStringValue("Auth:JWT:SymmetricKey");
            public static int ClockSkew => GetIntValue("Auth:JWT:ClockSkew");
            public static int Expires => GetIntValue("Auth:JWT:Expires");
        }

        public static class Oauth
        {
            public static class QQ
            {
                public static bool IsEnabled => GetBoolValue("Auth:Oauth:QQ:IsEnabled");
                public static string ClientId => GetStringValue("Auth:Oauth:QQ:ClientId");
                public static string ClientSecret => GetStringValue("Auth:Oauth:QQ:ClientSecret");
                public static string Scope => GetStringValue("Auth:Oauth:QQ:Scope");
                public static string RedirectUrl => GetStringValue("Auth:Oauth:QQ:RedirectUrl");
            }

            public static class Github
            {
                public static bool IsEnabled => GetBoolValue("Auth:Oauth:Github:IsEnabled");
                public static string ClientID => GetStringValue("Auth:Oauth:Github:ClientID");
                public static string ClientSecret => GetStringValue("Auth:Oauth:Github:ClientSecret");
                public static string Scope => GetStringValue("Auth:Oauth:Github:Scope");
                public static string RedirectUri => GetStringValue("Auth:Oauth:Github:RedirectUri");
            }

            public static class Gitee
            {
                public static bool IsEnabled => GetBoolValue("Auth:Oauth:Gitee:IsEnabled");
                public static string ClientID => GetStringValue("Auth:Oauth:Gitee:ClientID");
                public static string ClientSecret => GetStringValue("Auth:Oauth:Gitee:ClientSecret");
                public static string Scope => GetStringValue("Auth:Oauth:Gitee:Scope");
                public static string RedirectUri => GetStringValue("Auth:Oauth:Gitee:RedirectUri");
            }
        }
    }

    /// <summary>
    /// 网络分发
    /// </summary>
    public static class CDN
    {
        public static bool IsEnabled => GetBoolValue("CDN:IsEnabled");
        public static string Type => GetStringValue("CDN:Type");

        public static class Tencentcloud
        {
            public static string SecretId => GetStringValue("CDN:Tencentcloud:SecretId");
            public static string SecretKey => GetStringValue("CDN:Tencentcloud:SecretKey");
        }
    }

    /// <summary>
    /// 任务
    /// </summary>
    public static class Job
    {
        public static bool IsEnabled => GetBoolValue("Job:IsEnabled");
        public static string Cron => GetStringValue("Job:Cron");
    }

    /// <summary>
    /// 邮件
    /// </summary>
    public static class Email
    {
        public static string Host => GetStringValue("Email:Host");
        public static int Port => GetIntValue("Email:Port");
        public static bool UseSsl => GetBoolValue("Email:UseSsl");

        public static class From
        {
            public static string Username => GetStringValue("Email:From:Username");
            public static string Password => GetStringValue("Email:From:Password");
            public static string Name => GetStringValue("Email:From:Name");
            public static string Address => GetStringValue("Email:From:Address");
        }
    }

    /// <summary>
    /// 钉钉
    /// </summary>
    public static class DingTalk
    {
        public static string WebHookUrl => GetStringValue("DingTalk:WebHookUrl");
        public static string AccessToken => GetStringValue("DingTalk:AccessToken");
        public static string KeyWord => GetStringValue("DingTalk:KeyWord");
        public static string Secret => GetStringValue("DingTalk:Secret");
    }

    /// <summary>
    /// 微信
    /// </summary>
    public static class WeChart
    {
        public static string WebHookUrl => GetStringValue("WeChart:WebHookUrl");
        public static string UploadkUrl => GetStringValue("WeChart:UploadkUrl");
        public static string Key => GetStringValue("WeChart:Key");
    }

    #endregion 读取配置
}