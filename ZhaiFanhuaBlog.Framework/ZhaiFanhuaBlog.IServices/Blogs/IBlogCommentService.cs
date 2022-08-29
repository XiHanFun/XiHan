// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogCommentService
// Guid:1bf19ecb-ac1e-34ed-80c2-81b54781ebdd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:11:44
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Blogs;

namespace ZhaiFanhuaBlog.IServices.Blogs;

/// <summary>
/// IBlogCommentService
/// </summary>
public interface IBlogCommentService : IBaseService<BlogComment>
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<BlogComment> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="blogComments"></param>
    /// <returns></returns>
    Task<bool> InitBlogCommentAsync(List<BlogComment> blogComments);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="blogComment"></param>
    /// <returns></returns>
    Task<bool> CreateBlogCommentAsync(BlogComment blogComment);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteBlogCommentAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="blogComment"></param>
    /// <returns></returns>
    Task<BlogComment> ModifyBlogCommentAsync(BlogComment blogComment);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<BlogComment> FindBlogCommentAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<BlogComment>> QueryBlogCommentAsync();
}