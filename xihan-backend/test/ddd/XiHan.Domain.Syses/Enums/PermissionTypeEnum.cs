#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PermissionTypeEnum
// Guid:eebcea93-eefa-4f1d-90be-bda29b508009
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-20 下午 03:25:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Domain.Syses.Enums;

/// <summary>
/// 权限类型
/// </summary>
public enum PermissionTypeEnum
{
    /// <summary>
    /// 页面权限(菜单级)
    /// </summary>
    [Description("页面权限(菜单级)")]
    Page = 1,

    /// <summary>
    /// 操作权限(按钮级)
    /// </summary>
    [Description("操作权限(按钮级)")]
    Operation = 2,

    /// <summary>
    /// 数据权限(访问响应级)
    /// </summary>
    [Description("数据权限(访问响应级)")]
    Data = 3
}