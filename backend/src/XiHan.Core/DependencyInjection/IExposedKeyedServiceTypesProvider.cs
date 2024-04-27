#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IExposedKeyedServiceTypesProvider
// Guid:a1a4a4a8-cfb1-47f4-bdc4-62eecfe10639
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 22:41:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Core.Microsoft.Extensions.DependencyInjection;

namespace XiHan.Core.DependencyInjection;

/// <summary>
/// 暴露键服务类型提供器接口
/// </summary>
public interface IExposedKeyedServiceTypesProvider
{
    /// <summary>
    /// 获取暴露的服务类型
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns></returns>
    ServiceIdentifier[] GetExposedServiceTypes(Type targetType);
}