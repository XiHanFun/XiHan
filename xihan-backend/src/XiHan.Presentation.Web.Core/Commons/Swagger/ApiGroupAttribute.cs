#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ApiGroupAttribute
// Guid:6ae1a31a-3cb3-4386-ba0e-aff157271bc8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 6:13:04
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Presentation.Web.Core.Commons.Swagger;

/// <summary>
/// ApiGroupAttribute
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="names"></param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
public class ApiGroupAttribute(params ApiGroupNameEnum[] names) : Attribute
{
    /// <summary>
    /// 分组名
    /// </summary>
    public ApiGroupNameEnum[] GroupNames { get; set; } = names;
}