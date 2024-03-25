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

using XiHan.Common.Shared.Attributes;

namespace XiHan.Common.Shared.Enums;

/// <summary>
/// 接口分组名称枚举
/// </summary>
public enum ApiGroupNameEnum
{
    /// <summary>
    /// 所有接口
    /// </summary>
    [ApiGroupInfo(Title = "所有接口", Description = "用于生成调试接口的所有接口", Version = "v0.3.4")]
    All,

    /// <summary>
    /// 前台接口
    /// </summary>
    [ApiGroupInfo(Title = "前台接口", Description = "用于普通用户浏览的前台接口", Version = "v0.3.4")]
    Display,

    /// <summary>
    /// 后台接口
    /// </summary>
    [ApiGroupInfo(Title = "后台接口", Description = "用于管理的后台接口", Version = "v0.3.4")]
    Manage,

    /// <summary>
    /// 授权接口
    /// </summary>
    [ApiGroupInfo(Title = "授权接口", Description = "用于登录的授权接口", Version = "v0.3.4")]
    Authorize,

    /// <summary>
    /// 公共接口
    /// </summary>
    [ApiGroupInfo(Title = "公共接口", Description = "用于常用功能的公共接口", Version = "v0.3.4")]
    Common,

    /// <summary>
    /// 测试接口
    /// </summary>
    [ApiGroupInfo(Title = "测试接口", Description = "用于测试的测试接口", Version = "v0.3.4")]
    Test
}