// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAuthorityService
// Guid:02502f6a-01bf-49ba-857a-7fc267bd04dc
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 10:50:02
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;
using ZhaiFanhuaBlog.ViewModels.Response.Enum;
using ZhaiFanhuaBlog.ViewModels.Response.Model;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// 账户权限
/// </summary>
public class UserAuthorityService : BaseService<UserAuthority>, IUserAuthorityService
{
    private readonly IUserAuthorityRepository _IUserAuthorityRepository;

    public UserAuthorityService(IUserAuthorityRepository iUserAuthorityRepository)
    {
        _IUserAuthorityRepository = iUserAuthorityRepository;
        base._iBaseRepository = iUserAuthorityRepository;
    }

    public async Task<MessageModel<UserAuthority>> CreateUserAuthorityAsync(UserAuthority userAuthority)
    {
        MessageModel<UserAuthority> messageModel = new MessageModel<UserAuthority>();
        userAuthority.TypeKey = "UserAuthority";
        userAuthority.StateKey = 1;
        var result = await _IUserAuthorityRepository.CreateAsync(userAuthority);
        if (result)
        {
            messageModel.Code = ResultCode.OK;
            messageModel.Success = true;
            messageModel.Message = "用户权限添加成功";
            messageModel.Data = await _IUserAuthorityRepository.FindAsync(userAuthority.BaseId);
        }
        else
        {
            messageModel.Code = ResultCode.InternalServerError;
            messageModel.Success = false;
            messageModel.Message = "用户权限添加失败";
            messageModel.Data = null;
        }
        return messageModel;
    }
}