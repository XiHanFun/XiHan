// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogCommentPollService
// Guid:33c38140-d18e-4566-9bc0-3778fc44d069
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:53
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Repositories.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogCommentPollService
/// </summary>
public class BlogCommentPollService : BaseService<BlogCommentPoll>, IBlogCommentPollService
{
    private readonly IBlogCommentPollRepository _IBlogCommentPollRepository;
    private readonly IBlogCommentService _IBlogCommentService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iBlogCommentPollRepository"></param>
    /// <param name="iBlogCommentService"></param>
    public BlogCommentPollService(IBlogCommentPollRepository iBlogCommentPollRepository,
        IBlogCommentService iBlogCommentService)
    {
        base._IBaseRepository = iBlogCommentPollRepository;
        _IBlogCommentPollRepository = iBlogCommentPollRepository;
        _IBlogCommentService = iBlogCommentService;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<BlogCommentPoll> IsExistenceAsync(Guid guid)
    {
        var blogCommentPoll = await _IBlogCommentPollRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (blogCommentPoll == null)
            throw new ApplicationException("博客文章评论点赞不存在");
        return blogCommentPoll;
    }

    /// <summary>
    /// 初始化博客评论点赞
    /// </summary>
    /// <param name="blogCommentPolls"></param>
    /// <returns></returns>
    public async Task<bool> InitBlogCommentPollAsync(List<BlogCommentPoll> blogCommentPolls)
    {
        blogCommentPolls.ForEach(blogCommentPoll =>
        {
            blogCommentPoll.SoftDeleteLock = false;
        });
        var result = await _IBlogCommentPollRepository.CreateBatchAsync(blogCommentPolls);
        return result;
    }

    /// <summary>
    /// 新增博客评论点赞
    /// </summary>
    /// <param name="blogCommentPoll"></param>
    /// <returns></returns>
    public async Task<bool> CreateBlogCommentPollAsync(BlogCommentPoll blogCommentPoll)
    {
        await _IBlogCommentService.IsExistenceAsync(blogCommentPoll.CommentId);
        blogCommentPoll.SoftDeleteLock = false;
        var result = await _IBlogCommentPollRepository.CreateAsync(blogCommentPoll);
        return result;
    }

    /// <summary>
    /// 删除博客评论点赞
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteBlogCommentPollAsync(Guid guid, Guid deleteId)
    {
        var blogCommentPoll = await IsExistenceAsync(guid);
        blogCommentPoll.SoftDeleteLock = true;
        blogCommentPoll.DeleteId = deleteId;
        blogCommentPoll.DeleteTime = DateTime.Now;
        return await _IBlogCommentPollRepository.UpdateAsync(blogCommentPoll);
    }

    /// <summary>
    /// 修改博客评论点赞
    /// </summary>
    /// <param name="blogCommentPoll"></param>
    /// <returns></returns>
    public async Task<BlogCommentPoll> ModifyBlogCommentPollAsync(BlogCommentPoll blogCommentPoll)
    {
        await IsExistenceAsync(blogCommentPoll.BaseId);
        await _IBlogCommentService.IsExistenceAsync(blogCommentPoll.CommentId);
        var result = await _IBlogCommentPollRepository.UpdateAsync(blogCommentPoll);
        if (result) blogCommentPoll = await _IBlogCommentPollRepository.FindAsync(blogCommentPoll.BaseId);
        return blogCommentPoll;
    }

    /// <summary>
    /// 查找博客评论点赞
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<BlogCommentPoll> FindBlogCommentPollAsync(Guid guid)
    {
        var blogCommentPoll = await IsExistenceAsync(guid);
        return blogCommentPoll;
    }

    /// <summary>
    /// 查询博客评论点赞
    /// </summary>
    /// <returns></returns>
    public async Task<List<BlogCommentPoll>> QueryBlogCommentPollAsync()
    {
        var blogCommentPoll = from blogtag in await _IBlogCommentPollRepository.QueryListAsync(e => !e.SoftDeleteLock)
                              orderby blogtag.CreateTime descending
                              select blogtag;
        return blogCommentPoll.ToList();
    }
}