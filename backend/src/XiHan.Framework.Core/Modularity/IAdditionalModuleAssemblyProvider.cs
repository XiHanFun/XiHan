#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IAdditionalModuleAssemblyProvider
// Guid:a7304654-5e0f-479b-b08b-921dac954036
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 下午 05:39:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Reflection;

namespace XiHan.Framework.Core.Modularity;

/// <summary>
/// 附加模块组装提供器接口
/// </summary>
public interface IAdditionalModuleAssemblyProvider
{
    /// <summary>
    /// 获取程序集
    /// </summary>
    /// <returns></returns>
    Assembly[] GetAssemblies();
}