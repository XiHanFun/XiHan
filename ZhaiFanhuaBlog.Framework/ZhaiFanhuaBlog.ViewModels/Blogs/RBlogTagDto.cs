#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RBlogTagDto
// Guid:4eb11673-c96e-4bc2-a8f4-e5154e6ad16f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-28 下午 12:17:57
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.ViewModels.Bases.Results;

namespace ZhaiFanhuaBlog.ViewModels.Blogs;

/// <summary>
/// RBlogTagDto
/// </summary>
public class RBlogTagDto : BaseResultFieldDto
{
    /// <summary>
    /// 创建用户
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 标签名称
    /// </summary>
    public string TagName { get; set; } = string.Empty;

    /// <summary>
    /// 文章总数
    /// </summary>
    public int BlogCount { get; set; } = 0;
}