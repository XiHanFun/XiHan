#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:GroupMessageCDto
// Guid:5cb2ef28-074f-446e-bd7a-a46033dec947
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/26 2:48:40
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Subscriptions.Hubs.Enums;

namespace XiHan.Subscriptions.Hubs.Dtos;

/// <summary>
/// 群组消息
/// </summary>
public class GroupMessageCDto
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public MessageTypeEnum MessageType { get; set; }

    /// <summary>
    /// 群组名称
    /// </summary>
    public List<string> GroupNames { get; set; } = [];

    /// <summary>
    /// 消息标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 消息内容
    /// </summary>
    public string Message { get; set; } = string.Empty;
}