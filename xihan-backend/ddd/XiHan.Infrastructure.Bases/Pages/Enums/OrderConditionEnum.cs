#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:OrderConditionEnum
// Guid:53bfac82-7773-43fe-a3f3-d76baade29fe
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/1/29 21:01:43
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Infrastructure.Bases.Pages.Enums;

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