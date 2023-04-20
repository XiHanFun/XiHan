#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AppSettings
// Guid:075a4b94-d8d4-4b4e-8e13-83ae6b03e16c
// Author:zhaifanhua
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
    /// 日志
    /// </summary>
    public static class LogConfig
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
    /// 系统
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
    /// RabbitMQ
    /// </summary>
    public static class RabbitMQ
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
        public static int Port { get; set; }

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
            /// 过期时间容错值
            /// </summary>
            public static int ClockSkew { get; set; }

            /// <summary>
            /// 过期时间
            /// </summary>
            public static int Expires { get; set; }
        }
    }
}