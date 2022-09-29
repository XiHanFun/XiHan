// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SqlSugarSetup
// Guid:49736281-4a15-48db-ba3e-b66124a931d4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-26 下午 06:24:14
// ----------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using SqlSugar.IOC;
using ZhaiFanhuaBlog.Core.AppSettings;
using ZhaiFanhuaBlog.Utils.Console;

namespace ZhaiFanhuaBlog.Setups;

/// <summary>
/// SqlSugarSetup
/// </summary>
public static class SqlSugarSetup
{
    /// <summary>
    /// SqlSugar服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSqlSugarSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        string databaseType = AppSettings.Database.Type;
        services.AddSqlSugar(databaseType switch
        {
            "MySql" => new IocConfig()
            {
                DbType = IocDbType.MySql,
                ConnectionString = AppSettings.Database.Connectionstring.MySql,
                IsAutoCloseConnection = true
            },
            "SqlServer" => new IocConfig()
            {
                DbType = IocDbType.SqlServer,
                ConnectionString = AppSettings.Database.Connectionstring.SqlServer,
                IsAutoCloseConnection = true
            },
            "Sqlite" => new IocConfig()
            {
                DbType = IocDbType.Sqlite,
                ConnectionString = AppSettings.Database.Connectionstring.Sqlite,
                IsAutoCloseConnection = true
            },
            "Oracle" => new IocConfig()
            {
                DbType = IocDbType.Oracle,
                ConnectionString = AppSettings.Database.Connectionstring.Oracle,
                IsAutoCloseConnection = true
            },
            "PostgreSQL" => new IocConfig()
            {
                DbType = IocDbType.PostgreSQL,
                ConnectionString = AppSettings.Database.Connectionstring.PostgreSQL,
                IsAutoCloseConnection = true
            },
            _ => new IocConfig()
            {
                DbType = IocDbType.SqlServer,
                ConnectionString = AppSettings.Database.Connectionstring.SqlServer,
                IsAutoCloseConnection = true
            }
        });
        bool databaseConsole = AppSettings.Database.Console;
        if (databaseConsole)
        {
            services.ConfigurationSugar(db =>
            {
                // SQL语句输出方便排查问题
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    ConsoleHelper.WriteLineHandle("===========================================================================");
                    ConsoleHelper.WriteLineHandle($"{DateTime.Now}，SQL语句：");
                    ConsoleHelper.WriteLineHandle(sql + Environment.NewLine + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                    ConsoleHelper.WriteLineHandle("===========================================================================");
                };
            });
        }
        return services;
    }
}