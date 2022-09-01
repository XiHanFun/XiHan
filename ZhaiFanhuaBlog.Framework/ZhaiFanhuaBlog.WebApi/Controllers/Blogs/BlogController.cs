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
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.ViewModels.Bases.Results;
using ZhaiFanhuaBlog.ViewModels.Blogs;
using ZhaiFanhuaBlog.ViewModels.Response;
using ZhaiFanhuaBlog.WebApi.Common.Extensions.Swagger;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Blogs;

/// <summary>
/// 博客管理
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
public class BlogController : ControllerBase
{
    private readonly IUserAccountService _IUserAccountService;
    private readonly IBlogCategoryService _IBlogCategoryService;
    private readonly IBlogArticleService _IBlogArticleService;
    private readonly IBlogTagService _IBlogTagService;
    private readonly IBlogArticleTagService _IBlogArticleTagService;
    private readonly IBlogCommentService _IBlogCommentService;
    private readonly IBlogPollService _IBlogPollService;
    private readonly IBlogCommentPollService _IBlogCommentPollService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public BlogController(IUserAccountService iUserAccountService,
        IBlogCategoryService iBlogCategoryService,
        IBlogArticleService iBlogArticleService,
        IBlogTagService iBlogTagService,
        IBlogPollService iBlogPollService,
        IBlogArticleTagService iBlogArticleTagService,
        IBlogCommentService iBlogCommentService,
        IBlogCommentPollService iBlogCommentPollService)
    {
        _IUserAccountService = iUserAccountService;
        _IBlogCategoryService = iBlogCategoryService;
        _IBlogArticleService = iBlogArticleService;
        _IBlogTagService = iBlogTagService;
        _IBlogArticleTagService = iBlogArticleTagService;
        _IBlogCommentService = iBlogCommentService;
        _IBlogPollService = iBlogPollService;
        _IBlogCommentPollService = iBlogCommentPollService;
    }

    #region 博客文章分类

    /// <summary>
    /// 新增博客文章分类
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogCategoryDto"></param>
    /// <returns></returns>
    [HttpPost("Category")]
    public async Task<BaseResultDto> CreateBlogCategory([FromServices] IMapper iMapper, [FromBody] CBlogCategoryDto cBlogCategoryDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogCategory = iMapper.Map<BlogCategory>(cBlogCategoryDto);
            blogCategory.CreateId = Guid.Parse(user);
            if (await _IBlogCategoryService.CreateBlogCategoryAsync(blogCategory))
                return BaseResponseDto.OK("新增博客文章分类成功");
        }
        return BaseResponseDto.BadRequest("新增博客文章分类失败");
    }

    /// <summary>
    /// 删除博客文章分类
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Category/{guid}")]
    public async Task<BaseResultDto> DeleteBlogCategory([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IBlogCategoryService.DeleteBlogCategoryAsync(guid, deleteId))
                return BaseResponseDto.OK("删除博客文章分类成功");
        }
        return BaseResponseDto.BadRequest("删除博客文章分类失败");
    }

    /// <summary>
    /// 修改博客文章分类
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogCategoryDto"></param>
    /// <returns></returns>
    [HttpPut("Category")]
    public async Task<BaseResultDto> ModifyBlogCategory([FromServices] IMapper iMapper, [FromBody] CBlogCategoryDto cBlogCategoryDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogCategory = iMapper.Map<BlogCategory>(cBlogCategoryDto);
            blogCategory.ModifyId = Guid.Parse(user);
            blogCategory = await _IBlogCategoryService.ModifyBlogCategoryAsync(blogCategory);
            if (blogCategory != null)
                return BaseResponseDto.OK(iMapper.Map<RBlogCategoryDto>(blogCategory));
        }
        return BaseResponseDto.BadRequest("修改博客文章分类失败");
    }

    /// <summary>
    /// 查找博客文章分类
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Category/{guid}")]
    public async Task<BaseResultDto> FindBlogCategory([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogCategory = await _IBlogCategoryService.FindBlogCategoryAsync(guid);
            if (blogCategory != null)
                return BaseResponseDto.OK(iMapper.Map<RBlogCategoryDto>(blogCategory));
        }
        return BaseResponseDto.BadRequest("该博客文章分类不存在");
    }

    /// <summary>
    /// 查询博客文章分类
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Categories")]
    public async Task<BaseResultDto> QueryBlogCategories([FromServices] IMapper iMapper)
    {
        var blogCategories = await _IBlogCategoryService.QueryBlogCategoryAsync();
        if (blogCategories.Count != 0)
            return BaseResponseDto.OK(iMapper.Map<List<RBlogCategoryDto>>(blogCategories));
        return BaseResponseDto.BadRequest("未查询到博客文章分类");
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
    public async Task<BaseResultDto> CreateBlogArticle([FromServices] IMapper iMapper, [FromBody] CBlogArticleDto cBlogArticleDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogArticle = iMapper.Map<BlogArticle>(cBlogArticleDto);
            blogArticle.CreateId = Guid.Parse(user);
            if (await _IBlogArticleService.CreateBlogArticleAsync(blogArticle))
                return BaseResponseDto.OK("新增博客文章成功");
        }
        return BaseResponseDto.BadRequest("新增博客文章失败");
    }

    /// <summary>
    /// 删除博客文章
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Article/{guid}")]
    public async Task<BaseResultDto> DeleteBlogArticle([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IBlogArticleService.DeleteBlogArticleAsync(guid, deleteId))
                return BaseResponseDto.OK("删除博客文章成功");
        }
        return BaseResponseDto.BadRequest("删除博客文章失败");
    }

    /// <summary>
    /// 修改博客文章
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogArticleDto"></param>
    /// <returns></returns>
    [HttpPut("Article")]
    public async Task<BaseResultDto> ModifyBlogArticle([FromServices] IMapper iMapper, [FromBody] CBlogArticleDto cBlogArticleDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogArticle = iMapper.Map<BlogArticle>(cBlogArticleDto);
            blogArticle.ModifyId = Guid.Parse(user);
            blogArticle = await _IBlogArticleService.ModifyBlogArticleAsync(blogArticle);
            if (blogArticle != null)
                return BaseResponseDto.OK(iMapper.Map<RBlogArticleDto>(blogArticle));
        }
        return BaseResponseDto.BadRequest("修改博客文章失败");
    }

    /// <summary>
    /// 查找博客文章
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Article/{guid}")]
    public async Task<BaseResultDto> FindBlogArticle([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogArticle = await _IBlogArticleService.FindBlogArticleAsync(guid);
            if (blogArticle != null)
                return BaseResponseDto.OK(iMapper.Map<RBlogArticleDto>(blogArticle));
        }
        return BaseResponseDto.BadRequest("该博客文章不存在");
    }

    /// <summary>
    /// 查询博客文章
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Articles")]
    public async Task<BaseResultDto> QueryBlogArticles([FromServices] IMapper iMapper)
    {
        var blogArticles = await _IBlogArticleService.QueryBlogArticleAsync();
        if (blogArticles.Count != 0)
            return BaseResponseDto.OK(iMapper.Map<List<RBlogArticleDto>>(blogArticles));
        return BaseResponseDto.BadRequest("未查询到博客文章");
    }

    #endregion 博客文章

    #region 博客标签

    /// <summary>
    /// 新增博客标签
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogTagDto"></param>
    /// <returns></returns>
    [HttpPost("Tag")]
    public async Task<BaseResultDto> CreateBlogTag([FromServices] IMapper iMapper, [FromBody] CBlogTagDto cBlogTagDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogTag = iMapper.Map<BlogTag>(cBlogTagDto);
            blogTag.CreateId = Guid.Parse(user);
            if (await _IBlogTagService.CreateBlogTagAsync(blogTag))
                return BaseResponseDto.OK("新增博客标签成功");
        }
        return BaseResponseDto.BadRequest("新增博客标签失败");
    }

    /// <summary>
    /// 删除博客标签
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Tag/{guid}")]
    public async Task<BaseResultDto> DeleteBlogTag([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IBlogTagService.DeleteBlogTagAsync(guid, deleteId))
                return BaseResponseDto.OK("删除博客标签成功");
        }
        return BaseResponseDto.BadRequest("删除博客标签失败");
    }

    /// <summary>
    /// 修改博客标签
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogTagDto"></param>
    /// <returns></returns>
    [HttpPut("Tag")]
    public async Task<BaseResultDto> ModifyBlogTag([FromServices] IMapper iMapper, [FromBody] CBlogTagDto cBlogTagDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogTag = iMapper.Map<BlogTag>(cBlogTagDto);
            blogTag.ModifyId = Guid.Parse(user);
            blogTag = await _IBlogTagService.ModifyBlogTagAsync(blogTag);
            if (blogTag != null)
                return BaseResponseDto.OK(iMapper.Map<RBlogTagDto>(blogTag));
        }
        return BaseResponseDto.BadRequest("修改博客标签失败");
    }

    /// <summary>
    /// 查找博客标签
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Tag/{guid}")]
    public async Task<BaseResultDto> FindBlogTag([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogTag = await _IBlogTagService.FindBlogTagAsync(guid);
            if (blogTag != null)
                return BaseResponseDto.OK(iMapper.Map<RBlogTagDto>(blogTag));
        }
        return BaseResponseDto.BadRequest("该博客标签不存在");
    }

    /// <summary>
    /// 查询博客标签
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Tags")]
    public async Task<BaseResultDto> QueryBlogTags([FromServices] IMapper iMapper)
    {
        var blogTags = await _IBlogTagService.QueryBlogTagAsync();
        if (blogTags.Count != 0)
            return BaseResponseDto.OK(iMapper.Map<List<RBlogTagDto>>(blogTags));
        return BaseResponseDto.BadRequest("未查询到博客标签");
    }

    #endregion 博客标签

    #region 博客文章标签

    /// <summary>
    /// 新增博客文章标签
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogArticleTagDto"></param>
    /// <returns></returns>
    [HttpPost("Article/Tag")]
    public async Task<BaseResultDto> CreateBlogArticleTag([FromServices] IMapper iMapper, [FromBody] CBlogArticleTagDto cBlogArticleTagDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogArticleTag = iMapper.Map<BlogArticleTag>(cBlogArticleTagDto);
            blogArticleTag.CreateId = Guid.Parse(user);
            if (await _IBlogArticleTagService.CreateBlogArticleTagAsync(blogArticleTag))
                return BaseResponseDto.OK("新增博客文章标签成功");
        }
        return BaseResponseDto.BadRequest("新增博客文章标签失败");
    }

    /// <summary>
    /// 删除博客文章标签
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Article/Tag/{guid}")]
    public async Task<BaseResultDto> DeleteBlogArticleTag([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IBlogArticleTagService.DeleteBlogArticleTagAsync(guid, deleteId))
                return BaseResponseDto.OK("删除博客文章标签成功");
        }
        return BaseResponseDto.BadRequest("删除博客文章标签失败");
    }

    /// <summary>
    /// 修改博客文章标签
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogArticleTagDto"></param>
    /// <returns></returns>
    [HttpPut("Article/Tag")]
    public async Task<BaseResultDto> ModifyBlogArticleTag([FromServices] IMapper iMapper, [FromBody] CBlogArticleTagDto cBlogArticleTagDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogArticleTag = iMapper.Map<BlogArticleTag>(cBlogArticleTagDto);
            blogArticleTag.ModifyId = Guid.Parse(user);
            blogArticleTag = await _IBlogArticleTagService.ModifyBlogArticleTagAsync(blogArticleTag);
            if (blogArticleTag != null)
                return BaseResponseDto.OK(iMapper.Map<RBlogArticleTagDto>(blogArticleTag));
        }
        return BaseResponseDto.BadRequest("修改博客文章标签失败");
    }

    /// <summary>
    /// 查找博客文章标签
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Article/Tag/{guid}")]
    public async Task<BaseResultDto> FindBlogArticleTag([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogArticleTag = await _IBlogArticleTagService.FindBlogArticleTagAsync(guid);
            if (blogArticleTag != null)
                return BaseResponseDto.OK(iMapper.Map<RBlogArticleTagDto>(blogArticleTag));
        }
        return BaseResponseDto.BadRequest("该博客文章标签不存在");
    }

    /// <summary>
    /// 查询博客文章标签
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Article/Tags")]
    public async Task<BaseResultDto> QueryBlogArticleTags([FromServices] IMapper iMapper)
    {
        var blogArticleTags = await _IBlogArticleTagService.QueryBlogArticleTagAsync();
        if (blogArticleTags.Count != 0)
            return BaseResponseDto.OK(iMapper.Map<List<RBlogArticleTagDto>>(blogArticleTags));
        return BaseResponseDto.BadRequest("未查询到博客文章标签");
    }

    #endregion 博客文章标签

    #region 博客文章点赞

    /// <summary>
    /// 新增博客文章点赞
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogPollDto"></param>
    /// <returns></returns>
    [HttpPost("Poll")]
    public async Task<BaseResultDto> CreateBlogPoll([FromServices] IMapper iMapper, [FromBody] CBlogPollDto cBlogPollDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogPoll = iMapper.Map<BlogPoll>(cBlogPollDto);
            blogPoll.CreateId = Guid.Parse(user);
            if (await _IBlogPollService.CreateBlogPollAsync(blogPoll))
                return BaseResponseDto.OK("新增博客文章点赞成功");
        }
        return BaseResponseDto.BadRequest("新增博客文章点赞失败");
    }

    /// <summary>
    /// 删除博客文章点赞
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Poll/{guid}")]
    public async Task<BaseResultDto> DeleteBlogPoll([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IBlogPollService.DeleteBlogPollAsync(guid, deleteId))
                return BaseResponseDto.OK("删除博客文章点赞成功");
        }
        return BaseResponseDto.BadRequest("删除博客文章点赞失败");
    }

    /// <summary>
    /// 修改博客文章点赞
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogPollDto"></param>
    /// <returns></returns>
    [HttpPut("Poll")]
    public async Task<BaseResultDto> ModifyBlogPoll([FromServices] IMapper iMapper, [FromBody] CBlogPollDto cBlogPollDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogPoll = iMapper.Map<BlogPoll>(cBlogPollDto);
            blogPoll.ModifyId = Guid.Parse(user);
            blogPoll = await _IBlogPollService.ModifyBlogPollAsync(blogPoll);
            if (blogPoll != null)
                return BaseResponseDto.OK(iMapper.Map<RBlogPollDto>(blogPoll));
        }
        return BaseResponseDto.BadRequest("修改博客文章点赞失败");
    }

    /// <summary>
    /// 查找博客文章点赞
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Poll/{guid}")]
    public async Task<BaseResultDto> FindBlogPoll([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogPoll = await _IBlogPollService.FindBlogPollAsync(guid);
            if (blogPoll != null)
                return BaseResponseDto.OK(iMapper.Map<RBlogPollDto>(blogPoll));
        }
        return BaseResponseDto.BadRequest("该博客文章点赞不存在");
    }

    /// <summary>
    /// 查询博客文章点赞
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Polls")]
    public async Task<BaseResultDto> QueryBlogPolls([FromServices] IMapper iMapper)
    {
        var blogCategories = await _IBlogPollService.QueryBlogPollAsync();
        if (blogCategories.Count != 0)
            return BaseResponseDto.OK(iMapper.Map<List<RBlogPollDto>>(blogCategories));
        return BaseResponseDto.BadRequest("未查询到博客文章点赞");
    }

    #endregion 博客文章点赞

    #region 博客文章评论

    /// <summary>
    /// 新增博客文章评论
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogCommentDto"></param>
    /// <returns></returns>
    [HttpPost("Comment")]
    public async Task<BaseResultDto> CreateBlogComment([FromServices] IMapper iMapper, [FromBody] CBlogCommentDto cBlogCommentDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogComment = iMapper.Map<BlogComment>(cBlogCommentDto);
            blogComment.CreateId = Guid.Parse(user);
            if (await _IBlogCommentService.CreateBlogCommentAsync(blogComment))
                return BaseResponseDto.OK("新增博客文章评论成功");
        }
        return BaseResponseDto.BadRequest("新增博客文章评论失败");
    }

    /// <summary>
    /// 删除博客文章评论
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Comment/{guid}")]
    public async Task<BaseResultDto> DeleteBlogComment([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IBlogCommentService.DeleteBlogCommentAsync(guid, deleteId))
                return BaseResponseDto.OK("删除博客文章评论成功");
        }
        return BaseResponseDto.BadRequest("删除博客文章评论失败");
    }

    /// <summary>
    /// 修改博客文章评论
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogCommentDto"></param>
    /// <returns></returns>
    [HttpPut("Comment")]
    public async Task<BaseResultDto> ModifyBlogComment([FromServices] IMapper iMapper, [FromBody] CBlogCommentDto cBlogCommentDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogComment = iMapper.Map<BlogComment>(cBlogCommentDto);
            blogComment.ModifyId = Guid.Parse(user);
            blogComment = await _IBlogCommentService.ModifyBlogCommentAsync(blogComment);
            if (blogComment != null)
                return BaseResponseDto.OK(iMapper.Map<RBlogCommentDto>(blogComment));
        }
        return BaseResponseDto.BadRequest("修改博客文章评论失败");
    }

    /// <summary>
    /// 查找博客文章评论
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Comment/{guid}")]
    public async Task<BaseResultDto> FindBlogComment([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogComment = await _IBlogCommentService.FindBlogCommentAsync(guid);
            if (blogComment != null)
                return BaseResponseDto.OK(iMapper.Map<RBlogCommentDto>(blogComment));
        }
        return BaseResponseDto.BadRequest("该博客文章评论不存在");
    }

    /// <summary>
    /// 查询博客文章评论
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Comments")]
    public async Task<BaseResultDto> QueryBlogComments([FromServices] IMapper iMapper)
    {
        var blogComments = await _IBlogCommentService.QueryBlogCommentAsync();
        if (blogComments.Count != 0)
            return BaseResponseDto.OK(iMapper.Map<List<RBlogCommentDto>>(blogComments));
        return BaseResponseDto.BadRequest("未查询到博客文章评论");
    }

    #endregion 博客文章评论

    #region 博客文章评论点赞

    /// <summary>
    /// 新增博客文章评论点赞
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogCommentPollDto"></param>
    /// <returns></returns>
    [HttpPost("Comment/Poll")]
    public async Task<BaseResultDto> CreateBlogCommentPoll([FromServices] IMapper iMapper, [FromBody] CBlogCommentPollDto cBlogCommentPollDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogPoll = iMapper.Map<BlogCommentPoll>(cBlogCommentPollDto);
            blogPoll.CreateId = Guid.Parse(user);
            if (await _IBlogCommentPollService.CreateBlogCommentPollAsync(blogPoll))
                return BaseResponseDto.OK("新增博客文章评论点赞成功");
        }
        return BaseResponseDto.BadRequest("新增博客文章评论点赞失败");
    }

    /// <summary>
    /// 删除博客文章评论点赞
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpDelete("Comment/Poll/{guid}")]
    public async Task<BaseResultDto> DeleteBlogCommentPoll([FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            Guid deleteId = Guid.Parse(user);
            if (await _IBlogCommentPollService.DeleteBlogCommentPollAsync(guid, deleteId))
                return BaseResponseDto.OK("删除博客文章评论点赞成功");
        }
        return BaseResponseDto.BadRequest("删除博客文章评论点赞失败");
    }

    /// <summary>
    /// 修改博客文章评论点赞
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cBlogCommentPollDto"></param>
    /// <returns></returns>
    [HttpPut("Comment/Poll")]
    public async Task<BaseResultDto> ModifyBlogCommentPoll([FromServices] IMapper iMapper, [FromBody] CBlogCommentPollDto cBlogCommentPollDto)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogPoll = iMapper.Map<BlogCommentPoll>(cBlogCommentPollDto);
            blogPoll.ModifyId = Guid.Parse(user);
            blogPoll = await _IBlogCommentPollService.ModifyBlogCommentPollAsync(blogPoll);
            if (blogPoll != null)
                return BaseResponseDto.OK(iMapper.Map<RBlogCommentPollDto>(blogPoll));
        }
        return BaseResponseDto.BadRequest("修改博客文章评论点赞失败");
    }

    /// <summary>
    /// 查找博客文章评论点赞
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Comment/Poll/{guid}")]
    public async Task<BaseResultDto> FindBlogCommentPoll([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var user = User.FindFirstValue("UserId");
        if (user != null)
        {
            var blogPoll = await _IBlogCommentPollService.FindBlogCommentPollAsync(guid);
            if (blogPoll != null)
                return BaseResponseDto.OK(iMapper.Map<RBlogCommentPollDto>(blogPoll));
        }
        return BaseResponseDto.BadRequest("该博客文章评论点赞不存在");
    }

    /// <summary>
    /// 查询博客文章评论点赞
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Comment/Polls")]
    public async Task<BaseResultDto> QueryBlogCommentPolls([FromServices] IMapper iMapper)
    {
        var blogCategories = await _IBlogCommentPollService.QueryBlogCommentPollAsync();
        if (blogCategories.Count != 0)
            return BaseResponseDto.OK(iMapper.Map<List<RBlogCommentPollDto>>(blogCategories));
        return BaseResponseDto.BadRequest("未查询到博客文章评论点赞");
    }

    #endregion 博客文章评论点赞
}