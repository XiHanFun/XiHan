#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ApiGroupNameEnum
// Guid:1755b514-ce91-456e-97b1-ef98b8dc8b87
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 6:10:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Presentation.Web.Core.Commons.Swagger;

/// <summary>
/// ApiGroupNameEnum
/// </summary>
public enum ApiGroupNameEnum
{
    /// <summary>
    /// 所有接口
    /// </summary>
    [GroupInfo(Title = "所有接口", Description = "用于生成调试接口的所有接口", Version = "v0.3.4")]
    All,

    /// <summary>
    /// 前台接口
    /// </summary>
    [GroupInfo(Title = "前台接口", Description = "用于普通用户浏览的前台接口", Version = "v0.3.4")]
    Display,

    /// <summary>
    /// 后台接口
    /// </summary>
    [GroupInfo(Title = "后台接口", Description = "用于管理的后台接口", Version = "v0.3.4")]
    Manage,

    /// <summary>
    /// 授权接口
    /// </summary>
    [GroupInfo(Title = "授权接口", Description = "用于登录的授权接口", Version = "v0.3.4")]
    Authorize,

    /// <summary>
    /// 公共接口
    /// </summary>
    [GroupInfo(Title = "公共接口", Description = "用于常用功能的公共接口", Version = "v0.3.4")]
    Common,

    /// <summary>
    /// 测试接口
    /// </summary>
    [GroupInfo(Title = "测试接口", Description = "用于测试的测试接口", Version = "v0.3.4")]
    Test
}