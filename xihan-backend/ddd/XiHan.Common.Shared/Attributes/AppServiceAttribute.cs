#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AppServiceAttribute
// Guid:72f7e263-7daa-4767-b8e2-3e73f5e0b8e2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-12-04 下午 10:44:55
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Common.Shared.Enums;

namespace XiHan.Common.Shared;

/// <summary>
/// 应用服务标记特性
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