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

namespace XiHan.Infrastructure.Apps.Setting;

/// <summary>
/// AppSettings
/// </summary>
public static class AppSettings
{
    /// <summary>
    /// 环境
    /// </summary>
    public static string EnvironmentName { get; set; } = string.Empty;

    /// <summary>
    /// 端口
    /// </summary>
    public static int Port { get; set; }

    /// <summary>
    /// 日志
    /// </summary>
    public static class Logging
    {
        /// <summary>
        /// 授权
        /// </summary>
        public static bool Authorization { get; set; }

        /// <summary>
        /// 资源
        /// </summary>
        public static bool Resource { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        public static bool Action { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public static bool Result { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        public static bool Exception { get; set; }
    }

    /// <summary>
    /// 站点
    /// </summary>
    public static class Syses
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string Name { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public static string Description { get; set; } = string.Empty;

        /// <summary>
        /// 关键字
        /// </summary>
        public static string KeyWord { get; set; } = string.Empty;

        /// <summary>
        /// 域名
        /// </summary>
        public static string Domain { get; set; } = string.Empty;

        /// <summary>
        /// 升级时间
        /// </summary>
        public static DateTime UpdateTime { get; set; }

        /// <summary>
        /// 管理员
        /// </summary>
        public static class Admin
        {
            /// <summary>
            /// 名称
            /// </summary>
            public static string Name { get; set; } = string.Empty;

            /// <summary>
            /// 邮箱
            /// </summary>
            public static string Email { get; set; } = string.Empty;
        }
    }

    /// <summary>
    /// 密匙
    /// </summary>
    public static class Encryptions
    {
        /// <summary>
        /// AES
        /// </summary>
        public static string AesKey { get; set; } = string.Empty;

        /// <summary>
        /// DES
        /// </summary>
        public static string DesKey { get; set; } = string.Empty;
    }

    /// <summary>
    /// 跨域
    /// </summary>
    public static class Cors
    {
        /// <summary>
        /// 是否可用
        /// </summary>
        public static bool IsEnabled { get; set; }

        /// <summary>
        /// 策略名称
        /// </summary>
        public static string PolicyName { get; set; } = string.Empty;

        /// <summary>
        /// 溯源
        /// </summary>
        public static string[] Origins { get; set; } = Array.Empty<string>();
    }

    /// <summary>
    /// 数据库
    /// </summary>
    public static class Database
    {
        /// <summary>
        /// 是否已经初始化
        /// </summary>
        public static bool Inited { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public static string Type { get; set; } = string.Empty;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public static class ConnectionString
        {
            /// <summary>
            /// MySql
            /// </summary>
            public static string MySql { get; set; } = string.Empty;

            /// <summary>
            /// SqlServer
            /// </summary>
            public static string SqlServer { get; set; } = string.Empty;

            /// <summary>
            /// Sqlite
            /// </summary>
            public static string Sqlite { get; set; } = string.Empty;

            /// <summary>
            /// Oracle
            /// </summary>
            public static string Oracle { get; set; } = string.Empty;

            /// <summary>
            /// PostgreSQL
            /// </summary>
            public static string PostgreSQL { get; set; } = string.Empty;
        }

        /// <summary>
        /// 控制台打印
        /// </summary>
        public static bool Console { get; set; }

        /// <summary>
        /// 日志打印
        /// </summary>
        public static class Logging
        {
            /// <summary>
            /// 普通日志
            /// </summary>
            public static bool Info { get; set; }

            /// <summary>
            /// 错误日志
            /// </summary>
            public static bool Error { get; set; }
        }
    }

    /// <summary>
    /// 缓存
    /// </summary>
    public static class Cache
    {
        /// <summary>
        /// 同步时间
        /// </summary>
        public static int SyncTimeout { get; set; }

        /// <summary>
        /// 内存
        /// </summary>
        public static class MemoryCache
        {
            /// <summary>
            /// 是否可用
            /// </summary>
            public static bool IsEnabled { get; set; }
        }

        /// <summary>
        /// 分布式
        /// </summary>
        public static class Distributedcache
        {
            /// <summary>
            /// 是否可用
            /// </summary>
            public static bool IsEnabled { get; set; }

            /// <summary>
            /// Redis
            /// </summary>
            public static class Redis
            {
                /// <summary>
                /// 连接字符串
                /// </summary>
                public static string ConnectionString { get; set; } = string.Empty;

                /// <summary>
                /// 实例名称
                /// </summary>
                public static string InstanceName { get; set; } = string.Empty;
            }
        }

        /// <summary>
        /// 响应缓存
        /// </summary>
        public static class ResponseCache
        {
            /// <summary>
            /// 是否可用
            /// </summary>
            public static bool IsEnabled { get; set; }
        }
    }

    /// <summary>
    /// 文档
    /// </summary>
    public static class Swagger
    {
        /// <summary>
        /// 路由前缀
        /// </summary>
        public static string RoutePrefix { get; set; } = string.Empty;

        /// <summary>
        /// 分组
        /// </summary>
        public static string[] PublishGroup { get; set; } = Array.Empty<string>();
    }

    /// <summary>
    /// 性能分析
    /// </summary>
    public static class Miniprofiler
    {
        /// <summary>
        /// 是否可用
        /// </summary>
        public static bool IsEnabled { get; set; }
    }

    /// <summary>
    /// 授权
    /// </summary>
    public static class Auth
    {
        /// <summary>
        /// JWT
        /// </summary>
        public static class JWT
        {
            /// <summary>
            /// 颁发者
            /// </summary>
            public static string Issuer { get; set; } = string.Empty;

            /// <summary>
            /// 签收者
            /// </summary>
            public static string Audience { get; set; } = string.Empty;

            /// <summary>
            /// 秘钥
            /// </summary>
            public static string SymmetricKey { get; set; } = string.Empty;

            /// <summary>
            /// 过期时间容错值
            /// </summary>
            public static int ClockSkew { get; set; }

            /// <summary>
            /// 过期时间
            /// </summary>
            public static int Expires { get; set; }
        }

        /// <summary>
        /// 授权协议
        /// </summary>
        public static class Oauth
        {
            /// <summary>
            /// QQ
            /// </summary>
            public static class QQ
            {
                /// <summary>
                /// 是否可用
                /// </summary>
                public static bool IsEnabled { get; set; }

                /// <summary>
                /// ClientId
                /// </summary>
                public static string ClientId { get; set; } = string.Empty;

                /// <summary>
                /// ClientSecret
                /// </summary>
                public static string ClientSecret { get; set; } = string.Empty;

                /// <summary>
                /// Scope
                /// </summary>
                public static string Scope { get; set; } = string.Empty;

                /// <summary>
                /// RedirectUrl
                /// </summary>
                public static string RedirectUrl { get; set; } = string.Empty;
            }

            /// <summary>
            /// Github
            /// </summary>
            public static class Github
            {
                /// <summary>
                /// 是否可用
                /// </summary>
                public static bool IsEnabled { get; set; }

                /// <summary>
                /// ClientID
                /// </summary>
                public static string ClientID { get; set; } = string.Empty;

                /// <summary>
                /// ClientSecret
                /// </summary>
                public static string ClientSecret { get; set; } = string.Empty;

                /// <summary>
                /// Scope
                /// </summary>
                public static string Scope { get; set; } = string.Empty;

                /// <summary>
                /// RedirectUri
                /// </summary>
                public static string RedirectUri { get; set; } = string.Empty;
            }

            /// <summary>
            /// Gitee
            /// </summary>
            public static class Gitee
            {
                /// <summary>
                /// 是否可用
                /// </summary>
                public static bool IsEnabled { get; set; }

                /// <summary>
                /// ClientID
                /// </summary>
                public static string ClientID { get; set; } = string.Empty;

                /// <summary>
                /// ClientSecret
                /// </summary>
                public static string ClientSecret { get; set; } = string.Empty;

                /// <summary>
                /// Scope
                /// </summary>
                public static string Scope { get; set; } = string.Empty;

                /// <summary>
                /// RedirectUri
                /// </summary>
                public static string RedirectUri { get; set; } = string.Empty;
            }
        }
    }

    /// <summary>
    /// 网络分发
    /// </summary>
    public static class CDN
    {
        /// <summary>
        /// 是否可用
        /// </summary>
        public static bool IsEnabled { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public static string Type { get; set; } = string.Empty;

        /// <summary>
        /// 腾讯云
        /// </summary>
        public static class Tencentcloud
        {
            /// <summary>
            /// SecretId
            /// </summary>
            public static string SecretId { get; set; } = string.Empty;

            /// <summary>
            /// SecretKey
            /// </summary>
            public static string SecretKey { get; set; } = string.Empty;
        }
    }

    /// <summary>
    /// 任务
    /// </summary>
    public static class Job
    {
        /// <summary>
        /// 是否可用
        /// </summary>
        public static bool IsEnabled { get; set; }

        /// <summary>
        /// 表达式
        /// </summary>
        public static string Cron { get; set; } = string.Empty;
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
            /// <summary>
            /// WebHookUrl
            /// </summary>
            public static string WebHookUrl { get; set; } = string.Empty;

            /// <summary>
            /// AccessToken
            /// </summary>
            public static string AccessToken { get; set; } = string.Empty;

            /// <summary>
            /// Secret
            /// </summary>
            public static string Secret { get; set; } = string.Empty;
        }

        /// <summary>
        /// 微信
        /// </summary>
        public static class WeChart
        {
            /// <summary>
            /// WebHookUrl
            /// </summary>
            public static string WebHookUrl { get; set; } = string.Empty;

            /// <summary>
            /// UploadkUrl
            /// </summary>
            public static string UploadkUrl { get; set; } = string.Empty;

            /// <summary>
            /// Key
            /// </summary>
            public static string Key { get; set; } = string.Empty;
        }

        /// <summary>
        /// 邮件
        /// </summary>
        public static class Email
        {
            /// <summary>
            /// 主机
            /// </summary>
            public static string Host { get; set; } = string.Empty;

            /// <summary>
            /// 端口
            /// </summary>
            public static int Port { get; set; }

            /// <summary>
            /// 是否SSL加密
            /// </summary>
            public static bool UseSsl { get; set; }

            /// <summary>
            /// 发自
            /// </summary>
            public static class From
            {
                /// <summary>
                /// 名称
                /// </summary>
                public static string UserName { get; set; } = string.Empty;

                /// <summary>
                /// 密码
                /// </summary>
                public static string Password { get; set; } = string.Empty;

                /// <summary>
                /// 地址
                /// </summary>
                public static string Address { get; set; } = string.Empty;
            }
        }
    }
}