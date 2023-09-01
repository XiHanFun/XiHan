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
        List<ConnectionConfig> connectionConfigs = dbConfigs.Select(config => new ConnectionConfig()
        {
            ConfigId = config.ConfigId,
            DbType = config.DataBaseType.GetEnumByName<DataBaseTypeEnum>().ConvertDbType(),
            ConnectionString = config.ConnectionString,
            IsAutoCloseConnection = config.IsAutoCloseConnection
        }).ToList();

        return connectionConfigs;
    }
}