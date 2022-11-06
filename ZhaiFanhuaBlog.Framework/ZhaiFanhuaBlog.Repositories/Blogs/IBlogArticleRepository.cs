// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogArticleRepository
// Guid:43a7cca5-f66e-42e4-bd32-2d0b4810b125
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:03:08
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Repositories.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Repositories.Blogs;

/// <summary>
/// IBlogArticleRepository
/// </summary>
public interface IBlogArticleRepository : IBaseRepository<BlogArticle>, IScopeDependency
{
}