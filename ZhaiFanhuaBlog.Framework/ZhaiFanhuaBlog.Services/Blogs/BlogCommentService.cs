// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogCommentService
// Guid:7c118964-e09b-4bf2-87aa-d2a9765b1522
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:45
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.IServices.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogCommentService
/// </summary>
public class BlogCommentService : BaseService<BlogComment>, IBlogCommentService
{
    private readonly IBlogCommentRepository _IBlogCommentRepository;

    public BlogCommentService(IBlogCommentRepository iBlogCommentRepository)
    {
        _IBlogCommentRepository = iBlogCommentRepository;
        base._IBaseRepository = iBlogCommentRepository;
    }
}