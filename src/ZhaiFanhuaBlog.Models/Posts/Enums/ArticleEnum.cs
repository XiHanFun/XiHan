#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ArticleEnum
// Guid:c2955383-6546-43ac-ac2e-2dc3096e1bf6
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-01 上午 01:04:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace ZhaiFanhuaBlog.Models.Blogs.Enums;

/// <summary>
/// 发布状态
/// </summary>
public enum PubStatusEnum
{
    /// <summary>
    /// 回收站
    /// </summary>
    [Description("回收站")]
    Recycle = 0,

    /// <summary>
    /// 已发布
    /// </summary>
    [Description("已发布")]
    Published = 1,

    /// <summary>
    /// 草稿箱
    /// </summary>
    [Description("草稿箱")]
    Drafts = 2
}

/// <summary>
/// 文章来源
/// </summary>
public enum SourceEnum
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