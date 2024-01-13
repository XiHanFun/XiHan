#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SqlSugarExtensions
// Guid:60c43d26-cad6-4c69-a5f9-96f15a108bc2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/16 3:56:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHanCore.Infrastructures.Apps.Configs;

namespace XiHanCore.WebCore.Extensions;

/// <summary>
/// SqlSugarExtensions
/// </summary>
public static class SqlSugarExtensions
{
    /// <summary>
    /// 转换为 ConnectionConfig 数据库类型
    /// </summary>
    /// <param name="dbTypeValue"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static DbType ConvertDbType(this DataBaseTypeEnum dbTypeValue)
    {
        return dbTypeValue switch
        {
            DataBaseTypeEnum.MySql => DbType.MySql,
            DataBaseTypeEnum.SqlServer => DbType.SqlServer,
            DataBaseTypeEnum.Sqlite => DbType.Sqlite,
            DataBaseTypeEnum.Oracle => DbType.Oracle,
            DataBaseTypeEnum.PostgreSql => DbType.PostgreSQL,
            _ => throw new ArgumentException("Invalid value.", nameof(dbTypeValue))
        };
    }
}