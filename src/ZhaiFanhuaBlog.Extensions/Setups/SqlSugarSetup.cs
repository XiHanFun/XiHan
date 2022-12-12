#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SqlSugarSetup
// Guid:49736281-4a15-48db-ba3e-b66124a931d4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-26 下午 06:24:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using SqlSugar.IOC;
using ZhaiFanhuaBlog.Infrastructure.AppSetting;
using ZhaiFanhuaBlog.Models.Posts;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Models.Syses;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Utils.Console;

namespace ZhaiFanhuaBlog.Extensions.Setups;

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
    public static IServiceCollection AddSqlSugarSetup(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        string databaseType = AppSettings.Database.Type.Get();
        services.AddSqlSugar(databaseType switch
        {
            "MySql" => new IocConfig
            {
                DbType = IocDbType.MySql,
                ConnectionString = AppSettings.Database.Connectionstring.MySql.Get(),
                IsAutoCloseConnection = true
            },
            "SqlServer" => new IocConfig
            {
                DbType = IocDbType.SqlServer,
                ConnectionString = AppSettings.Database.Connectionstring.SqlServer.Get(),
                IsAutoCloseConnection = true
            },
            "Sqlite" => new IocConfig
            {
                DbType = IocDbType.Sqlite,
                ConnectionString = AppSettings.Database.Connectionstring.Sqlite.Get(),
                IsAutoCloseConnection = true
            },
            "Oracle" => new IocConfig
            {
                DbType = IocDbType.Oracle,
                ConnectionString = AppSettings.Database.Connectionstring.Oracle.Get(),
                IsAutoCloseConnection = true
            },
            "PostgreSQL" => new IocConfig
            {
                DbType = IocDbType.PostgreSQL,
                ConnectionString = AppSettings.Database.Connectionstring.PostgreSQL.Get(),
                IsAutoCloseConnection = true
            },
            _ => new IocConfig
            {
                DbType = IocDbType.SqlServer,
                ConnectionString = AppSettings.Database.Connectionstring.SqlServer.Get(),
                IsAutoCloseConnection = true
            }
        });

        services.ConfigurationSugar(client =>
        {
            // SQL语句输出方便排查问题
            bool databaseConsole = AppSettings.Database.Console.Get();
            if (databaseConsole)
            {
                client.Aop.OnLogExecuting = (sql, pars) =>
                {
                    ConsoleHelper.WriteLineHandle("===========================================================================");
                    ConsoleHelper.WriteLineHandle($"{DateTime.Now}，SQL语句：");
                    ConsoleHelper.WriteLineHandle(sql + Environment.NewLine + client.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                    ConsoleHelper.WriteLineHandle("===========================================================================");
                };
            }
        });
        services.ConfigurationSugar(client =>
        {
            // 初始化数据库
            InitDatabase(client);
        });
        return services;
    }

    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <param name="client"></param>
    public static void InitDatabase(SqlSugarClient client)
    {
        ConsoleHelper.WriteLineWarning("数据库正在初始化……");
        ConsoleHelper.WriteLineWarning("创建数据库……");
        client.Context.DbMaintenance.CreateDatabase();
        ConsoleHelper.WriteLineSuccess("数据库创建成功！");
        ConsoleHelper.WriteLineWarning("创建数据表……");
        client.Context.CodeFirst.SetStringDefaultLength(200).InitTables(
            // Syses
            typeof(SysConfig),
            typeof(SysSkin),
            typeof(SysLog),
            typeof(SysLoginLog),
            typeof(SysOperationLog),
            typeof(SysDictType),
            typeof(SysDictData),
            typeof(SysFile),

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
        ConsoleHelper.WriteLineSuccess("数据表创建成功！");
        ConsoleHelper.WriteLineSuccess("数据库初始化已完成！");
    }
}