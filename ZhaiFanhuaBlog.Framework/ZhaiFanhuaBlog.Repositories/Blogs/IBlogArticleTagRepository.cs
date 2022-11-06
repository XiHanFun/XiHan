// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogArticleTagRepository
// Guid:66cfa7ed-8a06-4fad-8ce2-38639387a094
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:07:53
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Repositories.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Repositories.Blogs;

/// <summary>
/// IBlogArticleTagRepository
/// </summary>
public interface IBlogArticleTagRepository : IBaseRepository<BlogArticleTag>, IScopeDependency
{
}