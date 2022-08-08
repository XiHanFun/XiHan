// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootAudit
// Guid:80aaad90-961a-4c97-aca3-12f12d26e056
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:44:51
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Roots;

/// <summary>
/// 系统审核表
/// </summary>
public class RootAudit : BaseEntity
{
    /// <summary>
    /// 申请人
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 审核人
    /// </summary>
    public Guid AuditAccountId { get; set; }

    /// <summary>
    /// 审核分类
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// 审核内容
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(2000)")]
    public string TheContent { get; set; } = string.Empty;

    /// <summary>
    /// 审核结果
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(500)", IsNullable = true)]
    public string? Result { get; set; }
}