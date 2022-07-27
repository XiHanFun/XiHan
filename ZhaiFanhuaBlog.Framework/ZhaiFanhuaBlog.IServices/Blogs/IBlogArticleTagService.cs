// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogArticleTagService
// Guid:881c7f18-8d3e-2c9c-cf06-9e6762e4f052
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:35:20
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Blogs;

namespace ZhaiFanhuaBlog.IServices.Blogs;

public interface IBlogArticleTagService : IBaseService<BlogArticleTag>
{
    Task<BlogArticleTag> IsExistenceAsync(Guid guid);

    Task<bool> InitBlogArticleTagAsync(List<BlogArticleTag> blogArticleTags);

    Task<bool> CreateBlogArticleTagAsync(BlogArticleTag blogArticleTag);

    Task<bool> DeleteBlogArticleTagAsync(Guid guid, Guid deleteId);

    Task<BlogArticleTag> ModifyBlogArticleTagAsync(BlogArticleTag blogArticleTag);

    Task<BlogArticleTag> FindBlogArticleTagAsync(Guid guid);

    Task<List<BlogArticleTag>> QueryBlogArticleTagAsync();
}