// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogCommentService
// Guid:7c118964-e09b-4bf2-87aa-d2a9765b1522
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:45
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IServices.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogCommentService
/// </summary>
public class BlogCommentService : BaseService<BlogComment>, IBlogCommentService
{
    private readonly IRootStateRepository _IRootStateRepository;
    private readonly IBlogArticleService _IBlogArticleService;
    private readonly IBlogCommentRepository _IBlogCommentRepository;

    public BlogCommentService(IRootStateRepository iRootStateRepository,
        IBlogArticleService iBlogArticleService,
        IBlogCommentRepository iBlogCommentRepository)
    {
        base._IBaseRepository = iBlogCommentRepository;
        _IRootStateRepository = iRootStateRepository;
        _IBlogArticleService = iBlogArticleService;
        _IBlogCommentRepository = iBlogCommentRepository;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<BlogComment> IsExistenceAsync(Guid guid)
    {
        var blogComment = await _IBlogCommentRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (blogComment == null)
            throw new ApplicationException("博客文章评论不存在");
        return blogComment;
    }

    public async Task<bool> InitBlogCommentAsync(List<BlogComment> blogComments)
    {
        blogComments.ForEach(blogComment =>
        {
            blogComment.SoftDeleteLock = false;
        });
        var result = await _IBlogCommentRepository.CreateBatchAsync(blogComments);
        return result;
    }

    public async Task<bool> CreateBlogCommentAsync(BlogComment blogComment)
    {
        await _IBlogArticleService.IsExistenceAsync(blogComment.AccountId);
        blogComment.SoftDeleteLock = false;
        var result = await _IBlogCommentRepository.CreateAsync(blogComment);
        return result;
    }

    public async Task<bool> DeleteBlogCommentAsync(Guid guid, Guid deleteId)
    {
        var blogComment = await IsExistenceAsync(guid);
        var rootState = await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == -1);
        blogComment.SoftDeleteLock = true;
        blogComment.DeleteId = deleteId;
        blogComment.DeleteTime = DateTime.Now;
        blogComment.StateId = rootState.BaseId;
        return await _IBlogCommentRepository.UpdateAsync(blogComment);
    }

    public async Task<BlogComment> ModifyBlogCommentAsync(BlogComment blogComment)
    {
        await IsExistenceAsync(blogComment.BaseId);
        if (blogComment.ParentId != null && await _IBlogCommentRepository.FindAsync(e => e.ParentId == blogComment.ParentId && !e.SoftDeleteLock) == null)
            throw new ApplicationException("父级博客文章评论不存在");
        var result = await _IBlogCommentRepository.UpdateAsync(blogComment);
        if (result) blogComment = await _IBlogCommentRepository.FindAsync(blogComment.BaseId);
        return blogComment;
    }

    public async Task<BlogComment> FindBlogCommentAsync(Guid guid)
    {
        var blogComment = await IsExistenceAsync(guid);
        return blogComment;
    }

    public async Task<List<BlogComment>> QueryBlogCommentAsync()
    {
        var blogComment = from blogcategory in await _IBlogCommentRepository.QueryAsync(e => !e.SoftDeleteLock)
                          orderby blogcategory.CreateTime descending
                          select blogcategory;
        return blogComment.ToList();
    }
}