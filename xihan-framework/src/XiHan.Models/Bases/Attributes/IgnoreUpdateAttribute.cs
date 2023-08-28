#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IgnoreUpdateAttribute
// Guid:ff1ef8ec-7cbb-4e43-b21f-976470644504
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/5 1:14:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Models.Bases.Attributes;

/// <summary>
/// 忽略更新种子数据特性
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class IgnoreUpdateAttribute : Attribute
{
}