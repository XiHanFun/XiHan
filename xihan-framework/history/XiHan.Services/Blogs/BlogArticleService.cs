#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogArticleService
// Guid:0d15e2e2-c341-4cfc-bd58-1e0d0be8ce10
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:12
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Infrastructure.AppService;
using ZhaiFanhuaBlog.Models.Posts;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogArticleService
/// </summary>
[AppService(ServiceType = typeof(IBlogArticleService), ServiceLifetime = LifeTime.Scoped)]
public class BlogArticleService : BaseService<PostArticle>, IBlogArticleService
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<PostArticle> IsExistenceAsync(Guid guid)
    {
        var blogArticle = await FindAsync(e => e.BaseId == guid && !e.SoftDelete);
        if (blogArticle == null)
            throw new ApplicationException("博客文章不存在");
        return blogArticle;
    }

    /// <summary>
    /// 初始化种子数据
    /// </summary>
    /// <param name="blogArticles"></param>
    /// <returns></returns>
    public async Task<bool> InitBlogArticleAsync(List<PostArticle> blogArticles)
    {
        blogArticles.ForEach(blogArticle =>
        {
            blogArticle.SoftDelete = false;
        });
        var result = await CreateBatchAsync(blogArticles);
        return result;
    }

    /// <summary>
    /// 新增博客文章
    /// </summary>
    /// <param name="blogArticle"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> CreateBlogArticleAsync(PostArticle blogArticle)
    {
        if (await FindAsync(e => e.ArtTitle == blogArticle.ArtTitle && !e.SoftDelete) != null)
            throw new ApplicationException("博客文章标题已存在");
        blogArticle.SoftDelete = false;
        var result = await CreateAsync(blogArticle);
        return result;
    }

    /// <summary>
    /// 删除博客文章
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteBlogArticleAsync(Guid guid, Guid deleteId)
    {
        var blogArticle = await IsExistenceAsync(guid);
        blogArticle.SoftDelete = true;
        blogArticle.DeleteId = deleteId;
        blogArticle.DeleteTime = DateTime.Now;
        return await UpdateAsync(blogArticle);
    }

    /// <summary>
    /// 修改博客文章
    /// </summary>
    /// <param name="blogArticle"></param>
    /// <returns></returns>
    public async Task<PostArticle> ModifyBlogArticleAsync(PostArticle blogArticle)
    {
        await IsExistenceAsync(blogArticle.BaseId);
        var result = await UpdateAsync(blogArticle);
        if (result) blogArticle = await FindAsync(blogArticle.BaseId);
        return blogArticle;
    }

    /// <summary>
    /// 获取博客文章详细信息
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<PostArticle> FindBlogArticleAsync(Guid guid)
    {
        var blogArticle = await IsExistenceAsync(guid);
        return blogArticle;
    }

    /// <summary>
    /// 获取博客文章列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<PostArticle>> QueryBlogArticleAsync()
    {
        var blogArticle = from blogarticle in await QueryListAsync(e => !e.SoftDelete)
                          orderby blogarticle.CreateTime descending
                          orderby blogarticle.ArtTitle descending
                          select blogarticle;
        return blogArticle.ToList();
    }
}