#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ExcelColumnAttribute
// Guid:c91d6da3-b505-40e3-8919-ff31f1ea759d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/10 3:17:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.WebCore.Common.Excels;

/// <summary>
/// Excel 列属性
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ExcelColumnAttribute : Attribute
{
    /// <summary>
    /// 列名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">列名称</param>
    public ExcelColumnAttribute(string name)
    {
        Name = name;
    }
}