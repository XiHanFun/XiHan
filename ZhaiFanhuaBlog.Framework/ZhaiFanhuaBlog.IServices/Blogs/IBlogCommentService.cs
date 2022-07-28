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

public interface IBlogCommentService : IBaseService<BlogComment>
{
    Task<BlogComment> IsExistenceAsync(Guid guid);

    Task<bool> InitBlogCommentAsync(List<BlogComment> blogComments);

    Task<bool> CreateBlogCommentAsync(BlogComment blogComment);

    Task<bool> DeleteBlogCommentAsync(Guid guid, Guid deleteId);

    Task<BlogComment> ModifyBlogCommentAsync(BlogComment blogComment);

    Task<BlogComment> FindBlogCommentAsync(Guid guid);

    Task<List<BlogComment>> QueryBlogCommentAsync();
}