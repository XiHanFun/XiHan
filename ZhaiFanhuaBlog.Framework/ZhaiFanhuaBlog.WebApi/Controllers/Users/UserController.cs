// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserController
// Guid:03069dd5-18ca-4109-b7da-4691b785bd11
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-15 下午 05:38:40
// ----------------------------------------------------------------

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.ViewModels.Users;
using ZhaiFanhuaBlog.WebApi.Common.Extensions.Swagger;
using ZhaiFanhuaBlog.WebApi.Common.Response;

namespace ZhaiFanhuaBlog.WebApi.Controllers.Users;

/// <summary>
/// 用户管理
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserAuthorityService _IUserAuthorityService;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="iUserAuthorityService"></param>
    public UserController(IUserAuthorityService iUserAuthorityService)
    {
        _IUserAuthorityService = iUserAuthorityService;
    }

    #region 用户权限

    /// <summary>
    /// 新增用户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="cDto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpPost("Authority")]
    [ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
    public async Task<ResultModel> CreateUserAuthority([FromServices] IMapper iMapper, [FromBody] CUserAuthorityDto cDto)
    {
        try
        {
            var userAuthority = iMapper.Map<UserAuthority>(cDto);
            if (await _IUserAuthorityService.CreateUserAuthorityAsync(userAuthority))
                return ResultResponse.OK("新增用户权限成功");
            return ResultResponse.BadRequest("新增用户权限失败");
        }
        catch (ApplicationException ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// 删除用户权限
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpDelete("Authority/{guid}")]
    [ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
    public async Task<ResultModel> DeleteUserAuthority([FromRoute] Guid guid)
    {
        if (await _IUserAuthorityService.DeleteUserAuthorityAsync(guid))
            return ResultResponse.OK("删除用户权限成功");
        return ResultResponse.BadRequest("删除用户权限失败");
    }

    /// <summary>
    /// 修改用户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <param name="cDto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpPut("Authority/{guid}")]
    [ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
    public async Task<ResultModel> ModifyUserAuthority([FromServices] IMapper iMapper, [FromRoute] Guid guid, [FromBody] CUserAuthorityDto cDto)
    {
        try
        {
            var userAuthority = await _IUserAuthorityService.FindUserAuthorityAsync(guid);
            userAuthority.ParentId = cDto.ParentId;
            userAuthority.Name = cDto.Name;
            userAuthority.Type = cDto.Type;
            userAuthority.Description = cDto.Description;
            userAuthority = await _IUserAuthorityService.ModifyUserAuthorityAsync(userAuthority);
            if (userAuthority != null)
                return ResultResponse.OK(iMapper.Map<RUserAuthorityDto>(userAuthority));
            return ResultResponse.BadRequest("修改用户权限失败");
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// 查找用户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Authority/{guid}")]
    [ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
    public async Task<ResultModel?> FindUserAuthority([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var userAuthority = await _IUserAuthorityService.FindUserAuthorityAsync(guid);
        if (userAuthority != null)
            return ResultResponse.OK(iMapper.Map<RUserAuthorityDto>(userAuthority));
        return ResultResponse.BadRequest("该用户权限不存在");
    }

    /// <summary>
    /// 查询用户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Authority")]
    [ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
    public async Task<ResultModel> FindUserAuthority([FromServices] IMapper iMapper)
    {
        var userAuthorities = await _IUserAuthorityService.QueryUserAuthoritiesAsync();
        if (userAuthorities.Count != 0)
            return ResultResponse.OK(iMapper.Map<List<RUserAuthorityDto>>(userAuthorities));
        return ResultResponse.BadRequest("未查询到用户权限");
    }

    #endregion 用户权限

    #region 用户角色

    //

    #endregion 用户角色
}