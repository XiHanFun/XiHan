#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SubscriptionInfoEntity
// Guid:8375a610-2698-4a1b-9f92-6b3030e7bc80
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/12/31 4:12:57
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.EventBus.Bases.Models;

/// <summary>
/// 订阅模型
/// </summary>
public class SubscriptionModel
{
    /// <summary>
    /// 是否动态
    /// </summary>
    public bool IsDynamic { get; }

    /// <summary>
    /// 处理器类型
    /// </summary>
    public Type HandlerType { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="isDynamic"></param>
    /// <param name="handlerType"></param>
    private SubscriptionModel(bool isDynamic, Type handlerType)
    {
        IsDynamic = isDynamic;
        HandlerType = handlerType;
    }

    /// <summary>
    /// 动态
    /// </summary>
    /// <param name="handlerType"></param>
    /// <returns></returns>
    public static SubscriptionModel Dynamic(Type handlerType)
    {
        return new SubscriptionModel(true, handlerType);
    }

    /// <summary>
    /// 非动态
    /// </summary>
    /// <param name="handlerType"></param>
    /// <returns></returns>
    public static SubscriptionModel NonDynamic(Type handlerType)
    {
        return new SubscriptionModel(false, handlerType);
    }
}