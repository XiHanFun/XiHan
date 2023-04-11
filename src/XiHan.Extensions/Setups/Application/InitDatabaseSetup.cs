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
using SqlSugar.IOC;
using XiHan.Infrastructure.Apps.Setting;
using XiHan.Models.Posts;
using XiHan.Models.Roots;
using XiHan.Models.Syses;
using XiHan.Models.Users;
using XiHan.Utils.Console;

namespace XiHan.Extensions.Setups.Application;

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
            return;
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
            db.CodeFirst.SetStringDefaultLength(512).InitTables(
                // Syses
                typeof(SysConfig),
                typeof(SysSkin),
                typeof(SysLog),
                typeof(SysLoginLog),
                typeof(SysOperationLog),
                typeof(SysDictType),
                typeof(SysDictData),
                typeof(SysFile),
                typeof(SysTasks),

                // Users
                typeof(UserAccount),
                typeof(UserAccountRole),
                typeof(UserOauth),
                typeof(UserStatistic),
                typeof(UserNotice),
                typeof(UserFollow),
                typeof(UserCollectCategory),
                typeof(UserCollect),

                // Roots
                typeof(RootAuthority),
                typeof(RootRole),
                typeof(RootRoleAuthority),
                typeof(RootMenu),
                typeof(RootRoleMenu),
                typeof(RootAnnouncement),
                typeof(RootAuditCategory),
                typeof(RootAudit),
                typeof(RootFriendlyLink),

                // Blogs
                typeof(PostCategory),
                typeof(PostTag),
                typeof(PostArticle),
                typeof(PostArticleTag),
                typeof(PostComment),
                typeof(PostPoll)
            );
            "数据表创建成功！".WriteLineSuccess();
            "数据库初始化已完成！".WriteLineSuccess();
        }
    }
}