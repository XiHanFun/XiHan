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
using XiHan.Infrastructures.Apps.HttpContexts;
using XiHan.Models.Bases.Filters;
using XiHan.Models.Bases.Interface;
using XiHan.Models.Syses;
using XiHan.Repositories.Bases;
using XiHan.Repositories.Extensions;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;
using XiHan.WebCore.Common.SqlSugar;

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
        ArgumentNullException.ThrowIfNull(services);

        // 注入多库参考，官方文档 https://www.donet5.com/Home/Doc?typeId=2405
        var connectionConfigs = SqlSugarConfig.GetConnectionConfigs();
        SqlSugarScope sugarScope = new(connectionConfigs, client =>
        {
            connectionConfigs.ForEach(config =>
            {
                var dbProvider = client.GetConnectionScope(config.ConfigId);

                // 动态添加全局过滤器参考，官方文档 https://www.donet5.com/home/doc?masterId=1&typeId=1205
                // 全局过滤器，作用是设置一个查询条件，当你使用查询操作的时候满足这个条件，那么你的语句就会附加你设置的条件。应用场景：过滤假删除数据，比如，每个查询后面都要加 IsDeleted = false

                // 获取当前请求上下文信息
                var httpCurrent = App.HttpContextCurrent;
                {
                    var user = httpCurrent.GetAuthInfo();
                    // 非超级管理员或未登录用户，添加过滤假删除数据的条件
                    client.QueryFilter.AddTableFilterIF<ISoftDeleteFilter>(user.IsSuperAdmin == false, it => it.IsDeleted == false);
                }

                SetSugarAop(dbProvider);
            });
        });

        // 单例注册
        _ = services.AddSingleton<ISqlSugarClient>(sugarScope);
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
        var databaseConsole = AppSettings.Database.Console.GetValue();
        var databaseLogInfo = AppSettings.Database.Logging.Info.GetValue();
        var databaseLogError = AppSettings.Database.Logging.Error.GetValue();

        var config = dbProvider.CurrentConnectionConfig;
        var configId = config.ConfigId;

        // 设置超时时间
        dbProvider.Ado.CommandTimeOut = 30;

        // 执行SQL数据
        dbProvider.Aop.DataExecuting = (value, entity) =>
        {
            // 演示环境判断
            if (entity.EntityColumnInfo.IsPrimarykey)
            {
                var entityNames = new List<string>()
                {
                    nameof(SysJob),
                    nameof(SysJobLog),
                    nameof(SysLogVisit),
                    nameof(SysLogOperation),
                    nameof(SysLogLogin),
                    nameof(SysLogException)
                };
                if (entityNames.Any(name => !name.Contains(entity.EntityName)))
                {
                    var isDemoMode = AppSettings.IsDemoMode.GetValue();
                    if (isDemoMode) throw new CustomException("演示环境禁止修改数据！");
                }
            }

            switch (entity.OperationType)
            {
                // 新增操作
                case DataFilterType.InsertByObject:
                    // 自动设置主键
                    if (entity.EntityColumnInfo.IsPrimarykey && entity.EntityValue is IBaseIdEntity<long>
                        {
                            BaseId: 0
                        } entityInfo) entityInfo.BaseId = App.GetSnowflakeId();
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
            var param = dbProvider.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value));
            var sqlInfo = $"【数据库{configId}】执行SQL语句：" + Environment.NewLine + UtilMethods.GetSqlString(config.DbType, sql, pars);
            // SQL控制台打印
            if (databaseConsole)
            {
                if (sql.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase)) sqlInfo.WriteLineHandle();

                if (sql.TrimStart().StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase) ||
                    sql.TrimStart().StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
                    sqlInfo.WriteLineWarning();

                if (sql.TrimStart().StartsWith("DELETE", StringComparison.OrdinalIgnoreCase) ||
                    sql.TrimStart().StartsWith("TRUNCATE", StringComparison.OrdinalIgnoreCase))
                    sqlInfo.WriteLineError();
            }

            // SQL日志打印
            if (databaseLogInfo) Log.Information(sqlInfo);
        };

        // 执行SQL时间
        dbProvider.Aop.OnLogExecuted = (_, _) =>
        {
            var handleInfo = $"【数据库{configId}】执行SQL时间：" + Environment.NewLine +
                dbProvider.Ado.SqlExecutionTime;
            _ = MiniProfiler.Current.CustomTiming("执行SQL时间", handleInfo);
            if (databaseConsole) handleInfo.WriteLineHandle();

            if (databaseLogInfo) Log.Information(handleInfo);
        };

        // 执行SQL出错
        dbProvider.Aop.OnError = exp =>
        {
            var errorInfo = $"【数据库{configId}】执行SQL出错：" + Environment.NewLine +
                exp.Message + Environment.NewLine +
                UtilMethods.GetSqlString(config.DbType, exp.Sql, (SugarParameter[])exp.Parametres);
            _ = MiniProfiler.Current.CustomTiming("执行SQL出错", errorInfo);
            if (databaseConsole) errorInfo.WriteLineError();

            if (databaseLogError) Log.Error(exp, errorInfo);
        };
    }
}