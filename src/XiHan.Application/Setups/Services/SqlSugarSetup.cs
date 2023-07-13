#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SqlSugarSetup
// Guid:49736281-4a15-48db-ba3e-b66124a931d4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-26 下午 06:24:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SqlSugar;
using SqlSugar.IOC;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Utils.Extensions;

namespace XiHan.Application.Setups.Services;

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
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddSqlSugarSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        var databaseType = AppSettings.Database.Type.GetValue();
        var databaseConsole = AppSettings.Database.Console.GetValue();
        var databaseLogInfo = AppSettings.Database.Logging.Info.GetValue();
        var databaseLogError = AppSettings.Database.Logging.Error.GetValue();

        services.AddSqlSugar(databaseType switch
        {
            "MySql" => new IocConfig
            {
                DbType = IocDbType.MySql,
                ConnectionString = AppSettings.Database.ConnectionString.MySql.GetValue(),
                IsAutoCloseConnection = true
            },
            "SqlServer" => new IocConfig
            {
                DbType = IocDbType.SqlServer,
                ConnectionString = AppSettings.Database.ConnectionString.SqlServer.GetValue(),
                IsAutoCloseConnection = true
            },
            "Sqlite" => new IocConfig
            {
                DbType = IocDbType.Sqlite,
                ConnectionString = AppSettings.Database.ConnectionString.Sqlite.GetValue(),
                IsAutoCloseConnection = true
            },
            "Oracle" => new IocConfig
            {
                DbType = IocDbType.Oracle,
                ConnectionString = AppSettings.Database.ConnectionString.Oracle.GetValue(),
                IsAutoCloseConnection = true
            },
            "PostgreSQL" => new IocConfig
            {
                DbType = IocDbType.PostgreSQL,
                ConnectionString = AppSettings.Database.ConnectionString.PostgreSql.GetValue(),
                IsAutoCloseConnection = true
            },
            _ => new IocConfig
            {
                DbType = IocDbType.MySql,
                ConnectionString = AppSettings.Database.ConnectionString.MySql.GetValue(),
                IsAutoCloseConnection = true
            }
        });

        services.ConfigurationSugar(client =>
        {
            var config = client.CurrentConnectionConfig;
            // SQL语句输出方便排查问题
            client.Aop.OnLogExecuting = (sql, pars) =>
            {
                var param = client.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value));
                var info = $"SQL语句:" + Environment.NewLine + UtilMethods.GetSqlString(config.DbType, sql, pars);
                if (databaseConsole) info.WriteLineHandle();
                if (databaseLogInfo) Log.Information(info);
            };
            client.Aop.OnLogExecuted = (_, _) =>
            {
                var handle = $"SQL时间:" + Environment.NewLine + client.Ado.SqlExecutionTime;
                if (databaseConsole) handle.WriteLineHandle();
                if (databaseLogInfo) Log.Information(handle);
            };
            client.Aop.OnError = exp =>
            {
                var errorInfo = $"SQL出错:" + Environment.NewLine + exp.Message + Environment.NewLine +
                    UtilMethods.GetSqlString(config.DbType, exp.Sql, (SugarParameter[])exp.Parametres);
                if (databaseConsole) errorInfo.WriteLineError();
                if (databaseLogError) Log.Error(exp, errorInfo);
            };
        });
        return services;
    }
}