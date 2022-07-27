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
using System.Security.Claims;
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

    #region 博客文章分类

    /// <summary>
    /// 新增博客文章分类
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogCategoryDto"></param>
    /// <returns></returns>
    [HttpPost("Category")]
    public async Task<ResultModel> CreateBlogCategory([FromServices] IMapper iMapper, [FromBody] CBlogCategoryDto cBlogCategoryDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogCategory = iMapper.Map<BlogCategory>(cBlogCategoryDto);
            blogCategory.CreateId = Guid.Parse(user);
            if (await _IBlogCategoryService.CreateBlogCategoryAsync(blogCategory))
                return ResultResponse.OK("新增博客文章分类成功");
        }
        return ResultResponse.BadRequest("新增博客文章分类失败");
    }

    /// <summary>
    /// 删除博客文章分类
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Category/{guid}")]
    public async Task<ResultModel> DeleteBlogCategory([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IBlogCategoryService.DeleteBlogCategoryAsync(guid, deleteId))
                return ResultResponse.OK("删除博客文章分类成功");
        }
        return ResultResponse.BadRequest("删除博客文章分类失败");
    }

    /// <summary>
    /// 修改博客文章分类
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogCategoryDto"></param>
    /// <returns></returns>
    [HttpPut("Category")]
    public async Task<ResultModel> ModifyBlogCategory([FromServices] IMapper iMapper, [FromBody] CBlogCategoryDto cBlogCategoryDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogCategory = iMapper.Map<BlogCategory>(cBlogCategoryDto);
            blogCategory.ModifyId = Guid.Parse(user);
            blogCategory = await _IBlogCategoryService.ModifyBlogCategoryAsync(blogCategory);
            if (blogCategory != null)
                return ResultResponse.OK(iMapper.Map<RBlogCategoryDto>(blogCategory));
        }
        return ResultResponse.BadRequest("修改博客文章分类失败");
    }

    /// <summary>
    /// 查找博客文章分类
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Category/{guid}")]
    public async Task<ResultModel> FindBlogCategory([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogCategory = await _IBlogCategoryService.FindBlogCategoryAsync(guid);
            if (blogCategory != null)
                return ResultResponse.OK(iMapper.Map<RBlogCategoryDto>(blogCategory));
        }
        return ResultResponse.BadRequest("该博客文章分类不存在");
    }

    /// <summary>
    /// 查询博客文章分类
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Categories")]
    public async Task<ResultModel> QueryBlogCategories([FromServices] IMapper iMapper)
    {
        var blogCategories = await _IBlogCategoryService.QueryBlogCategoryAsync();
        if (blogCategories.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RBlogCategoryDto>>(blogCategories));
        return ResultResponse.BadRequest("未查询到博客文章分类");
    }

    #endregion 博客文章分类

    #region 博客文章

    /// <summary>
    /// 新增博客文章
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogArticleDto"></param>
    /// <returns></returns>
    [HttpPost("Article")]
    public async Task<ResultModel> CreateBlogArticle([FromServices] IMapper iMapper, [FromBody] CBlogArticleDto cBlogArticleDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogArticle = iMapper.Map<BlogArticle>(cBlogArticleDto);
            blogArticle.CreateId = Guid.Parse(user);
            if (await _IBlogArticleService.CreateBlogArticleAsync(blogArticle))
                return ResultResponse.OK("新增博客文章成功");
        }
        return ResultResponse.BadRequest("新增博客文章失败");
    }

    /// <summary>
    /// 删除博客文章
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Article/{guid}")]
    public async Task<ResultModel> DeleteBlogArticle([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IBlogArticleService.DeleteBlogArticleAsync(guid, deleteId))
                return ResultResponse.OK("删除博客文章成功");
        }
        return ResultResponse.BadRequest("删除博客文章失败");
    }

    /// <summary>
    /// 修改博客文章
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogArticleDto"></param>
    /// <returns></returns>
    [HttpPut("Article")]
    public async Task<ResultModel> ModifyBlogArticle([FromServices] IMapper iMapper, [FromBody] CBlogArticleDto cBlogArticleDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogArticle = iMapper.Map<BlogArticle>(cBlogArticleDto);
            blogArticle.ModifyId = Guid.Parse(user);
            blogArticle = await _IBlogArticleService.ModifyBlogArticleAsync(blogArticle);
            if (blogArticle != null)
                return ResultResponse.OK(iMapper.Map<RBlogArticleDto>(blogArticle));
        }
        return ResultResponse.BadRequest("修改博客文章失败");
    }

    /// <summary>
    /// 查找博客文章
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Article/{guid}")]
    public async Task<ResultModel> FindBlogArticle([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogArticle = await _IBlogArticleService.FindBlogArticleAsync(guid);
            if (blogArticle != null)
                return ResultResponse.OK(iMapper.Map<RBlogArticleDto>(blogArticle));
        }
        return ResultResponse.BadRequest("该博客文章不存在");
    }

    /// <summary>
    /// 查询博客文章
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Articles")]
    public async Task<ResultModel> QueryBlogArticles([FromServices] IMapper iMapper)
    {
        var blogArticles = await _IBlogArticleService.QueryBlogArticleAsync();
        if (blogArticles.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RBlogArticleDto>>(blogArticles));
        return ResultResponse.BadRequest("未查询到博客文章");
    }

    #endregion 博客文章
}