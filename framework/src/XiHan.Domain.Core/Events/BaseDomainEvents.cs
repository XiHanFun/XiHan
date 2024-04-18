#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseDomainEvents
// Guid:bbe1486e-9835-45d3-8d47-aea76d533b9b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 4:48:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using XiHan.Domain.Core.Events.Abstracts;

namespace XiHan.Domain.Core.Events;

/// <summary>
/// 通用领域事件
/// </summary>
public class BaseDomainEvents : IBaseDomainEvents
{
    [NotMapped]
    private readonly IList<INotification> _domainEvents = [];

    /// <summary>
    /// 获取领域事件
    /// </summary>
    /// <returns></returns>
    public IEnumerable<INotification> GetDomainEvents()
    {
        return _domainEvents;
    }

    /// <summary>
    /// 添加领域事件
    /// </summary>
    /// <param name="eventItem"></param>
    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    /// <summary>
    /// 添加领域事件
    /// </summary>
    /// <remarks>
    /// 如果不存在就添加，存在则跳过，以避免对于同样的事件触发多次（比如在一个事务中修改领域模型的多个对象）
    /// </remarks>
    /// <param name="eventItem"></param>
    public void AddDomainEventIfAbsent(INotification eventItem)
    {
        if (_domainEvents.Any(e => e.GetType() == eventItem.GetType()))
        {
            // 如果已经存在相同类型的事件，跳过添加
            return;
        }

        _domainEvents.Add(eventItem);
    }

    /// <summary>
    /// 清除领域事件
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}