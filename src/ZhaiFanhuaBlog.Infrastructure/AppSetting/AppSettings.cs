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

namespace ZhaiFanhuaBlog.Infrastructure.AppSetting;

/// <summary>
/// AppSettings
/// </summary>
public static class AppSettings
{
    #region 读取配置

    public static string EnvironmentName { get; }

    /// <summary>
    /// 日志
    /// </summary>
    public static class Logging
    {
        public static bool Authorization { get; }
        public static bool Resource { get; }
        public static bool Action { get; }
        public static bool Result { get; }
        public static bool Exception { get; }
    }

    /// <summary>
    /// 站点
    /// </summary>
    public static class Sys
    {
        public static string Name { get; }
        public static string Description { get; }
        public static string KeyWord { get; }
        public static string Domain { get; }
        public static DateTime UpdateTime { get; }

        public static class Admin
        {
            public static string Name { get; }
            public static string Email { get; }
        }
    }

    /// <summary>
    /// 密匙
    /// </summary>
    public static class Encryptions
    {
        public static string AesKey { get; }
        public static string DesKey { get; }
    }

    /// <summary>
    /// 跨域
    /// </summary>
    public static class Cors
    {
        public static bool IsEnabled { get; }
        public static string PolicyName { get; }
        public static string[] Origins { get; }
    }

    /// <summary>
    /// 数据库
    /// </summary>
    public static class Database
    {
        public static bool Console { get; }
        public static bool Initialization { get; }
        public static string Type { get; }

        public static class Connectionstring
        {
            public static string MySql { get; }
            public static string SqlServer { get; }
            public static string Sqlite { get; }
            public static string Oracle { get; }
            public static string PostgreSQL { get; }
        }
    }

    /// <summary>
    /// 缓存
    /// </summary>
    public static class Cache
    {
        public static int SyncTimeout { get; }

        public static class MemoryCache
        {
            public static bool IsEnabled { get; }
        }

        public static class Distributedcache
        {
            public static bool IsEnabled { get; }

            public static class Redis
            {
                public static string ConnectionString { get; }
                public static string InstanceName { get; }
            }
        }

        public static class Responsecache
        {
            public static bool IsEnabled { get; }
        }
    }

    /// <summary>
    /// 文档
    /// </summary>
    public static class Swagger
    {
        public static string RoutePrefix { get; }
        public static string[] PublishGroup { get; }
    }

    /// <summary>
    /// 性能分析
    /// </summary>
    public static class Miniprofiler
    {
        public static bool IsEnabled { get; }
    }

    /// <summary>
    /// 授权
    /// </summary>
    public static class Auth
    {
        public static class JWT
        {
            public static string Issuer { get; }
            public static string Audience { get; }
            public static string SymmetricKey { get; }
            public static int ClockSkew { get; }
            public static int Expires { get; }
        }

        public static class Oauth
        {
            public static class QQ
            {
                public static bool IsEnabled { get; }
                public static string ClientId { get; }
                public static string ClientSecret { get; }
                public static string Scope { get; }
                public static string RedirectUrl { get; }
            }

            public static class Github
            {
                public static bool IsEnabled { get; }
                public static string ClientID { get; }
                public static string ClientSecret { get; }
                public static string Scope { get; }
                public static string RedirectUri { get; }
            }

            public static class Gitee
            {
                public static bool IsEnabled { get; }
                public static string ClientID { get; }
                public static string ClientSecret { get; }
                public static string Scope { get; }
                public static string RedirectUri { get; }
            }
        }
    }

    /// <summary>
    /// 网络分发
    /// </summary>
    public static class CDN
    {
        public static bool IsEnabled { get; }
        public static string Type { get; }

        public static class Tencentcloud
        {
            public static string SecretId { get; }
            public static string SecretKey { get; }
        }
    }

    /// <summary>
    /// 任务
    /// </summary>
    public static class Job
    {
        public static bool IsEnabled { get; }
        public static string Cron { get; }
    }

    /// <summary>
    /// 消息推送
    /// </summary>
    public static class Message
    {
        /// <summary>
        /// 钉钉
        /// </summary>
        public static class DingTalk
        {
            public static string WebHookUrl { get; }
            public static string AccessToken { get; }
            public static string Secret { get; }
        }

        /// <summary>
        /// 微信
        /// </summary>
        public static class WeChart
        {
            public static string WebHookUrl { get; }
            public static string UploadkUrl { get; }
            public static string Key { get; }
        }

        /// <summary>
        /// 邮件
        /// </summary>
        public static class Email
        {
            public static string Host { get; }
            public static int Port { get; }
            public static bool UseSsl { get; }

            public static class From
            {
                public static string Username { get; }
                public static string Password { get; }
                public static string Address { get; }
            }
        }
    }

    #endregion 读取配置
}