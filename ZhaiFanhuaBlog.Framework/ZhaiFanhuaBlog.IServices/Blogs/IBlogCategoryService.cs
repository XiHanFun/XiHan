// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogCategoryService
// Guid:9f81b7b2-58f1-95f7-8764-ca99f186e644
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:10:24
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Blogs;

namespace ZhaiFanhuaBlog.IServices.Blogs;

public interface IBlogCategoryService : IBaseService<BlogCategory>
{
    Task<BlogCategory> IsExistenceAsync(Guid guid);

    Task<bool> InitBlogCategoryAsync(List<BlogCategory> blogCategories);

    Task<bool> CreateBlogCategoryAsync(BlogCategory blogCategory);

    Task<bool> DeleteBlogCategoryAsync(Guid guid, Guid deleteId);

    Task<BlogCategory> ModifyBlogCategoryAsync(BlogCategory blogCategory);

    Task<BlogCategory> FindBlogCategoryAsync(Guid guid);

    Task<List<BlogCategory>> QueryBlogCategoryAsync();
}