#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IntegrationEvent
// Guid:20306067-84f1-450f-b1e9-58a88edf7ec7
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/12/31 3:49:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Newtonsoft.Json;

namespace XiHanCore.EventBus.Bases.Models;

/// <summary>
/// 集成事件基类
/// </summary>
public class IntegrationEvent
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public IntegrationEvent()
    {
        BaseId = Guid.NewGuid();
        CreatedTime = DateTime.Now;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="baseId"></param>
    /// <param name="createdTime"></param>
    public IntegrationEvent(Guid baseId, DateTime createdTime)
    {
        BaseId = baseId;
        CreatedTime = createdTime;
    }

    /// <summary>
    /// 主键标识
    /// </summary>
    [JsonProperty]
    public Guid BaseId { get; private set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [JsonProperty]
    public DateTime CreatedTime { get; private set; }
}