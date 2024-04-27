#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ServiceIdentifier
// Guid:d3095acd-dda3-46b0-a4b0-2f7035850ebe
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/27 22:33:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.DependencyInjection;

/// <summary>
/// 服务标识符
/// </summary>
public readonly struct ServiceIdentifier : IEquatable<ServiceIdentifier>
{
    /// <summary>
    /// 服务Key
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
    /// 是否相等
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
    /// 是否相等
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is ServiceIdentifier identifier && Equals(identifier);
    }

    /// <summary>
    /// 获取HashCode
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

    /// <summary>
    /// 相等操作符
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(ServiceIdentifier left, ServiceIdentifier right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// 不相等操作符
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(ServiceIdentifier left, ServiceIdentifier right)
    {
        return !(left == right);
    }
}