// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogArticleRepository
// Guid:14bef2d5-99b2-4fe6-a2a6-549e40fd676a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:44:01
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Blogs;

/// <summary>
/// BlogArticleRepository
/// </summary>
public class BlogArticleRepository : BaseRepository<BlogArticle>, IBlogArticleRepository
{
}