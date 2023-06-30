#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:PubStatusEnum
// Guid:40e93ef8-583e-43a1-b0cb-9b143c29ae09
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-20 下午 03:23:57
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Models.Posts.Enums;

/// <summary>
/// 发布状态
/// </summary>
public enum PubStatusEnum
{
    /// <summary>
    /// 回收站
    /// </summary>
    [Description("回收站")] Recycle = 0,

    /// <summary>
    /// 已发布
    /// </summary>
    [Description("已发布")] Published = 1,

    /// <summary>
    /// 草稿箱
    /// </summary>
    [Description("草稿箱")] Drafts = 2
}