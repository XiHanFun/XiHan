#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogArticleService
// Guid:4140006f-e480-a6a9-f18f-36c71c6de227
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:21:24
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Core.Services;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// IBlogArticleService
/// </summary>
public interface IBlogArticleService : IBaseService<BlogArticle>, IScopeDependency
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<BlogArticle> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="blogArticles"></param>
    /// <returns></returns>
    Task<bool> InitBlogArticleAsync(List<BlogArticle> blogArticles);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="blogArticle"></param>
    /// <returns></returns>
    Task<bool> CreateBlogArticleAsync(BlogArticle blogArticle);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteBlogArticleAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="blogArticle"></param>
    /// <returns></returns>
    Task<BlogArticle> ModifyBlogArticleAsync(BlogArticle blogArticle);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<BlogArticle> FindBlogArticleAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<BlogArticle>> QueryBlogArticleAsync();
}