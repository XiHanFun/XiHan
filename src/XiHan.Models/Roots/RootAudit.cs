#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootAudit
// long:80aaad90-961a-4c97-aca3-12f12d26e056
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:44:51
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases;

namespace XiHan.Models.Roots;

/// <summary>
/// 系统审核表
/// </summary>
[SugarTable(TableName = "RootAudit")]
public class RootAudit : BaseEntity
{
    /// <summary>
    /// 审核分类
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// 审核内容
    /// </summary>
    [SugarColumn(Length = 2000)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 审核结果
    /// </summary>
    [SugarColumn(Length = 500, IsNullable = true)]
    public string? Result { get; set; }
}