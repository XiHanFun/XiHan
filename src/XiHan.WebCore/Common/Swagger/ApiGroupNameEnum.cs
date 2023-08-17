#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ApiGroupNameEnum
// Guid:64fcc162-0845-4654-8b32-dd73e71ae15a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-17 下午 02:18:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.WebCore.Common.Swagger;

/// <summary>
/// ApiGroupNameEnum
/// </summary>
public enum ApiGroupNameEnum
{
    /// <summary>
    /// 所有接口
    /// </summary>
    [GroupInfo(Title = "所有接口", Description = "用于生成调试接口的所有接口", Version = "v0.3.2")]
    All,

    /// <summary>
    /// 前台接口
    /// </summary>
    [GroupInfo(Title = "前台接口", Description = "用于普通用户浏览的前台接口", Version = "v0.3.2")]
    Display,

    /// <summary>
    /// 后台接口
    /// </summary>
    [GroupInfo(Title = "后台接口", Description = "用于管理的后台接口", Version = "v0.3.2")]
    Manage,

    /// <summary>
    /// 授权接口
    /// </summary>
    [GroupInfo(Title = "授权接口", Description = "用于登录的授权接口", Version = "v0.3.2")]
    Authorize,

    /// <summary>
    /// 公共接口
    /// </summary>
    [GroupInfo(Title = "公共接口", Description = "用于常用功能的公共接口", Version = "v0.3.2")]
    Common,

    /// <summary>
    /// 测试接口
    /// </summary>
    [GroupInfo(Title = "测试接口", Description = "用于测试的测试接口", Version = "v0.3.2")]
    Test
}