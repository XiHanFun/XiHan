#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:HubConst
// Guid:742ccff1-5b63-484d-8d74-68d4934c51c4
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-08 下午 03:17:53
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Subscriptions.Hubs;

/// <summary>
/// HubConst
/// </summary>
public static class HubConst
{
    /// <summary>
    /// 公共组
    /// </summary>
    public const string CommonGroup = "CommonGroup";

    /// <summary>
    /// 加入群组
    /// </summary>
    public const string GroupAdded = "加入群组";

    /// <summary>
    /// 退出群组
    /// </summary>
    public const string GroupRemoved = "退出群组";

    /// <summary>
    /// 新建连接
    /// </summary>
    public const string Connected = "新建连接";

    /// <summary>
    /// 断开连接
    /// </summary>
    public const string Disconnected = "断开连接";
}