#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ApiGroupNames
// Guid:64fcc162-0845-4654-8b32-dd73e71ae15a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-17 下午 02:18:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Web.Common.Swagger;

/// <summary>
 /// ApiGroupNames
 /// </summary>
public enum ApiGroupNames
{
    /// <summary>
    /// 所有接口
    /// </summary>
    [GroupInfo(Title = "所有接口", Description = "这是用于生成调试接口的所有接口", Version = "v0.2.5")]
    All,

    /// <summary>
    /// 前台接口
    /// </summary>
    [GroupInfo(Title = "前台接口", Description = "这是用于普通用户浏览的前台接口", Version = "v0.2.5")]
    Display,

    /// <summary>
    /// 后台接口
    /// </summary>
    [GroupInfo(Title = "后台接口", Description = "这是用于管理的后台接口", Version = "v0.2.5")]
    Manage,

    /// <summary>
    /// 授权接口
    /// </summary>
    [GroupInfo(Title = "授权接口", Description = "这是用于登录的授权接口", Version = "v0.2.5")]
    Authorize,

    /// <summary>
    /// 公共接口
    /// </summary>
    [GroupInfo(Title = "公共接口", Description = "这是用于常用功能的公共接口", Version = "v0.2.5")]
    Common,

    /// <summary>
    /// 测试接口
    /// </summary>
    [GroupInfo(Title = "测试接口", Description = "这是用于测试的测试接口", Version = "v0.2.5")]
    Test,
}