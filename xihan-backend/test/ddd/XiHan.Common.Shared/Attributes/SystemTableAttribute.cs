#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SystemTableAttribute
// Guid:91d3aa42-ee8d-49a1-9234-a361e071288d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/14 3:13:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Common.Shared.Attributes;

/// <summary>
/// 系统表特性，用于标记系统表
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class SystemTableAttribute : Attribute;