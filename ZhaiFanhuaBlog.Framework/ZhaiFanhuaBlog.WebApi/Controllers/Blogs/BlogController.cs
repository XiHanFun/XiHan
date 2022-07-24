// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogController
// Guid:c644eda7-96b2-4216-8596-c6490c107585
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-03 上午 02:45:57
// ----------------------------------------------------------------

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZhaiFanhuaBlog.IServices.Blogs;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Bases.Response.Model;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Models.Response;
using ZhaiFanhuaBlog.ViewModels.Blogs;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Blogs;

/// <summary>
/// 博客管理
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BlogController : ControllerBase
{
    private readonly IUserAccountService _IUserAccountService;
    private readonly IBlogCategoryService _IBlogCategoryService;
    private readonly IBlogArticleService _IBlogArticleService;
    private readonly IBlogTagService _IBlogTagService;
    private readonly IBlogArticleTagService _IBlogArticleTagService;
    private readonly IBlogCommentService _BlogCommentService;
    private readonly IBlogPollService _IBlogPollService;
    private readonly IBlogCommentPollService _BlogCommentPollService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public BlogController(IUserAccountService iUserAccountService,
        IBlogCategoryService iBlogCategoryService,
        IBlogArticleService iBlogArticleService,
        IBlogTagService iBlogTagService,
        IBlogArticleTagService iBlogArticleTagService,
        IBlogCommentService blogCommentService,
        IBlogPollService iBlogPollService,
        IBlogCommentPollService blogCommentPollService)
    {
        _IUserAccountService = iUserAccountService;
        _IBlogCategoryService = iBlogCategoryService;
        _IBlogArticleService = iBlogArticleService;
        _IBlogTagService = iBlogTagService;
        _IBlogArticleTagService = iBlogArticleTagService;
        _BlogCommentService = blogCommentService;
        _IBlogPollService = iBlogPollService;
        _BlogCommentPollService = blogCommentPollService;
    }

    #region 文章分类

    /// <summary>
    /// 新增文章分类
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogCategoryDto"></param>
    /// <returns></returns>
    [HttpPost("Category")]
    public async Task<ResultModel> CreateBlogCategory([FromServices] IMapper iMapper, [FromBody] CBlogCategoryDto cBlogCategoryDto)
    {
        var user = User.FindFirst("UserId");
        if (user != null)
        {
            var blogCategory = iMapper.Map<BlogCategory>(cBlogCategoryDto);
            blogCategory.CreateId = Guid.Parse(user.Value);
            if (await _IBlogCategoryService.CreateBlogCategoryAsync(blogCategory))
                return ResultResponse.OK("新增文章分类成功");
        }
        return ResultResponse.BadRequest("新增文章分类失败");
    }

    /// <summary>
    /// 删除文章分类
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Category/{guid}")]
    public async Task<ResultModel> DeleteBlogCategory([FromRoute] Guid guid)
    {
        var user = User.FindFirst("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user.Value);
            if (await _IBlogCategoryService.DeleteBlogCategoryAsync(guid, deleteId))
                return ResultResponse.OK("删除文章分类成功");
        }
        return ResultResponse.BadRequest("删除文章分类失败");
    }

    /// <summary>
    /// 修改文章分类
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogCategoryDto"></param>
    /// <returns></returns>
    [HttpPut("Category")]
    public async Task<ResultModel> ModifyBlogCategory([FromServices] IMapper iMapper, [FromBody] CBlogCategoryDto cBlogCategoryDto)
    {
        var user = User.FindFirst("UserId");
        if (user != null)
        {
            var blogCategory = iMapper.Map<BlogCategory>(cBlogCategoryDto);
            blogCategory.ModifyId = Guid.Parse(user.Value);
            blogCategory = await _IBlogCategoryService.ModifyBlogCategoryAsync(blogCategory);
            if (blogCategory != null)
                return ResultResponse.OK(iMapper.Map<RBlogCategoryDto>(blogCategory));
        }
        return ResultResponse.BadRequest("修改文章分类失败");
    }

    /// <summary>
    /// 查找文章分类
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Category/{guid}")]
    public async Task<ResultModel> FindBlogCategory([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirst("UserId");
        if (user != null)
        {
            var blogCategory = await _IBlogCategoryService.FindBlogCategoryAsync(guid);
            if (blogCategory != null)
                return ResultResponse.OK(iMapper.Map<RBlogCategoryDto>(blogCategory));
        }
        return ResultResponse.BadRequest("该文章分类不存在");
    }

    /// <summary>
    /// 查询文章分类
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Categories")]
    public async Task<ResultModel> QueryBlogCategory([FromServices] IMapper iMapper)
    {
        var blogCategories = await _IBlogCategoryService.QueryBlogCategoryAsync();
        if (blogCategories.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RBlogCategoryDto>>(blogCategories));
        return ResultResponse.BadRequest("未查询到文章分类");
    }

    #endregion 文章分类
}