// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogCategoryService
// Guid:bd858f38-c035-4a4b-935c-d0b077a68113
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:00
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.IServices.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogCategoryService
/// </summary>
public class BlogCategoryService : BaseService<BlogCategory>, IBlogCategoryService
{
    private readonly IBlogCategoryRepository _IBlogCategoryRepository;

    public BlogCategoryService(IBlogCategoryRepository iBlogCategoryRepository)
    {
        _IBlogCategoryRepository = iBlogCategoryRepository;
        _iBaseRepository = iBlogCategoryRepository;
    }
}