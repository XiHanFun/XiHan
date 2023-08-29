#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SourceTypeEnum
// Guid:3dfe71d2-7ab3-4788-bd29-0c8134461141
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-20 下午 03:24:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Models.Blogs.Enums;

/// <summary>
/// 文章来源类型
/// </summary>
public enum SourceTypeEnum
{
    /// <summary>
    /// 转载
    /// </summary>
    [Description("转载")]
    Reprint = 0,

    /// <summary>
    /// 原创
    /// </summary>
    [Description("原创")]
    Original = 1,

    /// <summary>
    /// 衍生
    /// </summary>
    [Description("衍生")]
    Hybrid = 2
}