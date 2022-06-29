// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogTagService
// Guid:79d590ff-e314-4508-b1b3-8434eed357c6
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:20
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.IServices.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogTagService
/// </summary>
public class BlogTagService : BaseService<BlogTag>, IBlogTagService
{
    private readonly IBlogTagRepository _IBlogTagRepository;

    public BlogTagService(IBlogTagRepository iBlogTagRepository)
    {
        _IBlogTagRepository = iBlogTagRepository;
        base._IBaseRepository = iBlogTagRepository;
    }
}