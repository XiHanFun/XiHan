#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DisableConventionalRegistrationAttribute
// Guid:45363cfa-e069-44e6-ad2c-b9bc13682996
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 05:15:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Framework.Core.DependencyInjection;

/// <summary>
/// 禁止常规注册特性
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class DisableConventionalRegistrationAttribute : Attribute
{
}