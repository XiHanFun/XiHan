#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SqlSugarSetup
// Guid:49736281-4a15-48db-ba3e-b66124a931d4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-26 下午 06:24:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SqlSugar.IOC;
using ZhaiFanhuaBlog.Infrastructure.Apps.Setting;
using ZhaiFanhuaBlog.Utils.Console;

namespace ZhaiFanhuaBlog.Extensions.Setups;

/// <summary>
/// SqlSugarSetup
/// </summary>
public static class SqlSugarSetup
{
    /// <summary>
    /// SqlSugar 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSqlSugarSetup(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var databaseType = AppSettings.Database.Type.Get();
        var databaseConsole = AppSettings.Database.Console.Get();
        var databaseLogInfo = AppSettings.Database.Logging.Info.Get();
        var databaseLogError = AppSettings.Database.Logging.Error.Get();

        services.AddSqlSugar(databaseType switch
        {
            "MySql" => new IocConfig
            {
                DbType = IocDbType.MySql,
                ConnectionString = AppSettings.Database.ConnectionString.MySql.Get(),
                IsAutoCloseConnection = true
            },
            "SqlServer" => new IocConfig
            {
                DbType = IocDbType.SqlServer,
                ConnectionString = AppSettings.Database.ConnectionString.SqlServer.Get(),
                IsAutoCloseConnection = true
            },
            "Sqlite" => new IocConfig
            {
                DbType = IocDbType.Sqlite,
                ConnectionString = AppSettings.Database.ConnectionString.Sqlite.Get(),
                IsAutoCloseConnection = true
            },
            "Oracle" => new IocConfig
            {
                DbType = IocDbType.Oracle,
                ConnectionString = AppSettings.Database.ConnectionString.Oracle.Get(),
                IsAutoCloseConnection = true
            },
            "PostgreSQL" => new IocConfig
            {
                DbType = IocDbType.PostgreSQL,
                ConnectionString = AppSettings.Database.ConnectionString.PostgreSQL.Get(),
                IsAutoCloseConnection = true
            },
            _ => new IocConfig
            {
                DbType = IocDbType.SqlServer,
                ConnectionString = AppSettings.Database.ConnectionString.SqlServer.Get(),
                IsAutoCloseConnection = true
            }
        });

        services.ConfigurationSugar(client =>
        {
            // SQL语句输出方便排查问题
            client.Aop.OnLogExecuting = (sql, pars) =>
            {
                var param = client.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value));
                var info = $"SQL语句:" + Environment.NewLine + $"{sql}，{param}";
                if (databaseConsole)
                    info.WriteLineHandle();
                if (databaseLogInfo)
                    Log.Information(info);
            };
            client.Aop.OnLogExecuted = (sql, pars) =>
            {
                var handle = $"SQL时间:" + Environment.NewLine + client.Ado.SqlExecutionTime;
                if (databaseConsole)
                    handle.WriteLineHandle();
                if (databaseLogInfo)
                    Log.Information(handle);
            };
            client.Aop.OnError = (exp) =>
            {
                if (databaseLogError)
                    Log.Error(exp, $"SQL出错：{exp.Message}");
            };
        });
        return services;
    }
}