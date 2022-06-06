// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogArticleService
// Guid:0d15e2e2-c341-4cfc-bd58-1e0d0be8ce10
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:12
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.IServices.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogArticleService
/// </summary>
public class BlogArticleService : BaseService<BlogArticle>, IBlogArticleService
{
    private readonly IBlogArticleRepository _IBlogArticleRepository;

    public BlogArticleService(IBlogArticleRepository iBlogArticleRepository)
    {
        _IBlogArticleRepository = iBlogArticleRepository;
        _iBaseRepository = iBlogArticleRepository;
    }
}