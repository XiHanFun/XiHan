#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ExposeServicesAttribute
// Guid:78bdfa07-51d8-4980-988f-01b59d8d9c76
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 22:34:24
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Reflection;
using XiHan.Core.DependencyInjection.Providers;
using XiHan.Core.System.Collections.Generic.Extensions;
using XiHan.Core.System.Extensions;

namespace XiHan.Core.DependencyInjection.Attributes;

/// <summary>
/// 暴露服务特性
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ExposeServicesAttribute : Attribute, IExposedServiceTypesProvider
{
    private const string DefaultInterfaceNameInitial = "I";

    /// <summary>
    /// 服务类型
    /// </summary>
    public Type[] ServiceTypes { get; }

    /// <summary>
    /// 是否包含默认服务
    /// </summary>
    public bool IncludeDefaults { get; set; }

    /// <summary>
    /// 是否包含自身
    /// </summary>
    public bool IncludeSelf { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceTypes"></param>
    public ExposeServicesAttribute(params Type[] serviceTypes)
    {
        ServiceTypes = serviceTypes ?? Type.EmptyTypes;
    }

    /// <summary>
    /// 获取暴露的服务类型
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns></returns>
    public Type[] GetExposedServiceTypes(Type targetType)
    {
        var serviceList = ServiceTypes.ToList();

        if (IncludeDefaults)
        {
            foreach (var type in GetDefaultServices(targetType))
            {
                serviceList.AddIfNotContains(type);
            }

            if (IncludeSelf)
            {
                serviceList.AddIfNotContains(targetType);
            }
        }
        else if (IncludeSelf)
        {
            serviceList.AddIfNotContains(targetType);
        }

        return [.. serviceList];
    }

    /// <summary>
    /// 获取默认服务
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static List<Type> GetDefaultServices(Type type)
    {
        var serviceTypes = new List<Type>();

        foreach (var interfaceType in type.GetTypeInfo().GetInterfaces())
        {
            var interfaceName = interfaceType.Name;
            if (interfaceType.IsGenericType)
            {
                interfaceName = interfaceType.Name.Left(interfaceType.Name.IndexOf('`'));
            }

            if (interfaceName.StartsWith(DefaultInterfaceNameInitial))
            {
                interfaceName = interfaceName.Right(interfaceName.Length - 1);
            }

            if (type.Name.EndsWith(interfaceName))
            {
                serviceTypes.Add(interfaceType);
            }
        }

        return serviceTypes;
    }
}