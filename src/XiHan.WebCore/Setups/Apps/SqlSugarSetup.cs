#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SqlSugarSetup
// Guid:60e7dba5-c4d5-4e7f-819b-ceb28a096fa0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/16 3:32:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using SqlSugar;
using System.Collections;
using System.Reflection;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Models.Bases.Attributes;
using XiHan.Models.Bases.Entities;
using XiHan.Models.Bases.Filters;
using XiHan.Utils.Extensions;
using XiHan.Utils.Reflections;
using XiHan.WebCore.Extensions;

namespace XiHan.WebCore.Setups.Apps;

/// <summary>
/// SqlSugarSetup
/// </summary>
public static class SqlSugarSetup
{
    /// <summary>
    /// SqlSugar 应用扩展
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IApplicationBuilder UseSqlSugarSetup(this IApplicationBuilder app)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        var dbConfigs = AppSettings.Database.DatabaseConfigs.GetSection();
        var connectionConfigs = dbConfigs.Select(config => new ConnectionConfig()
        {
            ConfigId = config.ConfigId,
            DbType = config.DataBaseType.GetEnumByName<DataBaseTypeEnum>().ConvertDbType(),
            ConnectionString = config.ConnectionString,
            IsAutoCloseConnection = config.IsAutoCloseConnection
        }).ToList();

        // 初始化
        var client = new SqlSugarClient(connectionConfigs);
        InitDatabase(client);
        InitSeedData(client);

        return app;
    }

    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <param name="client"></param>
    private static void InitDatabase(SqlSugarClient client)
    {
        try
        {
            "正在从配置中检测是否需要初始化数据库……".WriteLineInfo();
            var enableInitDb = AppSettings.Database.EnableInitDb.GetValue();
            if (!enableInitDb) return;
            "数据库正在初始化……".WriteLineInfo();

            "创建数据库……".WriteLineInfo();
            client.DbMaintenance.CreateDatabase();
            "数据库创建成功！".WriteLineSuccess();

            "创建数据表……".WriteLineInfo();
            // 获取继承自 BaseIdEntity 含有 SugarTable 的所有实体
            var dbEntities = ReflectionHelper.GetContainsAttributeSubClasses<BaseIdEntity, SugarTable>().ToArray();
            if (!dbEntities.Any()) return;
            // 支持分表，官方文档 https://www.donet5.com/Home/Doc?typeId=1201
            client.CodeFirst.SetStringDefaultLength(256).SplitTables().InitTables(dbEntities);
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
    /// <param name="client"></param>
    private static void InitSeedData(SqlSugarClient client)
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
                var entityInfo = client.EntityMaintenance.GetEntityInfo(entityType);

                $"种子数据【{entityInfo.DbTableName}】初始化，共【{seedData.Count()}】条数据。".WriteLineInfo();

                var ignoreUpdate = hasDataMethod?.GetCustomAttribute<IgnoreUpdateAttribute>();
                if (client.Queryable(entityInfo.DbTableName, entityInfo.DbTableName).Any())
                {
                    $"种子数据【{entityInfo.DbTableName}】已初始化，本次跳过。".WriteLineSuccess();
                }
                else
                {
                    if (entityInfo.Columns.Any(u => u.IsPrimarykey))
                    {
                        // 按主键进行批量增加和更新
                        var storage = client.StorageableByObject(seedData.ToList()).ToStorage();
                        storage.AsInsertable.ExecuteCommand();
                        if (ignoreUpdate == null) storage.AsUpdateable.ExecuteCommand();
                    }
                    else
                    {
                        // 无主键则只进行插入
                        client.InsertableByObject(seedData.ToList()).ExecuteCommand();
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
}