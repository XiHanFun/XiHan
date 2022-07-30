// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomSqlSugarExtension
// Guid:49736281-4a15-48db-ba3e-b66124a931d4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-26 下午 06:24:14
// ----------------------------------------------------------------

using Newtonsoft.Json;
using SqlSugar.IOC;
using ZhaiFanhuaBlog.Utils.Config;
using ZhaiFanhuaBlog.Utils.Console;

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.DependencyInjection;

/// <summary>
/// CustomSqlSugarExtension
/// </summary>
public static class CustomSqlSugarExtension
{
    /// <summary>
    /// SqlSugar服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomSqlSugar(this IServiceCollection services)
    {
        string databaseType = ConfigHelper.Configuration.GetValue<string>("Database:Type");
        services.AddSqlSugar(databaseType switch
        {
            "MySql" => new IocConfig()
            {
                DbType = IocDbType.MySql,
                ConnectionString = ConfigHelper.Configuration.GetValue<string>("Database:ConnectionString:MySql"),
                IsAutoCloseConnection = true
            },
            "SqlServer" => new IocConfig()
            {
                DbType = IocDbType.SqlServer,
                ConnectionString = ConfigHelper.Configuration.GetValue<string>("Database:ConnectionString:SqlServer"),
                IsAutoCloseConnection = true
            },
            "Sqlite" => new IocConfig()
            {
                DbType = IocDbType.Sqlite,
                ConnectionString = ConfigHelper.Configuration.GetValue<string>("Database:ConnectionString:Sqlite"),
                IsAutoCloseConnection = true
            },
            "Oracle" => new IocConfig()
            {
                DbType = IocDbType.Oracle,
                ConnectionString = ConfigHelper.Configuration.GetValue<string>("Database:ConnectionString:Oracle"),
                IsAutoCloseConnection = true
            },
            "PostgreSQL" => new IocConfig()
            {
                DbType = IocDbType.PostgreSQL,
                ConnectionString = ConfigHelper.Configuration.GetValue<string>("Database:ConnectionString:PostgreSQL"),
                IsAutoCloseConnection = true
            },
            _ => new IocConfig()
            {
                DbType = IocDbType.SqlServer,
                ConnectionString = ConfigHelper.Configuration.GetValue<string>("Database:ConnectionString:SqlServer"),
                IsAutoCloseConnection = true
            }
        });
        bool databaseConsole = ConfigHelper.Configuration.GetValue<bool>("Database:Console");
        if (databaseConsole)
        {
            services.ConfigurationSugar(db =>
            {
                // SQL语句输出方便排查问题
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    ConsoleHelper.WriteLineHandle("===========================================================================");
                    ConsoleHelper.WriteLineHandle($"{DateTime.Now}，SQL语句：");
                    ConsoleHelper.WriteLineHandle(sql);
                    ConsoleHelper.WriteLineHandle("===========================================================================");
                };
            });
        }
        return services;
    }
}