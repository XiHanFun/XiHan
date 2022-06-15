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
using ZhaiFanhuaBlog.WebApi.Common.Filters;

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
    /// 添加账户权限
    /// </summary>
    /// <param name="iMapper"></param>
    /// <param name="userAuthorityDto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [TypeFilter(typeof(CustomAllActionResultFilterAttribute))]
    [HttpPost("Authorities")]
    [ApiExplorerSettings(GroupName = SwaggerGroup.Backstage)]
    public async Task<UserAuthorityDto> CreateAccountsAuthority([FromServices] IMapper iMapper, [FromBody] UserAuthorityDto userAuthorityDto)
    {
        try
        {
            if (string.IsNullOrEmpty(userAuthorityDto.Name)) throw new ArgumentNullException(userAuthorityDto.Name, "权限名称不能为空！");
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
}