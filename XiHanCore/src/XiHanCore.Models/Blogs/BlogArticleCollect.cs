#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BlogArticleCollect
// Guid:9a936a6b-f0bf-4fe9-9c43-868982a69e01
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:02:22
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHanCore.Models.Bases.Entities;

namespace XiHanCore.Models.Blogs;

/// <summary>
/// 博客文章收藏表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SugarTable]
public class BlogArticleCollect : BaseCreateEntity
{
    /// <summary>
    /// 所属分类
    /// </summary>
    [SugarColumn]
    public long CategoryId { get; set; }

    /// <summary>
    /// 收藏文章
    /// </summary>
    [SugarColumn]
    public long ArticleId { get; set; }
}