#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysMenuService
// Guid:69d412ed-50f6-4707-b560-8288ac375e49
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-22 上午 06:04:53
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Menus.Logic;

/// <summary>
/// 系统菜单服务
/// </summary>
[AppService(ServiceType = typeof(ISysMenuService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysMenuService : BaseService<SysMenu>, ISysMenuService
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public SysMenuService()
    {
    }
}