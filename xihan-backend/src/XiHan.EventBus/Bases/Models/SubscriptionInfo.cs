#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SubscriptionInfo
// Guid:8375a610-2698-4a1b-9f92-6b3030e7bc80
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/12/31 4:12:57
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.EventBus.Bases.Models;

/// <summary>
/// 订阅信息模型
/// </summary>
public class SubscriptionInfo
{
    /// <summary>
    /// 集成器类型
    /// </summary>
    public Type HandlerType { get; }

    /// <summary>
    /// 是否动态
    /// </summary>
    public bool IsDynamic { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="isDynamic">是否为动态</param>
    /// <param name="handlerType">集成器类型</param>
    private SubscriptionInfo(bool isDynamic, Type handlerType)
    {
        IsDynamic = isDynamic;
        HandlerType = handlerType;
    }

    /// <summary>
    /// 订阅消息
    /// </summary>
    /// <param name="handlerType">集成器类型</param>
    /// <returns></returns>
    public static SubscriptionInfo Dynamic(Type handlerType) => new(true, handlerType);

    /// <summary>
    /// 订阅消息
    /// </summary>
    /// <param name="handlerType">集成器类型</param>
    /// <returns></returns>
    public static SubscriptionInfo Typed(Type handlerType) => new(false, handlerType);
}