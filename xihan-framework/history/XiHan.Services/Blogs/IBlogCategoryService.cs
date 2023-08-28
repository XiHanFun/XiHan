#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogCategoryService
// Guid:9f81b7b2-58f1-95f7-8764-ca99f186e644
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:10:24
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Infrastructure.Services;
using ZhaiFanhuaBlog.Models.Posts;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// IBlogCategoryService
/// </summary>
public interface IBlogCategoryService : IBaseService<PostCategory>, IScopeDependency
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<PostCategory> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="blogCategories"></param>
    /// <returns></returns>
    Task<bool> InitBlogCategoryAsync(List<PostCategory> blogCategories);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="blogCategory"></param>
    /// <returns></returns>
    Task<bool> CreateBlogCategoryAsync(PostCategory blogCategory);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteBlogCategoryAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="blogCategory"></param>
    /// <returns></returns>
    Task<PostCategory> ModifyBlogCategoryAsync(PostCategory blogCategory);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<PostCategory> FindBlogCategoryAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<PostCategory>> QueryBlogCategoryAsync();
}