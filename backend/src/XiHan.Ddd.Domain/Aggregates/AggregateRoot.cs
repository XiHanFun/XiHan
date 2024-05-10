#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AggregateRoot
// Guid:e7c0bce1-c5c4-456c-816a-d84ed63d3f45
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 22:17:56
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Ddd.Domain.Aggregates.Abstracts;
using XiHan.Ddd.Domain.Events;

namespace XiHan.Ddd.Domain.Aggregates;

/// <summary>
/// 聚合根
/// </summary>
public class AggregateRoot : DomainEvents, IAggregateRoot
{
}