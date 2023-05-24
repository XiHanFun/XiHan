#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysOperation
// Guid:8177143f-3289-4de2-a4e7-160edc346292
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-22 上午 02:26:47
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统操作表
/// </summary>
/// <remarks>记录创建，修改信息</remarks>
[SugarTable(TableName = "Sys_Operation")]
public class SysOperation : BaseModifyEntity
{
    /// <summary>
    /// 操作代码
    /// </summary>
    [SugarColumn(Length = 10)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 操作名称
    /// </summary>
    [SugarColumn(Length = 10)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 操作描述
    /// </summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string? Description { get; set; }
}