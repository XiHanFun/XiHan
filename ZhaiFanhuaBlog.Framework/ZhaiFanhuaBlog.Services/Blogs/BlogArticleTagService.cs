// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogArticleTagService
// Guid:f9b3a059-9beb-4d04-8329-48b390fb1007
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:29
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.IServices.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogArticleTagService
/// </summary>
public class BlogArticleTagService : BaseService<BlogArticleTag>, IBlogArticleTagService
{
    private readonly IBlogArticleTagRepository _IBlogArticleTagRepository;

    public BlogArticleTagService(IBlogArticleTagRepository iBlogArticleTagRepository)
    {
        _IBlogArticleTagRepository = iBlogArticleTagRepository;
        base._IBaseRepository = iBlogArticleTagRepository;
    }
}