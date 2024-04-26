#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IntegrationServiceAttribute
// Guid:ea14a157-5c4c-453a-a0f4-23c3fa52bcbb
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-26 上午 11:19:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.Application;

/// <summary>
/// 集成服务特性
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class IntegrationServiceAttribute : Attribute
{
    /// <summary>
    /// 是否已定义或已继承
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsDefinedOrInherited<T>()
    {
        return IsDefinedOrInherited(typeof(T));
    }

    /// <summary>
    /// 是否已定义或已继承
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsDefinedOrInherited(Type type)
    {
        if (type.IsDefined(typeof(IntegrationServiceAttribute), true))
        {
            return true;
        }

        foreach (var @interface in type.GetInterfaces())
        {
            if (@interface.IsDefined(typeof(IntegrationServiceAttribute), true))
            {
                return true;
            }
        }

        return false;
    }
}