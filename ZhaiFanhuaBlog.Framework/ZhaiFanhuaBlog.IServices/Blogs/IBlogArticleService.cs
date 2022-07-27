// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogArticleService
// Guid:4140006f-e480-a6a9-f18f-36c71c6de227
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:21:24
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Blogs;

namespace ZhaiFanhuaBlog.IServices.Blogs;

public interface IBlogArticleService : IBaseService<BlogArticle>
{
    Task<BlogArticle> IsExistenceAsync(Guid guid);

    Task<bool> InitBlogArticleAsync(List<BlogArticle> blogArticles);

    Task<bool> CreateBlogArticleAsync(BlogArticle blogArticle);

    Task<bool> DeleteBlogArticleAsync(Guid guid, Guid deleteId);

    Task<BlogArticle> ModifyBlogArticleAsync(BlogArticle blogArticle);

    Task<BlogArticle> FindBlogArticleAsync(Guid guid);

    Task<List<BlogArticle>> QueryBlogArticleAsync();
}