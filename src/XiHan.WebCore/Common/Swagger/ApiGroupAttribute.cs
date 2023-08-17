#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ApiGroupAttribute
// Guid:c097f706-455c-434b-9a66-5c202d75968b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-17 下午 02:15:10
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.WebCore.Common.Swagger;

/// <summary>
/// ApiGroupAttribute
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
public class ApiGroupAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="names"></param>
    public ApiGroupAttribute(params ApiGroupNameEnum[] names)
    {
        GroupNames = names;
    }

    /// <summary>
    /// 分组名
    /// </summary>
    public ApiGroupNameEnum[] GroupNames { get; set; }
}

/// <summary>
/// 分组信息 Attribute
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = true)]
public class GroupInfoAttribute : Attribute
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 版本
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = string.Empty;
}