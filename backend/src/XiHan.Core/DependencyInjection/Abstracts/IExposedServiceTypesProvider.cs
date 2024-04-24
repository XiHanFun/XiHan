#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IExposedServiceTypesProvider
// Guid:a1598fb8-bcd7-4aae-be28-1759e1957a3b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 22:34:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.DependencyInjection.Abstracts;

/// <summary>
/// 暴露服务类型提供器接口
/// </summary>
public interface IExposedServiceTypesProvider
{
    /// <summary>
    /// 获取暴露的服务类型
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns></returns>
    Type[] GetExposedServiceTypes(Type targetType);
}