#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IBaseDomainEvents
// Guid:1d53805c-e998-4e54-b75b-722ff6b22957
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 4:39:43
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using MediatR;

namespace XiHan.Domain.Core.Events.Abstracts;

/// <summary>
/// 通用领域事件接口
/// </summary>
public interface IBaseDomainEvents
{
    /// <summary>
    /// 获取领域事件
    /// </summary>
    /// <returns></returns>
    IEnumerable<INotification> GetDomainEvents();

    /// <summary>
    /// 添加领域事件
    /// </summary>
    /// <param name="eventItem"></param>
    void AddDomainEvent(INotification eventItem);

    /// <summary>
    /// 添加领域事件
    /// </summary>
    /// <remarks>
    /// 如果不存在就添加，存在则跳过，以避免对于同样的事件触发多次（比如在一个事务中修改领域模型的多个对象）
    /// </remarks>
    /// <param name="eventItem"></param>
    void AddDomainEventIfAbsent(INotification eventItem);

    /// <summary>
    /// 移除领域事件
    /// </summary>
    void ClearDomainEvents();
}