#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:OrderConditionEnum
// Guid:1a69d03b-67d7-45a6-bd6e-860348df6bdd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 7:00:56
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Infrastructure.Core.Apps.RequestOrResponse.Enums;

/// <summary>
/// 排序条件枚举
/// </summary>
public enum OrderConditionEnum
{
    /// <summary>
    /// 不排序
    /// </summary>
    [Description("不排序")]
    None,

    /// <summary>
    /// 升序
    /// </summary>
    [Description("升序")]
    Asc,

    /// <summary>
    /// 降序
    /// </summary>
    [Description("降序")]
    Desc
}