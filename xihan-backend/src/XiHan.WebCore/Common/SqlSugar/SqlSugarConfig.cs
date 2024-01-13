#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SqlSugarConfig
// Guid:a7e0f740-69cf-452d-b858-de7139ce15e2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/31 4:02:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using System.Reflection;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Utils.Extensions;
using XiHan.WebCore.Extensions;

namespace XiHan.WebCore.Common.SqlSugar;

/// <summary>
/// SqlSugar 配置
/// </summary>
public static class SqlSugarConfig
{
    /// <summary>
    /// 获取数据库连接配置
    /// </summary>
    /// <returns></returns>
    public static List<ConnectionConfig> GetConnectionConfigs()
    {
        DatabaseConfig[] dbConfigs = AppSettings.Database.DatabaseConfigs.GetSection();

        // 一些扩展层务的集成参考，官方文档 https://www.donet5.com/home/doc?masterId=1&typeId=1206
        var configureExternalServices = new ConfigureExternalServices
        {
            // 处理表
            EntityNameService = (type, entity) =>
            {
                // 只处理贴了特性[SugarTable]表
                if (!type.GetCustomAttributes<SugarTable>().Any())
                    return;
                // 驼峰转下划线
                if (!entity.DbTableName.Contains('_'))
                    entity.DbTableName = UtilMethods.ToUnderLine(entity.DbTableName);
                // 禁止删除非 sqlsugar 创建的列
                entity.IsDisabledDelete = true;
            },
            // 处理列
            EntityService = (type, column) =>
            {
                // 只处理贴了特性[SugarColumn]列
                if (!type.GetCustomAttributes<SugarColumn>().Any())
                    return;
                // 驼峰转下划线
                if (!column.IsIgnore && !column.DbColumnName.Contains('_'))
                    column.DbColumnName = UtilMethods.ToUnderLine(column.DbColumnName);
                // 为空类型
                if (column.IsPrimarykey == false && new NullabilityInfoContext().Create(type).WriteState is NullabilityState.Nullable)
                    column.IsNullable = true;
            }
        };

        List<ConnectionConfig> connectionConfigs = dbConfigs.Select(config => new ConnectionConfig()
        {
            ConfigId = config.ConfigId,
            DbType = config.DataBaseType.GetEnumByName<DataBaseTypeEnum>().ConvertDbType(),
            ConnectionString = config.ConnectionString,
            // 自动释放和关闭数据库连接，如果有事务事务结束时关闭，否则每次操作后关闭
            IsAutoCloseConnection = config.IsAutoCloseConnection,
            // 设置提示错误的语言
            LanguageType = LanguageType.Chinese,
            //
            ConfigureExternalServices = configureExternalServices
        }).ToList();

        return connectionConfigs;
    }
}