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

namespace XiHan.Infrastructure.Core.EventBus.Bases.Models;

/// <summary>
/// 集成事件，抽象类，定义事件的基本结构，具体事件可以继承或实现它
/// </summary>
public abstract class IntegrationEvent
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreatedTime = DateTime.Now;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="id"></param>
    /// <param name="createdTime"></param>
    public IntegrationEvent(Guid id, DateTime createdTime)
    {
        Id = id;
        CreatedTime = createdTime;
    }

    /// <summary>
    /// 主键标识
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; private set; }
}