#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:JobTypeEnum
// Guid:03114ef5-0691-4dca-9080-c68c88bca0a0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-20 下午 03:28:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Domain.Syses.Enums;

/// <summary>
/// 任务类型
/// </summary>
public enum JobTypeEnum
{
    /// <summary>
    /// 程序集
    /// </summary>
    [Description("程序集")]
    Assembly = 1,

    /// <summary>
    /// 网络请求
    /// </summary>
    [Description("网络请求")]
    NetworkRequest = 2,

    /// <summary>
    /// SQL语句
    /// </summary>
    [Description("SQL语句")]
    SqlStatement = 3
}