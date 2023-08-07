#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:InitDatabaseSetup
// Guid:296e3193-261b-4529-903b-f7d28f3aa76f
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-07 下午 04:33:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using SqlSugar;
using SqlSugar.IOC;
using System.Collections;
using System.Reflection;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Models.Bases.Attributes;
using XiHan.Models.Bases.Entity;
using XiHan.Models.Bases.Interface;
using XiHan.Utils.Extensions;
using XiHan.Utils.Reflections;

namespace XiHan.WebCore.Setups.Apps;

/// <summary>
/// InitDatabaseSetup
/// </summary>
public static class InitDatabaseSetup
{
    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <param name="app"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void InitDatabase(this IApplicationBuilder app)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        var dbProvider = DbScoped.SugarScope;

        try
        {
            "正在从配置中检测是否需要初始化数据库……".WriteLineInfo();
            var enableInitDb = AppSettings.Database.EnableInitDb.GetValue();
            if (!enableInitDb) return;
            "数据库正在初始化……".WriteLineInfo();
            InitDatabase(dbProvider);
            InitTables(dbProvider);
            "数据库初始化已完成！".WriteLineSuccess();
        }
        catch (Exception ex)
        {
            ex.ThrowAndConsoleError("数据库初始化或数据表初始化失败，请检查数据库连接字符是否正确！");
        }

        try
        {
            "正在从配置中检测是否需要初始化种子数据……".WriteLineInfo();
            var enableInitSeed = AppSettings.Database.EnableInitSeed.GetValue();
            if (!enableInitSeed) return;
            "种子数据正在初始化……".WriteLineInfo();
            InitSeedData(dbProvider);
            "种子数据初始化成功！".WriteLineSuccess();
        }
        catch (Exception ex)
        {
            ex.ThrowAndConsoleError("种子数据初始化失败，请检查数据库连接或种子数据是否符合规范！");
        }
    }

    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <param name="dbProvider"></param>
    private static void InitDatabase(SqlSugarScope dbProvider)
    {
        "创建数据库……".WriteLineInfo();
        dbProvider.DbMaintenance.CreateDatabase();
        "数据库创建成功！".WriteLineSuccess();
    }

    /// <summary>
    /// 初始化数据表
    /// </summary>
    /// <param name="dbProvider"></param>
    private static void InitTables(SqlSugarScope dbProvider)
    {
        "创建数据表……".WriteLineInfo();
        // 获取继承自 BaseIdEntity 含有 SugarTable 的所有实体
        var dbEntities = ReflectionHelper.GetContainsAttributeSubClasses<BaseIdEntity, SugarTable>().ToArray();
        if (!dbEntities.Any()) return;

        dbProvider.CodeFirst.SetStringDefaultLength(256).InitTables(dbEntities);
        "数据表创建成功！".WriteLineSuccess();
    }

    /// <summary>
    /// 初始化种子数据
    /// </summary>
    /// <param name="dbProvider"></param>
    private static void InitSeedData(SqlSugarScope dbProvider)
    {
        // 获取继承自泛型接口 ISeedData<> 的所有类
        var seedTypes = ReflectionHelper.GetSubClassesByGenericInterface(typeof(ISeedData<>)).ToList();
        if (!seedTypes.Any()) return;

        seedTypes.ForEach(seedType =>
        {
            var instance = Activator.CreateInstance(seedType);

            var hasDataMethod = seedType.GetMethods().First();
            var seedData = (hasDataMethod?.Invoke(instance, null) as IEnumerable)?.Cast<object>();
            if (seedData == null) return;

            var entityType = seedType.GetInterfaces().First().GetGenericArguments().First();
            var entityInfo = dbProvider.EntityMaintenance.GetEntityInfo(entityType);

            if (!dbProvider.Queryable(entityInfo.DbTableName, entityInfo.DbTableName).Any())
            {
                if (entityInfo.Columns.Any(u => u.IsPrimarykey))
                {
                    // 按主键进行批量增加和更新
                    var storage = dbProvider.StorageableByObject(seedData.ToList()).ToStorage();
                    storage.AsInsertable.ExecuteCommand();
                    var ignoreUpdate = hasDataMethod?.GetCustomAttribute<IgnoreUpdateAttribute>();
                    if (ignoreUpdate == null) storage.AsUpdateable.ExecuteCommand();
                }
                else
                {
                    // 无主键则只进行插入
                    dbProvider.InsertableByObject(seedData.ToList()).ExecuteCommand();
                }
            }
        });
    }
}