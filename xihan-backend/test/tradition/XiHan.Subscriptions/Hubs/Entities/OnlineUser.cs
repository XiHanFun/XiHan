#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:OnlineUser
// Guid:029d22cd-d643-4b26-a999-7628c9a3e631
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-08 上午 11:16:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Subscriptions.Hubs.Entities;

/// <summary>
/// 在线用户
/// </summary>
public class OnlineUser
{
    /// <summary>
    /// 连接标识
    /// </summary>
    public string ConnectionId { get; set; } = string.Empty;

    /// <summary>
    /// 用户标识
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 账号
    /// </summary>
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 姓名
    /// </summary>
    public string? RealName { get; set; }

    /// <summary>
    /// 连接Ip
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    /// 连接地点
    ///</summary>
    public string? Location { get; set; }

    /// <summary>
    /// 设备类型
    /// </summary>
    public string? DeviceType { get; set; } = string.Empty;

    /// <summary>
    /// 系统名称、版本
    /// </summary>
    public string? Os { get; set; }

    /// <summary>
    /// 浏览器名称、版本
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    /// 连接时间
    /// </summary>
    public DateTime? ConnectionTime { get; set; }
}