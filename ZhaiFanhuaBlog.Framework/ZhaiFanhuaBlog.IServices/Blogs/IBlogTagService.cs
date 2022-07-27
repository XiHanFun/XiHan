// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogTagService
// Guid:e8c119bb-b7ae-29f6-0db5-da415d8eaefb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:16:38
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Blogs;

namespace ZhaiFanhuaBlog.IServices.Blogs;

public interface IBlogTagService : IBaseService<BlogTag>
{
    Task<BlogTag> IsExistenceAsync(Guid guid);

    Task<bool> InitBlogTagAsync(List<BlogTag> blogTags);

    Task<bool> CreateBlogTagAsync(BlogTag blogTag);

    Task<bool> DeleteBlogTagAsync(Guid guid, Guid deleteId);

    Task<BlogTag> ModifyBlogTagAsync(BlogTag blogTag);

    Task<BlogTag> FindBlogTagAsync(Guid guid);

    Task<List<BlogTag>> QueryBlogTagAsync();
}