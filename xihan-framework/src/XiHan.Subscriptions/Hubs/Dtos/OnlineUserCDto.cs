#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:OnlineUserCDto
// Guid:197d6b01-9041-4cd0-92ca-6e78d27b9041
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/26 1:42:26
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Subscriptions.Hubs.Dtos;

/// <summary>
/// OnlineUserCDto
/// </summary>
public class OnlineUserCDto
{
    /// <summary>
    /// 连接标识
    /// </summary>
    public string ConnectionId { get; set; } = string.Empty;
}