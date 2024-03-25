#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DataBaseTypeEnum
// Guid:ee059ab9-6a75-42da-bf39-c77c77f97d89
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/1/30 23:17:17
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace XiHan.Common.Shared.Enums;

/// <summary>
/// 数据库类型枚举
/// </summary>
public enum DataBaseTypeEnum
{
    /// <summary>
    /// MySql
    /// </summary>
    [Display(Name = "MySql")]
    MySql = 0,

    /// <summary>
    /// SqlServer
    /// </summary>
    [Display(Name = "SqlServer")]
    SqlServer = 1,

    /// <summary>
    /// Sqlite
    /// </summary>
    [Display(Name = "Sqlite")]
    Sqlite = 2,

    /// <summary>
    /// Oracle
    /// </summary>
    [Display(Name = "Oracle")]
    Oracle = 3,

    /// <summary>
    /// PostgreSql
    /// </summary>
    [Display(Name = "PostgreSql")]
    PostgreSql = 4
}