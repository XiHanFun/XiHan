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

    /// <summary>
    /// 添加用户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="userAuthorityDto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpPost("Authority")]
    [ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
    public async Task<UserAuthorityDto> CreateUserAuthority([FromServices] IMapper iMapper, [FromBody] UserAuthorityDto userAuthorityDto)
    {
        try
        {
            var userAuthority = iMapper.Map<UserAuthority>(userAuthorityDto);
            userAuthority = await _IUserAuthorityService.CreateUserAuthorityAsync(userAuthority);
            if (userAuthority != null) userAuthorityDto = iMapper.Map<UserAuthorityDto>(userAuthority);
        }
        catch (Exception)
        {
            throw;
        }
        return userAuthorityDto;
    }

    /// <summary>
    /// 删除用户权限
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpDelete("Authority/{guid}")]
    [ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
    public async Task<bool> DeleteUserAuthority([FromRoute] Guid guid)
    {
        var result = await _IUserAuthorityService.DeleteUserAuthorityAsync(guid);
        return result;
    }

    /// <summary>
    /// 修改用户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <param name="userAuthorityDto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpPut("Authority/{guid}")]
    [ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
    public async Task<UserAuthorityDto> ModifyUserAuthority([FromServices] IMapper iMapper, [FromRoute] Guid guid, [FromBody] UserAuthorityDto userAuthorityDto)
    {
        try
        {
            if (string.IsNullOrEmpty(userAuthorityDto.Name)) throw new ArgumentNullException(userAuthorityDto.Name, "权限名称不能为空！");
            var userAuthority = iMapper.Map<UserAuthority>(userAuthorityDto);
            userAuthority = await _IUserAuthorityService.ModifyUserAuthorityAsync(userAuthority);
            if (userAuthority != null) userAuthorityDto = iMapper.Map<UserAuthorityDto>(userAuthority);
        }
        catch (Exception)
        {
            throw;
        }
        return userAuthorityDto;
    }

    /// <summary>
    /// 查找用户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("Authority/{guid}")]
    [ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
    public async Task<UserAuthorityDto?> FindUserAuthority([FromServices] IMapper iMapper, [FromRoute] Guid guid)
    {
        var userAuthority = await _IUserAuthorityService.FindUserAuthorityAsync(guid);
        if (userAuthority != null)
        {
            UserAuthorityDto userAuthorityDto = iMapper.Map<UserAuthorityDto>(userAuthority);
            return userAuthorityDto;
        }
        return null;
    }

    /// <summary>
    /// 查询用户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <returns></returns>
    [HttpGet("Authority")]
    [ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
    public async Task<List<UserAuthorityDto>?> FindUserAuthority([FromServices] IMapper iMapper)
    {
        var userAuthority = await _IUserAuthorityService.QueryUserAuthoritiesAsync();
        if (userAuthority.Count != 0)
        {
            List<UserAuthorityDto> userAuthoritiesDto = iMapper.Map<List<UserAuthorityDto>>(userAuthority);
            return userAuthoritiesDto;
        }
        return null;
    }
}