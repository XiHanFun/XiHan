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

/// <summary>
/// IBlogTagService
/// </summary>
public interface IBlogTagService : IBaseService<BlogTag>
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<BlogTag> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="blogTags"></param>
    /// <returns></returns>
    Task<bool> InitBlogTagAsync(List<BlogTag> blogTags);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="blogTag"></param>
    /// <returns></returns>
    Task<bool> CreateBlogTagAsync(BlogTag blogTag);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteBlogTagAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="blogTag"></param>
    /// <returns></returns>
    Task<BlogTag> ModifyBlogTagAsync(BlogTag blogTag);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<BlogTag> FindBlogTagAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<BlogTag>> QueryBlogTagAsync();
}