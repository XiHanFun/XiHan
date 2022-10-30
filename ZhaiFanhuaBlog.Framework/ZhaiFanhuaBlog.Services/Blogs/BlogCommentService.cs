// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogCommentService
// Guid:7c118964-e09b-4bf2-87aa-d2a9765b1522
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:45
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Repositories.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// 博客评论
/// </summary>
public class BlogCommentService : BaseService<BlogComment>, IBlogCommentService
{
    private readonly IBlogArticleService _IBlogArticleService;
    private readonly IBlogCommentRepository _IBlogCommentRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iBlogArticleService"></param>
    /// <param name="iBlogCommentRepository"></param>
    public BlogCommentService(IBlogArticleService iBlogArticleService,
        IBlogCommentRepository iBlogCommentRepository)
    {
        base._IBaseRepository = iBlogCommentRepository;
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

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="blogComments"></param>
    /// <returns></returns>
    public async Task<bool> InitBlogCommentAsync(List<BlogComment> blogComments)
    {
        blogComments.ForEach(blogComment =>
        {
            blogComment.SoftDeleteLock = false;
        });
        var result = await _IBlogCommentRepository.CreateBatchAsync(blogComments);
        return result;
    }

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="blogComment"></param>
    /// <returns></returns>
    public async Task<bool> CreateBlogCommentAsync(BlogComment blogComment)
    {
        await _IBlogArticleService.IsExistenceAsync(blogComment.AccountId);
        blogComment.SoftDeleteLock = false;
        var result = await _IBlogCommentRepository.CreateAsync(blogComment);
        return result;
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteBlogCommentAsync(Guid guid, Guid deleteId)
    {
        var blogComment = await IsExistenceAsync(guid);
        blogComment.SoftDeleteLock = true;
        blogComment.DeleteId = deleteId;
        blogComment.DeleteTime = DateTime.Now;
        return await _IBlogCommentRepository.UpdateAsync(blogComment);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="blogComment"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<BlogComment> ModifyBlogCommentAsync(BlogComment blogComment)
    {
        await IsExistenceAsync(blogComment.BaseId);
        if (blogComment.ParentId != null && await _IBlogCommentRepository.FindAsync(e => e.ParentId == blogComment.ParentId && !e.SoftDeleteLock) == null)
            throw new ApplicationException("父级博客文章评论不存在");
        var result = await _IBlogCommentRepository.UpdateAsync(blogComment);
        if (result) blogComment = await _IBlogCommentRepository.FindAsync(blogComment.BaseId);
        return blogComment;
    }

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<BlogComment> FindBlogCommentAsync(Guid guid)
    {
        var blogComment = await IsExistenceAsync(guid);
        return blogComment;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    public async Task<List<BlogComment>> QueryBlogCommentAsync()
    {
        var blogComment = from blogcategory in await _IBlogCommentRepository.QueryListAsync(e => !e.SoftDeleteLock)
                          orderby blogcategory.CreateTime descending
                          select blogcategory;
        return blogComment.ToList();
    }
}