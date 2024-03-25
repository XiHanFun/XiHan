#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AppSettings
// Guid:075a4b94-d8d4-4b4e-8e13-83ae6b03e16c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-07-22 下午 12:21:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.Bases.Apps.Configs;

/// <summary>
/// 应用配置类
/// </summary>
public static class AppSettings
{
    #region 公共配置

    /// <summary>
    /// 是否演示模式
    /// </summary>
    public static bool IsDemoMode { get; set; }

    /// <summary>
    /// 环境
    /// </summary>
    public static string EnvironmentName { get; set; } = string.Empty;

    /// <summary>
    /// 端口
    /// </summary>
    public static int Port { get; set; }

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
        /// 域名
        /// </summary>
        public static string[] Origins { get; set; } = [];

        /// <summary>
        /// 请求头
        /// </summary>
        public static string[] Headers { get; set; } = [];
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
        public static string[] PublishGroup { get; set; } = [];
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
        /// Jwt
        /// </summary>
        public static class Jwt
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
            /// 过期时间
            /// </summary>
            public static int Expires { get; set; }

            /// <summary>
            /// 过期时间容错值
            /// </summary>
            public static int ClockSkew { get; set; }
        }

        /// <summary>
        /// QQ 授权配置
        /// </summary>
        public static class Qq
        {
            /// <summary>
            /// 客户端(AppId)
            /// </summary>
            public static string ClientId { get; set; } = string.Empty;

            /// <summary>
            /// 客户端密钥(AppKey)
            /// </summary>
            public static string ClientSecret { get; set; } = string.Empty;
        }

        /// <summary>
        /// WeChat 授权配置
        /// </summary>
        public static class WeChat
        {
            /// <summary>
            /// 客户端(AppId)
            /// </summary>
            public static string ClientId { get; set; } = string.Empty;

            /// <summary>
            /// 客户端密钥(AppKey)
            /// </summary>
            public static string ClientSecret { get; set; } = string.Empty;
        }

        /// <summary>
        /// Alipay 授权配置
        /// </summary>
        public static class Alipay
        {
            /// <summary>
            /// 客户端(AppId)
            /// </summary>
            public static string ClientId { get; set; } = string.Empty;

            /// <summary>
            /// 客户端密钥(AppKey)
            /// </summary>
            public static string ClientSecret { get; set; } = string.Empty;
        }

        /// <summary>
        /// Github 授权配置
        /// </summary>
        public static class Github
        {
            /// <summary>
            /// 客户端(AppId)
            /// </summary>
            public static string ClientId { get; set; } = string.Empty;

            /// <summary>
            /// 客户端密钥(AppKey)
            /// </summary>
            public static string ClientSecret { get; set; } = string.Empty;
        }

        /// <summary>
        /// Gitlab 授权配置
        /// </summary>
        public static class Gitlab
        {
            /// <summary>
            /// 客户端(AppId)
            /// </summary>
            public static string ClientId { get; set; } = string.Empty;

            /// <summary>
            /// 客户端密钥(AppKey)
            /// </summary>
            public static string ClientSecret { get; set; } = string.Empty;
        }

        /// <summary>
        /// Gitee 授权配置
        /// </summary>
        public static class Gitee
        {
            /// <summary>
            /// 客户端(AppId)
            /// </summary>
            public static string ClientId { get; set; } = string.Empty;

            /// <summary>
            /// 客户端密钥(AppKey)
            /// </summary>
            public static string ClientSecret { get; set; } = string.Empty;
        }
    }

    #endregion

    #region 特定配置

    /// <summary>
    /// 数据库
    /// </summary>
    public static class Database
    {
        /// <summary>
        /// 连接配置
        /// </summary>
        public static DatabaseConfig[] DatabaseConfigs { get; set; } = [];

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

        /// <summary>
        /// 是否初始化数据库
        /// </summary>
        public static bool EnableInitDb { get; set; }

        /// <summary>
        /// 是否初始化种子数据
        /// </summary>
        public static bool EnableInitSeed { get; set; }
    }

    /// <summary>
    /// RabbitMq
    /// </summary>
    public static class RabbitMq
    {
        /// <summary>
        /// 是否可用
        /// </summary>
        public static bool Enabled { get; set; }

        /// <summary>
        /// 主机名称
        /// </summary>
        public static string HostName { get; set; } = string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        public static string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        public static string Password { get; set; } = string.Empty;

        /// <summary>
        /// 端口
        /// </summary>
        public static int ThePort { get; set; }

        /// <summary>
        /// 重试次数
        /// </summary>
        public static int RetryCount { get; set; }
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
        /// 分布式
        /// </summary>
        public static class RedisCache
        {
            /// <summary>
            /// 是否可用
            /// </summary>
            public static bool IsEnabled { get; set; }

            /// <summary>
            /// 连接字符串
            /// </summary>
            public static string ConnectionString { get; set; } = string.Empty;

            /// <summary>
            /// 前辍
            /// </summary>
            public static string Prefix { get; set; } = string.Empty;
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

    #endregion
}

/// <summary>
/// 数据库配置
/// </summary>
public class DatabaseConfig
{
    /// <summary>
    /// 连接Id
    /// </summary>
    public int ConfigId { get; set; }

    /// <summary>
    /// 数据库类型
    /// DataBaseTypeEnum
    /// </summary>
    public string DataBaseType { get; set; } = string.Empty;

    /// <summary>
    /// 连接字符串
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// 是否自动关闭连接
    /// </summary>
    public bool IsAutoCloseConnection { get; set; }
}