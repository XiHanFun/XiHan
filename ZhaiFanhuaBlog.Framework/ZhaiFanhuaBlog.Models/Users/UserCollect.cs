// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserCollect
// Guid:9a936a6b-f0bf-4fe9-9c43-868982a69e01
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:02:22
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 用户收藏表
/// </summary>
public class UserCollect : BaseEntity
{
    /// <summary>
    /// 所属用户
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 收藏分类
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// 收藏文章
    /// </summary>
    public Guid ArticleId { get; set; }
}