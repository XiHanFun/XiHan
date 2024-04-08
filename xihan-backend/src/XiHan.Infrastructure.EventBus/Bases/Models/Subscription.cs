#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:Subscription
// Guid:8375a610-2698-4a1b-9f92-6b3030e7bc80
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/12/31 4:12:57
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.Core.EventBus.Bases.Models;

/// <summary>
/// 订阅模型，表示一个事件订阅关系，将事件和事件处理器进行关联，以便在事件发生时触发相应的事件处理器
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="isDynamic"></param>
/// <param name="handlerType"></param>
public class Subscription(bool isDynamic, Type handlerType)
{
    /// <summary>
    /// 是否动态
    /// </summary>
    public bool IsDynamic { get; } = isDynamic;

    /// <summary>
    /// 处理器类型
    /// </summary>
    public Type HandlerType { get; } = handlerType;

    /// <summary>
    /// 订阅消息
    /// </summary>
    /// <param name="handlerType">处理器类型</param>
    /// <returns></returns>
    public static Subscription Subscript(Type handlerType) => new(false, handlerType);

    /// <summary>
    /// 订阅动态消息
    /// </summary>
    /// <param name="handlerType">处理器类型</param>
    /// <returns></returns>
    public static Subscription SubscriptDynamic(Type handlerType) => new(true, handlerType);
}