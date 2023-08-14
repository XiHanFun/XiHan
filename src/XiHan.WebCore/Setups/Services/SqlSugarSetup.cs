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

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl.AdoJobStore.Common;
using Serilog;
using SqlSugar;
using SqlSugar.IOC;
using System.Collections;
using System.Reflection;
using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Consts;
using XiHan.Models.Bases;
using XiHan.Models.Bases.Attributes;
using XiHan.Models.Syses;
using XiHan.Repositories.Bases;
using XiHan.Services.Syses.Configs;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;
using XiHan.Utils.Reflections;
using Yitter.IdGenerator;

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

        // 雪花 Id 生成服务
        var options = new IdGeneratorOptions
        {
            WorkerId = 1,
            WorkerIdBitLength = 1,
            SeqBitLength = 6,
        };
        YitIdHelper.SetIdGenerator(options);
        StaticConfig.CustomSnowFlakeFunc = () =>
        {
            return YitIdHelper.NextId();
        };

        var dbConfigs = AppSettings.Database.DatabaseConfigs.GetSection();

        // 注入参考，官方文档 https://www.donet5.com/Home/Doc?typeId=2405
        var connConfigs = dbConfigs.Select(config => new ConnectionConfig()
        {
            ConfigId = config.ConfigId,
            DbType = config.DataBaseType.GetEnumByName<DataBaseTypeEnum>().ConvertDbType(),
            ConnectionString = config.ConnectionString,
            IsAutoCloseConnection = config.IsAutoCloseConnection
        }).ToList();

        var sqlSugar = new SqlSugarScope(connConfigs, client =>
        {
            connConfigs.ForEach(config =>
            {
                var dbProvider = client.GetConnectionScope(config.ConfigId);
                SetSugarAop(config, dbProvider);
            });
        });

        // 单例注册
        services.AddSingleton<ISqlSugarClient>(sqlSugar);
        // 仓储注册
        services.AddScoped(typeof(BaseRepository<>));

        connConfigs.ForEach(config =>
        {
            var dbProvider = sqlSugar.GetConnectionScope(config.ConfigId);
            InitDatabase(dbProvider);
            InitSeedData(dbProvider);
        });

        return services;
    }

    /// <summary>
    /// 配置数据库 Aop 设置
    /// </summary>
    /// <param name="config"></param>
    /// <param name="dbProvider"></param>
    private static void SetSugarAop(ConnectionConfig config, SqlSugarScopeProvider dbProvider)
    {
        var databaseConsole = AppSettings.Database.Console.GetValue();
        var databaseLogInfo = AppSettings.Database.Logging.Info.GetValue();
        var databaseLogError = AppSettings.Database.Logging.Error.GetValue();

        var configId = config.ConfigId;

        // 设置超时时间
        dbProvider.Ado.CommandTimeOut = 30;
        // 执行SQL语句
        dbProvider.Aop.OnLogExecuting = (sql, pars) =>
        {
            var param = dbProvider.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value));
            var sqlInfo = $"【数据库{configId}执行SQL语句】" + Environment.NewLine +
                          UtilMethods.GetSqlString(config.DbType, sql, pars);
            // SQL控制台打印
            if (databaseConsole)
            {
                if (sql.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                    sqlInfo.WriteLineHandle();
                if (sql.TrimStart().StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase) || sql.TrimStart().StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
                    sqlInfo.WriteLineWarning();
                if (sql.TrimStart().StartsWith("DELETE", StringComparison.OrdinalIgnoreCase) || sql.TrimStart().StartsWith("TRUNCATE", StringComparison.OrdinalIgnoreCase))
                    sqlInfo.WriteLineError();
            }
            // SQL日志打印
            if (databaseLogInfo) Log.Information(sqlInfo);
        };
        // 执行SQL时间
        dbProvider.Aop.OnLogExecuted = (_, _) =>
        {
            var handleInfo = $"【数据库{configId}执行SQL时间】" + Environment.NewLine +
                             dbProvider.Ado.SqlExecutionTime;
            if (databaseConsole) handleInfo.WriteLineHandle();
            if (databaseLogInfo) Log.Information(handleInfo);
        };
        // 执行SQL出错
        dbProvider.Aop.OnError = exp =>
        {
            var errorInfo = $"【数据库{configId}执行SQL出错】" + Environment.NewLine +
                            exp.Message + Environment.NewLine +
                            UtilMethods.GetSqlString(config.DbType, exp.Sql, (SugarParameter[])exp.Parametres);
            if (databaseConsole) errorInfo.WriteLineError();
            if (databaseLogError) Log.Error(exp, errorInfo);
        };
        //// 数据审计
        //dbProvider.Aop.DataExecuting = (oldValue, entityInfo) =>
        //{
        //    // 演示环境判断
        //    if (entityInfo.EntityColumnInfo.IsPrimarykey)
        //    {
        //        var entityNames = new List<string>() {
        //            nameof(SysJob),
        //            nameof(SysLogException),
        //            nameof(SysLogJob),
        //            nameof(SysLogLogin),
        //            nameof(SysLogOperation)
        //        };
        //        if (entityNames.Any(name => !name.Contains(entityInfo.EntityName)))
        //        {
        //            var sysConfigService = App.GetService<ISysConfigService>();
        //            var isDemoMode = sysConfigService.GetSysConfigValueByCode<bool>(GlobalConst.IsDemoMode).GetAwaiter().GetResult();
        //            if (isDemoMode) throw new CustomException("演示环境禁止修改数据！");
        //        }
        //    }
        //};
    }

    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <param name="dbProvider"></param>
    private static void InitDatabase(SqlSugarScopeProvider dbProvider)
    {
        try
        {
            "正在从配置中检测是否需要初始化数据库……".WriteLineInfo();
            var enableInitDb = AppSettings.Database.EnableInitDb.GetValue();
            if (!enableInitDb) return;
            "数据库正在初始化……".WriteLineInfo();

            "创建数据库……".WriteLineInfo();
            dbProvider.DbMaintenance.CreateDatabase();
            "数据库创建成功！".WriteLineSuccess();

            "创建数据表……".WriteLineInfo();
            // 获取继承自 BaseIdEntity 含有 SugarTable 的所有实体
            var dbEntities = ReflectionHelper.GetContainsAttributeSubClasses<BaseIdEntity, SugarTable>().ToArray();
            if (!dbEntities.Any()) return;
            // 支持分表，官方文档 https://www.donet5.com/Home/Doc?typeId=1201
            dbProvider.CodeFirst.SetStringDefaultLength(256).SplitTables().InitTables(dbEntities);
            "数据表创建成功！".WriteLineSuccess();

            "数据库初始化已完成！".WriteLineSuccess();
        }
        catch (Exception ex)
        {
            ex.ThrowAndConsoleError("数据库初始化或数据表初始化失败，请检查数据库连接字符是否正确！");
        }
    }

    /// <summary>
    /// 初始化种子数据
    /// </summary>
    /// <param name="dbProvider"></param>
    private static void InitSeedData(SqlSugarScopeProvider dbProvider)
    {
        try
        {
            "正在从配置中检测是否需要初始化种子数据……".WriteLineInfo();
            var enableInitSeed = AppSettings.Database.EnableInitSeed.GetValue();
            if (!enableInitSeed) return;
            "种子数据正在初始化……".WriteLineInfo();

            // 获取继承自泛型接口 ISeedData<> 的所有类
            var seedTypes = ReflectionHelper.GetSubClassesByGenericInterface(typeof(ISeedDataFilter<>)).ToList();
            if (!seedTypes.Any()) return;

            seedTypes.ForEach(seedType =>
            {
                var instance = Activator.CreateInstance(seedType);

                var hasDataMethod = seedType.GetMethods().First();
                var seedData = (hasDataMethod?.Invoke(instance, null) as IEnumerable)?.Cast<object>();
                if (seedData == null) return;

                var entityType = seedType.GetInterfaces().First().GetGenericArguments().First();
                var entityInfo = dbProvider.EntityMaintenance.GetEntityInfo(entityType);

                $"种子数据【{entityInfo.DbTableName}】初始化，共【{seedData.Count()}】条数据。".WriteLineInfo();

                var ignoreUpdate = hasDataMethod?.GetCustomAttribute<IgnoreUpdateAttribute>();
                if (dbProvider.Queryable(entityInfo.DbTableName, entityInfo.DbTableName).Any())
                {
                    $"种子数据【{entityInfo.DbTableName}】已初始化，本次跳过。".WriteLineSuccess();
                }
                else
                {
                    if (entityInfo.Columns.Any(u => u.IsPrimarykey))
                    {
                        // 按主键进行批量增加和更新
                        var storage = dbProvider.StorageableByObject(seedData.ToList()).ToStorage();
                        storage.AsInsertable.ExecuteCommand();
                        if (ignoreUpdate == null) storage.AsUpdateable.ExecuteCommand();
                    }
                    else
                    {
                        // 无主键则只进行插入
                        dbProvider.InsertableByObject(seedData.ToList()).ExecuteCommand();
                    }
                }
            });

            "种子数据初始化成功！".WriteLineSuccess();
        }
        catch (Exception ex)
        {
            ex.ThrowAndConsoleError("种子数据初始化失败，请检查数据库连接或种子数据是否符合规范！");
        }
    }

    /// <summary>
    /// 转换为 ConnectionConfig 数据库类型
    /// </summary>
    /// <param name="dbTypeValue"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static DbType ConvertDbType(this DataBaseTypeEnum dbTypeValue)
    {
        return dbTypeValue switch
        {
            DataBaseTypeEnum.MySql => DbType.MySql,
            DataBaseTypeEnum.SqlServer => DbType.SqlServer,
            DataBaseTypeEnum.Sqlite => DbType.Sqlite,
            DataBaseTypeEnum.Oracle => DbType.Oracle,
            DataBaseTypeEnum.PostgreSql => DbType.PostgreSQL,
            _ => throw new ArgumentException("Invalid value.", nameof(dbTypeValue)),
        };
    }
}