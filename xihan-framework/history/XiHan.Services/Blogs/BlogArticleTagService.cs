#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogArticleTagService
// Guid:f9b3a059-9beb-4d04-8329-48b390fb1007
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Posts;
using ZhaiFanhuaBlog.Repositories.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogArticleTagService
/// </summary>
public class BlogArticleTagService : BaseService<PostTag>, IBlogArticleTagService
{
    private readonly IBlogArticleService _IBlogArticleService;
    private readonly IBlogTagService _IBlogTagService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iBlogArticleService"></param>
    /// <param name="iIBlogTagService"></param>
    public BlogArticleTagService(IBlogArticleService iBlogArticleService, IBlogTagService iIBlogTagService)
    {
        _IBlogArticleService = iBlogArticleService;
        _IBlogTagService = iIBlogTagService;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<PostTag> IsExistenceAsync(Guid guid)
    {
        var blogArticleTag = await FindAsync(e => e.BaseId == guid && !e.SoftDelete);
        if (blogArticleTag == null)
            throw new ApplicationException("博客文章标签不存在");
        return blogArticleTag;
    }

    /// <summary>
    /// 初始化博客文章标签
    /// </summary>
    /// <param name="blogArticleTags"></param>
    /// <returns></returns>
    public async Task<bool> InitBlogArticleTagAsync(List<PostTag> blogArticleTags)
    {
        blogArticleTags.ForEach(blogArticleTag =>
        {
            blogArticleTag.SoftDelete = false;
        });
        var result = await CreateBatchAsync(blogArticleTags);
        return result;
    }

    /// <summary>
    /// 新增博客文章标签
    /// </summary>
    /// <param name="blogArticleTag"></param>
    /// <returns></returns>
    public async Task<bool> CreateBlogArticleTagAsync(PostTag blogArticleTag)
    {
        await _IBlogArticleService.IsExistenceAsync(blogArticleTag.ArticleId);
        await _IBlogTagService.IsExistenceAsync(blogArticleTag.TagId);
        blogArticleTag.SoftDelete = false;
        var result = await CreateAsync(blogArticleTag);
        return result;
    }

    /// <summary>
    /// 删除博客文章标签
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteBlogArticleTagAsync(Guid guid, Guid deleteId)
    {
        var blogArticleTag = await IsExistenceAsync(guid);
        blogArticleTag.SoftDelete = true;
        blogArticleTag.DeleteId = deleteId;
        blogArticleTag.DeleteTime = DateTime.Now;
        return await UpdateAsync(blogArticleTag);
    }

    /// <summary>
    /// 修改博客文章标签
    /// </summary>
    /// <param name="blogArticleTag"></param>
    /// <returns></returns>
    public async Task<PostTag> ModifyBlogArticleTagAsync(PostTag blogArticleTag)
    {
        await IsExistenceAsync(blogArticleTag.BaseId);
        await _IBlogArticleService.IsExistenceAsync(blogArticleTag.ArticleId);
        await _IBlogTagService.IsExistenceAsync(blogArticleTag.TagId);
        var result = await UpdateAsync(blogArticleTag);
        if (result) blogArticleTag = await FindAsync(blogArticleTag.BaseId);
        return blogArticleTag;
    }

    /// <summary>
    /// 查找博客文章标签
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<PostTag> FindBlogArticleTagAsync(Guid guid)
    {
        var blogArticleTag = await IsExistenceAsync(guid);
        return blogArticleTag;
    }

    /// <summary>
    /// 根据文章Id查寻所有标签
    /// </summary>
    /// <param name="articleGuid"></param>
    /// <returns></returns>
    public async Task<List<PostTag>> QueryBlogArticleTagAsync(Guid articleGuid)
    {
        var blogArticleTag = from blogarticletag in await QueryListAsync(e => !e.SoftDelete && e.ArticleId == articleGuid)
                             join blogtag in await _IBlogTagService.QueryListAsync(e => !e.SoftDelete) on blogarticletag.TagId equals blogtag.BaseId
                             orderby blogarticletag.CreateTime descending
                             select new PostTag
                             {
                                 TagName = blogtag.TagName,
                             };
        return blogArticleTag.ToList();
    }

    /// <summary>
    /// 查询所有博客文章标签
    /// </summary>
    /// <returns></returns>
    public async Task<List<PostTag>> QueryBlogArticleTagAsync()
    {
        var blogArticleTag = from blogarticletag in await QueryListAsync(e => !e.SoftDelete)
                             orderby blogarticletag.CreateTime descending
                             select blogarticletag;
        return blogArticleTag.ToList();
    }
}