#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:AppGlobalConstant
// Guid:6c5799dd-8d27-4fb0-9a12-59857ee66c84
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-06-13 下午 10:40:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructures.Apps;

/// <summary>
/// 全局静态常量
/// </summary>
public static class AppGlobalConstant
{
    /// <summary>
    /// 管理员权限
    /// </summary>
    public static string AdminPermission = "*:*:*";

    /// <summary>
    /// 管理员角色
    /// </summary>
    public static string AdminRole = "admin";

    /// <summary>
    /// 开发版本API映射路径
    /// </summary>
    public static string DevApiProxy = "/dev-api/";
}