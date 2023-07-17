#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:InitDatabaseStep
// Guid:65152a82-82db-4a62-88ff-15ace89b57c4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-12-13 上午 02:10:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using SqlSugar;
using SqlSugar.IOC;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Models.Bases.Interface;
using XiHan.Utils.Extensions;
using XiHan.Utils.Reflections;

namespace XiHan.Application.Setups.Apps;

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
        var databaseInitialized = AppSettings.Database.Initialized.GetValue();
        // 若数据库已经初始化，则跳过，否则初始化数据库
        "正在从配置中检测是否需要数据库初始化……".WriteLineInfo();
        if (databaseInitialized)
            "数据库已初始化。".WriteLineSuccess();
        else
            try
            {
                "数据库正在初始化……".WriteLineInfo();

                // 创建数据库
                "创建数据库……".WriteLineInfo();
                db.DbMaintenance.CreateDatabase();
                "数据库创建成功！".WriteLineSuccess();

                // 创建数据表
                "创建数据表……".WriteLineInfo();

                // 获取含有 SugarTable 的所有 Models 的实体
                var entities = ReflectionHelper.GetContainsAttributeSubClass<SugarTable>(typeof(IBaseIdEntity<>)).ToArray();

                db.CodeFirst.SetStringDefaultLength(512).InitTables(entities);

                "数据表创建成功！".WriteLineSuccess();
                "数据库初始化已完成！".WriteLineSuccess();
            }
            catch (Exception ex)
            {
                ex.ThrowAndConsoleError("数据库初始化或数据表初始化失败，请检查数据库连接字符是否正确！");
            }
    }
}