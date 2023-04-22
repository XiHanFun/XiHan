#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysRoleService
// Guid:dc2714b5-bc05-44a5-a93a-99b792065372
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-22 上午 02:09:39
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructure.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Logic;

/// <summary>
/// 系统角色服务
/// </summary>
[AppService(ServiceType = typeof(ISysRoleService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysRoleService : BaseService<SysRole>, ISysRoleService
{
    private ISysUserRoleService _sysUserRoleService;

    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public List<SysRole> GetRoleListByUserId(long userId)
    {
        return Context.Queryable<SysUserRole>()
            .LeftJoin<SysRole>((ur, r) => ur.RoleId == r.BaseId)
            .Where((ur, r) => ur.UserId == userId && !r.IsSoftDeleted)
            .Select((ur, r) => r)
            .ToList();
    }

    /// <summary>
    /// 获取用户键集合
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public List<string> GetRoleCodeListByUserId(long userId)
    {
        var list = GetRoleListByUserId(userId);
        return list.Select(r => r.Code).ToList();
    }
}