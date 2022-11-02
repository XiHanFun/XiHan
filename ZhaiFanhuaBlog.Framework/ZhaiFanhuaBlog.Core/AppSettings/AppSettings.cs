// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AppSettings
// Guid:075a4b94-d8d4-4b4e-8e13-83ae6b03e16c
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-07-22 下午 12:21:06
// ----------------------------------------------------------------

using Microsoft.Extensions.Configuration;

namespace ZhaiFanhuaBlog.Core.AppSettings;

/// <summary>
/// AppSettings
/// </summary>
public class AppSettings
{
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

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configuration"></param>
    public AppSettings(IConfiguration configuration)
    {
        _IConfiguration = configuration;
    }

    /// <summary>
    /// 配置文件的根节点
    /// </summary>
    public static IConfiguration _IConfiguration = new ConfigurationBuilder().Build();

    public static string EnvironmentName => _IConfiguration.GetValue<string>("EnvironmentName");

    /// <summary>
    /// 日志
    /// </summary>
    public static class Logging
    {
        public static bool Authorization => _IConfiguration.GetValue<bool>("Logging:Authorization");
        public static bool Resource => _IConfiguration.GetValue<bool>("Logging:Resource");
        public static bool Action => _IConfiguration.GetValue<bool>("Logging:Action");
        public static bool Result => _IConfiguration.GetValue<bool>("Logging:Result");
        public static bool Exception => _IConfiguration.GetValue<bool>("Logging:Exception");
    }

    /// <summary>
    /// 站点
    /// </summary>
    public static class Site
    {
        public static string Name => _IConfiguration.GetValue<string>("Site:Name");
        public static string Description => _IConfiguration.GetValue<string>("Site:Description");
        public static string KeyWord => _IConfiguration.GetValue<string>("Site:KeyWord");
        public static string Domain => _IConfiguration.GetValue<string>("Site:Domain");
        public static DateTime UpdateTime => _IConfiguration.GetValue<DateTime>("Site:UpdateTime");

        public static class Admin
        {
            public static string Name => _IConfiguration.GetValue<string>("Site:Admin:Name");
            public static string Email => _IConfiguration.GetValue<string>("Site:Admin:Email");
        }
    }

    /// <summary>
    /// 密匙
    /// </summary>
    public static class Encryptions
    {
        public static string AesKey => _IConfiguration.GetValue<string>("Encryptions:AesKey");
        public static string DesKey => _IConfiguration.GetValue<string>("Encryptions:DesKey");
    }

    /// <summary>
    /// 跨域
    /// </summary>
    public static class Cors
    {
        public static bool IsEnabled => _IConfiguration.GetValue<bool>("Cors:IsEnabled");
        public static string PolicyName => _IConfiguration.GetValue<string>("Cors:PolicyName");
        public static string[] Origins => _IConfiguration.GetSection("Cors:Origins").Get<string[]>();
    }

    /// <summary>
    /// 数据库
    /// </summary>
    public static class Database
    {
        public static bool Console => _IConfiguration.GetValue<bool>("Database:Console");
        public static bool Initialization => _IConfiguration.GetValue<bool>("Database:Initialization");
        public static string Type => _IConfiguration.GetValue<string>("Database:Type");

        public static class Connectionstring
        {
            public static string MySql => _IConfiguration.GetValue<string>("Database:Connectionstring:MySql");
            public static string SqlServer => _IConfiguration.GetValue<string>("Database:Connectionstring:SqlServer");
            public static string Sqlite => _IConfiguration.GetValue<string>("Database:Connectionstring:Sqlite");
            public static string Oracle => _IConfiguration.GetValue<string>("Database:Connectionstring:Oracle");
            public static string PostgreSQL => _IConfiguration.GetValue<string>("Database:Connectionstring:PostgreSQL");
        }
    }

    /// <summary>
    /// 缓存
    /// </summary>
    public static class Cache
    {
        public static int SyncTimeout => _IConfiguration.GetValue<int>("Cache:SyncTimeout");

        public static class MemoryCache
        {
            public static bool IsEnabled => _IConfiguration.GetValue<bool>("Cache:MemoryCache:IsEnabled");
        }

        public static class Distributedcache
        {
            public static bool IsEnabled => _IConfiguration.GetValue<bool>("Cache:Distributedcache:IsEnabled");

            public static class Redis
            {
                public static string ConnectionString => _IConfiguration.GetValue<string>("Cache:Distributedcache:Redis:ConnectionString");
                public static string InstanceName => _IConfiguration.GetValue<string>("Cache:Distributedcache:Redis:InstanceName");
            }
        }

        public static class Responsecache
        {
            public static bool IsEnabled => _IConfiguration.GetValue<bool>("Cache:Responsecache:IsEnabled");
        }
    }

    /// <summary>
    /// 文档
    /// </summary>
    public static class Swagger
    {
        public static string Version => _IConfiguration.GetValue<string>("Swagger:Version");
        public static string RoutePrefix => _IConfiguration.GetValue<string>("Swagger:RoutePrefix");
    }

    /// <summary>
    /// 性能分析
    /// </summary>
    public static class Miniprofiler
    {
        public static bool IsEnabled => _IConfiguration.GetValue<bool>("Miniprofiler:IsEnabled");
    }

    /// <summary>
    /// 授权
    /// </summary>
    public static class Auth
    {
        public static class JWT
        {
            public static string Issuer => _IConfiguration.GetValue<string>("Auth:JWT:Issuer");
            public static string Audience => _IConfiguration.GetValue<string>("Auth:JWT:Audience");
            public static string SymmetricKey => _IConfiguration.GetValue<string>("Auth:JWT:SymmetricKey");
            public static int ClockSkew => _IConfiguration.GetValue<int>("Auth:JWT:ClockSkew");
            public static int Expires => _IConfiguration.GetValue<int>("Auth:JWT:Expires");
        }

        public static class Oauth
        {
            public static class QQ
            {
                public static bool IsEnabled => _IConfiguration.GetValue<bool>("Auth:Oauth:QQ:IsEnabled");
                public static string ClientId => _IConfiguration.GetValue<string>("Auth:Oauth:QQ:ClientId");
                public static string ClientSecret => _IConfiguration.GetValue<string>("Auth:Oauth:QQ:ClientSecret");
                public static string Scope => _IConfiguration.GetValue<string>("Auth:Oauth:QQ:Scope");
                public static string RedirectUrl => _IConfiguration.GetValue<string>("Auth:Oauth:QQ:RedirectUrl");
            }

            public static class Github
            {
                public static bool IsEnabled => _IConfiguration.GetValue<bool>("Auth:Oauth:Github:IsEnabled");
                public static string ClientID => _IConfiguration.GetValue<string>("Auth:Oauth:Github:ClientID");
                public static string ClientSecret => _IConfiguration.GetValue<string>("Auth:Oauth:Github:ClientSecret");
                public static string Scope => _IConfiguration.GetValue<string>("Auth:Oauth:Github:Scope");
                public static string RedirectUri => _IConfiguration.GetValue<string>("Auth:Oauth:Github:RedirectUri");
            }

            public static class Gitee
            {
                public static bool IsEnabled => _IConfiguration.GetValue<bool>("Auth:Oauth:Gitee:IsEnabled");
                public static string ClientID => _IConfiguration.GetValue<string>("Auth:Oauth:Gitee:ClientID");
                public static string ClientSecret => _IConfiguration.GetValue<string>("Auth:Oauth:Gitee:ClientSecret");
                public static string Scope => _IConfiguration.GetValue<string>("Auth:Oauth:Gitee:Scope");
                public static string RedirectUri => _IConfiguration.GetValue<string>("Auth:Oauth:Gitee:RedirectUri");
            }
        }
    }

    /// <summary>
    /// 网络分发
    /// </summary>
    public static class CDN
    {
        public static bool IsEnabled => _IConfiguration.GetValue<bool>("CDN:IsEnabled");
        public static string Type => _IConfiguration.GetValue<string>("CDN:Type");

        public static class Tencentcloud
        {
            public static string SecretId => _IConfiguration.GetValue<string>("CDN:Tencentcloud:SecretId");
            public static string SecretKey => _IConfiguration.GetValue<string>("CDN:Tencentcloud:SecretKey");
        }
    }

    /// <summary>
    /// 任务
    /// </summary>
    public static class Job
    {
        public static bool IsEnabled => _IConfiguration.GetValue<bool>("Job:IsEnabled");
        public static string Cron => _IConfiguration.GetValue<string>("Job:Cron");
    }

    /// <summary>
    /// 邮件
    /// </summary>
    public static class Email
    {
        public static string Host => _IConfiguration.GetValue<string>("Email:Host");
        public static int Port => _IConfiguration.GetValue<int>("Email:Port");
        public static bool UseSsl => _IConfiguration.GetValue<bool>("Email:UseSsl");

        public static class From
        {
            public static string Username => _IConfiguration.GetValue<string>("Email:From:Username");
            public static string Password => _IConfiguration.GetValue<string>("Email:From:Password");
            public static string Name => _IConfiguration.GetValue<string>("Email:From:Name");
            public static string Address => _IConfiguration.GetValue<string>("Email:From:Address");
        }
    }

    /// <summary>
    /// 邮件
    /// </summary>
    public static class DingTalk
    {
        public static string WebHookUrl => _IConfiguration.GetValue<string>("DingTalk:WebHookUrl");
        public static string KeyWord => _IConfiguration.GetValue<string>("DingTalk:KeyWord");
    }
}