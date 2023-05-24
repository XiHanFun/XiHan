#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ExposedTypeEnum
// Guid:a9a04710-4679-4f64-8db8-157476cd53ed
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-04-20 下午 03:24:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Models.Posts.Enums;

/// <summary>
/// 公开类型
/// </summary>
public enum ExposedTypeEnum
{
    /// <summary>
    /// 保留
    /// </summary>
    [Description("保留")]
    Reserve = 0,

    /// <summary>
    /// 公开
    /// </summary>
    [Description("公开")]
    Public = 1,

    /// <summary>
    /// 私密
    /// </summary>
    [Description("私密")]
    Secret = 2,
}