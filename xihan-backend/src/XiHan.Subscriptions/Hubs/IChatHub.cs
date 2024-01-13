#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IChatHub
// Guid:4e4bd472-94b5-4e53-a02e-9f39156f5961
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-18 上午 01:30:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Subscriptions.Hubs;

/// <summary>
/// IChatHub
/// </summary>
public interface IChatHub
{
    /// <summary>
    /// 接收信息
    /// </summary>
    /// <param name="context">信息内容</param>
    /// <returns></returns>
    Task ReceiveMessage(object context);

    /// <summary>
    /// 消息更新
    /// </summary>
    /// <param name="context">信息内容</param>
    /// <returns></returns>
    Task ReceiveUpdate(object context);

    /// <summary>
    /// 强制下线
    /// </summary>
    /// <param name="context">信息内容</param>
    /// <returns></returns>
    Task ForceOffline(object context);
}