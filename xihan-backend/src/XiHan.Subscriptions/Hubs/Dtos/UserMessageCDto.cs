#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:MessageCDto
// Guid:32e1abe8-80a2-4755-be43-0897e1a09502
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/26 1:59:49
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Subscriptions.Hubs.Enums;

namespace XiHan.Subscriptions.Hubs.Dtos;

/// <summary>
/// 用户消息
/// </summary>
public class UserMessageCDto
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public MessageTypeEnum MessageType { get; set; }

    /// <summary>
    /// 用户标识列表
    /// </summary>
    public List<long> UserIds { get; set; } = [];

    /// <summary>
    /// 消息标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 消息内容
    /// </summary>
    public string Message { get; set; } = string.Empty;
}