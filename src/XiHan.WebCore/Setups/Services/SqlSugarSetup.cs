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
using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Models.Bases.Entity;
using XiHan.Models.Bases.Interface;
using XiHan.Models.Syses;
using XiHan.Services.Syses.Configs;
using XiHan.Services.Syses.Configs.Logic;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;
using XiHan.Utils.Reflections;

namespace XiHan.WebCore.Setups.Services;

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

            // 设置超时时间
            client.Ado.CommandTimeOut = 30;
            // 执行SQL语句
            client.Aop.OnLogExecuting = (sql, pars) =>
            {
                var param = client.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value));
                var sqlInfo = $"【执行SQL语句】" + Environment.NewLine + UtilMethods.GetSqlString(config.DbType, sql, pars) + Environment.NewLine;
                // SQL控制台打印
                if (databaseConsole)
                {
                    if (sql.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                        sqlInfo.WriteLineHandle();
                    if (sql.StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase) || sql.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
                        sqlInfo.WriteLineWarning();
                    if (sql.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase))
                        sqlInfo.WriteLineError();
                }
                // SQL日志打印
                if (databaseLogInfo) Log.Information(sqlInfo);
            };
            // 执行SQL时间
            client.Aop.OnLogExecuted = (_, _) =>
            {
                var handleInfo = $"【执行SQL时间】" + Environment.NewLine + client.Ado.SqlExecutionTime + Environment.NewLine;
                if (databaseConsole) handleInfo.WriteLineHandle();
                if (databaseLogInfo) Log.Information(handleInfo);
            };
            // 执行SQL出错
            client.Aop.OnError = exp =>
            {
                var errorInfo = $"【执行SQL出错】" + Environment.NewLine + exp.Message + Environment.NewLine
                                + UtilMethods.GetSqlString(config.DbType, exp.Sql, (SugarParameter[])exp.Parametres) + Environment.NewLine;
                if (databaseConsole) errorInfo.WriteLineError();
                if (databaseLogError) Log.Error(exp, errorInfo);
            };
            // 数据审计
            client.Aop.DataExecuting = (oldValue, entityInfo) =>
            {
                // 演示环境判断
                if (entityInfo.EntityColumnInfo.IsPrimarykey)
                {
                    var entityNames = new List<string>() {
                        nameof(SysJob),
                        nameof(SysLogException),
                        nameof(SysLogJob),
                        nameof(SysLogLogin),
                        nameof(SysLogOperation)
                    };
                    if (entityNames.Any(name => !name.Contains(entityInfo.EntityName)))
                    {
                        var isDemoMode = App.GetRequiredService<ISysConfigService>().GetSysConfigValueByCode<bool>(AppGlobalConstant.IsDemoMode).GetAwaiter().GetResult();
                        if (isDemoMode) throw new CustomException("演示环境禁止修改数据！");
                    }
                }
            };
        });
        return services;
    }
}