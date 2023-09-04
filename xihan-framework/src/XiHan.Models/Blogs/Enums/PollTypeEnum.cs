#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PollTypeEnum
// Guid:34e0e263-c7d1-41bd-968a-4c28b0e21422
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/17 19:22:42
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Models.Blogs.Enums;

/// <summary>
/// PollTypeEnum
/// </summary>
public enum PollTypeEnum
{
    /// <summary>
    /// 文章
    /// </summary>
    [Description("文章")] Article = 1,

    /// <summary>
    /// 评论
    /// </summary>
    [Description("评论")] Comment = 2
}