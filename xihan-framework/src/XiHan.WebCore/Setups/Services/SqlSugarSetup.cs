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
using StackExchange.Profiling;
using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Models.Bases.Interface;
using XiHan.Repositories.Bases;
using XiHan.Repositories.Extensions;
using XiHan.Utils.Extensions;
using XiHan.WebCore.Extensions;

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
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // 注入参考，官方文档 https://www.donet5.com/Home/Doc?typeId=2405
        DatabaseConfig[] dbConfigs = AppSettings.Database.DatabaseConfigs.GetSection();
        List<ConnectionConfig> connectionConfigs = dbConfigs.Select(config => new ConnectionConfig()
        {
            ConfigId = config.ConfigId,
            DbType = config.DataBaseType.GetEnumByName<DataBaseTypeEnum>().ConvertDbType(),
            ConnectionString = config.ConnectionString,
            IsAutoCloseConnection = config.IsAutoCloseConnection
        }).ToList();

        SqlSugarScope sqlSugar = new(connectionConfigs, client =>
        {
            connectionConfigs.ForEach(config =>
            {
                dynamic dbProvider = client.GetConnectionScope(config.ConfigId);
                SetSugarAop(dbProvider);
            });
        });

        // 单例注册
        _ = services.AddSingleton<ISqlSugarClient>(sqlSugar);
        // 仓储注册
        _ = services.AddScoped(typeof(BaseRepository<>));

        return services;
    }

    /// <summary>
    /// 配置数据库 Aop 设置
    /// </summary>
    /// <param name="dbProvider"></param>
    private static void SetSugarAop(SqlSugarScopeProvider dbProvider)
    {
        bool databaseConsole = AppSettings.Database.Console.GetValue();
        bool databaseLogInfo = AppSettings.Database.Logging.Info.GetValue();
        bool databaseLogError = AppSettings.Database.Logging.Error.GetValue();

        ConnectionConfig config = dbProvider.CurrentConnectionConfig;
        dynamic configId = config.ConfigId;

        // 设置超时时间
        dbProvider.Ado.CommandTimeOut = 30;

        // 执行SQL数据
        dbProvider.Aop.DataExecuting = (value, entity) =>
        {
            switch (entity.OperationType)
            {
                // 新增操作
                case DataFilterType.InsertByObject:
                    //自动设置主键
                    if (entity.EntityColumnInfo.IsPrimarykey && entity.EntityValue is IBaseIdEntity<long> { BaseId: 0 } entityInfo)
                    {
                        entityInfo.BaseId = App.GetSnowflakeId();
                    }
                    _ = entity.EntityValue.ToCreated();
                    break;
                // 更新操作
                case DataFilterType.UpdateByObject:
                    _ = entity.EntityValue.ToModified();
                    break;
                // 删除操作
                case DataFilterType.DeleteByObject:
                    _ = entity.EntityValue.ToDeleted();
                    break;
            }
        };

        // 执行SQL日志
        dbProvider.Aop.OnLogExecuting = (sql, pars) =>
        {
            string param = dbProvider.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value));
            string sqlInfo = $"【数据库{configId}】执行SQL语句：" + Environment.NewLine +
                          UtilMethods.GetSqlString(config.DbType, sql, pars);
            _ = MiniProfiler.Current.CustomTiming("执行SQL日志", sqlInfo);
            // SQL控制台打印
            if (databaseConsole)
            {
                if (sql.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                {
                    sqlInfo.WriteLineHandle();
                }

                if (sql.TrimStart().StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase) || sql.TrimStart().StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
                {
                    sqlInfo.WriteLineWarning();
                }

                if (sql.TrimStart().StartsWith("DELETE", StringComparison.OrdinalIgnoreCase) || sql.TrimStart().StartsWith("TRUNCATE", StringComparison.OrdinalIgnoreCase))
                {
                    sqlInfo.WriteLineError();
                }
            }
            // SQL日志打印
            if (databaseLogInfo)
            {
                Log.Information(sqlInfo);
            }
        };

        // 执行SQL时间
        dbProvider.Aop.OnLogExecuted = (_, _) =>
        {
            string handleInfo = $"【数据库{configId}】执行SQL时间：" + Environment.NewLine +
                             dbProvider.Ado.SqlExecutionTime;
            _ = MiniProfiler.Current.CustomTiming("执行SQL时间", handleInfo);
            if (databaseConsole)
            {
                handleInfo.WriteLineHandle();
            }

            if (databaseLogInfo)
            {
                Log.Information(handleInfo);
            }
        };

        // 执行SQL出错
        dbProvider.Aop.OnError = exp =>
        {
            string errorInfo = $"【数据库{configId}】执行SQL出错：" + Environment.NewLine +
                            exp.Message + Environment.NewLine +
                            UtilMethods.GetSqlString(config.DbType, exp.Sql, (SugarParameter[])exp.Parametres);
            _ = MiniProfiler.Current.CustomTiming("执行SQL出错", errorInfo);
            if (databaseConsole)
            {
                errorInfo.WriteLineError();
            }

            if (databaseLogError)
            {
                Log.Error(exp, errorInfo);
            }
        };
    }
}