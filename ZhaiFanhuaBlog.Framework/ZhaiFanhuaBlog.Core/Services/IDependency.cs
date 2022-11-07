// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IServiceDependency
// Guid:baab14fc-bb5c-4782-9381-5f6e30707702
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-06 上午 12:15:20
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Core.Services;

/// <summary>
/// 全局依赖注入接口
/// </summary>
public interface IDependency
{
}

/// <summary>
/// 实现该接口将自动注册到Ioc容器，生命周期为单例，全局唯一实例
/// </summary>
public interface ISingletonDependency : IDependency
{
}

/// <summary>
/// 实现该接口将自动注册到Ioc容器，生命周期为作用域，在一个作用域中唯一实例
/// </summary>
public interface IScopeDependency : IDependency
{
}

/// <summary>
/// 实现该接口将自动注册到Ioc容器，生命周期为瞬时，每次的实例都是一个新的对象
/// </summary>
public interface ITransientDependency : IDependency
{
}