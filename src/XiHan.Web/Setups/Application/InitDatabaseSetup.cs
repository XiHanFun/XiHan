#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:InitDatabaseStep
// Guid:65152a82-82db-4a62-88ff-15ace89b57c4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-13 上午 02:10:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using SqlSugar;
using SqlSugar.IOC;
using System.Reflection;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Utils.Extensions;
using XiHan.Utils.Reflections;

namespace XiHan.Web.Setups.Application;

/// <summary>
/// InitDatabaseStep
/// </summary>
public static class InitDatabaseStep
{
    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <param name="app"></param>
    public static void InitDatabase(this IApplicationBuilder app)
    {
        var db = DbScoped.SugarScope;
        var databaseInited = AppSettings.Database.Inited.GetValue();
        // 若数据库已经初始化，则跳过，否则初始化数据库
        if (databaseInited)
        {
        }
        else
        {
            "数据库正在初始化……".WriteLineInfo();

            // 创建数据库
            "创建数据库……".WriteLineInfo();
            db.DbMaintenance.CreateDatabase();
            "数据库创建成功！".WriteLineSuccess();

            // 创建数据表
            "创建数据表……".WriteLineInfo();

            // 获取所有 XiHan.Models 的实体
            var entityes = ReflectionHelper.GetAllTypes()
                .Where(p => !p.IsAbstract && p.GetCustomAttribute<SugarTable>() != null)
                .ToArray();

            db.CodeFirst.SetStringDefaultLength(512).InitTables(entityes);

            "数据表创建成功！".WriteLineSuccess();
            "数据库初始化已完成！".WriteLineSuccess();
        }
    }
}