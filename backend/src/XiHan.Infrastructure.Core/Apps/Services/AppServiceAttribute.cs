#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AppServiceAttribute
// Guid:a26e13d5-0fa9-429e-ae70-9f13be16486d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 14:32:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.Core.Apps.Services;

/// <summary>
/// 服务标记
/// 如果服务是本身，直接在类上使用[AppService]；如果服务是接口，需要指定实现接口，在类上使用 [AppService(ServiceType = typeof(实现接口))]；
/// </summary>
/// <remarks>由此启发：<see href="https://www.cnblogs.com/loogn/p/10566510.html"/></remarks>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class AppServiceAttribute : Attribute
{
    /// <summary>
    /// 指定服务类型
    /// </summary>
    public Type? ServiceType { get; set; }

    /// <summary>
    /// 服务声明周期，默认注册单例
    /// </summary>
    public ServiceLifeTimeEnum ServiceLifetime { get; set; } = ServiceLifeTimeEnum.Singleton;

    /// <summary>
    /// 是否可以从第一个接口获取服务类型
    /// </summary>
    public bool IsInterfaceServiceType { get; set; } = true;
}