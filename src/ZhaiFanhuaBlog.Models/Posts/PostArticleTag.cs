#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:PostArticleTag
// Guid:7f74e0d5-2c12-492c-b849-9651624ef6ae
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:33:08
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Posts;

/// <summary>
/// 文章标签关联表
/// </summary>
public class PostArticleTag : BaseEntity
{
    /// <summary>
    /// 关联文章
    /// </summary>
    public Guid ArticleId { get; set; }

    /// <summary>
    /// 关联标签
    /// </summary>
    public Guid TagId { get; set; }
}