// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogPollService
// Guid:182aa8bc-458b-4c25-8501-67417fc31ec3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:36
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Repositories.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogPollService
/// </summary>
public class BlogPollService : BaseService<BlogPoll>, IBlogPollService
{
    private readonly IBlogPollRepository _IBlogPollRepository;
    private readonly IBlogArticleService _IBlogArticleService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iBlogPollRepository"></param>
    /// <param name="iBlogArticleService"></param>
    public BlogPollService(IBlogPollRepository iBlogPollRepository,
        IBlogArticleService iBlogArticleService)
    {
        base._IBaseRepository = iBlogPollRepository;
        _IBlogPollRepository = iBlogPollRepository;
        _IBlogArticleService = iBlogArticleService;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<BlogPoll> IsExistenceAsync(Guid guid)
    {
        var blogCommentPoll = await _IBlogPollRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (blogCommentPoll == null)
            throw new ApplicationException("博客文章点赞不存在");
        return blogCommentPoll;
    }

    /// <summary>
    /// 初始化博客点赞
    /// </summary>
    /// <param name="blogCommentPolls"></param>
    /// <returns></returns>
    public async Task<bool> InitBlogPollAsync(List<BlogPoll> blogCommentPolls)
    {
        blogCommentPolls.ForEach(blogCommentPoll =>
        {
            blogCommentPoll.SoftDeleteLock = false;
        });
        var result = await _IBlogPollRepository.CreateBatchAsync(blogCommentPolls);
        return result;
    }

    /// <summary>
    /// 新增博客点赞
    /// </summary>
    /// <param name="blogCommentPoll"></param>
    /// <returns></returns>
    public async Task<bool> CreateBlogPollAsync(BlogPoll blogCommentPoll)
    {
        await _IBlogArticleService.IsExistenceAsync(blogCommentPoll.ArticleId);
        blogCommentPoll.SoftDeleteLock = false;
        var result = await _IBlogPollRepository.CreateAsync(blogCommentPoll);
        return result;
    }

    /// <summary>
    /// 删除博客点赞
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteBlogPollAsync(Guid guid, Guid deleteId)
    {
        var blogCommentPoll = await IsExistenceAsync(guid);
        blogCommentPoll.SoftDeleteLock = true;
        blogCommentPoll.DeleteId = deleteId;
        blogCommentPoll.DeleteTime = DateTime.Now;
        return await _IBlogPollRepository.UpdateAsync(blogCommentPoll);
    }

    /// <summary>
    /// 修改博客点赞
    /// </summary>
    /// <param name="blogCommentPoll"></param>
    /// <returns></returns>
    public async Task<BlogPoll> ModifyBlogPollAsync(BlogPoll blogCommentPoll)
    {
        await IsExistenceAsync(blogCommentPoll.BaseId);
        await _IBlogArticleService.IsExistenceAsync(blogCommentPoll.ArticleId);
        var result = await _IBlogPollRepository.UpdateAsync(blogCommentPoll);
        if (result) blogCommentPoll = await _IBlogPollRepository.FindAsync(blogCommentPoll.BaseId);
        return blogCommentPoll;
    }

    /// <summary>
    /// 查找博客点赞
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<BlogPoll> FindBlogPollAsync(Guid guid)
    {
        var blogCommentPoll = await IsExistenceAsync(guid);
        return blogCommentPoll;
    }

    /// <summary>
    /// 查询博客点赞
    /// </summary>
    /// <returns></returns>
    public async Task<List<BlogPoll>> QueryBlogPollAsync()
    {
        var blogCommentPoll = from blogtag in await _IBlogPollRepository.QueryListAsync(e => !e.SoftDeleteLock)
                              orderby blogtag.CreateTime descending
                              select blogtag;
        return blogCommentPoll.ToList();
    }
}