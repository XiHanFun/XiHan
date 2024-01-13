#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysMenuController
// Guid:fd14c90d-92d3-42f6-a909-025c5b3f858c
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-19 下午 05:51:42
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using XiHan.WebCore.Common.Swagger;

namespace XiHan.WebHost.Controllers.Syses;

/// <summary>
/// 系统菜单管理
/// </summary>
[Authorize]
[ApiGroup(ApiGroupNameEnum.Manage)]
public class SysMenuController : BaseApiController
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public SysMenuController()
    {
    }
}