#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:EditorTypeEnum
// Guid:2c9c9c7f-94b0-4813-b5c2-58e838d22a81
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-06-12 上午 11:23:49
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Models.Posts.Enums;

/// <summary>
/// 编辑器类型
/// </summary>
public enum EditorTypeEnum
{
    /// <summary>
    /// Markdown 类型
    /// </summary>
    [Description("Markdown 类型")]
    Markdown = 0,

    /// <summary>
    /// Html 类型
    /// </summary>
    [Description("Html 类型")]
    Html = 1
}