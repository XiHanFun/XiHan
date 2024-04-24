#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ServiceIdentifier
// Guid:23478219-5dbe-41d1-991c-f38d22d95b3f
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 05:02:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 服务标识符
/// </summary>
public readonly struct ServiceIdentifier : IEquatable<ServiceIdentifier>
{
    /// <summary>
    /// 服务标识
    /// </summary>
    public object? ServiceKey { get; }

    /// <summary>
    /// 服务类型
    /// </summary>
    public Type ServiceType { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceType"></param>
    public ServiceIdentifier(Type serviceType)
    {
        ServiceType = serviceType;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceKey"></param>
    /// <param name="serviceType"></param>
    public ServiceIdentifier(object? serviceKey, Type serviceType)
    {
        ServiceKey = serviceKey;
        ServiceType = serviceType;
    }

    /// <summary>
    /// 相等性比较
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(ServiceIdentifier other)
    {
        if (ServiceKey == null && other.ServiceKey == null)
        {
            return ServiceType == other.ServiceType;
        }
        else if (ServiceKey != null && other.ServiceKey != null)
        {
            return ServiceType == other.ServiceType && ServiceKey.Equals(other.ServiceKey);
        }
        return false;
    }

    /// <summary>
    /// 相等性比较
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is ServiceIdentifier identifier && Equals(identifier);
    }

    /// <summary>
    /// 获取哈希值
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        if (ServiceKey == null)
        {
            return ServiceType.GetHashCode();
        }
        unchecked
        {
            return (ServiceType.GetHashCode() * 397) ^ ServiceKey.GetHashCode();
        }
    }
}